using Craftsman.Core.CustomizeException;
using Craftsman.Core.Infrastructure.Config;
using Craftsman.Core.Infrastructure.Logging;
using Craftsman.Core.Infrastructure.Message;
using Craftsman.Core.Runtime;
using Craftsman.Waiter.Domain.Entities;
using Craftsman.Waiter.Domain.MessageConsumer.ExchangeData;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;

namespace Craftsman.Waiter.Domain.MessageConsumer
{
    public class ExchangeDataConsumer : BaseConsumer
    {
        private ProcessFactoryCreator processFactoryCreator { get; set; }

        public ExchangeDataConsumer(
            IConfigManager configManager,
            ILogger logger,
            ISession session,
            ProcessFactoryCreator processFactoryCreator
        ) : base(configManager, logger, session)
        {
            this.processFactoryCreator = processFactoryCreator;
        }

        public override void Process(MessageInfo message, MessageContext context)
        {
            /*
             * 构造上下文，以及Process对象
             */
            //if (context.TopicName != $"std-{message.Metadata.TenantCode}")
            //{
            //    var msg = $"ExchangeDataConsumer:订阅主题和租户信息不匹配！<{context.TopicName}> - <{message.Metadata.TenantCode}>";
            //    logger.Error(msg);
            //    throw new BusinessException(msg);
            //}

            // TODO: 需要测试 consumer 连接数量对应的性能指标。 5 个consumer 指标最佳。
            //【方案一】：简单实现 - 配置管理流程的各个部分
            //【方案二】: 抽象工厂实现 - 统一代码管理流程的各个部分

            var factory = processFactoryCreator.Create(message.Metadata);

            //消息的解析
            var messageResolver = factory.CreateMessageResolver();
            IOriginalData orgData = messageResolver.Parsing(message.Body.ToString());

            orgData.Metadata = message.Metadata;

            //映射逻辑
            var dataMapper = factory.CreateDataMapper();
            var targetData = dataMapper.MapperTo(orgData);

            //调用处理程序
            var processor = factory.CreateProcessor();
            processor.SendData(targetData, message.Metadata);
        }
    }
}
