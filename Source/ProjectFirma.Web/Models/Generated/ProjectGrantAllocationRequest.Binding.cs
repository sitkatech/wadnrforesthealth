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
using CodeFirstStoreFunctions;
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
        public ProjectGrantAllocationRequest(int projectGrantAllocationRequestID, int projectID, int grantAllocationID, decimal? totalAmount, decimal? matchAmount, decimal? payAmount, DateTime createDate, DateTime? updateDate, bool importedFromTabularData) : this()
        {
            this.ProjectGrantAllocationRequestID = projectGrantAllocationRequestID;
            this.ProjectID = projectID;
            this.GrantAllocationID = grantAllocationID;
            this.TotalAmount = totalAmount;
            this.MatchAmount = matchAmount;
            this.PayAmount = payAmount;
            this.CreateDate = createDate;
            this.UpdateDate = updateDate;
            this.ImportedFromTabularData = importedFromTabularData;
        }

        /// <summary>
        /// Constructor for building a new object with MinimalConstructor required fields in preparation for insert into database
        /// </summary>
        public ProjectGrantAllocationRequest(int projectID, int grantAllocationID, DateTime createDate, bool importedFromTabularData) : this()
        {
            // Mark this as a new object by setting primary key with special value
            this.ProjectGrantAllocationRequestID = ModelObjectHelpers.MakeNextUnsavedPrimaryKeyValue();
            
            this.ProjectID = projectID;
            this.GrantAllocationID = grantAllocationID;
            this.CreateDate = createDate;
            this.ImportedFromTabularData = importedFromTabularData;
        }

        /// <summary>
        /// Constructor for building a new object with MinimalConstructor required fields, using objects whenever possible
        /// </summary>
        public ProjectGrantAllocationRequest(Project project, FundSourceAllocation fundSourceAllocation, DateTime createDate, bool importedFromTabularData) : this()
        {
            // Mark this as a new object by setting primary key with special value
            this.ProjectGrantAllocationRequestID = ModelObjectHelpers.MakeNextUnsavedPrimaryKeyValue();
            this.ProjectID = project.ProjectID;
            this.Project = project;
            project.ProjectGrantAllocationRequests.Add(this);
            this.GrantAllocationID = fundSourceAllocation.GrantAllocationID;
            this.FundSourceAllocation = fundSourceAllocation;
            fundSourceAllocation.ProjectGrantAllocationRequests.Add(this);
            this.CreateDate = createDate;
            this.ImportedFromTabularData = importedFromTabularData;
        }

        /// <summary>
        /// Creates a "blank" object of this type and populates primitives with defaults
        /// </summary>
        public static ProjectGrantAllocationRequest CreateNewBlank(Project project, FundSourceAllocation fundSourceAllocation)
        {
            return new ProjectGrantAllocationRequest(project, fundSourceAllocation, default(DateTime), default(bool));
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
        public decimal? MatchAmount { get; set; }
        public decimal? PayAmount { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime? UpdateDate { get; set; }
        public bool ImportedFromTabularData { get; set; }
        [NotMapped]
        public int PrimaryKey { get { return ProjectGrantAllocationRequestID; } set { ProjectGrantAllocationRequestID = value; } }

        public virtual Project Project { get; set; }
        public virtual FundSourceAllocation FundSourceAllocation { get; set; }

        public static class FieldLengths
        {

        }
    }
}