//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[ProjectCustomGridColumn]
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
    // Table [dbo].[ProjectCustomGridColumn] is NOT multi-tenant, so is attributed as ICanDeleteFull
    [Table("[dbo].[ProjectCustomGridColumn]")]
    public partial class ProjectCustomGridColumn : IHavePrimaryKey, ICanDeleteFull
    {
        /// <summary>
        /// Default Constructor; only used by EF
        /// </summary>
        protected ProjectCustomGridColumn()
        {
            this.ProjectCustomGridConfigurations = new HashSet<ProjectCustomGridConfiguration>();
        }

        /// <summary>
        /// Constructor for building a new object with MaximalConstructor required fields in preparation for insert into database
        /// </summary>
        public ProjectCustomGridColumn(int projectCustomGridColumnID, string projectCustomGridColumnName, string projectCustomGridColumnDisplayName, bool isOptional) : this()
        {
            this.ProjectCustomGridColumnID = projectCustomGridColumnID;
            this.ProjectCustomGridColumnName = projectCustomGridColumnName;
            this.ProjectCustomGridColumnDisplayName = projectCustomGridColumnDisplayName;
            this.IsOptional = isOptional;
        }

        /// <summary>
        /// Constructor for building a new object with MinimalConstructor required fields in preparation for insert into database
        /// </summary>
        public ProjectCustomGridColumn(string projectCustomGridColumnName, string projectCustomGridColumnDisplayName, bool isOptional) : this()
        {
            // Mark this as a new object by setting primary key with special value
            this.ProjectCustomGridColumnID = ModelObjectHelpers.MakeNextUnsavedPrimaryKeyValue();
            
            this.ProjectCustomGridColumnName = projectCustomGridColumnName;
            this.ProjectCustomGridColumnDisplayName = projectCustomGridColumnDisplayName;
            this.IsOptional = isOptional;
        }


        /// <summary>
        /// Creates a "blank" object of this type and populates primitives with defaults
        /// </summary>
        public static ProjectCustomGridColumn CreateNewBlank()
        {
            return new ProjectCustomGridColumn(default(string), default(string), default(bool));
        }

        /// <summary>
        /// Does this object have any dependent objects? (If it does have dependent objects, these would need to be deleted before this object could be deleted.)
        /// </summary>
        /// <returns></returns>
        public bool HasDependentObjects()
        {
            return ProjectCustomGridConfigurations.Any();
        }

        /// <summary>
        /// Active Dependent type names of this object
        /// </summary>
        public List<string> DependentObjectNames() 
        {
            var dependentObjects = new List<string>();
            
            if(ProjectCustomGridConfigurations.Any())
            {
                dependentObjects.Add(typeof(ProjectCustomGridConfiguration).Name);
            }
            return dependentObjects.Distinct().ToList();
        }

        /// <summary>
        /// Dependent type names of this entity
        /// </summary>
        public static readonly List<string> DependentEntityTypeNames = new List<string> {typeof(ProjectCustomGridColumn).Name, typeof(ProjectCustomGridConfiguration).Name};


        /// <summary>
        /// Delete just the entity 
        /// </summary>
        public void Delete(DatabaseEntities dbContext)
        {
            dbContext.ProjectCustomGridColumns.Remove(this);
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

            foreach(var x in ProjectCustomGridConfigurations.ToList())
            {
                x.DeleteFull(dbContext);
            }
        }

        [Key]
        public int ProjectCustomGridColumnID { get; set; }
        public string ProjectCustomGridColumnName { get; set; }
        public string ProjectCustomGridColumnDisplayName { get; set; }
        public bool IsOptional { get; set; }
        [NotMapped]
        public int PrimaryKey { get { return ProjectCustomGridColumnID; } set { ProjectCustomGridColumnID = value; } }

        public virtual ICollection<ProjectCustomGridConfiguration> ProjectCustomGridConfigurations { get; set; }

        public static class FieldLengths
        {
            public const int ProjectCustomGridColumnName = 100;
            public const int ProjectCustomGridColumnDisplayName = 100;
        }
    }
}