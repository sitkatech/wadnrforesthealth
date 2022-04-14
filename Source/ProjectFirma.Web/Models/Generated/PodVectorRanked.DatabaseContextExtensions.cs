//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[PodVectorRanked]
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
        public static PodVectorRanked GetPodVectorRanked(this IQueryable<PodVectorRanked> podVectorRankeds, int podVectorRankedID)
        {
            var podVectorRanked = podVectorRankeds.SingleOrDefault(x => x.PodVectorRankedID == podVectorRankedID);
            Check.RequireNotNullThrowNotFound(podVectorRanked, "PodVectorRanked", podVectorRankedID);
            return podVectorRanked;
        }

        // Delete using an IDList (WADNR style)
        public static void DeletePodVectorRanked(this IQueryable<PodVectorRanked> podVectorRankeds, List<int> podVectorRankedIDList)
        {
            if(podVectorRankedIDList.Any())
            {
                var podVectorRankedsInSourceCollectionToDelete = podVectorRankeds.Where(x => podVectorRankedIDList.Contains(x.PodVectorRankedID));
                foreach (var podVectorRankedToDelete in podVectorRankedsInSourceCollectionToDelete.ToList())
                {
                    podVectorRankedToDelete.Delete(HttpRequestStorage.DatabaseEntities);
                }
            }
        }

        // Delete using an object list (WADNR style)
        public static void DeletePodVectorRanked(this IQueryable<PodVectorRanked> podVectorRankeds, ICollection<PodVectorRanked> podVectorRankedsToDelete)
        {
            if(podVectorRankedsToDelete.Any())
            {
                var podVectorRankedIDList = podVectorRankedsToDelete.Select(x => x.PodVectorRankedID).ToList();
                var podVectorRankedsToDeleteFromSourceList = podVectorRankeds.Where(x => podVectorRankedIDList.Contains(x.PodVectorRankedID)).ToList();

                foreach (var podVectorRankedToDelete in podVectorRankedsToDeleteFromSourceList)
                {
                    podVectorRankedToDelete.Delete(HttpRequestStorage.DatabaseEntities);
                }
            }
        }

        public static void DeletePodVectorRanked(this IQueryable<PodVectorRanked> podVectorRankeds, int podVectorRankedID)
        {
            DeletePodVectorRanked(podVectorRankeds, new List<int> { podVectorRankedID });
        }

        public static void DeletePodVectorRanked(this IQueryable<PodVectorRanked> podVectorRankeds, PodVectorRanked podVectorRankedToDelete)
        {
            DeletePodVectorRanked(podVectorRankeds, new List<PodVectorRanked> { podVectorRankedToDelete });
        }
    }
}