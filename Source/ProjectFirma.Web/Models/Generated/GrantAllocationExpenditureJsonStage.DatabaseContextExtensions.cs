//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[GrantAllocationExpenditureJsonStage]
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
        public static GrantAllocationExpenditureJsonStage GetGrantAllocationExpenditureJsonStage(this IQueryable<GrantAllocationExpenditureJsonStage> grantAllocationExpenditureJsonStages, int grantAllocationExpenditureJsonStageID)
        {
            var grantAllocationExpenditureJsonStage = grantAllocationExpenditureJsonStages.SingleOrDefault(x => x.GrantAllocationExpenditureJsonStageID == grantAllocationExpenditureJsonStageID);
            Check.RequireNotNullThrowNotFound(grantAllocationExpenditureJsonStage, "GrantAllocationExpenditureJsonStage", grantAllocationExpenditureJsonStageID);
            return grantAllocationExpenditureJsonStage;
        }

        // Delete using an IDList (WADNR style)
        public static void DeleteGrantAllocationExpenditureJsonStage(this IQueryable<GrantAllocationExpenditureJsonStage> grantAllocationExpenditureJsonStages, List<int> grantAllocationExpenditureJsonStageIDList)
        {
            if(grantAllocationExpenditureJsonStageIDList.Any())
            {
                var grantAllocationExpenditureJsonStagesInSourceCollectionToDelete = grantAllocationExpenditureJsonStages.Where(x => grantAllocationExpenditureJsonStageIDList.Contains(x.GrantAllocationExpenditureJsonStageID));
                foreach (var grantAllocationExpenditureJsonStageToDelete in grantAllocationExpenditureJsonStagesInSourceCollectionToDelete.ToList())
                {
                    grantAllocationExpenditureJsonStageToDelete.Delete(HttpRequestStorage.DatabaseEntities);
                }
            }
        }

        // Delete using an object list (WADNR style)
        public static void DeleteGrantAllocationExpenditureJsonStage(this IQueryable<GrantAllocationExpenditureJsonStage> grantAllocationExpenditureJsonStages, ICollection<GrantAllocationExpenditureJsonStage> grantAllocationExpenditureJsonStagesToDelete)
        {
            if(grantAllocationExpenditureJsonStagesToDelete.Any())
            {
                var grantAllocationExpenditureJsonStageIDList = grantAllocationExpenditureJsonStagesToDelete.Select(x => x.GrantAllocationExpenditureJsonStageID).ToList();
                var grantAllocationExpenditureJsonStagesToDeleteFromSourceList = grantAllocationExpenditureJsonStages.Where(x => grantAllocationExpenditureJsonStageIDList.Contains(x.GrantAllocationExpenditureJsonStageID)).ToList();

                foreach (var grantAllocationExpenditureJsonStageToDelete in grantAllocationExpenditureJsonStagesToDeleteFromSourceList)
                {
                    grantAllocationExpenditureJsonStageToDelete.Delete(HttpRequestStorage.DatabaseEntities);
                }
            }
        }

        public static void DeleteGrantAllocationExpenditureJsonStage(this IQueryable<GrantAllocationExpenditureJsonStage> grantAllocationExpenditureJsonStages, int grantAllocationExpenditureJsonStageID)
        {
            DeleteGrantAllocationExpenditureJsonStage(grantAllocationExpenditureJsonStages, new List<int> { grantAllocationExpenditureJsonStageID });
        }

        public static void DeleteGrantAllocationExpenditureJsonStage(this IQueryable<GrantAllocationExpenditureJsonStage> grantAllocationExpenditureJsonStages, GrantAllocationExpenditureJsonStage grantAllocationExpenditureJsonStageToDelete)
        {
            DeleteGrantAllocationExpenditureJsonStage(grantAllocationExpenditureJsonStages, new List<GrantAllocationExpenditureJsonStage> { grantAllocationExpenditureJsonStageToDelete });
        }
    }
}