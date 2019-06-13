/*-----------------------------------------------------------------------
<copyright file="ProjectFocusAreasGridSpec.cs" company="Tahoe Regional Planning Agency and Sitka Technology Group">
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

using System.Collections.Generic;
using System.Linq;
using System.Web;
using ProjectFirma.Web.Models;
using LtInfo.Common;
using LtInfo.Common.DhtmlWrappers;
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
        {
            ObjectNameSingular = $"{Models.FieldDefinition.GrantAllocationAward.GetFieldDefinitionLabel()}";
            ObjectNamePlural = $"{Models.FieldDefinition.GrantAllocationAward.GetFieldDefinitionLabelPluralized()} associated with {focusArea.FocusAreaName}";

            var userHasCreatePermissions = new GrantAllocationAwardCreateFeature().HasPermissionByPerson(currentPerson);
            if (userHasCreatePermissions)
            {
                var contentUrl = SitkaRoute<GrantAllocationAwardController>.BuildUrlFromExpression(t => t.NewForAFocusArea(focusArea));
                CreateEntityModalDialogForm = new ModalDialogForm(contentUrl, 950, $"Create a new {ObjectNameSingular}");
            }


            Add(Models.FieldDefinition.GrantNumber.ToGridHeaderString(), x => x.GrantAllocation.Grant.GrantNumber, 140, DhtmlxGridColumnFilterType.SelectFilterStrict);
            Add($"Funding {Models.FieldDefinition.GrantAllocationName.ToGridHeaderString()}", x => UrlTemplate.MakeHrefString(x.GrantAllocation.GetDetailUrl(), x.GrantAllocation.GrantAllocationName), 250, DhtmlxGridColumnFilterType.Text);
            Add(Models.FieldDefinition.GrantAllocationAwardName.ToGridHeaderString(), x => UrlTemplate.MakeHrefString(x.GetDetailUrl(), x.GrantAllocationAwardName), 150, DhtmlxGridColumnFilterType.SelectFilterStrict);
            Add(Models.FieldDefinition.GrantAllocationAwardAmount.ToGridHeaderString(), x => x.GrantAllocationAwardAmount, 90, DhtmlxGridColumnFormatType.CurrencyWithCents, DhtmlxGridColumnAggregationType.Total);
            Add("Spent Amount", x => x.SpentAmount, 90, DhtmlxGridColumnFormatType.CurrencyWithCents, DhtmlxGridColumnAggregationType.Total);
            Add("Remaining Amount", x => x.RemainingAmount, 90, DhtmlxGridColumnFormatType.CurrencyWithCents, DhtmlxGridColumnAggregationType.Total);
            Add(Models.FieldDefinition.GrantAllocationAwardExpirationDate.ToGridHeaderString(), x => x.GrantAllocationAwardExpirationDate.ToStringDate(), 90, DhtmlxGridColumnFilterType.SelectFilterStrict);
            Add(Models.FieldDefinition.ProgramIndexProjectCode.ToGridHeaderString(), x => x.GrantAllocation.GetAssociatedProgramIndexProjectCodePairsCommaDelimited(), 90, DhtmlxGridColumnFilterType.SelectFilterStrict);

        }
    }
}
