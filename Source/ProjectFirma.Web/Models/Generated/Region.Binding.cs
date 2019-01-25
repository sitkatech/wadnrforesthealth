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
    // Table [dbo].[Region] is NOT multi-tenant, so is attributed as ICanDeleteFull
    [Table("[dbo].[Region]")]
    public partial class Region : IHavePrimaryKey, ICanDeleteFull
    {
        /// <summary>
        /// Default Constructor; only used by EF
        /// </summary>
        protected Region()
        {
            this.Agreements = new HashSet<Agreement>();
            this.FocusAreas = new HashSet<FocusArea>();
            this.GrantAllocations = new HashSet<GrantAllocation>();
            this.PersonStewardRegions = new HashSet<PersonStewardRegion>();
            this.ProjectRegions = new HashSet<ProjectRegion>();
            this.ProjectRegionUpdates = new HashSet<ProjectRegionUpdate>();
        }

        /// <summary>
        /// Constructor for building a new object with MaximalConstructor required fields in preparation for insert into database
        /// </summary>
        public Region(int regionID, string regionName, DbGeometry regionLocation, string regionAbbrev) : this()
        {
            this.RegionID = regionID;
            this.RegionName = regionName;
            this.RegionLocation = regionLocation;
            this.RegionAbbrev = regionAbbrev;
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
            return Agreements.Any() || FocusAreas.Any() || GrantAllocations.Any() || PersonStewardRegions.Any() || ProjectRegions.Any() || ProjectRegionUpdates.Any();
        }

        /// <summary>
        /// Dependent type names of this entity
        /// </summary>
        public static readonly List<string> DependentEntityTypeNames = new List<string> {typeof(Region).Name, typeof(Agreement).Name, typeof(FocusArea).Name, typeof(GrantAllocation).Name, typeof(PersonStewardRegion).Name, typeof(ProjectRegion).Name, typeof(ProjectRegionUpdate).Name};


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

            foreach(var x in Agreements.ToList())
            {
                x.DeleteFull(dbContext);
            }

            foreach(var x in FocusAreas.ToList())
            {
                x.DeleteFull(dbContext);
            }

            foreach(var x in GrantAllocations.ToList())
            {
                x.DeleteFull(dbContext);
            }

            foreach(var x in PersonStewardRegions.ToList())
            {
                x.DeleteFull(dbContext);
            }

            foreach(var x in ProjectRegions.ToList())
            {
                x.DeleteFull(dbContext);
            }

            foreach(var x in ProjectRegionUpdates.ToList())
            {
                x.DeleteFull(dbContext);
            }
        }

        [Key]
        public int RegionID { get; set; }
        public string RegionName { get; set; }
        public DbGeometry RegionLocation { get; set; }
        public string RegionAbbrev { get; set; }
        [NotMapped]
        public int PrimaryKey { get { return RegionID; } set { RegionID = value; } }

        public virtual ICollection<Agreement> Agreements { get; set; }
        public virtual ICollection<FocusArea> FocusAreas { get; set; }
        public virtual ICollection<GrantAllocation> GrantAllocations { get; set; }
        public virtual ICollection<PersonStewardRegion> PersonStewardRegions { get; set; }
        public virtual ICollection<ProjectRegion> ProjectRegions { get; set; }
        public virtual ICollection<ProjectRegionUpdate> ProjectRegionUpdates { get; set; }

        public static class FieldLengths
        {
            public const int RegionName = 100;
            public const int RegionAbbrev = 10;
        }
    }
}