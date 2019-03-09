/*-----------------------------------------------------------------------
<copyright file="EditProjectViewModel.cs" company="Tahoe Regional Planning Agency and Sitka Technology Group">
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
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using ApprovalUtilities.Utilities;
using ProjectFirma.Web.Common;
using ProjectFirma.Web.Models;
using LtInfo.Common;
using LtInfo.Common.Models;

namespace ProjectFirma.Web.Views.Invoice
{
    public class EditInvoiceViewModel : FormViewModel, IValidatableObject
    {
        public int InvoiceID { get; set; }

        [FieldDefinitionDisplay(FieldDefinitionEnum.InvoiceIdentifyingName)]
        [Required]
        public string InvoiceIdentifyingName { get; set; }

        [FieldDefinitionDisplay(FieldDefinitionEnum.RequestorName)]
        public string RequestorName { get; set; }

        [FieldDefinitionDisplay(FieldDefinitionEnum.InvoiceDate)]
        public DateTime InvoiceDate { get; set; }

        [FieldDefinitionDisplay(FieldDefinitionEnum.PurchaseAuthority)]
        public string PurchaseAuthority { get; set; }

        [FieldDefinitionDisplay(FieldDefinitionEnum.PurchaseAuthority)]
        public bool PurchaseAuthorityIsLandownerCostShareAgreement { get; set; }

        [FieldDefinitionDisplay(FieldDefinitionEnum.TotalRequestedInvoicePaymentAmount)]
        public Money? InvoiceAmount { get; set; }

        [FieldDefinitionDisplay(FieldDefinitionEnum.InvoiceApprovalStatus)]
        public int InvoiceApprovalStatusID { get; set; }

        [FieldDefinitionDisplay(FieldDefinitionEnum.InvoiceApprovalComment)]
        public string InvoiceApprovalStatusComment { get; set; }

        /// <summary>
        /// Needed by the ModelBinder
        /// </summary>
        public EditInvoiceViewModel()
        {
        }

        public EditInvoiceViewModel(Models.Invoice invoice)
        {
            InvoiceIdentifyingName = invoice.InvoiceIdentifyingName;
            RequestorName = invoice.RequestorName;
            InvoiceDate = invoice.InvoiceDate;
            PurchaseAuthority = invoice.PurchaseAuthority;
            InvoiceAmount = invoice.TotalPaymentAmount;
            InvoiceApprovalStatusID = invoice.InvoiceApprovalStatusID;
            InvoiceApprovalStatusComment = invoice.InvoiceApprovalStatusComment;
            PurchaseAuthorityIsLandownerCostShareAgreement =
                invoice.PurchaseAuthorityIsLandownerCostShareAgreement;
        }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (InvoiceIdentifyingName == "")
            {
                yield return new SitkaValidationResult<EditInvoiceViewModel, string>(
                    FirmaValidationMessages.InvoiceNicknameMustBePopulated, m => m.InvoiceIdentifyingName);
            }
        }

        public void UpdateModel(Models.Invoice invoice, Person currentPerson)
        {
            invoice.InvoiceIdentifyingName = InvoiceIdentifyingName;
            invoice.RequestorName = RequestorName;
            invoice.InvoiceDate = InvoiceDate;
            invoice.PurchaseAuthority = PurchaseAuthority;
            invoice.TotalPaymentAmount = InvoiceAmount;
            invoice.InvoiceApprovalStatusID = InvoiceApprovalStatusID;
            invoice.InvoiceApprovalStatusComment = InvoiceApprovalStatusComment;
            invoice.PurchaseAuthorityIsLandownerCostShareAgreement = PurchaseAuthorityIsLandownerCostShareAgreement;
        }
    }
}