//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[FundingSource]
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
    // Table [dbo].[FundingSource] is NOT multi-tenant, so is attributed as ICanDeleteFull
    [Table("[dbo].[FundingSource]")]
    public partial class FundingSource : IHavePrimaryKey, ICanDeleteFull
    {
        /// <summary>
        /// Default Constructor; only used by EF
        /// </summary>
        protected FundingSource()
        {
            this.ProjectGrantAllocationExpenditures = new HashSet<ProjectGrantAllocationExpenditure>();
            this.ProjectGrantAllocationExpenditureUpdates = new HashSet<ProjectGrantAllocationExpenditureUpdate>();
            this.ProjectGrantAllocationRequests = new HashSet<ProjectGrantAllocationRequest>();
            this.ProjectGrantAllocationRequestUpdates = new HashSet<ProjectGrantAllocationRequestUpdate>();
        }

        /// <summary>
        /// Constructor for building a new object with MaximalConstructor required fields in preparation for insert into database
        /// </summary>
        public FundingSource(int fundingSourceID, int organizationID, string fundingSourceName, bool isActive, string fundingSourceDescription, decimal? fundingSourceAmount, int? grantAllocationID) : this()
        {
            this.FundingSourceID = fundingSourceID;
            this.OrganizationID = organizationID;
            this.FundingSourceName = fundingSourceName;
            this.IsActive = isActive;
            this.FundingSourceDescription = fundingSourceDescription;
            this.FundingSourceAmount = fundingSourceAmount;
            this.GrantAllocationID = grantAllocationID;
        }

        /// <summary>
        /// Constructor for building a new object with MinimalConstructor required fields in preparation for insert into database
        /// </summary>
        public FundingSource(int organizationID, string fundingSourceName, bool isActive) : this()
        {
            // Mark this as a new object by setting primary key with special value
            this.FundingSourceID = ModelObjectHelpers.MakeNextUnsavedPrimaryKeyValue();
            
            this.OrganizationID = organizationID;
            this.FundingSourceName = fundingSourceName;
            this.IsActive = isActive;
        }

        /// <summary>
        /// Constructor for building a new object with MinimalConstructor required fields, using objects whenever possible
        /// </summary>
        public FundingSource(Organization organization, string fundingSourceName, bool isActive) : this()
        {
            // Mark this as a new object by setting primary key with special value
            this.FundingSourceID = ModelObjectHelpers.MakeNextUnsavedPrimaryKeyValue();
            this.OrganizationID = organization.OrganizationID;
            this.Organization = organization;
            organization.FundingSources.Add(this);
            this.FundingSourceName = fundingSourceName;
            this.IsActive = isActive;
        }

        /// <summary>
        /// Creates a "blank" object of this type and populates primitives with defaults
        /// </summary>
        public static FundingSource CreateNewBlank(Organization organization)
        {
            return new FundingSource(organization, default(string), default(bool));
        }

        /// <summary>
        /// Does this object have any dependent objects? (If it does have dependent objects, these would need to be deleted before this object could be deleted.)
        /// </summary>
        /// <returns></returns>
        public bool HasDependentObjects()
        {
            return ProjectGrantAllocationExpenditures.Any() || ProjectGrantAllocationExpenditureUpdates.Any() || ProjectGrantAllocationRequests.Any() || ProjectGrantAllocationRequestUpdates.Any();
        }

        /// <summary>
        /// Dependent type names of this entity
        /// </summary>
        public static readonly List<string> DependentEntityTypeNames = new List<string> {typeof(FundingSource).Name, typeof(ProjectGrantAllocationExpenditure).Name, typeof(ProjectGrantAllocationExpenditureUpdate).Name, typeof(ProjectGrantAllocationRequest).Name, typeof(ProjectGrantAllocationRequestUpdate).Name};


        /// <summary>
        /// Delete just the entity 
        /// </summary>
        public void Delete(DatabaseEntities dbContext)
        {
            dbContext.FundingSources.Remove(this);
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
        public int FundingSourceID { get; set; }
        public int OrganizationID { get; set; }
        public string FundingSourceName { get; set; }
        public bool IsActive { get; set; }
        public string FundingSourceDescription { get; set; }
        public decimal? FundingSourceAmount { get; set; }
        public int? GrantAllocationID { get; set; }
        [NotMapped]
        public int PrimaryKey { get { return FundingSourceID; } set { FundingSourceID = value; } }

        public virtual ICollection<ProjectGrantAllocationExpenditure> ProjectGrantAllocationExpenditures { get; set; }
        public virtual ICollection<ProjectGrantAllocationExpenditureUpdate> ProjectGrantAllocationExpenditureUpdates { get; set; }
        public virtual ICollection<ProjectGrantAllocationRequest> ProjectGrantAllocationRequests { get; set; }
        public virtual ICollection<ProjectGrantAllocationRequestUpdate> ProjectGrantAllocationRequestUpdates { get; set; }
        public virtual Organization Organization { get; set; }
        public virtual GrantAllocation GrantAllocation { get; set; }

        public static class FieldLengths
        {
            public const int FundingSourceName = 200;
            public const int FundingSourceDescription = 500;
        }
    }
}