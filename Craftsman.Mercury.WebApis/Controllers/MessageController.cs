using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Craftsman.Core.Domain;
using Craftsman.Core.Infrastructure.Logging;
using Craftsman.Core.Runtime;
using Craftsman.Mercury.Domain;
using Microsoft.AspNetCore.Mvc;
using Craftsman.Core.Infrastructure.Message;

namespace Craftsman.Mercury.WebApis.Controllers
{
    [Route("api/topics")]
    [ApiController]
    public class MessageController : ControllerBase, IController
    {
        private ILogger _logger;
        private ISession _session;
        private IMessageService _messageService;

        public MessageController(
            ILogger logger,
            ISession session,
            IMessageService messageService
        )
        {
            _logger = logger;
            _session = session;
            _messageService = messageService;
        }


        #region Messages 相关
        [HttpPost("{topicName}/messages")]
        public void PublishMessage([FromRoute]string topicName, [FromBody] MessageInfo vmPublishMessage)
        {
            _messageService.PublishMessage(topicName, vmPublishMessage);
        }
        #endregion
    }
}
