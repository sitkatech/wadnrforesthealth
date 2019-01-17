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

using LtInfo.Common;
using LtInfo.Common.DhtmlWrappers;
using LtInfo.Common.Views;
using ProjectFirma.Web.Common;
using ProjectFirma.Web.Controllers;

namespace ProjectFirma.Web.Views.Grant
{
    public class GrantGridSpec : GridSpec<Models.Grant>
    {
        public GrantGridSpec(Models.Person currentPerson)
        {
            ObjectNameSingular = $"{Models.FieldDefinition.Grant.GetFieldDefinitionLabel()}";
            ObjectNamePlural = $"{Models.FieldDefinition.Grant.GetFieldDefinitionLabelPluralized()}";
            SaveFiltersInCookie = true;

            CustomExcelDownloadUrl = SitkaRoute<GrantController>.BuildUrlFromExpression(tc => tc.GrantsExcelDownload());

            Add("Grant Number", x => x.GrantNumber, GrantAllocationGridSpec.GrantNumberColumnWidth, DhtmlxGridColumnFilterType.SelectFilterStrict);
            Add("Start Date", x => x.StartDate.HasValue ? x.StartDate.Value.ToShortDateString() : string.Empty, 90, DhtmlxGridColumnFilterType.SelectFilterStrict);
            Add("End Date", x => x.EndDate.HasValue ? x.EndDate.Value.ToShortDateString() : string.Empty, 90, DhtmlxGridColumnFilterType.SelectFilterStrict);
            Add("Awarded Funds", x => x.AwardedFunds.ToStringCurrency(), 90, DhtmlxGridColumnFilterType.SelectFilterStrict);
        }
    }
}
