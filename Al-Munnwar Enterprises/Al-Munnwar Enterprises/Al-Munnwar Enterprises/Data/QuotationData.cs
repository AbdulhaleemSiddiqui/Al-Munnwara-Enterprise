using Al_Munnwar_Enterprises.Module.External;
using Al_Munnwar_Enterprises.Module.Internal;
using GP_Portal.DAL.Base;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;


namespace Al_Munnwar_Enterprises.Data
{
    public class QuotationData : SQLHandler
    {
        public void SaveQuotation(QuotationRequest request)
        {
            try
            {
                var tId = GetTax(request.type);
                this.dbparams.strSQL = "Insert into Qutation(Qty,Rate,Tax_ID,Date,MarginFormulaRate,Item_ID,Unit_ID,Status) " +
                                        "select @qty,@rate,@tId,GetDate(),0,id ,Unit_ID,@status from item where name=@iName";
                this.dbparams.cmdType = "Text";
                dbparams.useReadOnlyConn = false;
                this.dbparams.sqlParams = new List<SqlParameter>() {
                new SqlParameter(){ ParameterName="status",SqlDbType=SqlDbType.Bit,Value=false},
                new SqlParameter(){ ParameterName="qty",SqlDbType=SqlDbType.Int,Value=request.qty},
                new SqlParameter(){ ParameterName="rate",SqlDbType=SqlDbType.Decimal,Value=request.rate},
                new SqlParameter(){ ParameterName="tId",SqlDbType=SqlDbType.Int,Value=tId[0].id},
                new SqlParameter(){ ParameterName="iName",SqlDbType=SqlDbType.VarChar,Value=request.iName},


                };
                ExecuteQuery("");

            }
            catch (Exception)
            {

                throw;
            }
        }
        public void UpdateQuotation(QuotationRequest request)
        {
            try
            {
                var tId = GetTax(request.type);
                this.dbparams.strSQL = "update Qutation set Status=@status,Qty=@qty,Rate=@rate,Tax_ID=@tId,Unit_Id=item.Unit_ID,Item_ID=item.id  from item where name=@iName and Qutation.id=@id ";
                this.dbparams.cmdType = "Text";
                this.dbparams.sqlParams = new List<SqlParameter>() {
                new SqlParameter(){ ParameterName="status",SqlDbType=SqlDbType.Bit,Value=request.status},
                 new SqlParameter(){ ParameterName="qty",SqlDbType=SqlDbType.Int,Value=request.qty},
                new SqlParameter(){ ParameterName="rate",SqlDbType=SqlDbType.Decimal,Value=request.rate},
                new SqlParameter(){ ParameterName="tId",SqlDbType=SqlDbType.Int,Value=tId[0].id},
                new SqlParameter(){ ParameterName="iName",SqlDbType=SqlDbType.VarChar,Value=request.iName},
                    new SqlParameter(){ ParameterName="id",SqlDbType=SqlDbType.Int,Value=request.id},
                };
                ExecuteQuery("");

            }
            catch (Exception)
            {

                throw;
            }
        } 
        public void UpdateStatus(QuotationRequest request)
        {
            try
            {
                this.dbparams.strSQL = "update Qutation set Status=@status where id=@id";
                this.dbparams.cmdType = "Text";
                this.dbparams.sqlParams = new List<SqlParameter>() {
                new SqlParameter(){ ParameterName="status",SqlDbType=SqlDbType.Bit,Value=request.status},
                new SqlParameter(){ ParameterName="id",SqlDbType=SqlDbType.Int,Value=request.id},
                };
                ExecuteQuery("");

            }
            catch (Exception)
            {

                throw;
            }
        }
        public void DeleteQuotation(int id)
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
        public List<QuotationResponse> GetQuotation(PageModel page, string name, ref int rowCount)
        {

            try
            {
                List<QuotationResponse> response = new List<QuotationResponse>();

                string filterQuery = "SELECT q.id as qId,Status,Qty,Rate,q.Date as qDate,t.Type as Type ,u.Name as uName,i.Name as iName"+
                                     " FROM Qutation as q inner join Item as i on q.Item_ID = i.ID inner join unit as u on i.Unit_ID = u.ID inner join Tax as t on q.Tax_ID = t.id ";
                this.dbparams.cmdType = "Text";
                this.dbparams.sqlParams = new List<SqlParameter>();
                if (!string.IsNullOrEmpty(name))
                {
                    filterQuery += "where name like @name";
                    this.dbparams.sqlParams.Add(new SqlParameter() { ParameterName = "name", SqlDbType = SqlDbType.VarChar, Value = '%' + name + '%' });

                }
                if (!string.IsNullOrEmpty(name))
                {
                    filterQuery += "where name like @name";
                    this.dbparams.sqlParams.Add(new SqlParameter() { ParameterName = "name", SqlDbType = SqlDbType.VarChar, Value = '%' + name + '%' });

                }

                string query = filterQuery + " ORDER BY " + orderBy(page.sortBy, page.isSortAsc);
                if (page.isPagining)
                {
                    this.dbparams.strSQL = "select count(*)  from ( " + filterQuery + ") as Qutation";
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
                        QuotationResponse quotation = new QuotationResponse();

                        if (!(row["qId"] is null))
                            quotation.id = Convert.ToInt32(row["qId"]);

                        if (!(row["Status"] is null))
                            quotation.status = Convert.ToBoolean(row["Status"]);

                        if (!(row["Qty"] is null))
                            quotation.qty = Convert.ToInt32(row["Qty"]);

                        if (!(row["Rate"] is null))
                            quotation.rate = Convert.ToInt32(row["Rate"]);

                        if (!(row["qDate"] is null))
                            quotation.date = Convert.ToDateTime(row["qDate"]);

                        if (!(row["Type"] is null))
                             quotation.type = Convert.ToString(row["Type"]);
              
                        if (!(row["uName"] is null))
                             quotation.uName = Convert.ToString(row["uName"]);
                        

                        if (!(row["iName"] is null))
                                quotation.iName = Convert.ToString(row["iName"]);
                        

                        response.Add(quotation);
                    }
                }
                return response;

            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<Tax> GetTax(string type)
        {

            try
            {
                List<Tax> response = new List<Tax>();
                dbparams.useReadOnlyConn = true;
               string query = "select * from  tax WITH (NOLOCK)";
                this.dbparams.cmdType = "Text";
                if (!string.IsNullOrEmpty(type))
                {
                    query += " where type=@type";
                    this.dbparams.sqlParams = new List<SqlParameter>() {
                      new SqlParameter(){ ParameterName="type",SqlDbType=SqlDbType.VarChar,Value=type}
                };
                }
                this.dbparams.strSQL = query;
                DataTable dt = RetrieveSqlDataTable("");
                if (dt != null && dt.Rows.Count > 0)
                {
                    foreach (DataRow row in dt.Rows)
                    {
                        Tax t = new Tax();

                        if (!(row["Id"] is null))
                            t.id = Convert.ToInt32(row["Id"]);
                        if (!(row["Type"] is null))
                            t.type = Convert.ToString(row["Type"]);

                        response.Add(t);
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
                List<ItemResponse> response = new List<ItemResponse>();

                string filterQuery = "SELECT *  FROM item WITH (NOLOCK) ";
                this.dbparams.cmdType = "Text";


                dbparams.useReadOnlyConn = true;

                this.dbparams.strSQL = filterQuery;
                DataTable dt = RetrieveSqlDataTable("");
                if (dt != null && dt.Rows.Count > 0)
                {
                    foreach (DataRow row in dt.Rows)
                    {
                        ItemResponse item = new ItemResponse();

                        if (!(row["ID"] is null))
                            item.id = Convert.ToInt32(row["ID"]);

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

        private string orderBy(string name, bool isSortBy)
        {
            string sort;
            switch (name.ToLower())
            {
                case "name":
                    sort = "qDate";
                    break;
                default:
                    sort = "qDate";
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
