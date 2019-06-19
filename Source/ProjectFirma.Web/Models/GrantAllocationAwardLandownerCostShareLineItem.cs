using System;

namespace ProjectFirma.Web.Models
{
    public partial class GrantAllocationAwardLandownerCostShareLineItem : IAuditableEntity
    {
        public string AuditDescriptionString => GrantAllocationAwardLandownerCostShareLineItemID.ToString();
        public Money GrantAllocationAwardLandownerCostShareLineItemGrantCost
        {
            get
            {
                //TotalCost <(2 *AllocatedAmount) ? 0.5*TotalCost : AllocatedAmount
                return GrantAllocationAwardLandownerCostShareLineItemTotalCost < (2 * GrantAllocationAwardLandownerCostShareLineItemAllocatedAmount) ? .5m * GrantAllocationAwardLandownerCostShareLineItemTotalCost : GrantAllocationAwardLandownerCostShareLineItemAllocatedAmount;
            }
        }

        public string GetStatusDisplayName()
        {
            return this.LandownerCostShareLineItemStatus.LandownerCostShareLineItemStatusDisplayName;
        }
    }
}