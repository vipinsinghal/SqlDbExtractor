using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SqlDBE.Core.Utility;

namespace SqlDBE.Core.SqlEntity
{
    public class CompositeKey : AbstractPrimaryKey
    {
        public override PrimaryKeyType KeyType
        {
            get { return PrimaryKeyType.CompositeKey; }
        }
    }
}
