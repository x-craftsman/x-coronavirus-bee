using Craftsman.Core.CustomizeException;
using Craftsman.Core.Domain;
using Craftsman.Core.Domain.Repositories;
using Craftsman.Core.Runtime;
using Craftsman.LaunchPad.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Craftsman.LaunchPad.Domain.Services
{
    public interface IConsumerService: IService
    {
        Consumer RegisterConsumer(string code, string name);
    }

    public class ConsumerService : IConsumerService
    {
        #region Property Injection 属性注入
        public IRepository<Consumer> repoConsumer { get; set; }
        public IRepository<User> repoUser { get; set; }

        public ISession session { get; set; }
        #endregion Property Injection 属性注入

        public Consumer RegisterConsumer(string code, string name)
        {
            //repoUser.Get(session.UserId);
            //var currentUser = repoUser.Get(1);
            var consumer = repoConsumer.FirstOrDefault(x => x.Code == code);
            if (consumer != null)
            {
                throw new BusinessException($"code = <{code}> 的 consumer 已存在!");
            }

            consumer = new Consumer() { Code = code, Name = name };
            consumer.GenerateOrResetKey();
            return repoConsumer.Insert(consumer);
        }
    }
}
