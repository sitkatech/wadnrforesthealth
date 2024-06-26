//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[ProjectImage]
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
    // Table [dbo].[ProjectImage] is NOT multi-tenant, so is attributed as ICanDeleteFull
    [Table("[dbo].[ProjectImage]")]
    public partial class ProjectImage : IHavePrimaryKey, ICanDeleteFull
    {
        /// <summary>
        /// Default Constructor; only used by EF
        /// </summary>
        protected ProjectImage()
        {
            this.ProjectImageUpdates = new HashSet<ProjectImageUpdate>();
        }

        /// <summary>
        /// Constructor for building a new object with MaximalConstructor required fields in preparation for insert into database
        /// </summary>
        public ProjectImage(int projectImageID, int fileResourceID, int projectID, int? projectImageTimingID, string caption, string credit, bool isKeyPhoto, bool excludeFromFactSheet) : this()
        {
            this.ProjectImageID = projectImageID;
            this.FileResourceID = fileResourceID;
            this.ProjectID = projectID;
            this.ProjectImageTimingID = projectImageTimingID;
            this.Caption = caption;
            this.Credit = credit;
            this.IsKeyPhoto = isKeyPhoto;
            this.ExcludeFromFactSheet = excludeFromFactSheet;
        }

        /// <summary>
        /// Constructor for building a new object with MinimalConstructor required fields in preparation for insert into database
        /// </summary>
        public ProjectImage(int fileResourceID, int projectID, string caption, string credit, bool isKeyPhoto, bool excludeFromFactSheet) : this()
        {
            // Mark this as a new object by setting primary key with special value
            this.ProjectImageID = ModelObjectHelpers.MakeNextUnsavedPrimaryKeyValue();
            
            this.FileResourceID = fileResourceID;
            this.ProjectID = projectID;
            this.Caption = caption;
            this.Credit = credit;
            this.IsKeyPhoto = isKeyPhoto;
            this.ExcludeFromFactSheet = excludeFromFactSheet;
        }

        /// <summary>
        /// Constructor for building a new object with MinimalConstructor required fields, using objects whenever possible
        /// </summary>
        public ProjectImage(FileResource fileResource, Project project, string caption, string credit, bool isKeyPhoto, bool excludeFromFactSheet) : this()
        {
            // Mark this as a new object by setting primary key with special value
            this.ProjectImageID = ModelObjectHelpers.MakeNextUnsavedPrimaryKeyValue();
            this.FileResourceID = fileResource.FileResourceID;
            this.FileResource = fileResource;
            fileResource.ProjectImages.Add(this);
            this.ProjectID = project.ProjectID;
            this.Project = project;
            project.ProjectImages.Add(this);
            this.Caption = caption;
            this.Credit = credit;
            this.IsKeyPhoto = isKeyPhoto;
            this.ExcludeFromFactSheet = excludeFromFactSheet;
        }

        /// <summary>
        /// Creates a "blank" object of this type and populates primitives with defaults
        /// </summary>
        public static ProjectImage CreateNewBlank(FileResource fileResource, Project project)
        {
            return new ProjectImage(fileResource, project, default(string), default(string), default(bool), default(bool));
        }

        /// <summary>
        /// Does this object have any dependent objects? (If it does have dependent objects, these would need to be deleted before this object could be deleted.)
        /// </summary>
        /// <returns></returns>
        public bool HasDependentObjects()
        {
            return ProjectImageUpdates.Any();
        }

        /// <summary>
        /// Active Dependent type names of this object
        /// </summary>
        public List<string> DependentObjectNames() 
        {
            var dependentObjects = new List<string>();
            
            if(ProjectImageUpdates.Any())
            {
                dependentObjects.Add(typeof(ProjectImageUpdate).Name);
            }
            return dependentObjects.Distinct().ToList();
        }

        /// <summary>
        /// Dependent type names of this entity
        /// </summary>
        public static readonly List<string> DependentEntityTypeNames = new List<string> {typeof(ProjectImage).Name, typeof(ProjectImageUpdate).Name};


        /// <summary>
        /// Delete just the entity 
        /// </summary>
        public void Delete(DatabaseEntities dbContext)
        {
            dbContext.ProjectImages.Remove(this);
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

            foreach(var x in ProjectImageUpdates.ToList())
            {
                x.DeleteFull(dbContext);
            }
        }

        [Key]
        public int ProjectImageID { get; set; }
        public int FileResourceID { get; set; }
        public int ProjectID { get; set; }
        public int? ProjectImageTimingID { get; set; }
        public string Caption { get; set; }
        public string Credit { get; set; }
        public bool IsKeyPhoto { get; set; }
        public bool ExcludeFromFactSheet { get; set; }
        [NotMapped]
        public int PrimaryKey { get { return ProjectImageID; } set { ProjectImageID = value; } }

        public virtual ICollection<ProjectImageUpdate> ProjectImageUpdates { get; set; }
        public virtual FileResource FileResource { get; set; }
        public virtual Project Project { get; set; }
        public ProjectImageTiming ProjectImageTiming { get { return ProjectImageTimingID.HasValue ? ProjectImageTiming.AllLookupDictionary[ProjectImageTimingID.Value] : null; } }

        public static class FieldLengths
        {
            public const int Caption = 200;
            public const int Credit = 200;
        }
    }
}