//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[ProjectTypeProjectCustomAttributeType]
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
    // Table [dbo].[ProjectTypeProjectCustomAttributeType] is NOT multi-tenant, so is attributed as ICanDeleteFull
    [Table("[dbo].[ProjectTypeProjectCustomAttributeType]")]
    public partial class ProjectTypeProjectCustomAttributeType : IHavePrimaryKey, ICanDeleteFull
    {
        /// <summary>
        /// Default Constructor; only used by EF
        /// </summary>
        protected ProjectTypeProjectCustomAttributeType()
        {

        }

        /// <summary>
        /// Constructor for building a new object with MaximalConstructor required fields in preparation for insert into database
        /// </summary>
        public ProjectTypeProjectCustomAttributeType(int projectTypeProjectCustomAttributeTypeID, int projectTypeID, int projectCustomAttributeTypeID) : this()
        {
            this.ProjectTypeProjectCustomAttributeTypeID = projectTypeProjectCustomAttributeTypeID;
            this.ProjectTypeID = projectTypeID;
            this.ProjectCustomAttributeTypeID = projectCustomAttributeTypeID;
        }

        /// <summary>
        /// Constructor for building a new object with MinimalConstructor required fields in preparation for insert into database
        /// </summary>
        public ProjectTypeProjectCustomAttributeType(int projectTypeID, int projectCustomAttributeTypeID) : this()
        {
            // Mark this as a new object by setting primary key with special value
            this.ProjectTypeProjectCustomAttributeTypeID = ModelObjectHelpers.MakeNextUnsavedPrimaryKeyValue();
            
            this.ProjectTypeID = projectTypeID;
            this.ProjectCustomAttributeTypeID = projectCustomAttributeTypeID;
        }

        /// <summary>
        /// Constructor for building a new object with MinimalConstructor required fields, using objects whenever possible
        /// </summary>
        public ProjectTypeProjectCustomAttributeType(ProjectType projectType, ProjectCustomAttributeType projectCustomAttributeType) : this()
        {
            // Mark this as a new object by setting primary key with special value
            this.ProjectTypeProjectCustomAttributeTypeID = ModelObjectHelpers.MakeNextUnsavedPrimaryKeyValue();
            this.ProjectTypeID = projectType.ProjectTypeID;
            this.ProjectType = projectType;
            projectType.ProjectTypeProjectCustomAttributeTypes.Add(this);
            this.ProjectCustomAttributeTypeID = projectCustomAttributeType.ProjectCustomAttributeTypeID;
            this.ProjectCustomAttributeType = projectCustomAttributeType;
            projectCustomAttributeType.ProjectTypeProjectCustomAttributeTypes.Add(this);
        }

        /// <summary>
        /// Creates a "blank" object of this type and populates primitives with defaults
        /// </summary>
        public static ProjectTypeProjectCustomAttributeType CreateNewBlank(ProjectType projectType, ProjectCustomAttributeType projectCustomAttributeType)
        {
            return new ProjectTypeProjectCustomAttributeType(projectType, projectCustomAttributeType);
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
        public static readonly List<string> DependentEntityTypeNames = new List<string> {typeof(ProjectTypeProjectCustomAttributeType).Name};


        /// <summary>
        /// Dependent type names of this entity
        /// </summary>
        public void DeleteFull(DatabaseEntities dbContext)
        {
            
            dbContext.ProjectTypeProjectCustomAttributeTypes.Remove(this);
        }

        [Key]
        public int ProjectTypeProjectCustomAttributeTypeID { get; set; }
        public int ProjectTypeID { get; set; }
        public int ProjectCustomAttributeTypeID { get; set; }
        [NotMapped]
        public int PrimaryKey { get { return ProjectTypeProjectCustomAttributeTypeID; } set { ProjectTypeProjectCustomAttributeTypeID = value; } }

        public virtual ProjectType ProjectType { get; set; }
        public virtual ProjectCustomAttributeType ProjectCustomAttributeType { get; set; }

        public static class FieldLengths
        {

        }
    }
}