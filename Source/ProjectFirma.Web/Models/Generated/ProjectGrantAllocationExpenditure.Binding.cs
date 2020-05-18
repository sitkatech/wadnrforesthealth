//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[ProjectGrantAllocationExpenditure]
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
    // Table [dbo].[ProjectGrantAllocationExpenditure] is NOT multi-tenant, so is attributed as ICanDeleteFull
    [Table("[dbo].[ProjectGrantAllocationExpenditure]")]
    public partial class ProjectGrantAllocationExpenditure : IHavePrimaryKey, ICanDeleteFull
    {
        /// <summary>
        /// Default Constructor; only used by EF
        /// </summary>
        protected ProjectGrantAllocationExpenditure()
        {

        }

        /// <summary>
        /// Constructor for building a new object with MaximalConstructor required fields in preparation for insert into database
        /// </summary>
        public ProjectGrantAllocationExpenditure(int projectGrantAllocationExpenditureID, int projectID, int calendarYear, decimal expenditureAmount, int grantAllocationID) : this()
        {
            this.ProjectGrantAllocationExpenditureID = projectGrantAllocationExpenditureID;
            this.ProjectID = projectID;
            this.CalendarYear = calendarYear;
            this.ExpenditureAmount = expenditureAmount;
            this.GrantAllocationID = grantAllocationID;
        }

        /// <summary>
        /// Constructor for building a new object with MinimalConstructor required fields in preparation for insert into database
        /// </summary>
        public ProjectGrantAllocationExpenditure(int projectID, int calendarYear, decimal expenditureAmount, int grantAllocationID) : this()
        {
            // Mark this as a new object by setting primary key with special value
            this.ProjectGrantAllocationExpenditureID = ModelObjectHelpers.MakeNextUnsavedPrimaryKeyValue();
            
            this.ProjectID = projectID;
            this.CalendarYear = calendarYear;
            this.ExpenditureAmount = expenditureAmount;
            this.GrantAllocationID = grantAllocationID;
        }

        /// <summary>
        /// Constructor for building a new object with MinimalConstructor required fields, using objects whenever possible
        /// </summary>
        public ProjectGrantAllocationExpenditure(Project project, int calendarYear, decimal expenditureAmount, GrantAllocation grantAllocation) : this()
        {
            // Mark this as a new object by setting primary key with special value
            this.ProjectGrantAllocationExpenditureID = ModelObjectHelpers.MakeNextUnsavedPrimaryKeyValue();
            this.ProjectID = project.ProjectID;
            this.Project = project;
            project.ProjectGrantAllocationExpenditures.Add(this);
            this.CalendarYear = calendarYear;
            this.ExpenditureAmount = expenditureAmount;
            this.GrantAllocationID = grantAllocation.GrantAllocationID;
            this.GrantAllocation = grantAllocation;
            grantAllocation.ProjectGrantAllocationExpenditures.Add(this);
        }

        /// <summary>
        /// Creates a "blank" object of this type and populates primitives with defaults
        /// </summary>
        public static ProjectGrantAllocationExpenditure CreateNewBlank(Project project, GrantAllocation grantAllocation)
        {
            return new ProjectGrantAllocationExpenditure(project, default(int), default(decimal), grantAllocation);
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
        public static readonly List<string> DependentEntityTypeNames = new List<string> {typeof(ProjectGrantAllocationExpenditure).Name};


        /// <summary>
        /// Delete just the entity 
        /// </summary>
        public void Delete(DatabaseEntities dbContext)
        {
            dbContext.ProjectGrantAllocationExpenditures.Remove(this);
        }
        
        /// <summary>
        /// Delete entity plus all children
        /// </summary>
        public void DeleteFull(DatabaseEntities dbContext)
        {
            
            Delete(dbContext);
        }

        [Key]
        public int ProjectGrantAllocationExpenditureID { get; set; }
        public int ProjectID { get; set; }
        public int CalendarYear { get; set; }
        public decimal ExpenditureAmount { get; set; }
        public int GrantAllocationID { get; set; }
        [NotMapped]
        public int PrimaryKey { get { return ProjectGrantAllocationExpenditureID; } set { ProjectGrantAllocationExpenditureID = value; } }

        public virtual Project Project { get; set; }
        public virtual GrantAllocation GrantAllocation { get; set; }

        public static class FieldLengths
        {

        }
    }
}