using Castle.Core.Logging;
using CustomerContractData.Helpers;
using CustomerContractData.Models;
using CustomerContractData.ResourcesParameters;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CustomerContractData.Repos
{
    public class CustomerRepo : ICustomerRepo
    {
        public OrangeCustomerSubscriptionContext DBContext { get; set; }

        public CustomerRepo(OrangeCustomerSubscriptionContext _db)
        {
            DBContext = _db;
        }

        public PagedList<Customer> getAll(UserResourceParameters userResourceParameters)
        {
            if (userResourceParameters == null)
            {
                throw new ArgumentNullException(nameof(userResourceParameters));
            }
            if (userResourceParameters.PageNumber == 0 & userResourceParameters.PageSize == 0)
            {
                throw new ArgumentNullException(nameof(userResourceParameters));
            }

            var customers = DBContext.Customers as IQueryable<Customer>;

            if (!string.IsNullOrWhiteSpace(userResourceParameters.SearchQuery))
            {
                var searchQuery = userResourceParameters.SearchQuery.Trim();
                customers = customers.Where(a => a.CstName.Contains(searchQuery));
            }

            return PagedList<Customer>.Create(customers, userResourceParameters.PageNumber, userResourceParameters.PageSize);
        }

        public IEnumerable<Customer> getCustomerHasExpiredContractOnly()
        {
            try
            {
                var customers = DBContext.Customers
                                .FromSqlRaw("Exec CustomersContractExpired").AsEnumerable();
                return customers;
            }
            catch (Exception Ex)
            {
                throw ;
            }
        }

        public int getCustomerNumByServiceType(string serviceName)
        {
            string serviceNameUpper = serviceName.ToUpper();

            var service = DBContext.Services.SingleOrDefault(w => w.ServiceName == serviceNameUpper);
            if (service != null)
            {
                var r = DBContext.Set<CustomerCountPerServiceResult>().FromSqlInterpolated($"exec CustomersCountsByServiceType {serviceNameUpper}").ToList();
                int num = r[0].Counts;
                return num;
            }
            else
            {
                throw new ArgumentException($"We don't have a service for {serviceName}.", nameof(serviceName));
            }
        }

        public IEnumerable<Customer> GetCustomerWillExpireWithinMonth()
        {
            var customers = DBContext.Customers
                            .FromSqlRaw("Exec CustomersContractsWillExpireWithinMonth").AsEnumerable();
            return customers;
        }

        public IEnumerable<CountMonthsPerYear> GetCstCountMonthsPerYears(int year)
        {
            var result = DBContext.Set<CountMonthsPerYear>().FromSqlInterpolated($"exec customerCountsPerYearAndMonth {year}").ToList();
            int x = 0;
            return result;
        }

    }
}
