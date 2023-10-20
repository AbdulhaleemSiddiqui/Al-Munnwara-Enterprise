using System;
using System.Collections.Generic;


namespace Al_Munnwar_Enterprises.Module.External
{
    public class PurchaseListResponse
    {
        public int totalRecords { get; set; }
        public List<PurchaseResponse> item { get; set; }
    }
}
