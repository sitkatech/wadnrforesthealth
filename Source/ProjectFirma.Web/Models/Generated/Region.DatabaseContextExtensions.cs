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

        public static void DeleteRegion(this IQueryable<Region> regions, List<int> regionIDList)
        {
            if(regionIDList.Any())
            {
                regions.Where(x => regionIDList.Contains(x.RegionID)).Delete();
            }
        }

        public static void DeleteRegion(this IQueryable<Region> regions, ICollection<Region> regionsToDelete)
        {
            if(regionsToDelete.Any())
            {
                var regionIDList = regionsToDelete.Select(x => x.RegionID).ToList();
                regions.Where(x => regionIDList.Contains(x.RegionID)).Delete();
            }
        }

        public static void DeleteRegion(this IQueryable<Region> regions, int regionID)
        {
            DeleteRegion(regions, new List<int> { regionID });
        }

        public static void DeleteRegion(this IQueryable<Region> regions, Region regionToDelete)
        {
            DeleteRegion(regions, new List<Region> { regionToDelete });
        }
    }
}