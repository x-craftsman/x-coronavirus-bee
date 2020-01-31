using Craftsman.Core.Dependency;
using Craftsman.LaunchPad.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Craftsman.LaunchPad.Domain.AuthCenter.Factories
{
    public class LaunchPadAuthFactory : AuthFactory
    {
        public LaunchPadAuthFactory(ConsumerApp consumer) : base(consumer) { }

        protected override IUserAuthenticationCreator BuildUserAuthenticationCreator()
        {
            //TODO: Use Ioc container
            return IocFactory.CreateObject<LaunchPadAuthenticationCreator>();
            //return new LaunchPadAuthenticationCreator();
        }

        protected override IUserAuthorizationCreator BuildUserAuthorizationCreator()
        {
            //TODO: Use Ioc container
            return IocFactory.CreateObject<NoAuthorizationCreator>();
            //return new NoAuthorizationCreator();
        }

        protected override IUserInfomationCreator BuildUserInfomationCreator()
        {
            //TODO: Use Ioc container
            return IocFactory.CreateObject<LaunchPadUserInfomationCreator>();
            //return new LaunchPadUserInfomationCreator();
        }
    }
}
