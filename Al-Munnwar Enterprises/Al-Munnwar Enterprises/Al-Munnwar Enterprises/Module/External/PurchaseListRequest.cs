using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Al_Munnwar_Enterprises.Module.External
{
    public class PurchaseListRequest
    {
        public int pageNumber { get; set; }
        public int pageSize { get; set; }
        public bool isPagining { get; set; } = true;
        public string sortBy { get; set; }
        public bool isSortAsc { get; set; }
        public string name { get; set; }
        public string vendorName { get; set; }
    }
}
