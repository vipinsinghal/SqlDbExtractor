//Adapted from http://nmg.codeplex.com/
//This is only for reference purpose and not for commercial use
namespace SqlDBE.Core.Utility
{
    public enum PrimaryKeyType
    {
        /// <summary>
        /// Primary key consisting of one column.
        /// </summary>
        PrimaryKey,

        /// <summary>
        /// Primary key consisting of two or more columns.
        /// </summary>
        CompositeKey,

        /// <summary>
        /// Default primary key type.
        /// </summary>
        Default = PrimaryKey
    }
}