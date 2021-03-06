using Newtonsoft.Json;
using System;
using System.Collections.Generic;

#nullable disable

namespace CustomerContractData.Models
{
    public partial class Customer
    {
        public Customer()
        {
            ContractDates = new HashSet<ContractDate>();
        }

        public int CstId { get; set; }
        public string CstName { get; set; }

        public virtual ICollection<ContractDate> ContractDates { get; set; }

        public Customer(int CstID,string CstName, ICollection<ContractDate> contracts)
        {
            this.CstId = CstID;
            this.CstName = CstName;
            this.ContractDates = contracts;
        }
    }
}
