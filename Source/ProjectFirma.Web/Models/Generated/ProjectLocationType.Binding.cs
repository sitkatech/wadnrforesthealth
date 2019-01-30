//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[ProjectLocationType]
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
    // Table [dbo].[ProjectLocationType] is NOT multi-tenant, so is attributed as ICanDeleteFull
    [Table("[dbo].[ProjectLocationType]")]
    public partial class ProjectLocationType : IHavePrimaryKey, ICanDeleteFull
    {
        /// <summary>
        /// Default Constructor; only used by EF
        /// </summary>
        protected ProjectLocationType()
        {
            this.ProjectLocations = new HashSet<ProjectLocation>();
            this.ProjectLocationUpdates = new HashSet<ProjectLocationUpdate>();
        }

        /// <summary>
        /// Constructor for building a new object with MaximalConstructor required fields in preparation for insert into database
        /// </summary>
        public ProjectLocationType(int projectLocationTypeID, string projectLocationTypeName, string projectLocationTypeDisplayName) : this()
        {
            this.ProjectLocationTypeID = projectLocationTypeID;
            this.ProjectLocationTypeName = projectLocationTypeName;
            this.ProjectLocationTypeDisplayName = projectLocationTypeDisplayName;
        }



        /// <summary>
        /// Creates a "blank" object of this type and populates primitives with defaults
        /// </summary>
        public static ProjectLocationType CreateNewBlank()
        {
            return new ProjectLocationType();
        }

        /// <summary>
        /// Does this object have any dependent objects? (If it does have dependent objects, these would need to be deleted before this object could be deleted.)
        /// </summary>
        /// <returns></returns>
        public bool HasDependentObjects()
        {
            return ProjectLocations.Any() || ProjectLocationUpdates.Any();
        }

        /// <summary>
        /// Dependent type names of this entity
        /// </summary>
        public static readonly List<string> DependentEntityTypeNames = new List<string> {typeof(ProjectLocationType).Name, typeof(ProjectLocation).Name, typeof(ProjectLocationUpdate).Name};


        /// <summary>
        /// Dependent type names of this entity
        /// </summary>
        public void DeleteFull(DatabaseEntities dbContext)
        {
            DeleteChildren(dbContext);
            dbContext.ProjectLocationTypes.Remove(this);
        }
        /// <summary>
        /// Dependent type names of this entity
        /// </summary>
        public void DeleteChildren(DatabaseEntities dbContext)
        {

            foreach(var x in ProjectLocations.ToList())
            {
                x.DeleteFull(dbContext);
            }

            foreach(var x in ProjectLocationUpdates.ToList())
            {
                x.DeleteFull(dbContext);
            }
        }

        [Key]
        public int ProjectLocationTypeID { get; set; }
        public string ProjectLocationTypeName { get; set; }
        public string ProjectLocationTypeDisplayName { get; set; }
        [NotMapped]
        public int PrimaryKey { get { return ProjectLocationTypeID; } set { ProjectLocationTypeID = value; } }

        public virtual ICollection<ProjectLocation> ProjectLocations { get; set; }
        public virtual ICollection<ProjectLocationUpdate> ProjectLocationUpdates { get; set; }

        public static class FieldLengths
        {
            public const int ProjectLocationTypeName = 50;
            public const int ProjectLocationTypeDisplayName = 50;
        }
    }
}