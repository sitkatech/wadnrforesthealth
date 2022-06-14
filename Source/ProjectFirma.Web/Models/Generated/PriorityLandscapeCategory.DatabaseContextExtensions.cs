//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[PriorityLandscapeCategory]
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
        public static PriorityLandscapeCategory GetPriorityLandscapeCategory(this IQueryable<PriorityLandscapeCategory> priorityLandscapeCategories, int priorityLandscapeCategoryID)
        {
            var priorityLandscapeCategory = priorityLandscapeCategories.SingleOrDefault(x => x.PriorityLandscapeCategoryID == priorityLandscapeCategoryID);
            Check.RequireNotNullThrowNotFound(priorityLandscapeCategory, "PriorityLandscapeCategory", priorityLandscapeCategoryID);
            return priorityLandscapeCategory;
        }

        // Delete using an IDList (WADNR style)
        public static void DeletePriorityLandscapeCategory(this IQueryable<PriorityLandscapeCategory> priorityLandscapeCategories, List<int> priorityLandscapeCategoryIDList)
        {
            if(priorityLandscapeCategoryIDList.Any())
            {
                var priorityLandscapeCategoriesInSourceCollectionToDelete = priorityLandscapeCategories.Where(x => priorityLandscapeCategoryIDList.Contains(x.PriorityLandscapeCategoryID));
                foreach (var priorityLandscapeCategoryToDelete in priorityLandscapeCategoriesInSourceCollectionToDelete.ToList())
                {
                    priorityLandscapeCategoryToDelete.Delete(HttpRequestStorage.DatabaseEntities);
                }
            }
        }

        // Delete using an object list (WADNR style)
        public static void DeletePriorityLandscapeCategory(this IQueryable<PriorityLandscapeCategory> priorityLandscapeCategories, ICollection<PriorityLandscapeCategory> priorityLandscapeCategoriesToDelete)
        {
            if(priorityLandscapeCategoriesToDelete.Any())
            {
                var priorityLandscapeCategoryIDList = priorityLandscapeCategoriesToDelete.Select(x => x.PriorityLandscapeCategoryID).ToList();
                var priorityLandscapeCategoriesToDeleteFromSourceList = priorityLandscapeCategories.Where(x => priorityLandscapeCategoryIDList.Contains(x.PriorityLandscapeCategoryID)).ToList();

                foreach (var priorityLandscapeCategoryToDelete in priorityLandscapeCategoriesToDeleteFromSourceList)
                {
                    priorityLandscapeCategoryToDelete.Delete(HttpRequestStorage.DatabaseEntities);
                }
            }
        }

        public static void DeletePriorityLandscapeCategory(this IQueryable<PriorityLandscapeCategory> priorityLandscapeCategories, int priorityLandscapeCategoryID)
        {
            DeletePriorityLandscapeCategory(priorityLandscapeCategories, new List<int> { priorityLandscapeCategoryID });
        }

        public static void DeletePriorityLandscapeCategory(this IQueryable<PriorityLandscapeCategory> priorityLandscapeCategories, PriorityLandscapeCategory priorityLandscapeCategoryToDelete)
        {
            DeletePriorityLandscapeCategory(priorityLandscapeCategories, new List<PriorityLandscapeCategory> { priorityLandscapeCategoryToDelete });
        }
    }
}