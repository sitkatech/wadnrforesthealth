//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[InvoiceApprovalStatus]
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
        public static InvoiceApprovalStatus GetInvoiceApprovalStatus(this IQueryable<InvoiceApprovalStatus> invoiceApprovalStatuses, int invoiceApprovalStatusID)
        {
            var invoiceApprovalStatus = invoiceApprovalStatuses.SingleOrDefault(x => x.InvoiceApprovalStatusID == invoiceApprovalStatusID);
            Check.RequireNotNullThrowNotFound(invoiceApprovalStatus, "InvoiceApprovalStatus", invoiceApprovalStatusID);
            return invoiceApprovalStatus;
        }

        // Delete using an IDList (WADNR style)
        public static void DeleteInvoiceApprovalStatus(this IQueryable<InvoiceApprovalStatus> invoiceApprovalStatuses, List<int> invoiceApprovalStatusIDList)
        {
            if(invoiceApprovalStatusIDList.Any())
            {
                var invoiceApprovalStatusesInSourceCollectionToDelete = invoiceApprovalStatuses.Where(x => invoiceApprovalStatusIDList.Contains(x.InvoiceApprovalStatusID));
                foreach (var invoiceApprovalStatusToDelete in invoiceApprovalStatusesInSourceCollectionToDelete.ToList())
                {
                    invoiceApprovalStatusToDelete.Delete(HttpRequestStorage.DatabaseEntities);
                }
            }
        }

        // Delete using an object list (WADNR style)
        public static void DeleteInvoiceApprovalStatus(this IQueryable<InvoiceApprovalStatus> invoiceApprovalStatuses, ICollection<InvoiceApprovalStatus> invoiceApprovalStatusesToDelete)
        {
            if(invoiceApprovalStatusesToDelete.Any())
            {
                var invoiceApprovalStatusIDList = invoiceApprovalStatusesToDelete.Select(x => x.InvoiceApprovalStatusID).ToList();
                var invoiceApprovalStatusesToDeleteFromSourceList = invoiceApprovalStatuses.Where(x => invoiceApprovalStatusIDList.Contains(x.InvoiceApprovalStatusID)).ToList();

                foreach (var invoiceApprovalStatusToDelete in invoiceApprovalStatusesToDeleteFromSourceList)
                {
                    invoiceApprovalStatusToDelete.Delete(HttpRequestStorage.DatabaseEntities);
                }
            }
        }

        public static void DeleteInvoiceApprovalStatus(this IQueryable<InvoiceApprovalStatus> invoiceApprovalStatuses, int invoiceApprovalStatusID)
        {
            DeleteInvoiceApprovalStatus(invoiceApprovalStatuses, new List<int> { invoiceApprovalStatusID });
        }

        public static void DeleteInvoiceApprovalStatus(this IQueryable<InvoiceApprovalStatus> invoiceApprovalStatuses, InvoiceApprovalStatus invoiceApprovalStatusToDelete)
        {
            DeleteInvoiceApprovalStatus(invoiceApprovalStatuses, new List<InvoiceApprovalStatus> { invoiceApprovalStatusToDelete });
        }
    }
}