//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[PersonStewardRegion]
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
    [Table("[dbo].[PersonStewardRegion]")]
    public partial class PersonStewardRegion : IHavePrimaryKey, ICanDeleteFull
    {
        /// <summary>
        /// Default Constructor; only used by EF
        /// </summary>
        protected PersonStewardRegion()
        {

        }

        /// <summary>
        /// Constructor for building a new object with MaximalConstructor required fields in preparation for insert into database
        /// </summary>
        public PersonStewardRegion(int personStewardRegionID, int personID, int regionID) : this()
        {
            this.PersonStewardRegionID = personStewardRegionID;
            this.PersonID = personID;
            this.RegionID = regionID;
        }

        /// <summary>
        /// Constructor for building a new object with MinimalConstructor required fields in preparation for insert into database
        /// </summary>
        public PersonStewardRegion(int personID, int regionID) : this()
        {
            // Mark this as a new object by setting primary key with special value
            this.PersonStewardRegionID = ModelObjectHelpers.MakeNextUnsavedPrimaryKeyValue();
            
            this.PersonID = personID;
            this.RegionID = regionID;
        }

        /// <summary>
        /// Constructor for building a new object with MinimalConstructor required fields, using objects whenever possible
        /// </summary>
        public PersonStewardRegion(Person person, Region region) : this()
        {
            // Mark this as a new object by setting primary key with special value
            this.PersonStewardRegionID = ModelObjectHelpers.MakeNextUnsavedPrimaryKeyValue();
            this.PersonID = person.PersonID;
            this.Person = person;
            person.PersonStewardRegions.Add(this);
            this.RegionID = region.RegionID;
            this.Region = region;
            region.PersonStewardRegions.Add(this);
        }

        /// <summary>
        /// Creates a "blank" object of this type and populates primitives with defaults
        /// </summary>
        public static PersonStewardRegion CreateNewBlank(Person person, Region region)
        {
            return new PersonStewardRegion(person, region);
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
        public static readonly List<string> DependentEntityTypeNames = new List<string> {typeof(PersonStewardRegion).Name};


        /// <summary>
        /// Dependent type names of this entity
        /// </summary>
        public void DeleteFull(DatabaseEntities dbContext)
        {
            
            dbContext.PersonStewardRegions.Remove(this);
        }

        [Key]
        public int PersonStewardRegionID { get; set; }
        public int PersonID { get; set; }
        public int RegionID { get; set; }
        [NotMapped]
        public int PrimaryKey { get { return PersonStewardRegionID; } set { PersonStewardRegionID = value; } }

        public virtual Person Person { get; set; }
        public virtual Region Region { get; set; }

        public static class FieldLengths
        {

        }
    }
}