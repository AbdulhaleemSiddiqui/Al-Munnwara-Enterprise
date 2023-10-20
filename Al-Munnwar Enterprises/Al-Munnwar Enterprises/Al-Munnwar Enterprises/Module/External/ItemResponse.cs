using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Al_Munnwar_Enterprises.Module.External
{
    public class ItemResponse
    {
        public int? id { get; set; }
        public DateTime date { get; set; }
        public string name { get; set; }
        public Vendor vendor{ get; set; }
        public Unit unit{ get; set; }
        public int? price { get; set; }

    }
}
