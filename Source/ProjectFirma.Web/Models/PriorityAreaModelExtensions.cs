using System.Collections.Generic;
using System.Linq;
using GeoJSON.Net.Feature;

namespace ProjectFirma.Web.Models
{
    public static class PriorityAreaModelExtensions
    {
        public static FeatureCollection ToGeoJsonFeatureCollection(this IEnumerable<PriorityArea> priorityAreas)
        {
            return new FeatureCollection(priorityAreas.Select(x => x.MakeFeatureWithRelevantProperties()).ToList());
        }
    }
}