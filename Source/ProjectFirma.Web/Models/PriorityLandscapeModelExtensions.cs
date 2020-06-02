using System.Collections.Generic;
using System.Linq;
using GeoJSON.Net.Feature;

namespace ProjectFirma.Web.Models
{
    public static class PriorityLandscapeModelExtensions
    {
        public static FeatureCollection ToGeoJsonFeatureCollection(this IEnumerable<PriorityLandscape> priorityLandscapes)
        {
            return new FeatureCollection(priorityLandscapes.Select(x => x.MakeFeatureWithRelevantProperties()).ToList());
        }
    }
}