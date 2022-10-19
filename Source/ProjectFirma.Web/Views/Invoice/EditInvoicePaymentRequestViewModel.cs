/*-----------------------------------------------------------------------
<copyright file="EditInvoicePaymentRequestViewModel.cs" company="Tahoe Regional Planning Agency and Sitka Technology Group">
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
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using ApprovalUtilities.Utilities;
using ProjectFirma.Web.Common;
using ProjectFirma.Web.Models;
using LtInfo.Common;
using LtInfo.Common.Models;

namespace ProjectFirma.Web.Views.Invoice
{
    public class EditInvoicePaymentRequestViewModel : FormViewModel, IValidatableObject
    {
        public int InvoicePaymentRequestID { get; set; }
        public int ProjectID { get; set;}

        [FieldDefinitionDisplay(FieldDefinitionEnum.Vendor)]
        public int? VendorID { get; set;}

        [FieldDefinitionDisplay(FieldDefinitionEnum.PreparedByPerson)]
        [Required]
        public int? PreparedByPersonID { get; set;}

        [FieldDefinitionDisplay(FieldDefinitionEnum.PurchaseAuthority)]
        public string PurchaseAuthority { get; set;}
        [FieldDefinitionDisplay(FieldDefinitionEnum.PurchaseAuthority)]
        [Required]
        public bool? PurchaseAuthorityIsLandownerCostShareAgreement { get; set;}
        [DisplayName("DUNS")]
        [StringLength(InvoicePaymentRequest.FieldLengths.Duns)]
        public string Duns { get; set;}
        [DisplayName("Date")]
        public DateTime InvoicePaymentRequestDate { get; set;}
        [DisplayName("Notes")]
        public string Notes { get; set;}
        

        /// <summary>
        /// Needed by the ModelBinder
        /// </summary>
        public EditInvoicePaymentRequestViewModel()
        {
            InvoicePaymentRequestDate = DateTime.Now.Date;
        }

        public EditInvoicePaymentRequestViewModel(Models.InvoicePaymentRequest invoicePaymentRequest)
        {
            InvoicePaymentRequestID = invoicePaymentRequest.InvoicePaymentRequestID;
            ProjectID = invoicePaymentRequest.ProjectID;
            VendorID = invoicePaymentRequest.VendorID;
            PreparedByPersonID = invoicePaymentRequest.PreparedByPersonID;
            PurchaseAuthority = invoicePaymentRequest.PurchaseAuthority;
            PurchaseAuthorityIsLandownerCostShareAgreement = invoicePaymentRequest.PurchaseAuthorityIsLandownerCostShareAgreement;
            Duns = invoicePaymentRequest.Duns;
            InvoicePaymentRequestDate = invoicePaymentRequest.InvoicePaymentRequestDate;
            Notes = invoicePaymentRequest.Notes;

        }

        public void UpdateModel(Models.InvoicePaymentRequest invoicePaymentRequest, Person currentPerson)
        {
            invoicePaymentRequest.InvoicePaymentRequestID = InvoicePaymentRequestID;
            invoicePaymentRequest.ProjectID = ProjectID;
            invoicePaymentRequest.VendorID = VendorID;
            invoicePaymentRequest.PreparedByPersonID = PreparedByPersonID;
            invoicePaymentRequest.PurchaseAuthority = PurchaseAuthority;
            invoicePaymentRequest.PurchaseAuthorityIsLandownerCostShareAgreement = PurchaseAuthorityIsLandownerCostShareAgreement ?? false;
            invoicePaymentRequest.Duns = Duns;
            invoicePaymentRequest.InvoicePaymentRequestDate = InvoicePaymentRequestDate;
            invoicePaymentRequest.Notes = Notes;

        }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (PurchaseAuthorityIsLandownerCostShareAgreement.HasValue && PurchaseAuthorityIsLandownerCostShareAgreement.Value && !string.IsNullOrWhiteSpace(PurchaseAuthority))
            {
                yield return new SitkaValidationResult<EditInvoicePaymentRequestViewModel, string>(
                    FirmaValidationMessages.PurchaseAuthorityAgreementNumberMustBeBlankIfIsLandOwnerAgreement, m => m.PurchaseAuthority);
            }


        }
    }
}