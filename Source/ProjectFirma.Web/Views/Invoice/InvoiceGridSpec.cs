/*-----------------------------------------------------------------------
<copyright file="IndexGridSpec.cs" company="Tahoe Regional Planning Agency and Sitka Technology Group">
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

namespace ProjectFirma.Web.Views.Invoice
{
    public class InvoiceGridSpec : GridSpec<Models.Invoice>
    {
        public const int InvoiceColumnWidth = 180;
        public static string InvoiceIdHiddenColumnName = "InvoiceIdAsText";

        public InvoiceGridSpec(Models.Person currentPerson, bool invoiceFileExistsOnAtLeastOne)
        {
            ObjectNameSingular = $"{Models.FieldDefinition.Invoice.GetFieldDefinitionLabel()}";
            ObjectNamePlural = $"{Models.FieldDefinition.Invoice.GetFieldDefinitionLabelPluralized()}";
            SaveFiltersInCookie = true;

            //10/10/22 TK - TODO: allow new invoices, once controller action is updated to provide the IPR ID
            var userHasCreatePermissions = new InvoiceCreateFeature().HasPermissionByPerson(currentPerson);
            if (userHasCreatePermissions)
            {
                //var contentUrl = SitkaRoute<InvoiceController>.BuildUrlFromExpression(t => t.New());
                //CreateEntityModalDialogForm = new ModalDialogForm(contentUrl, "Create a new Invoice");
            }

            if (invoiceFileExistsOnAtLeastOne)
            {
                Add(string.Empty, x => DhtmlxGridHtmlHelpers.MakeFileDownloadIconAsHyperlinkBootstrap(x.GetFileDownloadUrl(), "Download Invoice file"), 30, DhtmlxGridColumnFilterType.None);
            }

            Add("Invoice ID", x => UrlTemplate.MakeHrefString(x.GetDetailUrl(), x.InvoiceID.ToString()), 50);
            Add(Models.FieldDefinition.InvoiceIdentifyingName.ToGridHeaderString(), x => UrlTemplate.MakeHrefString(x.GetDetailUrl(), x.InvoiceIdentifyingName),
                InvoiceGridSpec.InvoiceColumnWidth, DhtmlxGridColumnFilterType.SelectFilterHtmlStrict);

            Add(Models.FieldDefinition.InvoiceDate.ToGridHeaderString(), x => x.InvoiceDate, 90, DhtmlxGridColumnFormatType.Date);

            Add(Models.FieldDefinition.InvoiceStatus.ToGridHeaderString(), x =>
                x.InvoiceStatus.InvoiceStatusDisplayName, InvoiceGridSpec.InvoiceColumnWidth, DhtmlxGridColumnFilterType.SelectFilterStrict);
            Add(Models.FieldDefinition.TotalRequestedInvoicePaymentAmount.ToGridHeaderString(), x => x.TotalPaymentAmount.ToStringCurrency(), 90, DhtmlxGridColumnFilterType.SelectFilterStrict);
            Add(Models.FieldDefinition.InvoiceApprovalStatus.ToGridHeaderString(), x =>
                    x.InvoiceApprovalStatus.InvoiceApprovalStatusName, InvoiceGridSpec.InvoiceColumnWidth, DhtmlxGridColumnFilterType.SelectFilterStrict);

        }
    }
}
