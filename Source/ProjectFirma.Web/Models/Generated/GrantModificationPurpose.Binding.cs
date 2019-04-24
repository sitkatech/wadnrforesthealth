//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[GrantModificationPurpose]
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
    // Table [dbo].[GrantModificationPurpose] is NOT multi-tenant, so is attributed as ICanDeleteFull
    [Table("[dbo].[GrantModificationPurpose]")]
    public partial class GrantModificationPurpose : IHavePrimaryKey, ICanDeleteFull
    {
        /// <summary>
        /// Default Constructor; only used by EF
        /// </summary>
        protected GrantModificationPurpose()
        {
            this.GrantModificationGrantModificationPurposes = new HashSet<GrantModificationGrantModificationPurpose>();
        }

        /// <summary>
        /// Constructor for building a new object with MaximalConstructor required fields in preparation for insert into database
        /// </summary>
        public GrantModificationPurpose(int grantModificationPurposeID, string grantModificationPurposeName) : this()
        {
            this.GrantModificationPurposeID = grantModificationPurposeID;
            this.GrantModificationPurposeName = grantModificationPurposeName;
        }

        /// <summary>
        /// Constructor for building a new object with MinimalConstructor required fields in preparation for insert into database
        /// </summary>
        public GrantModificationPurpose(string grantModificationPurposeName) : this()
        {
            // Mark this as a new object by setting primary key with special value
            this.GrantModificationPurposeID = ModelObjectHelpers.MakeNextUnsavedPrimaryKeyValue();
            
            this.GrantModificationPurposeName = grantModificationPurposeName;
        }


        /// <summary>
        /// Creates a "blank" object of this type and populates primitives with defaults
        /// </summary>
        public static GrantModificationPurpose CreateNewBlank()
        {
            return new GrantModificationPurpose(default(string));
        }

        /// <summary>
        /// Does this object have any dependent objects? (If it does have dependent objects, these would need to be deleted before this object could be deleted.)
        /// </summary>
        /// <returns></returns>
        public bool HasDependentObjects()
        {
            return GrantModificationGrantModificationPurposes.Any();
        }

        /// <summary>
        /// Dependent type names of this entity
        /// </summary>
        public static readonly List<string> DependentEntityTypeNames = new List<string> {typeof(GrantModificationPurpose).Name, typeof(GrantModificationGrantModificationPurpose).Name};


        /// <summary>
        /// Delete just the entity 
        /// </summary>
        public void Delete(DatabaseEntities dbContext)
        {
            dbContext.GrantModificationPurposes.Remove(this);
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

            foreach(var x in GrantModificationGrantModificationPurposes.ToList())
            {
                x.DeleteFull(dbContext);
            }
        }

        [Key]
        public int GrantModificationPurposeID { get; set; }
        public string GrantModificationPurposeName { get; set; }
        [NotMapped]
        public int PrimaryKey { get { return GrantModificationPurposeID; } set { GrantModificationPurposeID = value; } }

        public virtual ICollection<GrantModificationGrantModificationPurpose> GrantModificationGrantModificationPurposes { get; set; }

        public static class FieldLengths
        {
            public const int GrantModificationPurposeName = 100;
        }
    }
}