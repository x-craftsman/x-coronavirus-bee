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
    [Route("api/tenants")]
    public class TenantController : Controller, IController
    {
        public IObjectMapper ObjectMapper { get; set; }
        public ITenantService TenantService { get; set; }

        #region tenants
        // GET: api/tenants
        [HttpGet]
        public PagingDataRespone<Tenant> GetTenants()
        {
            var data = TenantService.GetTenants();
            return new PagingDataRespone<Tenant>() { Data = data, Pagination = new Pagination() };
        }

        // GET api/tenants/5
        [HttpGet("{id}")]
        public Tenant GetTenant(int id)
        {
            return TenantService.GetTenant(id);
        }

        /// <summary>
        /// 修改服务信息
        /// </summary>
        /// <param name="model"></param>
        [HttpPut("{id}")]
        public IActionResult UpdateTenant([FromRoute]int id, [FromBody]TenantVM model)
        {
            var entity = ObjectMapper.Map<Tenant>(model);
            entity.Id = id;

            var objEntity = TenantService.UpdateTenant(entity);
            return Ok(objEntity);
        }

        /// <summary>
        /// 创建服务信息
        /// </summary>
        /// <param name="model"></param>
        [HttpDelete("{id}")]
        public IActionResult DeleteTenant([FromRoute]int id)
        {
            TenantService.DeleteTenant(id);
            return NoContent();
        }

        [HttpPost]
        public IActionResult CreateTenant([FromBody]TenantVM model)
        {
            var entity = ObjectMapper.Map<Tenant>(model);
            var objEntity = TenantService.CreateTenant(entity);
            return Created("api/system-services", objEntity);
        }
        #endregion tenants

        #region tenants apikey
        // GET: api/tenants/{tenantId}/api-keys
        [HttpGet("{tenantId}/api-keys")]
        public PagingDataRespone<TenantApiKey> GetTenantApiKeys([FromRoute]int tenantId)
        {
            var data = TenantService.GetTenantApiKeys(tenantId);
            return new PagingDataRespone<TenantApiKey>() { Data = data, Pagination = new Pagination() };
        }

        // GET: api/tenants/{tenantId}/api-keys/{apiKeyId}
        [HttpGet("{tenantId}/api-keys/{apiKeyId}")]
        public TenantApiKey GetTenantApiKey([FromRoute]int tenantId, [FromRoute]int apiKeyId)
        {
            return TenantService.GetTenantApiKey(tenantId, apiKeyId);
        }

        [HttpPut("{tenantId}/api-keys/{apiKeyId}")]
        public IActionResult UpdateTenantApiKey(
            [FromRoute]int tenantId,
            [FromRoute]int apiKeyId,
            [FromBody]TenantApiKeyVM model)
        {
            var entity = ObjectMapper.Map<TenantApiKey>(model);
            entity.TenantId = tenantId;
            entity.Id = apiKeyId;

            var objEntity = TenantService.UpdateTenantApiKey(entity);
            return Ok(objEntity);
        }

        /// <summary>
        /// 创建服务信息
        /// </summary>
        /// <param name="model"></param>
        [HttpDelete("{tenantId}/api-keys/{apiKeyId}")]
        public IActionResult DeleteTenantApiKey([FromRoute]int tenantId, [FromRoute]int apiKeyId)
        {
            TenantService.DeleteTenantApiKey(tenantId, apiKeyId);
            return NoContent();
        }

        [HttpPost("{tenantId}/api-keys")]
        public IActionResult CreateTenantApiKey([FromRoute]int tenantId, [FromBody]TenantApiKeyVM model)
        {
            var entity = ObjectMapper.Map<TenantApiKey>(model);
            entity.TenantId = tenantId;
            var objEntity = TenantService.CreateTenantApiKey(entity);
            return Created($"api/tenants/{tenantId}/api-keys/{objEntity.Id}", objEntity);
        }
        #endregion tenants apikey
    }
}
