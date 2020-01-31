using Craftsman.Core.Domain;
using Craftsman.Core.ObjectMapping;
using Craftsman.Core.Web;
using Craftsman.Waiter.Domain;
using Craftsman.Waiter.Domain.Entities;
using Craftsman.Waiter.Domain.Services;
using Craftsman.Waiter.Domain.ViewModel;
using Craftsman.Waiter.WebUI.ViewModel;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Craftsman.Waiter.WebUI.Controllers
{
    [Route("api/service-subscribers")]
    public class ServiceSubscriberController : Controller, IController
    {
        public IObjectMapper ObjectMapper { get; set; }

        public IServiceSubscriberService ServiceSubscriberService { get; set; }

        #region ServiceSubscriber
        // GET: api/service-subscribers
        [HttpGet]
        public PagingDataRespone<ServiceSubscriber> Get()
        {
            var data = ServiceSubscriberService.GetServiceSubscriberList();
            return new PagingDataRespone<ServiceSubscriber>() { Data = data, Pagination = new Pagination() };
        }

        // GET: api/service-subscribers/{id}
        [HttpGet("{id}")]
        public ServiceSubscriber GetServiceSubscriber([FromRoute]int id)
        {
            return ServiceSubscriberService.GetServiceSubscriber(id);
        }

        // POST: api/service-subscribers
        [HttpPost]
        public IActionResult CreateServiceSubscriber([FromBody]ServiceSubscriberCreateView viewModel)
        {
            var subscriber = ServiceSubscriberService.CreateServiceSubscriber(viewModel);
            return Created("api/service-subscribers", subscriber);
        }

        // DELETE: api/service-subscribers/{id}
        [HttpDelete("{id}")]
        public IActionResult DeleteServiceSubscriber([FromRoute]int id)
        {
            ServiceSubscriberService.DeleteServiceSubscriber(id);
            return NoContent();
        }
        #endregion ServiceSubscriber

        #region ServiceSubscriber - RuleDetail
        // POST: api/service-subscribers/{id}/mapping-rules/{id}/rule-details
        [HttpPost("{subscriberId}/mapping-rules/{ruleId}/rule-details")]
        public IActionResult CreateServiceSubscriberRuleDetail(
            [FromRoute]int subscriberId,
            [FromRoute]int ruleId,
            [FromBody]ServiceSubscriberDetailCreateView viewModel
        )
        {
            var model = ObjectMapper.Map<ServiceSubscriberMappingRuleDetail>(viewModel);
            var detail = ServiceSubscriberService.CreateServiceSubscriberRuleDetail(subscriberId, ruleId, model);
            return Created($"api/service-subscribers/{subscriberId}/mapping-rules/{ruleId}/rule-details/{detail?.Id}", detail);
        }
        // PUT: api/service-subscribers/{id}/mapping-rules/{id}/rule-details/{id}
        [HttpPut("{subscriberId}/mapping-rules/{ruleId}/rule-details/{id}")]
        public IActionResult UpdateServiceSubscriberRuleDetail(
            [FromRoute]int id,
            [FromRoute]int subscriberId,
            [FromRoute]int ruleId,
            [FromBody]ServiceSubscriberDetailCreateView viewModel
        )
        {
            var model = ObjectMapper.Map<ServiceSubscriberMappingRuleDetail>(viewModel);
            ServiceSubscriberService.UpdateServiceSubscriberRuleDetail(subscriberId, ruleId, id, model);
            return NoContent();
        }
        // DELETE: api/service-subscribers/{id}/mapping-rules/{id}/rule-details/{id}
        [HttpDelete("{subscriberId}/mapping-rules/{ruleId}/rule-details/{id}")]
        public IActionResult DeleteServiceSubscriberRuleDetail(
            [FromRoute]int id,
            [FromRoute]int subscriberId,
            [FromRoute]int ruleId
        )
        {
            ServiceSubscriberService.DeleteServiceSubscriberRuleDetail(subscriberId, ruleId, id);
            return NoContent();
        }
        #endregion ServiceSubscriber - RuleDetail

        #region ServiceSubscriber - ExecutionRecord

        // GET: api/service-subscribers/{id}/execution-records
        [HttpGet("{subscriberId}/execution-records")]
        public PagingDataRespone<ServiceSubscriberExecutionRecord> GetServiceSubscriberExecutionRecords([FromRoute]int subscriberId)
        {
            var records = ServiceSubscriberService.GetServiceSubscriberExecutionRecords(subscriberId);
            return new PagingDataRespone<ServiceSubscriberExecutionRecord>() { Data = records, Pagination = new Pagination() };
        }
        // GET: api/service-subscribers/{id}/execution-records
        [HttpGet("{subscriberId}/execution-records/{recordId}/execution-logs")]
        public PagingDataRespone<ServiceSubscriberExecutionLog> GetServiceSubscriberExecutionLogs([FromRoute]int subscriberId, [FromRoute]int recordId)
        {
            var logs = ServiceSubscriberService.GetServiceSubscriberExecutionLogs(subscriberId, recordId);
            return new PagingDataRespone<ServiceSubscriberExecutionLog>() { Data = logs, Pagination = new Pagination() };
        }
        #endregion ServiceSubscriber - ExecutionRecord
    }
}
