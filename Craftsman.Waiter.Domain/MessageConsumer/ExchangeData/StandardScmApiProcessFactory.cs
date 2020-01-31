using Craftsman.Core.Dependency;
using Craftsman.Core.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace Craftsman.Waiter.Domain.MessageConsumer.ExchangeData
{
    /// <summary>
    /// 标准Apis Scm 调用流程 创建工厂
    /// </summary>
    class StandardScmApiProcessFactory : AbstractProcessFactory, IServiceComponent
    {
        public IDataMapper DataMapper { get; set; }
        public IMessageResolver MessageResolver { get; set; }
        public IProcessor Processor { get; set; }
        public override IDataMapper CreateDataMapper()
        {
            return IocFactory.CreateObject<ObjectFieldMapper>();
        }

        public override IMessageResolver CreateMessageResolver()
        {
            return IocFactory.CreateObject<JsonMessageResolver>();
        }

        public override IProcessor CreateProcessor()
        {
            return IocFactory.CreateObject<StandardScmApiProcessor>();
        }
    }
}
