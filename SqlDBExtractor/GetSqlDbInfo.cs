using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using SqlDBE.Core.Connections;
using SqlDBE.Core.SqlCore;
using SqlDBE.Core.SqlEntity;
using SqlDBE.Core.SqlMetaData;
using SqlDBE.Core.Utility;

namespace SqlDBE.Core
{
    public class GetSqlDbInfo : ISqlDb
    {
        private readonly ISqlConnection _connectionString;


        public GetSqlDbInfo(string connString)
        {
            _connectionString = new SqlConnextion(connString);
            _connectionString.ConnString = _connectionString.GetSqlConnectionString();
            //_connectionString.ConnString = _connectionString.GetSqlConnectionString(connString);
        }

        #region Implementation of ISqlTable

        public IList<SqlTable> GetSqlTableNames()
        {
            return
                GetSqlTableNames(string.IsNullOrEmpty(Constants.SqlDbOwner)
                                     ? Constants.SqlDefaultDbOwner
                                     : Constants.SqlDbOwner);
        }

        public IList<string> GetSqlDbOwner()
        {
            var mySqlDbOwner = new List<string>();
            SqlConnection mySqlConn = _connectionString.GetSqlConnectionInstance(_connectionString.ConnString);
            mySqlConn.Open();
            using (mySqlConn)
            {
                SqlCommand mySqlComm = _connectionString.GetSqlCommandInstance(mySqlConn);
                mySqlComm.CommandText = SqlQuery.GetSqlForDatabaseOwner();
                SqlDataReader mySqlDataReader = _connectionString.GetSqlDataReaderInstance(mySqlComm,
                                                                                           CommandBehavior.
                                                                                               CloseConnection);
                while (mySqlDataReader != null && mySqlDataReader.Read())
                {
                    string mySqlDbOwnerName = mySqlDataReader.GetString(0);
                    mySqlDbOwner.Add(mySqlDbOwnerName);
                }
            }

            return mySqlDbOwner;
        }

        public IList<Column> GetSqlTableSchema()
        {
            return GetSqlTableSchema(new SqlTable(), Constants.SqlDbOwner);
        }

        public IList<Column> GetSqlTableSchema(SqlTable mySqlTable, string mySqlDbOwner)
        {
            var mySqlTableColumns = new List<Column>();
            SqlConnection mySqlConn = _connectionString.GetSqlConnectionInstance(_connectionString.ConnString);
            mySqlConn.Open();
            using (mySqlConn)
            {
                SqlCommand mySqlComm = _connectionString.GetSqlCommandInstance(mySqlConn);
                mySqlComm.CommandText = SqlQuery.GetSqlForTableSchema(mySqlTable.SqlTableName, mySqlDbOwner);
                SqlDataReader mySqlDataReader = _connectionString.GetSqlDataReaderInstance(mySqlComm,
                                                                                           CommandBehavior.
                                                                                               CloseConnection);
                while (mySqlDataReader != null && mySqlDataReader.Read())
                {
                    string columnName = mySqlDataReader.GetString(0);
                    string dataType = mySqlDataReader.GetString(1);
                    bool isNullable = mySqlDataReader.GetString(2).Equals("YES",
                                                                          StringComparison.CurrentCultureIgnoreCase);
                    var characterMaxLenth = mySqlDataReader["character_maximum_length"] as int?;
                    var numericPrecision = mySqlDataReader["numeric_precision"] as int?;
                    var numericScale = mySqlDataReader["numeric_scale"] as int?;

                    bool isPrimaryKey = (!mySqlDataReader.IsDBNull(3)
                                             ? mySqlDataReader.GetString(3).Equals(
                                                 SqlServerConstType.PrimaryKey.ToString(),
                                                 StringComparison.CurrentCultureIgnoreCase)
                                             : false);
                    bool isForeignKey = (!mySqlDataReader.IsDBNull(3)
                                             ? mySqlDataReader.GetString(3).Equals(
                                                 SqlServerConstType.ForeignKey.ToString(),
                                                 StringComparison.CurrentCultureIgnoreCase)
                                             : false);

                    var m = new SqlDataType();

                    mySqlTableColumns.Add(new Column
                                              {
                                                  Name = columnName,
                                                  DataType = dataType,
                                                  IsNullable = isNullable,
                                                  IsPrimaryKey = isPrimaryKey,
                                                  IsForeignKey = isForeignKey,
                                                  MappedDataType =
                                                      m.MapFromDbType(dataType, characterMaxLenth, numericPrecision,
                                                                      numericScale).ToString(),
                                                  DataLength = characterMaxLenth,
                                                  ConstraintName = mySqlDataReader["constraint_name"].ToString()
                                              });

                    mySqlTable.Columns = mySqlTableColumns;
                }
                mySqlTable.PrimaryKey = GetPrimaryKeys(mySqlTable);
                mySqlTable.ForeignKeys = GetForeignKeyReferences(mySqlTable);
                mySqlTable.HasManyRelationships = ChecknGetHasManyRelationships(mySqlTable);
            }
            return mySqlTableColumns;
        }

