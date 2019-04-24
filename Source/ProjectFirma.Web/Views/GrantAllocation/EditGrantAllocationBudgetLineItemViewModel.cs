/*-----------------------------------------------------------------------
<copyright file="EditProjectViewModel.cs" company="Tahoe Regional Planning Agency and Sitka Technology Group">
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
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using LtInfo.Common;
using LtInfo.Common.Models;
using ProjectFirma.Web.Common;
using ProjectFirma.Web.Models;

namespace ProjectFirma.Web.Views.GrantAllocation
{
    public class EditGrantAllocationBudgetLineItemViewModel : FormViewModel
    {
        public int GrantAllocationID { get; set; }

        public int GrantAllocationBudgetLineItemID { get; set; }

        [Required]
        [DisplayName("Budget Line Item Amount")]
        [ValidateMoneyInRangeForSqlServer]
        public Money GrantAllocationBudgetLineItemAmount { get; set; }

        [DisplayName("Note")]
        public string GrantAllocationBudgetLineItemNote { get; set; }

        [Required]
        [DisplayName("Cost Type")]
        public int CostTypeID { get; set; }


        /// <summary>
        /// Needed by the ModelBinder
        /// </summary>
        public EditGrantAllocationBudgetLineItemViewModel()
        {
        }

        public EditGrantAllocationBudgetLineItemViewModel(Models.GrantAllocation grantAllocation)
        {
            GrantAllocationID = grantAllocation.GrantAllocationID;

        }

        public EditGrantAllocationBudgetLineItemViewModel(GrantAllocationBudgetLineItem grantAllocationBudgetLineItem)
        {
            GrantAllocationID = grantAllocationBudgetLineItem.GrantAllocationID;
            GrantAllocationBudgetLineItemID = grantAllocationBudgetLineItem.GrantAllocationBudgetLineItemID;
            GrantAllocationBudgetLineItemAmount = grantAllocationBudgetLineItem.GrantAllocationBudgetLineItemAmount;
            GrantAllocationBudgetLineItemNote = grantAllocationBudgetLineItem.GrantAllocationBudgetLineItemNote;
            CostTypeID = grantAllocationBudgetLineItem.CostTypeID;
        }

        public void UpdateModel(GrantAllocationBudgetLineItem grantAllocationBudgetLineItem)
        {
            grantAllocationBudgetLineItem.GrantAllocationID = GrantAllocationID;
            grantAllocationBudgetLineItem.GrantAllocationBudgetLineItemID = GrantAllocationBudgetLineItemID;
            grantAllocationBudgetLineItem.GrantAllocationBudgetLineItemAmount = GrantAllocationBudgetLineItemAmount;
            grantAllocationBudgetLineItem.GrantAllocationBudgetLineItemNote = GrantAllocationBudgetLineItemNote;
            grantAllocationBudgetLineItem.CostTypeID = CostTypeID;
        }

    }
}