//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[GrantAllocationAwardContractorInvoice]
using System.Collections.Generic;
using System.Linq;
using Z.EntityFramework.Plus;
using CodeFirstStoreFunctions;
using LtInfo.Common.DesignByContract;
using LtInfo.Common.Models;
using ProjectFirma.Web.Common;

namespace ProjectFirma.Web.Models
{
    public static partial class DatabaseContextExtensions
    {
        public static GrantAllocationAwardContractorInvoice GetGrantAllocationAwardContractorInvoice(this IQueryable<GrantAllocationAwardContractorInvoice> grantAllocationAwardContractorInvoices, int grantAllocationAwardContractorInvoiceID)
        {
            var grantAllocationAwardContractorInvoice = grantAllocationAwardContractorInvoices.SingleOrDefault(x => x.GrantAllocationAwardContractorInvoiceID == grantAllocationAwardContractorInvoiceID);
            Check.RequireNotNullThrowNotFound(grantAllocationAwardContractorInvoice, "GrantAllocationAwardContractorInvoice", grantAllocationAwardContractorInvoiceID);
            return grantAllocationAwardContractorInvoice;
        }

        // Delete using an IDList (WADNR style)
        public static void DeleteGrantAllocationAwardContractorInvoice(this IQueryable<GrantAllocationAwardContractorInvoice> grantAllocationAwardContractorInvoices, List<int> grantAllocationAwardContractorInvoiceIDList)
        {
            if(grantAllocationAwardContractorInvoiceIDList.Any())
            {
                var grantAllocationAwardContractorInvoicesInSourceCollectionToDelete = grantAllocationAwardContractorInvoices.Where(x => grantAllocationAwardContractorInvoiceIDList.Contains(x.GrantAllocationAwardContractorInvoiceID));
                foreach (var grantAllocationAwardContractorInvoiceToDelete in grantAllocationAwardContractorInvoicesInSourceCollectionToDelete.ToList())
                {
                    grantAllocationAwardContractorInvoiceToDelete.Delete(HttpRequestStorage.DatabaseEntities);
                }
            }
        }

        // Delete using an object list (WADNR style)
        public static void DeleteGrantAllocationAwardContractorInvoice(this IQueryable<GrantAllocationAwardContractorInvoice> grantAllocationAwardContractorInvoices, ICollection<GrantAllocationAwardContractorInvoice> grantAllocationAwardContractorInvoicesToDelete)
        {
            if(grantAllocationAwardContractorInvoicesToDelete.Any())
            {
                var grantAllocationAwardContractorInvoiceIDList = grantAllocationAwardContractorInvoicesToDelete.Select(x => x.GrantAllocationAwardContractorInvoiceID).ToList();
                var grantAllocationAwardContractorInvoicesToDeleteFromSourceList = grantAllocationAwardContractorInvoices.Where(x => grantAllocationAwardContractorInvoiceIDList.Contains(x.GrantAllocationAwardContractorInvoiceID)).ToList();

                foreach (var grantAllocationAwardContractorInvoiceToDelete in grantAllocationAwardContractorInvoicesToDeleteFromSourceList)
                {
                    grantAllocationAwardContractorInvoiceToDelete.Delete(HttpRequestStorage.DatabaseEntities);
                }
            }
        }

        public static void DeleteGrantAllocationAwardContractorInvoice(this IQueryable<GrantAllocationAwardContractorInvoice> grantAllocationAwardContractorInvoices, int grantAllocationAwardContractorInvoiceID)
        {
            DeleteGrantAllocationAwardContractorInvoice(grantAllocationAwardContractorInvoices, new List<int> { grantAllocationAwardContractorInvoiceID });
        }

        public static void DeleteGrantAllocationAwardContractorInvoice(this IQueryable<GrantAllocationAwardContractorInvoice> grantAllocationAwardContractorInvoices, GrantAllocationAwardContractorInvoice grantAllocationAwardContractorInvoiceToDelete)
        {
            DeleteGrantAllocationAwardContractorInvoice(grantAllocationAwardContractorInvoices, new List<GrantAllocationAwardContractorInvoice> { grantAllocationAwardContractorInvoiceToDelete });
        }
    }
}