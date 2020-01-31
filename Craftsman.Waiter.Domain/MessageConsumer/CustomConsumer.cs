using Craftsman.Core.Infrastructure.Config;
using Craftsman.Core.Infrastructure.Logging;
using Craftsman.Core.Infrastructure.Message;
using Craftsman.Core.Runtime;
using System;
using System.Collections.Generic;
using System.Text;

namespace Craftsman.Waiter.Domain.MessageConsumer
{

    public class CustomConsumer : BaseConsumer
    {
        public CustomConsumer(
           IConfigManager configManager,
           ILogger logger,
           ISession session
        ) : base(configManager, logger, session) { }

        public override void Process(MessageInfo message, MessageContext context)
        {
            throw new NotImplementedException();
        }
    }
}
