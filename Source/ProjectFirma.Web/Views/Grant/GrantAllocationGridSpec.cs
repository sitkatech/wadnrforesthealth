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
using LtInfo.Common.HtmlHelperExtensions;
using LtInfo.Common.ModalDialog;
using LtInfo.Common.Views;
using ProjectFirma.Web.Common;
using ProjectFirma.Web.Controllers;
using ProjectFirma.Web.Models;
using ProjectFirma.Web.Security;

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
            var userHasDeletePermissions = new GrantAllocationDeleteFeature().HasPermissionByPerson(currentPerson);
            var userHasCreatePermissions = new GrantCreateFeature().HasPermissionByPerson(currentPerson);
            if (userHasCreatePermissions)
            {
                var contentUrl = SitkaRoute<GrantAllocationController>.BuildUrlFromExpression(t => t.New());
                CreateEntityModalDialogForm = new ModalDialogForm(contentUrl, 950, "Create a new Grant Allocation");
            }

            //CustomExcelDownloadUrl = SitkaRoute<GrantController>.BuildUrlFromExpression(tc => tc.GrantsExcelDownload());

            if (userHasDeletePermissions)
            {
                Add(string.Empty, x => DhtmlxGridHtmlHelpers.MakeDeleteIconAndLinkBootstrap(x.GetDeleteUrl(), true, true), 30, DhtmlxGridColumnFilterType.None);
            }
            Add(Models.FieldDefinition.GrantNumber.ToGridHeaderString(), x => x.Grant.GrantNumber, GrantNumberColumnWidth, DhtmlxGridColumnFilterType.SelectFilterStrict);
            Add(Models.FieldDefinition.ProjectName.ToGridHeaderString(), x => UrlTemplate.MakeHrefString(x.GetDetailUrl(), x.ProjectName), GrantNumberColumnWidth, DhtmlxGridColumnFilterType.Text);
            Add(Models.FieldDefinition.ProgramManager.ToGridHeaderString(), x => x.ProgramManagerPerson != null ? x.ProgramManagerPerson.FullNameFirstLast : string.Empty, 150, DhtmlxGridColumnFilterType.SelectFilterStrict);
            Add(Models.FieldDefinition.GrantStartDate.ToGridHeaderString(), x => x.StartDateDisplay, 90, DhtmlxGridColumnFilterType.SelectFilterStrict);
            Add(Models.FieldDefinition.GrantEndDate.ToGridHeaderString(), x => x.EndDateDisplay, 90, DhtmlxGridColumnFilterType.SelectFilterStrict);
            Add($"Parent Grant {Models.FieldDefinition.GrantStatus.ToGridHeaderString()}", x => x.Grant.GrantStatus.GrantStatusName, 90, DhtmlxGridColumnFilterType.SelectFilterStrict);
            Add(Models.FieldDefinition.Region.ToGridHeaderString(), x => x.Region != null ? x.Region.RegionName : string.Empty, 90, DhtmlxGridColumnFilterType.SelectFilterStrict);
            Add(Models.FieldDefinition.FederalFundCode.ToGridHeaderString(), x => x.FederalFundCode != null ? x.FederalFundCode.FederalFundCodeAbbrev : string.Empty, 90, DhtmlxGridColumnFilterType.SelectFilterStrict);
            Add(Models.FieldDefinition.AllocationAmount.ToGridHeaderString(), x => x.AllocationAmount, 90, DhtmlxGridColumnFormatType.Currency, DhtmlxGridColumnAggregationType.Total);
            Add(Models.FieldDefinition.ProgramIndex.ToGridHeaderString(), x => x.ProgramIndex != null ? x.ProgramIndex.ProgramIndexAbbrev : string.Empty, 90, DhtmlxGridColumnFilterType.SelectFilterStrict);
            Add(Models.FieldDefinition.ProjectCode.ToGridHeaderString(), x => x.ProjectCodesAsCsvString, 90, DhtmlxGridColumnFilterType.Text);
            Add(Models.FieldDefinition.Organization.ToGridHeaderString(), x => x.Organization != null ? x.Organization.OrganizationName : string.Empty, 90, DhtmlxGridColumnFilterType.SelectFilterStrict);
        }

    }
}
