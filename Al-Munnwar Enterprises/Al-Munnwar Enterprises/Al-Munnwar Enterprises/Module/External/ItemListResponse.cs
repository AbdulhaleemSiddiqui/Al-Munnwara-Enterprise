using System;
using System.Collections.Generic;


namespace Al_Munnwar_Enterprises.Module.External
{
    public class ItemListResponse
    {
        public int totalRecords { get; set; }
        public List<ItemResponse> item { get; set; }
    }
}
