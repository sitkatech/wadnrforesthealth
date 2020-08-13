//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[WashingtonCounty]
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
    // Table [dbo].[WashingtonCounty] is NOT multi-tenant, so is attributed as ICanDeleteFull
    [Table("[dbo].[WashingtonCounty]")]
    public partial class WashingtonCounty : IHavePrimaryKey, ICanDeleteFull
    {
        /// <summary>
        /// Default Constructor; only used by EF
        /// </summary>
        protected WashingtonCounty()
        {

        }

        /// <summary>
        /// Constructor for building a new object with MaximalConstructor required fields in preparation for insert into database
        /// </summary>
        public WashingtonCounty(int washingtonCountyID, DbGeometry washingtonCountyLocation, string washingtonCountyName, string washingtonCountyFullName) : this()
        {
            this.WashingtonCountyID = washingtonCountyID;
            this.WashingtonCountyLocation = washingtonCountyLocation;
            this.WashingtonCountyName = washingtonCountyName;
            this.WashingtonCountyFullName = washingtonCountyFullName;
        }

        /// <summary>
        /// Constructor for building a new object with MinimalConstructor required fields in preparation for insert into database
        /// </summary>
        public WashingtonCounty(DbGeometry washingtonCountyLocation, string washingtonCountyName, string washingtonCountyFullName) : this()
        {
            // Mark this as a new object by setting primary key with special value
            this.WashingtonCountyID = ModelObjectHelpers.MakeNextUnsavedPrimaryKeyValue();
            
            this.WashingtonCountyLocation = washingtonCountyLocation;
            this.WashingtonCountyName = washingtonCountyName;
            this.WashingtonCountyFullName = washingtonCountyFullName;
        }


        /// <summary>
        /// Creates a "blank" object of this type and populates primitives with defaults
        /// </summary>
        public static WashingtonCounty CreateNewBlank()
        {
            return new WashingtonCounty(default(DbGeometry), default(string), default(string));
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
        public static readonly List<string> DependentEntityTypeNames = new List<string> {typeof(WashingtonCounty).Name};


        /// <summary>
        /// Delete just the entity 
        /// </summary>
        public void Delete(DatabaseEntities dbContext)
        {
            dbContext.WashingtonCounties.Remove(this);
        }
        
        /// <summary>
        /// Delete entity plus all children
        /// </summary>
        public void DeleteFull(DatabaseEntities dbContext)
        {
            
            Delete(dbContext);
        }

        [Key]
        public int WashingtonCountyID { get; set; }
        public DbGeometry WashingtonCountyLocation { get; set; }
        public string WashingtonCountyName { get; set; }
        public string WashingtonCountyFullName { get; set; }
        [NotMapped]
        public int PrimaryKey { get { return WashingtonCountyID; } set { WashingtonCountyID = value; } }



        public static class FieldLengths
        {
            public const int WashingtonCountyName = 200;
            public const int WashingtonCountyFullName = 200;
        }
    }
}