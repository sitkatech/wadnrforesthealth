//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[PerformanceMeasureActualUpdate]
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
        public static PerformanceMeasureActualUpdate GetPerformanceMeasureActualUpdate(this IQueryable<PerformanceMeasureActualUpdate> performanceMeasureActualUpdates, int performanceMeasureActualUpdateID)
        {
            var performanceMeasureActualUpdate = performanceMeasureActualUpdates.SingleOrDefault(x => x.PerformanceMeasureActualUpdateID == performanceMeasureActualUpdateID);
            Check.RequireNotNullThrowNotFound(performanceMeasureActualUpdate, "PerformanceMeasureActualUpdate", performanceMeasureActualUpdateID);
            return performanceMeasureActualUpdate;
        }

        // Delete using an IDList (WADNR style)
        public static void DeletePerformanceMeasureActualUpdate(this IQueryable<PerformanceMeasureActualUpdate> performanceMeasureActualUpdates, List<int> performanceMeasureActualUpdateIDList)
        {
            if(performanceMeasureActualUpdateIDList.Any())
            {
                var performanceMeasureActualUpdatesInSourceCollectionToDelete = performanceMeasureActualUpdates.Where(x => performanceMeasureActualUpdateIDList.Contains(x.PerformanceMeasureActualUpdateID));
                foreach (var performanceMeasureActualUpdateToDelete in performanceMeasureActualUpdatesInSourceCollectionToDelete.ToList())
                {
                    performanceMeasureActualUpdateToDelete.Delete(HttpRequestStorage.DatabaseEntities);
                }
            }
        }

        // Delete using an object list (WADNR style)
        public static void DeletePerformanceMeasureActualUpdate(this IQueryable<PerformanceMeasureActualUpdate> performanceMeasureActualUpdates, ICollection<PerformanceMeasureActualUpdate> performanceMeasureActualUpdatesToDelete)
        {
            if(performanceMeasureActualUpdatesToDelete.Any())
            {
                var performanceMeasureActualUpdateIDList = performanceMeasureActualUpdatesToDelete.Select(x => x.PerformanceMeasureActualUpdateID).ToList();
                var performanceMeasureActualUpdatesToDeleteFromSourceList = performanceMeasureActualUpdates.Where(x => performanceMeasureActualUpdateIDList.Contains(x.PerformanceMeasureActualUpdateID)).ToList();

                foreach (var performanceMeasureActualUpdateToDelete in performanceMeasureActualUpdatesToDeleteFromSourceList)
                {
                    performanceMeasureActualUpdateToDelete.Delete(HttpRequestStorage.DatabaseEntities);
                }
            }
        }

        public static void DeletePerformanceMeasureActualUpdate(this IQueryable<PerformanceMeasureActualUpdate> performanceMeasureActualUpdates, int performanceMeasureActualUpdateID)
        {
            DeletePerformanceMeasureActualUpdate(performanceMeasureActualUpdates, new List<int> { performanceMeasureActualUpdateID });
        }

        public static void DeletePerformanceMeasureActualUpdate(this IQueryable<PerformanceMeasureActualUpdate> performanceMeasureActualUpdates, PerformanceMeasureActualUpdate performanceMeasureActualUpdateToDelete)
        {
            DeletePerformanceMeasureActualUpdate(performanceMeasureActualUpdates, new List<PerformanceMeasureActualUpdate> { performanceMeasureActualUpdateToDelete });
        }
    }
}