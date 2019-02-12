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
            this.AgreementGrantAllocations = new HashSet<AgreementGrantAllocation>();
            this.GrantAllocationNotes = new HashSet<GrantAllocationNote>();
            this.GrantAllocationProjectCodes = new HashSet<GrantAllocationProjectCode>();
        }

        /// <summary>
        /// Constructor for building a new object with MaximalConstructor required fields in preparation for insert into database
        /// </summary>
        public GrantAllocation(int grantAllocationID, int grantID, string projectName, DateTime? startDate, DateTime? endDate, decimal? allocationAmount, int? costTypeID, int? programManagerPersonID, int? programIndexID, int? federalFundCodeID, int? organizationID, int? regionID) : this()
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
            this.FederalFundCodeID = federalFundCodeID;
            this.OrganizationID = organizationID;
            this.RegionID = regionID;
        }

        /// <summary>
        /// Constructor for building a new object with MinimalConstructor required fields in preparation for insert into database
        /// </summary>
        public GrantAllocation(int grantID) : this()
        {
            // Mark this as a new object by setting primary key with special value
            this.GrantAllocationID = ModelObjectHelpers.MakeNextUnsavedPrimaryKeyValue();
            
            this.GrantID = grantID;
        }

        /// <summary>
        /// Constructor for building a new object with MinimalConstructor required fields, using objects whenever possible
        /// </summary>
        public GrantAllocation(Grant grant) : this()
        {
            // Mark this as a new object by setting primary key with special value
            this.GrantAllocationID = ModelObjectHelpers.MakeNextUnsavedPrimaryKeyValue();
            this.GrantID = grant.GrantID;
            this.Grant = grant;
            grant.GrantAllocations.Add(this);
        }

        /// <summary>
        /// Creates a "blank" object of this type and populates primitives with defaults
        /// </summary>
        public static GrantAllocation CreateNewBlank(Grant grant)
        {
            return new GrantAllocation(grant);
        }

        /// <summary>
        /// Does this object have any dependent objects? (If it does have dependent objects, these would need to be deleted before this object could be deleted.)
        /// </summary>
        /// <returns></returns>
        public bool HasDependentObjects()
        {
            return AgreementGrantAllocations.Any() || GrantAllocationNotes.Any() || GrantAllocationProjectCodes.Any();
        }

        /// <summary>
        /// Dependent type names of this entity
        /// </summary>
        public static readonly List<string> DependentEntityTypeNames = new List<string> {typeof(GrantAllocation).Name, typeof(AgreementGrantAllocation).Name, typeof(GrantAllocationNote).Name, typeof(GrantAllocationProjectCode).Name};


        /// <summary>
        /// Delete just the entity 
        /// </summary>
        public void Delete(DatabaseEntities dbContext)
        {
            dbContext.GrantAllocations.Remove(this);
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

            foreach(var x in AgreementGrantAllocations.ToList())
            {
                x.DeleteFull(dbContext);
            }

            foreach(var x in GrantAllocationNotes.ToList())
            {
                x.DeleteFull(dbContext);
            }

            foreach(var x in GrantAllocationProjectCodes.ToList())
            {
                x.DeleteFull(dbContext);
            }
        }

        [Key]
        public int GrantAllocationID { get; set; }
        public int GrantID { get; set; }
        public string ProjectName { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public decimal? AllocationAmount { get; set; }
        public int? CostTypeID { get; set; }
        public int? ProgramManagerPersonID { get; set; }
        public int? ProgramIndexID { get; set; }
        public int? FederalFundCodeID { get; set; }
        public int? OrganizationID { get; set; }
        public int? RegionID { get; set; }
        [NotMapped]
        public int PrimaryKey { get { return GrantAllocationID; } set { GrantAllocationID = value; } }

        public virtual ICollection<AgreementGrantAllocation> AgreementGrantAllocations { get; set; }
        public virtual ICollection<GrantAllocationNote> GrantAllocationNotes { get; set; }
        public virtual ICollection<GrantAllocationProjectCode> GrantAllocationProjectCodes { get; set; }
        public virtual Grant Grant { get; set; }
        public virtual CostType CostType { get; set; }
        public virtual Person ProgramManagerPerson { get; set; }
        public virtual ProgramIndex ProgramIndex { get; set; }
        public virtual FederalFundCode FederalFundCode { get; set; }
        public virtual Organization Organization { get; set; }
        public virtual Region Region { get; set; }

        public static class FieldLengths
        {
            public const int ProjectName = 100;
        }
    }
}