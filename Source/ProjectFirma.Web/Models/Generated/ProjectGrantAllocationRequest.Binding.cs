//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[ProjectGrantAllocationRequest]
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
    // Table [dbo].[ProjectGrantAllocationRequest] is NOT multi-tenant, so is attributed as ICanDeleteFull
    [Table("[dbo].[ProjectGrantAllocationRequest]")]
    public partial class ProjectGrantAllocationRequest : IHavePrimaryKey, ICanDeleteFull
    {
        /// <summary>
        /// Default Constructor; only used by EF
        /// </summary>
        protected ProjectGrantAllocationRequest()
        {

        }

        /// <summary>
        /// Constructor for building a new object with MaximalConstructor required fields in preparation for insert into database
        /// </summary>
        public ProjectGrantAllocationRequest(int projectGrantAllocationRequestID, int projectID, int grantAllocationID, decimal? totalAmount) : this()
        {
            this.ProjectGrantAllocationRequestID = projectGrantAllocationRequestID;
            this.ProjectID = projectID;
            this.GrantAllocationID = grantAllocationID;
            this.TotalAmount = totalAmount;
        }

        /// <summary>
        /// Constructor for building a new object with MinimalConstructor required fields in preparation for insert into database
        /// </summary>
        public ProjectGrantAllocationRequest(int projectID, int grantAllocationID) : this()
        {
            // Mark this as a new object by setting primary key with special value
            this.ProjectGrantAllocationRequestID = ModelObjectHelpers.MakeNextUnsavedPrimaryKeyValue();
            
            this.ProjectID = projectID;
            this.GrantAllocationID = grantAllocationID;
        }

        /// <summary>
        /// Constructor for building a new object with MinimalConstructor required fields, using objects whenever possible
        /// </summary>
        public ProjectGrantAllocationRequest(Project project, GrantAllocation grantAllocation) : this()
        {
            // Mark this as a new object by setting primary key with special value
            this.ProjectGrantAllocationRequestID = ModelObjectHelpers.MakeNextUnsavedPrimaryKeyValue();
            this.ProjectID = project.ProjectID;
            this.Project = project;
            project.ProjectGrantAllocationRequests.Add(this);
            this.GrantAllocationID = grantAllocation.GrantAllocationID;
            this.GrantAllocation = grantAllocation;
            grantAllocation.ProjectGrantAllocationRequests.Add(this);
        }

        /// <summary>
        /// Creates a "blank" object of this type and populates primitives with defaults
        /// </summary>
        public static ProjectGrantAllocationRequest CreateNewBlank(Project project, GrantAllocation grantAllocation)
        {
            return new ProjectGrantAllocationRequest(project, grantAllocation);
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
        public static readonly List<string> DependentEntityTypeNames = new List<string> {typeof(ProjectGrantAllocationRequest).Name};


        /// <summary>
        /// Delete just the entity 
        /// </summary>
        public void Delete(DatabaseEntities dbContext)
        {
            dbContext.ProjectGrantAllocationRequests.Remove(this);
        }
        
        /// <summary>
        /// Delete entity plus all children
        /// </summary>
        public void DeleteFull(DatabaseEntities dbContext)
        {
            
            Delete(dbContext);
        }

        [Key]
        public int ProjectGrantAllocationRequestID { get; set; }
        public int ProjectID { get; set; }
        public int GrantAllocationID { get; set; }
        public decimal? TotalAmount { get; set; }
        [NotMapped]
        public int PrimaryKey { get { return ProjectGrantAllocationRequestID; } set { ProjectGrantAllocationRequestID = value; } }

        public virtual Project Project { get; set; }
        public virtual GrantAllocation GrantAllocation { get; set; }

        public static class FieldLengths
        {

        }
    }
}