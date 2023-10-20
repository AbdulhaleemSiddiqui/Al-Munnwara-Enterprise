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
    public class PurchaseData : SQLHandler
    {
        public void SavePurchase(PurchaseRequest request)
        {
            try
            {
                this.dbparams.strSQL = "Insert into purchase(ItemName,ItemPrice,Expancename,ExpanceAmount,Date) values(@ItemName,@ItemPrice,@Expancename,@ExpanceAmount,GetDate())";
                this.dbparams.cmdType = "Text";
                dbparams.useReadOnlyConn = false;
                this.dbparams.sqlParams = new List<SqlParameter>() {
                new SqlParameter(){ ParameterName="ItemName",SqlDbType=SqlDbType.Int,Value=request.ItemName},
                new SqlParameter(){ ParameterName="ItemPrice",SqlDbType=SqlDbType.Int,Value=request.ItemPrice},
                new SqlParameter(){ ParameterName="Expancename",SqlDbType=SqlDbType.Int,Value=request.Expancename},
                new SqlParameter(){ ParameterName="ExpanceAmount",SqlDbType=SqlDbType.VarChar,Value=request.ExpanceAmount},
                };
                ExecuteQuery("");

            }
            catch (Exception)
            {

                throw;
            }
        }
        public void Update(PurchaseRequest request)
        {
            try
            {
                this.dbparams.strSQL = "update purchase set ItemName=@ItemName,ItemPrice=@ItemPrice,Expancename=@Expancename,ExpanceAmount=@ExpanceAmount,Date=GetDate() where id=@id";
                this.dbparams.cmdType = "Text";
                this.dbparams.sqlParams = new List<SqlParameter>() {
                new SqlParameter(){ ParameterName="ItemName",SqlDbType=SqlDbType.VarChar,Value=request.ItemName},
                new SqlParameter(){ ParameterName="ItemPrice",SqlDbType=SqlDbType.Int,Value=request.ItemPrice},
                new SqlParameter(){ ParameterName="Expancename",SqlDbType=SqlDbType.VarChar,Value=request.Expancename},
                new SqlParameter(){ ParameterName="ExpanceAmount",SqlDbType=SqlDbType.Int,Value=request.ExpanceAmount},
                new SqlParameter(){ ParameterName="Id",SqlDbType=SqlDbType.Int,Value=request.Id},
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
                this.dbparams.strSQL = "delete from purchase where id=@id";
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
        public List<PurchaseResponse> Get(PageModel page, string name, ref int rowCount)
        {

            try
            {
                List<PurchaseResponse> response = new List<PurchaseResponse>();

                string filterQuery = "SELECT * FROM purchase WITH (NOLOCK) ";
                this.dbparams.cmdType = "Text";
                this.dbparams.sqlParams = new List<SqlParameter>();
                if (!string.IsNullOrEmpty(name))
                {
                    filterQuery += "where name like @name";
                    this.dbparams.sqlParams.Add(new SqlParameter() { ParameterName = "name", SqlDbType = SqlDbType.VarChar, Value = '%' + name + '%' });

                }
                //if (!string.IsNullOrEmpty(vendorName))
                //{
                //    filterQuery += "where id like @name";
                //    this.dbparams.sqlParams.Add(new SqlParameter() {ParameterName="name",SqlDbType=SqlDbType.VarChar,Value='%'+name+'%' });

                //}
                string query = filterQuery + " ORDER BY " + orderBy(page.sortBy, page.isSortAsc);
                if (page.isPagining)
                {
                    this.dbparams.strSQL = "select count(*)  from ( " + filterQuery + ") as item";
                    dbparams.useReadOnlyConn = true;
                    rowCount = int.Parse(ExecuteScalar("").ToString());

                    this.dbparams.sqlParams.Add(new SqlParameter() { ParameterName = "offset", SqlDbType = SqlDbType.Int, Value = (page.pageNumber - 1) * page.pageSize });
                    this.dbparams.sqlParams.Add(new SqlParameter() { ParameterName = "size", SqlDbType = SqlDbType.Int, Value = page.pageSize });
                    query += string.Format(" offset @offset row fetch next @size row only", page.sortBy, page.isSortAsc ? "" : "desc");
                }
                dbparams.useReadOnlyConn = true;

                this.dbparams.strSQL = query;
                DataTable dt = RetrieveSqlDataTable("");
                if (dt != null && dt.Rows.Count > 0)
                {
                    foreach (DataRow row in dt.Rows)
                    {
                        PurchaseResponse p = new PurchaseResponse();


                        if (!(row["Date"] is null))
                            p.date = Convert.ToDateTime(row["Date"]);

                        if (!(row["ExpanceAmount"] is null))
                            p.ExpanceAmount = Convert.ToInt32(row["ExpanceAmount"]);

                        if (!(row["ItemName"] is null))
                            p.Expancename = Convert.ToString(row["Expancename"]);

                        if (!(row["ItemPrice"] is null))
                            p.ItemPrice = Convert.ToInt32(row["ItemPrice"]);

                        if (!(row["ItemName"] is null))
                            p.ItemName = Convert.ToString(row["ItemName"]);

                        if (!(row["Id"] is null))
                            p.id = Convert.ToInt16(row["Id"]);

                        response.Add(p);
                    }
                }
                return response;

            }
            catch (Exception)
            {

                throw;
            }
        }
       
        public List<ItemResponse> GetItem()
        {

            try
            {
                List<ItemResponse> lst = new List<ItemResponse>();

                dbparams.useReadOnlyConn = true;
                this.dbparams.strSQL = "select * from Item WITH (NOLOCK) ";
                this.dbparams.cmdType = "Text";
          
                DataTable dt = RetrieveSqlDataTable("");
                if (dt != null && dt.Rows.Count > 0)
                {
                    ItemResponse i = new ItemResponse();

                    foreach (DataRow row in dt.Rows)
                    {
                        if (!(row["Id"] is null))
                            i.id = Convert.ToInt32(row["Id"]);
                        if (!(row["Name"] is null))
                            i.name = Convert.ToString(row["Name"]);
                        lst.Add(i);                    }
                }
                return lst;

            }
            catch (Exception)
            {

                throw;
            }
        }
      
        private string orderBy(string name, bool isSortBy)
        {
            string sort;
            switch (name.ToLower())
            {
                case "name":
                    sort = "name";
                    break;
                default:
                    sort = "name";
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
