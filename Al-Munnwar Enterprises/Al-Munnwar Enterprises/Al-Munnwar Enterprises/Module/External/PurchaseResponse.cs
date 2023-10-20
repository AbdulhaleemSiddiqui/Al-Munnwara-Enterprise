using System;

namespace Al_Munnwar_Enterprises.Module.External
{
    public class PurchaseResponse
    {
        public int? id { get; set; }
        public DateTime date { get; set; }
        public string ItemName { get; set; }
        public int? ItemPrice { get; set; }
        public string Expancename { get; set; }
        public int? ExpanceAmount { get; set; }

    }
}
