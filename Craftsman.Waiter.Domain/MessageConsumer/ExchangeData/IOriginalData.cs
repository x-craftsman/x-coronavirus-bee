using Craftsman.Core.Infrastructure.Message;
using System;
using System.Collections.Generic;
using System.Text;

namespace Craftsman.Waiter.Domain.MessageConsumer.ExchangeData
{
    /// <summary>
    /// 数据交换原始数据
    /// </summary>
    public interface IOriginalData
    {
        MessageMetadata Metadata { get; set; }
    }

    public class CommonOriginalData : IOriginalData
    {
        public string OriginalData { get; set; }
        public dynamic DynamicData { get; set; }

        public MessageMetadata Metadata { get; set; }

    }
}
