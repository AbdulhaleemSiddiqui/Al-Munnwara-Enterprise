using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace Al_Munnwar_Enterprises.Module.Internal
{
    public class DbModel
    {

        public string defaultConnectionString { get; set; }
        public string readOnlyConnectionString { get; set; }
        public bool useReadOnlyConn { get; set; }
        public string strSQL { get; set; }
        public string cmdType { get; set; }
        public List<SqlParameter> sqlParams { get; set; }
        public string sql_message { get; set; }
        public string sql_error { get; set; }

    }
}
