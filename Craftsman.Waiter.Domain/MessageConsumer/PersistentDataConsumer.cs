using Craftsman.Core.Domain;
using Craftsman.Core.Domain.Repositories;
using Craftsman.Core.Infrastructure.Config;
using Craftsman.Core.Infrastructure.Logging;
using Craftsman.Core.Infrastructure.Message;
using Craftsman.Core.Runtime;
using Craftsman.Waiter.Domain.Entities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace Craftsman.Waiter.Domain.MessageConsumer
{
    /// <summary>
    /// 数据持久化消费程序
    /// </summary>
    public class PersistentDataConsumer : BaseConsumer
    {
        private IMessageBroker messageBroker;
        private IRepository<MessageRecord> repoMessageRecord;

        public PersistentDataConsumer(
            IConfigManager configManager,
            ILogger logger,
            ISession session,
            IMessageBroker messageBroker,
            IRepository<MessageRecord> repoMessageRecord
        ):base(configManager, logger, session)
        {
            this.messageBroker = messageBroker;
            this.session = session;
            this.repoMessageRecord = repoMessageRecord;
        }

        public override void Process(MessageInfo message, MessageContext context)
        {
            var messageRecord = new MessageRecord()
            {
                Body = message.Body.ToString(),
                State = 0,
                Metadata = message.Metadata.ToString(),
                Tag = message.Tag,
                TopicId = 0 //TODO: ????
            };
            messageRecord.SetCommonFileds(session.CurrentUser, true);

            repoMessageRecord.Insert(messageRecord);
        }
    }
}
