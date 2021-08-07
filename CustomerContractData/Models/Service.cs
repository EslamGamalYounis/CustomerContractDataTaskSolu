using Newtonsoft.Json;
using System;
using System.Collections.Generic;

#nullable disable

namespace CustomerContractData.Models
{
    public partial class Service
    {
        public Service()
        {
            ContractDates = new HashSet<ContractDate>();
        }

        public int ServiceId { get; set; }
        public string ServiceName { get; set; }

        [JsonIgnore]
        public virtual ICollection<ContractDate> ContractDates { get; set; }
    }
}
