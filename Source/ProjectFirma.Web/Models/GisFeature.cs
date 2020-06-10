using System.Collections.Generic;
using System.Web;
using GeoJSON.Net.Feature;
using LtInfo.Common;
using LtInfo.Common.DbSpatial;
using LtInfo.Common.GeoJson;
using ProjectFirma.Web.Common;
using ProjectFirma.Web.Controllers;

namespace ProjectFirma.Web.Models
{
    public partial class GisFeature : IAuditableEntity
    {
        public string AuditDescriptionString => $"GisUploadAttemptID: {GisUploadAttemptID}";


        public static List<LayerGeoJson> GetGisFeatureLayers(GisFeature gisFeature)
        {
            var gisFeatureLayer = new LayerGeoJson(gisFeature.GisFeatureID.ToString(),
                new List<GisFeature> { gisFeature }.ToGeoJsonFeatureCollection(), "#2dc3a1", 1,
                LayerInitialVisibility.Show);

            var layerGeoJsons = new List<LayerGeoJson>
            {
                gisFeatureLayer
            };

            return layerGeoJsons;
        }

        public Feature MakeFeatureWithRelevantProperties()
        {
            var sqlGeom = GisFeatureGeometry.ToSqlGeometry().MakeValid();
            var feature = DbGeometryToGeoJsonHelper.FromDbGeometry(sqlGeom.ToDbGeometryWithCoordinateSystem());
            feature.Properties.Add("GisFeature", GetDisplayNameAsUrl().ToString());
            return feature;
        }


        public HtmlString GetDisplayNameAsUrl()
        {
            return UrlTemplate.MakeHrefString(GetDetailUrl(), GisFeatureID.ToString());
        }

        public static readonly UrlTemplate<int> DetailUrlTemplate = new UrlTemplate<int>(SitkaRoute<GisFeatureController>.BuildUrlFromExpression(t => t.Detail(UrlTemplate.Parameter1Int)));

        public string GetDetailUrl()
        {
            return DetailUrlTemplate.ParameterReplace(GisFeatureID);
        }
    }
}