//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[GrantType]
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
    // Table [dbo].[GrantType] is NOT multi-tenant, so is attributed as ICanDeleteFull
    [Table("[dbo].[GrantType]")]
    public partial class FundSourceType : IHavePrimaryKey, ICanDeleteFull
    {
        /// <summary>
        /// Default Constructor; only used by EF
        /// </summary>
        protected FundSourceType()
        {
            this.Grants = new HashSet<FundSource>();
        }

        /// <summary>
        /// Constructor for building a new object with MaximalConstructor required fields in preparation for insert into database
        /// </summary>
        public FundSourceType(int grantTypeID, string grantTypeName) : this()
        {
            this.GrantTypeID = grantTypeID;
            this.GrantTypeName = grantTypeName;
        }

        /// <summary>
        /// Constructor for building a new object with MinimalConstructor required fields in preparation for insert into database
        /// </summary>
        public FundSourceType(string grantTypeName) : this()
        {
            // Mark this as a new object by setting primary key with special value
            this.GrantTypeID = ModelObjectHelpers.MakeNextUnsavedPrimaryKeyValue();
            
            this.GrantTypeName = grantTypeName;
        }


        /// <summary>
        /// Creates a "blank" object of this type and populates primitives with defaults
        /// </summary>
        public static FundSourceType CreateNewBlank()
        {
            return new FundSourceType(default(string));
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
        /// Active Dependent type names of this object
        /// </summary>
        public List<string> DependentObjectNames() 
        {
            var dependentObjects = new List<string>();
            
            if(Grants.Any())
            {
                dependentObjects.Add(typeof(FundSource).Name);
            }
            return dependentObjects.Distinct().ToList();
        }

        /// <summary>
        /// Dependent type names of this entity
        /// </summary>
        public static readonly List<string> DependentEntityTypeNames = new List<string> {typeof(FundSourceType).Name, typeof(FundSource).Name};


        /// <summary>
        /// Delete just the entity 
        /// </summary>
        public void Delete(DatabaseEntities dbContext)
        {
            dbContext.GrantTypes.Remove(this);
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

            foreach(var x in Grants.ToList())
            {
                x.DeleteFull(dbContext);
            }
        }

        [Key]
        public int GrantTypeID { get; set; }
        public string GrantTypeName { get; set; }
        [NotMapped]
        public int PrimaryKey { get { return GrantTypeID; } set { GrantTypeID = value; } }

        public virtual ICollection<FundSource> Grants { get; set; }

        public static class FieldLengths
        {
            public const int GrantTypeName = 100;
        }
    }
}