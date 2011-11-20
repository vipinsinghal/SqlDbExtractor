using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SqlDBE.Core.Utility;

namespace SqlDBE.Core.SqlEntity
{
    public class PrimaryKey
    {
        public PrimaryKey()
        {
            Columns = new List<Column>();
        }

        public PrimaryKeyType Type { get; set; }
        public IList<Column> Columns { get; set; }
        public bool IsGeneratedBySequence { get; set; } // Oracle only.
        public bool IsSelfReferencing { get; set; }
    }
}
