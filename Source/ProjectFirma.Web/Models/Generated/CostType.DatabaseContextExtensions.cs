//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[CostType]
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
        public static CostType GetCostType(this IQueryable<CostType> costTypes, int costTypeID)
        {
            var costType = costTypes.SingleOrDefault(x => x.CostTypeID == costTypeID);
            Check.RequireNotNullThrowNotFound(costType, "CostType", costTypeID);
            return costType;
        }

        // Delete using an IDList (WADNR style)
        public static void DeleteCostType(this IQueryable<CostType> costTypes, List<int> costTypeIDList)
        {
            if(costTypeIDList.Any())
            {
                var costTypesInSourceCollectionToDelete = costTypes.Where(x => costTypeIDList.Contains(x.CostTypeID));
                foreach (var costTypeToDelete in costTypesInSourceCollectionToDelete.ToList())
                {
                    costTypeToDelete.Delete(HttpRequestStorage.DatabaseEntities);
                }
            }
        }

        // Delete using an object list (WADNR style)
        public static void DeleteCostType(this IQueryable<CostType> costTypes, ICollection<CostType> costTypesToDelete)
        {
            if(costTypesToDelete.Any())
            {
                var costTypeIDList = costTypesToDelete.Select(x => x.CostTypeID).ToList();
                var costTypesToDeleteFromSourceList = costTypes.Where(x => costTypeIDList.Contains(x.CostTypeID)).ToList();

                foreach (var costTypeToDelete in costTypesToDeleteFromSourceList)
                {
                    costTypeToDelete.Delete(HttpRequestStorage.DatabaseEntities);
                }
            }
        }

        public static void DeleteCostType(this IQueryable<CostType> costTypes, int costTypeID)
        {
            DeleteCostType(costTypes, new List<int> { costTypeID });
        }

        public static void DeleteCostType(this IQueryable<CostType> costTypes, CostType costTypeToDelete)
        {
            DeleteCostType(costTypes, new List<CostType> { costTypeToDelete });
        }
    }
}