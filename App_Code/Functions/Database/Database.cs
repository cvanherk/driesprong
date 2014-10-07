using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web.Configuration; 

namespace AspNetDataHandler.Functions.Database
{
    public class Database : IDisposable
    {
        private readonly string _connectionString;
        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        public Database()
        {
            _connectionString = WebConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            //Checken of credentials goed zijn.
            using (
    var connection =
        new SqlConnection(_connectionString))
            {
                connection.Open();
                connection.Close();
                //Connectie voltooit
            }
        }

        public DataTable ExecuteQueryWithResult(string query, Dictionary<String, String> parameters = null )
        {
            var table = new DataTable();

            using (
    var connection =
        new SqlConnection(_connectionString))
            {
                
                connection.Open();
                using (var sqlCommand = new SqlCommand())
                {
                    sqlCommand.Connection = connection;
                    sqlCommand.CommandText = query;

                    if (parameters != null)
                    {
                        foreach (var o in parameters)
                        {
                            Guid guid;
                            var val = Guid.TryParse(o.Value, out guid);

                            if (val)
                                sqlCommand.Parameters.AddWithValue(o.Key, guid );
                            else
                                sqlCommand.Parameters.AddWithValue(o.Key, o.Value);
                        }
                    }

                    var reader = sqlCommand.ExecuteReader();
                    table.Load(reader);


                }
                connection.Close();    
            } 
            return table;
        }

        public void ExecuteNonResultQuery(string query, Dictionary<String, String> parameters = null)
        {
            using (
    var connection =
        new SqlConnection(_connectionString))
            {
                connection.Open();
                using (var sqlCommand = new SqlCommand())
                {
                    sqlCommand.Connection = connection;
                    sqlCommand.CommandText = query;

                    if (parameters != null)
                    {
                        foreach (var o in parameters)
                        {
                            Guid guid;
                            var val = Guid.TryParse(o.Value, out guid);

                            if (val)
                                sqlCommand.Parameters.AddWithValue(o.Key, guid);
                            else
                                sqlCommand.Parameters.AddWithValue(o.Key, o.Value);
                        }
                    }

                    var executed = sqlCommand.ExecuteNonQuery();

                    if (executed != 1)
                    {
                        throw new Exception("Query did not succeed!");
                    }

                }
                connection.Close();
            }
        }

        public DataRow GotoRecord(string tableName, Guid recordGuid)
        {

            var record = ExecuteQueryWithResult("SELECT * FROM " + tableName + " WHERE RecordGUID = @guid",
                new Dictionary<string, string> {{"guid", recordGuid.ToString("N")}});

            record.ColumnChanged +=
                (obj, args) =>
                    ExecuteNonResultQuery("UPDATE " + tableName + " SET [" + args.Column.ColumnName + "] = @value WHERE RecordGUID = @guid",
                        new Dictionary<string, string> {{"value", args.ProposedValue.ToString()}, {"guid", recordGuid.ToString("N")}});

            return (record.Rows.Count == 0 ? null : record.Rows[0]);
        }
    }
}