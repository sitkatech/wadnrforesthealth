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
            this.GrantAllocationBudgetLineItems = new HashSet<GrantAllocationBudgetLineItem>();
            this.GrantAllocationNotes = new HashSet<GrantAllocationNote>();
            this.GrantAllocationNoteInternals = new HashSet<GrantAllocationNoteInternal>();
            this.GrantAllocationProgramManagers = new HashSet<GrantAllocationProgramManager>();
            this.GrantAllocationProjectCodes = new HashSet<GrantAllocationProjectCode>();
            this.InvoiceLineItems = new HashSet<InvoiceLineItem>();
            this.ProjectGrantAllocationExpenditures = new HashSet<ProjectGrantAllocationExpenditure>();
            this.ProjectGrantAllocationExpenditureUpdates = new HashSet<ProjectGrantAllocationExpenditureUpdate>();
            this.ProjectGrantAllocationRequests = new HashSet<ProjectGrantAllocationRequest>();
            this.ProjectGrantAllocationRequestUpdates = new HashSet<ProjectGrantAllocationRequestUpdate>();
        }

        /// <summary>
        /// Constructor for building a new object with MaximalConstructor required fields in preparation for insert into database
        /// </summary>
        public GrantAllocation(int grantAllocationID, int grantID, string grantAllocationName, DateTime? startDate, DateTime? endDate, decimal? allocationAmount, int? programIndexID, int? federalFundCodeID, int? organizationID, int? regionID, int? divisionID, int? grantManagerID, int? grantAllocationFileResourceID) : this()
        {
            this.GrantAllocationID = grantAllocationID;
            this.GrantID = grantID;
            this.GrantAllocationName = grantAllocationName;
            this.StartDate = startDate;
            this.EndDate = endDate;
            this.AllocationAmount = allocationAmount;
            this.ProgramIndexID = programIndexID;
            this.FederalFundCodeID = federalFundCodeID;
            this.OrganizationID = organizationID;
            this.RegionID = regionID;
            this.DivisionID = divisionID;
            this.GrantManagerID = grantManagerID;
            this.GrantAllocationFileResourceID = grantAllocationFileResourceID;
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
            return AgreementGrantAllocations.Any() || GrantAllocationBudgetLineItems.Any() || GrantAllocationNotes.Any() || GrantAllocationNoteInternals.Any() || GrantAllocationProgramManagers.Any() || GrantAllocationProjectCodes.Any() || InvoiceLineItems.Any() || ProjectGrantAllocationExpenditures.Any() || ProjectGrantAllocationExpenditureUpdates.Any() || ProjectGrantAllocationRequests.Any() || ProjectGrantAllocationRequestUpdates.Any();
        }

        /// <summary>
        /// Dependent type names of this entity
        /// </summary>
        public static readonly List<string> DependentEntityTypeNames = new List<string> {typeof(GrantAllocation).Name, typeof(AgreementGrantAllocation).Name, typeof(GrantAllocationBudgetLineItem).Name, typeof(GrantAllocationNote).Name, typeof(GrantAllocationNoteInternal).Name, typeof(GrantAllocationProgramManager).Name, typeof(GrantAllocationProjectCode).Name, typeof(InvoiceLineItem).Name, typeof(ProjectGrantAllocationExpenditure).Name, typeof(ProjectGrantAllocationExpenditureUpdate).Name, typeof(ProjectGrantAllocationRequest).Name, typeof(ProjectGrantAllocationRequestUpdate).Name};


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

            foreach(var x in GrantAllocationBudgetLineItems.ToList())
            {
                x.DeleteFull(dbContext);
            }

            foreach(var x in GrantAllocationNotes.ToList())
            {
                x.DeleteFull(dbContext);
            }

            foreach(var x in GrantAllocationNoteInternals.ToList())
            {
                x.DeleteFull(dbContext);
            }

            foreach(var x in GrantAllocationProgramManagers.ToList())
            {
                x.DeleteFull(dbContext);
            }

            foreach(var x in GrantAllocationProjectCodes.ToList())
            {
                x.DeleteFull(dbContext);
            }

            foreach(var x in InvoiceLineItems.ToList())
            {
                x.DeleteFull(dbContext);
            }

            foreach(var x in ProjectGrantAllocationExpenditures.ToList())
            {
                x.DeleteFull(dbContext);
            }

            foreach(var x in ProjectGrantAllocationExpenditureUpdates.ToList())
            {
                x.DeleteFull(dbContext);
            }

            foreach(var x in ProjectGrantAllocationRequests.ToList())
            {
                x.DeleteFull(dbContext);
            }

            foreach(var x in ProjectGrantAllocationRequestUpdates.ToList())
            {
                x.DeleteFull(dbContext);
            }
        }

        [Key]
        public int GrantAllocationID { get; set; }
        public int GrantID { get; set; }
        public string GrantAllocationName { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public decimal? AllocationAmount { get; set; }
        public int? ProgramIndexID { get; set; }
        public int? FederalFundCodeID { get; set; }
        public int? OrganizationID { get; set; }
        public int? RegionID { get; set; }
        public int? DivisionID { get; set; }
        public int? GrantManagerID { get; set; }
        public int? GrantAllocationFileResourceID { get; set; }
        [NotMapped]
        public int PrimaryKey { get { return GrantAllocationID; } set { GrantAllocationID = value; } }

        public virtual ICollection<AgreementGrantAllocation> AgreementGrantAllocations { get; set; }
        public virtual ICollection<GrantAllocationBudgetLineItem> GrantAllocationBudgetLineItems { get; set; }
        public virtual ICollection<GrantAllocationNote> GrantAllocationNotes { get; set; }
        public virtual ICollection<GrantAllocationNoteInternal> GrantAllocationNoteInternals { get; set; }
        public virtual ICollection<GrantAllocationProgramManager> GrantAllocationProgramManagers { get; set; }
        public virtual ICollection<GrantAllocationProjectCode> GrantAllocationProjectCodes { get; set; }
        public virtual ICollection<InvoiceLineItem> InvoiceLineItems { get; set; }
        public virtual ICollection<ProjectGrantAllocationExpenditure> ProjectGrantAllocationExpenditures { get; set; }
        public virtual ICollection<ProjectGrantAllocationExpenditureUpdate> ProjectGrantAllocationExpenditureUpdates { get; set; }
        public virtual ICollection<ProjectGrantAllocationRequest> ProjectGrantAllocationRequests { get; set; }
        public virtual ICollection<ProjectGrantAllocationRequestUpdate> ProjectGrantAllocationRequestUpdates { get; set; }
        public virtual Grant Grant { get; set; }
        public virtual ProgramIndex ProgramIndex { get; set; }
        public virtual FederalFundCode FederalFundCode { get; set; }
        public virtual Organization Organization { get; set; }
        public virtual Region Region { get; set; }
        public Division Division { get { return DivisionID.HasValue ? Division.AllLookupDictionary[DivisionID.Value] : null; } }
        public virtual Person GrantManager { get; set; }
        public virtual FileResource GrantAllocationFileResource { get; set; }

        public static class FieldLengths
        {
            public const int GrantAllocationName = 100;
        }
    }
}