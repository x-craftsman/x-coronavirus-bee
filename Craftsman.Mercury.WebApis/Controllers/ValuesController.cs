//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;
//using Craftsman.Core.Domain;
//using Craftsman.Core.Infrastructure.Logging;
//using Craftsman.Core.Runtime;
//using Craftsman.Mercury.Domain;
//using Microsoft.AspNetCore.Mvc;
//using Craftsman.Mercury.Domain.Entities.World;

//namespace Craftsman.Mercury.WebApis.Controllers
//{
//    [Route("api/[controller]")]
//    [ApiController]
//    public class ValuesController : ControllerBase, IController
//    {
//        public ISession MySession { get; set; }
//        public IDemoService DemoService { get; set; }

//        public ILogger Logger { get; set; }
        
//        public ValuesController(
//            ISession session,
//            IDemoService service)
//        {
            
//        }
//        // GET api/values
//        [HttpGet]
//        public ActionResult<IEnumerable<string>> Get()
//        {
//            //DemoService.TestMethod();
//            return new string[] { "value1", "value2" };
//        }
//        [HttpGet("city")]
//        public City GetCity()
//        {
//            //var city = DemoService.TestMethod();
//            //city.CountryCodeNavigation = null;
//            //return city;
//            return null;
//        }

//        // GET api/values/5
//        [HttpGet("{id}")]
//        public ActionResult<string> Get(int id)
//        {
//            return "value";
//        }

//        // POST api/values
//        [HttpPost]
//        public void Post([FromBody] string value)
//        {
//        }

//        // PUT api/values/5
//        [HttpPut("{id}")]
//        public void Put(int id, [FromBody] string value)
//        {
//        }

//        // DELETE api/values/5
//        [HttpDelete("{id}")]
//        public void Delete(int id)
//        {
//        }
//    }
//}
