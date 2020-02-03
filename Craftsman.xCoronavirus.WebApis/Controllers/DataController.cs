//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;
//using Craftsman.Core.Domain;
//using Craftsman.Core.Infrastructure.Logging;
//using Craftsman.Core.Runtime;
//using Microsoft.AspNetCore.Mvc;
//using Craftsman.Core.Web;
//using Craftsman.xCoronavirus.Domain.Entities;
//using Craftsman.Core.ObjectMapping;
//using Craftsman.xCoronavirus.Domain;
//using Craftsman.Core.Domain.Repositories;

//namespace Craftsman.xCoronavirus.WebApis.Controllers
//{
//    [Route("api/hospitals")]
//    [ApiController]
//    public class DataController : ControllerBase, IController
//    {
//        private ILogger _logger;
//        private ISession _session;
//        private IObjectMapper _objectMapper;
//        private IHospitalService _hospitalService { get; set; }
        
//        private IRepository<MedicalSupply> _repoMedicalSupply;

//        public DataController(
//            ILogger logger,
//            ISession session,
//            IObjectMapper objectMapper,
//            IHospitalService hospitalService,
//            IRepository<MedicalSupply> repoMedicalSupply
//        )
//        {
//            _logger = logger;
//            _session = session;
//            _objectMapper = objectMapper;
//            _hospitalService = hospitalService;
//            _repoMedicalSupply = repoMedicalSupply;
//        }


//        #region Hospital
//        // GET: api/hospitals
//        [HttpGet]
//        public IActionResult GetHospitals(
//            [FromQuery]string city,
//            [FromQuery]string name
//        )
//        {
//            return NoContent();
//        }
        
//        #endregion Hospital
//    }
//}
