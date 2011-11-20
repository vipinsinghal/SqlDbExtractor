using SqlDBE.Core.Utility;

namespace SqlDBE.Core.SqlMetaData
{
    public class SqlQuery
    {
        public static string GetSqlForDatabaseOwner()
        {
            return Constants.SqlDbOwnerQuery;
        }

        public static string GetSqlForTableName()
        {
            return GetSqlForTableName(Constants.SqlDbOwner);
        }

        public static string GetSqlForTableName(string sqlTableOwner)
        {
            return string.Format(Constants.SqlTableNameQuery, StringHelper.GetDefaultSqlDbOwner(sqlTableOwner));
        }

        public static string GetSqlForTableSchema()
        {
            return GetSqlForTableSchema(Constants.SqlDefaultTableName, Constants.SqlDefaultDbOwner);
        }

        public static string GetSqlForTableSchema(string sqlTableName, string sqlDbOwner)
        {
            return string.Format(Constants.SqlTableSchemaQuery, StringHelper.GetDefaultTableName(sqlTableName),
                                 StringHelper.GetDefaultSqlDbOwner(sqlDbOwner));
        }

        public static string GetSqlForForeignKeyRefTableName()
        {
            return GetSqlForForeignKeyRefTableName(Constants.SqlDefaultTableName, Constants.SqlDefaultColumnName);
        }

        public static string GetSqlForForeignKeyRefTableName(string sqlTableName, string sqlColumnName)
        {
            return string.Format(Constants.SqlForeignKeyRefTableName,
                                 StringHelper.GetDefaultTableName(sqlTableName),
                                 StringHelper.GetDefaultColumnName(sqlColumnName));
        }

        public static string GetSqlToFetchHasManyRelationships(string tableName)
        {
            return string.Format(Constants.SqlGetHasManyRelationships, tableName);
        }
    }
}