/*-----------------------------------------------------------------------
<copyright file="EditGrantAllocationAwardSuppliesLineItemViewModel.cs" company="Tahoe Regional Planning Agency and Environmental Science Associates">
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
    public class EditGrantAllocationAwardSuppliesLineItemViewModel : FormViewModel
    {
        public int GrantAllocationAwardSuppliesLineItemID { get; set; }
        public int GrantAllocationAwardID { get; set; }


        [StringLength(Models.GrantAllocationAwardSuppliesLineItem.FieldLengths.GrantAllocationAwardSuppliesLineItemDescription)]
        [FieldDefinitionDisplay(FieldDefinitionEnum.GrantAllocationAwardSuppliesDescription)]
        [Required]
        public string Description { get; set; }

        [StringLength(GrantAllocationAwardSuppliesLineItem.FieldLengths.GrantAllocationAwardSuppliesLineItemTarOrMonth)]
        [FieldDefinitionDisplay(FieldDefinitionEnum.GrantAllocationAwardSuppliesTarOrMonth)]
        [Required]
        public string TarOrMonth { get; set; }

        [FieldDefinitionDisplay(FieldDefinitionEnum.GrantAllocationAwardSuppliesDate)]
        [Required]
        public DateTime Date { get; set; }

        [FieldDefinitionDisplay(FieldDefinitionEnum.GrantAllocationAwardSuppliesAmount)]
        [Required]
        public Money Amount { get; set; }

        [StringLength(GrantAllocationAwardSuppliesLineItem.FieldLengths.GrantAllocationAwardSuppliesLineItemNotes)]
        [FieldDefinitionDisplay(FieldDefinitionEnum.GrantAllocationAwardSuppliesNotes)]
        public string Notes { get; set; }

        

        /// <summary>
        /// Needed by the ModelBinder
        /// </summary>
        public EditGrantAllocationAwardSuppliesLineItemViewModel()
        {
            Date = DateTime.Today;
        }

        public EditGrantAllocationAwardSuppliesLineItemViewModel(Models.GrantAllocationAwardSuppliesLineItem grantAllocationAwardSuppliesLineItem)
        {
            GrantAllocationAwardSuppliesLineItemID = grantAllocationAwardSuppliesLineItem.GrantAllocationAwardSuppliesLineItemID;
            GrantAllocationAwardID = grantAllocationAwardSuppliesLineItem.GrantAllocationAwardID;
            Description = grantAllocationAwardSuppliesLineItem.GrantAllocationAwardSuppliesLineItemDescription;
            TarOrMonth = grantAllocationAwardSuppliesLineItem.GrantAllocationAwardSuppliesLineItemTarOrMonth;
            if (grantAllocationAwardSuppliesLineItem.GrantAllocationAwardSuppliesLineItemDate == DateTime.MinValue)
            {
                Date = DateTime.Today;
            }
            else
            {
                Date = grantAllocationAwardSuppliesLineItem.GrantAllocationAwardSuppliesLineItemDate;
            }

            Amount = grantAllocationAwardSuppliesLineItem.GrantAllocationAwardSuppliesLineItemAmount;
            Notes = grantAllocationAwardSuppliesLineItem.GrantAllocationAwardSuppliesLineItemNotes;

        }



        public void UpdateModel(Models.GrantAllocationAwardSuppliesLineItem grantAllocationAwardSuppliesLineItem)
        {
            grantAllocationAwardSuppliesLineItem.GrantAllocationAwardSuppliesLineItemID = GrantAllocationAwardSuppliesLineItemID;
            grantAllocationAwardSuppliesLineItem.GrantAllocationAwardID = GrantAllocationAwardID;
            grantAllocationAwardSuppliesLineItem.GrantAllocationAwardSuppliesLineItemDescription = Description;
            grantAllocationAwardSuppliesLineItem.GrantAllocationAwardSuppliesLineItemTarOrMonth = TarOrMonth;
            grantAllocationAwardSuppliesLineItem.GrantAllocationAwardSuppliesLineItemDate = Date;
            grantAllocationAwardSuppliesLineItem.GrantAllocationAwardSuppliesLineItemAmount = Amount;
            grantAllocationAwardSuppliesLineItem.GrantAllocationAwardSuppliesLineItemNotes = Notes;

        }
    }

}