//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[WashingtonCounty]
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
        public static WashingtonCounty GetWashingtonCounty(this IQueryable<WashingtonCounty> washingtonCounties, int washingtonCountyID)
        {
            var washingtonCounty = washingtonCounties.SingleOrDefault(x => x.WashingtonCountyID == washingtonCountyID);
            Check.RequireNotNullThrowNotFound(washingtonCounty, "WashingtonCounty", washingtonCountyID);
            return washingtonCounty;
        }

        // Delete using an IDList (WADNR style)
        public static void DeleteWashingtonCounty(this IQueryable<WashingtonCounty> washingtonCounties, List<int> washingtonCountyIDList)
        {
            if(washingtonCountyIDList.Any())
            {
                var washingtonCountiesInSourceCollectionToDelete = washingtonCounties.Where(x => washingtonCountyIDList.Contains(x.WashingtonCountyID));
                foreach (var washingtonCountyToDelete in washingtonCountiesInSourceCollectionToDelete.ToList())
                {
                    washingtonCountyToDelete.Delete(HttpRequestStorage.DatabaseEntities);
                }
            }
        }

        // Delete using an object list (WADNR style)
        public static void DeleteWashingtonCounty(this IQueryable<WashingtonCounty> washingtonCounties, ICollection<WashingtonCounty> washingtonCountiesToDelete)
        {
            if(washingtonCountiesToDelete.Any())
            {
                var washingtonCountyIDList = washingtonCountiesToDelete.Select(x => x.WashingtonCountyID).ToList();
                var washingtonCountiesToDeleteFromSourceList = washingtonCounties.Where(x => washingtonCountyIDList.Contains(x.WashingtonCountyID)).ToList();

                foreach (var washingtonCountyToDelete in washingtonCountiesToDeleteFromSourceList)
                {
                    washingtonCountyToDelete.Delete(HttpRequestStorage.DatabaseEntities);
                }
            }
        }

        public static void DeleteWashingtonCounty(this IQueryable<WashingtonCounty> washingtonCounties, int washingtonCountyID)
        {
            DeleteWashingtonCounty(washingtonCounties, new List<int> { washingtonCountyID });
        }

        public static void DeleteWashingtonCounty(this IQueryable<WashingtonCounty> washingtonCounties, WashingtonCounty washingtonCountyToDelete)
        {
            DeleteWashingtonCounty(washingtonCounties, new List<WashingtonCounty> { washingtonCountyToDelete });
        }
    }
}