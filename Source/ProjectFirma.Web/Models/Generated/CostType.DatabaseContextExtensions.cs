//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[CostType]
using System.Collections.Generic;
using System.Linq;
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

        public static void DeleteCostType(this List<int> costTypeIDList)
        {
            if(costTypeIDList.Any())
            {
                HttpRequestStorage.DatabaseEntities.AllCostTypes.RemoveRange(HttpRequestStorage.DatabaseEntities.CostTypes.Where(x => costTypeIDList.Contains(x.CostTypeID)));
            }
        }

        public static void DeleteCostType(this ICollection<CostType> costTypesToDelete)
        {
            if(costTypesToDelete.Any())
            {
                HttpRequestStorage.DatabaseEntities.AllCostTypes.RemoveRange(costTypesToDelete);
            }
        }

        public static void DeleteCostType(this int costTypeID)
        {
            DeleteCostType(new List<int> { costTypeID });
        }

        public static void DeleteCostType(this CostType costTypeToDelete)
        {
            DeleteCostType(new List<CostType> { costTypeToDelete });
        }
    }
}