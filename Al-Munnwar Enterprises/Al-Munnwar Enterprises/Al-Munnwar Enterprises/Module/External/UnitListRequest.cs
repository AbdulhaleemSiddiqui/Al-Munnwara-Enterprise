﻿

namespace Al_Munnwar_Enterprises.Module.External
{
    public class UnitListRequest
    {
        public int pageNumber { get; set; }
        public int pageSize { get; set; }
        public bool isPagining { get; set; } = true;
        public string sortBy { get; set; }
        public bool isSortAsc { get; set; }
        public string name { get; set; }
   
    }
}
