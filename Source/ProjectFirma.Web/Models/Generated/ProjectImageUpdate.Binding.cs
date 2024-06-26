//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[ProjectImageUpdate]
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
    // Table [dbo].[ProjectImageUpdate] is NOT multi-tenant, so is attributed as ICanDeleteFull
    [Table("[dbo].[ProjectImageUpdate]")]
    public partial class ProjectImageUpdate : IHavePrimaryKey, ICanDeleteFull
    {
        /// <summary>
        /// Default Constructor; only used by EF
        /// </summary>
        protected ProjectImageUpdate()
        {

        }

        /// <summary>
        /// Constructor for building a new object with MaximalConstructor required fields in preparation for insert into database
        /// </summary>
        public ProjectImageUpdate(int projectImageUpdateID, int? fileResourceID, int projectUpdateBatchID, int? projectImageTimingID, string caption, string credit, bool isKeyPhoto, bool excludeFromFactSheet, int? projectImageID) : this()
        {
            this.ProjectImageUpdateID = projectImageUpdateID;
            this.FileResourceID = fileResourceID;
            this.ProjectUpdateBatchID = projectUpdateBatchID;
            this.ProjectImageTimingID = projectImageTimingID;
            this.Caption = caption;
            this.Credit = credit;
            this.IsKeyPhoto = isKeyPhoto;
            this.ExcludeFromFactSheet = excludeFromFactSheet;
            this.ProjectImageID = projectImageID;
        }

        /// <summary>
        /// Constructor for building a new object with MinimalConstructor required fields in preparation for insert into database
        /// </summary>
        public ProjectImageUpdate(int projectUpdateBatchID, string caption, string credit, bool isKeyPhoto, bool excludeFromFactSheet) : this()
        {
            // Mark this as a new object by setting primary key with special value
            this.ProjectImageUpdateID = ModelObjectHelpers.MakeNextUnsavedPrimaryKeyValue();
            
            this.ProjectUpdateBatchID = projectUpdateBatchID;
            this.Caption = caption;
            this.Credit = credit;
            this.IsKeyPhoto = isKeyPhoto;
            this.ExcludeFromFactSheet = excludeFromFactSheet;
        }

        /// <summary>
        /// Constructor for building a new object with MinimalConstructor required fields, using objects whenever possible
        /// </summary>
        public ProjectImageUpdate(ProjectUpdateBatch projectUpdateBatch, string caption, string credit, bool isKeyPhoto, bool excludeFromFactSheet) : this()
        {
            // Mark this as a new object by setting primary key with special value
            this.ProjectImageUpdateID = ModelObjectHelpers.MakeNextUnsavedPrimaryKeyValue();
            this.ProjectUpdateBatchID = projectUpdateBatch.ProjectUpdateBatchID;
            this.ProjectUpdateBatch = projectUpdateBatch;
            projectUpdateBatch.ProjectImageUpdates.Add(this);
            this.Caption = caption;
            this.Credit = credit;
            this.IsKeyPhoto = isKeyPhoto;
            this.ExcludeFromFactSheet = excludeFromFactSheet;
        }

        /// <summary>
        /// Creates a "blank" object of this type and populates primitives with defaults
        /// </summary>
        public static ProjectImageUpdate CreateNewBlank(ProjectUpdateBatch projectUpdateBatch)
        {
            return new ProjectImageUpdate(projectUpdateBatch, default(string), default(string), default(bool), default(bool));
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
        public static readonly List<string> DependentEntityTypeNames = new List<string> {typeof(ProjectImageUpdate).Name};


        /// <summary>
        /// Delete just the entity 
        /// </summary>
        public void Delete(DatabaseEntities dbContext)
        {
            dbContext.ProjectImageUpdates.Remove(this);
        }
        
        /// <summary>
        /// Delete entity plus all children
        /// </summary>
        public void DeleteFull(DatabaseEntities dbContext)
        {
            
            Delete(dbContext);
        }

        [Key]
        public int ProjectImageUpdateID { get; set; }
        public int? FileResourceID { get; set; }
        public int ProjectUpdateBatchID { get; set; }
        public int? ProjectImageTimingID { get; set; }
        public string Caption { get; set; }
        public string Credit { get; set; }
        public bool IsKeyPhoto { get; set; }
        public bool ExcludeFromFactSheet { get; set; }
        public int? ProjectImageID { get; set; }
        [NotMapped]
        public int PrimaryKey { get { return ProjectImageUpdateID; } set { ProjectImageUpdateID = value; } }

        public virtual FileResource FileResource { get; set; }
        public virtual ProjectUpdateBatch ProjectUpdateBatch { get; set; }
        public ProjectImageTiming ProjectImageTiming { get { return ProjectImageTimingID.HasValue ? ProjectImageTiming.AllLookupDictionary[ProjectImageTimingID.Value] : null; } }
        public virtual ProjectImage ProjectImage { get; set; }

        public static class FieldLengths
        {
            public const int Caption = 200;
            public const int Credit = 200;
        }
    }
}