        public IList<SqlTable> GetSqlTableNames(string sqlDbOwner)
        {
            var sqlTableName = new List<SqlTable>();
            SqlConnection mySqlConn = _connectionString.GetSqlConnectionInstance(_connectionString.ConnString);
            mySqlConn.Open();
            using (mySqlConn)
            {
                SqlCommand mySqlComm = _connectionString.GetSqlCommandInstance(mySqlConn);
                mySqlComm.CommandText = SqlQuery.GetSqlForTableName(sqlDbOwner);
                SqlDataReader mySqlDataReader = _connectionString.GetSqlDataReaderInstance(mySqlComm,
                                                                                           CommandBehavior.
                                                                                               CloseConnection);
                while (mySqlDataReader != null && mySqlDataReader.Read())
                {
                    string mySqlTableName = mySqlDataReader.GetString(0);
                    sqlTableName.Add(new SqlTable {SqlTableName = mySqlTableName, SqlDbOwner = sqlDbOwner});
                }
            }

            return sqlTableName;
        }

        #endregion

        #region Private Methods

        private static PrimaryKey GetPrimaryKeys(SqlTable sqltable)
        {
            IEnumerable<Column> primaryKeys = sqltable.Columns.Where(x => x.IsPrimaryKey.Equals(true));

            switch (primaryKeys.Count())
            {
                case 1:
                    {
                        Column col = primaryKeys.First();
                        var pkey = new PrimaryKey
                                       {
                                           Type = PrimaryKeyType.PrimaryKey,
                                           Columns =
                                               {
                                                   new Column
                                                       {
                                                           DataType = col.DataType,
                                                           Name = col.Name
                                                       }
                                               }
                                       };
                        return pkey;
                    }
                default:
                    {
                        var pkey = new PrimaryKey {Type = PrimaryKeyType.CompositeKey};
                        foreach (Column primaryKey in primaryKeys)
                        {
                            pkey.Columns.Add(new Column
                                                 {
                                                     DataType = primaryKey.DataType,
                                                     Name = primaryKey.Name
                                                 });
                        }
                        return pkey;
                    }
            }
        }

        private IList<ForeignKey> GetForeignKeyReferences(SqlTable sqltable)
        {
            List<string> constraints =
                sqltable.Columns.Where(x => x.IsForeignKey.Equals(true)).Select(x => x.ConstraintName).Distinct().ToList
                    ();
            var foreignKeys = new List<ForeignKey>();
            constraints.ForEach(c =>
                                    {
                                        Column[] fkColumns =
                                            sqltable.Columns.Where(x => x.ConstraintName.Equals(c)).ToArray();
                                        var fk = new ForeignKey
                                                     {
                                                         Name = fkColumns[0].Name,
                                                         References =
                                                             GetForeignKeyReferenceTableName(sqltable.SqlTableName,
                                                                                             fkColumns[0].Name),
                                                         AllColumnsNamesForTheSameConstraint =
                                                             fkColumns.Select(f => f.Name).ToArray()
                                                     };
                                        foreignKeys.Add(fk);
                                    });

            SqlTable.SetUniqueNamesForForeignKeyProperties(foreignKeys);

            return foreignKeys;
        }

        private string GetForeignKeyReferenceTableName(string tableName, string colName)
        {
            object referencedTableName;

            SqlConnection mySqlConn = _connectionString.GetSqlConnectionInstance(_connectionString.ConnString);
            mySqlConn.Open();
            using (mySqlConn)
            {
                SqlCommand mySqlComm = _connectionString.GetSqlCommandInstance(mySqlConn);
                mySqlComm.CommandText = SqlQuery.GetSqlForForeignKeyRefTableName(tableName, colName);
                referencedTableName = mySqlComm.ExecuteScalar();
            }

            return Convert.ToString(referencedTableName);
        }

        // http://blog.sqlauthority.com/2006/11/01/sql-server-query-to-display-foreign-key-relationships-and-name-of-the-constraint-for-each-table-in-database/
        private IList<HasMany> ChecknGetHasManyRelationships(SqlTable sqlTable)
        {
            var hasManyRelationships = new List<HasMany>();
            SqlConnection mySqlConn = _connectionString.GetSqlConnectionInstance(_connectionString.ConnString);
            mySqlConn.Open();
            using (mySqlConn)
            {
                using (SqlCommand mySqlComm = _connectionString.GetSqlCommandInstance())
                {
                    mySqlComm.CommandText =
                        SqlQuery.GetSqlToFetchHasManyRelationships(sqlTable.SqlTableName);
                    mySqlComm.Connection = mySqlConn;
                    SqlDataReader mySqlDataReader = _connectionString.GetSqlDataReaderInstance(mySqlComm);

                    GetRefrences(hasManyRelationships, mySqlDataReader);
                }
            }
            return hasManyRelationships;
        }

        private static void GetRefrences(List<HasMany> hasManyRelationships, SqlDataReader mySqlDataReader)
        {
            while (mySqlDataReader != null && mySqlDataReader.Read())
            {
                string constraintName = mySqlDataReader["CONSTRAINT_NAME"].ToString();
                string fkColName = mySqlDataReader["FK_COLUMN_NAME"].ToString();
                HasMany existing =
                    hasManyRelationships.Where(hmr => hmr.ConstraintName == constraintName).FirstOrDefault();
                if (existing == null)
                {
                    var newHasManyItem = new HasMany
                                             {
                                                 ConstraintName = constraintName,
                                                 Reference = mySqlDataReader.GetString(1)
                                             };
                    newHasManyItem.AllReferenceColumns.Add(fkColName);
                    hasManyRelationships.Add(newHasManyItem);
                }
                else
                {
                    existing.AllReferenceColumns.Add(fkColName);
                }
            }
        }

        #endregion
    }
}