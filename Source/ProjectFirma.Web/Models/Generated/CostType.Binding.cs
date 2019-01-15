//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[CostType]
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
    // Table [dbo].[CostType] is multi-tenant, so is attributed as IHaveATenantID
    [Table("[dbo].[CostType]")]
    public partial class CostType : IHavePrimaryKey, IHaveATenantID
    {
        /// <summary>
        /// Default Constructor; only used by EF
        /// </summary>
        protected CostType()
        {
            this.GrantAllocations = new HashSet<GrantAllocation>();
            this.TenantID = HttpRequestStorage.Tenant.TenantID;
        }

        /// <summary>
        /// Constructor for building a new object with MaximalConstructor required fields in preparation for insert into database
        /// </summary>
        public CostType(int costTypeID, string costTypeDescription) : this()
        {
            this.CostTypeID = costTypeID;
            this.CostTypeDescription = costTypeDescription;
        }

        /// <summary>
        /// Constructor for building a new object with MinimalConstructor required fields in preparation for insert into database
        /// </summary>
        public CostType(string costTypeDescription) : this()
        {
            // Mark this as a new object by setting primary key with special value
            this.CostTypeID = ModelObjectHelpers.MakeNextUnsavedPrimaryKeyValue();
            
            this.CostTypeDescription = costTypeDescription;
        }


        /// <summary>
        /// Creates a "blank" object of this type and populates primitives with defaults
        /// </summary>
        public static CostType CreateNewBlank()
        {
            return new CostType(default(string));
        }

        /// <summary>
        /// Does this object have any dependent objects? (If it does have dependent objects, these would need to be deleted before this object could be deleted.)
        /// </summary>
        /// <returns></returns>
        public bool HasDependentObjects()
        {
            return GrantAllocations.Any();
        }

        /// <summary>
        /// Dependent type names of this entity
        /// </summary>
        public static readonly List<string> DependentEntityTypeNames = new List<string> {typeof(CostType).Name, typeof(GrantAllocation).Name};


        /// <summary>
        /// Dependent type names of this entity
        /// </summary>
        public void DeleteFull(DatabaseEntities dbContext)
        {
            DeleteChildren(dbContext);
            dbContext.AllCostTypes.Remove(this);
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
        }

        [Key]
        public int CostTypeID { get; set; }
        public int TenantID { get; private set; }
        public string CostTypeDescription { get; set; }
        [NotMapped]
        public int PrimaryKey { get { return CostTypeID; } set { CostTypeID = value; } }

        public virtual ICollection<GrantAllocation> GrantAllocations { get; set; }
        public Tenant Tenant { get { return Tenant.AllLookupDictionary[TenantID]; } }

        public static class FieldLengths
        {
            public const int CostTypeDescription = 255;
        }
    }
}