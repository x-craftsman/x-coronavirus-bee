//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;
//using Craftsman.Core.Domain;
//using Craftsman.Core.Infrastructure.Logging;
//using Craftsman.Core.Runtime;
//using Craftsman.Mercury.Domain;
//using Microsoft.AspNetCore.Mvc;
//using Craftsman.Mercury.Domain.Entities;

//namespace Craftsman.Mercury.WebApis.Controllers
//{
//    [Route("api/topics")]
//    [ApiController]
//    public class SubscriptionController : ControllerBase, IController
//    {
//        public ISession Session { get; set; }
//        public ILogger Logger { get; set; }

//        public IMessageService MessageService { get; set; }

//        public SubscriptionController() { }

//        #region Subscription 相关
        
//        /// <summary>
//        /// 创建订阅信息
//        /// </summary>
//        /// <param name="topicName"></param>
//        /// <param name="subscriptionName"></param>
//        /// <param name="vmSubscriptionCreate"></param>
//        [HttpPut("{topicName}/subscriptions/{subscriptionName}")]
//        public void CreateSubscription([FromRoute]string topicName, [FromRoute]string subscriptionName, [FromBody] dynamic vmSubscriptionCreate)
//        {
//            //Build Entity & Some Check Here... ...
//            var subscription = new Subscription();
//            subscription.Name = subscriptionName;
//            subscription.Endpoint = vmSubscriptionCreate.endpoint;
//            subscription.FilterTag = vmSubscriptionCreate.filterTag;
//            subscription.NotifyStrategy = vmSubscriptionCreate.notifyStrategy;

//            MessageService.SubscribeTopic(topicName, subscription);

//        }

//        /// <summary>
//        /// 修改订阅信息
//        /// </summary>
//        /// <param name="topicName"></param>
//        /// <param name="subscriptionName"></param>
//        /// <param name="vmSubscriptionCreate"></param>
//        [HttpPatch("{topicName}/subscriptions/{subscriptionName}")]
//        public void UpdateSubscription([FromRoute]string topicName, [FromRoute]string subscriptionName, [FromBody] dynamic vmSubscriptionPacth)
//        {
//            //Build Entity & Some Check Here... ...
//            var subscription = new Subscription();
//            subscription.Name = subscriptionName;
//            subscription.NotifyStrategy = vmSubscriptionPacth.notifyStrategy;

//            MessageService.ModifySubscriptionInfo(topicName, subscription);
//        }

//        [HttpGet("{topicName}/subscriptions/{subscriptionName}")]
//        public void GetSubscription([FromRoute]string topicName, [FromRoute]string subscriptionName)
//        {
//            /*
//                {
//                    "subscriptionName":"<< subscription name >>",
//                    "subscriber":"<< subscriber >>",
//                    "topicOwner":"<< topic owner >>",
//                    "topicName":"<< topic name >>",
//                    "endpoint":"http(s)://custom-host.com/apis-name",
//                    "notifyStrategy":"<< notify-strategy >>",
//                    "filterTag":"<< filter tag >>",
//                    "createdTime": "2019-09-09 08:30:00",
//                    "updatedTime": "2019-09-12 08:30:36"
//                }
//            */
//        }

//        /// <summary>
//        /// 取消订阅
//        /// </summary>
//        /// <param name="topicName"></param>
//        /// <param name="subscriptionName"></param>
//        [HttpDelete("{topicName}/subscriptions/{subscriptionName}")]
//        public void UnsubscribeTopic([FromRoute]string topicName, [FromRoute]string subscriptionName)
//        {
//            MessageService.UnsubscribeTopic(topicName, subscriptionName);
//        }


//        [HttpGet("{topicName}/subscriptions")]
//        public void GetSubscriptionList([FromRoute]string topicName, [FromQuery] int index = 1, [FromQuery] int size = 10)
//        {
//            /*
//                {
//                    "index": 1,
//                    "size": 50,
//                    "count": 2365,
//                    "datas":[
//                        {
//                            "subscriptionName":"<< subscription name >>",
//                            "subscriber":"<< subscriber >>",
//                            "topicOwner":"<< topic owner >>",
//                            "topicName":"<< topic name >>",
//                            "endpoint":"http(s)://custom-host.com/apis-name",
//                            "notifyStrategy":"<< notify-strategy >>",
//                            "filterTag":"<< filter tag >>",
//                            "createdTime": "2019-09-09 08:30:00",
//                            "updatedTime": "2019-09-12 08:30:36"
//                        }
//                        ... ...
//                    ]
//                }
//            */
//        }
//        #endregion
//    }
//}
