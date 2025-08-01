//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[GrantAllocationExpenditure]
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
    // Table [dbo].[GrantAllocationExpenditure] is NOT multi-tenant, so is attributed as ICanDeleteFull
    [Table("[dbo].[GrantAllocationExpenditure]")]
    public partial class GrantAllocationExpenditure : IHavePrimaryKey, ICanDeleteFull
    {
        /// <summary>
        /// Default Constructor; only used by EF
        /// </summary>
        protected GrantAllocationExpenditure()
        {

        }

        /// <summary>
        /// Constructor for building a new object with MaximalConstructor required fields in preparation for insert into database
        /// </summary>
        public GrantAllocationExpenditure(int grantAllocationExpenditureID, int grantAllocationID, int? costTypeID, int biennium, int fiscalMonth, int calendarYear, int calendarMonth, decimal expenditureAmount) : this()
        {
            this.GrantAllocationExpenditureID = grantAllocationExpenditureID;
            this.GrantAllocationID = grantAllocationID;
            this.CostTypeID = costTypeID;
            this.Biennium = biennium;
            this.FiscalMonth = fiscalMonth;
            this.CalendarYear = calendarYear;
            this.CalendarMonth = calendarMonth;
            this.ExpenditureAmount = expenditureAmount;
        }

        /// <summary>
        /// Constructor for building a new object with MinimalConstructor required fields in preparation for insert into database
        /// </summary>
        public GrantAllocationExpenditure(int grantAllocationID, int biennium, int fiscalMonth, int calendarYear, int calendarMonth, decimal expenditureAmount) : this()
        {
            // Mark this as a new object by setting primary key with special value
            this.GrantAllocationExpenditureID = ModelObjectHelpers.MakeNextUnsavedPrimaryKeyValue();
            
            this.GrantAllocationID = grantAllocationID;
            this.Biennium = biennium;
            this.FiscalMonth = fiscalMonth;
            this.CalendarYear = calendarYear;
            this.CalendarMonth = calendarMonth;
            this.ExpenditureAmount = expenditureAmount;
        }

        /// <summary>
        /// Constructor for building a new object with MinimalConstructor required fields, using objects whenever possible
        /// </summary>
        public GrantAllocationExpenditure(FundSourceAllocation fundSourceAllocation, int biennium, int fiscalMonth, int calendarYear, int calendarMonth, decimal expenditureAmount) : this()
        {
            // Mark this as a new object by setting primary key with special value
            this.GrantAllocationExpenditureID = ModelObjectHelpers.MakeNextUnsavedPrimaryKeyValue();
            this.GrantAllocationID = fundSourceAllocation.GrantAllocationID;
            this.FundSourceAllocation = fundSourceAllocation;
            fundSourceAllocation.GrantAllocationExpenditures.Add(this);
            this.Biennium = biennium;
            this.FiscalMonth = fiscalMonth;
            this.CalendarYear = calendarYear;
            this.CalendarMonth = calendarMonth;
            this.ExpenditureAmount = expenditureAmount;
        }

        /// <summary>
        /// Creates a "blank" object of this type and populates primitives with defaults
        /// </summary>
        public static GrantAllocationExpenditure CreateNewBlank(FundSourceAllocation fundSourceAllocation)
        {
            return new GrantAllocationExpenditure(fundSourceAllocation, default(int), default(int), default(int), default(int), default(decimal));
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
        public static readonly List<string> DependentEntityTypeNames = new List<string> {typeof(GrantAllocationExpenditure).Name};


        /// <summary>
        /// Delete just the entity 
        /// </summary>
        public void Delete(DatabaseEntities dbContext)
        {
            dbContext.GrantAllocationExpenditures.Remove(this);
        }
        
        /// <summary>
        /// Delete entity plus all children
        /// </summary>
        public void DeleteFull(DatabaseEntities dbContext)
        {
            
            Delete(dbContext);
        }

        [Key]
        public int GrantAllocationExpenditureID { get; set; }
        public int GrantAllocationID { get; set; }
        public int? CostTypeID { get; set; }
        public int Biennium { get; set; }
        public int FiscalMonth { get; set; }
        public int CalendarYear { get; set; }
        public int CalendarMonth { get; set; }
        public decimal ExpenditureAmount { get; set; }
        [NotMapped]
        public int PrimaryKey { get { return GrantAllocationExpenditureID; } set { GrantAllocationExpenditureID = value; } }

        public virtual FundSourceAllocation FundSourceAllocation { get; set; }
        public CostType CostType { get { return CostTypeID.HasValue ? CostType.AllLookupDictionary[CostTypeID.Value] : null; } }

        public static class FieldLengths
        {

        }
    }
}