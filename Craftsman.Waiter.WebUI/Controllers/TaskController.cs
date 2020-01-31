using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Craftsman.Core.Domain;
using Craftsman.Waiter.Domain;
using Craftsman.Waiter.Domain.MessageConsumer;
using Hangfire;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Craftsman.Waiter.WebUI.Controllers
{
    [Route("api/task")]
    public class TaskController : Controller, IController
    {
        public IConsumerService ConsumerService { get; set; }

        // POST api/<controller>
        [HttpPost("{type}")]
        public void CreateTask([FromBody]dynamic data, [FromRoute] string type = "PersistentData")
        {
            var topicName = (string)data.topicName;

            var consumerType = ConsumerType.PersistentData;
            switch (type.ToLower())
            {
                case "persistentdata":
                case "persistent-data":
                    consumerType = ConsumerType.PersistentData;
                    break;
                case "custom":
                    consumerType = ConsumerType.Custom;
                    break;
                case "exchangedata":
                case "exchange-data":
                    consumerType = ConsumerType.ExchangeData;
                    break;
                default:
                    throw new Exception($"未知的消费者类型：{type}");
            }


            var consumer = ConsumerService.CreateConsumer(consumerType);

            BackgroundJob.Enqueue(() => consumer.Run(topicName, $"group-{type}"));
        }

        //[HttpPost("{type}")]
        //public void CreateTaskForType([FromBody]dynamic data, [FromRoute]string type)
        //{
        //    var topicName = (string)data.topicName;
        //    var consumer = ConsumerService.CreateConsumer(ConsumerType.PersistentData);

        //    BackgroundJob.Enqueue(() => consumer.Run(topicName, "default"));
        //}

        // PUT api/<controller>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        public class TestVM
        {
            [Required(ErrorMessage = "consumer key 不能为空！")]
            public string ConsumerKey { get; set; }
        }
        [HttpPut("test")]
        public void Test([FromBody]TestVM data)
        {
        }

        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
