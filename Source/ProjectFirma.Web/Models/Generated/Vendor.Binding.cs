//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[Vendor]
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
    // Table [dbo].[Vendor] is NOT multi-tenant, so is attributed as ICanDeleteFull
    [Table("[dbo].[Vendor]")]
    public partial class Vendor : IHavePrimaryKey, ICanDeleteFull
    {
        /// <summary>
        /// Default Constructor; only used by EF
        /// </summary>
        protected Vendor()
        {
            this.Organizations = new HashSet<Organization>();
            this.People = new HashSet<Person>();
        }

        /// <summary>
        /// Constructor for building a new object with MaximalConstructor required fields in preparation for insert into database
        /// </summary>
        public Vendor(int vendorID, string vendorName, string statewideVendorNumber, string vendorSuffix) : this()
        {
            this.VendorID = vendorID;
            this.VendorName = vendorName;
            this.StatewideVendorNumber = statewideVendorNumber;
            this.VendorSuffix = vendorSuffix;
        }

        /// <summary>
        /// Constructor for building a new object with MinimalConstructor required fields in preparation for insert into database
        /// </summary>
        public Vendor(string vendorName, string statewideVendorNumber, string vendorSuffix) : this()
        {
            // Mark this as a new object by setting primary key with special value
            this.VendorID = ModelObjectHelpers.MakeNextUnsavedPrimaryKeyValue();
            
            this.VendorName = vendorName;
            this.StatewideVendorNumber = statewideVendorNumber;
            this.VendorSuffix = vendorSuffix;
        }


        /// <summary>
        /// Creates a "blank" object of this type and populates primitives with defaults
        /// </summary>
        public static Vendor CreateNewBlank()
        {
            return new Vendor(default(string), default(string), default(string));
        }

        /// <summary>
        /// Does this object have any dependent objects? (If it does have dependent objects, these would need to be deleted before this object could be deleted.)
        /// </summary>
        /// <returns></returns>
        public bool HasDependentObjects()
        {
            return Organizations.Any() || People.Any();
        }

        /// <summary>
        /// Dependent type names of this entity
        /// </summary>
        public static readonly List<string> DependentEntityTypeNames = new List<string> {typeof(Vendor).Name, typeof(Organization).Name, typeof(Person).Name};


        /// <summary>
        /// Delete just the entity 
        /// </summary>
        public void Delete(DatabaseEntities dbContext)
        {
            dbContext.Vendors.Remove(this);
        }
        
        /// <summary>
        /// Delete entity plus all children
        /// </summary>
        public void DeleteFull(DatabaseEntities dbContext)
        {
            DeleteChildren(dbContext);
            Delete(dbContext);
        }
        /// <summary>
        /// Dependent type names of this entity
        /// </summary>
        public void DeleteChildren(DatabaseEntities dbContext)
        {

            foreach(var x in Organizations.ToList())
            {
                x.DeleteFull(dbContext);
            }

            foreach(var x in People.ToList())
            {
                x.DeleteFull(dbContext);
            }
        }

        [Key]
        public int VendorID { get; set; }
        public string VendorName { get; set; }
        public string StatewideVendorNumber { get; set; }
        public string VendorSuffix { get; set; }
        [NotMapped]
        public int PrimaryKey { get { return VendorID; } set { VendorID = value; } }

        public virtual ICollection<Organization> Organizations { get; set; }
        public virtual ICollection<Person> People { get; set; }

        public static class FieldLengths
        {
            public const int VendorName = 100;
            public const int StatewideVendorNumber = 20;
            public const int VendorSuffix = 10;
        }
    }
}