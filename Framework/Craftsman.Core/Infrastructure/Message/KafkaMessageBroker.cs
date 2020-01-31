using Confluent.Kafka;
using Craftsman.Core.Dependency;
using Craftsman.Core.Infrastructure.Config;
using Craftsman.Core.Infrastructure.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Craftsman.Core.Infrastructure.Message
{
    /// <summary>
    /// Kafka 版本的 Message Broker（需要优化）
    /// </summary>
    public class KafkaMessageBroker : AbstractMessageBroker
    {
        public IConfigManager ConfigManager { get; set; }
        public ILogger Logger { get; set; }

        public override IList<string> GetTopics()
        {
            var topics = new List<string>();
            var adminConf = new AdminClientConfig { BootstrapServers = ConfigManager.Kafka.BootstrapServers };
            using (var client = new AdminClientBuilder(adminConf).Build())
            {
                var metaData = client.GetMetadata(TimeSpan.FromSeconds(100));
                foreach (var data in metaData.Topics)
                {
                    if (data.Topic != "__consumer_offsets")
                    {
                        topics.Add(data.Topic);
                    }
                    
                }
            }
            return topics;
        }
        public override bool ExistTopic(string name)
        {
            var isExist = false;
            var adminConf = new AdminClientConfig { BootstrapServers = ConfigManager.Kafka.BootstrapServers };
            using (var client = new AdminClientBuilder(adminConf).Build())
            {
                var metaData = client.GetMetadata(TimeSpan.FromSeconds(100));
                if (metaData.Topics.Exists(x=>x.Topic== name))
                {
                    isExist = true;
                }
            }
            return isExist;
        }
        protected override void SendMessage<T>(string topic, MessageInfo<T> content)
        {
            var conf = new ProducerConfig { BootstrapServers = ConfigManager.Kafka.BootstrapServers };

            //TODO: Check Topic是否存在

            //TODO: 定义消息回写逻辑
            Action<DeliveryReport<Null, string>> handler = result =>
            {
                if (result.Error.IsError)
                {
                    Logger.Error($"Delivery Error: {result.Error.Reason}");
                }
                else
                {
                    Logger.Info($"Delivered message to {result.Topic}:{result.TopicPartitionOffset}");
                }
            };

            //向Kafka 发送消息
            using (var producer = new ProducerBuilder<Null, string>(conf).Build())
            {
                producer.Produce(topic, new Message<Null, string> { Value = JsonConvert.SerializeObject(content) }, handler);
                
                // wait for up to 10 seconds for any inflight messages to be delivered.
                producer.Flush(TimeSpan.FromSeconds(10));
            }
            //记录消息信息到数据库
        }
    }
}
