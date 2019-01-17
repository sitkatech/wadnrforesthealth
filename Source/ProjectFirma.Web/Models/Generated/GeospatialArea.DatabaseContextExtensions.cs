//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[GeospatialArea]
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
        public static GeospatialArea GetGeospatialArea(this IQueryable<GeospatialArea> geospatialAreas, int geospatialAreaID)
        {
            var geospatialArea = geospatialAreas.SingleOrDefault(x => x.GeospatialAreaID == geospatialAreaID);
            Check.RequireNotNullThrowNotFound(geospatialArea, "GeospatialArea", geospatialAreaID);
            return geospatialArea;
        }

        public static void DeleteGeospatialArea(this IQueryable<GeospatialArea> geospatialAreas, List<int> geospatialAreaIDList)
        {
            if(geospatialAreaIDList.Any())
            {
                geospatialAreas.Where(x => geospatialAreaIDList.Contains(x.GeospatialAreaID)).Delete();
            }
        }

        public static void DeleteGeospatialArea(this IQueryable<GeospatialArea> geospatialAreas, ICollection<GeospatialArea> geospatialAreasToDelete)
        {
            if(geospatialAreasToDelete.Any())
            {
                var geospatialAreaIDList = geospatialAreasToDelete.Select(x => x.GeospatialAreaID).ToList();
                geospatialAreas.Where(x => geospatialAreaIDList.Contains(x.GeospatialAreaID)).Delete();
            }
        }

        public static void DeleteGeospatialArea(this IQueryable<GeospatialArea> geospatialAreas, int geospatialAreaID)
        {
            DeleteGeospatialArea(geospatialAreas, new List<int> { geospatialAreaID });
        }

        public static void DeleteGeospatialArea(this IQueryable<GeospatialArea> geospatialAreas, GeospatialArea geospatialAreaToDelete)
        {
            DeleteGeospatialArea(geospatialAreas, new List<GeospatialArea> { geospatialAreaToDelete });
        }
    }
}