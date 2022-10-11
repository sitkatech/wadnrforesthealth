using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace ProjectFirma.Web.Models.ApiJson
{
    /// <summary>
    /// This class is specifically meant to be used with the external WADNR APIs, and so should not be changed casually.
    /// </summary>
    [DebuggerDisplay("InvoiceID: {InvoiceID} - InvoiceIdentifyingName: {InvoiceIdentifyingName}")]
    public class InvoiceApiJson
    {
        // There is some selective denormalization here to assist WADNR's comprehension (PreparedByPersonName, InvoiceStatusName, etc.)
        public int InvoiceID { get; set; }
        public string InvoiceIdentifyingName { get; set; }
        public string RequestorName { get; set; }
        public DateTime InvoiceDate { get; set; }
        public string PurchaseAuthority { get; set; }
        public decimal? TotalPaymentAmount { get; set; }
        public int PreparedByPersonID { get; set; }
        public string PreparedByPersonName { get; set; }
        public int InvoiceApprovalStatusID { get; set; }
        public string InvoiceApprovalStatusName { get; set; }
        public string InvoiceApprovalStatusComment { get; set; }
        public bool PurchaseAuthorityIsLandownerCostShareAgreement { get; set; }
        public int InvoiceMatchAmountTypeID { get; set; }
        public decimal? MatchAmount { get; set; }
        public int InvoiceStatusID { get; set; }
        public string InvoiceStatusName { get; set; }

        // For use by model binder
        public InvoiceApiJson()
        {
        }

        public InvoiceApiJson(Invoice invoice)
        {
            InvoiceID = invoice.InvoiceID;
            InvoiceIdentifyingName = invoice.InvoiceIdentifyingName;
            //RequestorName = invoice.RequestorName;
            InvoiceDate = invoice.InvoiceDate;
            //PurchaseAuthority = invoice.PurchaseAuthority;
            TotalPaymentAmount = invoice.TotalPaymentAmount;
            //PreparedByPersonID = invoice.PreparedByPersonID;
            //PreparedByPersonName = invoice.PreparedByPerson.FullNameFirstLastAndOrgShortName;
            InvoiceApprovalStatusID = invoice.InvoiceApprovalStatusID;
            InvoiceApprovalStatusName = invoice.InvoiceApprovalStatus.InvoiceApprovalStatusName;
            InvoiceApprovalStatusComment = invoice.InvoiceApprovalStatusComment;
            //PurchaseAuthorityIsLandownerCostShareAgreement = invoice.PurchaseAuthorityIsLandownerCostShareAgreement;
            InvoiceMatchAmountTypeID = invoice.InvoiceMatchAmountTypeID;
            MatchAmount = invoice.MatchAmount;
            InvoiceStatusID = invoice.InvoiceStatusID;
            InvoiceStatusName = invoice.InvoiceStatus.InvoiceStatusName;
        }

    public static List<InvoiceApiJson> MakeInvoiceApiJsonsFromAgreements(List<Invoice> invoices, bool doAlphaSort = true)
        {
            var outgoingInvoices = invoices;
            if (doAlphaSort)
            {
                outgoingInvoices = outgoingInvoices.OrderBy(i => i.InvoiceIdentifyingName).ToList();
            }
            return outgoingInvoices.Select(inv => new InvoiceApiJson(inv)).ToList();
        }
    }
}