using Newtonsoft.Json;
using System;
using System.Collections.Generic;

#nullable disable

namespace CustomerContractData.Models
{
    public partial class ContractDate
    {
        public int CstId { get; set; }
        public int ServiceId { get; set; }
        public DateTime ContractDate1 { get; set; }
        public DateTime ContractExpiryDate { get; set; }
        public int ContractId { get; set; }
        [JsonIgnore]
        public virtual Customer Cst { get; set; }
        [JsonIgnore]
        public virtual Service Service { get; set; }
    }
}
