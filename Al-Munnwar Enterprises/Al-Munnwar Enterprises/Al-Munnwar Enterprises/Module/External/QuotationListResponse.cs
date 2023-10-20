using System;
using System.Collections.Generic;


namespace Al_Munnwar_Enterprises.Module.External
{
    public class QuotationListResponse
    {
        public int totalRecords { get; set; }
        public List<QuotationResponse> Quotation { get; set; }
    }
}
