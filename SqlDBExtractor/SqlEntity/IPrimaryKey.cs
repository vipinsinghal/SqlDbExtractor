using System.Collections.Generic;
using SqlDBE.Core.Utility;

//Adapted from http://nmg.codeplex.com/
//This is only for reference purpose and not for commercial use
namespace SqlDBE.Core.SqlEntity
{
    public interface IPrimaryKey
    {
        PrimaryKeyType KeyType { get; }
        IList<Column> Columns { get; set; }
    }
}