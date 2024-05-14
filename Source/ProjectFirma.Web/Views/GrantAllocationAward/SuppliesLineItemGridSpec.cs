/*-----------------------------------------------------------------------
<copyright file="SuppliesLineItemGridSpec.cs" company="Tahoe Regional Planning Agency and Environmental Science Associates">
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
    public class SuppliesLineItemGridSpec : GridSpec<GrantAllocationAwardSuppliesLineItem>
    {
        public SuppliesLineItemGridSpec(Person currentPerson, Models.GrantAllocationAward grantAllocationAward)
        {
            ShowFilterBar = true;
            ObjectNameSingular = Models.FieldDefinition.GrantAllocationAwardSuppliesLineItem.GetFieldDefinitionLabel();
            ObjectNamePlural = Models.FieldDefinition.GrantAllocationAwardSuppliesLineItem.GetFieldDefinitionLabelPluralized();

            bool hasDeletePermission = new GrantAllocationAwardSuppliesLineItemDeleteFeature().HasPermissionByPerson(currentPerson);
            bool hasEditPermission = new GrantAllocationAwardSuppliesLineItemEditAsAdminFeature().HasPermissionByPerson(currentPerson);
            bool hasCreatePermission = new GrantAllocationAwardSuppliesLineItemCreateFeature().HasPermissionByPerson(currentPerson);
            if (hasCreatePermission)
            {
                var newSuppliesLineItemUrl = SitkaRoute<GrantAllocationAwardController>.BuildUrlFromExpression(t => t.NewSuppliesLineItemFromGrantAllocationAward(grantAllocationAward.PrimaryKey));
                CreateEntityModalDialogForm = new ModalDialogForm(newSuppliesLineItemUrl, $"Create a new {Models.FieldDefinition.GrantAllocationAwardSupplies.GetFieldDefinitionLabel()} Line Item");
            }

            //delete column
            if (hasDeletePermission)
            {
                Add("Delete", x => AgGridHtmlHelpers.MakeDeleteIconAndLinkBootstrap(x.GetDeleteSuppliesLineItemUrl(), true, true), 30, AgGridColumnFilterType.None);
            }
            //edit column
            if (hasEditPermission)
            {
                Add("Edit", x => AgGridHtmlHelpers.MakeEditIconAsModalDialogLinkBootstrap(new ModalDialogForm(x.GetEditSuppliesLineItemUrl(), $"Edit this {Models.FieldDefinition.GrantAllocationAwardSupplies.GetFieldDefinitionLabel()} Line Item")), 30, AgGridColumnFilterType.None);
            }

            Add(Models.FieldDefinition.GrantAllocationAwardSuppliesDescription.ToGridHeaderString(), s => s.GrantAllocationAwardSuppliesLineItemDescription, 200, AgGridColumnFilterType.Text);
            Add(Models.FieldDefinition.GrantAllocationAwardSuppliesTarOrMonth.ToGridHeaderString(), s => s.GrantAllocationAwardSuppliesLineItemTarOrMonth , 125, AgGridColumnFilterType.SelectFilterStrict);
            Add(Models.FieldDefinition.GrantAllocationAwardSuppliesDate.ToGridHeaderString(), s => s.GrantAllocationAwardSuppliesLineItemDate, 125, AgGridColumnFormatType.Date);
            Add(Models.FieldDefinition.GrantAllocationAwardSuppliesAmount.ToGridHeaderString(), s => s.GrantAllocationAwardSuppliesLineItemAmount, 125, AgGridColumnFormatType.CurrencyWithCents, AgGridColumnAggregationType.Total);
            Add(Models.FieldDefinition.GrantAllocationAwardSuppliesNotes.ToGridHeaderString(), s => s.GrantAllocationAwardSuppliesLineItemNotes, 250, AgGridColumnFilterType.Text);
        }
    }
}
