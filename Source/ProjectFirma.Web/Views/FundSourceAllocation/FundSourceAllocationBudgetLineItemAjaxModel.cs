/*-----------------------------------------------------------------------
<copyright file="FundSourceAllocationBudgetLineItemAjaxModel.cs" company="Tahoe Regional Planning Agency and Environmental Science Associates">
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
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using LtInfo.Common;

namespace ProjectFirma.Web.Views.FundSourceAllocation
{
    public class FundSourceAllocationBudgetLineItemAjaxModel
    {
        public int FundSourceAllocationID { get; set; }

        public int FundSourceAllocationBudgetLineItemID { get; set; }

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
        public FundSourceAllocationBudgetLineItemAjaxModel()
        {
        }

    }
}
