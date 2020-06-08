//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[GrantAllocationExpenditure]
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
        public static GrantAllocationExpenditure GetGrantAllocationExpenditure(this IQueryable<GrantAllocationExpenditure> grantAllocationExpenditures, int grantAllocationExpenditureID)
        {
            var grantAllocationExpenditure = grantAllocationExpenditures.SingleOrDefault(x => x.GrantAllocationExpenditureID == grantAllocationExpenditureID);
            Check.RequireNotNullThrowNotFound(grantAllocationExpenditure, "GrantAllocationExpenditure", grantAllocationExpenditureID);
            return grantAllocationExpenditure;
        }

        // Delete using an IDList (WADNR style)
        public static void DeleteGrantAllocationExpenditure(this IQueryable<GrantAllocationExpenditure> grantAllocationExpenditures, List<int> grantAllocationExpenditureIDList)
        {
            if(grantAllocationExpenditureIDList.Any())
            {
                var grantAllocationExpendituresInSourceCollectionToDelete = grantAllocationExpenditures.Where(x => grantAllocationExpenditureIDList.Contains(x.GrantAllocationExpenditureID));
                foreach (var grantAllocationExpenditureToDelete in grantAllocationExpendituresInSourceCollectionToDelete.ToList())
                {
                    grantAllocationExpenditureToDelete.Delete(HttpRequestStorage.DatabaseEntities);
                }
            }
        }

        // Delete using an object list (WADNR style)
        public static void DeleteGrantAllocationExpenditure(this IQueryable<GrantAllocationExpenditure> grantAllocationExpenditures, ICollection<GrantAllocationExpenditure> grantAllocationExpendituresToDelete)
        {
            if(grantAllocationExpendituresToDelete.Any())
            {
                var grantAllocationExpenditureIDList = grantAllocationExpendituresToDelete.Select(x => x.GrantAllocationExpenditureID).ToList();
                var grantAllocationExpendituresToDeleteFromSourceList = grantAllocationExpenditures.Where(x => grantAllocationExpenditureIDList.Contains(x.GrantAllocationExpenditureID)).ToList();

                foreach (var grantAllocationExpenditureToDelete in grantAllocationExpendituresToDeleteFromSourceList)
                {
                    grantAllocationExpenditureToDelete.Delete(HttpRequestStorage.DatabaseEntities);
                }
            }
        }

        public static void DeleteGrantAllocationExpenditure(this IQueryable<GrantAllocationExpenditure> grantAllocationExpenditures, int grantAllocationExpenditureID)
        {
            DeleteGrantAllocationExpenditure(grantAllocationExpenditures, new List<int> { grantAllocationExpenditureID });
        }

        public static void DeleteGrantAllocationExpenditure(this IQueryable<GrantAllocationExpenditure> grantAllocationExpenditures, GrantAllocationExpenditure grantAllocationExpenditureToDelete)
        {
            DeleteGrantAllocationExpenditure(grantAllocationExpenditures, new List<GrantAllocationExpenditure> { grantAllocationExpenditureToDelete });
        }
    }
}