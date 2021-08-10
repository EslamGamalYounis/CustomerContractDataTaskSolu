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

        //public int getCustomerNumByServiceType(string serviceName)
        //{
        //    string serviceNameUpper = serviceName.ToUpper();

        //    var service = DBContext.Services.SingleOrDefault(w => w.ServiceName == serviceNameUpper);
        //    if (service != null)
        //    {
        //        var r = DBContext.Set<CustomerCountPerServiceResult>().FromSqlInterpolated($"exec CustomersCountsByServiceType {serviceNameUpper}").ToList();
        //        int num = r[0].Counts;
        //        return num;
        //    }
        //    else
        //    {
        //        throw new ArgumentException($"We don't have a service for {serviceName}.", nameof(serviceName));
        //    }
        //}

        IEnumerable<GetCustomerConutsByServiceTypeResult> ICustomerRepo.getCustomerNumByServiceType()
        {
            var result = from c in DBContext.ContractDates
                         join s in DBContext.Services
                         on c.ServiceId equals s.ServiceId
                         group s by s.ServiceName into G
                         select new GetCustomerConutsByServiceTypeResult
                         {
                             ServiceName = G.Key,
                             Count = G.Count()
                         };
  
            return result;
        }

        public IEnumerable<Customer> GetCustomerWillExpireWithinMonth()
        {
            DateTime dateTime = DateTime.UtcNow.Date;
            DbFunctions dfunc = null;
            //var customers = DBContext.Customers
            //                //.FromSqlRaw("Exec CustomersContractsWillExpireWithinMonth").AsEnumerable();
            //                .Join(
            //                  DBContext.ContractDates,
            //                  customer => customer.CstId,
            //                  contract => contract.CstId,
            //                  (cst, cont) => new Customer { CstId = cst.CstId, CstName = cst.CstName, ContractDates = (ICollection<ContractDate>)cont }
            //                )
            //                //.Where(y => SqlServerDbFunctionsExtensions.DateDiffDay(dfunc, dateTime, y.ContractDates.Select(p => p.ContractExpiryDate) ) <= 30)
            //                //.Where(z => SqlServerDbFunctionsExtensions.DateDiffDay(dfunc, dateTime, z.ContractExpiryDate) > 0)

            //                .Where(y => DBContext.ContractDates
            //                .Select(SqlServerDbFunctionsExtensions.DateDiffDay(dfunc,, y.ContractDates.ContractExpiryDate) <= 30) )
            //                ;

            var customers = from c in DBContext.Customers
                            join cd in DBContext.ContractDates
                            on c.CstId equals cd.CstId
                            where SqlServerDbFunctionsExtensions.DateDiffDay(dfunc, dateTime, cd.ContractExpiryDate) <= 30
                            where SqlServerDbFunctionsExtensions.DateDiffDay(dfunc, dateTime, cd.ContractExpiryDate) > 0
                            select new Customer {CstId = c.CstId, CstName = c.CstName, ContractDates = new HashSet<ContractDate>() { cd} };                   
            return customers;
        }

        //old
        //public IEnumerable<CountMonthsPerYear> GetCstCountMonthsPerYears(int year)
        //{
        //    var result = DBContext.Set<CountMonthsPerYear>().FromSqlInterpolated($"exec customerCountsPerYearAndMonth {year}").ToList();
        //    int x = 0;
        //    return result;
        //}

        public IEnumerable<GetCountMonthsPerYearResult> GetCstCountMonthsPerYears(int year)
        {
            var result = DBContext.Set<GetCountMonthsPerYearResult>().FromSqlInterpolated($"exec customerCountsPerYearAndMonthNew {year}").ToList();
            for(int i = 1; i < 13; i++)
            {
                if(result.All(x=>x.Month != i ))
                    result.Add(new GetCountMonthsPerYearResult { Month = i, Count = 0});
              
            }
            
            
            return result;
        }

    }
}
