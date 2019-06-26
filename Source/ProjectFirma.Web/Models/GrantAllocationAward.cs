using System;
using System.Linq;

namespace ProjectFirma.Web.Models
{
    public partial class GrantAllocationAward : IAuditableEntity
    {
        public string AuditDescriptionString => GrantAllocationAwardName;

        public Money GrantAllocationAwardAmount
        {
            get
            {
                //Sum of all the budget line items for this grant allocation
                return this.GrantAllocation.GrantAllocationBudgetLineItems.Where(x => x.CostTypeID == (int)CostTypeEnum.IndirectCosts || x.CostTypeID == (int)CostTypeEnum.Supplies || x.CostTypeID == (int)CostTypeEnum.Personnel || x.CostTypeID == (int)CostTypeEnum.Benefits || x.CostTypeID == (int)CostTypeEnum.Contractual || x.CostTypeID == (int)CostTypeEnum.Travel).Sum(v => v.GrantAllocationBudgetLineItemAmount);
            }
        }

        public decimal SpentAmount
        {
            get
            {
                var totalCosts = IndirectCostTotal + SuppliesLineItemAmountSum + PersonnelAndBenefitsTotalCost + TravelLineItemSum + ContractualLineItemSum;
                return totalCosts;
            }
        }

        public decimal Balance => GrantAllocationAwardAmount - SpentAmount;

        /// <summary>
        /// This is the sum of the Supplies Line Item Total, Personnel & Benefits TAR total, and Travel line item total
        /// </summary>
        public Money IndirectCostApplicableAmount
        {
            get
            {
                return SuppliesLineItemAmountSum + PersonnelAndBenefitsTotalCost + TravelLineItemSum;
            }
        }

        public Money IndirectCostTotal
        {
            get { return IndirectCostApplicableAmount * 0.224m; }
        }

        public Money IndirectCostAllocationTotal
        {
            get
            {
                return this.GrantAllocation.GrantAllocationBudgetLineItems.Where(x => x.CostTypeID == (int)CostTypeEnum.IndirectCosts).Sum(v => v.GrantAllocationBudgetLineItemAmount);
            }
        }

        public Money IndirectCostBalance {
            get
            {
                return IndirectCostAllocationTotal - IndirectCostTotal;
            }
        }

        public Money SuppliesAllocationTotal
        {
            get
            {
                return this.GrantAllocation.GrantAllocationBudgetLineItems.Where(x => x.CostTypeID == (int)CostTypeEnum.Supplies).Sum(v => v.GrantAllocationBudgetLineItemAmount);
            }
        }

        public Money SuppliesLineItemAmountSum
        {
            get
            {
                return GrantAllocationAwardSuppliesLineItems.Select(s => s.GrantAllocationAwardSuppliesLineItemAmount).Sum();
            }
        }

        public Money SuppliesAllocationBalance
        {
            get
            {
                return SuppliesAllocationTotal - SuppliesLineItemAmountSum;
            }
        }

        public Money PersonnelAndBenefitsAllocationTotal
        {
            get
            {
                return this.GrantAllocation.GrantAllocationBudgetLineItems.Where(x => x.CostTypeID == (int)CostTypeEnum.Personnel || x.CostTypeID == (int)CostTypeEnum.Benefits).Sum(v => v.GrantAllocationBudgetLineItemAmount);
            }
        }


        public Money PersonnelAndBenefitsTotalCost
        {
            get
            {
                return GrantAllocationAwardPersonnelAndBenefitsLineItems.Select(x => (decimal)x.GrantAllocationAwardPersonnelAndBenefitsLineItemTarTotal).Sum();
            }
        }

        public Money PersonnelAndBenefitsAllocationBalance {
            get
            {
                return PersonnelAndBenefitsAllocationTotal - PersonnelAndBenefitsTotalCost;
            }
        }

        public Money TravelAllocationTotal
        {
            get
            {
                return this.GrantAllocation.GrantAllocationBudgetLineItems.Where(x => x.CostTypeID == (int)CostTypeEnum.Travel).Sum(v => v.GrantAllocationBudgetLineItemAmount);
            }
        }

        public Money TravelLineItemSum
        {
            get
            {
                return GrantAllocationAwardTravelLineItems.Select(s => (decimal)s.GrantAllocationAwardTravelLineItemCalculatedAmount).Sum();
            }
        }

        public Money TravelAllocationBalance
        {
            get
            {
                return TravelAllocationTotal - TravelLineItemSum;
            }
        }

        /// <summary>
        ///     This is the combined total for both contractual type (Landowner Cost Share and Contractor Invoice)
        /// </summary>
        public Money ContractualAllocationTotal
        {
            get
            {
                return GrantAllocation.GrantAllocationBudgetLineItems.Where(x => x.CostTypeID == (int)CostTypeEnum.Contractual).Sum(v => v.GrantAllocationBudgetLineItemAmount);
            }
        }

        public Money ContractualAllocationBalance
        {
            get
            {
                return ContractualAllocationTotal - ContractualLineItemSum;
            }
        }


        public Money LandownerCostShareLineItemSum
        {
            get
            {
                return GrantAllocationAwardLandownerCostShareLineItems.Select(s => s.GrantAllocationAwardLandownerCostShareLineItemAllocatedAmount).Sum();
            }
        }

        public Money ContractorInvoiceItemSum
        {
            get
            {
                return GrantAllocationAwardContractorInvoices.Select(s => (decimal)s.GrantAllocationAwardContractorInvoiceTotalWithTax).Sum();
            }
        }

        public Money ContractualLineItemSum
        {
            get
            {
                return LandownerCostShareLineItemSum + ContractorInvoiceItemSum;
            }
        }
    }
}