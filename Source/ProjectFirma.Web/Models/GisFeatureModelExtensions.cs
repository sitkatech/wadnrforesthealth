using System.Collections.Generic;
using System.Linq;
using GeoJSON.Net.Feature;

namespace ProjectFirma.Web.Models
{
    public static class GisFeatureModelExtensions
    {
        public static FeatureCollection ToGeoJsonFeatureCollection(this IEnumerable<GisFeature> gisFeatures)
        {
            return new FeatureCollection(gisFeatures.Select(x => x.MakeFeatureWithRelevantProperties()).ToList());
        }
    }
}