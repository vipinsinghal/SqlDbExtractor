using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SqlDBE.Core.SqlCore
{
    public sealed class SqlServerConstType
    {

        public static readonly SqlServerConstType PrimaryKey = new SqlServerConstType(1, "PRIMARY KEY");
        public static readonly SqlServerConstType ForeignKey = new SqlServerConstType(2, "FOREIGN KEY");
        public static readonly SqlServerConstType Check = new SqlServerConstType(3, "CHECK");
        public static readonly SqlServerConstType Unique = new SqlServerConstType(4, "UNIQUE");

        private readonly String _name;
        private readonly int _value;

        private SqlServerConstType(int value, String name)
        {
            _name = name;
            _value = value;
        }

        public override String ToString()
        {
            return _name;
        }

    }
}
