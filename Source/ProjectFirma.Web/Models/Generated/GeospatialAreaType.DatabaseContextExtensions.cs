//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[GeospatialAreaType]
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
        public static GeospatialAreaType GetGeospatialAreaType(this IQueryable<GeospatialAreaType> geospatialAreaTypes, int geospatialAreaTypeID)
        {
            var geospatialAreaType = geospatialAreaTypes.SingleOrDefault(x => x.GeospatialAreaTypeID == geospatialAreaTypeID);
            Check.RequireNotNullThrowNotFound(geospatialAreaType, "GeospatialAreaType", geospatialAreaTypeID);
            return geospatialAreaType;
        }

        public static void DeleteGeospatialAreaType(this IQueryable<GeospatialAreaType> geospatialAreaTypes, List<int> geospatialAreaTypeIDList)
        {
            if(geospatialAreaTypeIDList.Any())
            {
                geospatialAreaTypes.Where(x => geospatialAreaTypeIDList.Contains(x.GeospatialAreaTypeID)).Delete();
            }
        }

        public static void DeleteGeospatialAreaType(this IQueryable<GeospatialAreaType> geospatialAreaTypes, ICollection<GeospatialAreaType> geospatialAreaTypesToDelete)
        {
            if(geospatialAreaTypesToDelete.Any())
            {
                var geospatialAreaTypeIDList = geospatialAreaTypesToDelete.Select(x => x.GeospatialAreaTypeID).ToList();
                geospatialAreaTypes.Where(x => geospatialAreaTypeIDList.Contains(x.GeospatialAreaTypeID)).Delete();
            }
        }

        public static void DeleteGeospatialAreaType(this IQueryable<GeospatialAreaType> geospatialAreaTypes, int geospatialAreaTypeID)
        {
            DeleteGeospatialAreaType(geospatialAreaTypes, new List<int> { geospatialAreaTypeID });
        }

        public static void DeleteGeospatialAreaType(this IQueryable<GeospatialAreaType> geospatialAreaTypes, GeospatialAreaType geospatialAreaTypeToDelete)
        {
            DeleteGeospatialAreaType(geospatialAreaTypes, new List<GeospatialAreaType> { geospatialAreaTypeToDelete });
        }
    }
}