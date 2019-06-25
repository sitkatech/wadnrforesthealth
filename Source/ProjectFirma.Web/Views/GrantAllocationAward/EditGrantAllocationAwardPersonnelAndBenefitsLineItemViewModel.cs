/*-----------------------------------------------------------------------
<copyright file="EditGrantAllocationAwardPersonnelAndBenefitsLineItemViewModel.cs" company="Tahoe Regional Planning Agency and Sitka Technology Group">
Copyright (c) Tahoe Regional Planning Agency and Sitka Technology Group. All rights reserved.
<author>Sitka Technology Group</author>
</copyright>

<license>
This program is free software: you can redistribute it and/or modify
it under the terms of the GNU Affero General Public License as published by
the Free Software Foundation, either version 3 of the License, or
(at your option) any later version.

This program is distributed in the hope that it will be useful,
but WITHOUT ANY WARRANTY; without even the implied warranty of
MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
GNU Affero General Public License <http://www.gnu.org/licenses/> for more details.

Source code is available upon request via <support@sitkatech.com>.
</license>
-----------------------------------------------------------------------*/
using System;
using System.ComponentModel.DataAnnotations;
using LtInfo.Common.Models;
using ProjectFirma.Web.Common;
using ProjectFirma.Web.Models;

namespace ProjectFirma.Web.Views.GrantAllocationAward
{
    public class EditGrantAllocationAwardPersonnelAndBenefitsLineItemViewModel : FormViewModel
    {
        public int GrantAllocationAwardPersonnelAndBenefitsLineItemID { get; set; }
        public int GrantAllocationAwardID { get; set; }


        [FieldDefinitionDisplay(FieldDefinitionEnum.GrantAllocationAwardPersonnelAndBenefitsName)]
        [Required]
        public int? PersonID { get; set; }

        [StringLength(GrantAllocationAwardPersonnelAndBenefitsLineItem.FieldLengths.GrantAllocationAwardPersonnelAndBenefitsLineItemTarOrMonth)]
        [FieldDefinitionDisplay(FieldDefinitionEnum.GrantAllocationAwardPersonnelAndBenefitsTarOrMonth)]
        [Required]
        public string TarOrMonth { get; set; }

        [FieldDefinitionDisplay(FieldDefinitionEnum.GrantAllocationAwardPersonnelAndBenefitsDate)]
        [Required]
        public DateTime Date { get; set; }

        [FieldDefinitionDisplay(FieldDefinitionEnum.GrantAllocationAwardPersonnelAndBenefitsTarHours)]
        [Required]
        public int TarHours { get; set; }

        [FieldDefinitionDisplay(FieldDefinitionEnum.GrantAllocationAwardPersonnelAndBenefitsHourlyRate)]
        [Required]
        public Money HourlyRate { get; set; }

        [FieldDefinitionDisplay(FieldDefinitionEnum.GrantAllocationAwardPersonnelAndBenefitsFringeRate)]
        [Required]
        public Money FringeRate { get; set; }

        [StringLength(GrantAllocationAwardPersonnelAndBenefitsLineItem.FieldLengths.GrantAllocationAwardPersonnelAndBenefitsLineItemNotes)]
        [FieldDefinitionDisplay(FieldDefinitionEnum.GrantAllocationAwardPersonnelAndBenefitsNotes)]
        public string Notes { get; set; }

        

        /// <summary>
        /// Needed by the ModelBinder
        /// </summary>
        public EditGrantAllocationAwardPersonnelAndBenefitsLineItemViewModel()
        {
            Date = DateTime.Today;
        }

        public EditGrantAllocationAwardPersonnelAndBenefitsLineItemViewModel(Models.GrantAllocationAwardPersonnelAndBenefitsLineItem grantAllocationAwardPersonnelAndBenefitsLineItem)
        {
            GrantAllocationAwardPersonnelAndBenefitsLineItemID = grantAllocationAwardPersonnelAndBenefitsLineItem.GrantAllocationAwardPersonnelAndBenefitsLineItemID;
            GrantAllocationAwardID = grantAllocationAwardPersonnelAndBenefitsLineItem.GrantAllocationAwardID;

            PersonID = grantAllocationAwardPersonnelAndBenefitsLineItem.PersonID;
            TarOrMonth = grantAllocationAwardPersonnelAndBenefitsLineItem.GrantAllocationAwardPersonnelAndBenefitsLineItemTarOrMonth;
            if (grantAllocationAwardPersonnelAndBenefitsLineItem.GrantAllocationAwardPersonnelAndBenefitsLineItemDate == DateTime.MinValue)
            {
                Date = DateTime.Today;
            }
            else
            {
                Date = grantAllocationAwardPersonnelAndBenefitsLineItem.GrantAllocationAwardPersonnelAndBenefitsLineItemDate;
            }
            TarHours = grantAllocationAwardPersonnelAndBenefitsLineItem.GrantAllocationAwardPersonnelAndBenefitsLineItemTarHours;
            HourlyRate = grantAllocationAwardPersonnelAndBenefitsLineItem.GrantAllocationAwardPersonnelAndBenefitsLineItemHourlyRate;
            FringeRate = grantAllocationAwardPersonnelAndBenefitsLineItem.GrantAllocationAwardPersonnelAndBenefitsLineItemFringeRate;
            Notes = grantAllocationAwardPersonnelAndBenefitsLineItem.GrantAllocationAwardPersonnelAndBenefitsLineItemNotes;

        }



        public void UpdateModel(Models.GrantAllocationAwardPersonnelAndBenefitsLineItem grantAllocationAwardPersonnelAndBenefitsLineItem)
        {
            grantAllocationAwardPersonnelAndBenefitsLineItem.GrantAllocationAwardPersonnelAndBenefitsLineItemID = GrantAllocationAwardPersonnelAndBenefitsLineItemID;
            grantAllocationAwardPersonnelAndBenefitsLineItem.GrantAllocationAwardID = GrantAllocationAwardID;
            grantAllocationAwardPersonnelAndBenefitsLineItem.PersonID = PersonID;
            grantAllocationAwardPersonnelAndBenefitsLineItem.GrantAllocationAwardPersonnelAndBenefitsLineItemTarOrMonth = TarOrMonth;
            grantAllocationAwardPersonnelAndBenefitsLineItem.GrantAllocationAwardPersonnelAndBenefitsLineItemDate = Date;
            grantAllocationAwardPersonnelAndBenefitsLineItem.GrantAllocationAwardPersonnelAndBenefitsLineItemTarHours = TarHours;
            grantAllocationAwardPersonnelAndBenefitsLineItem.GrantAllocationAwardPersonnelAndBenefitsLineItemHourlyRate = HourlyRate;
            grantAllocationAwardPersonnelAndBenefitsLineItem.GrantAllocationAwardPersonnelAndBenefitsLineItemFringeRate = FringeRate;
            grantAllocationAwardPersonnelAndBenefitsLineItem.GrantAllocationAwardPersonnelAndBenefitsLineItemNotes = Notes;

        }
    }

}