using Al_Munnwar_Enterprises.Module.External;
using Al_Munnwar_Enterprises.Module.Internal;
using GP_Portal.DAL.Base;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace Al_Munnwar_Enterprises.Data
{
    public class VendorData : SQLHandler
    {
        public void SaveVendor(Vendor request)
        {
            try
            {
                this.dbparams.strSQL = "Insert into Vendor(Name) values(@name)";
                this.dbparams.cmdType = "Text";
                dbparams.useReadOnlyConn = false;
                this.dbparams.sqlParams = new List<SqlParameter>() {
                new SqlParameter(){ ParameterName="name",SqlDbType=SqlDbType.VarChar,Value=request.name},
                };
                ExecuteQuery("");

            }
            catch (Exception)
            {

                throw;
            }
        }
        public void UpdateVendor(Vendor request)
        {
            try
            {
                this.dbparams.strSQL = "update Vendor set Name=@name  from Vendor where id=@id";
                this.dbparams.cmdType = "Text";
                this.dbparams.sqlParams = new List<SqlParameter>() {
                new SqlParameter(){ ParameterName="name",SqlDbType=SqlDbType.VarChar,Value=request.name},
                new SqlParameter(){ ParameterName="id",SqlDbType=SqlDbType.Int,Value=request.id},
                };
                ExecuteQuery("");

            }
            catch (Exception)
            {

                throw;
            }
        }
        public void DeleteVendor(int id)
        {
            try
            {
                this.dbparams.strSQL = "delete from Vendor where id=@id";
                this.dbparams.cmdType = "Text";
                this.dbparams.sqlParams = new List<SqlParameter>() {
                new SqlParameter(){ ParameterName="id",SqlDbType=SqlDbType.Int,Value=id}
                };
                ExecuteQuery("");

            }
            catch (Exception)
            {

                throw;
            }
        }
        public List<Vendor> GetVendor(PageModel page,string name, ref int rowCount)
        {

            try
            {
                List<Vendor> response = new List<Vendor>();
              
               string filterQuery = "SELECT * FROM Vendor WITH (NOLOCK) ";
                this.dbparams.cmdType = "Text";
                this.dbparams.sqlParams = new List<SqlParameter>();
                if (!string.IsNullOrEmpty(name))
                {
                    filterQuery += "where name like @name";
                    this.dbparams.sqlParams.Add(new SqlParameter() {ParameterName="name",SqlDbType=SqlDbType.VarChar,Value='%'+name+'%' });

                }
               
                string query = filterQuery + " ORDER BY " + orderBy(page.sortBy,page.isSortAsc);
                if (page.isPagining)
                {
                    this.dbparams.strSQL = "select count(*)  from ( " + filterQuery + ") as Vendor";
                    dbparams.useReadOnlyConn = true;
                    rowCount = int.Parse(ExecuteScalar("").ToString());

                    this.dbparams.sqlParams.Add(new SqlParameter() { ParameterName = "offset", SqlDbType = SqlDbType.Int, Value = (page.pageNumber-1) *page.pageSize});
                    this.dbparams.sqlParams.Add(new SqlParameter() { ParameterName = "size", SqlDbType = SqlDbType.Int, Value = page.pageSize});
                    query += string.Format(" offset @offset row fetch next @size row only", page.sortBy, page.isSortAsc ? "" : "desc");
                }
                dbparams.useReadOnlyConn = true;

                this.dbparams.strSQL = query;
                DataTable dt = RetrieveSqlDataTable("");
                if (dt != null && dt.Rows.Count > 0)
                {
                    foreach (DataRow row in dt.Rows)
                    {
                        Vendor Vendor = new Vendor();

                        if (!(row["Id"] is null))
                            Vendor.id = Convert.ToInt32(row["Id"]);
                    

                        if (!(row["Name"] is null))
                            Vendor.name = Convert.ToString(row["Name"]);

                        response.Add(Vendor);
                    }
                }
                return response;

            }
            catch (Exception)
            {

                throw;
            }
        }
   
        
        private string orderBy(string name,bool isSortBy)
        {
            string sort;
            switch (name.ToLower())
            {
                case "name":
                    sort= "name";
                    break;
                default:
                    sort= "name";
                    break;
            }
            if (isSortBy)
            {
                sort += " asc";
            }
            else
            {
                sort += " desc";
            }
            return sort;

        }
    }
}
