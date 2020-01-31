using Autofac;
using Autofac.Extensions.DependencyInjection;
using Craftsman.Core.Domain;
using Craftsman.Core.Domain.Repositories;
using Craftsman.Core.Infrastructure.Config;
using Craftsman.Core.Infrastructure.FileManager;
using Craftsman.Core.Infrastructure.Logging;
using Craftsman.Core.Infrastructure.Message;
using Craftsman.Core.ObjectMapping;
using Craftsman.Core.Runtime;
using Craftsman.Core.Runtime.Caching;
using Craftsman.Core.Runtime.HardCode;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Reflection;

namespace Craftsman.Core.Dependency.Installers
{
    /// <summary>
    /// 核心类库装载（生命周期起点）
    /// </summary>
    public  class CoreInstaller
    {
        public IContainer ApplicationContainer { get; private set; }
        public void Install(string projectName, WebPortalType webType = WebPortalType.WebApi)
        {
            Install(null, projectName, webType);
        }
        public IServiceProvider Install(IServiceCollection services, string projectName, WebPortalType webType = WebPortalType.WebApi)
        {
            //TODO: 【Beta版本】简单实现，后续详细设计。
            var builder = new ContainerBuilder();

            //测试代码services == null, 此处需要重构。
            if (services != null)
            {
                builder.Populate(services);
            }
            

            // TODO: Add to config file.
            var strAssembly = (webType == WebPortalType.WebApplication ? "WebUI" : "WebApi");
            //var assemblyApi = Assembly.Load($"Craftsman.{projectName}.WebApi");
            var assemblyWebPortal = Assembly.Load($"Craftsman.{projectName}.{strAssembly}");
            var assemblyDomain = Assembly.Load($"Craftsman.{projectName}.Domain");
            var assemblyInfrastructure = Assembly.Load($"Craftsman.{projectName}.Infrastructure");
            var assemblyCore = Assembly.Load("Craftsman.Core");
            var assemblies = new Assembly[] { assemblyWebPortal, assemblyDomain, assemblyInfrastructure, assemblyCore };


            var typeInfrastructure = assemblyInfrastructure.GetTypes();

            // Autofac doc - http://autofac.readthedocs.io
            //程序结构级别注入
            //Register Framework Dependency.
            builder.RegisterAssemblyTypes(assemblyWebPortal)
                .PropertiesAutowired()
                .Where(type => typeof(IController).IsAssignableFrom(type) && !type.GetTypeInfo().IsAbstract);

            builder.RegisterAssemblyTypes(assemblies)
                .PropertiesAutowired()
                .Where(type => typeof(ITransientDependency).IsAssignableFrom(type) && !type.GetTypeInfo().IsAbstract)
                .AsImplementedInterfaces().InstancePerLifetimeScope();

            builder.RegisterAssemblyTypes(assemblies)
                .PropertiesAutowired()
                .Where(type => typeof(ISingletonDependency).IsAssignableFrom(type) && !type.GetTypeInfo().IsAbstract)
                .AsImplementedInterfaces().SingleInstance();

            builder.RegisterAssemblyTypes(assemblies)
                .PropertiesAutowired()
                .Where(type => typeof(IPolymorphismDependency).IsAssignableFrom(type) && !type.GetTypeInfo().IsAbstract)
                .AsSelf().InstancePerLifetimeScope();


            //注册 IRepository<TEntity> 到Ioc容器
            #region Ef RepositoryBase
            //var efReposutoryType = GetImplementForGenericType(assemblyInfrastructure, typeof(EfRepositoryBase<,>));
            //if (efReposutoryType != null)
            //{
            //    builder.RegisterGeneric(GetImplementForGenericType(assemblyInfrastructure, typeof(EfRepositoryBase<,>)))
            //        .As(typeof(IRepository<,>)).InstancePerDependency();
            //    builder.RegisterGeneric(GetImplementForGenericType(assemblyInfrastructure, typeof(EfRepositoryBase<>)))
            //        .As(typeof(IRepository<>)).InstancePerDependency();
            //}

            #endregion

            #region Dapper RepositoryBase
            //builder.RegisterGeneric(typeof(DapperRepositoryBase<,>)).As(typeof(IRepository<,>)).InstancePerDependency();
            //builder.RegisterGeneric(typeof(DapperRepositoryBase<>)).As(typeof(IRepository<>)).InstancePerDependency();
            builder.RegisterGeneric(typeof(DapperRepositoryBase<,>)).As(typeof(IRepository<,>)).InstancePerLifetimeScope();
            builder.RegisterGeneric(typeof(DapperRepositoryBase<>)).As(typeof(IRepository<>)).InstancePerLifetimeScope();
            #endregion


            //框架组件级别注入
            // TODO: Add to config file.

            //Session组件注入
            builder.RegisterType<HardCodeSession>().As<ISession>().PropertiesAutowired().InstancePerLifetimeScope();    //Register ISession： 请求级别。
                                                
            //Log
            Type lotType = FindLogType(typeInfrastructure);
            builder.RegisterType(lotType).As<ILogger>().PropertiesAutowired().InstancePerLifetimeScope();
            //builder.RegisterType<DemoLogger>().As<ILogger>().PropertiesAutowired().SingleInstance();                //Register ILogger：单例。

            #region TODO: 需要优化处理方式，使用框架接口，ISingletonDependency，ITransientDependency，IPolymorphismDependency
            //外部资源组件注入（TODO 准备移除～～， IConfigManager替代）
            builder.RegisterType<DbResourceManager>().As<IDbResourceManager>().PropertiesAutowired().InstancePerLifetimeScope();

            //配置组件注入
            builder.RegisterType<HardCodeConfigManager>().As<IConfigManager>().PropertiesAutowired().SingleInstance();

            //消息组件注入
            builder.RegisterType<KafkaMessageBroker>().As<IMessageBroker>().PropertiesAutowired().SingleInstance();

            //对象映射组件注入
            builder.RegisterType<SimpleObjectMapper>().As<IObjectMapper>().PropertiesAutowired().SingleInstance();
            
            //缓存组件注入
            builder.RegisterType<DemoCacheManager>().As<ICacheManager>().PropertiesAutowired().SingleInstance();

            //文件服务注入
            builder.RegisterType<SftpFileManager>().As<IFileManager>().PropertiesAutowired().SingleInstance();

            #endregion

            this.ApplicationContainer = builder.Build();
            IocFactory.Set(this.ApplicationContainer);
            return new AutofacServiceProvider(this.ApplicationContainer);
        }

