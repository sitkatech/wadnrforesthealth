/*-----------------------------------------------------------------------
<copyright file="EditProject.cs" company="Tahoe Regional Planning Agency and Sitka Technology Group">
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

using LtInfo.Common.Mvc;

namespace ProjectFirma.Web.Views.Invoice
{
    public abstract class EditInvoicePaymentRequest : TypedWebPartialViewPage<EditInvoicePaymentRequestViewData, EditInvoicePaymentRequestViewModel>
    {
    }

    public abstract class EditInvoicePaymentRequestType
    {
        public readonly string IntroductoryText;

        internal EditInvoicePaymentRequestType(string introductoryText)
        {
            IntroductoryText = introductoryText;
        }

        public static readonly EditInvoicePaymentRequestTypeNewIpr NewIpr = EditInvoicePaymentRequestTypeNewIpr.Instance;
        public static readonly EditInvoicePaymentRequestTypeExistingIpr ExistingIpr = EditInvoicePaymentRequestTypeExistingIpr.Instance;
    }

    public class EditInvoicePaymentRequestTypeNewIpr : EditInvoicePaymentRequestType
    {
        private EditInvoicePaymentRequestTypeNewIpr(string introductoryText) : base(introductoryText)
        {
        }

        public static readonly EditInvoicePaymentRequestTypeNewIpr Instance = new EditInvoicePaymentRequestTypeNewIpr(
            $"<p>Enter basic information about the {Models.FieldDefinition.InvoicePaymentRequest.GetFieldDefinitionLabel()}.</p>");
    }

    public class EditInvoicePaymentRequestTypeExistingIpr : EditInvoicePaymentRequestType
    {
        private EditInvoicePaymentRequestTypeExistingIpr(string introductoryText) : base(introductoryText)
        {
        }

        public static readonly EditInvoicePaymentRequestTypeExistingIpr Instance =
            new EditInvoicePaymentRequestTypeExistingIpr(
                $"<p>Update this {Models.FieldDefinition.InvoicePaymentRequest.GetFieldDefinitionLabel()}'s information.</p>");
    }
}
