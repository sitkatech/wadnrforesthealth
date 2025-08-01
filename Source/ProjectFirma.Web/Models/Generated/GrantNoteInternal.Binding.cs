//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[GrantNoteInternal]
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
    // Table [dbo].[GrantNoteInternal] is NOT multi-tenant, so is attributed as ICanDeleteFull
    [Table("[dbo].[GrantNoteInternal]")]
    public partial class FundSourceNoteInternal : IHavePrimaryKey, ICanDeleteFull
    {
        /// <summary>
        /// Default Constructor; only used by EF
        /// </summary>
        protected FundSourceNoteInternal()
        {

        }

        /// <summary>
        /// Constructor for building a new object with MaximalConstructor required fields in preparation for insert into database
        /// </summary>
        public FundSourceNoteInternal(int grantNoteInternalID, int grantID, string grantNoteText, int createdByPersonID, DateTime createdDate, int? lastUpdatedByPersonID, DateTime? lastUpdatedDate) : this()
        {
            this.GrantNoteInternalID = grantNoteInternalID;
            this.GrantID = grantID;
            this.GrantNoteText = grantNoteText;
            this.CreatedByPersonID = createdByPersonID;
            this.CreatedDate = createdDate;
            this.LastUpdatedByPersonID = lastUpdatedByPersonID;
            this.LastUpdatedDate = lastUpdatedDate;
        }

        /// <summary>
        /// Constructor for building a new object with MinimalConstructor required fields in preparation for insert into database
        /// </summary>
        public FundSourceNoteInternal(int grantID, int createdByPersonID, DateTime createdDate) : this()
        {
            // Mark this as a new object by setting primary key with special value
            this.GrantNoteInternalID = ModelObjectHelpers.MakeNextUnsavedPrimaryKeyValue();
            
            this.GrantID = grantID;
            this.CreatedByPersonID = createdByPersonID;
            this.CreatedDate = createdDate;
        }

        /// <summary>
        /// Constructor for building a new object with MinimalConstructor required fields, using objects whenever possible
        /// </summary>
        public FundSourceNoteInternal(FundSource fundSource, Person createdByPerson, DateTime createdDate) : this()
        {
            // Mark this as a new object by setting primary key with special value
            this.GrantNoteInternalID = ModelObjectHelpers.MakeNextUnsavedPrimaryKeyValue();
            this.GrantID = fundSource.FundSourceID;
            this.FundSource = fundSource;
            fundSource.GrantNoteInternals.Add(this);
            this.CreatedByPersonID = createdByPerson.PersonID;
            this.CreatedByPerson = createdByPerson;
            createdByPerson.GrantNoteInternalsWhereYouAreTheCreatedByPerson.Add(this);
            this.CreatedDate = createdDate;
        }

        /// <summary>
        /// Creates a "blank" object of this type and populates primitives with defaults
        /// </summary>
        public static FundSourceNoteInternal CreateNewBlank(FundSource fundSource, Person createdByPerson)
        {
            return new FundSourceNoteInternal(fundSource, createdByPerson, default(DateTime));
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
        public static readonly List<string> DependentEntityTypeNames = new List<string> {typeof(FundSourceNoteInternal).Name};


        /// <summary>
        /// Delete just the entity 
        /// </summary>
        public void Delete(DatabaseEntities dbContext)
        {
            dbContext.GrantNoteInternals.Remove(this);
        }
        
        /// <summary>
        /// Delete entity plus all children
        /// </summary>
        public void DeleteFull(DatabaseEntities dbContext)
        {
            
            Delete(dbContext);
        }

        [Key]
        public int GrantNoteInternalID { get; set; }
        public int GrantID { get; set; }
        public string GrantNoteText { get; set; }
        public int CreatedByPersonID { get; set; }
        public DateTime CreatedDate { get; set; }
        public int? LastUpdatedByPersonID { get; set; }
        public DateTime? LastUpdatedDate { get; set; }
        [NotMapped]
        public int PrimaryKey { get { return GrantNoteInternalID; } set { GrantNoteInternalID = value; } }

        public virtual FundSource FundSource { get; set; }
        public virtual Person CreatedByPerson { get; set; }
        public virtual Person LastUpdatedByPerson { get; set; }

        public static class FieldLengths
        {

        }
    }
}