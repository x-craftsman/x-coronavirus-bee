using Craftsman.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Craftsman.Mercury.Domain.Entities
{
    /// <summary>
    /// 消息消费者（聚合根）
    /// </summary>
    public class Subscriber : IAggregateRoot
    {
        public int Id { get; set; }

        public bool IsTransient()
        {
            throw new NotImplementedException();
        }
    }
}
