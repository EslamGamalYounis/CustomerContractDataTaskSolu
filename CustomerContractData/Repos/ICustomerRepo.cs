using CustomerContractData.Helpers;
using CustomerContractData.Models;
using CustomerContractData.ResourcesParameters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CustomerContractData.Repos
{
    public interface ICustomerRepo
    {
        PagedList<Customer> getAll(UserResourceParameters userResourceParameters);
        IEnumerable<Customer> getCustomerHasExpiredContractOnly();
        IEnumerable<Customer> GetCustomerWillExpireWithinMonth();
        int getCustomerNumByServiceType(string serviceName);
        IEnumerable<CountMonthsPerYear> GetCstCountMonthsPerYears(int year);

    }
}
