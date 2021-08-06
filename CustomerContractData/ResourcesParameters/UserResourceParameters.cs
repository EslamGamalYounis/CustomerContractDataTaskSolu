using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CustomerContractData.ResourcesParameters
{
    public class UserResourceParameters
    {
        const int maxPageSize = 20;
        public string SearchQuery { get; set; }
        public int PageNumber { get; set; }
        public int _pageSize { get; set; }

        public int PageSize
        {
            get => _pageSize;
            set => _pageSize = (value > maxPageSize) ? maxPageSize : value;
        }
    }
}
