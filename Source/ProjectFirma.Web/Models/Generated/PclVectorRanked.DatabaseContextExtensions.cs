//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[PclVectorRanked]
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
        public static PclVectorRanked GetPclVectorRanked(this IQueryable<PclVectorRanked> pclVectorRankeds, int pclVectorRankedID)
        {
            var pclVectorRanked = pclVectorRankeds.SingleOrDefault(x => x.PclVectorRankedID == pclVectorRankedID);
            Check.RequireNotNullThrowNotFound(pclVectorRanked, "PclVectorRanked", pclVectorRankedID);
            return pclVectorRanked;
        }

        // Delete using an IDList (WADNR style)
        public static void DeletePclVectorRanked(this IQueryable<PclVectorRanked> pclVectorRankeds, List<int> pclVectorRankedIDList)
        {
            if(pclVectorRankedIDList.Any())
            {
                var pclVectorRankedsInSourceCollectionToDelete = pclVectorRankeds.Where(x => pclVectorRankedIDList.Contains(x.PclVectorRankedID));
                foreach (var pclVectorRankedToDelete in pclVectorRankedsInSourceCollectionToDelete.ToList())
                {
                    pclVectorRankedToDelete.Delete(HttpRequestStorage.DatabaseEntities);
                }
            }
        }

        // Delete using an object list (WADNR style)
        public static void DeletePclVectorRanked(this IQueryable<PclVectorRanked> pclVectorRankeds, ICollection<PclVectorRanked> pclVectorRankedsToDelete)
        {
            if(pclVectorRankedsToDelete.Any())
            {
                var pclVectorRankedIDList = pclVectorRankedsToDelete.Select(x => x.PclVectorRankedID).ToList();
                var pclVectorRankedsToDeleteFromSourceList = pclVectorRankeds.Where(x => pclVectorRankedIDList.Contains(x.PclVectorRankedID)).ToList();

                foreach (var pclVectorRankedToDelete in pclVectorRankedsToDeleteFromSourceList)
                {
                    pclVectorRankedToDelete.Delete(HttpRequestStorage.DatabaseEntities);
                }
            }
        }

        public static void DeletePclVectorRanked(this IQueryable<PclVectorRanked> pclVectorRankeds, int pclVectorRankedID)
        {
            DeletePclVectorRanked(pclVectorRankeds, new List<int> { pclVectorRankedID });
        }

        public static void DeletePclVectorRanked(this IQueryable<PclVectorRanked> pclVectorRankeds, PclVectorRanked pclVectorRankedToDelete)
        {
            DeletePclVectorRanked(pclVectorRankeds, new List<PclVectorRanked> { pclVectorRankedToDelete });
        }
    }
}