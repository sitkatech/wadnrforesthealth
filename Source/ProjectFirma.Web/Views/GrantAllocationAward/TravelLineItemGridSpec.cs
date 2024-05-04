/*-----------------------------------------------------------------------
<copyright file="TravelLineItemGridSpec.cs" company="Tahoe Regional Planning Agency and Environmental Science Associates">
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

namespace ProjectFirma.Web.Views.GrantAllocationAward
{
    public class TravelLineItemGridSpec : GridSpec<GrantAllocationAwardTravelLineItem>
    {
        public TravelLineItemGridSpec(Person currentPerson, Models.GrantAllocationAward grantAllocationAward)
        {
            ShowFilterBar = true;
            ObjectNameSingular = Models.FieldDefinition.GrantAllocationAwardTravelLineItem.GetFieldDefinitionLabel();
            ObjectNamePlural = Models.FieldDefinition.GrantAllocationAwardTravelLineItem.GetFieldDefinitionLabelPluralized();

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
                Add(string.Empty, x => AgGridHtmlHelpers.MakeDeleteIconAndLinkBootstrap(x.GetDeleteTravelLineItemUrl(), true, true), 30, AgGridColumnFilterType.None);
            }
            //edit column
            if (hasEditPermission)
            {
                Add(string.Empty, x => AgGridHtmlHelpers.MakeEditIconAsModalDialogLinkBootstrap(new ModalDialogForm(x.GetEditTravelLineItemUrl(), $"Edit this {Models.FieldDefinition.GrantAllocationAwardTravel.GetFieldDefinitionLabel()} Line Item")), 30, AgGridColumnFilterType.None);
            }

            Add(Models.FieldDefinition.GrantAllocationAwardTravelName.ToGridHeaderString(), s => s.Person != null ? s.Person.FullNameFirstLastAndOrgShortName : string.Empty, 200, AgGridColumnFilterType.Text);
            Add(Models.FieldDefinition.GrantAllocationAwardTravelTarOrMonth.ToGridHeaderString(), s => s.GrantAllocationAwardTravelLineItemTarOrMonth , 125, AgGridColumnFilterType.SelectFilterStrict);
            Add(Models.FieldDefinition.GrantAllocationAwardTravelDate.ToGridHeaderString(), s => s.GrantAllocationAwardTravelLineItemDate, 125, AgGridColumnFormatType.Date);
            Add(Models.FieldDefinition.GrantAllocationAwardTravelMiles.ToGridHeaderString(), s => s.GrantAllocationAwardTravelLineItemMiles, 125, AgGridColumnFormatType.Integer, AgGridColumnAggregationType.Total);
            Add(Models.FieldDefinition.GrantAllocationAwardTravelMileageRate.ToGridHeaderString(), s => s.GrantAllocationAwardTravelLineItemMileageRate, 125, AgGridColumnFormatType.CurrencyWithCents);
            Add(Models.FieldDefinition.GrantAllocationAwardTravelAmount.ToGridHeaderString(), s => s.GrantAllocationAwardTravelLineItemCalculatedAmount, 125, AgGridColumnFormatType.CurrencyWithCents, AgGridColumnAggregationType.Total);
            Add(Models.FieldDefinition.GrantAllocationAwardTravelNotes.ToGridHeaderString(), s => s.GrantAllocationAwardTravelLineItemNotes, 250, AgGridColumnFilterType.Text);
        }
    }
}
