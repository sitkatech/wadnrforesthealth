//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[TaxonomyBranch]
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
    // Table [dbo].[TaxonomyBranch] is NOT multi-tenant, so is attributed as ICanDeleteFull
    [Table("[dbo].[TaxonomyBranch]")]
    public partial class TaxonomyBranch : IHavePrimaryKey, ICanDeleteFull
    {
        /// <summary>
        /// Default Constructor; only used by EF
        /// </summary>
        protected TaxonomyBranch()
        {
            this.PersonStewardTaxonomyBranches = new HashSet<PersonStewardTaxonomyBranch>();
            this.ProjectTypes = new HashSet<ProjectType>();
        }

        /// <summary>
        /// Constructor for building a new object with MaximalConstructor required fields in preparation for insert into database
        /// </summary>
        public TaxonomyBranch(int taxonomyBranchID, int taxonomyTrunkID, string taxonomyBranchName, string taxonomyBranchDescription, string themeColor, string taxonomyBranchCode, int? taxonomyBranchSortOrder) : this()
        {
            this.TaxonomyBranchID = taxonomyBranchID;
            this.TaxonomyTrunkID = taxonomyTrunkID;
            this.TaxonomyBranchName = taxonomyBranchName;
            this.TaxonomyBranchDescription = taxonomyBranchDescription;
            this.ThemeColor = themeColor;
            this.TaxonomyBranchCode = taxonomyBranchCode;
            this.TaxonomyBranchSortOrder = taxonomyBranchSortOrder;
        }

        /// <summary>
        /// Constructor for building a new object with MinimalConstructor required fields in preparation for insert into database
        /// </summary>
        public TaxonomyBranch(int taxonomyTrunkID, string taxonomyBranchName) : this()
        {
            // Mark this as a new object by setting primary key with special value
            this.TaxonomyBranchID = ModelObjectHelpers.MakeNextUnsavedPrimaryKeyValue();
            
            this.TaxonomyTrunkID = taxonomyTrunkID;
            this.TaxonomyBranchName = taxonomyBranchName;
        }

        /// <summary>
        /// Constructor for building a new object with MinimalConstructor required fields, using objects whenever possible
        /// </summary>
        public TaxonomyBranch(TaxonomyTrunk taxonomyTrunk, string taxonomyBranchName) : this()
        {
            // Mark this as a new object by setting primary key with special value
            this.TaxonomyBranchID = ModelObjectHelpers.MakeNextUnsavedPrimaryKeyValue();
            this.TaxonomyTrunkID = taxonomyTrunk.TaxonomyTrunkID;
            this.TaxonomyTrunk = taxonomyTrunk;
            taxonomyTrunk.TaxonomyBranches.Add(this);
            this.TaxonomyBranchName = taxonomyBranchName;
        }

        /// <summary>
        /// Creates a "blank" object of this type and populates primitives with defaults
        /// </summary>
        public static TaxonomyBranch CreateNewBlank(TaxonomyTrunk taxonomyTrunk)
        {
            return new TaxonomyBranch(taxonomyTrunk, default(string));
        }

        /// <summary>
        /// Does this object have any dependent objects? (If it does have dependent objects, these would need to be deleted before this object could be deleted.)
        /// </summary>
        /// <returns></returns>
        public bool HasDependentObjects()
        {
            return PersonStewardTaxonomyBranches.Any() || ProjectTypes.Any();
        }

        /// <summary>
        /// Active Dependent type names of this object
        /// </summary>
        public List<string> DependentObjectNames() 
        {
            var dependentObjects = new List<string>();
            
            if(PersonStewardTaxonomyBranches.Any())
            {
                dependentObjects.Add(typeof(PersonStewardTaxonomyBranch).Name);
            }

            if(ProjectTypes.Any())
            {
                dependentObjects.Add(typeof(ProjectType).Name);
            }
            return dependentObjects.Distinct().ToList();
        }

        /// <summary>
        /// Dependent type names of this entity
        /// </summary>
        public static readonly List<string> DependentEntityTypeNames = new List<string> {typeof(TaxonomyBranch).Name, typeof(PersonStewardTaxonomyBranch).Name, typeof(ProjectType).Name};


        /// <summary>
        /// Delete just the entity 
        /// </summary>
        public void Delete(DatabaseEntities dbContext)
        {
            dbContext.TaxonomyBranches.Remove(this);
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

            foreach(var x in PersonStewardTaxonomyBranches.ToList())
            {
                x.DeleteFull(dbContext);
            }

            foreach(var x in ProjectTypes.ToList())
            {
                x.DeleteFull(dbContext);
            }
        }

        [Key]
        public int TaxonomyBranchID { get; set; }
        public int TaxonomyTrunkID { get; set; }
        public string TaxonomyBranchName { get; set; }
        public string TaxonomyBranchDescription { get; set; }
        [NotMapped]
        public HtmlString TaxonomyBranchDescriptionHtmlString
        { 
            get { return TaxonomyBranchDescription == null ? null : new HtmlString(TaxonomyBranchDescription); }
            set { TaxonomyBranchDescription = value?.ToString(); }
        }
        public string ThemeColor { get; set; }
        public string TaxonomyBranchCode { get; set; }
        public int? TaxonomyBranchSortOrder { get; set; }
        [NotMapped]
        public int PrimaryKey { get { return TaxonomyBranchID; } set { TaxonomyBranchID = value; } }

        public virtual ICollection<PersonStewardTaxonomyBranch> PersonStewardTaxonomyBranches { get; set; }
        public virtual ICollection<ProjectType> ProjectTypes { get; set; }
        public virtual TaxonomyTrunk TaxonomyTrunk { get; set; }

        public static class FieldLengths
        {
            public const int TaxonomyBranchName = 100;
            public const int ThemeColor = 7;
            public const int TaxonomyBranchCode = 10;
        }
    }
}