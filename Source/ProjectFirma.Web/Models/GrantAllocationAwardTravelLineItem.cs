using System;

namespace ProjectFirma.Web.Models
{
    public partial class GrantAllocationAwardTravelLineItem : IAuditableEntity
    {
        public string AuditDescriptionString => $"GrantAllocationAwardTravelLineItemID: {GrantAllocationAwardTravelLineItemID}";

        public Money GrantAllocationAwardTravelLineItemCalculatedAmount
        {
            get
            {
                if (this.GrantAllocationAwardTravelLineItemTypeID == (int) GrantAllocationAwardTravelLineItemTypeEnum.Transportation)
                {
                    return (GrantAllocationAwardTravelLineItemMiles ?? 0) * (GrantAllocationAwardTravelLineItemMileageRate ?? 0);
                }

                return GrantAllocationAwardTravelLineItemAmount ?? 0m;

            }
        }
    }
}