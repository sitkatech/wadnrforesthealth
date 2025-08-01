//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[GrantAllocationSource]
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
    // Table [dbo].[GrantAllocationSource] is NOT multi-tenant, so is attributed as ICanDeleteFull
    [Table("[dbo].[GrantAllocationSource]")]
    public partial class GrantAllocationSource : IHavePrimaryKey, ICanDeleteFull
    {
        /// <summary>
        /// Default Constructor; only used by EF
        /// </summary>
        protected GrantAllocationSource()
        {
            this.GrantAllocations = new HashSet<FundSourceAllocation>();
        }

        /// <summary>
        /// Constructor for building a new object with MaximalConstructor required fields in preparation for insert into database
        /// </summary>
        public GrantAllocationSource(int grantAllocationSourceID, string grantAllocationSourceName, string grantAllocationSourceDisplayName, int sortOrder) : this()
        {
            this.GrantAllocationSourceID = grantAllocationSourceID;
            this.GrantAllocationSourceName = grantAllocationSourceName;
            this.GrantAllocationSourceDisplayName = grantAllocationSourceDisplayName;
            this.SortOrder = sortOrder;
        }

        /// <summary>
        /// Constructor for building a new object with MinimalConstructor required fields in preparation for insert into database
        /// </summary>
        public GrantAllocationSource(string grantAllocationSourceName, string grantAllocationSourceDisplayName, int sortOrder) : this()
        {
            // Mark this as a new object by setting primary key with special value
            this.GrantAllocationSourceID = ModelObjectHelpers.MakeNextUnsavedPrimaryKeyValue();
            
            this.GrantAllocationSourceName = grantAllocationSourceName;
            this.GrantAllocationSourceDisplayName = grantAllocationSourceDisplayName;
            this.SortOrder = sortOrder;
        }


        /// <summary>
        /// Creates a "blank" object of this type and populates primitives with defaults
        /// </summary>
        public static GrantAllocationSource CreateNewBlank()
        {
            return new GrantAllocationSource(default(string), default(string), default(int));
        }

        /// <summary>
        /// Does this object have any dependent objects? (If it does have dependent objects, these would need to be deleted before this object could be deleted.)
        /// </summary>
        /// <returns></returns>
        public bool HasDependentObjects()
        {
            return GrantAllocations.Any();
        }

        /// <summary>
        /// Active Dependent type names of this object
        /// </summary>
        public List<string> DependentObjectNames() 
        {
            var dependentObjects = new List<string>();
            
            if(GrantAllocations.Any())
            {
                dependentObjects.Add(typeof(FundSourceAllocation).Name);
            }
            return dependentObjects.Distinct().ToList();
        }

        /// <summary>
        /// Dependent type names of this entity
        /// </summary>
        public static readonly List<string> DependentEntityTypeNames = new List<string> {typeof(GrantAllocationSource).Name, typeof(FundSourceAllocation).Name};


        /// <summary>
        /// Delete just the entity 
        /// </summary>
        public void Delete(DatabaseEntities dbContext)
        {
            dbContext.GrantAllocationSources.Remove(this);
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

            foreach(var x in GrantAllocations.ToList())
            {
                x.DeleteFull(dbContext);
            }
        }

        [Key]
        public int GrantAllocationSourceID { get; set; }
        public string GrantAllocationSourceName { get; set; }
        public string GrantAllocationSourceDisplayName { get; set; }
        public int SortOrder { get; set; }
        [NotMapped]
        public int PrimaryKey { get { return GrantAllocationSourceID; } set { GrantAllocationSourceID = value; } }

        public virtual ICollection<FundSourceAllocation> GrantAllocations { get; set; }

        public static class FieldLengths
        {
            public const int GrantAllocationSourceName = 200;
            public const int GrantAllocationSourceDisplayName = 200;
        }
    }
}