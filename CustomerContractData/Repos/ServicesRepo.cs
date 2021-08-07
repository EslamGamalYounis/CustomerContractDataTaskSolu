using CustomerContractData.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CustomerContractData.Repos
{
    public class ServicesRepo : IServiceRepo
    {
        public OrangeCustomerSubscriptionContext DBContext { get; set; }

        public ServicesRepo(OrangeCustomerSubscriptionContext DBContext)
        {
            this.DBContext = DBContext;
        }
        public IEnumerable<Service> GetAllServices()
        {
            var services = DBContext.Services.ToList();
            return services;
        }
    }
}
