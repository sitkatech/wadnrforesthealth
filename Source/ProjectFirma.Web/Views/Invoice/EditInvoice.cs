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
    public abstract class EditInvoice : TypedWebPartialViewPage<EditInvoiceViewData, EditInvoiceViewModel>
    {
    }

    public abstract class EditInvoiceType
    {
        public readonly string IntroductoryText;

        internal EditInvoiceType(string introductoryText)
        {
            IntroductoryText = introductoryText;
        }

        public static readonly EditInvoiceTypeNewInvoice NewInvoice = EditInvoiceTypeNewInvoice.Instance;
        public static readonly EditInvoiceTypeExistingInvoice ExistingInvoice = EditInvoiceTypeExistingInvoice.Instance;
    }

    public class EditInvoiceTypeNewInvoice : EditInvoiceType
    {
        private EditInvoiceTypeNewInvoice(string introductoryText) : base(introductoryText)
        {
        }

        public static readonly EditInvoiceTypeNewInvoice Instance = new EditInvoiceTypeNewInvoice(
            $"<p>Enter basic information about the {Models.FieldDefinition.Invoice.GetFieldDefinitionLabel()}.</p>");
    }

    public class EditInvoiceTypeExistingInvoice : EditInvoiceType
    {
        private EditInvoiceTypeExistingInvoice(string introductoryText) : base(introductoryText)
        {
        }

        public static readonly EditInvoiceTypeExistingInvoice Instance =
            new EditInvoiceTypeExistingInvoice(
                $"<p>Update this {Models.FieldDefinition.Invoice.GetFieldDefinitionLabel()}'s information.</p>");
    }
}
