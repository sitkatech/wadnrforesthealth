/*-----------------------------------------------------------------------
<copyright file="EditGrantAllocationAwardTravelLineItemViewModel.cs" company="Tahoe Regional Planning Agency and Environmental Science Associates">
Copyright (c) Tahoe Regional Planning Agency and Environmental Science Associates. All rights reserved.
<author>Environmental Science Associates</author>
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
    public class EditGrantAllocationAwardTravelLineItemViewModel : FormViewModel
    {
        public int GrantAllocationAwardTravelLineItemID { get; set; }
        public int GrantAllocationAwardID { get; set; }


        [FieldDefinitionDisplay(FieldDefinitionEnum.GrantAllocationAwardTravelName)]
        [Required]
        public int? PersonID { get; set; }

        [StringLength(GrantAllocationAwardTravelLineItem.FieldLengths.GrantAllocationAwardTravelLineItemTarOrMonth)]
        [FieldDefinitionDisplay(FieldDefinitionEnum.GrantAllocationAwardTravelTarOrMonth)]
        [Required]
        public string TarOrMonth { get; set; }

        [FieldDefinitionDisplay(FieldDefinitionEnum.GrantAllocationAwardTravelDate)]
        [Required]
        public DateTime Date { get; set; }

        [FieldDefinitionDisplay(FieldDefinitionEnum.GrantAllocationAwardTravelType)]
        [Required]
        public int TypeID { get; set; }

        [FieldDefinitionDisplay(FieldDefinitionEnum.GrantAllocationAwardTravelMiles)]
        public int Miles { get; set; }

        [FieldDefinitionDisplay(FieldDefinitionEnum.GrantAllocationAwardTravelMileageRate)]
        public Money MileageRate { get; set; }

        [FieldDefinitionDisplay(FieldDefinitionEnum.GrantAllocationAwardTravelAmount)]
        public Money Amount { get; set; }

        [StringLength(GrantAllocationAwardTravelLineItem.FieldLengths.GrantAllocationAwardTravelLineItemNotes)]
        [FieldDefinitionDisplay(FieldDefinitionEnum.GrantAllocationAwardTravelNotes)]
        public string Notes { get; set; }

        

        /// <summary>
        /// Needed by the ModelBinder
        /// </summary>
        public EditGrantAllocationAwardTravelLineItemViewModel()
        {
            Date = DateTime.Today;
            //Default to Transportation Type
            TypeID = GrantAllocationAwardTravelLineItemType.Transportation.GrantAllocationAwardTravelLineItemTypeID;
        }

        public EditGrantAllocationAwardTravelLineItemViewModel(Models.GrantAllocationAwardTravelLineItem grantAllocationAwardTravelLineItem)
        {
            GrantAllocationAwardTravelLineItemID = grantAllocationAwardTravelLineItem.GrantAllocationAwardTravelLineItemID;
            GrantAllocationAwardID = grantAllocationAwardTravelLineItem.GrantAllocationAwardID;

            PersonID = grantAllocationAwardTravelLineItem.PersonID;
            TarOrMonth = grantAllocationAwardTravelLineItem.GrantAllocationAwardTravelLineItemTarOrMonth;
            if (grantAllocationAwardTravelLineItem.GrantAllocationAwardTravelLineItemDate == DateTime.MinValue)
            {
                Date = DateTime.Today;
            }
            else
            {
                Date = grantAllocationAwardTravelLineItem.GrantAllocationAwardTravelLineItemDate;
            }

            TypeID = grantAllocationAwardTravelLineItem.GrantAllocationAwardTravelLineItemTypeID;
            switch (TypeID)
            {
                case (int)GrantAllocationAwardTravelLineItemTypeEnum.Transportation:
                    Miles = grantAllocationAwardTravelLineItem.GrantAllocationAwardTravelLineItemMiles ?? 0;
                    MileageRate = grantAllocationAwardTravelLineItem.GrantAllocationAwardTravelLineItemMileageRate ?? 0m;
                    Amount = 0m;
                    break;
                case (int)GrantAllocationAwardTravelLineItemTypeEnum.Other:
                    Miles = 0;
                    MileageRate = 0m;
                    Amount = grantAllocationAwardTravelLineItem.GrantAllocationAwardTravelLineItemAmount ?? 0m;
                    break;
                default:
                    throw new Exception($"Unhandled TypeID {TypeID} for GrantAllocationAwardTravelLineItemTypeEnum in EditGrantAllocationAwardTravelLineItemViewModel");
            }
            
            Notes = grantAllocationAwardTravelLineItem.GrantAllocationAwardTravelLineItemNotes;
        }



        public void UpdateModel(Models.GrantAllocationAwardTravelLineItem grantAllocationAwardTravelLineItem)
        {
            grantAllocationAwardTravelLineItem.GrantAllocationAwardTravelLineItemID = GrantAllocationAwardTravelLineItemID;
            grantAllocationAwardTravelLineItem.GrantAllocationAwardID = GrantAllocationAwardID;
            grantAllocationAwardTravelLineItem.PersonID = PersonID;
            grantAllocationAwardTravelLineItem.GrantAllocationAwardTravelLineItemTarOrMonth = TarOrMonth;
            grantAllocationAwardTravelLineItem.GrantAllocationAwardTravelLineItemDate = Date;
            grantAllocationAwardTravelLineItem.GrantAllocationAwardTravelLineItemTypeID = TypeID;
            switch (TypeID)
            {
                case (int)GrantAllocationAwardTravelLineItemTypeEnum.Transportation:
                    grantAllocationAwardTravelLineItem.GrantAllocationAwardTravelLineItemMiles = Miles;
                    grantAllocationAwardTravelLineItem.GrantAllocationAwardTravelLineItemMileageRate = MileageRate;
                    grantAllocationAwardTravelLineItem.GrantAllocationAwardTravelLineItemAmount = null;
                    break;
                case (int)GrantAllocationAwardTravelLineItemTypeEnum.Other:
                    grantAllocationAwardTravelLineItem.GrantAllocationAwardTravelLineItemMiles = null;
                    grantAllocationAwardTravelLineItem.GrantAllocationAwardTravelLineItemMileageRate = null;
                    grantAllocationAwardTravelLineItem.GrantAllocationAwardTravelLineItemAmount = Amount;
                    break;
                default:
                    throw new Exception($"Unhandled TypeID {TypeID} for GrantAllocationAwardTravelLineItemTypeEnum in EditGrantAllocationAwardTravelLineItemViewModel");
            }

            
            grantAllocationAwardTravelLineItem.GrantAllocationAwardTravelLineItemNotes = Notes;

        }
    }

}