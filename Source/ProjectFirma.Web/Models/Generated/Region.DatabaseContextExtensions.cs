//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[Region]
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
        public static Region GetRegion(this IQueryable<Region> regions, int regionID)
        {
            var region = regions.SingleOrDefault(x => x.RegionID == regionID);
            Check.RequireNotNullThrowNotFound(region, "Region", regionID);
            return region;
        }

        public static void DeleteRegion(this List<int> regionIDList)
        {
            if(regionIDList.Any())
            {
                HttpRequestStorage.DatabaseEntities.Regions.RemoveRange(HttpRequestStorage.DatabaseEntities.Regions.Where(x => regionIDList.Contains(x.RegionID)));
            }
        }

        public static void DeleteRegion(this ICollection<Region> regionsToDelete)
        {
            if(regionsToDelete.Any())
            {
                HttpRequestStorage.DatabaseEntities.Regions.RemoveRange(regionsToDelete);
            }
        }

        public static void DeleteRegion(this int regionID)
        {
            DeleteRegion(new List<int> { regionID });
        }

        public static void DeleteRegion(this Region regionToDelete)
        {
            DeleteRegion(new List<Region> { regionToDelete });
        }
    }


}