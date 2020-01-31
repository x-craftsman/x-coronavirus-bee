using System;
using System.Collections.Generic;
using System.Text;

namespace Craftsman.Core.Infrastructure.Message
{
    public interface IMessageBroker
    {
        IList<string> GetTopics();
        bool ExistTopic(string name);
        void PublishMessage<T>(string topicName, MessageInfo<T> message);
        void PublishLog(string content, string level);
        void PublishAudit(string content, dynamic module);
        void PublishEvent(string content, dynamic eventCode);
    }
}

