//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[InvoiceStatus]
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
        public static InvoiceStatus GetInvoiceStatus(this IQueryable<InvoiceStatus> invoiceStatuses, int invoiceStatusID)
        {
            var invoiceStatus = invoiceStatuses.SingleOrDefault(x => x.InvoiceStatusID == invoiceStatusID);
            Check.RequireNotNullThrowNotFound(invoiceStatus, "InvoiceStatus", invoiceStatusID);
            return invoiceStatus;
        }

        // Delete using an IDList (WADNR style)
        public static void DeleteInvoiceStatus(this IQueryable<InvoiceStatus> invoiceStatuses, List<int> invoiceStatusIDList)
        {
            if(invoiceStatusIDList.Any())
            {
                var invoiceStatusesInSourceCollectionToDelete = invoiceStatuses.Where(x => invoiceStatusIDList.Contains(x.InvoiceStatusID));
                foreach (var invoiceStatusToDelete in invoiceStatusesInSourceCollectionToDelete.ToList())
                {
                    invoiceStatusToDelete.Delete(HttpRequestStorage.DatabaseEntities);
                }
            }
        }

        // Delete using an object list (WADNR style)
        public static void DeleteInvoiceStatus(this IQueryable<InvoiceStatus> invoiceStatuses, ICollection<InvoiceStatus> invoiceStatusesToDelete)
        {
            if(invoiceStatusesToDelete.Any())
            {
                var invoiceStatusIDList = invoiceStatusesToDelete.Select(x => x.InvoiceStatusID).ToList();
                var invoiceStatusesToDeleteFromSourceList = invoiceStatuses.Where(x => invoiceStatusIDList.Contains(x.InvoiceStatusID)).ToList();

                foreach (var invoiceStatusToDelete in invoiceStatusesToDeleteFromSourceList)
                {
                    invoiceStatusToDelete.Delete(HttpRequestStorage.DatabaseEntities);
                }
            }
        }

        public static void DeleteInvoiceStatus(this IQueryable<InvoiceStatus> invoiceStatuses, int invoiceStatusID)
        {
            DeleteInvoiceStatus(invoiceStatuses, new List<int> { invoiceStatusID });
        }

        public static void DeleteInvoiceStatus(this IQueryable<InvoiceStatus> invoiceStatuses, InvoiceStatus invoiceStatusToDelete)
        {
            DeleteInvoiceStatus(invoiceStatuses, new List<InvoiceStatus> { invoiceStatusToDelete });
        }
    }
}