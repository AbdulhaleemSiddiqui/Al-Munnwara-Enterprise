using System;


namespace Al_Munnwar_Enterprises.Module.External
{
    public class QuotationResponse
    {
        public int? id { get; set; }
        public DateTime date { get; set; }
        public string iName { get; set; }
        public string uName { get; set; }
        public string type { get; set; }
        public int? qty { get; set; }
        public double? rate { get; set; }
        public bool status { get; set; }

    }
}
