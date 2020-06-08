//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[ProjectTypePerformanceMeasure]
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
    // Table [dbo].[ProjectTypePerformanceMeasure] is NOT multi-tenant, so is attributed as ICanDeleteFull
    [Table("[dbo].[ProjectTypePerformanceMeasure]")]
    public partial class ProjectTypePerformanceMeasure : IHavePrimaryKey, ICanDeleteFull
    {
        /// <summary>
        /// Default Constructor; only used by EF
        /// </summary>
        protected ProjectTypePerformanceMeasure()
        {

        }

        /// <summary>
        /// Constructor for building a new object with MaximalConstructor required fields in preparation for insert into database
        /// </summary>
        public ProjectTypePerformanceMeasure(int projectTypePerformanceMeasureID, int projectTypeID, int performanceMeasureID, bool isPrimaryProjectType) : this()
        {
            this.ProjectTypePerformanceMeasureID = projectTypePerformanceMeasureID;
            this.ProjectTypeID = projectTypeID;
            this.PerformanceMeasureID = performanceMeasureID;
            this.IsPrimaryProjectType = isPrimaryProjectType;
        }

        /// <summary>
        /// Constructor for building a new object with MinimalConstructor required fields in preparation for insert into database
        /// </summary>
        public ProjectTypePerformanceMeasure(int projectTypeID, int performanceMeasureID, bool isPrimaryProjectType) : this()
        {
            // Mark this as a new object by setting primary key with special value
            this.ProjectTypePerformanceMeasureID = ModelObjectHelpers.MakeNextUnsavedPrimaryKeyValue();
            
            this.ProjectTypeID = projectTypeID;
            this.PerformanceMeasureID = performanceMeasureID;
            this.IsPrimaryProjectType = isPrimaryProjectType;
        }

        /// <summary>
        /// Constructor for building a new object with MinimalConstructor required fields, using objects whenever possible
        /// </summary>
        public ProjectTypePerformanceMeasure(ProjectType projectType, PerformanceMeasure performanceMeasure, bool isPrimaryProjectType) : this()
        {
            // Mark this as a new object by setting primary key with special value
            this.ProjectTypePerformanceMeasureID = ModelObjectHelpers.MakeNextUnsavedPrimaryKeyValue();
            this.ProjectTypeID = projectType.ProjectTypeID;
            this.ProjectType = projectType;
            projectType.ProjectTypePerformanceMeasures.Add(this);
            this.PerformanceMeasureID = performanceMeasure.PerformanceMeasureID;
            this.PerformanceMeasure = performanceMeasure;
            performanceMeasure.ProjectTypePerformanceMeasures.Add(this);
            this.IsPrimaryProjectType = isPrimaryProjectType;
        }

        /// <summary>
        /// Creates a "blank" object of this type and populates primitives with defaults
        /// </summary>
        public static ProjectTypePerformanceMeasure CreateNewBlank(ProjectType projectType, PerformanceMeasure performanceMeasure)
        {
            return new ProjectTypePerformanceMeasure(projectType, performanceMeasure, default(bool));
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
        public static readonly List<string> DependentEntityTypeNames = new List<string> {typeof(ProjectTypePerformanceMeasure).Name};


        /// <summary>
        /// Delete just the entity 
        /// </summary>
        public void Delete(DatabaseEntities dbContext)
        {
            dbContext.ProjectTypePerformanceMeasures.Remove(this);
        }
        
        /// <summary>
        /// Delete entity plus all children
        /// </summary>
        public void DeleteFull(DatabaseEntities dbContext)
        {
            
            Delete(dbContext);
        }

        [Key]
        public int ProjectTypePerformanceMeasureID { get; set; }
        public int ProjectTypeID { get; set; }
        public int PerformanceMeasureID { get; set; }
        public bool IsPrimaryProjectType { get; set; }
        [NotMapped]
        public int PrimaryKey { get { return ProjectTypePerformanceMeasureID; } set { ProjectTypePerformanceMeasureID = value; } }

        public virtual ProjectType ProjectType { get; set; }
        public virtual PerformanceMeasure PerformanceMeasure { get; set; }

        public static class FieldLengths
        {

        }
    }
}