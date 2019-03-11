//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[InvoiceMatchAmountType]
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
        public static InvoiceMatchAmountType GetInvoiceMatchAmountType(this IQueryable<InvoiceMatchAmountType> invoiceMatchAmountTypes, int invoiceMatchAmountTypeID)
        {
            var invoiceMatchAmountType = invoiceMatchAmountTypes.SingleOrDefault(x => x.InvoiceMatchAmountTypeID == invoiceMatchAmountTypeID);
            Check.RequireNotNullThrowNotFound(invoiceMatchAmountType, "InvoiceMatchAmountType", invoiceMatchAmountTypeID);
            return invoiceMatchAmountType;
        }

        // Delete using an IDList (WADNR style)
        public static void DeleteInvoiceMatchAmountType(this IQueryable<InvoiceMatchAmountType> invoiceMatchAmountTypes, List<int> invoiceMatchAmountTypeIDList)
        {
            if(invoiceMatchAmountTypeIDList.Any())
            {
                var invoiceMatchAmountTypesInSourceCollectionToDelete = invoiceMatchAmountTypes.Where(x => invoiceMatchAmountTypeIDList.Contains(x.InvoiceMatchAmountTypeID));
                foreach (var invoiceMatchAmountTypeToDelete in invoiceMatchAmountTypesInSourceCollectionToDelete.ToList())
                {
                    invoiceMatchAmountTypeToDelete.Delete(HttpRequestStorage.DatabaseEntities);
                }
            }
        }

        // Delete using an object list (WADNR style)
        public static void DeleteInvoiceMatchAmountType(this IQueryable<InvoiceMatchAmountType> invoiceMatchAmountTypes, ICollection<InvoiceMatchAmountType> invoiceMatchAmountTypesToDelete)
        {
            if(invoiceMatchAmountTypesToDelete.Any())
            {
                var invoiceMatchAmountTypeIDList = invoiceMatchAmountTypesToDelete.Select(x => x.InvoiceMatchAmountTypeID).ToList();
                var invoiceMatchAmountTypesToDeleteFromSourceList = invoiceMatchAmountTypes.Where(x => invoiceMatchAmountTypeIDList.Contains(x.InvoiceMatchAmountTypeID)).ToList();

                foreach (var invoiceMatchAmountTypeToDelete in invoiceMatchAmountTypesToDeleteFromSourceList)
                {
                    invoiceMatchAmountTypeToDelete.Delete(HttpRequestStorage.DatabaseEntities);
                }
            }
        }

        public static void DeleteInvoiceMatchAmountType(this IQueryable<InvoiceMatchAmountType> invoiceMatchAmountTypes, int invoiceMatchAmountTypeID)
        {
            DeleteInvoiceMatchAmountType(invoiceMatchAmountTypes, new List<int> { invoiceMatchAmountTypeID });
        }

        public static void DeleteInvoiceMatchAmountType(this IQueryable<InvoiceMatchAmountType> invoiceMatchAmountTypes, InvoiceMatchAmountType invoiceMatchAmountTypeToDelete)
        {
            DeleteInvoiceMatchAmountType(invoiceMatchAmountTypes, new List<InvoiceMatchAmountType> { invoiceMatchAmountTypeToDelete });
        }
    }
}