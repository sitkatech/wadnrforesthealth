//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[PerformanceMeasureActual]
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
        public static PerformanceMeasureActual GetPerformanceMeasureActual(this IQueryable<PerformanceMeasureActual> performanceMeasureActuals, int performanceMeasureActualID)
        {
            var performanceMeasureActual = performanceMeasureActuals.SingleOrDefault(x => x.PerformanceMeasureActualID == performanceMeasureActualID);
            Check.RequireNotNullThrowNotFound(performanceMeasureActual, "PerformanceMeasureActual", performanceMeasureActualID);
            return performanceMeasureActual;
        }

        // Delete using an IDList (WADNR style)
        public static void DeletePerformanceMeasureActual(this IQueryable<PerformanceMeasureActual> performanceMeasureActuals, List<int> performanceMeasureActualIDList)
        {
            if(performanceMeasureActualIDList.Any())
            {
                var performanceMeasureActualsInSourceCollectionToDelete = performanceMeasureActuals.Where(x => performanceMeasureActualIDList.Contains(x.PerformanceMeasureActualID));
                foreach (var performanceMeasureActualToDelete in performanceMeasureActualsInSourceCollectionToDelete.ToList())
                {
                    performanceMeasureActualToDelete.Delete(HttpRequestStorage.DatabaseEntities);
                }
            }
        }

        // Delete using an object list (WADNR style)
        public static void DeletePerformanceMeasureActual(this IQueryable<PerformanceMeasureActual> performanceMeasureActuals, ICollection<PerformanceMeasureActual> performanceMeasureActualsToDelete)
        {
            if(performanceMeasureActualsToDelete.Any())
            {
                var performanceMeasureActualIDList = performanceMeasureActualsToDelete.Select(x => x.PerformanceMeasureActualID).ToList();
                var performanceMeasureActualsToDeleteFromSourceList = performanceMeasureActuals.Where(x => performanceMeasureActualIDList.Contains(x.PerformanceMeasureActualID)).ToList();

                foreach (var performanceMeasureActualToDelete in performanceMeasureActualsToDeleteFromSourceList)
                {
                    performanceMeasureActualToDelete.Delete(HttpRequestStorage.DatabaseEntities);
                }
            }
        }

        public static void DeletePerformanceMeasureActual(this IQueryable<PerformanceMeasureActual> performanceMeasureActuals, int performanceMeasureActualID)
        {
            DeletePerformanceMeasureActual(performanceMeasureActuals, new List<int> { performanceMeasureActualID });
        }

        public static void DeletePerformanceMeasureActual(this IQueryable<PerformanceMeasureActual> performanceMeasureActuals, PerformanceMeasureActual performanceMeasureActualToDelete)
        {
            DeletePerformanceMeasureActual(performanceMeasureActuals, new List<PerformanceMeasureActual> { performanceMeasureActualToDelete });
        }
    }
}