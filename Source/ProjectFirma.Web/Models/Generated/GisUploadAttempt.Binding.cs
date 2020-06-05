//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[GisUploadAttempt]
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
    // Table [dbo].[GisUploadAttempt] is NOT multi-tenant, so is attributed as ICanDeleteFull
    [Table("[dbo].[GisUploadAttempt]")]
    public partial class GisUploadAttempt : IHavePrimaryKey, ICanDeleteFull
    {
        /// <summary>
        /// Default Constructor; only used by EF
        /// </summary>
        protected GisUploadAttempt()
        {

        }

        /// <summary>
        /// Constructor for building a new object with MaximalConstructor required fields in preparation for insert into database
        /// </summary>
        public GisUploadAttempt(int gisUploadAttemptID, int gisUploadSourceOrganizationID, int gisUploadAttemptCreatePersonID, DateTime gisUploadAttemptCreateDate) : this()
        {
            this.GisUploadAttemptID = gisUploadAttemptID;
            this.GisUploadSourceOrganizationID = gisUploadSourceOrganizationID;
            this.GisUploadAttemptCreatePersonID = gisUploadAttemptCreatePersonID;
            this.GisUploadAttemptCreateDate = gisUploadAttemptCreateDate;
        }

        /// <summary>
        /// Constructor for building a new object with MinimalConstructor required fields in preparation for insert into database
        /// </summary>
        public GisUploadAttempt(int gisUploadSourceOrganizationID, int gisUploadAttemptCreatePersonID, DateTime gisUploadAttemptCreateDate) : this()
        {
            // Mark this as a new object by setting primary key with special value
            this.GisUploadAttemptID = ModelObjectHelpers.MakeNextUnsavedPrimaryKeyValue();
            
            this.GisUploadSourceOrganizationID = gisUploadSourceOrganizationID;
            this.GisUploadAttemptCreatePersonID = gisUploadAttemptCreatePersonID;
            this.GisUploadAttemptCreateDate = gisUploadAttemptCreateDate;
        }

        /// <summary>
        /// Constructor for building a new object with MinimalConstructor required fields, using objects whenever possible
        /// </summary>
        public GisUploadAttempt(GisUploadSourceOrganization gisUploadSourceOrganization, Person gisUploadAttemptCreatePerson, DateTime gisUploadAttemptCreateDate) : this()
        {
            // Mark this as a new object by setting primary key with special value
            this.GisUploadAttemptID = ModelObjectHelpers.MakeNextUnsavedPrimaryKeyValue();
            this.GisUploadSourceOrganizationID = gisUploadSourceOrganization.GisUploadSourceOrganizationID;
            this.GisUploadSourceOrganization = gisUploadSourceOrganization;
            gisUploadSourceOrganization.GisUploadAttempts.Add(this);
            this.GisUploadAttemptCreatePersonID = gisUploadAttemptCreatePerson.PersonID;
            this.GisUploadAttemptCreatePerson = gisUploadAttemptCreatePerson;
            gisUploadAttemptCreatePerson.GisUploadAttemptsWhereYouAreTheGisUploadAttemptCreatePerson.Add(this);
            this.GisUploadAttemptCreateDate = gisUploadAttemptCreateDate;
        }

        /// <summary>
        /// Creates a "blank" object of this type and populates primitives with defaults
        /// </summary>
        public static GisUploadAttempt CreateNewBlank(GisUploadSourceOrganization gisUploadSourceOrganization, Person gisUploadAttemptCreatePerson)
        {
            return new GisUploadAttempt(gisUploadSourceOrganization, gisUploadAttemptCreatePerson, default(DateTime));
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
        public static readonly List<string> DependentEntityTypeNames = new List<string> {typeof(GisUploadAttempt).Name};


        /// <summary>
        /// Delete just the entity 
        /// </summary>
        public void Delete(DatabaseEntities dbContext)
        {
            dbContext.GisUploadAttempts.Remove(this);
        }
        
        /// <summary>
        /// Delete entity plus all children
        /// </summary>
        public void DeleteFull(DatabaseEntities dbContext)
        {
            
            Delete(dbContext);
        }

        [Key]
        public int GisUploadAttemptID { get; set; }
        public int GisUploadSourceOrganizationID { get; set; }
        public int GisUploadAttemptCreatePersonID { get; set; }
        public DateTime GisUploadAttemptCreateDate { get; set; }
        [NotMapped]
        public int PrimaryKey { get { return GisUploadAttemptID; } set { GisUploadAttemptID = value; } }

        public virtual GisUploadSourceOrganization GisUploadSourceOrganization { get; set; }
        public virtual Person GisUploadAttemptCreatePerson { get; set; }

        public static class FieldLengths
        {

        }
    }
}