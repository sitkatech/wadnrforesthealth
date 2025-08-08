/*-----------------------------------------------------------------------
<copyright file="FundSourceAllocationGridSpec.cs" company="Tahoe Regional Planning Agency and Environmental Science Associates">
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

using LtInfo.Common.AgGridWrappers;
using LtInfo.Common.HtmlHelperExtensions;
using LtInfo.Common.ModalDialog;
using ProjectFirma.Web.Common;
using ProjectFirma.Web.Controllers;
using ProjectFirma.Web.Models;
using ProjectFirma.Web.Security;

namespace ProjectFirma.Web.Views.FundSource
{

    public class FundSourceAllocationGridSpec : GridSpec<Models.FundSourceAllocation>
    {
        public enum FundSourceAllocationGridCreateButtonType
        {
            Hidden,
            Shown
        }

        public const int FundSourceNumberColumnWidth = 180;

        public FundSourceAllocationGridSpec(Person currentPerson, FundSourceAllocationGridCreateButtonType createButtonType, Models.FundSource optionalRelevantFundSource)
        {
            ObjectNameSingular = $"{Models.FieldDefinition.FundSourceAllocation.GetFieldDefinitionLabel()}";
            ObjectNamePlural = $"{Models.FieldDefinition.FundSourceAllocation.GetFieldDefinitionLabelPluralized()}";
            SaveFiltersInCookie = true;
            var userHasDeletePermissions = new FundSourceAllocationDeleteFeature().HasPermissionByPerson(currentPerson);
            var userHasCreatePermissions = new FundSourceAllocationCreateFeature().HasPermissionByPerson(currentPerson);
            if (userHasCreatePermissions && createButtonType == FundSourceAllocationGridCreateButtonType.Shown)
            {
                var contentUrl = SitkaRoute<FundSourceAllocationController>.BuildUrlFromExpression(t => t.New());
                if (optionalRelevantFundSource != null)
                {
                    contentUrl = SitkaRoute<FundSourceAllocationController>.BuildUrlFromExpression(t => t.NewFromFundSource(optionalRelevantFundSource));
                }
                CreateEntityModalDialogForm = new ModalDialogForm(contentUrl, 950, $"Create a new {Models.FieldDefinition.FundSourceAllocation.GetFieldDefinitionLabel()}");
            }

            CustomExcelDownloadUrl = SitkaRoute<FundSourceController>.BuildUrlFromExpression(tc => tc.FundSourcesExcelDownload());

            if (userHasDeletePermissions)
            {
                Add("Delete", x => AgGridHtmlHelpers.MakeDeleteIconAndLinkBootstrap(x.GetDeleteUrl(), true, true), 30, AgGridColumnFilterType.None);
            }

            if (userHasCreatePermissions)
            {
                Add("Duplicate", x => AgGridHtmlHelpers.MakeDuplicateIconAndLinkBootstrap(x.GetDuplicateUrl(), 950, $"Duplicate {Models.FieldDefinition.FundSourceAllocation.GetFieldDefinitionLabel()}"), 30, AgGridColumnFilterType.None);
            }

            Add(Models.FieldDefinition.FundSourceNumber.ToGridHeaderString(), x => x.FundSource.FundSourceNumber, FundSourceNumberColumnWidth, AgGridColumnFilterType.SelectFilterStrict);
            Add(Models.FieldDefinition.FundSourceAllocationName.ToGridHeaderString(), x => new HtmlLinkObject(x.FundSourceAllocationName, x.GetDetailUrl()).ToJsonObjectForAgGrid(), 250, AgGridColumnFilterType.HtmlLinkJson);
            Add(Models.FieldDefinition.AllocationAmount.ToGridHeaderString(), x => x.AllocationAmount, 90, AgGridColumnFormatType.CurrencyWithCents, AgGridColumnAggregationType.Total);

            if (!currentPerson.IsAnonymousUser)
            {
                Add(Models.FieldDefinition.FundSourceAllocationCurrentBalance.ToGridHeaderString(),
                    x => x.GetTotalBudgetVsActualLineItem().BudgetMinusExpendituresFromDatamart, 90,
                    AgGridColumnFormatType.CurrencyWithCents, AgGridColumnAggregationType.Total);
            }

            Add(Models.FieldDefinition.FundSourceManager.ToGridHeaderString(), x => x.FundSourceManager != null ? x.FundSourceManager.FullNameFirstLastAndOrgShortName : string.Empty, 150, AgGridColumnFilterType.SelectFilterStrict);
            Add(Models.FieldDefinition.ProgramManager.ToGridHeaderString(), x => x.GetAllProgramManagerPersonNamesAsString(), 150, AgGridColumnFilterType.SelectFilterStrict);
            Add(Models.FieldDefinition.FundSourceStartDate.ToGridHeaderString(), x => x.StartDate, 90, AgGridColumnFormatType.Date);
            Add(Models.FieldDefinition.FundSourceEndDate.ToGridHeaderString(), x => x.EndDate, 90, AgGridColumnFormatType.Date);
            Add($"Parent FundSource {Models.FieldDefinition.FundSourceStatus.ToGridHeaderString()}", x => x.FundSource.FundSourceStatus.FundSourceStatusName, 90, AgGridColumnFilterType.SelectFilterStrict);
            Add(Models.FieldDefinition.Division.ToGridHeaderString(), x => x.DivisionNameDisplay, 180, AgGridColumnFilterType.SelectFilterStrict);
            Add(Models.FieldDefinition.DNRUplandRegion.ToGridHeaderString(), x => x.RegionNameDisplay, 100, AgGridColumnFilterType.SelectFilterStrict);
            Add(Models.FieldDefinition.FederalFundCode.ToGridHeaderString(), x => x.FederalFundCode != null ? x.FederalFundCode.FederalFundCodeAbbrev : string.Empty, 90, AgGridColumnFilterType.SelectFilterStrict);
            Add(Models.FieldDefinition.ProgramIndexProjectCode.ToGridHeaderString(), x => x.GetAssociatedProgramIndexProjectCodePairsForAgGrid(), 90, AgGridColumnFilterType.HtmlLinkListJson);
            Add(Models.FieldDefinition.Organization.ToGridHeaderString(), x => x.Organization != null ? x.Organization.OrganizationName : string.Empty, 90, AgGridColumnFilterType.SelectFilterStrict);
            
        }

        

    }
}
