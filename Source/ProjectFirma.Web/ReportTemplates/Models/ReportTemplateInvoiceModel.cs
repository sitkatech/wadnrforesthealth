using System;
using System.Collections.Generic;
using System.Linq;
using ProjectFirma.Web.Models;

namespace ProjectFirma.Web.ReportTemplates.Models
{
    public class ReportTemplateInvoiceModel : ReportTemplateBaseModel
    {

        // Public properties
        public DateTime InvoiceDate { get; set; }
        public string InvoiceDateDisplay => InvoiceDate.ToShortDateString();
        public decimal? PaymentAmount { get; set; }
        public string PaymentAmountDisplay(int decimalPlaces = 2) => PaymentAmount.HasValue ? PaymentAmount.Value.ToString($"C{decimalPlaces}") : string.Empty;
        public decimal? MatchAmount { get; set; }
        private string MatchAmountDisplayFromModel { get; set; }
        public string MatchAmountDisplay(int decimalPlaces = 2) => MatchAmount.HasValue ? MatchAmount.Value.ToString($"C{decimalPlaces}") : MatchAmountDisplayFromModel;
        public string FundSourceNumber { get; set; }
        public string ProgramIndexCode { get; set; }
        public string ProjectCodeName { get; set; }
        public string OrganizationCodeValue { get; set; }
        public string OrganizationCodeName { get; set; }
        public string OrganizationCodeDisplay =>
            !string.IsNullOrEmpty(OrganizationCodeValue) && !string.IsNullOrEmpty(OrganizationCodeName)
                ? $"{OrganizationCodeValue} - {OrganizationCodeName}"
                : string.Empty;

        public string InvoiceNumber { get; set; }
        public string Fund { get; set; }
        public string Appn { get; set; }
        public string SubObject { get; set; }


        public ReportTemplateInvoiceModel(Invoice invoice)
        {
            if (invoice != null)
            {
                // Public properties
                InvoiceDate = invoice.InvoiceDate;
                PaymentAmount = invoice.PaymentAmount;
                MatchAmount = invoice.MatchAmount;
                MatchAmountDisplayFromModel = invoice.MatchAmountForDisplay;
                FundSourceNumber = invoice.FundSource?.FundSourceNumber;
                ProgramIndexCode = invoice.ProgramIndex?.ProgramIndexCode;
                ProjectCodeName = invoice.ProjectCode?.ProjectCodeName;
                OrganizationCodeValue = invoice.OrganizationCode?.OrganizationCodeValue;
                OrganizationCodeName = invoice.OrganizationCode?.OrganizationCodeName;
                InvoiceNumber = invoice.InvoiceNumber;
                Fund = invoice.Fund;
                Appn = invoice.Appn;
                SubObject = invoice.SubObject;

            }
        }

    }
}