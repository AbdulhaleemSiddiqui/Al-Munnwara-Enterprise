using System;
using System.Collections.Generic;


namespace Al_Munnwar_Enterprises.Module.External
{
    public class VendorListResponse
    {
        public int totalRecords { get; set; }
        public List<Vendor> vendor { get; set; }
    }
}
