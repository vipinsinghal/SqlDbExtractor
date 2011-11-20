namespace SqlDBE.Core.Utility
{
    public class StringHelper
    {
        public static string GetDefaultSqlDbOwner(string sqlDbOwner)
        {
            return string.IsNullOrEmpty(sqlDbOwner) ? Constants.SqlDefaultDbOwner : sqlDbOwner;
        }

        public static string GetDefaultTableName(string sqlTableName)
        {
            return string.IsNullOrEmpty(sqlTableName) ? Constants.SqlDefaultTableName : sqlTableName;
        }

        public static string GetDefaultConnectionString(string connString)
        {
            return string.IsNullOrEmpty(connString) ? Constants.SqlDefaultConnectionString : connString;
        }

        public static string GetDefaultColumnName(string sqlColumnName)
        {
            return string.IsNullOrEmpty(sqlColumnName) ? Constants.SqlDefaultColumnName : sqlColumnName;
        }
    }
}