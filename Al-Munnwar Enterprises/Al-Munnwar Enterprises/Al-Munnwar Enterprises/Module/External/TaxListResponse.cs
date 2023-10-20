using System;
using System.Collections.Generic;


namespace Al_Munnwar_Enterprises.Module.External
{
    public class TaxListResponse
    {
        public int totalRecords { get; set; }
        public List<Tax> Tax { get; set; }
    }
}
