using Confluent.Kafka;
using Craftsman.Core.Infrastructure.Config;
using Craftsman.Core.Infrastructure.Logging;
using Craftsman.Core.Runtime;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Craftsman.Core.Infrastructure.Message
{
    public abstract class BaseConsumer : IMessageConsumer
    {
        protected IConfigManager configManager { get; set; }
        protected ILogger logger { get; set; }
        protected ISession session;

        public BaseConsumer(
            IConfigManager configManager,
            ILogger logger,
            ISession session
        )
        {
            this.configManager = configManager;
            this.logger = logger;
            this.session = session;
        }

        public void Run(string topicName, string groupId)
        {
            var conf = new ConsumerConfig
            {
                GroupId = groupId,
                BootstrapServers = configManager.Kafka.BootstrapServers,
                AutoOffsetReset = AutoOffsetReset.Latest
            };

            using (var consumer = new ConsumerBuilder<Ignore, string>(conf).Build())
            {
                consumer.Subscribe(topicName);
                /* TODO
                 * var cts = new CancellationTokenSource();
                 * 服务停止的时候需要定义退出策略
                 */
                try
                {
                    while (true)
                    {
                        try
                        {
                            var cr = consumer.Consume();
                            var msgInfo = JsonConvert.DeserializeObject<MessageInfo>(cr.Message.Value);
                            var context = new MessageContext() { TopicName = cr.Topic };

                            Process(msgInfo, context);
                        }
                        catch (ConsumeException e)
                        {
                            logger.Error($"Error occured: {e.Error.Reason}");
                        }
                    }
                }
                catch (OperationCanceledException e)
                {
                    logger.Info($"Info occured: {e.Message}");
                    // Ensure the consumer leaves the group cleanly and final offsets are committed.
                    consumer.Close();
                }
                catch (Exception e)
                {
                    logger.Error($"Error occured: {e.Message}");
                    consumer.Close();
                }
            }
        }

        public abstract void Process(MessageInfo message, MessageContext context);
    }
}
