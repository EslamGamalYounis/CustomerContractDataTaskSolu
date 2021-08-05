using CustomerContractData.Repos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CustomerContractData.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        public ICustomerRepo customerRepo{ get; set; }

        public CustomerController(ICustomerRepo _customerRepo)
        {
            customerRepo = _customerRepo;
        }

        [HttpGet]
        public ActionResult GetAll()
        {
            var result =customerRepo.getAll();
            return Ok(result);
        }

        [HttpGet("/api/customer/expired")]
        public ActionResult GetCustomerContractExpired()
        {
            var result = customerRepo.getCustomerHasExpiredContractOnly();
            return Ok(result);
        }

        [HttpGet("/api/customer/expiredwithinmonth")]
        public ActionResult GetCustomerContractWillExpireWithMonth()
        {
            var result = customerRepo.GetCustomerWillExpireWithinMonth();
            return Ok(result);
        }

        [HttpGet("/api/customer/count/{serviceName}")]
        public ActionResult GetCustomerCountByServiceName(string serviceName)
        {
            var result = customerRepo.getCustomerNumByServiceType(serviceName);
            return Ok(result);
        }
    }
}
