//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[Region]
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
    [Table("[dbo].[Region]")]
    public partial class Region : IHavePrimaryKey, ICanDeleteFull
    {
        /// <summary>
        /// Default Constructor; only used by EF
        /// </summary>
        protected Region()
        {
            this.ProjectRegions = new HashSet<ProjectRegion>();
        }

        /// <summary>
        /// Constructor for building a new object with MaximalConstructor required fields in preparation for insert into database
        /// </summary>
        public Region(int regionID, string regionName, DbGeometry regionLocation) : this()
        {
            this.RegionID = regionID;
            this.RegionName = regionName;
            this.RegionLocation = regionLocation;
        }

        /// <summary>
        /// Constructor for building a new object with MinimalConstructor required fields in preparation for insert into database
        /// </summary>
        public Region(string regionName) : this()
        {
            // Mark this as a new object by setting primary key with special value
            this.RegionID = ModelObjectHelpers.MakeNextUnsavedPrimaryKeyValue();
            
            this.RegionName = regionName;
        }


        /// <summary>
        /// Creates a "blank" object of this type and populates primitives with defaults
        /// </summary>
        public static Region CreateNewBlank()
        {
            return new Region(default(string));
        }

        /// <summary>
        /// Does this object have any dependent objects? (If it does have dependent objects, these would need to be deleted before this object could be deleted.)
        /// </summary>
        /// <returns></returns>
        public bool HasDependentObjects()
        {
            return ProjectRegions.Any();
        }

        /// <summary>
        /// Dependent type names of this entity
        /// </summary>
        public static readonly List<string> DependentEntityTypeNames = new List<string> {typeof(Region).Name, typeof(ProjectRegion).Name};


        /// <summary>
        /// Dependent type names of this entity
        /// </summary>
        public void DeleteFull(DatabaseEntities dbContext)
        {
            DeleteChildren(dbContext);
            dbContext.Regions.Remove(this);
        }
        /// <summary>
        /// Dependent type names of this entity
        /// </summary>
        public void DeleteChildren(DatabaseEntities dbContext)
        {

            foreach(var x in ProjectRegions.ToList())
            {
                x.DeleteFull(dbContext);
            }
        }

        [Key]
        public int RegionID { get; set; }
        public string RegionName { get; set; }
        public DbGeometry RegionLocation { get; set; }
        [NotMapped]
        public int PrimaryKey { get { return RegionID; } set { RegionID = value; } }

        public virtual ICollection<ProjectRegion> ProjectRegions { get; set; }

        public static class FieldLengths
        {
            public const int RegionName = 200;
        }
    }
}