using Craftsman.Core.CustomizeException;
using Craftsman.Core.Domain;
using Craftsman.Core.Domain.Repositories;
using Craftsman.Core.Infrastructure.Logging;
using Craftsman.Core.Infrastructure.Message;
using Craftsman.Core.Runtime;
using Craftsman.Mercury.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Craftsman.Mercury.Domain
{
    public interface IMessageService : IService
    {
        #region Topic 相关
        void CreateTopic(Topic topic);
        void UpdateTopic(Topic topic);
        void DeleteTopic(Topic topic);
        IList<Topic> GetTopics();
        Topic GetTopic(string name);
        #endregion

        #region Message 相关
        void PublishMessage<T>(string topicName, MessageInfo<T> message);
        #endregion

        #region Subscription 相关
        void SubscribeTopic(string topicName, Subscription subscription);
        void ModifySubscriptionInfo(string topicName, Subscription subscription);
        void UnsubscribeTopic(string topicName, string subscriptionName);
        # endregion
    }

    public class MessageService : IMessageService
    {
        public ILogger Logger { get; set; }
        public ISession Session { get; set; }
        public IMessageBroker MessageBroker { get; set; }

        public IRepository<Topic> repoTopic { get; set; }
        public IRepository<Subscription> repoSubscription { get; set; }
        public void CreateTopic(Topic topic)
        {
            //some 
            topic.SetCommonFileds(Session.CurrentUser, true); //设置Default字段

            //从Kafka 创建 
            /*
             * MessageBroker.CreateTopic();
             */
            topic.Id = repoTopic.InsertAndGetId(topic);
        }

        public void DeleteTopic(Topic topic)
        {
            throw new NotImplementedException();
        }

        public IList<Topic> GetTopics()
        {
            //从Kafka 获取 Topic 名称列表
            var topicNames = MessageBroker.GetTopics();
            var tempTopics = repoTopic.GetAllList();
            var topics = tempTopics.FindAll(x => tempTopics.Exists(y => y.Name == x.Name));
            return topics;
        }

        public Topic GetTopic(string name)
        {
            return this.GetTopics().FirstOrDefault(x => x.Name == name);
        }

        public void UpdateTopic(Topic topic)
        {
            throw new NotImplementedException();
        }
        public void PublishMessage<T>(string topicName, MessageInfo<T> message)
        {
            MessageBroker.PublishMessage(topicName, message);
        }

        public void SubscribeTopic(string topicName, Subscription subscription)
        {
            var topic = repoTopic.FirstOrDefault(x => x.Name == topicName);

            if (topic == null)
            {
                throw new VerifyException($"Not exits topic named:{topicName}");
            }

            var entity = repoSubscription.FirstOrDefault(x => x.TopicId == topic.Id && x.Name == subscription.Name);
            if (entity != null)
            {
                throw new VerifyException($"Exits subscription like:topic is <{topicName}> subscription name is:<{subscription.Name}>");
            }
            subscription.TopicId = topic.Id;
            subscription.SetCommonFileds(Session.CurrentUser, true);

            //subscription.Id = repoSubscription.InsertAndGetId(subscription);
            repoSubscription.Insert(subscription);
        }

        public void ModifySubscriptionInfo(string topicName, Subscription subscription)
        {
            var topic = repoTopic.FirstOrDefault(x => x.Name == topicName);

            if (topic == null)
            {
                throw new VerifyException($"Not exits topic named:{topicName}");
            }

            var entity = repoSubscription.FirstOrDefault(x => x.TopicId == topic.Id && x.Name == subscription.Name);
            if (entity == null)
            {
                throw new VerifyException($"Not exits subscription like:topic is <{topicName}> subscription name is:<{subscription.Name}>");
            }

            //目前只是支持修改 NotifyStrategy
            entity.NotifyStrategy = subscription.NotifyStrategy;

            subscription.SetCommonFileds(Session.CurrentUser);

            //subscription.Id = repoSubscription.InsertAndGetId(subscription);
            repoSubscription.Update(entity);
        }

        public void UnsubscribeTopic(string topicName, string subscriptionName)
        {
            var topic = repoTopic.FirstOrDefault(x => x.Name == topicName);

            if (topic == null)
            {
                throw new VerifyException($"Not exits topic named:{topicName}");
            }

            var entity = repoSubscription.FirstOrDefault(x => x.TopicId == topic.Id && x.Name == subscriptionName);
            if (entity == null)
            {
                throw new VerifyException($"Not exits subscription like:topic is <{topicName}> subscription name is:<{subscriptionName}>");
            }

            repoSubscription.Delete(entity.Id);
        }
    }
}
