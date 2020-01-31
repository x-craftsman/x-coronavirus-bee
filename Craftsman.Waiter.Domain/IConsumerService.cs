using Craftsman.Core.Dependency;
using Craftsman.Core.Domain;
using Craftsman.Core.Infrastructure.Message;
using Craftsman.Waiter.Domain.MessageConsumer;
using System;
using System.Collections.Generic;
using System.Text;

namespace Craftsman.Waiter.Domain
{
    public interface IConsumerService : IService
    {
        IMessageConsumer CreateConsumer(ConsumerType type);
    }

    public class ConsumerService : IConsumerService
    {
        public IMessageConsumer CreateConsumer(ConsumerType type)
        {
            IMessageConsumer consumer = null;
            switch (type)
            {
                case ConsumerType.PersistentData:
                    consumer = IocFactory.CreateObject<PersistentDataConsumer>();
                    break;
                case ConsumerType.ExchangeData:
                    consumer = IocFactory.CreateObject<ExchangeDataConsumer>();
                    break;
                case ConsumerType.Custom:
                    consumer = IocFactory.CreateObject<CustomConsumer>();
                    break;
                default:
                    throw new Exception($"未知的ConsumerType:{type}");
            }
            return consumer;
        }
    }
}
