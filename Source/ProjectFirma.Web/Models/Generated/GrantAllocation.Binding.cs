//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[GrantAllocation]
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
    [Table("[dbo].[GrantAllocation]")]
    public partial class GrantAllocation : IHavePrimaryKey, IHaveATenantID
    {
        /// <summary>
        /// Default Constructor; only used by EF
        /// </summary>
        protected GrantAllocation()
        {

            this.TenantID = HttpRequestStorage.Tenant.TenantID;
        }

        /// <summary>
        /// Constructor for building a new object with MaximalConstructor required fields in preparation for insert into database
        /// </summary>
        public GrantAllocation(int grantAllocationID, int grantID, string projectName, DateTime startDate, DateTime endDate, decimal? allocationAmount, int? costTypeID) : this()
        {
            this.GrantAllocationID = grantAllocationID;
            this.GrantID = grantID;
            this.ProjectName = projectName;
            this.StartDate = startDate;
            this.EndDate = endDate;
            this.AllocationAmount = allocationAmount;
            this.CostTypeID = costTypeID;
        }

        /// <summary>
        /// Constructor for building a new object with MinimalConstructor required fields in preparation for insert into database
        /// </summary>
        public GrantAllocation(int grantID, DateTime startDate, DateTime endDate) : this()
        {
            // Mark this as a new object by setting primary key with special value
            this.GrantAllocationID = ModelObjectHelpers.MakeNextUnsavedPrimaryKeyValue();
            
            this.GrantID = grantID;
            this.StartDate = startDate;
            this.EndDate = endDate;
        }

        /// <summary>
        /// Constructor for building a new object with MinimalConstructor required fields, using objects whenever possible
        /// </summary>
        public GrantAllocation(Grant grant, DateTime startDate, DateTime endDate) : this()
        {
            // Mark this as a new object by setting primary key with special value
            this.GrantAllocationID = ModelObjectHelpers.MakeNextUnsavedPrimaryKeyValue();
            this.GrantID = grant.GrantID;
            this.Grant = grant;
            grant.GrantAllocations.Add(this);
            this.StartDate = startDate;
            this.EndDate = endDate;
        }

        /// <summary>
        /// Creates a "blank" object of this type and populates primitives with defaults
        /// </summary>
        public static GrantAllocation CreateNewBlank(Grant grant)
        {
            return new GrantAllocation(grant, default(DateTime), default(DateTime));
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
        /// Dependent type names of this entity
        /// </summary>
        public static readonly List<string> DependentEntityTypeNames = new List<string> {typeof(GrantAllocation).Name};


        /// <summary>
        /// Dependent type names of this entity
        /// </summary>
        public void DeleteFull(DatabaseEntities dbContext)
        {
            
            dbContext.AllGrantAllocations.Remove(this);
        }

        [Key]
        public int GrantAllocationID { get; set; }
        public int TenantID { get; private set; }
        public int GrantID { get; set; }
        public string ProjectName { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public decimal? AllocationAmount { get; set; }
        public int? CostTypeID { get; set; }
        [NotMapped]
        public int PrimaryKey { get { return GrantAllocationID; } set { GrantAllocationID = value; } }

        public Tenant Tenant { get { return Tenant.AllLookupDictionary[TenantID]; } }
        public virtual Grant Grant { get; set; }
        public virtual CostType CostType { get; set; }

        public static class FieldLengths
        {
            public const int ProjectName = 100;
        }
    }
}