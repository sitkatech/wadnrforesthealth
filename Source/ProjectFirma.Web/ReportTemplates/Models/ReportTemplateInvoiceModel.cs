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
        public decimal? PaymentAmount { get; set; }
        public decimal? MatchAmount { get; set; }
        public string GrantNumber { get; set; }
        public string ProgramIndexCode { get; set; }
        public string ProjectCodeName { get; set; }
        public string OrganizationCodeName { get; set; }
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
                GrantNumber = invoice.Grant?.GrantNumber;
                ProgramIndexCode = invoice.ProgramIndex?.ProgramIndexCode;
                ProjectCodeName = invoice.ProjectCode?.ProjectCodeName;
                OrganizationCodeName = invoice.OrganizationCode?.OrganizationCodeName;
                InvoiceNumber = invoice.InvoiceNumber;
                Fund = invoice.Fund;
                Appn = invoice.Appn;
                SubObject = invoice.SubObject;
            }
        }

    }
}