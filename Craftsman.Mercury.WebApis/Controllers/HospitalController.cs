using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Craftsman.Core.Domain;
using Craftsman.Core.Infrastructure.Logging;
using Craftsman.Core.Runtime;
using Microsoft.AspNetCore.Mvc;
using Craftsman.Core.Web;
using Craftsman.xCoronavirus.Domain.Entities;
using Craftsman.Core.ObjectMapping;
using Craftsman.xCoronavirus.Domain;

namespace Craftsman.xCoronavirus.WebApis.Controllers
{
    [Route("api/hospitals")]
    [ApiController]
    public class HospitalController : ControllerBase, IController
    {
        private ILogger _logger;
        private ISession _session;
        private IObjectMapper _objectMapper;
        public IHospitalService _hospitalService { get; set; }

        public HospitalController(
            ILogger logger,
            ISession session,
            IObjectMapper objectMapper,
            IHospitalService hospitalService
        )
        {
            _logger = logger;
            _session = session;
            _objectMapper = objectMapper;
            _hospitalService = hospitalService;
        }


        #region Hospital
        // GET: api/hospitals
        [HttpGet]
        public PagingDataRespone<Hospital> GetHospitals(
            [FromQuery]string city, 
            [FromQuery]string name
        )
        {
            var data = _hospitalService.GetHospitals(city, name);
            return new PagingDataRespone<Hospital>() { Data = data, Pagination = new Pagination() };
        }

        // GET api/hospitals/5
        [HttpGet("{id}")]
        public Hospital GetHospital(int id)
        {
            return _hospitalService.GetHospital(id);
        }

        ///// <summary>
        ///// 修改服务信息
        ///// </summary>
        ///// <param name="model"></param>
        //[HttpPut("{id}")]
        //public IActionResult UpdateTenant([FromRoute]int id, [FromBody]TenantVM model)
        //{
        //    var entity = ObjectMapper.Map<Tenant>(model);
        //    entity.Id = id;

        //    var objEntity = TenantService.UpdateTenant(entity);
        //    return Ok(objEntity);
        //}

        [HttpPost]
        public IActionResult CreateHospital([FromBody]object model)
        {
            var entity = _objectMapper.Map<Hospital>(model);
            var objEntity = _hospitalService.CreateHospital(entity);
            return Created("api/hospitals", objEntity);
        }

        // GET: api/hospitals/{hospitalId}/medical-supplies
        [HttpGet("{hospitalId}/medical-supplies")]
        public PagingDataRespone<MedicalSupply> GetHospitalMedicalSupplies([FromRoute]int hospitalId)
        {
            var data = _hospitalService.GetHospitalMedicalSupplies(hospitalId);
            return new PagingDataRespone<MedicalSupply>() { Data = data, Pagination = new Pagination() };
        }
        #endregion Hospital
    }
}
