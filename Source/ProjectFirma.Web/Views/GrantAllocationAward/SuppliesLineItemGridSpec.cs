/*-----------------------------------------------------------------------
<copyright file="SuppliesLineItemGridSpec.cs" company="Tahoe Regional Planning Agency and Sitka Technology Group">
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
                Add(string.Empty, x => DhtmlxGridHtmlHelpers.MakeDeleteIconAndLinkBootstrap(x.GetDeleteSuppliesLineItemUrl(), true, true), 30, DhtmlxGridColumnFilterType.None);
            }
            //edit column
            if (hasEditPermission)
            {
                Add(string.Empty, x => DhtmlxGridHtmlHelpers.MakeEditIconAsModalDialogLinkBootstrap(new ModalDialogForm(x.GetEditSuppliesLineItemUrl(), $"Edit this {Models.FieldDefinition.GrantAllocationAwardSupplies.GetFieldDefinitionLabel()} Line Item")), 30, DhtmlxGridColumnFilterType.None);
            }

            Add(Models.FieldDefinition.GrantAllocationAwardSuppliesDescription.ToGridHeaderString(), s => s.GrantAllocationAwardSuppliesLineItemDescription, 200, DhtmlxGridColumnFilterType.Text);
            Add(Models.FieldDefinition.GrantAllocationAwardSuppliesTarOrMonth.ToGridHeaderString(), s => s.GrantAllocationAwardSuppliesLineItemTarOrMonth , 125, DhtmlxGridColumnFilterType.SelectFilterStrict);
            Add(Models.FieldDefinition.GrantAllocationAwardSuppliesDate.ToGridHeaderString(), s => s.GrantAllocationAwardSuppliesLineItemDate, 125, DhtmlxGridColumnFormatType.Date);
            Add(Models.FieldDefinition.GrantAllocationAwardSuppliesAmount.ToGridHeaderString(), s => s.GrantAllocationAwardSuppliesLineItemAmount, 125, DhtmlxGridColumnFormatType.CurrencyWithCents, DhtmlxGridColumnAggregationType.Total);
            Add(Models.FieldDefinition.GrantAllocationAwardSuppliesNotes.ToGridHeaderString(), s => s.GrantAllocationAwardSuppliesLineItemNotes, 250, DhtmlxGridColumnFilterType.Text);
        }
    }
}
