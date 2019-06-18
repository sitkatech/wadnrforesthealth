/*-----------------------------------------------------------------------
<copyright file="TravelLineItemGridSpec.cs" company="Tahoe Regional Planning Agency and Sitka Technology Group">
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
    public class TravelLineItemGridSpec : GridSpec<GrantAllocationAwardTravelLineItem>
    {
        public TravelLineItemGridSpec(Person currentPerson, Models.GrantAllocationAward grantAllocationAward)
        {
            ShowFilterBar = true;
            ObjectNameSingular = Models.FieldDefinition.GrantAllocationAwardTravel.GetFieldDefinitionLabel();
            ObjectNamePlural = Models.FieldDefinition.GrantAllocationAwardTravel.GetFieldDefinitionLabelPluralized();

            bool hasDeletePermission = new GrantAllocationAwardTravelLineItemDeleteFeature().HasPermissionByPerson(currentPerson);
            bool hasEditPermission = new GrantAllocationAwardTravelLineItemEditAsAdminFeature().HasPermissionByPerson(currentPerson);
            bool hasCreatePermission = new GrantAllocationAwardTravelLineItemCreateFeature().HasPermissionByPerson(currentPerson);
            if (hasCreatePermission)
            {
                var newTravelLineItemUrl = SitkaRoute<GrantAllocationAwardController>.BuildUrlFromExpression(t => t.NewTravelLineItemFromGrantAllocationAward(grantAllocationAward.PrimaryKey));
                CreateEntityModalDialogForm = new ModalDialogForm(newTravelLineItemUrl, $"Create a new {Models.FieldDefinition.GrantAllocationAwardTravel.GetFieldDefinitionLabel()} Line Item");
            }

            //delete column
            if (hasDeletePermission)
            {
                Add(string.Empty, x => DhtmlxGridHtmlHelpers.MakeDeleteIconAndLinkBootstrap(x.GetDeleteTravelLineItemUrl(), true, true), 30, DhtmlxGridColumnFilterType.None);
            }
            //edit column
            if (hasEditPermission)
            {
                Add(string.Empty, x => DhtmlxGridHtmlHelpers.MakeEditIconAsModalDialogLinkBootstrap(new ModalDialogForm(x.GetEditTravelLineItemUrl(), $"Edit this {Models.FieldDefinition.GrantAllocationAwardTravel.GetFieldDefinitionLabel()} Line Item")), 30, DhtmlxGridColumnFilterType.None);
            }

            Add(Models.FieldDefinition.GrantAllocationAwardTravelDescription.ToGridHeaderString(), s => s.GrantAllocationAwardTravelLineItemDescription, 200, DhtmlxGridColumnFilterType.Text);
            Add(Models.FieldDefinition.GrantAllocationAwardTravelTarOrMonth.ToGridHeaderString(), s => s.GrantAllocationAwardTravelLineItemTarOrMonth , 125, DhtmlxGridColumnFilterType.SelectFilterStrict);
            Add(Models.FieldDefinition.GrantAllocationAwardTravelDate.ToGridHeaderString(), s => s.GrantAllocationAwardTravelLineItemDate.ToShortDateString(), 125, DhtmlxGridColumnFilterType.SelectFilterStrict);
            Add(Models.FieldDefinition.GrantAllocationAwardTravelMiles.ToGridHeaderString(), s => s.GrantAllocationAwardTravelLineItemMiles, 125, DhtmlxGridColumnFormatType.Integer, DhtmlxGridColumnAggregationType.Total);
            Add(Models.FieldDefinition.GrantAllocationAwardTravelMileageRate.ToGridHeaderString(), s => s.GrantAllocationAwardTravelLineItemMileageRate, 125, DhtmlxGridColumnFormatType.CurrencyWithCents);
            Add(Models.FieldDefinition.GrantAllocationAwardTravelAmount.ToGridHeaderString(), s => s.GrantAllocationAwardTravelLineItemAmountForDisplay, 125, DhtmlxGridColumnFormatType.CurrencyWithCents, DhtmlxGridColumnAggregationType.Total);
            Add(Models.FieldDefinition.GrantAllocationAwardTravelNotes.ToGridHeaderString(), s => s.GrantAllocationAwardTravelLineItemNotes, 250, DhtmlxGridColumnFilterType.Text);
        }
    }
}
