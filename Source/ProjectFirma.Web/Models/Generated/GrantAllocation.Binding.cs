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
using CodeFirstStoreFunctions;
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
            this.GrantAllocationChangeLogs = new HashSet<GrantAllocationChangeLog>();
            this.GrantAllocationExpenditures = new HashSet<GrantAllocationExpenditure>();
            this.GrantAllocationFileResources = new HashSet<GrantAllocationFileResource>();
            this.GrantAllocationLikelyPeople = new HashSet<GrantAllocationLikelyPerson>();
            this.GrantAllocationNotes = new HashSet<GrantAllocationNote>();
            this.GrantAllocationNoteInternals = new HashSet<GrantAllocationNoteInternal>();
            this.GrantAllocationProgramIndexProjectCodes = new HashSet<GrantAllocationProgramIndexProjectCode>();
            this.GrantAllocationProgramManagers = new HashSet<GrantAllocationProgramManager>();
            this.ProjectGrantAllocationRequests = new HashSet<ProjectGrantAllocationRequest>();
            this.ProjectGrantAllocationRequestUpdates = new HashSet<ProjectGrantAllocationRequestUpdate>();
        }

        /// <summary>
        /// Constructor for building a new object with MaximalConstructor required fields in preparation for insert into database
        /// </summary>
        public GrantAllocation(int grantAllocationID, string grantAllocationName, DateTime? startDate, DateTime? endDate, decimal? allocationAmount, int? federalFundCodeID, int? organizationID, int? dNRUplandRegionID, int? divisionID, int? grantManagerID, int? grantAllocationPriorityID, bool? hasFundFSPs, int? grantAllocationSourceID, bool? likelyToUse, int grantID) : this()
        {
            this.GrantAllocationID = grantAllocationID;
            this.GrantAllocationName = grantAllocationName;
            this.StartDate = startDate;
            this.EndDate = endDate;
            this.AllocationAmount = allocationAmount;
            this.FederalFundCodeID = federalFundCodeID;
            this.OrganizationID = organizationID;
            this.DNRUplandRegionID = dNRUplandRegionID;
            this.DivisionID = divisionID;
            this.GrantManagerID = grantManagerID;
            this.GrantAllocationPriorityID = grantAllocationPriorityID;
            this.HasFundFSPs = hasFundFSPs;
            this.GrantAllocationSourceID = grantAllocationSourceID;
            this.LikelyToUse = likelyToUse;
            this.GrantID = grantID;
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
            return AgreementGrantAllocations.Any() || GrantAllocationBudgetLineItems.Any() || GrantAllocationChangeLogs.Any() || GrantAllocationExpenditures.Any() || GrantAllocationFileResources.Any() || GrantAllocationLikelyPeople.Any() || GrantAllocationNotes.Any() || GrantAllocationNoteInternals.Any() || GrantAllocationProgramIndexProjectCodes.Any() || GrantAllocationProgramManagers.Any() || ProjectGrantAllocationRequests.Any() || ProjectGrantAllocationRequestUpdates.Any();
        }

        /// <summary>
        /// Active Dependent type names of this object
        /// </summary>
        public List<string> DependentObjectNames() 
        {
            var dependentObjects = new List<string>();
            
            if(AgreementGrantAllocations.Any())
            {
                dependentObjects.Add(typeof(AgreementGrantAllocation).Name);
            }

            if(GrantAllocationBudgetLineItems.Any())
            {
                dependentObjects.Add(typeof(GrantAllocationBudgetLineItem).Name);
            }

            if(GrantAllocationChangeLogs.Any())
            {
                dependentObjects.Add(typeof(GrantAllocationChangeLog).Name);
            }

            if(GrantAllocationExpenditures.Any())
            {
                dependentObjects.Add(typeof(GrantAllocationExpenditure).Name);
            }

            if(GrantAllocationFileResources.Any())
            {
                dependentObjects.Add(typeof(GrantAllocationFileResource).Name);
            }

            if(GrantAllocationLikelyPeople.Any())
            {
                dependentObjects.Add(typeof(GrantAllocationLikelyPerson).Name);
            }

            if(GrantAllocationNotes.Any())
            {
                dependentObjects.Add(typeof(GrantAllocationNote).Name);
            }

            if(GrantAllocationNoteInternals.Any())
            {
                dependentObjects.Add(typeof(GrantAllocationNoteInternal).Name);
            }

            if(GrantAllocationProgramIndexProjectCodes.Any())
            {
                dependentObjects.Add(typeof(GrantAllocationProgramIndexProjectCode).Name);
            }

            if(GrantAllocationProgramManagers.Any())
            {
                dependentObjects.Add(typeof(GrantAllocationProgramManager).Name);
            }

            if(ProjectGrantAllocationRequests.Any())
            {
                dependentObjects.Add(typeof(ProjectGrantAllocationRequest).Name);
            }

            if(ProjectGrantAllocationRequestUpdates.Any())
            {
                dependentObjects.Add(typeof(ProjectGrantAllocationRequestUpdate).Name);
            }
            return dependentObjects.Distinct().ToList();
        }

        /// <summary>
        /// Dependent type names of this entity
        /// </summary>
        public static readonly List<string> DependentEntityTypeNames = new List<string> {typeof(GrantAllocation).Name, typeof(AgreementGrantAllocation).Name, typeof(GrantAllocationBudgetLineItem).Name, typeof(GrantAllocationChangeLog).Name, typeof(GrantAllocationExpenditure).Name, typeof(GrantAllocationFileResource).Name, typeof(GrantAllocationLikelyPerson).Name, typeof(GrantAllocationNote).Name, typeof(GrantAllocationNoteInternal).Name, typeof(GrantAllocationProgramIndexProjectCode).Name, typeof(GrantAllocationProgramManager).Name, typeof(ProjectGrantAllocationRequest).Name, typeof(ProjectGrantAllocationRequestUpdate).Name};


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

            foreach(var x in GrantAllocationChangeLogs.ToList())
            {
                x.DeleteFull(dbContext);
            }

            foreach(var x in GrantAllocationExpenditures.ToList())
            {
                x.DeleteFull(dbContext);
            }

            foreach(var x in GrantAllocationFileResources.ToList())
            {
                x.DeleteFull(dbContext);
            }

            foreach(var x in GrantAllocationLikelyPeople.ToList())
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

            foreach(var x in GrantAllocationProgramIndexProjectCodes.ToList())
            {
                x.DeleteFull(dbContext);
            }

            foreach(var x in GrantAllocationProgramManagers.ToList())
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
        public string GrantAllocationName { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public decimal? AllocationAmount { get; set; }
        public int? FederalFundCodeID { get; set; }
        public int? OrganizationID { get; set; }
        public int? DNRUplandRegionID { get; set; }
        public int? DivisionID { get; set; }
        public int? GrantManagerID { get; set; }
        public int? GrantAllocationPriorityID { get; set; }
        public bool? HasFundFSPs { get; set; }
        public int? GrantAllocationSourceID { get; set; }
        public bool? LikelyToUse { get; set; }
        public int GrantID { get; set; }
        [NotMapped]
        public int PrimaryKey { get { return GrantAllocationID; } set { GrantAllocationID = value; } }

        public virtual ICollection<AgreementGrantAllocation> AgreementGrantAllocations { get; set; }
        public virtual ICollection<GrantAllocationBudgetLineItem> GrantAllocationBudgetLineItems { get; set; }
        public virtual ICollection<GrantAllocationChangeLog> GrantAllocationChangeLogs { get; set; }
        public virtual ICollection<GrantAllocationExpenditure> GrantAllocationExpenditures { get; set; }
        public virtual ICollection<GrantAllocationFileResource> GrantAllocationFileResources { get; set; }
        public virtual ICollection<GrantAllocationLikelyPerson> GrantAllocationLikelyPeople { get; set; }
        public virtual ICollection<GrantAllocationNote> GrantAllocationNotes { get; set; }
        public virtual ICollection<GrantAllocationNoteInternal> GrantAllocationNoteInternals { get; set; }
        public virtual ICollection<GrantAllocationProgramIndexProjectCode> GrantAllocationProgramIndexProjectCodes { get; set; }
        public virtual ICollection<GrantAllocationProgramManager> GrantAllocationProgramManagers { get; set; }
        public virtual ICollection<ProjectGrantAllocationRequest> ProjectGrantAllocationRequests { get; set; }
        public virtual ICollection<ProjectGrantAllocationRequestUpdate> ProjectGrantAllocationRequestUpdates { get; set; }
        public virtual FederalFundCode FederalFundCode { get; set; }
        public virtual Organization Organization { get; set; }
        public virtual DNRUplandRegion DNRUplandRegion { get; set; }
        public Division Division { get { return DivisionID.HasValue ? Division.AllLookupDictionary[DivisionID.Value] : null; } }
        public virtual Person GrantManager { get; set; }
        public virtual GrantAllocationPriority GrantAllocationPriority { get; set; }
        public virtual GrantAllocationSource GrantAllocationSource { get; set; }
        public virtual Grant Grant { get; set; }

        public static class FieldLengths
        {
            public const int GrantAllocationName = 100;
        }
    }
}