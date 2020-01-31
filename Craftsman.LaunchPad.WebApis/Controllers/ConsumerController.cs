using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Craftsman.LaunchPad.Domain.Services;
using Craftsman.Core.CustomizeException;
using Craftsman.LaunchPad.Domain.Entities;
using Craftsman.Core.Domain;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Craftsman.LaunchPad.WebApi.Controllers
{
    [Route("api/consumers")]
    public class ConsumerController : ControllerBase, IController
    {
        public IConsumerService serviceConsumer { get; set; }
        // GET: api/consumers
        //[HttpGet]
        //public IEnumerable<string> Get()
        //{
        //    return new string[] { "value1", "value2" };
        //}

        // GET api/values/5
        //[HttpGet("{id}")]
        //public string Get(int id)
        //{
        //    return "value";
        //}

        // POST api/consumers
        [HttpPost]
        public Consumer Post([FromBody]RegisterConsumerVM vm)
        {
            if (string.IsNullOrEmpty(vm.Code) || string.IsNullOrEmpty(vm.Name))
            {
                //throw new BusinessException("消费者Code或Name不能为空！", ExceptionCode.ABC);
                throw new BusinessException("消费者Code或Name不能为空！");
            }
            return serviceConsumer.RegisterConsumer(vm.Code, vm.Name);
        }
        public class RegisterConsumerVM
        {
            public string Code { get; set; }
            public string Name { get; set; }
        }

        //// PUT api/values/5
        //[HttpPut("{id}")]
        //public void Put(int id, [FromBody]string value)
        //{
        //}

        //// DELETE api/values/5
        //[HttpDelete("{id}")]
        //public void Delete(int id)
        //{
        //}
    }
}
