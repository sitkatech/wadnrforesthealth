//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[InvoiceLineItem]
using System.Collections.Generic;
using System.Linq;
using Z.EntityFramework.Plus;
using LtInfo.Common.DesignByContract;
using LtInfo.Common.Models;
using ProjectFirma.Web.Common;

namespace ProjectFirma.Web.Models
{
    public static partial class DatabaseContextExtensions
    {
        public static InvoiceLineItem GetInvoiceLineItem(this IQueryable<InvoiceLineItem> invoiceLineItems, int invoiceLineItemID)
        {
            var invoiceLineItem = invoiceLineItems.SingleOrDefault(x => x.InvoiceLineItemID == invoiceLineItemID);
            Check.RequireNotNullThrowNotFound(invoiceLineItem, "InvoiceLineItem", invoiceLineItemID);
            return invoiceLineItem;
        }

        // Delete using an IDList (WADNR style)
        public static void DeleteInvoiceLineItem(this IQueryable<InvoiceLineItem> invoiceLineItems, List<int> invoiceLineItemIDList)
        {
            if(invoiceLineItemIDList.Any())
            {
                var invoiceLineItemsInSourceCollectionToDelete = invoiceLineItems.Where(x => invoiceLineItemIDList.Contains(x.InvoiceLineItemID));
                foreach (var invoiceLineItemToDelete in invoiceLineItemsInSourceCollectionToDelete.ToList())
                {
                    invoiceLineItemToDelete.Delete(HttpRequestStorage.DatabaseEntities);
                }
            }
        }

        // Delete using an object list (WADNR style)
        public static void DeleteInvoiceLineItem(this IQueryable<InvoiceLineItem> invoiceLineItems, ICollection<InvoiceLineItem> invoiceLineItemsToDelete)
        {
            if(invoiceLineItemsToDelete.Any())
            {
                var invoiceLineItemIDList = invoiceLineItemsToDelete.Select(x => x.InvoiceLineItemID).ToList();
                var invoiceLineItemsToDeleteFromSourceList = invoiceLineItems.Where(x => invoiceLineItemIDList.Contains(x.InvoiceLineItemID)).ToList();

                foreach (var invoiceLineItemToDelete in invoiceLineItemsToDeleteFromSourceList)
                {
                    invoiceLineItemToDelete.Delete(HttpRequestStorage.DatabaseEntities);
                }
            }
        }

        public static void DeleteInvoiceLineItem(this IQueryable<InvoiceLineItem> invoiceLineItems, int invoiceLineItemID)
        {
            DeleteInvoiceLineItem(invoiceLineItems, new List<int> { invoiceLineItemID });
        }

        public static void DeleteInvoiceLineItem(this IQueryable<InvoiceLineItem> invoiceLineItems, InvoiceLineItem invoiceLineItemToDelete)
        {
            DeleteInvoiceLineItem(invoiceLineItems, new List<InvoiceLineItem> { invoiceLineItemToDelete });
        }
    }
}