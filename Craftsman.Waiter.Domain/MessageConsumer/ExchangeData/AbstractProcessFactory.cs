using Craftsman.Core.CustomizeException;
using Craftsman.Core.Dependency;
using Craftsman.Core.Domain;
using Craftsman.Core.Domain.Repositories;
using Craftsman.Core.Infrastructure.Message;
using Craftsman.Waiter.Domain.Entities;
using Craftsman.Waiter.Domain.RepositoryContract;
using Craftsman.Waiter.Domain.SysEnum;
using System;
using System.Collections.Generic;
using System.Text;

namespace Craftsman.Waiter.Domain.MessageConsumer.ExchangeData
{
    public abstract class AbstractProcessFactory
    {
        public abstract IMessageResolver CreateMessageResolver();
        public abstract IDataMapper CreateDataMapper();
        public abstract IProcessor CreateProcessor();
    }

    public class ProcessFactoryCreator : IServiceComponent
    {
        private IServiceSubscriberRepository _repoServiceSubscriber;
        public ProcessFactoryCreator(
            IServiceSubscriberRepository repoServiceSubscriber
        )
        {
            _repoServiceSubscriber = repoServiceSubscriber;
        }

        public AbstractProcessFactory Create(MessageMetadata metadata)
        {
            var subscriber = _repoServiceSubscriber.GetServiceSubscriber(metadata.ActionCode, metadata.TenantCode);
            if (subscriber == null || subscriber.MappingRule == null)
            {
                throw new BusinessException($"不存在对应的订阅信息或映射逻辑：ActionCode:{metadata.ActionCode} - TenantCode:{metadata.TenantCode}");
            }

            if (subscriber.Type == ServiceSubscriberType.Custom)
            {
                throw new Exception("目前不支持自定义类型的数据交换规则！");
            }

            AbstractProcessFactory factory;
            switch (subscriber.MappingRule.Type)
            {
                case MappingRuleType.Json:
                    factory = IocFactory.CreateObject<StandardScmApiProcessFactory>();
                    break;
                case MappingRuleType.Text:
                    factory = IocFactory.CreateObject<TextScmApiProcessFactory>();
                    break;
                case MappingRuleType.Unknown:
                default:
                    throw new Exception($"未能识别的映射逻辑：<{subscriber.MappingRule.Type}>！");
            }
            return factory;
        }
    }
}
