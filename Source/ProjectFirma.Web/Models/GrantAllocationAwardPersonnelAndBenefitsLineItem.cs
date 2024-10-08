﻿using System;

namespace ProjectFirma.Web.Models
{
    public partial class GrantAllocationAwardPersonnelAndBenefitsLineItem : IAuditableEntity
    {
        public string AuditDescriptionString => $"GrantAllocationAwardPersonnelAndBenefitsLineItemID:{GrantAllocationAwardPersonnelAndBenefitsLineItemID} Note:{GrantAllocationAwardPersonnelAndBenefitsLineItemNotes}";
        public Money GrantAllocationAwardPersonnelAndBenefitsLineItemHourlyTotal
        {
            get
            {
                return GrantAllocationAwardPersonnelAndBenefitsLineItemHourlyRate * GrantAllocationAwardPersonnelAndBenefitsLineItemTarHours;
            }
        }

        public Money GrantAllocationAwardPersonnelAndBenefitsLineItemFringeTotal
        {
            get
            {
                return GrantAllocationAwardPersonnelAndBenefitsLineItemFringeRate * GrantAllocationAwardPersonnelAndBenefitsLineItemTarHours;
            }
        }

        public Money GrantAllocationAwardPersonnelAndBenefitsLineItemTarTotal
        {
            get
            {
                return GrantAllocationAwardPersonnelAndBenefitsLineItemHourlyTotal + GrantAllocationAwardPersonnelAndBenefitsLineItemFringeTotal;
            }
        }
    }
}