using Craftsman.Core.Domain;
using Craftsman.Core.Infrastructure.Message;
using System;
using System.Collections.Generic;
using System.Text;

namespace Craftsman.Waiter.Domain.MessageConsumer.ExchangeData
{
    /// <summary>
    /// 数据处理程序
    /// </summary>
    public interface IProcessor : IServiceComponent
    {
        void SendData(ITargetData targetData, MessageMetadata metadata);
    }
}
