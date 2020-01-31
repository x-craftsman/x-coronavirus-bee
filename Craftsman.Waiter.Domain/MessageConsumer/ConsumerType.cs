using System;
using System.Collections.Generic;
using System.Text;

namespace Craftsman.Waiter.Domain.MessageConsumer
{
    /// <summary>
    /// 消费者类型
    /// </summary>
    public enum ConsumerType
    {
        /// <summary>
        /// 用户自定义
        /// </summary>
        Custom = 0,
        /// <summary>
        /// 持久化数据
        /// </summary>
        PersistentData = 1,
        /// <summary>
        /// 数据交换
        /// </summary>
        ExchangeData = 2
    }
}
