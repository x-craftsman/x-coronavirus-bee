using Craftsman.Core.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace Craftsman.Waiter.Domain.MessageConsumer.ExchangeData
{
    /// <summary>
    /// 数据映射逻辑
    /// </summary>
    public interface IDataMapper : IServiceComponent
    {
        ITargetData MapperTo(IOriginalData orgData);
    }
}
