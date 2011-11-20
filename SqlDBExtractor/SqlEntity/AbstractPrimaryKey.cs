using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SqlDBE.Core.Utility;

namespace SqlDBE.Core.SqlEntity
{
    public abstract class AbstractPrimaryKey : IPrimaryKey
    {
        protected AbstractPrimaryKey()
        {
            Columns = new List<Column>();
        }

        #region IPrimaryKey Members

        public abstract PrimaryKeyType KeyType { get; }
        public IList<Column> Columns { get; set; }

        #endregion
    }
}
