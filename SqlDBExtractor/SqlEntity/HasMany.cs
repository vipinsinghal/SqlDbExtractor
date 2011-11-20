using System.Collections.Generic;

//Adapted from http://nmg.codeplex.com/
//This is only for reference purpose and not for commercial use
namespace SqlDBE.Core.SqlEntity
{
    public class HasMany
    {
        public HasMany()
        {
            AllReferenceColumns = new List<string>();
        }

        /// <summary>
        /// An identifier for a constraint so that we might detect from querying the database whether a relationship has one is a composite key.
        /// </summary>
        public string ConstraintName { get; set; }

        public string Reference { get; set; }

        /// <summary>
        /// In support of relationships that use composite keys.
        /// </summary>
        public IList<string> AllReferenceColumns { get; set; }

        /// <summary>
        /// Provide the first (and very often the only) column used to define a foreign key relationship.
        /// </summary>
        public string ReferenceColumn
        {
            get { return AllReferenceColumns.Count > 0 ? AllReferenceColumns[0] : ""; }
            set { AllReferenceColumns = new List<string> {value}; }
        }
    }
}