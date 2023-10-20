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
    public class UserData : SQLHandler
    {
        

        public string LoginUser(User request)
        {

            try
            {
                this.dbparams.strSQL = "SELECT count(*) FROM [User] WITH (NOLOCK) where name =@name ";
                this.dbparams.cmdType = "Text";
                this.dbparams.sqlParams = new List<SqlParameter>();
                this.dbparams.sqlParams.Add(new SqlParameter() { ParameterName = "name", SqlDbType = SqlDbType.VarChar, Value = request.name });
                dbparams.useReadOnlyConn = true;

                int dt = ExecuteQuery("");
                if (dt == 1)
                {
                    this.dbparams.sqlParams.Add(new SqlParameter() { ParameterName = "password", SqlDbType = SqlDbType.VarChar, Value = request.password });
                    this.dbparams.strSQL +="and password=@password ";
                    dt = (int)ExecuteScalar("");
                    if (dt == 1)
                    {
                        return "";
                    }
                    else
                    {
                        return "Invalid Password";

                    }

                }
                else
                {
                    return "Invalid User Name";
                }
               
            }
            catch (Exception)
            {

                throw;
            }
        }

        public void SaveUser(User request)
        {
            try
            {
                this.dbparams.strSQL = "Insert into [User](Name,Email,Password,PhoneNbr) values(@name,@email,@password,@phoneNbr)";
                this.dbparams.cmdType = "Text";
                dbparams.useReadOnlyConn = false;
                this.dbparams.sqlParams = new List<SqlParameter>() {
                new SqlParameter(){ ParameterName="name",SqlDbType=SqlDbType.VarChar,Value=request.name},
                new SqlParameter(){ ParameterName="email",SqlDbType=SqlDbType.VarChar,Value=request.email},
                new SqlParameter(){ ParameterName="password",SqlDbType=SqlDbType.VarChar,Value=request.password},
                new SqlParameter(){ ParameterName="phoneNbr",SqlDbType=SqlDbType.Int,Value=request.phoneNbr},
                };
                ExecuteQuery("");

            }
            catch (Exception)
            {

                throw;
            }
        }
        public void UpdateUser(User request)
        {
            try
            {
                this.dbparams.strSQL = "update User set Name=@name  from User where id=@id";
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
        public void DeleteUser(int id)
        {
            try
            {
                this.dbparams.strSQL = "delete from User where id=@id";
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
        public List<User> GetUser(PageModel page, string name, ref int rowCount)
        {

            try
            {
                List<User> response = new List<User>();

                string filterQuery = "SELECT * FROM [User] WITH (NOLOCK) ";
                this.dbparams.cmdType = "Text";
                this.dbparams.sqlParams = new List<SqlParameter>();
                if (!string.IsNullOrEmpty(name))
                {
                    filterQuery += "where name like @name";
                    this.dbparams.sqlParams.Add(new SqlParameter() { ParameterName = "name", SqlDbType = SqlDbType.VarChar, Value = '%' + name + '%' });

                }

                string query = filterQuery + " ORDER BY " + orderBy(page.sortBy, page.isSortAsc);
                if (page.isPagining)
                {
                    this.dbparams.strSQL = "select count(*)  from ( " + filterQuery + ") as [User]";
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
                        User User = new User();

                        if (!(row["Id"] is null))
                            User.id = Convert.ToInt32(row["Id"]);
                        if (!(row["Name"] is null))
                            User.name = Convert.ToString(row["Name"]);

                        if (!(row["Email"] is null))
                            User.email = Convert.ToString(row["Email"]);

                        if (!(row["PhoneNbr"] is null))
                            User.phoneNbr = Convert.ToInt32(row["PhoneNbr"]);

                        if (!(row["Password"] is null))
                            User.password = Convert.ToString(row["Password"]);

          

                        response.Add(User);
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
