using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CustomerContractData.Models
{
    [Keyless]
    public class CountMonthsPerYear
    {
        public int Count { get; set; }
        public string Month { get; set; }

    }
}
