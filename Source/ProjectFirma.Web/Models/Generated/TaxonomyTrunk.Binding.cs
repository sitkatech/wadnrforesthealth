//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[TaxonomyTrunk]
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;
using System.Data.Entity.Spatial;
using System.Linq;
using System.Web;
using CodeFirstStoreFunctions;
using LtInfo.Common.DesignByContract;
using LtInfo.Common.Models;
using ProjectFirma.Web.Common;

namespace ProjectFirma.Web.Models
{
    // Table [dbo].[TaxonomyTrunk] is NOT multi-tenant, so is attributed as ICanDeleteFull
    [Table("[dbo].[TaxonomyTrunk]")]
    public partial class TaxonomyTrunk : IHavePrimaryKey, ICanDeleteFull
    {
        /// <summary>
        /// Default Constructor; only used by EF
        /// </summary>
        protected TaxonomyTrunk()
        {
            this.TaxonomyBranches = new HashSet<TaxonomyBranch>();
        }

        /// <summary>
        /// Constructor for building a new object with MaximalConstructor required fields in preparation for insert into database
        /// </summary>
        public TaxonomyTrunk(int taxonomyTrunkID, string taxonomyTrunkName, string taxonomyTrunkDescription, string themeColor, string taxonomyTrunkCode, int? taxonomyTrunkSortOrder) : this()
        {
            this.TaxonomyTrunkID = taxonomyTrunkID;
            this.TaxonomyTrunkName = taxonomyTrunkName;
            this.TaxonomyTrunkDescription = taxonomyTrunkDescription;
            this.ThemeColor = themeColor;
            this.TaxonomyTrunkCode = taxonomyTrunkCode;
            this.TaxonomyTrunkSortOrder = taxonomyTrunkSortOrder;
        }

        /// <summary>
        /// Constructor for building a new object with MinimalConstructor required fields in preparation for insert into database
        /// </summary>
        public TaxonomyTrunk(string taxonomyTrunkName) : this()
        {
            // Mark this as a new object by setting primary key with special value
            this.TaxonomyTrunkID = ModelObjectHelpers.MakeNextUnsavedPrimaryKeyValue();
            
            this.TaxonomyTrunkName = taxonomyTrunkName;
        }


        /// <summary>
        /// Creates a "blank" object of this type and populates primitives with defaults
        /// </summary>
        public static TaxonomyTrunk CreateNewBlank()
        {
            return new TaxonomyTrunk(default(string));
        }

        /// <summary>
        /// Does this object have any dependent objects? (If it does have dependent objects, these would need to be deleted before this object could be deleted.)
        /// </summary>
        /// <returns></returns>
        public bool HasDependentObjects()
        {
            return TaxonomyBranches.Any();
        }

        /// <summary>
        /// Active Dependent type names of this object
        /// </summary>
        public List<string> DependentObjectNames() 
        {
            var dependentObjects = new List<string>();
            
            if(TaxonomyBranches.Any())
            {
                dependentObjects.Add(typeof(TaxonomyBranch).Name);
            }
            return dependentObjects.Distinct().ToList();
        }

        /// <summary>
        /// Dependent type names of this entity
        /// </summary>
        public static readonly List<string> DependentEntityTypeNames = new List<string> {typeof(TaxonomyTrunk).Name, typeof(TaxonomyBranch).Name};


        /// <summary>
        /// Delete just the entity 
        /// </summary>
        public void Delete(DatabaseEntities dbContext)
        {
            dbContext.TaxonomyTrunks.Remove(this);
        }
        
        /// <summary>
        /// Delete entity plus all children
        /// </summary>
        public void DeleteFull(DatabaseEntities dbContext)
        {
            DeleteChildren(dbContext);
            Delete(dbContext);
        }
        /// <summary>
        /// Dependent type names of this entity
        /// </summary>
        public void DeleteChildren(DatabaseEntities dbContext)
        {

            foreach(var x in TaxonomyBranches.ToList())
            {
                x.DeleteFull(dbContext);
            }
        }

        [Key]
        public int TaxonomyTrunkID { get; set; }
        public string TaxonomyTrunkName { get; set; }
        public string TaxonomyTrunkDescription { get; set; }
        [NotMapped]
        public HtmlString TaxonomyTrunkDescriptionHtmlString
        { 
            get { return TaxonomyTrunkDescription == null ? null : new HtmlString(TaxonomyTrunkDescription); }
            set { TaxonomyTrunkDescription = value?.ToString(); }
        }
        public string ThemeColor { get; set; }
        public string TaxonomyTrunkCode { get; set; }
        public int? TaxonomyTrunkSortOrder { get; set; }
        [NotMapped]
        public int PrimaryKey { get { return TaxonomyTrunkID; } set { TaxonomyTrunkID = value; } }

        public virtual ICollection<TaxonomyBranch> TaxonomyBranches { get; set; }

        public static class FieldLengths
        {
            public const int TaxonomyTrunkName = 100;
            public const int ThemeColor = 20;
            public const int TaxonomyTrunkCode = 10;
        }
    }
}