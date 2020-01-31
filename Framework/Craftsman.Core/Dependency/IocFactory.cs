using Autofac;
using Autofac.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace Craftsman.Core.Dependency
{
    public static class IocFactory
    {
        private static IContainer _container;

        public static IContainer Container { get { return _container; } }

        static IocFactory() { }

        public static void Set(IContainer container)
        {
            _container = container;
        }
        public static T CreateObject<T>()
        {
            return _container.Resolve<T>();
        }
        public static T CreateObject<T>(params Parameter[] parameters)
        {
            return _container.Resolve<T>(parameters);
        }
    }
}
