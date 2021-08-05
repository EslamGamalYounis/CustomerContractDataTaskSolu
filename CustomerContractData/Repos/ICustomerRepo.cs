using CustomerContractData.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CustomerContractData.Repos
{
    public interface ICustomerRepo
    {
        Task<List<Customer>> getAll(int pageIndex);
        IEnumerable<Customer> getCustomerHasExpiredContractOnly();
        IEnumerable<Customer> GetCustomerWillExpireWithinMonth();
        int getCustomerNumByServiceType(string serviceName);

    }
}
