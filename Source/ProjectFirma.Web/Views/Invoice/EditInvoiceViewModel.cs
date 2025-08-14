/*-----------------------------------------------------------------------
<copyright file="EditInvoiceViewModel.cs" company="Tahoe Regional Planning Agency and Environmental Science Associates">
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
    public class EditInvoiceViewModel : FormViewModel, IValidatableObject
    {
        [Required]
        public int InvoiceID { get; set; }

        [FieldDefinitionDisplay(FieldDefinitionEnum.InvoiceIdentifyingName)]
        public string InvoiceIdentifyingName { get; set; }
        
        [FieldDefinitionDisplay(FieldDefinitionEnum.InvoiceDate)]
        [Required]
        public DateTime? InvoiceDate { get; set; }

        [FieldDefinitionDisplay(FieldDefinitionEnum.PaymentAmount)]
        public Money? PaymentAmount { get; set; }

        [FieldDefinitionDisplay(FieldDefinitionEnum.InvoiceApprovalStatus)]
        [Required]
        public int InvoiceApprovalStatusID { get; set; }

        [FieldDefinitionDisplay(FieldDefinitionEnum.InvoiceApprovalComment)]
        public string InvoiceApprovalStatusComment { get; set; }

        [FieldDefinitionDisplay(FieldDefinitionEnum.MatchAmount)]
        [Required]
        public int InvoiceMatchAmountTypeID { get; set; }

        [FieldDefinitionDisplay(FieldDefinitionEnum.MatchAmount)]
        public Money? MatchAmount { get; set; }

        [FieldDefinitionDisplay(FieldDefinitionEnum.InvoiceStatus)]
        [Required]
        public int InvoiceStatusID { get; set; }

        [DisplayName("Invoice Voucher Upload")]
        //[SitkaFileExtensions("jpg|jpeg|gif|png")]
        public HttpPostedFileBase InvoiceFileResourceData { get; set; }

        [Required]
        public int InvoicePaymentRequestID { get; set; }

        [FieldDefinitionDisplay(FieldDefinitionEnum.FundSource)]
        public int? FundSourceID { get; set; }

        [FieldDefinitionDisplay(FieldDefinitionEnum.ProgramIndex)]
        public int? ProgramIndexID { get; set; }
        
        [FieldDefinitionDisplay(FieldDefinitionEnum.ProjectCode)]
        public int? ProjectCodeID { get; set; }
        
        [FieldDefinitionDisplay(FieldDefinitionEnum.OrganizationCode)]
        public int? OrganizationCodeID { get; set; }
        
        [Required]
        [FieldDefinitionDisplay(FieldDefinitionEnum.InvoiceNumber)]
        public string InvoiceNumber { get; set; }
        
        [StringLength(Models.Invoice.FieldLengths.Fund)]
        [FieldDefinitionDisplay(FieldDefinitionEnum.Fund)]
        public string Fund { get; set; }
        
        [StringLength(Models.Invoice.FieldLengths.Appn)]
        [FieldDefinitionDisplay(FieldDefinitionEnum.Appn)]
        public string Appn { get; set; }
        
        [StringLength(Models.Invoice.FieldLengths.SubObject)]
        [FieldDefinitionDisplay(FieldDefinitionEnum.SubObject)]
        public string SubObject { get; set; }

        /// <summary>
        /// Needed by the ModelBinder
        /// </summary>
        public EditInvoiceViewModel()
        {
        }

        public EditInvoiceViewModel(Models.Invoice invoice)
        {
            InvoiceIdentifyingName = invoice.InvoiceIdentifyingName;
            InvoiceDate = invoice.InvoiceDate;
            PaymentAmount = invoice.PaymentAmount;
            InvoiceApprovalStatusID = invoice.InvoiceApprovalStatusID;
            InvoiceApprovalStatusComment = invoice.InvoiceApprovalStatusComment;
            MatchAmount = invoice.MatchAmount;
            InvoiceMatchAmountTypeID = invoice.InvoiceMatchAmountTypeID;
            InvoiceStatusID = invoice.InvoiceStatusID;
            InvoicePaymentRequestID = invoice.InvoicePaymentRequestID;
            FundSourceID = invoice.FundSourceID;
            ProgramIndexID = invoice.ProgramIndexID;
            ProjectCodeID = invoice.ProjectCodeID;
            InvoiceNumber = invoice.InvoiceNumber;
            Fund = invoice.Fund;
            Appn = invoice.Appn;
            SubObject = invoice.SubObject;
            OrganizationCodeID = invoice.OrganizationCodeID;

        }

        public void UpdateModel(Models.Invoice invoice, Person currentPerson)
        {
            invoice.InvoiceIdentifyingName = InvoiceIdentifyingName;
            invoice.InvoiceDate = InvoiceDate ?? DateTime.MaxValue;
            invoice.PaymentAmount = PaymentAmount;
            invoice.InvoiceApprovalStatusID = InvoiceApprovalStatusID;
            invoice.InvoiceApprovalStatusComment = InvoiceApprovalStatusComment;
            invoice.MatchAmount = MatchAmount;
            invoice.InvoiceMatchAmountTypeID = InvoiceMatchAmountTypeID;

            invoice.InvoiceStatusID = InvoiceStatusID;
            invoice.FundSourceID = FundSourceID;
            invoice.ProgramIndexID = ProgramIndexID;
            invoice.ProjectCodeID = ProjectCodeID;
            invoice.InvoiceNumber = InvoiceNumber;
            invoice.Fund = Fund;
            invoice.Appn = Appn;
            invoice.SubObject = SubObject;
            invoice.OrganizationCodeID = OrganizationCodeID;

            if (InvoiceFileResourceData != null)
            {
                var currentInvoiceFileResource = invoice.InvoiceFileResource;
                invoice.InvoiceFileResource = null;
                // Delete old Invoice file, if present
                if (currentInvoiceFileResource != null)
                {
                    HttpRequestStorage.DatabaseEntities.SaveChanges();
                    HttpRequestStorage.DatabaseEntities.FileResources.DeleteFileResource(currentInvoiceFileResource);
                }
                invoice.InvoiceFileResource = FileResource.CreateNewFromHttpPostedFileAndSave(InvoiceFileResourceData, currentPerson);
            }
        }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (InvoiceIdentifyingName == "")
            {
                yield return new SitkaValidationResult<EditInvoiceViewModel, string>(
                    FirmaValidationMessages.InvoiceNicknameMustBePopulated, m => m.InvoiceIdentifyingName);
            }


            if (InvoiceMatchAmountTypeID == InvoiceMatchAmountType.DollarAmount.InvoiceMatchAmountTypeID && !MatchAmount.HasValue)
            {
                yield return new SitkaValidationResult<EditInvoiceViewModel, Money?>(
                    FirmaValidationMessages.InvoiceMatchAmountDollarValueMustNotBeNull, m => m.MatchAmount);
            }


            if (InvoiceApprovalStatusID == InvoiceApprovalStatus.Denied.InvoiceApprovalStatusID && String.IsNullOrEmpty(InvoiceApprovalStatusComment))
            {
                yield return new SitkaValidationResult<EditInvoiceViewModel, string>(
                    FirmaValidationMessages.InvoiceApprovalStatusCommentIsRequiredIfStatusIsDenied, m => m.InvoiceApprovalStatusComment);
            }
        }
    }
}