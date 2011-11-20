using System.Collections.Generic;
using SqlDBE.Core.SqlEntity;

//Adapted from http://nmg.codeplex.com/
//This is only for reference purpose and not for commercial use
namespace SqlDBE.Core.SqlCore
{
    public interface ISqlDb
    {
        IList<SqlTable> GetSqlTableNames();
        IList<string> GetSqlDbOwner();
        IList<Column> GetSqlTableSchema();
        IList<Column> GetSqlTableSchema(SqlTable mySqlTable, string mySqlDbOwner);
    }
}