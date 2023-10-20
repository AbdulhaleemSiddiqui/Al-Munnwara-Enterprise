using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Al_Munnwar_Enterprises.Module.External
{
    public class QuotationListRequest
    {
        public int pageNumber { get; set; }
        public int pageSize { get; set; }
        public bool isPagining { get; set; } = true;
        public string sortBy { get; set; }
        public bool isSortAsc { get; set; }
        public string itemName { get; set; }
        public int  qty { get; set; }
        public int  rate { get; set; }
        public DateTime date { get; set; }
        public int status { get; set; }
    }

}
