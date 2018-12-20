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
        public GrantAllocation(int grantAllocationID, string grantNumber, DateTime startDate, DateTime endDate, decimal? totalAward) : this()
        {
            this.GrantAllocationID = grantAllocationID;
            this.GrantNumber = grantNumber;
            this.StartDate = startDate;
            this.EndDate = endDate;
            this.TotalAward = totalAward;
        }

        /// <summary>
        /// Constructor for building a new object with MinimalConstructor required fields in preparation for insert into database
        /// </summary>
        public GrantAllocation(DateTime startDate, DateTime endDate) : this()
        {
            // Mark this as a new object by setting primary key with special value
            this.GrantAllocationID = ModelObjectHelpers.MakeNextUnsavedPrimaryKeyValue();
            
            this.StartDate = startDate;
            this.EndDate = endDate;
        }


        /// <summary>
        /// Creates a "blank" object of this type and populates primitives with defaults
        /// </summary>
        public static GrantAllocation CreateNewBlank()
        {
            return new GrantAllocation(default(DateTime), default(DateTime));
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
        public string GrantNumber { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public decimal? TotalAward { get; set; }
        [NotMapped]
        public int PrimaryKey { get { return GrantAllocationID; } set { GrantAllocationID = value; } }

        public Tenant Tenant { get { return Tenant.AllLookupDictionary[TenantID]; } }

        public static class FieldLengths
        {
            public const int GrantNumber = 30;
        }
    }
}