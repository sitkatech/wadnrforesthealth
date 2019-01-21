//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[GrantStatus]
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
    [Table("[dbo].[GrantStatus]")]
    public partial class GrantStatus : IHavePrimaryKey, ICanDeleteFull
    {
        /// <summary>
        /// Default Constructor; only used by EF
        /// </summary>
        protected GrantStatus()
        {
            this.Grants = new HashSet<Grant>();
        }

        /// <summary>
        /// Constructor for building a new object with MaximalConstructor required fields in preparation for insert into database
        /// </summary>
        public GrantStatus(int grantStatusID, string grantStatusName) : this()
        {
            this.GrantStatusID = grantStatusID;
            this.GrantStatusName = grantStatusName;
        }

        /// <summary>
        /// Constructor for building a new object with MinimalConstructor required fields in preparation for insert into database
        /// </summary>
        public GrantStatus(string grantStatusName) : this()
        {
            // Mark this as a new object by setting primary key with special value
            this.GrantStatusID = ModelObjectHelpers.MakeNextUnsavedPrimaryKeyValue();
            
            this.GrantStatusName = grantStatusName;
        }


        /// <summary>
        /// Creates a "blank" object of this type and populates primitives with defaults
        /// </summary>
        public static GrantStatus CreateNewBlank()
        {
            return new GrantStatus(default(string));
        }

        /// <summary>
        /// Does this object have any dependent objects? (If it does have dependent objects, these would need to be deleted before this object could be deleted.)
        /// </summary>
        /// <returns></returns>
        public bool HasDependentObjects()
        {
            return Grants.Any();
        }

        /// <summary>
        /// Dependent type names of this entity
        /// </summary>
        public static readonly List<string> DependentEntityTypeNames = new List<string> {typeof(GrantStatus).Name, typeof(Grant).Name};


        /// <summary>
        /// Dependent type names of this entity
        /// </summary>
        public void DeleteFull(DatabaseEntities dbContext)
        {
            DeleteChildren(dbContext);
            dbContext.GrantStatuses.Remove(this);
        }
        /// <summary>
        /// Dependent type names of this entity
        /// </summary>
        public void DeleteChildren(DatabaseEntities dbContext)
        {

            foreach(var x in Grants.ToList())
            {
                x.DeleteFull(dbContext);
            }
        }

        [Key]
        public int GrantStatusID { get; set; }
        public string GrantStatusName { get; set; }
        [NotMapped]
        public int PrimaryKey { get { return GrantStatusID; } set { GrantStatusID = value; } }

        public virtual ICollection<Grant> Grants { get; set; }

        public static class FieldLengths
        {
            public const int GrantStatusName = 100;
        }
    }
}