//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[GrantModificationStatus]
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
    // Table [dbo].[GrantModificationStatus] is NOT multi-tenant, so is attributed as ICanDeleteFull
    [Table("[dbo].[GrantModificationStatus]")]
    public partial class GrantModificationStatus : IHavePrimaryKey, ICanDeleteFull
    {
        /// <summary>
        /// Default Constructor; only used by EF
        /// </summary>
        protected GrantModificationStatus()
        {
            this.GrantModifications = new HashSet<GrantModification>();
        }

        /// <summary>
        /// Constructor for building a new object with MaximalConstructor required fields in preparation for insert into database
        /// </summary>
        public GrantModificationStatus(int grantModificationStatusID, string grantModificationStatusName) : this()
        {
            this.GrantModificationStatusID = grantModificationStatusID;
            this.GrantModificationStatusName = grantModificationStatusName;
        }

        /// <summary>
        /// Constructor for building a new object with MinimalConstructor required fields in preparation for insert into database
        /// </summary>
        public GrantModificationStatus(string grantModificationStatusName) : this()
        {
            // Mark this as a new object by setting primary key with special value
            this.GrantModificationStatusID = ModelObjectHelpers.MakeNextUnsavedPrimaryKeyValue();
            
            this.GrantModificationStatusName = grantModificationStatusName;
        }


        /// <summary>
        /// Creates a "blank" object of this type and populates primitives with defaults
        /// </summary>
        public static GrantModificationStatus CreateNewBlank()
        {
            return new GrantModificationStatus(default(string));
        }

        /// <summary>
        /// Does this object have any dependent objects? (If it does have dependent objects, these would need to be deleted before this object could be deleted.)
        /// </summary>
        /// <returns></returns>
        public bool HasDependentObjects()
        {
            return GrantModifications.Any();
        }

        /// <summary>
        /// Dependent type names of this entity
        /// </summary>
        public static readonly List<string> DependentEntityTypeNames = new List<string> {typeof(GrantModificationStatus).Name, typeof(GrantModification).Name};


        /// <summary>
        /// Delete just the entity 
        /// </summary>
        public void Delete(DatabaseEntities dbContext)
        {
            dbContext.GrantModificationStatuses.Remove(this);
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

            foreach(var x in GrantModifications.ToList())
            {
                x.DeleteFull(dbContext);
            }
        }

        [Key]
        public int GrantModificationStatusID { get; set; }
        public string GrantModificationStatusName { get; set; }
        [NotMapped]
        public int PrimaryKey { get { return GrantModificationStatusID; } set { GrantModificationStatusID = value; } }

        public virtual ICollection<GrantModification> GrantModifications { get; set; }

        public static class FieldLengths
        {
            public const int GrantModificationStatusName = 100;
        }
    }
}