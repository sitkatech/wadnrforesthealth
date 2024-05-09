/*-----------------------------------------------------------------------
<copyright file="GrantGridSpec.cs" company="Tahoe Regional Planning Agency and Environmental Science Associates">
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

using LtInfo.Common;
using LtInfo.Common.AgGridWrappers;
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
                CreateEntityModalDialogForm = new ModalDialogForm(contentUrl, 950, "Create a new Grant");
            }

            CustomExcelDownloadUrl = SitkaRoute<GrantController>.BuildUrlFromExpression(tc => tc.GrantsExcelDownload());

            // hidden column for grant number for use by JavaScript
            Add(GrantNumberHiddenColumnName, x => x.GrantNumber, 0);
            if (userHasDeletePermissions)
            {
                Add(string.Empty, x => AgGridHtmlHelpers.MakeDeleteIconAndLinkBootstrap(x.GetDeleteUrl(), true, true), 30, AgGridColumnFilterType.None);
            }

            if (userHasCreatePermissions)
            {
                Add(string.Empty, x => AgGridHtmlHelpers.MakeDuplicateIconAndLinkBootstrap(x.GetDuplicateUrl(), 950, $"Duplicate {Models.FieldDefinition.Grant.GetFieldDefinitionLabel()} \"{x.GrantName}\" to New {Models.FieldDefinition.Grant.GetFieldDefinitionLabel()}"), 30, AgGridColumnFilterType.None);
            }

            //Add(string.Empty, x => UrlTemplate.MakeHrefString(x.GetDetailUrl(), AgGridHtmlHelpers.FactSheetIcon.ToString()), 30, AgGridColumnFilterType.None);
            Add(Models.FieldDefinition.GrantNumber.ToGridHeaderString(), x => UrlTemplate.MakeHrefString(x.GetDetailUrl(), x.GrantNumber), GrantAllocationGridSpec.GrantNumberColumnWidth, AgGridColumnFilterType.Html);
            Add(Models.FieldDefinition.CFDA.ToGridHeaderString(), x => x.CFDANumber, 90, AgGridColumnFilterType.Html);
            Add(Models.FieldDefinition.GrantName.ToGridHeaderString(), x => UrlTemplate.MakeHrefString(x.GetDetailUrl(), x.GrantTitle), 250, AgGridColumnFilterType.Html);
            Add(Models.FieldDefinition.TotalAwardAmount.ToGridHeaderString(), x => x.GetTotalAwardAmount(), 90, AgGridColumnFormatType.CurrencyWithCents);
            Add(Models.FieldDefinition.GrantCurrentBalance.ToGridHeaderString(), x => x.GetCurrentBalanceOfGrantBasedOnAllGrantAllocationExpenditures(), 90, AgGridColumnFormatType.CurrencyWithCents);
            Add(Models.FieldDefinition.GrantStartDate.ToGridHeaderString(), x => x.StartDate, 90, AgGridColumnFormatType.Date);
            Add(Models.FieldDefinition.GrantEndDate.ToGridHeaderString(), x => x.EndDate, 90, AgGridColumnFormatType.Date);
            Add(Models.FieldDefinition.GrantStatus.ToGridHeaderString(), x => x.GrantStatus.GrantStatusName, 90, AgGridColumnFilterType.SelectFilterStrict);
            Add(Models.FieldDefinition.GrantType.ToGridHeaderString(), x => x.GrantTypeDisplay, 90, AgGridColumnFilterType.SelectFilterStrict);
            
        }
    }
}
