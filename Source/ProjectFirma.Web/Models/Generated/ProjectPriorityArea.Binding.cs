//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[ProjectPriorityArea]
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
    // Table [dbo].[ProjectPriorityArea] is NOT multi-tenant, so is attributed as ICanDeleteFull
    [Table("[dbo].[ProjectPriorityArea]")]
    public partial class ProjectPriorityArea : IHavePrimaryKey, ICanDeleteFull
    {
        /// <summary>
        /// Default Constructor; only used by EF
        /// </summary>
        protected ProjectPriorityArea()
        {

        }

        /// <summary>
        /// Constructor for building a new object with MaximalConstructor required fields in preparation for insert into database
        /// </summary>
        public ProjectPriorityArea(int projectPriorityAreaID, int projectID, int priorityAreaID) : this()
        {
            this.ProjectPriorityAreaID = projectPriorityAreaID;
            this.ProjectID = projectID;
            this.PriorityAreaID = priorityAreaID;
        }

        /// <summary>
        /// Constructor for building a new object with MinimalConstructor required fields in preparation for insert into database
        /// </summary>
        public ProjectPriorityArea(int projectID, int priorityAreaID) : this()
        {
            // Mark this as a new object by setting primary key with special value
            this.ProjectPriorityAreaID = ModelObjectHelpers.MakeNextUnsavedPrimaryKeyValue();
            
            this.ProjectID = projectID;
            this.PriorityAreaID = priorityAreaID;
        }

        /// <summary>
        /// Constructor for building a new object with MinimalConstructor required fields, using objects whenever possible
        /// </summary>
        public ProjectPriorityArea(Project project, PriorityArea priorityArea) : this()
        {
            // Mark this as a new object by setting primary key with special value
            this.ProjectPriorityAreaID = ModelObjectHelpers.MakeNextUnsavedPrimaryKeyValue();
            this.ProjectID = project.ProjectID;
            this.Project = project;
            project.ProjectPriorityAreas.Add(this);
            this.PriorityAreaID = priorityArea.PriorityAreaID;
            this.PriorityArea = priorityArea;
            priorityArea.ProjectPriorityAreas.Add(this);
        }

        /// <summary>
        /// Creates a "blank" object of this type and populates primitives with defaults
        /// </summary>
        public static ProjectPriorityArea CreateNewBlank(Project project, PriorityArea priorityArea)
        {
            return new ProjectPriorityArea(project, priorityArea);
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
        public static readonly List<string> DependentEntityTypeNames = new List<string> {typeof(ProjectPriorityArea).Name};


        /// <summary>
        /// Delete just the entity 
        /// </summary>
        public void Delete(DatabaseEntities dbContext)
        {
            dbContext.ProjectPriorityAreas.Remove(this);
        }
        
        /// <summary>
        /// Delete entity plus all children
        /// </summary>
        public void DeleteFull(DatabaseEntities dbContext)
        {
            
            Delete(dbContext);
        }

        [Key]
        public int ProjectPriorityAreaID { get; set; }
        public int ProjectID { get; set; }
        public int PriorityAreaID { get; set; }
        [NotMapped]
        public int PrimaryKey { get { return ProjectPriorityAreaID; } set { ProjectPriorityAreaID = value; } }

        public virtual Project Project { get; set; }
        public virtual PriorityArea PriorityArea { get; set; }

        public static class FieldLengths
        {

        }
    }
}