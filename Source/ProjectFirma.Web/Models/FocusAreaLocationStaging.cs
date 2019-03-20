using System.Collections.Generic;
using System.IO;
using System.Linq;
using GeoJSON.Net;
using GeoJSON.Net.Feature;
using LtInfo.Common;
using LtInfo.Common.DesignByContract;
using LtInfo.Common.GdalOgr;
using ProjectFirma.Web.Common;

namespace ProjectFirma.Web.Models
{
    public partial class FocusAreaLocationStaging : IAuditableEntity
    {
        public static List<FocusAreaLocationStaging> CreateFocusAreaLocationStagingListFromGdb(
                    FileInfo gisFile, FocusArea focusArea)
        {
            var ogr2OgrCommandLineRunner = new Ogr2OgrCommandLineRunner(FirmaWebConfiguration.Ogr2OgrExecutable,
                Ogr2OgrCommandLineRunner.DefaultCoordinateSystemId,
                FirmaWebConfiguration.HttpRuntimeExecutionTimeout.TotalMilliseconds);

            var geoJsons =
                OgrInfoCommandLineRunner.GetFeatureClassNamesFromFileGdb(new FileInfo(FirmaWebConfiguration.OgrInfoExecutable), gisFile, Ogr2OgrCommandLineRunner.DefaultTimeOut)
                    .ToDictionary(x => x, x => ogr2OgrCommandLineRunner.ImportFileGdbToGeoJson(gisFile, x, false))
                    .Where(x => IsUsableFeatureCollectionGeoJson(JsonTools.DeserializeObject<FeatureCollection>(x.Value)))
                    .ToDictionary(x => x.Key, x => new FeatureCollection(JsonTools.DeserializeObject<FeatureCollection>(x.Value).Features.Where(IsUsableFeatureGeoJson).ToList()).ToGeoJsonString());

            Check.Assert(geoJsons.Count != 0, "Number of usable Feature Classes in uploaded file must be greater than 0.");

            return geoJsons.Select(x => new FocusAreaLocationStaging(focusArea, x.Key, x.Value)).ToList();
        }

        public string AuditDescriptionString => $"Focus Area location staging {FocusArea?.FocusAreaName ?? "(FocusArea Not Found)"}";

        public FeatureCollection ToGeoJsonFeatureCollection()
        {
            return JsonTools.DeserializeObject<FeatureCollection>(GeoJson);
        }

        public static bool IsUsableFeatureCollectionGeoJson(FeatureCollection featureCollection)
        {
            return featureCollection.Features.Any(IsUsableFeatureGeoJson);
        }

        public static bool IsUsableFeatureGeoJson(Feature feature)
        {
            return new List<GeoJSONObjectType> { GeoJSONObjectType.Polygon, GeoJSONObjectType.MultiPolygon }.Contains(feature.Geometry.Type);
        }
    }
}