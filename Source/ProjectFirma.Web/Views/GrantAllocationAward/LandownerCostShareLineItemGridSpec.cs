/*-----------------------------------------------------------------------
<copyright file="LandownerCostShareLineItemGridSpec.cs" company="Tahoe Regional Planning Agency and Sitka Technology Group">
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

namespace ProjectFirma.Web.Views.GrantAllocationAward
{
    public class LandownerCostShareLineItemGridSpec : GridSpec<GrantAllocationAwardLandownerCostShareLineItem>
    {
        public LandownerCostShareLineItemGridSpec(Person currentPerson, Models.GrantAllocationAward grantAllocationAward)
        {
            ShowFilterBar = true;
            ObjectNameSingular = Models.FieldDefinition.GrantAllocationAwardLandownerCostShare.GetFieldDefinitionLabel();
            ObjectNamePlural = Models.FieldDefinition.GrantAllocationAwardLandownerCostShare.GetFieldDefinitionLabelPluralized();

            bool hasDeletePermission = new GrantAllocationAwardLandownerCostShareLineItemDeleteFeature().HasPermissionByPerson(currentPerson);
            bool hasEditPermission = new GrantAllocationAwardLandownerCostShareLineItemEditAsAdminFeature().HasPermissionByPerson(currentPerson);
            bool hasCreatePermission = new GrantAllocationAwardLandownerCostShareLineItemCreateFeature().HasPermissionByPerson(currentPerson);
            if (hasCreatePermission)
            {
                var newLandownerCostShareLineItemUrl = SitkaRoute<GrantAllocationAwardController>.BuildUrlFromExpression(t => t.NewLandownerCostShareLineItemFromGrantAllocationAward(grantAllocationAward.PrimaryKey));
                CreateEntityModalDialogForm = new ModalDialogForm(newLandownerCostShareLineItemUrl, $"Create a new {Models.FieldDefinition.GrantAllocationAwardLandownerCostShare.GetFieldDefinitionLabel()} Line Item");
            }

            //delete column
            if (hasDeletePermission)
            {
                Add(string.Empty, x => DhtmlxGridHtmlHelpers.MakeDeleteIconAndLinkBootstrap(x.GetDeleteLandownerCostShareLineItemUrl(), true, true), 30, DhtmlxGridColumnFilterType.None);
            }
            //edit column
            if (hasEditPermission)
            {
                Add(string.Empty, x => DhtmlxGridHtmlHelpers.MakeEditIconAsModalDialogLinkBootstrap(new ModalDialogForm(x.GetEditLandownerCostShareLineItemUrl(), $"Edit this {Models.FieldDefinition.GrantAllocationAwardLandownerCostShare.GetFieldDefinitionLabel()} Line Item")), 30, DhtmlxGridColumnFilterType.None);
            }

            //Add(Models.FieldDefinition.GrantAllocationAwardLandownerCostShareDescription.ToGridHeaderString(), s => s.GrantAllocationAwardLandownerCostShareLineItemDescription, 200, DhtmlxGridColumnFilterType.Text);
            //Add(Models.FieldDefinition.GrantAllocationAwardLandownerCostShareTarOrMonth.ToGridHeaderString(), s => s.GrantAllocationAwardLandownerCostShareLineItemTarOrMonth , 125, DhtmlxGridColumnFilterType.SelectFilterStrict);
            //Add(Models.FieldDefinition.GrantAllocationAwardLandownerCostShareDate.ToGridHeaderString(), s => s.GrantAllocationAwardLandownerCostShareLineItemDate.ToShortDateString(), 125, DhtmlxGridColumnFilterType.SelectFilterStrict);
            //Add(Models.FieldDefinition.GrantAllocationAwardLandownerCostShareMiles.ToGridHeaderString(), s => s.GrantAllocationAwardLandownerCostShareLineItemMiles, 125, DhtmlxGridColumnFormatType.Integer, DhtmlxGridColumnAggregationType.Total);
            //Add(Models.FieldDefinition.GrantAllocationAwardLandownerCostShareMileageRate.ToGridHeaderString(), s => s.GrantAllocationAwardLandownerCostShareLineItemMileageRate, 125, DhtmlxGridColumnFormatType.CurrencyWithCents);
            //Add(Models.FieldDefinition.GrantAllocationAwardLandownerCostShareAmount.ToGridHeaderString(), s => s.GrantAllocationAwardLandownerCostShareLineItemAmountForDisplay, 125, DhtmlxGridColumnFormatType.CurrencyWithCents, DhtmlxGridColumnAggregationType.Total);
            Add(Models.FieldDefinition.GrantAllocationAwardLandownerCostShareNotes.ToGridHeaderString(), s => s.GrantAllocationAwardLandownerCostShareLineItemNotes, 250, DhtmlxGridColumnFilterType.Text);
        }
    }
}
