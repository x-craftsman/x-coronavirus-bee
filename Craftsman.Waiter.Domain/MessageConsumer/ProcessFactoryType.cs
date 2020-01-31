using System;
using System.Collections.Generic;
using System.Text;

namespace Craftsman.Waiter.Domain.MessageConsumer
{
    /// <summary>
    /// 抽象工厂类型
    /// </summary>
    public enum ProcessFactoryType
    {
        /// <summary>
        /// Json数据交换
        /// </summary>
        Json = 0,
        /// <summary>
        /// 文本数据交换
        /// </summary>
        Text = 1,
        /// <summary>
        /// 自定义数据交换
        /// </summary>
        Custom = 2
    }
}
