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
    public class GrantGridSpec : GridSpec<Models.Grant>
    {
        public static string GrantNumberHiddenColumnName = "GrantNumberAsText";

        public GrantGridSpec(Models.Person currentPerson)
        {
            ObjectNameSingular = $"{Models.FieldDefinition.Grant.GetFieldDefinitionLabel()}";
            ObjectNamePlural = $"{Models.FieldDefinition.Grant.GetFieldDefinitionLabelPluralized()}";
            SaveFiltersInCookie = true;
            var userHasDeletePermissions = new GrantDeleteFeature().HasPermissionByPerson(currentPerson);
            var userHasCreatePermissions = new GrantCreateFeature().HasPermissionByPerson(currentPerson);
            if (userHasCreatePermissions)
            {
                var contentUrl = SitkaRoute<GrantController>.BuildUrlFromExpression(t => t.New());
                CreateEntityModalDialogForm = new ModalDialogForm(contentUrl, "Create a new Grant");
            }

            CustomExcelDownloadUrl = SitkaRoute<GrantController>.BuildUrlFromExpression(tc => tc.GrantsExcelDownload());

            // hidden column for grant number for use by JavaScript
            Add(GrantNumberHiddenColumnName, x => x.GrantNumber, 0);
            if (userHasDeletePermissions)
            {
                Add(string.Empty, x => DhtmlxGridHtmlHelpers.MakeDeleteIconAndLinkBootstrap(x.GetDeleteUrl(), true, true), 30, DhtmlxGridColumnFilterType.None);
            }
            Add(string.Empty, x => UrlTemplate.MakeHrefString(x.GetDetailUrl(), FirmaDhtmlxGridHtmlHelpers.FactSheetIcon.ToString()), 30, DhtmlxGridColumnFilterType.None);
            Add(Models.FieldDefinition.GrantNumber.ToGridHeaderString(), x => UrlTemplate.MakeHrefString(x.GetDetailUrl(), x.GrantNumber), GrantAllocationGridSpec.GrantNumberColumnWidth, DhtmlxGridColumnFilterType.SelectFilterHtmlStrict);
            Add(Models.FieldDefinition.CFDA.ToGridHeaderString(), x => x.CFDANumber, 90, DhtmlxGridColumnFilterType.SelectFilterStrict);
            Add(Models.FieldDefinition.GrantTitle.ToGridHeaderString(), x => UrlTemplate.MakeHrefString(x.GetDetailUrl(), x.GrantTitle), GrantAllocationGridSpec.GrantNumberColumnWidth, DhtmlxGridColumnFilterType.SelectFilterHtmlStrict);
            Add(Models.FieldDefinition.TotalAwardAmount.ToGridHeaderString(), x => x.AwardedFunds.ToStringCurrency(), 90, DhtmlxGridColumnFilterType.SelectFilterStrict);
            Add(Models.FieldDefinition.GrantStartDate.ToGridHeaderString(), x => x.StartDateDisplay, 90, DhtmlxGridColumnFilterType.SelectFilterStrict);
            Add(Models.FieldDefinition.GrantEndDate.ToGridHeaderString(), x => x.EndDateDisplay, 90, DhtmlxGridColumnFilterType.SelectFilterStrict);
            Add(Models.FieldDefinition.GrantStatus.ToGridHeaderString(), x => x.GrantStatus.GrantStatusName, 90, DhtmlxGridColumnFilterType.SelectFilterStrict);
            Add(Models.FieldDefinition.GrantType.ToGridHeaderString(), x => x.GrantTypeDisplay, 90, DhtmlxGridColumnFilterType.SelectFilterStrict);
        }
    }
}
