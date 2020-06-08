//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[PerformanceMeasureSubcategory]
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
        public static PerformanceMeasureSubcategory GetPerformanceMeasureSubcategory(this IQueryable<PerformanceMeasureSubcategory> performanceMeasureSubcategories, int performanceMeasureSubcategoryID)
        {
            var performanceMeasureSubcategory = performanceMeasureSubcategories.SingleOrDefault(x => x.PerformanceMeasureSubcategoryID == performanceMeasureSubcategoryID);
            Check.RequireNotNullThrowNotFound(performanceMeasureSubcategory, "PerformanceMeasureSubcategory", performanceMeasureSubcategoryID);
            return performanceMeasureSubcategory;
        }

        // Delete using an IDList (WADNR style)
        public static void DeletePerformanceMeasureSubcategory(this IQueryable<PerformanceMeasureSubcategory> performanceMeasureSubcategories, List<int> performanceMeasureSubcategoryIDList)
        {
            if(performanceMeasureSubcategoryIDList.Any())
            {
                var performanceMeasureSubcategoriesInSourceCollectionToDelete = performanceMeasureSubcategories.Where(x => performanceMeasureSubcategoryIDList.Contains(x.PerformanceMeasureSubcategoryID));
                foreach (var performanceMeasureSubcategoryToDelete in performanceMeasureSubcategoriesInSourceCollectionToDelete.ToList())
                {
                    performanceMeasureSubcategoryToDelete.Delete(HttpRequestStorage.DatabaseEntities);
                }
            }
        }

        // Delete using an object list (WADNR style)
        public static void DeletePerformanceMeasureSubcategory(this IQueryable<PerformanceMeasureSubcategory> performanceMeasureSubcategories, ICollection<PerformanceMeasureSubcategory> performanceMeasureSubcategoriesToDelete)
        {
            if(performanceMeasureSubcategoriesToDelete.Any())
            {
                var performanceMeasureSubcategoryIDList = performanceMeasureSubcategoriesToDelete.Select(x => x.PerformanceMeasureSubcategoryID).ToList();
                var performanceMeasureSubcategoriesToDeleteFromSourceList = performanceMeasureSubcategories.Where(x => performanceMeasureSubcategoryIDList.Contains(x.PerformanceMeasureSubcategoryID)).ToList();

                foreach (var performanceMeasureSubcategoryToDelete in performanceMeasureSubcategoriesToDeleteFromSourceList)
                {
                    performanceMeasureSubcategoryToDelete.Delete(HttpRequestStorage.DatabaseEntities);
                }
            }
        }

        public static void DeletePerformanceMeasureSubcategory(this IQueryable<PerformanceMeasureSubcategory> performanceMeasureSubcategories, int performanceMeasureSubcategoryID)
        {
            DeletePerformanceMeasureSubcategory(performanceMeasureSubcategories, new List<int> { performanceMeasureSubcategoryID });
        }

        public static void DeletePerformanceMeasureSubcategory(this IQueryable<PerformanceMeasureSubcategory> performanceMeasureSubcategories, PerformanceMeasureSubcategory performanceMeasureSubcategoryToDelete)
        {
            DeletePerformanceMeasureSubcategory(performanceMeasureSubcategories, new List<PerformanceMeasureSubcategory> { performanceMeasureSubcategoryToDelete });
        }
    }
}