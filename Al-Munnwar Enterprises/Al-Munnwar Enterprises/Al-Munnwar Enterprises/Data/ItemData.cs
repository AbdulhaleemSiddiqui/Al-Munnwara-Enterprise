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
    public class ItemData : SQLHandler
    {
        public void SaveItem(ItemRequest request)
        {
            try
            {
                this.dbparams.strSQL = "Insert into Item(Price,Vendor_Id,Unit_Id,Date,Name) values(@price,@vendorId,@unitId,GetDate(),@name)";
                this.dbparams.cmdType = "Text";
                dbparams.useReadOnlyConn = false;
                this.dbparams.sqlParams = new List<SqlParameter>() {
                new SqlParameter(){ ParameterName="price",SqlDbType=SqlDbType.Int,Value=request.price},
                new SqlParameter(){ ParameterName="vendorId",SqlDbType=SqlDbType.Int,Value=request.vId},
                new SqlParameter(){ ParameterName="unitId",SqlDbType=SqlDbType.Int,Value=request.uId},
                new SqlParameter(){ ParameterName="name",SqlDbType=SqlDbType.VarChar,Value=request.name},
                };
                ExecuteQuery("");

            }
            catch (Exception)
            {

                throw;
            }
        }
        public void UpdateItem(ItemRequest request)
        {
            try
            {
                this.dbparams.strSQL = "update item set Price=@price,Vendor_Id=@vendorId,Unit_Id=@unitId,Date=GetDate(),Name=@name  from item where id=@id";
                this.dbparams.cmdType = "Text";
                this.dbparams.sqlParams = new List<SqlParameter>() {
                new SqlParameter(){ ParameterName="price",SqlDbType=SqlDbType.Int,Value=request.price},
                new SqlParameter(){ ParameterName="vendorId",SqlDbType=SqlDbType.Int,Value=request.vId},
                new SqlParameter(){ ParameterName="unitId",SqlDbType=SqlDbType.Int,Value=request.uId},
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
        public void DeleteItem(int id)
        {
            try
            {
                this.dbparams.strSQL = "delete from item where id=@id";
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
        public List<ItemResponse> GetItem(PageModel page,string name,string vendorName, ref int rowCount)
        {

            try
            {
                List<ItemResponse> response = new List<ItemResponse>();
              
               string filterQuery = "SELECT * FROM item WITH (NOLOCK) ";
                this.dbparams.cmdType = "Text";
                this.dbparams.sqlParams = new List<SqlParameter>();
                if (!string.IsNullOrEmpty(name))
                {
                    filterQuery += "where name like @name";
                    this.dbparams.sqlParams.Add(new SqlParameter() {ParameterName="name",SqlDbType=SqlDbType.VarChar,Value='%'+name+'%' });

                }
                //if (!string.IsNullOrEmpty(vendorName))
                //{
                //    filterQuery += "where id like @name";
                //    this.dbparams.sqlParams.Add(new SqlParameter() {ParameterName="name",SqlDbType=SqlDbType.VarChar,Value='%'+name+'%' });

                //}
                string query = filterQuery + " ORDER BY " + orderBy(page.sortBy,page.isSortAsc);
                if (page.isPagining)
                {
                    this.dbparams.strSQL = "select count(*)  from ( " + filterQuery + ") as item";
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
                        ItemResponse item = new ItemResponse();

                        if (!(row["Id"] is null))
                            item.id = Convert.ToInt32(row["Id"]);
                        if (!(row["Vendor_Id"] is null))
                        {
                           
                            int id = Convert.ToInt32(row["Vendor_Id"]);
                             var vResponse = GetVendorById(id);
                            if (vResponse != null)
                            {
                                if (item.vendor == null)
                                {
                                    item.vendor = new Vendor(); // Initialize item.vendor if it's null
                                }

                                item.vendor.id = vResponse.id;     
                                item.vendor.name = vResponse.name;
                            }
                        }
                        if (!(row["Unit_Id"] is null))
                        {
                            int id = Convert.ToInt32(row["Unit_Id"]);
                            var uResponse = GetUnitById(id);
                            if (uResponse != null)
                            {
                                if (item.unit == null)
                                {
                                    item.unit = new Unit(); // Initialize item.vendor if it's null
                                }

                                item.unit.id = uResponse.id;
                                item.unit.name = uResponse.name;
                            }
                        }
                        if (!(row["Date"] is null))
                            item.date = Convert.ToDateTime(row["Date"]);
                        
                        if (!(row["Price"] is null))
                            item.price = Convert.ToInt32(row["Price"]);
                        
                        if (!(row["Name"] is null))
                            item.name = Convert.ToString(row["Name"]);

                        response.Add(item);
                    }
                }
                return response;

            }
            catch (Exception)
            {

                throw;
            }
        }
        private Unit GetUnitById(int id)
        {

            try
            {
                Unit u = new Unit();
                dbparams.useReadOnlyConn = true;
                this.dbparams.strSQL = "select * from  unit WITH (NOLOCK) where id=@id ";
                this.dbparams.cmdType = "Text";
                this.dbparams.sqlParams = new List<SqlParameter>() {
                new SqlParameter(){ ParameterName="id",SqlDbType=SqlDbType.Int,Value=id}
                };

                DataTable dt = RetrieveSqlDataTable("");
                if (dt != null && dt.Rows.Count > 0)
                {
                    foreach (DataRow row in dt.Rows)
                    {
                        if (!(row["Id"] is null))
                            u.id = Convert.ToInt32(row["Id"]);
                        if (!(row["Name"] is null))
                            u.name = Convert.ToString(row["Name"]);
                    }
                }
                return u;

            }
            catch (Exception)
            {

                throw;
            }
        }
        private Vendor GetVendorById(int id)
        {

            try
            {
                Vendor v = new Vendor();
                dbparams.useReadOnlyConn = true;
                this.dbparams.strSQL = "select * from vendor WITH (NOLOCK) where id=@id ";
                this.dbparams.cmdType = "Text";
                this.dbparams.sqlParams = new List<SqlParameter>() {
                new SqlParameter(){ ParameterName="id",SqlDbType=SqlDbType.Int,Value=id}
                };
                DataTable dt = RetrieveSqlDataTable("");
                if (dt != null && dt.Rows.Count > 0)
                {
                    foreach (DataRow row in dt.Rows)
                    {
                        if (!(row["Id"] is null))
                            v.id = Convert.ToInt32(row["Id"]);
                        if (!(row["Name"] is null))
                           v .name= Convert.ToString(row["Name"]);     
                    }
                }
                return v;

            }
            catch (Exception)
            {

                throw;
            }
        }   
        public List<Unit> GetUnit()
        {

            try
            {
                List<Unit> response = new List<Unit>();
                dbparams.useReadOnlyConn = true;
                this.dbparams.strSQL = "select * from  unit WITH (NOLOCK)  ";
                this.dbparams.cmdType = "Text";
         
                DataTable dt = RetrieveSqlDataTable("");
                if (dt != null && dt.Rows.Count > 0)
                {
                    foreach (DataRow row in dt.Rows)
                    {
                        Unit u = new Unit();

                        if (!(row["Id"] is null))
                            u.id = Convert.ToInt32(row["Id"]);
                        if (!(row["Name"] is null))
                            u.name = Convert.ToString(row["Name"]);

                        response.Add(u);
                    }
                }
                return response;

            }
            catch (Exception)
            {

                throw;
            }
        }
        public List<Vendor> GetVendor()
        {

            try
            {
                List<Vendor> response = new List<Vendor>();
                dbparams.useReadOnlyConn = true;
                this.dbparams.strSQL = "select * from  vendor WITH (NOLOCK)";
                this.dbparams.cmdType = "Text";

                DataTable dt = RetrieveSqlDataTable("");
                if (dt != null && dt.Rows.Count > 0)
                {
                    foreach (DataRow row in dt.Rows)
                    {
                        Vendor v = new Vendor();

                        if (!(row["Id"] is null))
                            v.id = Convert.ToInt32(row["Id"]);
                        if (!(row["Name"] is null))
                            v.name = Convert.ToString(row["Name"]);

                        response.Add(v);
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
