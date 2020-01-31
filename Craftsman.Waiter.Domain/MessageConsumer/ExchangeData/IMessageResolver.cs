using Confluent.Kafka;
using Craftsman.Core.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace Craftsman.Waiter.Domain.MessageConsumer.ExchangeData
{
    /// <summary>
    /// 消息解析者（接口）
    /// </summary>
    public interface IMessageResolver : IServiceComponent
    {
        /// <summary>
        /// 解析数据
        /// </summary>
        /// <param name="consumer">消息队列消费者返回数据</param>
        /// <returns></returns>
        IOriginalData Parsing(string body);
    }
}
