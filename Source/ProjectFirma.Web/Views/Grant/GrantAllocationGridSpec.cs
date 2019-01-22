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

using System;
using ProjectFirma.Web.Models;
using LtInfo.Common;
using LtInfo.Common.DhtmlWrappers;
using LtInfo.Common.Views;
using ProjectFirma.Web.Common;
using ProjectFirma.Web.Controllers;

namespace ProjectFirma.Web.Views.Grant
{

    public class GrantAllocationGridSpec : GridSpec<Models.GrantAllocation>
    {
        public const int GrantNumberColumnWidth = 140;

        public GrantAllocationGridSpec(Person currentPerson)
        {
            ObjectNameSingular = $"{Models.FieldDefinition.GrantAllocation.GetFieldDefinitionLabel()}";
            ObjectNamePlural = $"{Models.FieldDefinition.GrantAllocation.GetFieldDefinitionLabelPluralized()}"; ;
            SaveFiltersInCookie = true;

            CustomExcelDownloadUrl = SitkaRoute<GrantController>.BuildUrlFromExpression(tc => tc.GrantsExcelDownload());

            Add("Grant Number", x => x.Grant.GrantNumber, GrantNumberColumnWidth, DhtmlxGridColumnFilterType.SelectFilterStrict);
            Add("CFDA Number", x => x.Grant.CFDANumber, 90, DhtmlxGridColumnFilterType.SelectFilterStrict);
            Add("Grant Allocation Title", x => UrlTemplate.MakeHrefString(x.GetDetailUrl(), x.ProjectName), GrantAllocationGridSpec.GrantNumberColumnWidth, DhtmlxGridColumnFilterType.Text);
            Add("Program Manager", x => x.ProgramManagerPerson != null ? x.ProgramManagerPerson.FullNameFirstLast : string.Empty, 150, DhtmlxGridColumnFilterType.SelectFilterStrict);
            Add("Start Date", x => x.StartDateDisplay, 90, DhtmlxGridColumnFilterType.SelectFilterStrict);
            Add("End Date", x => x.EndDateDisplay, 90, DhtmlxGridColumnFilterType.SelectFilterStrict);
            Add("Status", x => x.Grant.GrantStatus.GrantStatusName, 90, DhtmlxGridColumnFilterType.SelectFilterStrict);
            Add("Region", x => x.Region != null ? x.Region.RegionName : string.Empty, 90, DhtmlxGridColumnFilterType.SelectFilterStrict);
            Add("Federal Fund Code", x => x.FederalFundCode != null ? x.FederalFundCode.FederalFundCodeAbbrev : string.Empty, 90, DhtmlxGridColumnFilterType.SelectFilterStrict); 
            Add("Allocation Amount", x => x.AllocationAmount, 90, DhtmlxGridColumnFormatType.Currency, DhtmlxGridColumnAggregationType.Total);
            Add("Project Codes", x => x.ProjectCodesString, 90, DhtmlxGridColumnFilterType.Text);
            Add("Program Index", x => x.ProgramIndex != null ? x.ProgramIndex.ProgramIndexAbbrev : string.Empty, 90, DhtmlxGridColumnFilterType.SelectFilterStrict);
            Add("Organization", x => x.Organization != null ? x.Organization.OrganizationName : string.Empty, 90, DhtmlxGridColumnFilterType.SelectFilterStrict);
        }

    }
}
