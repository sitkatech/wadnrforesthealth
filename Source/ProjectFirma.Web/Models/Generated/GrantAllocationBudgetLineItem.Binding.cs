//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[GrantAllocationBudgetLineItem]
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
    // Table [dbo].[GrantAllocationBudgetLineItem] is NOT multi-tenant, so is attributed as ICanDeleteFull
    [Table("[dbo].[GrantAllocationBudgetLineItem]")]
    public partial class GrantAllocationBudgetLineItem : IHavePrimaryKey, ICanDeleteFull
    {
        /// <summary>
        /// Default Constructor; only used by EF
        /// </summary>
        protected GrantAllocationBudgetLineItem()
        {

        }

        /// <summary>
        /// Constructor for building a new object with MaximalConstructor required fields in preparation for insert into database
        /// </summary>
        public GrantAllocationBudgetLineItem(int grantAllocationBudgetLineItemID, int grantAllocationID, int costTypeID, decimal grantAllocationBudgetLineItemAmount, string grantAllocationBudgetLineItemNote) : this()
        {
            this.GrantAllocationBudgetLineItemID = grantAllocationBudgetLineItemID;
            this.GrantAllocationID = grantAllocationID;
            this.CostTypeID = costTypeID;
            this.GrantAllocationBudgetLineItemAmount = grantAllocationBudgetLineItemAmount;
            this.GrantAllocationBudgetLineItemNote = grantAllocationBudgetLineItemNote;
        }

        /// <summary>
        /// Constructor for building a new object with MinimalConstructor required fields in preparation for insert into database
        /// </summary>
        public GrantAllocationBudgetLineItem(int grantAllocationID, int costTypeID, decimal grantAllocationBudgetLineItemAmount) : this()
        {
            // Mark this as a new object by setting primary key with special value
            this.GrantAllocationBudgetLineItemID = ModelObjectHelpers.MakeNextUnsavedPrimaryKeyValue();
            
            this.GrantAllocationID = grantAllocationID;
            this.CostTypeID = costTypeID;
            this.GrantAllocationBudgetLineItemAmount = grantAllocationBudgetLineItemAmount;
        }

        /// <summary>
        /// Constructor for building a new object with MinimalConstructor required fields, using objects whenever possible
        /// </summary>
        public GrantAllocationBudgetLineItem(FundSourceAllocation fundSourceAllocation, CostType costType, decimal grantAllocationBudgetLineItemAmount) : this()
        {
            // Mark this as a new object by setting primary key with special value
            this.GrantAllocationBudgetLineItemID = ModelObjectHelpers.MakeNextUnsavedPrimaryKeyValue();
            this.GrantAllocationID = fundSourceAllocation.GrantAllocationID;
            this.FundSourceAllocation = fundSourceAllocation;
            fundSourceAllocation.GrantAllocationBudgetLineItems.Add(this);
            this.CostTypeID = costType.CostTypeID;
            this.GrantAllocationBudgetLineItemAmount = grantAllocationBudgetLineItemAmount;
        }

        /// <summary>
        /// Creates a "blank" object of this type and populates primitives with defaults
        /// </summary>
        public static GrantAllocationBudgetLineItem CreateNewBlank(FundSourceAllocation fundSourceAllocation, CostType costType)
        {
            return new GrantAllocationBudgetLineItem(fundSourceAllocation, costType, default(decimal));
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
        public static readonly List<string> DependentEntityTypeNames = new List<string> {typeof(GrantAllocationBudgetLineItem).Name};


        /// <summary>
        /// Delete just the entity 
        /// </summary>
        public void Delete(DatabaseEntities dbContext)
        {
            dbContext.GrantAllocationBudgetLineItems.Remove(this);
        }
        
        /// <summary>
        /// Delete entity plus all children
        /// </summary>
        public void DeleteFull(DatabaseEntities dbContext)
        {
            
            Delete(dbContext);
        }

        [Key]
        public int GrantAllocationBudgetLineItemID { get; set; }
        public int GrantAllocationID { get; set; }
        public int CostTypeID { get; set; }
        public decimal GrantAllocationBudgetLineItemAmount { get; set; }
        public string GrantAllocationBudgetLineItemNote { get; set; }
        [NotMapped]
        public int PrimaryKey { get { return GrantAllocationBudgetLineItemID; } set { GrantAllocationBudgetLineItemID = value; } }

        public virtual FundSourceAllocation FundSourceAllocation { get; set; }
        public CostType CostType { get { return CostType.AllLookupDictionary[CostTypeID]; } }

        public static class FieldLengths
        {

        }
    }
}