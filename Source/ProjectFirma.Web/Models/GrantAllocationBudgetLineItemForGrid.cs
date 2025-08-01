using System.Collections.Generic;
using System.Linq;
using LtInfo.Common;
using ProjectFirma.Web.Common;
using ProjectFirma.Web.Controllers;

namespace ProjectFirma.Web.Models
{
    public class GrantAllocationBudgetLineItemForGrid 
    {
        public FundSourceAllocation FundSourceAllocation { get; set; }

        public decimal PersonnelAmount { get; set; }
        public decimal BenefitsAmount { get; set; }
        public decimal TravelAmount { get; set; }
        public decimal SuppliesAmount { get; set; }
        public decimal ContractualAmount { get; set; }
        public decimal IndirectCostsAmount { get; set; }
        public decimal OtherAmount { get; set; }
        public decimal EquipmentAmount { get; set; }

        public decimal TotalAmount { get; set; }


        public GrantAllocationBudgetLineItemForGrid(FundSourceAllocation fundSourceAllocation)
        {
            FundSourceAllocation = fundSourceAllocation;

            //There are DB constraints that force one and only one Budget Line Item for each Cost Type for each Allocation, therefore the .First call should never crash.
            PersonnelAmount = fundSourceAllocation.GrantAllocationBudgetLineItems.First(bli => bli.CostTypeID == CostType.Personnel.CostTypeID).GrantAllocationBudgetLineItemAmount;
            BenefitsAmount = fundSourceAllocation.GrantAllocationBudgetLineItems.First(bli => bli.CostTypeID == CostType.Benefits.CostTypeID).GrantAllocationBudgetLineItemAmount;
            TravelAmount = fundSourceAllocation.GrantAllocationBudgetLineItems.First(bli => bli.CostTypeID == CostType.Travel.CostTypeID).GrantAllocationBudgetLineItemAmount;
            SuppliesAmount = fundSourceAllocation.GrantAllocationBudgetLineItems.First(bli => bli.CostTypeID == CostType.Supplies.CostTypeID).GrantAllocationBudgetLineItemAmount;
            ContractualAmount = fundSourceAllocation.GrantAllocationBudgetLineItems.First(bli => bli.CostTypeID == CostType.Contractual.CostTypeID).GrantAllocationBudgetLineItemAmount;
            IndirectCostsAmount = fundSourceAllocation.GrantAllocationBudgetLineItems.First(bli => bli.CostTypeID == CostType.IndirectCosts.CostTypeID).GrantAllocationBudgetLineItemAmount;
            OtherAmount = fundSourceAllocation.GrantAllocationBudgetLineItems.First(bli => bli.CostTypeID == CostType.Other.CostTypeID).GrantAllocationBudgetLineItemAmount;
            EquipmentAmount = fundSourceAllocation.GrantAllocationBudgetLineItems.First(bli => bli.CostTypeID == CostType.Equipment.CostTypeID).GrantAllocationBudgetLineItemAmount;

            TotalAmount = fundSourceAllocation.GrantAllocationBudgetLineItems.Where(bli => bli.CostTypeID != CostType.Other.CostTypeID && bli.CostTypeID != CostType.Equipment.CostTypeID).Sum(bli => bli.GrantAllocationBudgetLineItemAmount);

        }
    }
}