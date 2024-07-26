/*-----------------------------------------------------------------------
<copyright file="ProjectFocusAreasGridSpec.cs" company="Tahoe Regional Planning Agency and Environmental Science Associates">
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

using System.Collections.Generic;
using System.Linq;
using System.Web;
using ProjectFirma.Web.Models;
using LtInfo.Common;
using LtInfo.Common.AgGridWrappers;
using LtInfo.Common.HtmlHelperExtensions;
using LtInfo.Common.ModalDialog;
using LtInfo.Common.Views;
using ProjectFirma.Web.Common;
using ProjectFirma.Web.Controllers;
using ProjectFirma.Web.Security;

namespace ProjectFirma.Web.Views.FocusArea
{
    public class GrantAllocationAwardGridSpec : GridSpec<Models.GrantAllocationAward>
    {
        public GrantAllocationAwardGridSpec(Person currentPerson, Models.FocusArea focusArea)
        : this(currentPerson)
        {
            ObjectNameSingular = $"{Models.FieldDefinition.GrantAllocationAward.GetFieldDefinitionLabel()}";
            ObjectNamePlural = $"{Models.FieldDefinition.GrantAllocationAward.GetFieldDefinitionLabelPluralized()} associated with {focusArea.FocusAreaName}";

            var userHasCreatePermissions = new GrantAllocationAwardCreateFeature().HasPermissionByPerson(currentPerson);
            if (userHasCreatePermissions)
            {
                var contentUrl = SitkaRoute<GrantAllocationAwardController>.BuildUrlFromExpression(t => t.NewForAFocusArea(focusArea));
                CreateEntityModalDialogForm = new ModalDialogForm(contentUrl, 950, $"Create a new {ObjectNameSingular}");
            }
        }


        public GrantAllocationAwardGridSpec(Person currentPerson, Models.GrantAllocation grantAllocation)
            : this(currentPerson)
        {
            ObjectNameSingular = $"{Models.FieldDefinition.GrantAllocationAward.GetFieldDefinitionLabel()}";
            ObjectNamePlural = $"{Models.FieldDefinition.GrantAllocationAward.GetFieldDefinitionLabelPluralized()} associated with {grantAllocation.DisplayName}";

            var userHasCreatePermissions = new GrantAllocationAwardCreateFeature().HasPermissionByPerson(currentPerson);
            if (userHasCreatePermissions && grantAllocation.DNRUplandRegionID.HasValue)
            {
                var contentUrl = SitkaRoute<GrantAllocationAwardController>.BuildUrlFromExpression(t => t.NewForAGrantAllocation(grantAllocation));
                CreateEntityModalDialogForm = new ModalDialogForm(contentUrl, 950, $"Create a new {ObjectNameSingular}");
            }
        }

        public GrantAllocationAwardGridSpec(Person currentPerson)
        {

            ObjectNameSingular = $"{Models.FieldDefinition.GrantAllocationAward.GetFieldDefinitionLabel()}";
            ObjectNamePlural = $"{Models.FieldDefinition.GrantAllocationAward.GetFieldDefinitionLabelPluralized()}";

            var userHasDeletePermissions = new GrantAllocationAwardDeleteFeature().HasPermissionByPerson(currentPerson);
            if (userHasDeletePermissions)
            {
                Add("Delete", x => AgGridHtmlHelpers.MakeDeleteIconAndLinkBootstrap(x.GetDeleteUrl(), userHasDeletePermissions, x.CanGrantAllocationAwardBeDeleted(), true), 30, AgGridColumnFilterType.None);
            }

            Add(Models.FieldDefinition.GrantAllocationAwardName.ToGridHeaderString(), x => UrlTemplate.MakeHrefString(x.GetDetailUrl(), x.GrantAllocationAwardName), 150, AgGridColumnFilterType.SelectFilterStrict);
            Add(Models.FieldDefinition.GrantNumber.ToGridHeaderString(), x => x.GrantAllocation.GrantModification.Grant.GetGrantNumberAsUrl(), 140, AgGridColumnFilterType.SelectFilterStrict);
            Add(Models.FieldDefinition.FocusArea.ToGridHeaderString(), x => x.FocusArea.GetDisplayNameAsUrl(), 140, AgGridColumnFilterType.SelectFilterStrict);
            Add(Models.FieldDefinition.GrantAllocationName.ToGridHeaderString("Funding Grant Allocation"), x => UrlTemplate.MakeHrefString(x.GrantAllocation.GetDetailUrl(), x.GrantAllocation.GrantAllocationName), 250, AgGridColumnFilterType.Text);
            Add(Models.FieldDefinition.GrantAllocationAwardAmount.ToGridHeaderString(), x => x.GrantAllocationAwardAmount, 90, AgGridColumnFormatType.CurrencyWithCents, AgGridColumnAggregationType.Total);
            Add("Spent Amount", x => x.SpentAmount, 90, AgGridColumnFormatType.CurrencyWithCents, AgGridColumnAggregationType.Total);
            Add("Remaining Amount", x => x.Balance, 90, AgGridColumnFormatType.CurrencyWithCents, AgGridColumnAggregationType.Total);
            Add(Models.FieldDefinition.GrantAllocationAwardExpirationDate.ToGridHeaderString(), x => x.GrantAllocationAwardExpirationDate, 90, AgGridColumnFormatType.Date);
            Add(Models.FieldDefinition.ProgramIndexProjectCode.ToGridHeaderString(), x => x.GrantAllocation.GetAssociatedProgramIndexProjectCodePairsForAgGrid(), 90, AgGridColumnFilterType.HtmlLinkListJson);
        }
    }
}
