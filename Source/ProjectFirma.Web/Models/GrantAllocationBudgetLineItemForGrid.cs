using System.Collections.Generic;
using System.Linq;
using LtInfo.Common;
using ProjectFirma.Web.Common;
using ProjectFirma.Web.Controllers;

namespace ProjectFirma.Web.Models
{
    public class FundSourceAllocationBudgetLineItemForGrid 
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


        public FundSourceAllocationBudgetLineItemForGrid(FundSourceAllocation fundSourceAllocation)
        {
            FundSourceAllocation = fundSourceAllocation;

            //There are DB constraints that force one and only one Budget Line Item for each Cost Type for each Allocation, therefore the .First call should never crash.
            PersonnelAmount = fundSourceAllocation.FundSourceAllocationBudgetLineItems.First(bli => bli.CostTypeID == CostType.Personnel.CostTypeID).FundSourceAllocationBudgetLineItemAmount;
            BenefitsAmount = fundSourceAllocation.FundSourceAllocationBudgetLineItems.First(bli => bli.CostTypeID == CostType.Benefits.CostTypeID).FundSourceAllocationBudgetLineItemAmount;
            TravelAmount = fundSourceAllocation.FundSourceAllocationBudgetLineItems.First(bli => bli.CostTypeID == CostType.Travel.CostTypeID).FundSourceAllocationBudgetLineItemAmount;
            SuppliesAmount = fundSourceAllocation.FundSourceAllocationBudgetLineItems.First(bli => bli.CostTypeID == CostType.Supplies.CostTypeID).FundSourceAllocationBudgetLineItemAmount;
            ContractualAmount = fundSourceAllocation.FundSourceAllocationBudgetLineItems.First(bli => bli.CostTypeID == CostType.Contractual.CostTypeID).FundSourceAllocationBudgetLineItemAmount;
            IndirectCostsAmount = fundSourceAllocation.FundSourceAllocationBudgetLineItems.First(bli => bli.CostTypeID == CostType.IndirectCosts.CostTypeID).FundSourceAllocationBudgetLineItemAmount;
            OtherAmount = fundSourceAllocation.FundSourceAllocationBudgetLineItems.First(bli => bli.CostTypeID == CostType.Other.CostTypeID).FundSourceAllocationBudgetLineItemAmount;
            EquipmentAmount = fundSourceAllocation.FundSourceAllocationBudgetLineItems.First(bli => bli.CostTypeID == CostType.Equipment.CostTypeID).FundSourceAllocationBudgetLineItemAmount;

            TotalAmount = fundSourceAllocation.FundSourceAllocationBudgetLineItems.Where(bli => bli.CostTypeID != CostType.Other.CostTypeID && bli.CostTypeID != CostType.Equipment.CostTypeID).Sum(bli => bli.FundSourceAllocationBudgetLineItemAmount);

        }
    }
}