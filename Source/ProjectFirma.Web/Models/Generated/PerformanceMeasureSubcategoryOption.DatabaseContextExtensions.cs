//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[PerformanceMeasureSubcategoryOption]
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
        public static PerformanceMeasureSubcategoryOption GetPerformanceMeasureSubcategoryOption(this IQueryable<PerformanceMeasureSubcategoryOption> performanceMeasureSubcategoryOptions, int performanceMeasureSubcategoryOptionID)
        {
            var performanceMeasureSubcategoryOption = performanceMeasureSubcategoryOptions.SingleOrDefault(x => x.PerformanceMeasureSubcategoryOptionID == performanceMeasureSubcategoryOptionID);
            Check.RequireNotNullThrowNotFound(performanceMeasureSubcategoryOption, "PerformanceMeasureSubcategoryOption", performanceMeasureSubcategoryOptionID);
            return performanceMeasureSubcategoryOption;
        }

        // Delete using an IDList (WADNR style)
        public static void DeletePerformanceMeasureSubcategoryOption(this IQueryable<PerformanceMeasureSubcategoryOption> performanceMeasureSubcategoryOptions, List<int> performanceMeasureSubcategoryOptionIDList)
        {
            if(performanceMeasureSubcategoryOptionIDList.Any())
            {
                var performanceMeasureSubcategoryOptionsInSourceCollectionToDelete = performanceMeasureSubcategoryOptions.Where(x => performanceMeasureSubcategoryOptionIDList.Contains(x.PerformanceMeasureSubcategoryOptionID));
                foreach (var performanceMeasureSubcategoryOptionToDelete in performanceMeasureSubcategoryOptionsInSourceCollectionToDelete.ToList())
                {
                    performanceMeasureSubcategoryOptionToDelete.Delete(HttpRequestStorage.DatabaseEntities);
                }
            }
        }

        // Delete using an object list (WADNR style)
        public static void DeletePerformanceMeasureSubcategoryOption(this IQueryable<PerformanceMeasureSubcategoryOption> performanceMeasureSubcategoryOptions, ICollection<PerformanceMeasureSubcategoryOption> performanceMeasureSubcategoryOptionsToDelete)
        {
            if(performanceMeasureSubcategoryOptionsToDelete.Any())
            {
                var performanceMeasureSubcategoryOptionIDList = performanceMeasureSubcategoryOptionsToDelete.Select(x => x.PerformanceMeasureSubcategoryOptionID).ToList();
                var performanceMeasureSubcategoryOptionsToDeleteFromSourceList = performanceMeasureSubcategoryOptions.Where(x => performanceMeasureSubcategoryOptionIDList.Contains(x.PerformanceMeasureSubcategoryOptionID)).ToList();

                foreach (var performanceMeasureSubcategoryOptionToDelete in performanceMeasureSubcategoryOptionsToDeleteFromSourceList)
                {
                    performanceMeasureSubcategoryOptionToDelete.Delete(HttpRequestStorage.DatabaseEntities);
                }
            }
        }

        public static void DeletePerformanceMeasureSubcategoryOption(this IQueryable<PerformanceMeasureSubcategoryOption> performanceMeasureSubcategoryOptions, int performanceMeasureSubcategoryOptionID)
        {
            DeletePerformanceMeasureSubcategoryOption(performanceMeasureSubcategoryOptions, new List<int> { performanceMeasureSubcategoryOptionID });
        }

        public static void DeletePerformanceMeasureSubcategoryOption(this IQueryable<PerformanceMeasureSubcategoryOption> performanceMeasureSubcategoryOptions, PerformanceMeasureSubcategoryOption performanceMeasureSubcategoryOptionToDelete)
        {
            DeletePerformanceMeasureSubcategoryOption(performanceMeasureSubcategoryOptions, new List<PerformanceMeasureSubcategoryOption> { performanceMeasureSubcategoryOptionToDelete });
        }
    }
}