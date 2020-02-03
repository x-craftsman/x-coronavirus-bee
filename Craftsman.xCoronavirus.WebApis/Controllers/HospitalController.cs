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
        public PagingDataRespone<Hospital> GetAllHospitals(
            [FromQuery]string name
        )
        {
            var data = new List<Hospital>();
            if (string.IsNullOrEmpty(name))
            {
                data = _hospitalService.GetHospitals();
            }
            else
            {
                data = _hospitalService.GetHospitals(name);
            }
            
            return new PagingDataRespone<Hospital>() { Data = data, Pagination = new Pagination() };
        }

        // GET api/hospitals/{guid}
        [HttpGet("{id}")]
        public Hospital GetHospital(string id)
        {
            return _hospitalService.GetHospital(id);
        }

        [HttpPost]
        public IActionResult CreateHospital([FromBody]Hospital model)
        {
            // var entity = _objectMapper.Map<Hospital>(model);
            var objEntity = _hospitalService.CreateHospital(model);
            return Created("api/hospitals", objEntity);
        }
        /// <summary>
        /// 修改服务信息
        /// </summary>
        /// <param name="model"></param>
        [HttpPut("{id}")]
        public IActionResult UpdateHospital([FromRoute]string id, [FromBody]Hospital model)
        {
            //var entity = ObjectMapper.Map<Tenant>(model);
            model.Id = id;
            var objEntity = _hospitalService.UpdateHospital(model);
            return Ok(objEntity);
        }
        #endregion Hospital

        #region medical-supplies
        // GET: api/hospitals/{hospitalId}/medical-supplies
        [HttpGet("{hospitalId}/medical-supplies")]
        public PagingDataRespone<MaterialDemand> GetHospitalMedicalSupplies([FromRoute]string hospitalId)
        {
            var data = _hospitalService.GetHospitalMedicalSupplies(hospitalId);
            return new PagingDataRespone<MaterialDemand>() { Data = data, Pagination = new Pagination() };
        }
        // GET: api/hospitals/{hospitalId}/medical-supplies
        [HttpGet("{hospitalId}/medical-supplies/{materialDemandId}")]
        public MaterialDemand GetHospitalMedicalSupply(
            [FromRoute]string hospitalId,
            [FromRoute]string materialDemandId)
        {
            var data = _hospitalService.GetHospitalMedicalSupply(hospitalId, materialDemandId);
            return data;
        }
        // POST: api/hospitals/{hospitalId}/medical-supplies
        [HttpPost("{hospitalId}/medical-supplies")]
        public IActionResult CreateHospitalMedicalSupplies(
            [FromRoute]string hospitalId, 
            [FromBody]MaterialDemand materialDemand
        )
        {
            var data = _hospitalService.CreateHospitalMedicalSupply(hospitalId, materialDemand);
            return Created($"api/hospitals/{hospitalId}/medical-supplies", data);
        }
        // PUT: api/hospitals/{hospitalId}/medical-supplies
        [HttpPut("{hospitalId}/medical-supplies/{materialDemandId}")]
        public IActionResult UpdateHospitalMedicalSupplies(
            [FromRoute]string hospitalId,
            [FromRoute]string materialDemandId,
            [FromBody]MaterialDemand materialDemand
        )
        {
            var data = _hospitalService.UpdateHospitalMedicalSupply(hospitalId, materialDemandId, materialDemand);
            return NoContent();
        }
        // DELETE: api/hospitals/{hospitalId}/medical-supplies
        [HttpDelete("{hospitalId}/medical-supplies/{materialDemandId}")]
        public IActionResult DeleteHospitalMedicalSupply(
            [FromRoute]string hospitalId,
            [FromRoute]string materialDemandId
        )
        {
            _hospitalService.DeleteHospitalMedicalSupply(hospitalId, materialDemandId);
            return NoContent();
        }
        #endregion medical-supplies

        #region Contacts
        // GET: api/hospitals/{hospitalId}/contacts
        [HttpGet("{hospitalId}/contacts")]
        public PagingDataRespone<Contact> GetHospitalContacts([FromRoute]string hospitalId)
        {
            var data = _hospitalService.GetHospitalContacts(hospitalId);
            return new PagingDataRespone<Contact>() { Data = data, Pagination = new Pagination() };
        }
        // GET: api/hospitals/{hospitalId}/contacts
        [HttpGet("{hospitalId}/contacts/{contactId}")]
        public Contact GetHospitalContact(
            [FromRoute]string hospitalId,
            [FromRoute]string contactId)
        {
            var data = _hospitalService.GetHospitalContact(hospitalId, contactId);
            return data;
        }
        // POST: api/hospitals/{hospitalId}/contacts
        [HttpPost("{hospitalId}/contacts")]
        public IActionResult CreateHospitalContacts(
            [FromRoute]string hospitalId,
            [FromBody]Contact contact
        )
        {
            var data = _hospitalService.CreateHospitalContact(hospitalId, contact);
            return Created($"api/hospitals/{hospitalId}/contacts", data);
        }
        // PUT: api/hospitals/{hospitalId}/contacts/{contactId}
        [HttpPut("{hospitalId}/contacts/{contactId}")]
        public IActionResult UpdateHospitalContacts(
            [FromRoute]string hospitalId,
            [FromRoute]string contactId,
            [FromBody]Contact contact
        )
        {
            var data = _hospitalService.UpdateHospitalContact(hospitalId, contactId, contact);
            return NoContent();
        }
        // DELETE: api/hospitals/{hospitalId}/contacts
        [HttpDelete("{hospitalId}/contacts/{contactId}")]
        public IActionResult DeleteHospitalContact(
            [FromRoute]string hospitalId,
            [FromRoute]string contactId
        )
        {
            _hospitalService.DeleteHospitalContact(hospitalId, contactId);
            return NoContent();
        }
        #endregion Contacts
    }
}
