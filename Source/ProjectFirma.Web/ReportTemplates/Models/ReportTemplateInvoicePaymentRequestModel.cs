using System;
using System.Collections.Generic;
using System.Linq;
using ProjectFirma.Web.Models;

namespace ProjectFirma.Web.ReportTemplates.Models
{
    public class ReportTemplateInvoicePaymentRequestModel : ReportTemplateBaseModel
    {

        // Public properties
        public string VendorName { get; set; }
        public string VendorNumber { get; set; }
        public string VendorAddressLine1 { get; set; }
        public string VendorAddressLine2 { get; set; }
        public string VendorAddressLine3 { get; set; }
        public string VendorCity { get; set; }
        public string VendorState { get; set; }
        public string VendorZip { get; set; }

        public string VendorCityStateZip
        {
            get
            {
                if (!string.IsNullOrEmpty(VendorCity)
                    && !string.IsNullOrEmpty(VendorState)
                    && !string.IsNullOrEmpty(VendorZip))
                {
                    return $"{VendorCity}, {VendorState}  {VendorZip}";
                }

                if (!string.IsNullOrEmpty(VendorCity) && !string.IsNullOrEmpty(VendorState))
                    return $"{VendorCity}, {VendorState}";

                if (!string.IsNullOrEmpty(VendorCity))
                    return VendorCity;

                if (!string.IsNullOrEmpty(VendorState))
                    return VendorState;

                if (!string.IsNullOrEmpty(VendorZip))
                    return VendorZip;

                return string.Empty;
            }
        }

        public string VendorAddressDisplay => $"{(!string.IsNullOrEmpty(VendorAddressLine1) ? VendorAddressLine1 + Environment.NewLine : string.Empty)}" +
                                              $"{(!string.IsNullOrEmpty(VendorAddressLine2) ? VendorAddressLine2 + Environment.NewLine : string.Empty)}" +
                                              $"{(!string.IsNullOrEmpty(VendorAddressLine3) ? VendorAddressLine3 + Environment.NewLine : string.Empty)}" +
                                              $"{VendorCityStateZip}";

        public ReportTemplatePersonModel PreparedByPerson { get; set; }
        public string PurchaseAuthority { get; set; }
        public string DUNS { get; set; }
        public DateTime InvoicePaymentRequestDate { get; set; }
        public string InvoicePaymentRequestDateDisplay => InvoicePaymentRequestDate.ToShortDateString();
        public string Notes { get; set; }
        public List<ReportTemplateInvoiceModel> Invoices { get; set; }


        public ReportTemplateInvoicePaymentRequestModel(InvoicePaymentRequest invoicePaymentRequest)
        {
            if (invoicePaymentRequest!= null)
            {
                // Public properties
                VendorName = invoicePaymentRequest.Vendor?.VendorName;
                VendorNumber = invoicePaymentRequest.Vendor?.StatewideVendorNumberWithSuffix;
                VendorAddressLine1 = invoicePaymentRequest.Vendor?.VendorAddressLine1;
                VendorAddressLine2 = invoicePaymentRequest.Vendor?.VendorAddressLine2;
                VendorAddressLine3 = invoicePaymentRequest.Vendor?.VendorAddressLine3;
                VendorCity = invoicePaymentRequest.Vendor?.VendorCity;
                VendorState = invoicePaymentRequest.Vendor?.VendorState;
                VendorZip = invoicePaymentRequest.Vendor?.VendorZip;
                PreparedByPerson = invoicePaymentRequest.PreparedByPersonID.HasValue ? new ReportTemplatePersonModel(invoicePaymentRequest.PreparedByPerson) : null; 
                PurchaseAuthority = invoicePaymentRequest.PurchaseAuthority;
                DUNS = invoicePaymentRequest.Duns;
                InvoicePaymentRequestDate = invoicePaymentRequest.InvoicePaymentRequestDate;
                Notes = invoicePaymentRequest.Notes;
                Invoices = invoicePaymentRequest.Invoices.Select(x => new ReportTemplateInvoiceModel(x)).ToList();
            }
        }

        public List<ReportTemplateInvoiceModel> GetInvoices()
        {
            return Invoices;
        }

    }
}