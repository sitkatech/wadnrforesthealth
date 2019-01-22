//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[ProjectCode]
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
    // Table [dbo].[ProjectCode] is NOT multi-tenant, so is attributed as ICanDeleteFull
    [Table("[dbo].[ProjectCode]")]
    public partial class ProjectCode : IHavePrimaryKey, ICanDeleteFull
    {
        /// <summary>
        /// Default Constructor; only used by EF
        /// </summary>
        protected ProjectCode()
        {
            this.GrantAllocationProjectCodes = new HashSet<GrantAllocationProjectCode>();
        }

        /// <summary>
        /// Constructor for building a new object with MaximalConstructor required fields in preparation for insert into database
        /// </summary>
        public ProjectCode(int projectCodeID, string projectCodeAbbrev) : this()
        {
            this.ProjectCodeID = projectCodeID;
            this.ProjectCodeAbbrev = projectCodeAbbrev;
        }

        /// <summary>
        /// Constructor for building a new object with MinimalConstructor required fields in preparation for insert into database
        /// </summary>
        public ProjectCode(string projectCodeAbbrev) : this()
        {
            // Mark this as a new object by setting primary key with special value
            this.ProjectCodeID = ModelObjectHelpers.MakeNextUnsavedPrimaryKeyValue();
            
            this.ProjectCodeAbbrev = projectCodeAbbrev;
        }


        /// <summary>
        /// Creates a "blank" object of this type and populates primitives with defaults
        /// </summary>
        public static ProjectCode CreateNewBlank()
        {
            return new ProjectCode(default(string));
        }

        /// <summary>
        /// Does this object have any dependent objects? (If it does have dependent objects, these would need to be deleted before this object could be deleted.)
        /// </summary>
        /// <returns></returns>
        public bool HasDependentObjects()
        {
            return GrantAllocationProjectCodes.Any();
        }

        /// <summary>
        /// Dependent type names of this entity
        /// </summary>
        public static readonly List<string> DependentEntityTypeNames = new List<string> {typeof(ProjectCode).Name, typeof(GrantAllocationProjectCode).Name};


        /// <summary>
        /// Dependent type names of this entity
        /// </summary>
        public void DeleteFull(DatabaseEntities dbContext)
        {
            DeleteChildren(dbContext);
            dbContext.ProjectCodes.Remove(this);
        }
        /// <summary>
        /// Dependent type names of this entity
        /// </summary>
        public void DeleteChildren(DatabaseEntities dbContext)
        {

            foreach(var x in GrantAllocationProjectCodes.ToList())
            {
                x.DeleteFull(dbContext);
            }
        }

        [Key]
        public int ProjectCodeID { get; set; }
        public string ProjectCodeAbbrev { get; set; }
        [NotMapped]
        public int PrimaryKey { get { return ProjectCodeID; } set { ProjectCodeID = value; } }

        public virtual ICollection<GrantAllocationProjectCode> GrantAllocationProjectCodes { get; set; }

        public static class FieldLengths
        {
            public const int ProjectCodeAbbrev = 100;
        }
    }
}