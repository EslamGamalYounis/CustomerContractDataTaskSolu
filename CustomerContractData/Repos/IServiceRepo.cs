using CustomerContractData.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CustomerContractData.Repos
{
    public interface IServiceRepo
    {
       public IEnumerable<Service> GetAllServices();
    }
}
