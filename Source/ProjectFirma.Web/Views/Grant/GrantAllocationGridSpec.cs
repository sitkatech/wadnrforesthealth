/*-----------------------------------------------------------------------
<copyright file="GrantAllocationGridSpec.cs" company="Tahoe Regional Planning Agency and Environmental Science Associates">
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
using System.Collections.Generic;

namespace ProjectFirma.Web.Views.Grant
{

    public class GrantAllocationGridSpec : GridSpec<Models.GrantAllocation>
    {
        public enum GrantAllocationGridCreateButtonType
        {
            Hidden,
            Shown
        }

        public const int GrantNumberColumnWidth = 180;

        public GrantAllocationGridSpec(Person currentPerson, GrantAllocationGridCreateButtonType createButtonType, Models.Grant optionalRelevantGrant)
        {
            ObjectNameSingular = $"{Models.FieldDefinition.GrantAllocation.GetFieldDefinitionLabel()}";
            ObjectNamePlural = $"{Models.FieldDefinition.GrantAllocation.GetFieldDefinitionLabelPluralized()}";
            SaveFiltersInCookie = true;
            var userHasDeletePermissions = new GrantAllocationDeleteFeature().HasPermissionByPerson(currentPerson);
            var userHasCreatePermissions = new GrantAllocationCreateFeature().HasPermissionByPerson(currentPerson);
            if (userHasCreatePermissions && createButtonType == GrantAllocationGridCreateButtonType.Shown)
            {
                var contentUrl = SitkaRoute<GrantAllocationController>.BuildUrlFromExpression(t => t.New(optionalRelevantGrant));
                CreateEntityModalDialogForm = new ModalDialogForm(contentUrl, 950, $"Create a new {Models.FieldDefinition.GrantAllocation.GetFieldDefinitionLabel()}");
            }

            CustomExcelDownloadUrl = SitkaRoute<GrantController>.BuildUrlFromExpression(tc => tc.GrantsExcelDownload());

            if (userHasDeletePermissions)
            {
                Add("Delete", x => AgGridHtmlHelpers.MakeDeleteIconAndLinkBootstrap(x.GetDeleteUrl(), true, true), 30, AgGridColumnFilterType.None);
            }

            if (userHasCreatePermissions)
            {
                Add("Duplicate", x => AgGridHtmlHelpers.MakeDuplicateIconAndLinkBootstrap(x.GetDuplicateUrl(), 950, $"Duplicate {Models.FieldDefinition.GrantAllocation.GetFieldDefinitionLabel()}"), 30, AgGridColumnFilterType.None);
            }

            Add(Models.FieldDefinition.GrantNumber.ToGridHeaderString(), x => x.GrantModification.Grant.GrantNumber, GrantNumberColumnWidth, AgGridColumnFilterType.SelectFilterStrict);
            Add(Models.FieldDefinition.GrantAllocationName.ToGridHeaderString(), x => new HtmlLinkObject(x.GrantAllocationName, x.GetDetailUrl()).ToJsonObjectForAgGrid(), 250, AgGridColumnFilterType.HtmlLinkJson);
            Add(Models.FieldDefinition.GrantModification.ToGridHeaderString(), x => UrlTemplate.MakeHrefString(x.GrantModification.GetDetailUrl(), x.GrantModification.GrantModificationName), 250, AgGridColumnFilterType.Text);
            Add(Models.FieldDefinition.AllocationAmount.ToGridHeaderString(), x => x.AllocationAmount, 90, AgGridColumnFormatType.CurrencyWithCents, AgGridColumnAggregationType.Total);

            if (!currentPerson.IsAnonymousUser)
            {
                Add(Models.FieldDefinition.GrantAllocationCurrentBalance.ToGridHeaderString(),
                    x => x.GetTotalBudgetVsActualLineItem().BudgetMinusExpendituresFromDatamart, 90,
                    AgGridColumnFormatType.CurrencyWithCents, AgGridColumnAggregationType.Total);
            }

            Add(Models.FieldDefinition.GrantManager.ToGridHeaderString(), x => x.GrantManager != null ? x.GrantManager.FullNameFirstLastAndOrgShortName : string.Empty, 150, AgGridColumnFilterType.SelectFilterStrict);
            Add(Models.FieldDefinition.ProgramManager.ToGridHeaderString(), x => x.GetAllProgramManagerPersonNamesAsString(), 150, AgGridColumnFilterType.SelectFilterStrict);
            Add(Models.FieldDefinition.GrantStartDate.ToGridHeaderString(), x => x.StartDate, 90, AgGridColumnFormatType.Date);
            Add(Models.FieldDefinition.GrantEndDate.ToGridHeaderString(), x => x.EndDate, 90, AgGridColumnFormatType.Date);
            Add($"Parent Grant {Models.FieldDefinition.GrantStatus.ToGridHeaderString()}", x => x.GrantModification.Grant.GrantStatus.GrantStatusName, 90, AgGridColumnFilterType.SelectFilterStrict);
            Add(Models.FieldDefinition.Division.ToGridHeaderString(), x => x.DivisionNameDisplay, 180, AgGridColumnFilterType.SelectFilterStrict);
            Add(Models.FieldDefinition.DNRUplandRegion.ToGridHeaderString(), x => x.RegionNameDisplay, 100, AgGridColumnFilterType.SelectFilterStrict);
            Add(Models.FieldDefinition.FederalFundCode.ToGridHeaderString(), x => x.FederalFundCode != null ? x.FederalFundCode.FederalFundCodeAbbrev : string.Empty, 90, AgGridColumnFilterType.SelectFilterStrict);
            Add(Models.FieldDefinition.ProgramIndexProjectCode.ToGridHeaderString(), x => x.GetAssociatedProgramIndexProjectCodePairsForAgGrid(), 90, AgGridColumnFilterType.HtmlLinkListJson);
            Add(Models.FieldDefinition.Organization.ToGridHeaderString(), x => x.Organization != null ? x.Organization.OrganizationName : string.Empty, 90, AgGridColumnFilterType.SelectFilterStrict);
            
        }

        

    }
}
