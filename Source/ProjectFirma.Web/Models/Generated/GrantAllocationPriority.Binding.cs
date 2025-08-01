//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[GrantAllocationPriority]
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
    // Table [dbo].[GrantAllocationPriority] is NOT multi-tenant, so is attributed as ICanDeleteFull
    [Table("[dbo].[GrantAllocationPriority]")]
    public partial class GrantAllocationPriority : IHavePrimaryKey, ICanDeleteFull
    {
        /// <summary>
        /// Default Constructor; only used by EF
        /// </summary>
        protected GrantAllocationPriority()
        {
            this.GrantAllocations = new HashSet<FundSourceAllocation>();
        }

        /// <summary>
        /// Constructor for building a new object with MaximalConstructor required fields in preparation for insert into database
        /// </summary>
        public GrantAllocationPriority(int grantAllocationPriorityID, int grantAllocationPriorityNumber, string grantAllocationPriorityColor) : this()
        {
            this.GrantAllocationPriorityID = grantAllocationPriorityID;
            this.GrantAllocationPriorityNumber = grantAllocationPriorityNumber;
            this.GrantAllocationPriorityColor = grantAllocationPriorityColor;
        }

        /// <summary>
        /// Constructor for building a new object with MinimalConstructor required fields in preparation for insert into database
        /// </summary>
        public GrantAllocationPriority(int grantAllocationPriorityNumber, string grantAllocationPriorityColor) : this()
        {
            // Mark this as a new object by setting primary key with special value
            this.GrantAllocationPriorityID = ModelObjectHelpers.MakeNextUnsavedPrimaryKeyValue();
            
            this.GrantAllocationPriorityNumber = grantAllocationPriorityNumber;
            this.GrantAllocationPriorityColor = grantAllocationPriorityColor;
        }


        /// <summary>
        /// Creates a "blank" object of this type and populates primitives with defaults
        /// </summary>
        public static GrantAllocationPriority CreateNewBlank()
        {
            return new GrantAllocationPriority(default(int), default(string));
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
        public static readonly List<string> DependentEntityTypeNames = new List<string> {typeof(GrantAllocationPriority).Name, typeof(FundSourceAllocation).Name};


        /// <summary>
        /// Delete just the entity 
        /// </summary>
        public void Delete(DatabaseEntities dbContext)
        {
            dbContext.GrantAllocationPriorities.Remove(this);
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
        public int GrantAllocationPriorityID { get; set; }
        public int GrantAllocationPriorityNumber { get; set; }
        public string GrantAllocationPriorityColor { get; set; }
        [NotMapped]
        public int PrimaryKey { get { return GrantAllocationPriorityID; } set { GrantAllocationPriorityID = value; } }

        public virtual ICollection<FundSourceAllocation> GrantAllocations { get; set; }

        public static class FieldLengths
        {
            public const int GrantAllocationPriorityColor = 8;
        }
    }
}