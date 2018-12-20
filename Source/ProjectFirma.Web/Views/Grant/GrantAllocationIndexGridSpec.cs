/*-----------------------------------------------------------------------
<copyright file="IndexGridSpec.cs" company="Tahoe Regional Planning Agency and Sitka Technology Group">
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

using ProjectFirma.Web.Models;
using LtInfo.Common;
using LtInfo.Common.DhtmlWrappers;
using LtInfo.Common.Views;
using ProjectFirma.Web.Common;
using ProjectFirma.Web.Controllers;

namespace ProjectFirma.Web.Views.Grant
{
    public class GrantAllocationIndexGridSpec : GridSpec<Models.GrantAllocation>
    {
        public GrantAllocationIndexGridSpec(Person currentPerson)
        {
            ObjectNameSingular = "Grant Allocation";
            ObjectNamePlural = "Grant Allocations";
            SaveFiltersInCookie = true;

            CustomExcelDownloadUrl = SitkaRoute<GrantController>.BuildUrlFromExpression(tc => tc.GrantAllocationIndexExcelDownload());

            Add("Grant Number", x => x.GrantNumber, 90, DhtmlxGridColumnFilterType.SelectFilterStrict);
            Add("Start Date", x => x.StartDate.ToShortDateString(), 90, DhtmlxGridColumnFilterType.SelectFilterStrict);
            Add("End Date", x => x.EndDate.ToShortDateString(), 90, DhtmlxGridColumnFilterType.SelectFilterStrict);
            Add("Total Award", x => x.TotalAward.ToStringCurrency(), 90, DhtmlxGridColumnFilterType.SelectFilterStrict);
        }
    }
}
