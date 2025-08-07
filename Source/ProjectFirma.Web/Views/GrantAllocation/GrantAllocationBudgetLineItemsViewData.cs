/*-----------------------------------------------------------------------
<copyright file="EditProjectViewData.cs" company="Tahoe Regional Planning Agency and Environmental Science Associates">
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
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using LtInfo.Common;
using ProjectFirma.Web.Common;
using ProjectFirma.Web.Controllers;
using ProjectFirma.Web.Models;
using ProjectFirma.Web.Security;

namespace ProjectFirma.Web.Views.GrantAllocation
{
    public class GrantAllocationBudgetLineItemsViewData : FirmaUserControlViewData
    {
        public List<CostType> CostTypes { get; }
        public string FormPostUrl { get; }
        public int GrantAllocationID { get; }
        public List<GrantAllocationBudgetLineItem> GrantAllocationBudgetLineItems { get; }
        public bool PersonHasPermissionToEditBudgetLineItems { get; }

        public GrantAllocationBudgetLineItemsViewData(Person currentPerson, Models.FundSourceAllocation fundSourceAllocationBeingEdited, List<GrantAllocationBudgetLineItem> grantAllocationBudgetLineItems)
        {
            CostTypes = CostType.GetLineItemCostTypes();
            GrantAllocationID = fundSourceAllocationBeingEdited.GrantAllocationID;
            GrantAllocationBudgetLineItems = grantAllocationBudgetLineItems.OrderBy(x => x.CostType.SortOrder).ToList();

            PersonHasPermissionToEditBudgetLineItems = new FundSourceAllocationBudgetLineItemEditAsAdminFeature().HasPermissionByPerson(currentPerson);
            //This will prevent the JS from posting back if the user doesn't have permission to edit the budget line items
            if (PersonHasPermissionToEditBudgetLineItems)
            {
                FormPostUrl = SitkaRoute<GrantAllocationController>.BuildUrlFromExpression(x =>
                    x.EditGrantAllocationBudgetLineItemAjax(fundSourceAllocationBeingEdited.PrimaryKey));
            }
        }
    }

    public class GrantAllocationBudgetLineItemAjaxModel
    {
        public int GrantAllocationID { get; set; }

        public int GrantAllocationBudgetLineItemID { get; set; }

        [Required]
        [DisplayName("Budget Line Item Amount")]
        [ValidateMoneyInRangeForSqlServer]
        public Money LineItemAmount { get; set; }

        [DisplayName("Note")]
        public string LineItemNote { get; set; }

        [Required]
        [DisplayName("Cost Type")]
        public int CostTypeID { get; set; }

        /// <summary>
        /// Needed by the ModelBinder
        /// </summary>
        public GrantAllocationBudgetLineItemAjaxModel()
        {
        }

    }
}
