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

        public InvoiceGridSpec(Models.Person currentPerson, bool invoiceFileExistsOnAtLeastOne, InvoicePaymentRequest invoicePaymentRequest)
        {
            ObjectNameSingular = $"{Models.FieldDefinition.Invoice.GetFieldDefinitionLabel()}";
            ObjectNamePlural = $"{Models.FieldDefinition.Invoice.GetFieldDefinitionLabelPluralized()}";
            SaveFiltersInCookie = true;


            var userHasCreatePermissions = new InvoiceCreateFeature().HasPermissionByPerson(currentPerson);
            if (userHasCreatePermissions)
            {
                var contentUrl = SitkaRoute<InvoiceController>.BuildUrlFromExpression(t => t.New(invoicePaymentRequest));
                CreateEntityModalDialogForm = new ModalDialogForm(contentUrl, $"Create a new {Models.FieldDefinition.Invoice.GetFieldDefinitionLabel()}");
            }

            var userHasEditPermissions = new InvoiceEditFeature().HasPermissionByPerson(currentPerson);
            if (userHasEditPermissions)
            {
                Add(string.Empty, x => DhtmlxGridHtmlHelpers.MakeEditIconAsModalDialogLinkBootstrap(new ModalDialogForm(x.GetEditUrl(), $"Edit {ObjectNameSingular} - {x.InvoiceNumber}"),
                    userHasEditPermissions), 30, DhtmlxGridColumnFilterType.None, true);
            }

            if (invoiceFileExistsOnAtLeastOne)
            {
                Add(string.Empty, x => DhtmlxGridHtmlHelpers.MakeFileDownloadIconAsHyperlinkBootstrap(x.GetFileDownloadUrl(), "Download Invoice file"), 30, DhtmlxGridColumnFilterType.None);
            }

            Add(Models.FieldDefinition.GrantNumber.ToGridHeaderString(), x => x.Grant?.GetGrantNumberAsUrl(), 180,
                DhtmlxGridColumnFilterType.Text);
            Add(Models.FieldDefinition.InvoiceNumber.ToGridHeaderString(), x => x.InvoiceNumber, 90, DhtmlxGridColumnFilterType.Text);
            Add(Models.FieldDefinition.InvoiceDate.ToGridHeaderString(), x => x.InvoiceDate, 90, DhtmlxGridColumnFormatType.Date);
            Add(Models.FieldDefinition.Fund.ToGridHeaderString(), x => x.Fund, 90, DhtmlxGridColumnFilterType.Text);
            Add(Models.FieldDefinition.Appn.ToGridHeaderString(), x => x.Appn, 90, DhtmlxGridColumnFilterType.Text);
            Add(Models.FieldDefinition.ProgramIndex.ToGridHeaderString(), x => x.ProgramIndex?.ProgramIndexCode, 40, DhtmlxGridColumnFilterType.Text);
            Add(Models.FieldDefinition.ProjectCode.ToGridHeaderString(), x => x.ProjectCode?.ProjectCodeName, 40, DhtmlxGridColumnFilterType.Text);
            Add(Models.FieldDefinition.SubObject.ToGridHeaderString(), x => x.SubObject, 90, DhtmlxGridColumnFilterType.Text);
            Add(Models.FieldDefinition.OrganizationCode.ToGridHeaderString(), x => x.OrganizationCode?.OrganizationCodeValue, 90, DhtmlxGridColumnFilterType.SelectFilterStrict);
            Add(Models.FieldDefinition.MatchAmount.ToGridHeaderString(), x => x.MatchAmountForDisplay, 60);
            Add(Models.FieldDefinition.PaymentAmount.ToGridHeaderString(), x => x.PaymentAmount.ToStringCurrency(), 90, DhtmlxGridColumnFilterType.FormattedNumeric);


            Add(Models.FieldDefinition.InvoiceStatus.ToGridHeaderString(), x =>
                x.InvoiceStatus.InvoiceStatusDisplayName, InvoiceGridSpec.InvoiceColumnWidth, DhtmlxGridColumnFilterType.SelectFilterStrict);
            Add(Models.FieldDefinition.InvoiceApprovalStatus.ToGridHeaderString(), x =>
                    x.InvoiceApprovalStatus.InvoiceApprovalStatusName, InvoiceGridSpec.InvoiceColumnWidth, DhtmlxGridColumnFilterType.SelectFilterStrict);

            Add(Models.FieldDefinition.InvoiceIdentifyingName.ToGridHeaderString(), x =>  x.InvoiceIdentifyingName,
                InvoiceGridSpec.InvoiceColumnWidth, DhtmlxGridColumnFilterType.Text);

        }
    }
}
