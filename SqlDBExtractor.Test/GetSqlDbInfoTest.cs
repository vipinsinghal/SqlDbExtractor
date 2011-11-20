using System.Collections.Generic;
using NUnit.Framework;
using SqlDBE.Core;
using SqlDBE.Core.SqlCore;
using SqlDBE.Core.SqlEntity;

namespace SqlDBE.Test
{
    [TestFixture]
    [Category("DatabaseCheck")]
    public class GetSqlDbInfoTest
    {
        private GetSqlDbInfo _getSqlDbInfo;
        private string _connectionString;

        [TestFixtureSetUp]
        private void Setup()
        {
            _connectionString = "Data Source=localhost;Initial Catalog=myDB;Integrated Security=SSPI;";
            _getSqlDbInfo = new GetSqlDbInfo(_connectionString);
        }

        [TestFixtureTearDown]
        private void TearDown()
        {
            _connectionString = null;
            _getSqlDbInfo = null;
        }

        [Test]
        public void CanGetSqlTableNames()
        {
            IList<SqlTable> sqlmyTableNames = _getSqlDbInfo.GetSqlTableNames("dbo");
            Assert.That(sqlmyTableNames.Count, Is.GreaterThan(0));
        }

        [Test]
        public void CanGetSqlTableSchema()
        {
            IList<Column> sqlmyTableSchema = _getSqlDbInfo.GetSqlTableSchema(new SqlTable {SqlTableName = "users"},
                                                                             "dbo");
            Assert.That(sqlmyTableSchema.Count, Is.GreaterThan(0));
        }

        [Test]
        public void CanParseSqlConnectionString()
        {
            IList<string> sqlDbOwner = _getSqlDbInfo.GetSqlDbOwner();
            Assert.That(sqlDbOwner.Count, Is.GreaterThan(0));
        }
    }
}