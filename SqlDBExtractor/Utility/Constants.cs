namespace SqlDBE.Core.Utility
{
    public class Constants
    {
        #region Members

        public static string SqlDbOwner { get; set; }
        public static string SqlTableName { get; set; }
        public static string SqlConnString { get; set; }

        #endregion

        #region SQL Meta Data Queries

        internal static string SqlTableNameQuery =
            "select table_name from information_schema.tables where table_type like 'BASE TABLE' and TABLE_SCHEMA = '{0}'";

        internal static string SqlDbOwnerQuery = "select schema_name from information_schema.schemata";

        internal static string SqlTableSchemaQuery =
            @"SELECT distinct c.column_name, c.data_type, c.is_nullable, tc.constraint_type, c.numeric_precision, c.numeric_scale, c.character_maximum_length, c.table_name, c.ordinal_position, tc.constraint_name
                        from information_schema.columns c
                            left outer join (
                                information_schema.constraint_column_usage ccu
                                join information_schema.table_constraints tc on (
                                    tc.table_schema = ccu.table_schema
                                    and tc.constraint_name = ccu.constraint_name
                                    and tc.constraint_type <> 'CHECK'
                                )
                            ) on (
                                c.table_schema = ccu.table_schema and ccu.table_schema = '{1}'
                                and c.table_name = ccu.table_name
                                and c.column_name = ccu.column_name
                            )
                        where c.table_name = '{0}'
                        order by c.table_name, c.ordinal_position";

        internal static string SqlForeignKeyRefTableName =
            @"select pk_table = pk.table_name
                        from information_schema.referential_constraints c
                        inner join information_schema.table_constraints fk on c.constraint_name = fk.constraint_name
                        inner join information_schema.table_constraints pk on c.unique_constraint_name = pk.constraint_name
                        inner join information_schema.key_column_usage cu on c.constraint_name = cu.constraint_name
                        inner join (
                        select i1.table_name, i2.column_name
                        from information_schema.table_constraints i1
                        inner join information_schema.key_column_usage i2 on i1.constraint_name = i2.constraint_name
                        where i1.constraint_type = 'PRIMARY KEY'
                        ) pt on pt.table_name = pk.table_name
                        where fk.table_name = '{0}' and cu.column_name = '{1}'";

        internal static string SqlGetHasManyRelationships =
            @"SELECT DISTINCT 
                            PK_TABLE = b.TABLE_NAME,
                            FK_TABLE = c.TABLE_NAME,
                            FK_COLUMN_NAME = d.COLUMN_NAME,
                            CONSTRAINT_NAME = a.CONSTRAINT_NAME
                        FROM INFORMATION_SCHEMA.REFERENTIAL_CONSTRAINTS a 
                          JOIN INFORMATION_SCHEMA.TABLE_CONSTRAINTS b ON a.CONSTRAINT_SCHEMA = b.CONSTRAINT_SCHEMA AND a.UNIQUE_CONSTRAINT_NAME = b.CONSTRAINT_NAME 
                          JOIN INFORMATION_SCHEMA.TABLE_CONSTRAINTS c ON a.CONSTRAINT_SCHEMA = c.CONSTRAINT_SCHEMA AND a.CONSTRAINT_NAME = c.CONSTRAINT_NAME
                          JOIN INFORMATION_SCHEMA.CONSTRAINT_COLUMN_USAGE d on a.CONSTRAINT_NAME = d.CONSTRAINT_NAME
                        WHERE b.TABLE_NAME = '{0}'
                        ORDER BY 1,2";

        public static string SqlDbConnectionString = StringHelper.GetDefaultConnectionString(SqlConnString);

        #endregion

        #region SqlTableDefaultConstants

        internal static string SqlDefaultTableName = "myTable";
        internal static string SqlDefaultDbOwner = "dbo";
        internal static string SqlDefaultColumnName = "Id";

        #endregion

        #region Connection strings

        public static string SqlDefaultConnectionString =
            "Data Source=localhost;Initial Catalog=myDB;Integrated Security=SSPI;";

        #endregion
    }
}