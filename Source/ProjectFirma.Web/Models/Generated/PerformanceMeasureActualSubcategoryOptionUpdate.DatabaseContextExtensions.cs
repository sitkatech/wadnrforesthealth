//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[PerformanceMeasureActualSubcategoryOptionUpdate]
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
        public static PerformanceMeasureActualSubcategoryOptionUpdate GetPerformanceMeasureActualSubcategoryOptionUpdate(this IQueryable<PerformanceMeasureActualSubcategoryOptionUpdate> performanceMeasureActualSubcategoryOptionUpdates, int performanceMeasureActualSubcategoryOptionUpdateID)
        {
            var performanceMeasureActualSubcategoryOptionUpdate = performanceMeasureActualSubcategoryOptionUpdates.SingleOrDefault(x => x.PerformanceMeasureActualSubcategoryOptionUpdateID == performanceMeasureActualSubcategoryOptionUpdateID);
            Check.RequireNotNullThrowNotFound(performanceMeasureActualSubcategoryOptionUpdate, "PerformanceMeasureActualSubcategoryOptionUpdate", performanceMeasureActualSubcategoryOptionUpdateID);
            return performanceMeasureActualSubcategoryOptionUpdate;
        }

        // Delete using an IDList (WADNR style)
        public static void DeletePerformanceMeasureActualSubcategoryOptionUpdate(this IQueryable<PerformanceMeasureActualSubcategoryOptionUpdate> performanceMeasureActualSubcategoryOptionUpdates, List<int> performanceMeasureActualSubcategoryOptionUpdateIDList)
        {
            if(performanceMeasureActualSubcategoryOptionUpdateIDList.Any())
            {
                var performanceMeasureActualSubcategoryOptionUpdatesInSourceCollectionToDelete = performanceMeasureActualSubcategoryOptionUpdates.Where(x => performanceMeasureActualSubcategoryOptionUpdateIDList.Contains(x.PerformanceMeasureActualSubcategoryOptionUpdateID));
                foreach (var performanceMeasureActualSubcategoryOptionUpdateToDelete in performanceMeasureActualSubcategoryOptionUpdatesInSourceCollectionToDelete.ToList())
                {
                    performanceMeasureActualSubcategoryOptionUpdateToDelete.Delete(HttpRequestStorage.DatabaseEntities);
                }
            }
        }

        // Delete using an object list (WADNR style)
        public static void DeletePerformanceMeasureActualSubcategoryOptionUpdate(this IQueryable<PerformanceMeasureActualSubcategoryOptionUpdate> performanceMeasureActualSubcategoryOptionUpdates, ICollection<PerformanceMeasureActualSubcategoryOptionUpdate> performanceMeasureActualSubcategoryOptionUpdatesToDelete)
        {
            if(performanceMeasureActualSubcategoryOptionUpdatesToDelete.Any())
            {
                var performanceMeasureActualSubcategoryOptionUpdateIDList = performanceMeasureActualSubcategoryOptionUpdatesToDelete.Select(x => x.PerformanceMeasureActualSubcategoryOptionUpdateID).ToList();
                var performanceMeasureActualSubcategoryOptionUpdatesToDeleteFromSourceList = performanceMeasureActualSubcategoryOptionUpdates.Where(x => performanceMeasureActualSubcategoryOptionUpdateIDList.Contains(x.PerformanceMeasureActualSubcategoryOptionUpdateID)).ToList();

                foreach (var performanceMeasureActualSubcategoryOptionUpdateToDelete in performanceMeasureActualSubcategoryOptionUpdatesToDeleteFromSourceList)
                {
                    performanceMeasureActualSubcategoryOptionUpdateToDelete.Delete(HttpRequestStorage.DatabaseEntities);
                }
            }
        }

        public static void DeletePerformanceMeasureActualSubcategoryOptionUpdate(this IQueryable<PerformanceMeasureActualSubcategoryOptionUpdate> performanceMeasureActualSubcategoryOptionUpdates, int performanceMeasureActualSubcategoryOptionUpdateID)
        {
            DeletePerformanceMeasureActualSubcategoryOptionUpdate(performanceMeasureActualSubcategoryOptionUpdates, new List<int> { performanceMeasureActualSubcategoryOptionUpdateID });
        }

        public static void DeletePerformanceMeasureActualSubcategoryOptionUpdate(this IQueryable<PerformanceMeasureActualSubcategoryOptionUpdate> performanceMeasureActualSubcategoryOptionUpdates, PerformanceMeasureActualSubcategoryOptionUpdate performanceMeasureActualSubcategoryOptionUpdateToDelete)
        {
            DeletePerformanceMeasureActualSubcategoryOptionUpdate(performanceMeasureActualSubcategoryOptionUpdates, new List<PerformanceMeasureActualSubcategoryOptionUpdate> { performanceMeasureActualSubcategoryOptionUpdateToDelete });
        }
    }
}