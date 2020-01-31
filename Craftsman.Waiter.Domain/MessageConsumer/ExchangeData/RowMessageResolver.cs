using System;
using System.Collections.Generic;
using System.Text;
using Confluent.Kafka;
using Newtonsoft.Json;

namespace Craftsman.Waiter.Domain.MessageConsumer.ExchangeData
{
    internal class RowMessageResolver : IMessageResolver
    {
        public IOriginalData Parsing(string body)
        {
            var dynamicData = body;//sonConvert.DeserializeObject<dynamic>(body);
            var data = new CommonOriginalData()
            {
                OriginalData = body,
                DynamicData = dynamicData
            };

            return data;
        }
    }
}
