//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[GrantAllocation]
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;
using System.Data.Entity.Spatial;
using System.Linq;
using System.Web;
using LtInfo.Common.DesignByContract;
using LtInfo.Common.Models;
using ProjectFirma.Web.Common;

namespace ProjectFirma.Web.Models
{
    // Table [dbo].[GrantAllocation] is NOT multi-tenant, so is attributed as ICanDeleteFull
    [Table("[dbo].[GrantAllocation]")]
    public partial class GrantAllocation : IHavePrimaryKey, ICanDeleteFull
    {
        /// <summary>
        /// Default Constructor; only used by EF
        /// </summary>
        protected GrantAllocation()
        {
            this.GrantAllocationProjectCodes = new HashSet<GrantAllocationProjectCode>();
        }

        /// <summary>
        /// Constructor for building a new object with MaximalConstructor required fields in preparation for insert into database
        /// </summary>
        public GrantAllocation(int grantAllocationID, int grantID, string projectName, DateTime startDate, DateTime endDate, decimal? allocationAmount, int? costTypeID, int? programManagerPersonID, int? programIndexID) : this()
        {
            this.GrantAllocationID = grantAllocationID;
            this.GrantID = grantID;
            this.ProjectName = projectName;
            this.StartDate = startDate;
            this.EndDate = endDate;
            this.AllocationAmount = allocationAmount;
            this.CostTypeID = costTypeID;
            this.ProgramManagerPersonID = programManagerPersonID;
            this.ProgramIndexID = programIndexID;
        }

        /// <summary>
        /// Constructor for building a new object with MinimalConstructor required fields in preparation for insert into database
        /// </summary>
        public GrantAllocation(int grantID, DateTime startDate, DateTime endDate) : this()
        {
            // Mark this as a new object by setting primary key with special value
            this.GrantAllocationID = ModelObjectHelpers.MakeNextUnsavedPrimaryKeyValue();
            
            this.GrantID = grantID;
            this.StartDate = startDate;
            this.EndDate = endDate;
        }

        /// <summary>
        /// Constructor for building a new object with MinimalConstructor required fields, using objects whenever possible
        /// </summary>
        public GrantAllocation(Grant grant, DateTime startDate, DateTime endDate) : this()
        {
            // Mark this as a new object by setting primary key with special value
            this.GrantAllocationID = ModelObjectHelpers.MakeNextUnsavedPrimaryKeyValue();
            this.GrantID = grant.GrantID;
            this.Grant = grant;
            grant.GrantAllocations.Add(this);
            this.StartDate = startDate;
            this.EndDate = endDate;
        }

        /// <summary>
        /// Creates a "blank" object of this type and populates primitives with defaults
        /// </summary>
        public static GrantAllocation CreateNewBlank(Grant grant)
        {
            return new GrantAllocation(grant, default(DateTime), default(DateTime));
        }

        /// <summary>
        /// Does this object have any dependent objects? (If it does have dependent objects, these would need to be deleted before this object could be deleted.)
        /// </summary>
        /// <returns></returns>
        public bool HasDependentObjects()
        {
            return GrantAllocationProjectCodes.Any();
        }

        /// <summary>
        /// Dependent type names of this entity
        /// </summary>
        public static readonly List<string> DependentEntityTypeNames = new List<string> {typeof(GrantAllocation).Name, typeof(GrantAllocationProjectCode).Name};


        /// <summary>
        /// Dependent type names of this entity
        /// </summary>
        public void DeleteFull(DatabaseEntities dbContext)
        {
            DeleteChildren(dbContext);
            dbContext.GrantAllocations.Remove(this);
        }
        /// <summary>
        /// Dependent type names of this entity
        /// </summary>
        public void DeleteChildren(DatabaseEntities dbContext)
        {

            foreach(var x in GrantAllocationProjectCodes.ToList())
            {
                x.DeleteFull(dbContext);
            }
        }

        [Key]
        public int GrantAllocationID { get; set; }
        public int GrantID { get; set; }
        public string ProjectName { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public decimal? AllocationAmount { get; set; }
        public int? CostTypeID { get; set; }
        public int? ProgramManagerPersonID { get; set; }
        public int? ProgramIndexID { get; set; }
        [NotMapped]
        public int PrimaryKey { get { return GrantAllocationID; } set { GrantAllocationID = value; } }

        public virtual ICollection<GrantAllocationProjectCode> GrantAllocationProjectCodes { get; set; }
        public virtual Grant Grant { get; set; }
        public virtual CostType CostType { get; set; }
        public virtual Person ProgramManagerPerson { get; set; }
        public virtual ProgramIndex ProgramIndex { get; set; }

        public static class FieldLengths
        {
            public const int ProjectName = 100;
        }
    }
}