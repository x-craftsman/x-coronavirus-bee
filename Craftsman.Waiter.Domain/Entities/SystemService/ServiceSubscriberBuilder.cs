using Craftsman.Core.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace Craftsman.Waiter.Domain.Entities
{
    /// <summary>
    /// 服务订阅 Builder
    /// </summary>
    public class ServiceSubscriberBuilder : IServiceComponent
    {
        public Guid Guid { get; set; }
        public ServiceSubscriberBuilder()
        {
            Guid = Guid.NewGuid();
        }

        public ServiceSubscriberBuilder SetSystemService(SystemService systemService) { return this; }
        public ServiceSubscriberBuilder SetTenant(Tenant tenant) { return this; }

        public ServiceSubscriberBuilder SetMappingLogic(ServiceSubscriberMappingRule mappingRule, ServiceSubscriberCustomLogic customLogic)
        {
            //设置映射逻辑：标准&自定义
            return this;
        }

        public ServiceSubscriber Build() { return null; }
    }
}
