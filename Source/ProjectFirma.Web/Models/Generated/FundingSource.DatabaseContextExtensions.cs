//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[FundingSource]
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
        public static FundingSource GetFundingSource(this IQueryable<FundingSource> fundingSources, int fundingSourceID)
        {
            var fundingSource = fundingSources.SingleOrDefault(x => x.FundingSourceID == fundingSourceID);
            Check.RequireNotNullThrowNotFound(fundingSource, "FundingSource", fundingSourceID);
            return fundingSource;
        }

        // Delete using an IDList (WADNR style)
        public static void DeleteFundingSource(this IQueryable<FundingSource> fundingSources, List<int> fundingSourceIDList)
        {
            if(fundingSourceIDList.Any())
            {
                var fundingSourcesInSourceCollectionToDelete = fundingSources.Where(x => fundingSourceIDList.Contains(x.FundingSourceID));
                foreach (var fundingSourceToDelete in fundingSourcesInSourceCollectionToDelete.ToList())
                {
                    fundingSourceToDelete.Delete(HttpRequestStorage.DatabaseEntities);
                }
            }
        }

        // Delete using an object list (WADNR style)
        public static void DeleteFundingSource(this IQueryable<FundingSource> fundingSources, ICollection<FundingSource> fundingSourcesToDelete)
        {
            if(fundingSourcesToDelete.Any())
            {
                var fundingSourceIDList = fundingSourcesToDelete.Select(x => x.FundingSourceID).ToList();
                var fundingSourcesToDeleteFromSourceList = fundingSources.Where(x => fundingSourceIDList.Contains(x.FundingSourceID)).ToList();

                foreach (var fundingSourceToDelete in fundingSourcesToDeleteFromSourceList)
                {
                    fundingSourceToDelete.Delete(HttpRequestStorage.DatabaseEntities);
                }
            }
        }

        public static void DeleteFundingSource(this IQueryable<FundingSource> fundingSources, int fundingSourceID)
        {
            DeleteFundingSource(fundingSources, new List<int> { fundingSourceID });
        }

        public static void DeleteFundingSource(this IQueryable<FundingSource> fundingSources, FundingSource fundingSourceToDelete)
        {
            DeleteFundingSource(fundingSources, new List<FundingSource> { fundingSourceToDelete });
        }
    }
}