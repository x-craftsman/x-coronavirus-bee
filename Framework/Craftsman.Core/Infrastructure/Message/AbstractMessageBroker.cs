using System;
using System.Collections.Generic;
using System.Text;

namespace Craftsman.Core.Infrastructure.Message
{
    public abstract class AbstractMessageBroker : IMessageBroker
    {
        public abstract IList<string> GetTopics();
        public abstract bool ExistTopic(string name);

        protected abstract void SendMessage<T>(string topic, MessageInfo<T> content);
        


        public void PublishAudit(string content, dynamic module)
        {
            var topic = "system-audit";
            SendMessage(topic, new MessageInfo { Body = content, Tag = module.ToString() });
        }

        public void PublishEvent(string content, dynamic eventCode)
        {
            var topic = "system-event";
            SendMessage(topic, new MessageInfo { Body = content, Tag = eventCode.ToString() });
        }

        public void PublishLog(string content, string level)
        {
            var topic = "system-log";
            SendMessage(topic, new MessageInfo { Body = content, Tag = level.ToString() });
        }
        public void PublishMessage<T>(string topicName, MessageInfo<T> message)
        {
            var topic = topicName; //"custom-message";
            var messageInfo = new MessageInfo<T>
            {
                Body = message.Body,
                Tag = "custom-message" + message.Tag,
                Metadata = message.Metadata
            };
            SendMessage(topic, messageInfo);
        }
    }
}
