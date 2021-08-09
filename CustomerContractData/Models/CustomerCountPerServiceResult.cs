using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CustomerContractData.Models
{
    [Keyless]
    public class GetCustomerConutsByServiceTypeResultOld
    {
        public int Counts{ get; set; }

    }
}
