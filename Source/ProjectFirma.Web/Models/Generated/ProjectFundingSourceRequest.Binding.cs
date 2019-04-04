//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[ProjectFundingSourceRequest]
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
    // Table [dbo].[ProjectFundingSourceRequest] is NOT multi-tenant, so is attributed as ICanDeleteFull
    [Table("[dbo].[ProjectFundingSourceRequest]")]
    public partial class ProjectFundingSourceRequest : IHavePrimaryKey, ICanDeleteFull
    {
        /// <summary>
        /// Default Constructor; only used by EF
        /// </summary>
        protected ProjectFundingSourceRequest()
        {

        }

        /// <summary>
        /// Constructor for building a new object with MaximalConstructor required fields in preparation for insert into database
        /// </summary>
        public ProjectFundingSourceRequest(int projectFundingSourceRequestID, int projectID, int? fundingSourceID, decimal? securedAmount, decimal? unsecuredAmount, int grantAllocationID) : this()
        {
            this.ProjectFundingSourceRequestID = projectFundingSourceRequestID;
            this.ProjectID = projectID;
            this.FundingSourceID = fundingSourceID;
            this.SecuredAmount = securedAmount;
            this.UnsecuredAmount = unsecuredAmount;
            this.GrantAllocationID = grantAllocationID;
        }

        /// <summary>
        /// Constructor for building a new object with MinimalConstructor required fields in preparation for insert into database
        /// </summary>
        public ProjectFundingSourceRequest(int projectID, int grantAllocationID) : this()
        {
            // Mark this as a new object by setting primary key with special value
            this.ProjectFundingSourceRequestID = ModelObjectHelpers.MakeNextUnsavedPrimaryKeyValue();
            
            this.ProjectID = projectID;
            this.GrantAllocationID = grantAllocationID;
        }

        /// <summary>
        /// Constructor for building a new object with MinimalConstructor required fields, using objects whenever possible
        /// </summary>
        public ProjectFundingSourceRequest(Project project, GrantAllocation grantAllocation) : this()
        {
            // Mark this as a new object by setting primary key with special value
            this.ProjectFundingSourceRequestID = ModelObjectHelpers.MakeNextUnsavedPrimaryKeyValue();
            this.ProjectID = project.ProjectID;
            this.Project = project;
            project.ProjectFundingSourceRequests.Add(this);
            this.GrantAllocationID = grantAllocation.GrantAllocationID;
            this.GrantAllocation = grantAllocation;
            grantAllocation.ProjectFundingSourceRequests.Add(this);
        }

        /// <summary>
        /// Creates a "blank" object of this type and populates primitives with defaults
        /// </summary>
        public static ProjectFundingSourceRequest CreateNewBlank(Project project, GrantAllocation grantAllocation)
        {
            return new ProjectFundingSourceRequest(project, grantAllocation);
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
        /// Dependent type names of this entity
        /// </summary>
        public static readonly List<string> DependentEntityTypeNames = new List<string> {typeof(ProjectFundingSourceRequest).Name};


        /// <summary>
        /// Delete just the entity 
        /// </summary>
        public void Delete(DatabaseEntities dbContext)
        {
            dbContext.ProjectFundingSourceRequests.Remove(this);
        }
        
        /// <summary>
        /// Delete entity plus all children
        /// </summary>
        public void DeleteFull(DatabaseEntities dbContext)
        {
            
            Delete(dbContext);
        }

        [Key]
        public int ProjectFundingSourceRequestID { get; set; }
        public int ProjectID { get; set; }
        public int? FundingSourceID { get; set; }
        public decimal? SecuredAmount { get; set; }
        public decimal? UnsecuredAmount { get; set; }
        public int GrantAllocationID { get; set; }
        [NotMapped]
        public int PrimaryKey { get { return ProjectFundingSourceRequestID; } set { ProjectFundingSourceRequestID = value; } }

        public virtual Project Project { get; set; }
        public virtual FundingSource FundingSource { get; set; }
        public virtual GrantAllocation GrantAllocation { get; set; }

        public static class FieldLengths
        {

        }
    }
}