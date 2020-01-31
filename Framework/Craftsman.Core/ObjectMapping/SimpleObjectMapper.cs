using AutoMapper;
using Craftsman.Core.Dependency;
using System;
using System.Collections.Generic;
using System.Text;

namespace Craftsman.Core.ObjectMapping
{
    public sealed class SimpleObjectMapper : IObjectMapper//, ISingletonDependency
    {
        /// <summary>
        /// Singleton instance.
        /// </summary>
        public static SimpleObjectMapper Instance { get; } = new SimpleObjectMapper();

        public TDestination Map<TDestination>(object source)
        {
            //TODO: 这个实现简单粗暴，可能会影响性能。项目紧张 后面有时间再修改....
            var config = new MapperConfiguration(cfg =>
                cfg
                    //.CreateMap<TSource, TDestination>()
                    .CreateMap(source.GetType(), typeof(TDestination))
                    .ForAllMembers(opt => opt.Condition((src, target, sMember) => sMember != null))
            );

            var mapper = config.CreateMapper();
            return mapper.Map<TDestination>(source);
        }

        public TDestination Map<TSource, TDestination>(TSource source, TDestination destination)
        {
            //TODO: 这个实现简单粗暴，可能会影响性能。项目紧张 后面有时间再修改....
            var config = new MapperConfiguration(cfg =>
                cfg
                    .CreateMap<TSource, TDestination>()
                    .ForAllMembers(opt => opt.Condition((src, target, sMember) => sMember != null))
            );
            var mapper = config.CreateMapper();
            return mapper.Map(source, destination);
        }
    }
}
