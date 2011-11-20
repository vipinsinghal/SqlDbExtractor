using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SqlDBE.Core.SqlEntity
{
    public class Column
    {
        public string Name { get; set; }
        public bool IsPrimaryKey { get; set; }
        public bool IsForeignKey { get; set; }
        public bool IsUnique { get; set; }
        public string DataType { get; set; }
        public int? DataLength { get; set; }
        public string MappedDataType { get; set; }
        public bool IsNullable { get; set; }
        public string ConstraintName { get; set; }
    }
}
