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

        public static void DeleteCostType(this IQueryable<CostType> costTypes, List<int> costTypeIDList)
        {
            if(costTypeIDList.Any())
            {
                costTypes.Where(x => costTypeIDList.Contains(x.CostTypeID)).Delete();
            }
        }

        public static void DeleteCostType(this IQueryable<CostType> costTypes, ICollection<CostType> costTypesToDelete)
        {
            if(costTypesToDelete.Any())
            {
                var costTypeIDList = costTypesToDelete.Select(x => x.CostTypeID).ToList();
                costTypes.Where(x => costTypeIDList.Contains(x.CostTypeID)).Delete();
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