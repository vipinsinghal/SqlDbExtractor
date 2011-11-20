using System;
using System.Collections.Generic;
using System.Linq;
using SqlDBE.Core.SqlEntity;

namespace SqlDBE.Core.SqlCore
{
    public class SqlTable
    {
        public SqlTable()
        {
            ForeignKeys = new List<ForeignKey>();
            Columns = new List<Column>();
            HasManyRelationships = new List<HasMany>();
        }

        public string SqlTableName { get; set; }
        public string SqlDbOwner { get; set; }
        public PrimaryKey PrimaryKey { get; set; }

        public IList<ForeignKey> ForeignKeys { get; set; }
        public IList<Column> Columns { get; set; }
        public IList<HasMany> HasManyRelationships { get; set; }


        public override string ToString()
        {
            return SqlTableName;
        }
        //Adapted from http://nmg.codeplex.com/
        //This is only for reference purpose and not for commercial use
        public string ForeignKeyReferenceForColumn(Column column)
        {
            if (ForeignKeys != null)
            {
                ForeignKey fKey = ForeignKeys.Where(fk => fk.Name == column.Name).FirstOrDefault();
                if (fKey != null) return fKey.References;
            }
            return String.Format("/* TODO: UNKNOWN FOREIGN ENTITY for column {0} */", column.Name);
        }

        public static void SetUniqueNamesForForeignKeyProperties(IEnumerable<ForeignKey> foreignKeys)
        {
            IEnumerable<string> refsUsedMoreThanOnce = foreignKeys.Select(f => f.References).Distinct()
                .GroupJoin(foreignKeys, a => a, b => b.References, (a, b) => new { References = a, Count = b.Count() })
                .Where(@t => t.Count > 1)
                .Select(@t => t.References);

            foreignKeys.Join(refsUsedMoreThanOnce, a => a.References, b => b, (a, b) => a).ToList()
                .ForEach(fk => { fk.UniquePropertyName = fk.Name + "_" + fk.References; });
        }
    }
}