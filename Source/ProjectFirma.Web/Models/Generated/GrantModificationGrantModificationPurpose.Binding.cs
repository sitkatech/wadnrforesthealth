//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[GrantModificationGrantModificationPurpose]
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
    // Table [dbo].[GrantModificationGrantModificationPurpose] is NOT multi-tenant, so is attributed as ICanDeleteFull
    [Table("[dbo].[GrantModificationGrantModificationPurpose]")]
    public partial class GrantModificationGrantModificationPurpose : IHavePrimaryKey, ICanDeleteFull
    {
        /// <summary>
        /// Default Constructor; only used by EF
        /// </summary>
        protected GrantModificationGrantModificationPurpose()
        {

        }

        /// <summary>
        /// Constructor for building a new object with MaximalConstructor required fields in preparation for insert into database
        /// </summary>
        public GrantModificationGrantModificationPurpose(int grantModificationGrantModificationPurposeID, int grantModificationID, int grantModificationPurposeID) : this()
        {
            this.GrantModificationGrantModificationPurposeID = grantModificationGrantModificationPurposeID;
            this.GrantModificationID = grantModificationID;
            this.GrantModificationPurposeID = grantModificationPurposeID;
        }

        /// <summary>
        /// Constructor for building a new object with MinimalConstructor required fields in preparation for insert into database
        /// </summary>
        public GrantModificationGrantModificationPurpose(int grantModificationID, int grantModificationPurposeID) : this()
        {
            // Mark this as a new object by setting primary key with special value
            this.GrantModificationGrantModificationPurposeID = ModelObjectHelpers.MakeNextUnsavedPrimaryKeyValue();
            
            this.GrantModificationID = grantModificationID;
            this.GrantModificationPurposeID = grantModificationPurposeID;
        }

        /// <summary>
        /// Constructor for building a new object with MinimalConstructor required fields, using objects whenever possible
        /// </summary>
        public GrantModificationGrantModificationPurpose(GrantModification grantModification, GrantModificationPurpose grantModificationPurpose) : this()
        {
            // Mark this as a new object by setting primary key with special value
            this.GrantModificationGrantModificationPurposeID = ModelObjectHelpers.MakeNextUnsavedPrimaryKeyValue();
            this.GrantModificationID = grantModification.GrantModificationID;
            this.GrantModification = grantModification;
            grantModification.GrantModificationGrantModificationPurposes.Add(this);
            this.GrantModificationPurposeID = grantModificationPurpose.GrantModificationPurposeID;
            this.GrantModificationPurpose = grantModificationPurpose;
            grantModificationPurpose.GrantModificationGrantModificationPurposes.Add(this);
        }

        /// <summary>
        /// Creates a "blank" object of this type and populates primitives with defaults
        /// </summary>
        public static GrantModificationGrantModificationPurpose CreateNewBlank(GrantModification grantModification, GrantModificationPurpose grantModificationPurpose)
        {
            return new GrantModificationGrantModificationPurpose(grantModification, grantModificationPurpose);
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
        public static readonly List<string> DependentEntityTypeNames = new List<string> {typeof(GrantModificationGrantModificationPurpose).Name};


        /// <summary>
        /// Delete just the entity 
        /// </summary>
        public void Delete(DatabaseEntities dbContext)
        {
            dbContext.GrantModificationGrantModificationPurposes.Remove(this);
        }
        
        /// <summary>
        /// Delete entity plus all children
        /// </summary>
        public void DeleteFull(DatabaseEntities dbContext)
        {
            
            Delete(dbContext);
        }

        [Key]
        public int GrantModificationGrantModificationPurposeID { get; set; }
        public int GrantModificationID { get; set; }
        public int GrantModificationPurposeID { get; set; }
        [NotMapped]
        public int PrimaryKey { get { return GrantModificationGrantModificationPurposeID; } set { GrantModificationGrantModificationPurposeID = value; } }

        public virtual GrantModification GrantModification { get; set; }
        public virtual GrantModificationPurpose GrantModificationPurpose { get; set; }

        public static class FieldLengths
        {

        }
    }
}