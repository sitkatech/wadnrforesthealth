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
                return this.GrantAllocation.GrantAllocationBudgetLineItems.Sum(v => v.GrantAllocationBudgetLineItemAmount);
            }
        }

        //todo: tom this is not right
        public decimal SpentAmount
        {
            get
            {
                var totalCosts = PersonnelAndBenefitsTotalCost;
                return GrantAllocationAwardAmount - 0m;
            }
        }

        public decimal Balance => GrantAllocationAwardAmount - SpentAmount;
        public Money IndirectCostApplicableAmount
        {
            get
            {
                //todo: tom fix this
                //This is the sum of the Supplies Total, Personnel&Benefits TAR amount total, and travel total
                var suppliesTotal = this.GrantAllocationAwardSuppliesLineItems.Select(x => x.GrantAllocationAwardSuppliesLineItemAmount).Sum();
                var personnelAndBenefitsTarTotal = this.GrantAllocationAwardPersonnelAndBenefitsLineItems.Select(x => (decimal)x.GrantAllocationAwardPersonnelAndBenefitsLineItemTarTotal).Sum();
                var travelTotal = this.GrantAllocationAwardTravelLineItems.Where(x => x.GrantAllocationAwardTravelLineItemAmount != null).Select(x => x.GrantAllocationAwardTravelLineItemAmount).Sum();

                return (Money)suppliesTotal + personnelAndBenefitsTarTotal + (Money)travelTotal;
            }
        }

        public Money IndirectCostTotal
        {
            get { return IndirectCostApplicableAmount * 0.287m; }
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

        public Money SuppliesAllocationBalance {
            get
            {
                Money lineItemTotal = GrantAllocationAwardSuppliesLineItems.Select(s => s.GrantAllocationAwardSuppliesLineItemAmount).Sum();
                return SuppliesAllocationTotal - lineItemTotal;
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
                //this is the sum of the TAR total(hourly total and fringe totals) of all personnel and benefit line items
                var hourlyTotal = this.GrantAllocationAwardPersonnelAndBenefitsLineItems.Select(x => (decimal)x.GrantAllocationAwardPersonnelAndBenefitsLineItemHourlyTotal).Sum();
                var fringeTotal = this.GrantAllocationAwardPersonnelAndBenefitsLineItems.Select(x => (decimal)x.GrantAllocationAwardPersonnelAndBenefitsLineItemFringeTotal).Sum();
                return (hourlyTotal + fringeTotal);
            }
        }

        public Money PersonnelAndBenefitsAllocationBalance {
            get
            {
                //this is the sum of the TAR total(hourly total and fringe totals) of all personnel and benefit line items
                var hourlyTotal = this.GrantAllocationAwardPersonnelAndBenefitsLineItems.Select(x => (decimal)x.GrantAllocationAwardPersonnelAndBenefitsLineItemHourlyTotal).Sum();
                var fringeTotal = this.GrantAllocationAwardPersonnelAndBenefitsLineItems.Select(x => (decimal)x.GrantAllocationAwardPersonnelAndBenefitsLineItemFringeTotal).Sum();
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

        public Money TravelAllocationBalance {
            get
            {
                Money lineItemTotal = GrantAllocationAwardTravelLineItems.Select(s => s.GrantAllocationAwardTravelLineItemAmount.HasValue ? s.GrantAllocationAwardTravelLineItemAmount.Value : 0).Sum();
                return TravelAllocationTotal - lineItemTotal;
            }
        }

        /// <summary>
        ///     This is the combined total for both contractual type (Landowner Cost Share and Contractor Invoice)
        /// </summary>
        public Money ContractualAllocationTotal
        {
            get
            {
                return this.GrantAllocation.GrantAllocationBudgetLineItems.Where(x => x.CostTypeID == (int)CostTypeEnum.Contractual).Sum(v => v.GrantAllocationBudgetLineItemAmount);
            }
        }

        //todo: tom fix this
        public Money LandownerCostShareAllocationBalance
        {
            get
            {
                //this is the allocation total - sum of allocated amount from landowner cost share line items 
                Money lineItemTotal = GrantAllocationAwardLandownerCostShareLineItems.Select(s => s.GrantAllocationAwardLandownerCostShareLineItemAllocatedAmount).Sum();
                //if (LandownerCostShareAllocationTotal.HasValue)
                //{
                //    return LandownerCostShareAllocationTotal.Value - lineItemTotal;
                //}

                return 0 - lineItemTotal;
            }
        }

        //todo: tom fix this
        public decimal LandownerCostSharePercentAllocated
        {
            get
            {
                //this is the sum of amount allocated from landowner cost share line items / allocation total
                Money lineItemTotal = GrantAllocationAwardLandownerCostShareLineItems.Select(s => s.GrantAllocationAwardLandownerCostShareLineItemAllocatedAmount).Sum();
                //if (LandownerCostShareAllocationTotal.HasValue)
                //{
                //    return lineItemTotal / LandownerCostShareAllocationTotal.Value;
                //}
                return 0;
            }
        }

        //todo: tom fix this
        public Money LandownerCostShareFundBalance
        {
            get
            {
                //this is the allocation total - sum of grant cost from landowner cost share line items 
                Money lineItemTotal = GrantAllocationAwardLandownerCostShareLineItems.Select(s => (decimal)s.GrantAllocationAwardLandownerCostShareLineItemGrantCost).Sum();
                //if (LandownerCostShareAllocationTotal.HasValue)
                //{
                //    return LandownerCostShareAllocationTotal.Value - lineItemTotal;
                //}

                return 0 - lineItemTotal;
            }
        }

        //todo: tom fix this
        public Money ContractorInvoiceAllocationBalance
        {
            get
            {
                //todo: tom fix this calculation
                //this is the allocation total - a sum of invoice total on contractor invoice line items
                Money lineItemTotal = 0m;// GrantAllocationAwardLandownerCostShareLineItems.Select(s => s.GrantAllocationAwardLandownerCostShareAllo .HasValue ? s.GrantAllocationAwardTravelLineItemAmount.Value : 0).Sum();
                //if (ContractorInvoiceAllocationTotal.HasValue)
                //{
                //    return ContractorInvoiceAllocationTotal.Value - lineItemTotal;
                //}

                return 0 - lineItemTotal;
            }
        }

        public Money ContractorInvoiceLandownerCostShareBalance
        {
            get
            {
                //todo: tom fix this calculation
                //this is the allocation total - a sum of invoice total on contractor invoice line items
                Money lineItemTotal = 0m;// GrantAllocationAwardLandownerCostShareLineItems.Select(s => s.GrantAllocationAwardLandownerCostShareAllo .HasValue ? s.GrantAllocationAwardTravelLineItemAmount.Value : 0).Sum();
                //if (ContractorInvoiceAllocationTotal.HasValue)
                //{
                //    return ContractorInvoiceAllocationTotal.Value - lineItemTotal;
                //}

                return 0 - lineItemTotal;
            }
        }
    }
}