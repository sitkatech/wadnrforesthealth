//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[ProjectCustomGridConfiguration]
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
    // Table [dbo].[ProjectCustomGridConfiguration] is NOT multi-tenant, so is attributed as ICanDeleteFull
    [Table("[dbo].[ProjectCustomGridConfiguration]")]
    public partial class ProjectCustomGridConfiguration : IHavePrimaryKey, ICanDeleteFull
    {
        /// <summary>
        /// Default Constructor; only used by EF
        /// </summary>
        protected ProjectCustomGridConfiguration()
        {

        }

        /// <summary>
        /// Constructor for building a new object with MaximalConstructor required fields in preparation for insert into database
        /// </summary>
        public ProjectCustomGridConfiguration(int projectCustomGridConfigurationID, int projectCustomGridTypeID, int projectCustomGridColumnID, int? projectCustomAttributeTypeID, bool isEnabled, int? sortOrder) : this()
        {
            this.ProjectCustomGridConfigurationID = projectCustomGridConfigurationID;
            this.ProjectCustomGridTypeID = projectCustomGridTypeID;
            this.ProjectCustomGridColumnID = projectCustomGridColumnID;
            this.ProjectCustomAttributeTypeID = projectCustomAttributeTypeID;
            this.IsEnabled = isEnabled;
            this.SortOrder = sortOrder;
        }

        /// <summary>
        /// Constructor for building a new object with MinimalConstructor required fields in preparation for insert into database
        /// </summary>
        public ProjectCustomGridConfiguration(int projectCustomGridTypeID, int projectCustomGridColumnID, bool isEnabled) : this()
        {
            // Mark this as a new object by setting primary key with special value
            this.ProjectCustomGridConfigurationID = ModelObjectHelpers.MakeNextUnsavedPrimaryKeyValue();
            
            this.ProjectCustomGridTypeID = projectCustomGridTypeID;
            this.ProjectCustomGridColumnID = projectCustomGridColumnID;
            this.IsEnabled = isEnabled;
        }

        /// <summary>
        /// Constructor for building a new object with MinimalConstructor required fields, using objects whenever possible
        /// </summary>
        public ProjectCustomGridConfiguration(ProjectCustomGridType projectCustomGridType, ProjectCustomGridColumn projectCustomGridColumn, bool isEnabled) : this()
        {
            // Mark this as a new object by setting primary key with special value
            this.ProjectCustomGridConfigurationID = ModelObjectHelpers.MakeNextUnsavedPrimaryKeyValue();
            this.ProjectCustomGridTypeID = projectCustomGridType.ProjectCustomGridTypeID;
            this.ProjectCustomGridColumnID = projectCustomGridColumn.ProjectCustomGridColumnID;
            this.ProjectCustomGridColumn = projectCustomGridColumn;
            projectCustomGridColumn.ProjectCustomGridConfigurations.Add(this);
            this.IsEnabled = isEnabled;
        }

        /// <summary>
        /// Creates a "blank" object of this type and populates primitives with defaults
        /// </summary>
        public static ProjectCustomGridConfiguration CreateNewBlank(ProjectCustomGridType projectCustomGridType, ProjectCustomGridColumn projectCustomGridColumn)
        {
            return new ProjectCustomGridConfiguration(projectCustomGridType, projectCustomGridColumn, default(bool));
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
        public static readonly List<string> DependentEntityTypeNames = new List<string> {typeof(ProjectCustomGridConfiguration).Name};


        /// <summary>
        /// Delete just the entity 
        /// </summary>
        public void Delete(DatabaseEntities dbContext)
        {
            dbContext.ProjectCustomGridConfigurations.Remove(this);
        }
        
        /// <summary>
        /// Delete entity plus all children
        /// </summary>
        public void DeleteFull(DatabaseEntities dbContext)
        {
            
            Delete(dbContext);
        }

        [Key]
        public int ProjectCustomGridConfigurationID { get; set; }
        public int ProjectCustomGridTypeID { get; set; }
        public int ProjectCustomGridColumnID { get; set; }
        public int? ProjectCustomAttributeTypeID { get; set; }
        public bool IsEnabled { get; set; }
        public int? SortOrder { get; set; }
        [NotMapped]
        public int PrimaryKey { get { return ProjectCustomGridConfigurationID; } set { ProjectCustomGridConfigurationID = value; } }

        public ProjectCustomGridType ProjectCustomGridType { get { return ProjectCustomGridType.AllLookupDictionary[ProjectCustomGridTypeID]; } }
        public virtual ProjectCustomGridColumn ProjectCustomGridColumn { get; set; }
        public virtual ProjectCustomAttributeType ProjectCustomAttributeType { get; set; }

        public static class FieldLengths
        {

        }
    }
}