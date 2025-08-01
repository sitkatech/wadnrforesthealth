//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[GrantAllocationFileResource]
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
    // Table [dbo].[GrantAllocationFileResource] is NOT multi-tenant, so is attributed as ICanDeleteFull
    [Table("[dbo].[GrantAllocationFileResource]")]
    public partial class GrantAllocationFileResource : IHavePrimaryKey, ICanDeleteFull
    {
        /// <summary>
        /// Default Constructor; only used by EF
        /// </summary>
        protected GrantAllocationFileResource()
        {

        }

        /// <summary>
        /// Constructor for building a new object with MaximalConstructor required fields in preparation for insert into database
        /// </summary>
        public GrantAllocationFileResource(int grantAllocationFileResourceID, int grantAllocationID, int fileResourceID, string displayName, string description) : this()
        {
            this.GrantAllocationFileResourceID = grantAllocationFileResourceID;
            this.GrantAllocationID = grantAllocationID;
            this.FileResourceID = fileResourceID;
            this.DisplayName = displayName;
            this.Description = description;
        }

        /// <summary>
        /// Constructor for building a new object with MinimalConstructor required fields in preparation for insert into database
        /// </summary>
        public GrantAllocationFileResource(int grantAllocationID, int fileResourceID, string displayName) : this()
        {
            // Mark this as a new object by setting primary key with special value
            this.GrantAllocationFileResourceID = ModelObjectHelpers.MakeNextUnsavedPrimaryKeyValue();
            
            this.GrantAllocationID = grantAllocationID;
            this.FileResourceID = fileResourceID;
            this.DisplayName = displayName;
        }

        /// <summary>
        /// Constructor for building a new object with MinimalConstructor required fields, using objects whenever possible
        /// </summary>
        public GrantAllocationFileResource(FundSourceAllocation fundSourceAllocation, FileResource fileResource, string displayName) : this()
        {
            // Mark this as a new object by setting primary key with special value
            this.GrantAllocationFileResourceID = ModelObjectHelpers.MakeNextUnsavedPrimaryKeyValue();
            this.GrantAllocationID = fundSourceAllocation.GrantAllocationID;
            this.FundSourceAllocation = fundSourceAllocation;
            fundSourceAllocation.GrantAllocationFileResources.Add(this);
            this.FileResourceID = fileResource.FileResourceID;
            this.FileResource = fileResource;
            fileResource.GrantAllocationFileResources.Add(this);
            this.DisplayName = displayName;
        }

        /// <summary>
        /// Creates a "blank" object of this type and populates primitives with defaults
        /// </summary>
        public static GrantAllocationFileResource CreateNewBlank(FundSourceAllocation fundSourceAllocation, FileResource fileResource)
        {
            return new GrantAllocationFileResource(fundSourceAllocation, fileResource, default(string));
        }

        /// <summary>
        /// Does this object have any dependent objects? (If it does have dependent objects, these would need to be deleted before this object could be deleted.)
        /// </summary>
        /// <returns></returns>
        public bool HasDependentObjects()
        {
            return false;
        }

        /// <summary>
        /// Active Dependent type names of this object
        /// </summary>
        public List<string> DependentObjectNames() 
        {
            var dependentObjects = new List<string>();
            
            return dependentObjects.Distinct().ToList();
        }

        /// <summary>
        /// Dependent type names of this entity
        /// </summary>
        public static readonly List<string> DependentEntityTypeNames = new List<string> {typeof(GrantAllocationFileResource).Name};


        /// <summary>
        /// Delete just the entity 
        /// </summary>
        public void Delete(DatabaseEntities dbContext)
        {
            dbContext.GrantAllocationFileResources.Remove(this);
        }
        
        /// <summary>
        /// Delete entity plus all children
        /// </summary>
        public void DeleteFull(DatabaseEntities dbContext)
        {
            
            Delete(dbContext);
        }

        [Key]
        public int GrantAllocationFileResourceID { get; set; }
        public int GrantAllocationID { get; set; }
        public int FileResourceID { get; set; }
        public string DisplayName { get; set; }
        public string Description { get; set; }
        [NotMapped]
        public int PrimaryKey { get { return GrantAllocationFileResourceID; } set { GrantAllocationFileResourceID = value; } }

        public virtual FundSourceAllocation FundSourceAllocation { get; set; }
        public virtual FileResource FileResource { get; set; }

        public static class FieldLengths
        {
            public const int DisplayName = 200;
            public const int Description = 1000;
        }
    }
}