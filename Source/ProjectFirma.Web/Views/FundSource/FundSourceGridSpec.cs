/*-----------------------------------------------------------------------
<copyright file="FundSourceGridSpec.cs" company="Tahoe Regional Planning Agency and Environmental Science Associates">
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

namespace ProjectFirma.Web.Views.FundSource
{
    public class FundSourceGridSpec : GridSpec<Models.FundSource>
    {
        public static string FundSourceNumberHiddenColumnName = $"{Models.FieldDefinition.FundSourceNumber.GetFieldDefinitionLabel().ToStringSpecialForJavascript()}AsText";

        public FundSourceGridSpec(Models.Person currentPerson)
        {
            ObjectNameSingular = $"{Models.FieldDefinition.FundSource.GetFieldDefinitionLabel()}";
            ObjectNamePlural = $"{Models.FieldDefinition.FundSource.GetFieldDefinitionLabelPluralized()}";
            SaveFiltersInCookie = true;
            var userHasDeletePermissions = new FundSourceDeleteFeature().HasPermissionByPerson(currentPerson);
            var userHasCreatePermissions = new FundSourceCreateFeature().HasPermissionByPerson(currentPerson);
            if (userHasCreatePermissions)
            {
                var contentUrl = SitkaRoute<FundSourceController>.BuildUrlFromExpression(t => t.New());
                CreateEntityModalDialogForm = new ModalDialogForm(contentUrl, 950, $"Create a new {Models.FieldDefinition.FundSource.GetFieldDefinitionLabel()}");
            }

            CustomExcelDownloadUrl = SitkaRoute<FundSourceController>.BuildUrlFromExpression(tc => tc.FundSourcesExcelDownload());

            // hidden column for fundSource number for use by JavaScript
            Add(FundSourceNumberHiddenColumnName, x => x.FundSourceNumber, 0);

            if (userHasDeletePermissions)
            {
                Add("Delete", x => AgGridHtmlHelpers.MakeDeleteIconAndLinkBootstrap(x.GetDeleteUrl(), true, true), 30, AgGridColumnFilterType.None);
            }

            if (userHasCreatePermissions)
            {
                Add("Duplicate", x => AgGridHtmlHelpers.MakeDuplicateIconAndLinkBootstrap(x.GetDuplicateUrl(), 950, $"Duplicate {Models.FieldDefinition.FundSource.GetFieldDefinitionLabel()} \"{x.FundSourceName}\" to New {Models.FieldDefinition.FundSource.GetFieldDefinitionLabel()}"), 30, AgGridColumnFilterType.None);
            }


            Add(Models.FieldDefinition.FundSourceNumber.ToGridHeaderString(), x => new HtmlLinkObject(x.FundSourceNumber, x.GetDetailUrl()).ToJsonObjectForAgGrid(), FundSourceAllocationGridSpec.FundSourceNumberColumnWidth, AgGridColumnFilterType.HtmlLinkJson);
            Add(Models.FieldDefinition.CFDA.ToGridHeaderString(), x => x.CFDANumber, 120, AgGridColumnFilterType.Html);
            Add(Models.FieldDefinition.FundSourceName.ToGridHeaderString(), x => new HtmlLinkObject(x.FundSourceTitle, x.GetDetailUrl()).ToJsonObjectForAgGrid(), 250, AgGridColumnFilterType.HtmlLinkJson);
            Add(Models.FieldDefinition.TotalAwardAmount.ToGridHeaderString(), x => x.TotalAwardAmount, 90, AgGridColumnFormatType.CurrencyWithCents);

            if (!currentPerson.IsAnonymousOrUnassigned)
            {
                Add(Models.FieldDefinition.FundSourceCurrentBalance.ToGridHeaderString(), x => x.GetCurrentBalanceOfFundSourceBasedOnAllFundSourceAllocationExpenditures(), 90, AgGridColumnFormatType.CurrencyWithCents);
            }

            Add(Models.FieldDefinition.FundSourceStartDate.ToGridHeaderString(), x => x.StartDate, 90, AgGridColumnFormatType.Date);
            Add(Models.FieldDefinition.FundSourceEndDate.ToGridHeaderString(), x => x.EndDate, 90, AgGridColumnFormatType.Date);
            Add(Models.FieldDefinition.FundSourceStatus.ToGridHeaderString(), x => x.FundSourceStatus.FundSourceStatusName, 90, AgGridColumnFilterType.SelectFilterStrict);
            Add(Models.FieldDefinition.FundSourceType.ToGridHeaderString(), x => x.FundSourceTypeDisplay, 90, AgGridColumnFilterType.SelectFilterStrict);
            
        }
    }
}
