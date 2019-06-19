using System;
using System.Linq;

namespace ProjectFirma.Web.Models
{
    public partial class GrantAllocationAward : IAuditableEntity
    {
        public string AuditDescriptionString => GrantAllocationAwardName;

        //todo: tom this is not right
        public decimal SpentAmount
        {
            get { return GrantAllocationAwardAmount - 0m; }
        }

        public decimal RemainingAmount => GrantAllocationAwardAmount - SpentAmount;
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

        public Money IndirectTotal
        {
            get { return IndirectCostApplicableAmount * 0.287m; }
        }

        public Money IndirectRemaining {
            get
            {
                if (IndirectCostAllocationTotal.HasValue)
                {
                    return IndirectCostAllocationTotal.Value - IndirectTotal;
                }

                return 0 - IndirectTotal;
            }
        }

        public Money SuppliesAllocationRemaining {
            get
            {
                Money lineItemTotal = GrantAllocationAwardSuppliesLineItems.Select(s => s.GrantAllocationAwardSuppliesLineItemAmount).Sum();
                if (SuppliesAllocationTotal.HasValue)
                {
                    return SuppliesAllocationTotal.Value - lineItemTotal;
                }

                return 0 - lineItemTotal;
            }
        }

        public Money PersonnelAndBenefitsAllocationRemaining {
            get
            {
                //this is the sum of the TAR total(hourly total and fringe totals) of all personnel and benefit line items
                var hourlyTotal = this.GrantAllocationAwardPersonnelAndBenefitsLineItems.Select(x => (decimal)x.GrantAllocationAwardPersonnelAndBenefitsLineItemHourlyTotal).Sum();
                var fringeTotal = this.GrantAllocationAwardPersonnelAndBenefitsLineItems.Select(x => (decimal)x.GrantAllocationAwardPersonnelAndBenefitsLineItemFringeTotal).Sum();
                if (PersonnelAndBenefitsAllocationTotal.HasValue)
                {
                    return PersonnelAndBenefitsAllocationTotal.Value - (hourlyTotal + fringeTotal);
                }

                return 0 - (hourlyTotal + fringeTotal);
            }
        }

        public Money TravelAllocationRemaining {
            get
            {
                Money lineItemTotal = GrantAllocationAwardTravelLineItems.Select(s => s.GrantAllocationAwardTravelLineItemAmount.HasValue ? s.GrantAllocationAwardTravelLineItemAmount.Value : 0).Sum();
                if (TravelAllocationTotal.HasValue)
                {
                    return TravelAllocationTotal.Value - lineItemTotal;
                }

                return 0 - lineItemTotal;
            }
        }

        public Money LandownerCostShareAllocationRemaining
        {
            get
            {
                //this is the allocation total - sum of allocated amount from landowner cost share line items 
                Money lineItemTotal = GrantAllocationAwardLandownerCostShareLineItems.Select(s => s.GrantAllocationAwardLandownerCostShareLineItemAllocatedAmount).Sum();
                if (LandownerCostShareAllocationTotal.HasValue)
                {
                    return LandownerCostShareAllocationTotal.Value - lineItemTotal;
                }

                return 0 - lineItemTotal;
            }
        }

        public decimal LandownerCostSharePercentAllocated
        {
            get
            {
                //this is the sum of amount allocated from landowner cost share line items / allocation total
                Money lineItemTotal = GrantAllocationAwardLandownerCostShareLineItems.Select(s => s.GrantAllocationAwardLandownerCostShareLineItemAllocatedAmount).Sum();
                if (LandownerCostShareAllocationTotal.HasValue)
                {
                    return lineItemTotal / LandownerCostShareAllocationTotal.Value;
                }
                return 0;
            }
        }

        public Money LandownerCostShareFundRemaining
        {
            get
            {
                //this is the allocation total - sum of grant cost from landowner cost share line items 
                Money lineItemTotal = GrantAllocationAwardLandownerCostShareLineItems.Select(s => (decimal)s.GrantAllocationAwardLandownerCostShareLineItemGrantCost).Sum();
                if (LandownerCostShareAllocationTotal.HasValue)
                {
                    return LandownerCostShareAllocationTotal.Value - lineItemTotal;
                }

                return 0 - lineItemTotal;
            }
        }

        public Money ContractorInvoiceAllocationRemaining
        {
            get
            {
                //todo: tom fix this calculation
                //this is the allocation total - a sum of invoice total on contractor invoice line items
                Money lineItemTotal = 0m;// GrantAllocationAwardLandownerCostShareLineItems.Select(s => s.GrantAllocationAwardLandownerCostShareAllo .HasValue ? s.GrantAllocationAwardTravelLineItemAmount.Value : 0).Sum();
                if (ContractorInvoiceAllocationTotal.HasValue)
                {
                    return ContractorInvoiceAllocationTotal.Value - lineItemTotal;
                }

                return 0 - lineItemTotal;
            }
        }

        public Money ContractorInvoiceLandownerCostShareRemaining
        {
            get
            {
                //todo: tom fix this calculation
                //this is the allocation total - a sum of invoice total on contractor invoice line items
                Money lineItemTotal = 0m;// GrantAllocationAwardLandownerCostShareLineItems.Select(s => s.GrantAllocationAwardLandownerCostShareAllo .HasValue ? s.GrantAllocationAwardTravelLineItemAmount.Value : 0).Sum();
                if (ContractorInvoiceAllocationTotal.HasValue)
                {
                    return ContractorInvoiceAllocationTotal.Value - lineItemTotal;
                }

                return 0 - lineItemTotal;
            }
        }
    }
}