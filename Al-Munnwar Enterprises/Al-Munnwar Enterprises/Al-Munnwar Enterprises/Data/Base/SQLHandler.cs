using Al_Munnwar_Enterprises.Module.Internal;
using Al_Munnwar_Enterprises.Utility;
using System;
using System.Data;
using System.Data.SqlClient;
namespace GP_Portal.DAL.Base
{
    public class SQLHandler
    {
        public DbModel dbparams { get; set; }

        public SQLHandler()
        {
            dbparams = new DbModel();
            dbparams.defaultConnectionString = ConnectionHandler.defaultConnectionString;
            dbparams.readOnlyConnectionString = ConnectionHandler.readOnlyConnectionString;
        }

        public DataTable RetrieveSqlDataTable(string messageId)
        {
            try
            {
                string strDB = CheckReadOnlyConnectionString(messageId);

                using (SqlConnection conn = new SqlConnection(strDB))
                {
                    SqlDataAdapter dap = new SqlDataAdapter(dbparams.strSQL, conn);

                    if (dbparams.sqlParams != null && dbparams.sqlParams.Count > 0)
                        dap.SelectCommand.Parameters.AddRange(dbparams.sqlParams.ToArray());
                    if (dbparams.cmdType == "stored_procedure")
                        dap.SelectCommand.CommandType = CommandType.StoredProcedure;

                    DataTable dt = new DataTable();
                    dap.Fill(dt);
                    dap.SelectCommand.Parameters.Clear();
                    return dt;
                }
            }
            catch (Exception e)
            { throw e; }

        }

        public DataSet RetrieveSqlDataSet(string messageId)
        {
            try
            {
                string strDB = CheckReadOnlyConnectionString(messageId);

                using (SqlConnection conn = new SqlConnection(strDB))
                {
                    SqlDataAdapter dap = new SqlDataAdapter(dbparams.strSQL, conn);

                    if (dbparams.sqlParams != null && dbparams.sqlParams.Count > 0)
                        dap.SelectCommand.Parameters.AddRange(dbparams.sqlParams.ToArray());
                    if (dbparams.cmdType == "stored_procedure")
                        dap.SelectCommand.CommandType = CommandType.StoredProcedure;

                    DataSet ds = new DataSet();
                    dap.Fill(ds);
                    dap.SelectCommand.Parameters.Clear();
                    return ds;
                }
            }
            catch (Exception e)
            { throw e; }

        }

        public int ExecuteQuery(string messageId)
        {
            try
            {
                var strDBConn = CheckReadOnlyConnectionString(messageId);

                using (SqlConnection connection = new SqlConnection(strDBConn))
                using (SqlCommand command = new SqlCommand(dbparams.strSQL, connection))
                {
                    CommandType ct = CommandType.Text;
                    if (dbparams.cmdType == "stored_procedure")
                        ct = CommandType.StoredProcedure;

                    command.CommandTimeout = 0;
                    command.CommandType = ct;


                    if (dbparams.sqlParams != null && dbparams.sqlParams.Count > 0)
                        command.Parameters.AddRange(dbparams.sqlParams.ToArray());

                    connection.Open();

                    int rowsReturned = command.ExecuteNonQuery();

                    command.Parameters.Clear();

                    return rowsReturned;
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public object ExecuteScalar(string messageId)
        {
            try
            {
                var strDBConn = CheckReadOnlyConnectionString(messageId);
                using (SqlConnection connection = new SqlConnection(strDBConn))
                using (SqlCommand command = new SqlCommand(dbparams.strSQL, connection))
                {
                    CommandType ct = CommandType.Text;
                    if (dbparams.cmdType == "stored_procedure")
                        ct = CommandType.StoredProcedure;

                    command.CommandTimeout = 0;
                    command.CommandType = ct;


                    if (dbparams.sqlParams != null && dbparams.sqlParams.Count > 0)
                        command.Parameters.AddRange(dbparams.sqlParams.ToArray());

                    connection.Open();
                    var record = command.ExecuteScalar();

                    command.Parameters.Clear();
                    return record;
                }
            }

            catch (Exception e)
            { throw e; }
        }

        public void BulkInsert(DataTable dtrecords, string tableName, string messageId)
        {
            try
            {
                var strDBConn = CheckReadOnlyConnectionString(messageId);

                using (SqlConnection connection = new SqlConnection(strDBConn))
                {
                    SqlBulkCopy bulkCopy = new SqlBulkCopy(connection);

                    bulkCopy.DestinationTableName = tableName;
                    connection.Open();
                    MapColumns(dtrecords, bulkCopy);
                    // write the data in the "dataTable"
                    bulkCopy.WriteToServer(dtrecords);
                    connection.Close();

                }

            }
            catch (Exception e)
            {
                throw e;
            }
        }

        private void MapColumns(DataTable infoTable, SqlBulkCopy bulkCopy)
        {

            foreach (DataColumn dc in infoTable.Columns)
            {
                bulkCopy.ColumnMappings.Add(dc.ColumnName,
                  dc.ColumnName);
            }
        }
        private string GetReadOnlyConnectionString()
        {
            return dbparams.readOnlyConnectionString;
        }

        private string GetDefaultConnectionString()
        {
            return dbparams.defaultConnectionString;
        }

        public string CheckReadOnlyConnectionString(string messageId)
        {
            string strDBCon = string.Empty;
            try
            {
                string ReadOnlyConn = string.Empty;
                if (!dbparams.useReadOnlyConn || string.IsNullOrEmpty(ReadOnlyConn = GetReadOnlyConnectionString()))
                {
                    dbparams.useReadOnlyConn = false;
                    strDBCon = GetDefaultConnectionString();
                }
                else
                {
                    strDBCon = ReadOnlyConn;
                    var conn = new SqlConnection(strDBCon);
                    conn.Open();
                    conn.Close();
                }
            }
            catch (Exception e)
            {
                dbparams.useReadOnlyConn = false;
                strDBCon = GetDefaultConnectionString();
            }

            return strDBCon;
        }
    }
}