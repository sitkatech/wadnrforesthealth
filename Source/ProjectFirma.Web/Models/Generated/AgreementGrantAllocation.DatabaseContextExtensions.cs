//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[AgreementGrantAllocation]
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
        public static AgreementGrantAllocation GetAgreementGrantAllocation(this IQueryable<AgreementGrantAllocation> agreementGrantAllocations, int agreementGrantAllocationID)
        {
            var agreementGrantAllocation = agreementGrantAllocations.SingleOrDefault(x => x.AgreementGrantAllocationID == agreementGrantAllocationID);
            Check.RequireNotNullThrowNotFound(agreementGrantAllocation, "AgreementGrantAllocation", agreementGrantAllocationID);
            return agreementGrantAllocation;
        }

        // Delete using an IDList (WADNR style)
        public static void DeleteAgreementGrantAllocation(this IQueryable<AgreementGrantAllocation> agreementGrantAllocations, List<int> agreementGrantAllocationIDList)
        {
            if(agreementGrantAllocationIDList.Any())
            {
                var agreementGrantAllocationsInSourceCollectionToDelete = agreementGrantAllocations.Where(x => agreementGrantAllocationIDList.Contains(x.AgreementGrantAllocationID));
                foreach (var agreementGrantAllocationToDelete in agreementGrantAllocationsInSourceCollectionToDelete.ToList())
                {
                    agreementGrantAllocationToDelete.Delete(HttpRequestStorage.DatabaseEntities);
                }
            }
        }

        // Delete using an object list (WADNR style)
        public static void DeleteAgreementGrantAllocation(this IQueryable<AgreementGrantAllocation> agreementGrantAllocations, ICollection<AgreementGrantAllocation> agreementGrantAllocationsToDelete)
        {
            if(agreementGrantAllocationsToDelete.Any())
            {
                var agreementGrantAllocationIDList = agreementGrantAllocationsToDelete.Select(x => x.AgreementGrantAllocationID).ToList();
                var agreementGrantAllocationsToDeleteFromSourceList = agreementGrantAllocations.Where(x => agreementGrantAllocationIDList.Contains(x.AgreementGrantAllocationID)).ToList();

                foreach (var agreementGrantAllocationToDelete in agreementGrantAllocationsToDeleteFromSourceList)
                {
                    agreementGrantAllocationToDelete.Delete(HttpRequestStorage.DatabaseEntities);
                }
            }
        }

        public static void DeleteAgreementGrantAllocation(this IQueryable<AgreementGrantAllocation> agreementGrantAllocations, int agreementGrantAllocationID)
        {
            DeleteAgreementGrantAllocation(agreementGrantAllocations, new List<int> { agreementGrantAllocationID });
        }

        public static void DeleteAgreementGrantAllocation(this IQueryable<AgreementGrantAllocation> agreementGrantAllocations, AgreementGrantAllocation agreementGrantAllocationToDelete)
        {
            DeleteAgreementGrantAllocation(agreementGrantAllocations, new List<AgreementGrantAllocation> { agreementGrantAllocationToDelete });
        }
    }
}