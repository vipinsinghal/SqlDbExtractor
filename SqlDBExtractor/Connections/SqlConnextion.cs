using System.Data;
using System.Data.SqlClient;
using SqlDBE.Core.Utility;

namespace SqlDBE.Core.Connections
{
    public class SqlConnextion : ISqlConnection
    {
        public SqlConnextion()
        {
        }

        public SqlConnextion(string strConnString)
        {
            ConnString = strConnString;
        }

        #region Implementation of ISqlConnection

        public string ConnString { get; set; }

        public string GetSqlConnectionString()
        {
            return GetSqlConnectionString(ConnString);
        }

        public string GetSqlConnectionString(string connString)
        {
            Constants.SqlConnString = connString;
            return string.IsNullOrEmpty(connString) ? Constants.SqlDefaultConnectionString : connString;
        }

        public SqlConnection GetSqlConnectionInstance()
        {
            return new SqlConnection();
        }

        public SqlConnection GetSqlConnectionInstance(string connString)
        {
            return new SqlConnection(connString);
        }

        public SqlCommand GetSqlCommandInstance(SqlConnection sqlConnection)
        {
            return sqlConnection.CreateCommand();
        }

        public SqlDataReader GetSqlDataReaderInstance(SqlCommand sqlCommand)
        {
            return sqlCommand.ExecuteReader();
        }

        public SqlDataReader GetSqlDataReaderInstance(SqlCommand sqlCommand, CommandBehavior commandBehavior)
        {
            return sqlCommand.ExecuteReader(commandBehavior);
        }

        public SqlCommand GetSqlCommandInstance()
        {
            return new SqlCommand();
        }

        #endregion
    }
}