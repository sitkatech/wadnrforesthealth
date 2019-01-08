//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[FocusArea]
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
    [Table("[dbo].[FocusArea]")]
    public partial class FocusArea : IHavePrimaryKey, IHaveATenantID
    {
        /// <summary>
        /// Default Constructor; only used by EF
        /// </summary>
        protected FocusArea()
        {
            this.Projects = new HashSet<Project>();
            this.TenantID = HttpRequestStorage.Tenant.TenantID;
        }

        /// <summary>
        /// Constructor for building a new object with MaximalConstructor required fields in preparation for insert into database
        /// </summary>
        public FocusArea(int focusAreaID, string focusAreaName, int focusAreaStatusID, DbGeometry focusAreaLocation) : this()
        {
            this.FocusAreaID = focusAreaID;
            this.FocusAreaName = focusAreaName;
            this.FocusAreaStatusID = focusAreaStatusID;
            this.FocusAreaLocation = focusAreaLocation;
        }

        /// <summary>
        /// Constructor for building a new object with MinimalConstructor required fields in preparation for insert into database
        /// </summary>
        public FocusArea(string focusAreaName, int focusAreaStatusID) : this()
        {
            // Mark this as a new object by setting primary key with special value
            this.FocusAreaID = ModelObjectHelpers.MakeNextUnsavedPrimaryKeyValue();
            
            this.FocusAreaName = focusAreaName;
            this.FocusAreaStatusID = focusAreaStatusID;
        }

        /// <summary>
        /// Constructor for building a new object with MinimalConstructor required fields, using objects whenever possible
        /// </summary>
        public FocusArea(string focusAreaName, FocusAreaStatus focusAreaStatus) : this()
        {
            // Mark this as a new object by setting primary key with special value
            this.FocusAreaID = ModelObjectHelpers.MakeNextUnsavedPrimaryKeyValue();
            this.FocusAreaName = focusAreaName;
            this.FocusAreaStatusID = focusAreaStatus.FocusAreaStatusID;
        }

        /// <summary>
        /// Creates a "blank" object of this type and populates primitives with defaults
        /// </summary>
        public static FocusArea CreateNewBlank(FocusAreaStatus focusAreaStatus)
        {
            return new FocusArea(default(string), focusAreaStatus);
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
        /// Dependent type names of this entity
        /// </summary>
        public static readonly List<string> DependentEntityTypeNames = new List<string> {typeof(FocusArea).Name, typeof(Project).Name};


        /// <summary>
        /// Dependent type names of this entity
        /// </summary>
        public void DeleteFull(DatabaseEntities dbContext)
        {
            DeleteChildren(dbContext);
            dbContext.AllFocusAreas.Remove(this);
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
        public int FocusAreaID { get; set; }
        public int TenantID { get; private set; }
        public string FocusAreaName { get; set; }
        public int FocusAreaStatusID { get; set; }
        public DbGeometry FocusAreaLocation { get; set; }
        [NotMapped]
        public int PrimaryKey { get { return FocusAreaID; } set { FocusAreaID = value; } }

        public virtual ICollection<Project> Projects { get; set; }
        public Tenant Tenant { get { return Tenant.AllLookupDictionary[TenantID]; } }
        public FocusAreaStatus FocusAreaStatus { get { return FocusAreaStatus.AllLookupDictionary[FocusAreaStatusID]; } }

        public static class FieldLengths
        {
            public const int FocusAreaName = 200;
        }
    }
}