//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[GrantAllocationAwardContractorInvoiceType]
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
        public static GrantAllocationAwardContractorInvoiceType GetGrantAllocationAwardContractorInvoiceType(this IQueryable<GrantAllocationAwardContractorInvoiceType> grantAllocationAwardContractorInvoiceTypes, int grantAllocationAwardContractorInvoiceTypeID)
        {
            var grantAllocationAwardContractorInvoiceType = grantAllocationAwardContractorInvoiceTypes.SingleOrDefault(x => x.GrantAllocationAwardContractorInvoiceTypeID == grantAllocationAwardContractorInvoiceTypeID);
            Check.RequireNotNullThrowNotFound(grantAllocationAwardContractorInvoiceType, "GrantAllocationAwardContractorInvoiceType", grantAllocationAwardContractorInvoiceTypeID);
            return grantAllocationAwardContractorInvoiceType;
        }

        // Delete using an IDList (WADNR style)
        public static void DeleteGrantAllocationAwardContractorInvoiceType(this IQueryable<GrantAllocationAwardContractorInvoiceType> grantAllocationAwardContractorInvoiceTypes, List<int> grantAllocationAwardContractorInvoiceTypeIDList)
        {
            if(grantAllocationAwardContractorInvoiceTypeIDList.Any())
            {
                var grantAllocationAwardContractorInvoiceTypesInSourceCollectionToDelete = grantAllocationAwardContractorInvoiceTypes.Where(x => grantAllocationAwardContractorInvoiceTypeIDList.Contains(x.GrantAllocationAwardContractorInvoiceTypeID));
                foreach (var grantAllocationAwardContractorInvoiceTypeToDelete in grantAllocationAwardContractorInvoiceTypesInSourceCollectionToDelete.ToList())
                {
                    grantAllocationAwardContractorInvoiceTypeToDelete.Delete(HttpRequestStorage.DatabaseEntities);
                }
            }
        }

        // Delete using an object list (WADNR style)
        public static void DeleteGrantAllocationAwardContractorInvoiceType(this IQueryable<GrantAllocationAwardContractorInvoiceType> grantAllocationAwardContractorInvoiceTypes, ICollection<GrantAllocationAwardContractorInvoiceType> grantAllocationAwardContractorInvoiceTypesToDelete)
        {
            if(grantAllocationAwardContractorInvoiceTypesToDelete.Any())
            {
                var grantAllocationAwardContractorInvoiceTypeIDList = grantAllocationAwardContractorInvoiceTypesToDelete.Select(x => x.GrantAllocationAwardContractorInvoiceTypeID).ToList();
                var grantAllocationAwardContractorInvoiceTypesToDeleteFromSourceList = grantAllocationAwardContractorInvoiceTypes.Where(x => grantAllocationAwardContractorInvoiceTypeIDList.Contains(x.GrantAllocationAwardContractorInvoiceTypeID)).ToList();

                foreach (var grantAllocationAwardContractorInvoiceTypeToDelete in grantAllocationAwardContractorInvoiceTypesToDeleteFromSourceList)
                {
                    grantAllocationAwardContractorInvoiceTypeToDelete.Delete(HttpRequestStorage.DatabaseEntities);
                }
            }
        }

        public static void DeleteGrantAllocationAwardContractorInvoiceType(this IQueryable<GrantAllocationAwardContractorInvoiceType> grantAllocationAwardContractorInvoiceTypes, int grantAllocationAwardContractorInvoiceTypeID)
        {
            DeleteGrantAllocationAwardContractorInvoiceType(grantAllocationAwardContractorInvoiceTypes, new List<int> { grantAllocationAwardContractorInvoiceTypeID });
        }

        public static void DeleteGrantAllocationAwardContractorInvoiceType(this IQueryable<GrantAllocationAwardContractorInvoiceType> grantAllocationAwardContractorInvoiceTypes, GrantAllocationAwardContractorInvoiceType grantAllocationAwardContractorInvoiceTypeToDelete)
        {
            DeleteGrantAllocationAwardContractorInvoiceType(grantAllocationAwardContractorInvoiceTypes, new List<GrantAllocationAwardContractorInvoiceType> { grantAllocationAwardContractorInvoiceTypeToDelete });
        }
    }
}