using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace ProjectFirma.Web.Models.ApiJson
{
    /// <summary>
    /// This class is specifically meant to be used with the external WADNR APIs, and so should not be changed casually.
    /// </summary>
    [DebuggerDisplay("InvoiceLineItemID: {InvoiceLineItemID} - InvoiceID: {InvoiceID} - GrantAllocationID: {GrantAllocationID}")]
    public class InvoiceLineItemApiJson
    {
        // There is some selective denormalization here to assist WADNR's comprehension (PreparedByPersonName, InvoiceStatusName, etc.)
        public int InvoiceLineItemID { get; set; }
        public int InvoiceID { get; set; }
        public int GrantAllocationID { get; set; }
        public int CostTypeID { get; set; }
        public string CostTypeName { get; set; }
        public decimal InvoiceLineItemAmount { get; set; }
        public string InvoiceLineItemNote { get; set; }


        // For use by model binder
        public InvoiceLineItemApiJson()
        {
        }

        public InvoiceLineItemApiJson(InvoiceLineItem invoiceLineItem)
        {
            InvoiceLineItemID = invoiceLineItem.InvoiceLineItemID;
            InvoiceID = invoiceLineItem.InvoiceID;
            GrantAllocationID = invoiceLineItem.GrantAllocationID;
            CostTypeID = invoiceLineItem.CostTypeID;
            CostTypeName = invoiceLineItem.CostType.CostTypeName;
            InvoiceLineItemAmount = invoiceLineItem.InvoiceLineItemAmount;
            InvoiceLineItemNote = invoiceLineItem.InvoiceLineItemNote;
        }

        public static List<InvoiceLineItemApiJson> MakeInvoiceLineItemApiJsonsFromAgreements(List<InvoiceLineItem> invoiceLineItems)
        {
            return invoiceLineItems.Select(inv => new InvoiceLineItemApiJson(inv)).ToList();
        }
    }
}