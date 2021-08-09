using CustomerContractData.Helpers;
using CustomerContractData.Models;
using CustomerContractData.Repos;
using CustomerContractData.ResourcesParameters;
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
        [ProducesResponseType(200,Type=typeof(List<Customer>))]
        public ActionResult GetAll([FromQuery] UserResourceParameters userResourceParameters)
        {
            if(userResourceParameters == null)
            {
                return BadRequest(ModelState);
            }
            var customers = customerRepo.getAll(userResourceParameters);
            Response.AddPagination(customers.CurrentPage, customers.PageSize, customers.TotalCount, customers.TotalPages);
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

        [HttpGet("/api/customer/count/service")]
        public ActionResult GetCustomerCountByServiceName()
        {
            var result = customerRepo.getCustomerNumByServiceType();
            return Ok(result);
        }

        [HttpGet("/api/customer/count/year/{year}")]
        public ActionResult GetCustomerCountByMonthsAndYear(int year)
        {
            var result = customerRepo.GetCstCountMonthsPerYears(year);
            if(result != null)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest("year is not have data");
            }
        }


    }
}
