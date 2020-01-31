using Craftsman.Core.Domain;
using Craftsman.Core.ObjectMapping;
using Craftsman.Core.Web;
using Craftsman.Waiter.Domain;
using Craftsman.Waiter.Domain.Entities;
using Craftsman.Waiter.Domain.Services;
using Craftsman.Waiter.WebUI.ViewModel;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Craftsman.Waiter.WebUI.Controllers
{
    [Route("api/system-services")]
    public class SystemServiceController : Controller, IController
    {
        public IObjectMapper ObjectMapper { get; set; }

        public ISystemServiceService SystemServiceService { get; set; }
        // GET: api/system-services
        [HttpGet]
        public PagingDataRespone<SystemService> GetSystemServices()
        {
            var data = SystemServiceService.GetSystemServices();
            return new PagingDataRespone<SystemService>() { Data = data, Pagination = new Pagination() };
        }

        [HttpGet("{id}")]
        public SystemService GetSystemService(int id)
        {
            return SystemServiceService.GetSystemService(id);
        }

        /// <summary>
        /// 修改服务信息
        /// </summary>
        /// <param name="model"></param>
        [HttpPut("{id}")]
        public IActionResult UpdateSystemService([FromRoute]int id, [FromBody]SystemServiceVM model)
        {
            var entity = ObjectMapper.Map<SystemService>(model);
            entity.Id = id;

            var objEntity = SystemServiceService.UpdateSystemService(entity);
            return Ok(objEntity);
        }

        /// <summary>
        /// 创建服务信息
        /// </summary>
        /// <param name="model"></param>
        [HttpDelete("{id}")]
        public IActionResult DeleteSystemService([FromRoute]int id)
        {
            SystemServiceService.DeleteSystemService(id);
            return NoContent();
        }

        [HttpPost]
        public IActionResult CreateSystemService([FromBody]SystemServiceVM model)
        {
            var entity = ObjectMapper.Map<SystemService>(model);
            var objEntity = SystemServiceService.CreateSystemService(entity);
            return Created("api/system-services", objEntity);
        }
        // GET: api/system-services/{id}/auth-configs
        [HttpGet("{systemServiceId}/auth-configs")]
        public PagingDataRespone<SystemServiceAuthConfig> GetSystemServiceAuthConfigs()
        {
            var data = SystemServiceService.GetSystemServiceAuthConfigs();
            return new PagingDataRespone<SystemServiceAuthConfig>() { Data = data, Pagination = new Pagination() };
        }
        
    }
}
