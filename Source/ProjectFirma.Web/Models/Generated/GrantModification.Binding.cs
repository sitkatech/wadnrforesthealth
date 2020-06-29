//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[GrantModification]
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
    // Table [dbo].[GrantModification] is NOT multi-tenant, so is attributed as ICanDeleteFull
    [Table("[dbo].[GrantModification]")]
    public partial class GrantModification : IHavePrimaryKey, ICanDeleteFull
    {
        /// <summary>
        /// Default Constructor; only used by EF
        /// </summary>
        protected GrantModification()
        {
            this.GrantAllocations = new HashSet<GrantAllocation>();
            this.GrantModificationGrantModificationPurposes = new HashSet<GrantModificationGrantModificationPurpose>();
            this.GrantModificationNoteInternals = new HashSet<GrantModificationNoteInternal>();
        }

        /// <summary>
        /// Constructor for building a new object with MaximalConstructor required fields in preparation for insert into database
        /// </summary>
        public GrantModification(int grantModificationID, string grantModificationName, int grantID, DateTime grantModificationStartDate, DateTime grantModificationEndDate, int grantModificationStatusID, decimal grantModificationAmount, string grantModificationDescription, int? grantModificationFileResourceID) : this()
        {
            this.GrantModificationID = grantModificationID;
            this.GrantModificationName = grantModificationName;
            this.GrantID = grantID;
            this.GrantModificationStartDate = grantModificationStartDate;
            this.GrantModificationEndDate = grantModificationEndDate;
            this.GrantModificationStatusID = grantModificationStatusID;
            this.GrantModificationAmount = grantModificationAmount;
            this.GrantModificationDescription = grantModificationDescription;
            this.GrantModificationFileResourceID = grantModificationFileResourceID;
        }

        /// <summary>
        /// Constructor for building a new object with MinimalConstructor required fields in preparation for insert into database
        /// </summary>
        public GrantModification(string grantModificationName, int grantID, DateTime grantModificationStartDate, DateTime grantModificationEndDate, int grantModificationStatusID, decimal grantModificationAmount) : this()
        {
            // Mark this as a new object by setting primary key with special value
            this.GrantModificationID = ModelObjectHelpers.MakeNextUnsavedPrimaryKeyValue();
            
            this.GrantModificationName = grantModificationName;
            this.GrantID = grantID;
            this.GrantModificationStartDate = grantModificationStartDate;
            this.GrantModificationEndDate = grantModificationEndDate;
            this.GrantModificationStatusID = grantModificationStatusID;
            this.GrantModificationAmount = grantModificationAmount;
        }

        /// <summary>
        /// Constructor for building a new object with MinimalConstructor required fields, using objects whenever possible
        /// </summary>
        public GrantModification(string grantModificationName, Grant grant, DateTime grantModificationStartDate, DateTime grantModificationEndDate, GrantModificationStatus grantModificationStatus, decimal grantModificationAmount) : this()
        {
            // Mark this as a new object by setting primary key with special value
            this.GrantModificationID = ModelObjectHelpers.MakeNextUnsavedPrimaryKeyValue();
            this.GrantModificationName = grantModificationName;
            this.GrantID = grant.GrantID;
            this.Grant = grant;
            grant.GrantModifications.Add(this);
            this.GrantModificationStartDate = grantModificationStartDate;
            this.GrantModificationEndDate = grantModificationEndDate;
            this.GrantModificationStatusID = grantModificationStatus.GrantModificationStatusID;
            this.GrantModificationStatus = grantModificationStatus;
            grantModificationStatus.GrantModifications.Add(this);
            this.GrantModificationAmount = grantModificationAmount;
        }

        /// <summary>
        /// Creates a "blank" object of this type and populates primitives with defaults
        /// </summary>
        public static GrantModification CreateNewBlank(Grant grant, GrantModificationStatus grantModificationStatus)
        {
            return new GrantModification(default(string), grant, default(DateTime), default(DateTime), grantModificationStatus, default(decimal));
        }

        /// <summary>
        /// Does this object have any dependent objects? (If it does have dependent objects, these would need to be deleted before this object could be deleted.)
        /// </summary>
        /// <returns></returns>
        public bool HasDependentObjects()
        {
            return GrantAllocations.Any() || GrantModificationGrantModificationPurposes.Any() || GrantModificationNoteInternals.Any();
        }

        /// <summary>
        /// Active Dependent type names of this object
        /// </summary>
        public List<string> DependentObjectNames() 
        {
            var dependentObjects = new List<string>();
            
            if(GrantAllocations.Any())
            {
                dependentObjects.Add(typeof(GrantAllocation).Name);
            }

            if(GrantModificationGrantModificationPurposes.Any())
            {
                dependentObjects.Add(typeof(GrantModificationGrantModificationPurpose).Name);
            }

            if(GrantModificationNoteInternals.Any())
            {
                dependentObjects.Add(typeof(GrantModificationNoteInternal).Name);
            }
            return dependentObjects.Distinct().ToList();
        }

        /// <summary>
        /// Dependent type names of this entity
        /// </summary>
        public static readonly List<string> DependentEntityTypeNames = new List<string> {typeof(GrantModification).Name, typeof(GrantAllocation).Name, typeof(GrantModificationGrantModificationPurpose).Name, typeof(GrantModificationNoteInternal).Name};


        /// <summary>
        /// Delete just the entity 
        /// </summary>
        public void Delete(DatabaseEntities dbContext)
        {
            dbContext.GrantModifications.Remove(this);
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

            foreach(var x in GrantAllocations.ToList())
            {
                x.DeleteFull(dbContext);
            }

            foreach(var x in GrantModificationGrantModificationPurposes.ToList())
            {
                x.DeleteFull(dbContext);
            }

            foreach(var x in GrantModificationNoteInternals.ToList())
            {
                x.DeleteFull(dbContext);
            }
        }

        [Key]
        public int GrantModificationID { get; set; }
        public string GrantModificationName { get; set; }
        public int GrantID { get; set; }
        public DateTime GrantModificationStartDate { get; set; }
        public DateTime GrantModificationEndDate { get; set; }
        public int GrantModificationStatusID { get; set; }
        public decimal GrantModificationAmount { get; set; }
        public string GrantModificationDescription { get; set; }
        public int? GrantModificationFileResourceID { get; set; }
        [NotMapped]
        public int PrimaryKey { get { return GrantModificationID; } set { GrantModificationID = value; } }

        public virtual ICollection<GrantAllocation> GrantAllocations { get; set; }
        public virtual ICollection<GrantModificationGrantModificationPurpose> GrantModificationGrantModificationPurposes { get; set; }
        public virtual ICollection<GrantModificationNoteInternal> GrantModificationNoteInternals { get; set; }
        public virtual Grant Grant { get; set; }
        public virtual GrantModificationStatus GrantModificationStatus { get; set; }
        public virtual FileResource GrantModificationFileResource { get; set; }

        public static class FieldLengths
        {
            public const int GrantModificationName = 100;
        }
    }
}