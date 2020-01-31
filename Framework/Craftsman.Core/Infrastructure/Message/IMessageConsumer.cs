using Craftsman.Core.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace Craftsman.Core.Infrastructure.Message
{
    public interface IMessageConsumer : IServiceComponent
    {
        void Run(string topicName, string groupId);
    }
}
