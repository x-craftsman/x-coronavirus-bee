using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Craftsman.Waiter.Domain.MessageConsumer.ExchangeData
{
    /// <summary>
    /// 数据交换目标数据
    /// </summary>
    public interface ITargetData
    {
    }
    public class CommonTargetData : ITargetData
    {
        public Dictionary<string, object> DictionaryData { get; set; }
        public string JsonString
        {
            get
            {
                return JsonConvert.SerializeObject(DictionaryData);
            }
        }
    }
}
