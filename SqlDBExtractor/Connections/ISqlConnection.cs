using System.Data;
using System.Data.SqlClient;

namespace SqlDBE.Core.Connections
{
    public interface ISqlConnection
    {
        string ConnString { get; set; }
        string GetSqlConnectionString();
        string GetSqlConnectionString(string connString);
        SqlConnection GetSqlConnectionInstance();
        SqlConnection GetSqlConnectionInstance(string connString);
        SqlCommand GetSqlCommandInstance(SqlConnection sqlConnection);
        SqlDataReader GetSqlDataReaderInstance(SqlCommand sqlCommand);
        SqlDataReader GetSqlDataReaderInstance(SqlCommand sqlCommand, CommandBehavior commandBehavior);
        SqlCommand GetSqlCommandInstance();
    }
}