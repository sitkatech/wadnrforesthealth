//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[ProjectFundingSource]
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
    // Table [dbo].[ProjectFundingSource] is NOT multi-tenant, so is attributed as ICanDeleteFull
    [Table("[dbo].[ProjectFundingSource]")]
    public partial class ProjectFundingSource : IHavePrimaryKey, ICanDeleteFull
    {
        /// <summary>
        /// Default Constructor; only used by EF
        /// </summary>
        protected ProjectFundingSource()
        {
            this.Projects = new HashSet<Project>();
        }

        /// <summary>
        /// Constructor for building a new object with MaximalConstructor required fields in preparation for insert into database
        /// </summary>
        public ProjectFundingSource(int projectFundingSourceID, string projectFundingSourceName, string projectFundingSourceDisplayName) : this()
        {
            this.ProjectFundingSourceID = projectFundingSourceID;
            this.ProjectFundingSourceName = projectFundingSourceName;
            this.ProjectFundingSourceDisplayName = projectFundingSourceDisplayName;
        }

        /// <summary>
        /// Constructor for building a new object with MinimalConstructor required fields in preparation for insert into database
        /// </summary>
        public ProjectFundingSource(string projectFundingSourceName, string projectFundingSourceDisplayName) : this()
        {
            // Mark this as a new object by setting primary key with special value
            this.ProjectFundingSourceID = ModelObjectHelpers.MakeNextUnsavedPrimaryKeyValue();
            
            this.ProjectFundingSourceName = projectFundingSourceName;
            this.ProjectFundingSourceDisplayName = projectFundingSourceDisplayName;
        }


        /// <summary>
        /// Creates a "blank" object of this type and populates primitives with defaults
        /// </summary>
        public static ProjectFundingSource CreateNewBlank()
        {
            return new ProjectFundingSource(default(string), default(string));
        }

        /// <summary>
        /// Does this object have any dependent objects? (If it does have dependent objects, these would need to be deleted before this object could be deleted.)
        /// </summary>
        /// <returns></returns>
        public bool HasDependentObjects()
        {
            return Projects.Any();
        }

        /// <summary>
        /// Active Dependent type names of this object
        /// </summary>
        public List<string> DependentObjectNames() 
        {
            var dependentObjects = new List<string>();
            
            if(Projects.Any())
            {
                dependentObjects.Add(typeof(Project).Name);
            }
            return dependentObjects.Distinct().ToList();
        }

        /// <summary>
        /// Dependent type names of this entity
        /// </summary>
        public static readonly List<string> DependentEntityTypeNames = new List<string> {typeof(ProjectFundingSource).Name, typeof(Project).Name};


        /// <summary>
        /// Delete just the entity 
        /// </summary>
        public void Delete(DatabaseEntities dbContext)
        {
            dbContext.ProjectFundingSources.Remove(this);
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

            foreach(var x in Projects.ToList())
            {
                x.DeleteFull(dbContext);
            }
        }

        [Key]
        public int ProjectFundingSourceID { get; set; }
        public string ProjectFundingSourceName { get; set; }
        public string ProjectFundingSourceDisplayName { get; set; }
        [NotMapped]
        public int PrimaryKey { get { return ProjectFundingSourceID; } set { ProjectFundingSourceID = value; } }

        public virtual ICollection<Project> Projects { get; set; }

        public static class FieldLengths
        {
            public const int ProjectFundingSourceName = 150;
            public const int ProjectFundingSourceDisplayName = 150;
        }
    }
}