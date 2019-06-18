using System;

namespace ProjectFirma.Web.Models
{
    public partial class GrantAllocationAwardTravelLineItem : IAuditableEntity
    {
        public string AuditDescriptionString => GrantAllocationAwardTravelLineItemDescription;

        public Money GrantAllocationAwardTravelLineItemAmountForDisplay
        {
            get
            {
                if (this.GrantAllocationAwardTravelLineItemTypeID == (int) GrantAllocationAwardTravelLineItemTypeEnum.Transportation)
                {
                    return (Money) ((GrantAllocationAwardTravelLineItemMiles ?? 0) * GrantAllocationAwardTravelLineItemMileageRate);
                }
                else
                {
                    return (Money) (GrantAllocationAwardTravelLineItemAmount ?? 0m);
                }
                
            }
        }
    }
}