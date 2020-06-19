using System;
using System.Linq;

namespace ProjectFirma.Web.Models
{
    public partial class GrantAllocationAwardLandownerCostShareLineItem : IAuditableEntity
    {
        public string AuditDescriptionString => GrantAllocationAwardLandownerCostShareLineItemID.ToString();
        public Money GrantAllocationAwardLandownerCostShareLineItemGrantCost
        {
            get
            {
                return GrantAllocationAwardLandownerCostShareLineItemTotalCost - GrantAllocationAwardLandownerCostShareLineItemActualMatch;
            }
        }

        public Money GrantAllocationAwardLandownerCostShareLineItemActualMatch
        {
            get
            {
                return GrantAllocationAwardLandownerCostShareLineItemTotalCost - GrantAllocationAwardLandownerCostShareLineItemAllocatedAmount;
            }
        }

        public string GetStatusDisplayName()
        {
            return this.LandownerCostShareLineItemStatus.LandownerCostShareLineItemStatusDisplayName;
        }

        public Treatment Treatment => Treatments.SingleOrDefault();

    }
}