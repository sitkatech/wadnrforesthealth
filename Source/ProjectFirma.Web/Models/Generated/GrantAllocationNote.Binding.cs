//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[GrantAllocationNote]
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
    // Table [dbo].[GrantAllocationNote] is NOT multi-tenant, so is attributed as ICanDeleteFull
    [Table("[dbo].[GrantAllocationNote]")]
    public partial class GrantAllocationNote : IHavePrimaryKey, ICanDeleteFull
    {
        /// <summary>
        /// Default Constructor; only used by EF
        /// </summary>
        protected GrantAllocationNote()
        {

        }

        /// <summary>
        /// Constructor for building a new object with MaximalConstructor required fields in preparation for insert into database
        /// </summary>
        public GrantAllocationNote(int grantAllocationNoteID, int grantAllocationID, string grantAllocationNoteText, int createdByPersonID, DateTime createdDate, int? lastUpdatedByPersonID, DateTime? lastUpdatedDate) : this()
        {
            this.GrantAllocationNoteID = grantAllocationNoteID;
            this.GrantAllocationID = grantAllocationID;
            this.GrantAllocationNoteText = grantAllocationNoteText;
            this.CreatedByPersonID = createdByPersonID;
            this.CreatedDate = createdDate;
            this.LastUpdatedByPersonID = lastUpdatedByPersonID;
            this.LastUpdatedDate = lastUpdatedDate;
        }

        /// <summary>
        /// Constructor for building a new object with MinimalConstructor required fields in preparation for insert into database
        /// </summary>
        public GrantAllocationNote(int grantAllocationID, int createdByPersonID, DateTime createdDate) : this()
        {
            // Mark this as a new object by setting primary key with special value
            this.GrantAllocationNoteID = ModelObjectHelpers.MakeNextUnsavedPrimaryKeyValue();
            
            this.GrantAllocationID = grantAllocationID;
            this.CreatedByPersonID = createdByPersonID;
            this.CreatedDate = createdDate;
        }

        /// <summary>
        /// Constructor for building a new object with MinimalConstructor required fields, using objects whenever possible
        /// </summary>
        public GrantAllocationNote(GrantAllocation grantAllocation, Person createdByPerson, DateTime createdDate) : this()
        {
            // Mark this as a new object by setting primary key with special value
            this.GrantAllocationNoteID = ModelObjectHelpers.MakeNextUnsavedPrimaryKeyValue();
            this.GrantAllocationID = grantAllocation.GrantAllocationID;
            this.GrantAllocation = grantAllocation;
            grantAllocation.GrantAllocationNotes.Add(this);
            this.CreatedByPersonID = createdByPerson.PersonID;
            this.CreatedByPerson = createdByPerson;
            createdByPerson.GrantAllocationNotesWhereYouAreTheCreatedByPerson.Add(this);
            this.CreatedDate = createdDate;
        }

        /// <summary>
        /// Creates a "blank" object of this type and populates primitives with defaults
        /// </summary>
        public static GrantAllocationNote CreateNewBlank(GrantAllocation grantAllocation, Person createdByPerson)
        {
            return new GrantAllocationNote(grantAllocation, createdByPerson, default(DateTime));
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
        public static readonly List<string> DependentEntityTypeNames = new List<string> {typeof(GrantAllocationNote).Name};


        /// <summary>
        /// Delete just the entity 
        /// </summary>
        public void Delete(DatabaseEntities dbContext)
        {
            dbContext.GrantAllocationNotes.Remove(this);
        }
        
        /// <summary>
        /// Delete entity plus all children
        /// </summary>
        public void DeleteFull(DatabaseEntities dbContext)
        {
            
            Delete(dbContext);
        }

        [Key]
        public int GrantAllocationNoteID { get; set; }
        public int GrantAllocationID { get; set; }
        public string GrantAllocationNoteText { get; set; }
        public int CreatedByPersonID { get; set; }
        public DateTime CreatedDate { get; set; }
        public int? LastUpdatedByPersonID { get; set; }
        public DateTime? LastUpdatedDate { get; set; }
        [NotMapped]
        public int PrimaryKey { get { return GrantAllocationNoteID; } set { GrantAllocationNoteID = value; } }

        public virtual GrantAllocation GrantAllocation { get; set; }
        public virtual Person CreatedByPerson { get; set; }
        public virtual Person LastUpdatedByPerson { get; set; }

        public static class FieldLengths
        {

        }
    }
}