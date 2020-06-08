//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[PerformanceMeasure]
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
        public static PerformanceMeasure GetPerformanceMeasure(this IQueryable<PerformanceMeasure> performanceMeasures, int performanceMeasureID)
        {
            var performanceMeasure = performanceMeasures.SingleOrDefault(x => x.PerformanceMeasureID == performanceMeasureID);
            Check.RequireNotNullThrowNotFound(performanceMeasure, "PerformanceMeasure", performanceMeasureID);
            return performanceMeasure;
        }

        // Delete using an IDList (WADNR style)
        public static void DeletePerformanceMeasure(this IQueryable<PerformanceMeasure> performanceMeasures, List<int> performanceMeasureIDList)
        {
            if(performanceMeasureIDList.Any())
            {
                var performanceMeasuresInSourceCollectionToDelete = performanceMeasures.Where(x => performanceMeasureIDList.Contains(x.PerformanceMeasureID));
                foreach (var performanceMeasureToDelete in performanceMeasuresInSourceCollectionToDelete.ToList())
                {
                    performanceMeasureToDelete.Delete(HttpRequestStorage.DatabaseEntities);
                }
            }
        }

        // Delete using an object list (WADNR style)
        public static void DeletePerformanceMeasure(this IQueryable<PerformanceMeasure> performanceMeasures, ICollection<PerformanceMeasure> performanceMeasuresToDelete)
        {
            if(performanceMeasuresToDelete.Any())
            {
                var performanceMeasureIDList = performanceMeasuresToDelete.Select(x => x.PerformanceMeasureID).ToList();
                var performanceMeasuresToDeleteFromSourceList = performanceMeasures.Where(x => performanceMeasureIDList.Contains(x.PerformanceMeasureID)).ToList();

                foreach (var performanceMeasureToDelete in performanceMeasuresToDeleteFromSourceList)
                {
                    performanceMeasureToDelete.Delete(HttpRequestStorage.DatabaseEntities);
                }
            }
        }

        public static void DeletePerformanceMeasure(this IQueryable<PerformanceMeasure> performanceMeasures, int performanceMeasureID)
        {
            DeletePerformanceMeasure(performanceMeasures, new List<int> { performanceMeasureID });
        }

        public static void DeletePerformanceMeasure(this IQueryable<PerformanceMeasure> performanceMeasures, PerformanceMeasure performanceMeasureToDelete)
        {
            DeletePerformanceMeasure(performanceMeasures, new List<PerformanceMeasure> { performanceMeasureToDelete });
        }
    }
}