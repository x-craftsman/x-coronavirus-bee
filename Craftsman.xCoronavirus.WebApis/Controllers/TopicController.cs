using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Craftsman.Core.Domain;
using Craftsman.Core.Infrastructure.Logging;
using Craftsman.Core.Runtime;
using Craftsman.Mercury.Domain;
using Microsoft.AspNetCore.Mvc;

namespace Craftsman.Mercury.WebApis.Controllers
{
    [Route("api/topics")]
    [ApiController]
    public class TopicController : ControllerBase, IController
    {
        public ISession MySession { get; set; }
        public IMessageService MessageService { get; set; }

        public ILogger Logger { get; set; }

        public TopicController(
            ISession session)
        { }

        #region Topics 相关
        /// <summary>
        /// 创建主题 POST api/topics
        /// </summary>
        /// <param name="topicName">主题名称</param>
        //暂时不提供
        //[HttpPost] 
        //public IActionResult CreateTopic([FromBody] dynamic vmTopicCreate)
        //{
        //    var topic = new Topic();
        //    topic.Name = vmTopicCreate.topicName;
        //    topic.MaxMessageSize = vmTopicCreate.maxMessageSize;
        //    MessageService.CreateTopic(topic);
        //    return Created(string.Empty, null);
        //}

        /// <summary>
        /// 修改主题 PUT api/topics/${topicName}
        /// </summary>
        /// <param name="topicName">主题名称</param>
        //暂时不提供
        //[HttpPut("{topicName}")]
        //public void Put([FromRoute] string topicName, [FromBody] dynamic vmTopicUpdate) { }


        /// <summary>
        /// 删除主题 DELETE api/topics/${topicName}
        /// </summary>
        /// <param name="topicName">主题名称</param>
        //暂时不提供
        //[HttpDelete("{topicName}")]
        //public void Delete([FromRoute] string topicName) { }

        /// <summary>
        /// 获取主题信息（元数据） GET /topics/${topicName}
        /// </summary>
        /// <param name="topicName"></param>
        /// <returns></returns>
        [HttpGet("{topicName}")]
        public IActionResult Get([FromRoute] string topicName)
        {
            var topic = MessageService.GetTopic(topicName);
            return Ok(topic);
        }

        [HttpGet]
        public IActionResult Get([FromQuery] int index = 1, [FromQuery] int size = 10)
        {
            var topics = MessageService.GetTopics();
            var result = new
            {
                index = index,
                size = size,
                count = topics.Count(),
                datas = topics.Skip((index - 1) * size).Take(size)
            };
            return Ok(result);
        }
        #endregion
    }
}
