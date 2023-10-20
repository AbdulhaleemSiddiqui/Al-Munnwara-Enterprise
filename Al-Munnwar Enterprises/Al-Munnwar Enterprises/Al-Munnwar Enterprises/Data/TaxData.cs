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
    public class TaxData : SQLHandler
    {
        public void SaveTax(Tax request)
        {
            try
            {
                this.dbparams.strSQL = "Insert into Tax(Type,Amount) values(@type,@amount)";
                this.dbparams.cmdType = "Text";
                dbparams.useReadOnlyConn = false;
                this.dbparams.sqlParams = new List<SqlParameter>() {
                new SqlParameter(){ ParameterName="type",SqlDbType=SqlDbType.VarChar,Value=request.type},
                new SqlParameter(){ ParameterName="amount",SqlDbType=SqlDbType.Decimal,Value=request.amount}
                };
                ExecuteQuery("");

            }
            catch (Exception)
            {

                throw;
            }
        }
        public void UpdateTax(Tax request)
        {
            try
            {
                this.dbparams.strSQL = "update Tax set Type=@type,Amount=@amount  from Tax where id=@id";
                this.dbparams.cmdType = "Text";
                this.dbparams.sqlParams = new List<SqlParameter>() {
                new SqlParameter(){ ParameterName="type",SqlDbType=SqlDbType.VarChar,Value=request.type},
                new SqlParameter(){ ParameterName="id",SqlDbType=SqlDbType.Int,Value=request.id},
                                new SqlParameter(){ ParameterName="amount",SqlDbType=SqlDbType.Int,Value=request.amount}

                };
                ExecuteQuery("");

            }
            catch (Exception)
            {

                throw;
            }
        }
        public void DeleteTax(int id)
        {
            try
            {
                this.dbparams.strSQL = "delete from Tax where id=@id";
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
        public List<Tax> GetTax(PageModel page, string type, ref int rowCount)
        {

            try
            {
                List<Tax> response = new List<Tax>();

                string filterQuery = "SELECT * FROM Tax WITH (NOLOCK) ";
                this.dbparams.cmdType = "Text";
                this.dbparams.sqlParams = new List<SqlParameter>();
                if (!string.IsNullOrEmpty(type))
                {
                    filterQuery += "where name like @type";
                    this.dbparams.sqlParams.Add(new SqlParameter() { ParameterName = "type", SqlDbType = SqlDbType.VarChar, Value = '%' + type + '%' });

                }

                string query = filterQuery + " ORDER BY " + orderBy(page.sortBy, page.isSortAsc);
                if (page.isPagining)
                {
                    this.dbparams.strSQL = "select count(*)  from ( " + filterQuery + ") as Tax";
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
                        Tax Tax = new Tax();

                        if (!(row["Id"] is null))
                            Tax.id = Convert.ToInt32(row["Id"]);


                        if (!(row["Type"] is null))
                            Tax.type = Convert.ToString(row["Type"]);
                        if (!(row["Amount"] is null))
                            Tax.amount = Convert.ToDecimal(row["Amount"]);

                        response.Add(Tax);
                    }
                }
                return response;

            }
            catch (Exception)
            {

                throw;
            }
        }


        private string orderBy(string type, bool isSortBy)
        {
            string sort;
            switch (type.ToLower())
            {
                case "type":
                    sort = "type";
                    break;
                default:
                    sort = "amount";
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
