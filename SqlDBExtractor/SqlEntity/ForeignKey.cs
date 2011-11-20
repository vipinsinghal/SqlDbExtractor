//Adapted from http://nmg.codeplex.com/
//This is only for reference purpose and not for commercial use
namespace SqlDBE.Core.SqlEntity
{
    public class ForeignKey
    {
        private string _uniquePropertyName;

        /// <summary>
        /// Foreign key column name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Defines what table the foreign key references.
        /// </summary>
        public string References { get; set; }

        /// <summary>
        /// When one table has multiple fields that represent different relationships to the same foreign entity, it is required to give them unique names.
        /// </summary>
        public string UniquePropertyName
        {
            get { return string.IsNullOrEmpty(_uniquePropertyName) ? References : _uniquePropertyName; }
            set { _uniquePropertyName = value; }
        }

        /// <summary>
        /// A foreign key may be one of multiple columns of a composite key to a foreign entity
        /// </summary>
        public string[] AllColumnsNamesForTheSameConstraint { get; set; }

        public override string ToString()
        {
            return Name;
        }
    }
}