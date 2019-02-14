//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[PriorityArea]
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
        public static PriorityArea GetPriorityArea(this IQueryable<PriorityArea> priorityAreas, int priorityAreaID)
        {
            var priorityArea = priorityAreas.SingleOrDefault(x => x.PriorityAreaID == priorityAreaID);
            Check.RequireNotNullThrowNotFound(priorityArea, "PriorityArea", priorityAreaID);
            return priorityArea;
        }

        // Delete using an IDList (WADNR style)
        public static void DeletePriorityArea(this IQueryable<PriorityArea> priorityAreas, List<int> priorityAreaIDList)
        {
            if(priorityAreaIDList.Any())
            {
                var priorityAreasInSourceCollectionToDelete = priorityAreas.Where(x => priorityAreaIDList.Contains(x.PriorityAreaID));
                foreach (var priorityAreaToDelete in priorityAreasInSourceCollectionToDelete.ToList())
                {
                    priorityAreaToDelete.Delete(HttpRequestStorage.DatabaseEntities);
                }
            }
        }

        // Delete using an object list (WADNR style)
        public static void DeletePriorityArea(this IQueryable<PriorityArea> priorityAreas, ICollection<PriorityArea> priorityAreasToDelete)
        {
            if(priorityAreasToDelete.Any())
            {
                var priorityAreaIDList = priorityAreasToDelete.Select(x => x.PriorityAreaID).ToList();
                var priorityAreasToDeleteFromSourceList = priorityAreas.Where(x => priorityAreaIDList.Contains(x.PriorityAreaID)).ToList();

                foreach (var priorityAreaToDelete in priorityAreasToDeleteFromSourceList)
                {
                    priorityAreaToDelete.Delete(HttpRequestStorage.DatabaseEntities);
                }
            }
        }

        public static void DeletePriorityArea(this IQueryable<PriorityArea> priorityAreas, int priorityAreaID)
        {
            DeletePriorityArea(priorityAreas, new List<int> { priorityAreaID });
        }

        public static void DeletePriorityArea(this IQueryable<PriorityArea> priorityAreas, PriorityArea priorityAreaToDelete)
        {
            DeletePriorityArea(priorityAreas, new List<PriorityArea> { priorityAreaToDelete });
        }
    }
}