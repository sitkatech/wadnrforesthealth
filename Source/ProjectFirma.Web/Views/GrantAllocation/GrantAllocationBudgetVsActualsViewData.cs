/*-----------------------------------------------------------------------
<copyright file="FundSourceAllocationBudgetVsActualsViewData.cs" company="Tahoe Regional Planning Agency and Environmental Science Associates">
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
using ProjectFirma.Web.Common;
using ProjectFirma.Web.Controllers;
using ProjectFirma.Web.Models;

namespace ProjectFirma.Web.Views.FundSourceAllocation
{
    public class FundSourceAllocationBudgetVsActualsViewData : FirmaViewData
    {
        public FundSourceAllocationBudgetVsActualsGridSpec FundSourceAllocationBudgetVsActualsGridSpec { get; }
        public string FundSourceAllocationBudgetVsActualsGridDataUrl { get; }

        public readonly string FundSourceAllocationBudgetVsActualsGridName = "fundSourceAllocationBudgetVsActualGrid";


        public FundSourceAllocationBudgetVsActualsViewData(Person currentPerson, Models.FundSourceAllocation fundSourceAllocation) : base(currentPerson)
        {
            FundSourceAllocationBudgetVsActualsGridSpec = new FundSourceAllocationBudgetVsActualsGridSpec(currentPerson);
            FundSourceAllocationBudgetVsActualsGridDataUrl = SitkaRoute<FundSourceAllocationController>.BuildUrlFromExpression(gac => gac.FundSourceAllocationBudgetVsActualsGridJsonData(fundSourceAllocation));

        }
    }

    public class BudgetVsActualLineItem
    {
        public CostType CostType { get; set; }
        public Money Budget { get; set; }
        public Money ExpendituresFromDatamart { get; set; }


        public Money BudgetMinusExpendituresFromDatamart => Budget - ExpendituresFromDatamart;

        public BudgetVsActualLineItem(CostType costType, Money budget, Money expendituresFromDatamart)
        {
            CostType = costType;
            Budget = budget;
            ExpendituresFromDatamart = expendituresFromDatamart;

        }

        public BudgetVsActualLineItem(Money budget, Money expendituresFromDatamart)
        {
            Budget = budget;
            ExpendituresFromDatamart = expendituresFromDatamart;

        }
    }
}
