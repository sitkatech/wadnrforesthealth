/*-----------------------------------------------------------------------
<copyright file="GrantAllocationBudgetVsActualsViewData.cs" company="Tahoe Regional Planning Agency and Sitka Technology Group">
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
using ProjectFirma.Web.Common;
using ProjectFirma.Web.Controllers;
using ProjectFirma.Web.Models;

namespace ProjectFirma.Web.Views.GrantAllocation
{
    public class GrantAllocationBudgetVsActualsViewData : FirmaViewData
    {
        public GrantAllocationBudgetVsActualsGridSpec GrantAllocationBudgetVsActualsGridSpec { get; }
        public string GrantAllocationBudgetVsActualsGridDataUrl { get; }

        public readonly string GrantAllocationBudgetVsActualsGridName = "grantAllocationBudgetVsActualGrid";


        public GrantAllocationBudgetVsActualsViewData(Person currentPerson, Models.GrantAllocation grantAllocation) : base(currentPerson)
        {
            GrantAllocationBudgetVsActualsGridSpec = new GrantAllocationBudgetVsActualsGridSpec();
            GrantAllocationBudgetVsActualsGridDataUrl = SitkaRoute<GrantAllocationController>.BuildUrlFromExpression(gac => gac.GrantAllocationBudgetVsActualsGridJsonData(grantAllocation));

        }
    }

    public class BudgetVsActualLineItem
    {
        public CostType CostType { get; set; }
        public Money Budget { get; set; }
        public Money ExpendituresFromDatamart { get; set; }
        public Money InvoicedToDate { get; set; }

        public Money BudgetMinusExpendituresFromDatamart => Budget - ExpendituresFromDatamart;
        public Money BudgetMinusInvoicedToDate => Budget - InvoicedToDate;

        public BudgetVsActualLineItem(CostType costType, Money budget, Money expendituresFromDatamart, Money invoicedToDate)
        {
            CostType = costType;
            Budget = budget;
            ExpendituresFromDatamart = expendituresFromDatamart;
            InvoicedToDate = invoicedToDate;
        }

        public BudgetVsActualLineItem(Money budget, Money expendituresFromDatamart, Money invoicedToDate)
        {
            Budget = budget;
            ExpendituresFromDatamart = expendituresFromDatamart;
            InvoicedToDate = invoicedToDate;
        }
    }
}
