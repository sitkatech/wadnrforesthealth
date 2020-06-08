//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[GisUploadSourceOrganization]
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
    // Table [dbo].[GisUploadSourceOrganization] is NOT multi-tenant, so is attributed as ICanDeleteFull
    [Table("[dbo].[GisUploadSourceOrganization]")]
    public partial class GisUploadSourceOrganization : IHavePrimaryKey, ICanDeleteFull
    {
        /// <summary>
        /// Default Constructor; only used by EF
        /// </summary>
        protected GisUploadSourceOrganization()
        {
            this.GisUploadAttempts = new HashSet<GisUploadAttempt>();
        }

        /// <summary>
        /// Constructor for building a new object with MaximalConstructor required fields in preparation for insert into database
        /// </summary>
        public GisUploadSourceOrganization(int gisUploadSourceOrganizationID, string gisUploadSourceOrganizationName) : this()
        {
            this.GisUploadSourceOrganizationID = gisUploadSourceOrganizationID;
            this.GisUploadSourceOrganizationName = gisUploadSourceOrganizationName;
        }

        /// <summary>
        /// Constructor for building a new object with MinimalConstructor required fields in preparation for insert into database
        /// </summary>
        public GisUploadSourceOrganization(string gisUploadSourceOrganizationName) : this()
        {
            // Mark this as a new object by setting primary key with special value
            this.GisUploadSourceOrganizationID = ModelObjectHelpers.MakeNextUnsavedPrimaryKeyValue();
            
            this.GisUploadSourceOrganizationName = gisUploadSourceOrganizationName;
        }


        /// <summary>
        /// Creates a "blank" object of this type and populates primitives with defaults
        /// </summary>
        public static GisUploadSourceOrganization CreateNewBlank()
        {
            return new GisUploadSourceOrganization(default(string));
        }

        /// <summary>
        /// Does this object have any dependent objects? (If it does have dependent objects, these would need to be deleted before this object could be deleted.)
        /// </summary>
        /// <returns></returns>
        public bool HasDependentObjects()
        {
            return GisUploadAttempts.Any();
        }

        /// <summary>
        /// Active Dependent type names of this object
        /// </summary>
        public List<string> DependentObjectNames() 
        {
            var dependentObjects = new List<string>();
            
            if(GisUploadAttempts.Any())
            {
                dependentObjects.Add(typeof(GisUploadAttempt).Name);
            }
            return dependentObjects.Distinct().ToList();
        }

        /// <summary>
        /// Dependent type names of this entity
        /// </summary>
        public static readonly List<string> DependentEntityTypeNames = new List<string> {typeof(GisUploadSourceOrganization).Name, typeof(GisUploadAttempt).Name};


        /// <summary>
        /// Delete just the entity 
        /// </summary>
        public void Delete(DatabaseEntities dbContext)
        {
            dbContext.GisUploadSourceOrganizations.Remove(this);
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

            foreach(var x in GisUploadAttempts.ToList())
            {
                x.DeleteFull(dbContext);
            }
        }

        [Key]
        public int GisUploadSourceOrganizationID { get; set; }
        public string GisUploadSourceOrganizationName { get; set; }
        [NotMapped]
        public int PrimaryKey { get { return GisUploadSourceOrganizationID; } set { GisUploadSourceOrganizationID = value; } }

        public virtual ICollection<GisUploadAttempt> GisUploadAttempts { get; set; }

        public static class FieldLengths
        {
            public const int GisUploadSourceOrganizationName = 100;
        }
    }
}