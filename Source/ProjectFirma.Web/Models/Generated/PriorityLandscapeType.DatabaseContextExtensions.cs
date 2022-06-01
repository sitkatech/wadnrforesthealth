//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[PriorityLandscapeType]
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
        public static PriorityLandscapeType GetPriorityLandscapeType(this IQueryable<PriorityLandscapeType> priorityLandscapeTypes, int priorityLandscapeTypeID)
        {
            var priorityLandscapeType = priorityLandscapeTypes.SingleOrDefault(x => x.PriorityLandscapeTypeID == priorityLandscapeTypeID);
            Check.RequireNotNullThrowNotFound(priorityLandscapeType, "PriorityLandscapeType", priorityLandscapeTypeID);
            return priorityLandscapeType;
        }

        // Delete using an IDList (WADNR style)
        public static void DeletePriorityLandscapeType(this IQueryable<PriorityLandscapeType> priorityLandscapeTypes, List<int> priorityLandscapeTypeIDList)
        {
            if(priorityLandscapeTypeIDList.Any())
            {
                var priorityLandscapeTypesInSourceCollectionToDelete = priorityLandscapeTypes.Where(x => priorityLandscapeTypeIDList.Contains(x.PriorityLandscapeTypeID));
                foreach (var priorityLandscapeTypeToDelete in priorityLandscapeTypesInSourceCollectionToDelete.ToList())
                {
                    priorityLandscapeTypeToDelete.Delete(HttpRequestStorage.DatabaseEntities);
                }
            }
        }

        // Delete using an object list (WADNR style)
        public static void DeletePriorityLandscapeType(this IQueryable<PriorityLandscapeType> priorityLandscapeTypes, ICollection<PriorityLandscapeType> priorityLandscapeTypesToDelete)
        {
            if(priorityLandscapeTypesToDelete.Any())
            {
                var priorityLandscapeTypeIDList = priorityLandscapeTypesToDelete.Select(x => x.PriorityLandscapeTypeID).ToList();
                var priorityLandscapeTypesToDeleteFromSourceList = priorityLandscapeTypes.Where(x => priorityLandscapeTypeIDList.Contains(x.PriorityLandscapeTypeID)).ToList();

                foreach (var priorityLandscapeTypeToDelete in priorityLandscapeTypesToDeleteFromSourceList)
                {
                    priorityLandscapeTypeToDelete.Delete(HttpRequestStorage.DatabaseEntities);
                }
            }
        }

        public static void DeletePriorityLandscapeType(this IQueryable<PriorityLandscapeType> priorityLandscapeTypes, int priorityLandscapeTypeID)
        {
            DeletePriorityLandscapeType(priorityLandscapeTypes, new List<int> { priorityLandscapeTypeID });
        }

        public static void DeletePriorityLandscapeType(this IQueryable<PriorityLandscapeType> priorityLandscapeTypes, PriorityLandscapeType priorityLandscapeTypeToDelete)
        {
            DeletePriorityLandscapeType(priorityLandscapeTypes, new List<PriorityLandscapeType> { priorityLandscapeTypeToDelete });
        }
    }
}