//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[ProjectDocumentUpdate]
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
    // Table [dbo].[ProjectDocumentUpdate] is NOT multi-tenant, so is attributed as ICanDeleteFull
    [Table("[dbo].[ProjectDocumentUpdate]")]
    public partial class ProjectDocumentUpdate : IHavePrimaryKey, ICanDeleteFull
    {
        /// <summary>
        /// Default Constructor; only used by EF
        /// </summary>
        protected ProjectDocumentUpdate()
        {

        }

        /// <summary>
        /// Constructor for building a new object with MaximalConstructor required fields in preparation for insert into database
        /// </summary>
        public ProjectDocumentUpdate(int projectDocumentUpdateID, int projectUpdateBatchID, int fileResourceID, string displayName, string description, int? projectDocumentTypeID) : this()
        {
            this.ProjectDocumentUpdateID = projectDocumentUpdateID;
            this.ProjectUpdateBatchID = projectUpdateBatchID;
            this.FileResourceID = fileResourceID;
            this.DisplayName = displayName;
            this.Description = description;
            this.ProjectDocumentTypeID = projectDocumentTypeID;
        }

        /// <summary>
        /// Constructor for building a new object with MinimalConstructor required fields in preparation for insert into database
        /// </summary>
        public ProjectDocumentUpdate(int projectUpdateBatchID, int fileResourceID, string displayName) : this()
        {
            // Mark this as a new object by setting primary key with special value
            this.ProjectDocumentUpdateID = ModelObjectHelpers.MakeNextUnsavedPrimaryKeyValue();
            
            this.ProjectUpdateBatchID = projectUpdateBatchID;
            this.FileResourceID = fileResourceID;
            this.DisplayName = displayName;
        }

        /// <summary>
        /// Constructor for building a new object with MinimalConstructor required fields, using objects whenever possible
        /// </summary>
        public ProjectDocumentUpdate(ProjectUpdateBatch projectUpdateBatch, FileResource fileResource, string displayName) : this()
        {
            // Mark this as a new object by setting primary key with special value
            this.ProjectDocumentUpdateID = ModelObjectHelpers.MakeNextUnsavedPrimaryKeyValue();
            this.ProjectUpdateBatchID = projectUpdateBatch.ProjectUpdateBatchID;
            this.ProjectUpdateBatch = projectUpdateBatch;
            projectUpdateBatch.ProjectDocumentUpdates.Add(this);
            this.FileResourceID = fileResource.FileResourceID;
            this.FileResource = fileResource;
            fileResource.ProjectDocumentUpdates.Add(this);
            this.DisplayName = displayName;
        }

        /// <summary>
        /// Creates a "blank" object of this type and populates primitives with defaults
        /// </summary>
        public static ProjectDocumentUpdate CreateNewBlank(ProjectUpdateBatch projectUpdateBatch, FileResource fileResource)
        {
            return new ProjectDocumentUpdate(projectUpdateBatch, fileResource, default(string));
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
        public static readonly List<string> DependentEntityTypeNames = new List<string> {typeof(ProjectDocumentUpdate).Name};


        /// <summary>
        /// Delete just the entity 
        /// </summary>
        public void Delete(DatabaseEntities dbContext)
        {
            dbContext.ProjectDocumentUpdates.Remove(this);
        }
        
        /// <summary>
        /// Delete entity plus all children
        /// </summary>
        public void DeleteFull(DatabaseEntities dbContext)
        {
            
            Delete(dbContext);
        }

        [Key]
        public int ProjectDocumentUpdateID { get; set; }
        public int ProjectUpdateBatchID { get; set; }
        public int FileResourceID { get; set; }
        public string DisplayName { get; set; }
        public string Description { get; set; }
        public int? ProjectDocumentTypeID { get; set; }
        [NotMapped]
        public int PrimaryKey { get { return ProjectDocumentUpdateID; } set { ProjectDocumentUpdateID = value; } }

        public virtual ProjectUpdateBatch ProjectUpdateBatch { get; set; }
        public virtual FileResource FileResource { get; set; }
        public virtual ProjectDocumentType ProjectDocumentType { get; set; }

        public static class FieldLengths
        {
            public const int DisplayName = 200;
            public const int Description = 1000;
        }
    }
}