        private Type FindLogType(Type[] typeInfrastructure)
        {
            var defaultLogType = typeof(DemoLogger);
            var lotInterface = typeof(ILogger);
            Type lotType = defaultLogType;
            foreach (var t in typeInfrastructure)
            {
                if (lotInterface.IsAssignableFrom(t))
                {
                    lotType = t;
                    break;
                }
            }

            return lotType;
        }
        //protected Type GetImplementForGenericType(Assembly assembly, Type genericType)
        //{
        //    var repoType = assembly.GetTypes()
        //       .Where(type =>
        //       {
        //           var interfaces = type.GetInterfaces().ToList();
        //           var hasRepo = false;
        //           hasRepo = interfaces.Exists(_interface => _interface.IsGenericType && genericType == _interface.GetGenericTypeDefinition()) || hasRepo;
        //           return hasRepo;
        //       }).SingleOrDefault();

        //    return repoType;
        //}

        protected Type GetImplementForGenericType(Assembly assembly, Type genericType)
        {
            var repoType = assembly.GetTypes()
               .Where(type =>
               {
                   var hasRepo = type.BaseType.IsGenericType && (type.BaseType.GetGenericTypeDefinition() == genericType);
                   return hasRepo;
               })
               .SingleOrDefault();

            return repoType;
        }
    }
}
