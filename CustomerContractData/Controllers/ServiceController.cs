using CustomerContractData.Repos;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CustomerContractData.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServiceController : ControllerBase
    {
        public IServiceRepo serviceRepo { get; set; }

        public ServiceController(IServiceRepo serviceRepo)
        {
            this.serviceRepo = serviceRepo;
        }
        // GET: api/<ServiceController>
        [HttpGet]
        public IActionResult Get()
        {
            var services = serviceRepo.GetAllServices().ToList();
            return Ok(services);
        }

    }
}
