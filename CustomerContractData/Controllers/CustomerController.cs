using CustomerContractData.Models;
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

        [HttpGet("{pageIndex}")]
        public async Task<ActionResult> GetAll(int pageIndex=1)
        {
            var customers =await customerRepo.getAll(pageIndex);
            return Ok(customers);
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
