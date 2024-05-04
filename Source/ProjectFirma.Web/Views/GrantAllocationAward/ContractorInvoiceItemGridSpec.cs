/*-----------------------------------------------------------------------
<copyright file="ContractorInvoiceItemGridSpec.cs" company="Tahoe Regional Planning Agency and Environmental Science Associates">
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
    public class ContractorInvoiceItemGridSpec : GridSpec<GrantAllocationAwardContractorInvoice>
    {
        public ContractorInvoiceItemGridSpec(Person currentPerson, Models.GrantAllocationAward grantAllocationAward)
        {
            ShowFilterBar = true;
            ObjectNameSingular = Models.FieldDefinition.GrantAllocationAwardContractorInvoiceLineItem.GetFieldDefinitionLabel();
            ObjectNamePlural = Models.FieldDefinition.GrantAllocationAwardContractorInvoiceLineItem.GetFieldDefinitionLabelPluralized();

            bool hasDeletePermission = new GrantAllocationAwardContractorInvoiceItemDeleteFeature().HasPermissionByPerson(currentPerson);
            bool hasEditPermission = new GrantAllocationAwardContractorInvoiceItemEditAsAdminFeature().HasPermissionByPerson(currentPerson);
            bool hasCreatePermission = new GrantAllocationAwardContractorInvoiceItemCreateFeature().HasPermissionByPerson(currentPerson);
            if (hasCreatePermission)
            {
                var newContractorInvoiceUrl = SitkaRoute<GrantAllocationAwardController>.BuildUrlFromExpression(t => t.NewContractorInvoiceItemFromGrantAllocationAward(grantAllocationAward.PrimaryKey));
                CreateEntityModalDialogForm = new ModalDialogForm(newContractorInvoiceUrl, $"Create a new {Models.FieldDefinition.GrantAllocationAwardContractorInvoice.GetFieldDefinitionLabel()} Line Item");
            }

            //delete column
            if (hasDeletePermission)
            {
                Add(string.Empty, x => AgGridHtmlHelpers.MakeDeleteIconAndLinkBootstrap(x.GetDeleteContractorInvoiceUrl(), true, true), 30, AgGridColumnFilterType.None);
            }
            //edit column
            if (hasEditPermission)
            {
                Add(string.Empty, x => AgGridHtmlHelpers.MakeEditIconAsModalDialogLinkBootstrap(new ModalDialogForm(x.GetEditContractorInvoiceUrl(), $"Edit this {Models.FieldDefinition.GrantAllocationAwardContractorInvoice.GetFieldDefinitionLabel()} Line Item")), 30, AgGridColumnFilterType.None);
            }

            //need column for file download


            Add(Models.FieldDefinition.GrantAllocationAwardContractorInvoiceDescription.ToGridHeaderString(), s => s.GrantAllocationAwardContractorInvoiceDescription, 200, AgGridColumnFilterType.Text);
            Add(Models.FieldDefinition.GrantAllocationAwardContractorInvoiceNumber.ToGridHeaderString(), s => s.GrantAllocationAwardContractorInvoiceNumber, 125, AgGridColumnFilterType.SelectFilterStrict);
            Add(Models.FieldDefinition.GrantAllocationAwardContractorInvoiceDate.ToGridHeaderString(), s => s.GrantAllocationAwardContractorInvoiceDate, 125, AgGridColumnFormatType.Date);

            Add(Models.FieldDefinition.GrantAllocationAwardContractorInvoiceForemanHours.ToGridHeaderString(), s => s.GrantAllocationAwardContractorInvoiceForemanHours, 125, AgGridColumnFormatType.Decimal, AgGridColumnAggregationType.Total);
            Add(Models.FieldDefinition.GrantAllocationAwardContractorInvoiceForemanRate.ToGridHeaderString(), s => s.GrantAllocationAwardContractorInvoiceForemanRate, 125, AgGridColumnFormatType.CurrencyWithCents);
            Add("Foreman Amount", s => s.GrantAllocationAwardContractorInvoiceForemanAmount, 125, AgGridColumnFormatType.CurrencyWithCents, AgGridColumnAggregationType.Total);

            Add(Models.FieldDefinition.GrantAllocationAwardContractorInvoiceLaborHours.ToGridHeaderString(), s => s.GrantAllocationAwardContractorInvoiceLaborHours, 125, AgGridColumnFormatType.Decimal, AgGridColumnAggregationType.Total);
            Add(Models.FieldDefinition.GrantAllocationAwardContractorInvoiceLaborRate.ToGridHeaderString(), s => s.GrantAllocationAwardContractorInvoiceLaborRate, 125, AgGridColumnFormatType.CurrencyWithCents);
            Add("Labor Amount", s => s.GrantAllocationAwardContractorInvoiceLaborAmount, 125, AgGridColumnFormatType.CurrencyWithCents, AgGridColumnAggregationType.Total);

            Add(Models.FieldDefinition.GrantAllocationAwardContractorInvoiceGrappleHours.ToGridHeaderString(), s => s.GrantAllocationAwardContractorInvoiceGrappleHours, 125, AgGridColumnFormatType.Decimal, AgGridColumnAggregationType.Total);
            Add(Models.FieldDefinition.GrantAllocationAwardContractorInvoiceGrappleRate.ToGridHeaderString(), s => s.GrantAllocationAwardContractorInvoiceGrappleRate, 125, AgGridColumnFormatType.CurrencyWithCents);
            Add("Grapple Amount", s => s.GrantAllocationAwardContractorInvoiceGrappleAmount, 125, AgGridColumnFormatType.CurrencyWithCents, AgGridColumnAggregationType.Total);

            Add(Models.FieldDefinition.GrantAllocationAwardContractorInvoiceMasticationHours.ToGridHeaderString(), s => s.GrantAllocationAwardContractorInvoiceMasticationHours, 125, AgGridColumnFormatType.Decimal, AgGridColumnAggregationType.Total);
            Add(Models.FieldDefinition.GrantAllocationAwardContractorInvoiceMasticationRate.ToGridHeaderString(), s => s.GrantAllocationAwardContractorInvoiceMasticationRate, 125, AgGridColumnFormatType.CurrencyWithCents);
            Add("Mastication Amount", s => s.GrantAllocationAwardContractorInvoiceMasticationAmount, 125, AgGridColumnFormatType.CurrencyWithCents, AgGridColumnAggregationType.Total);

            Add("Invoice w/o Tax", s => s.GrantAllocationAwardContractorInvoiceTotalWithoutTax, 125, AgGridColumnFormatType.CurrencyWithCents, AgGridColumnAggregationType.Total);

            Add(Models.FieldDefinition.GrantAllocationAwardContractorInvoiceTaxRate.ToGridHeaderString(), s => s.GrantAllocationAwardContractorInvoiceTaxRate, 125, AgGridColumnFormatType.Percent);
            Add("Tax Amount", s => s.GrantAllocationAwardContractorInvoiceTaxAmount, 125, AgGridColumnFormatType.CurrencyWithCents, AgGridColumnAggregationType.Total);

            Add("Invoice Total", s => s.GrantAllocationAwardContractorInvoiceTotalWithTax, 125, AgGridColumnFormatType.CurrencyWithCents, AgGridColumnAggregationType.Total);

            Add(Models.FieldDefinition.GrantAllocationAwardContractorInvoiceAcresReported.ToGridHeaderString(), s => s.GrantAllocationAwardContractorInvoiceAcresReported, 125, AgGridColumnFormatType.Decimal, AgGridColumnAggregationType.Total);
            Add(Models.FieldDefinition.GrantAllocationAwardContractorInvoiceNotes.ToGridHeaderString(), s => s.GrantAllocationAwardContractorInvoiceNotes, 250, AgGridColumnFilterType.Text);
        }
    }
}
