using System.Web.Mvc;
using LtInfo.Common.DbSpatial;
using LtInfo.Common.GeoJson;
using ProjectFirma.Web.Models;
using ProjectFirma.Web.Security;
using ProjectFirma.Web.Views.GisFeature;

namespace ProjectFirma.Web.Controllers
{
    public class GisFeatureController : FirmaBaseController
    {
        [PriorityLandscapeViewFeature]
        public ViewResult Detail(GisFeaturePrimaryKey gisFeaturePrimaryKey)
        {
            var gisFeature = gisFeaturePrimaryKey.EntityObject;
            var mapDivID = $"gisFeature_{gisFeature.GisFeatureID}_Map";

            var sqlGeom = gisFeature.GisFeatureGeometry.ToSqlGeometry().MakeValid();
            var feature = sqlGeom.ToDbGeometryWithCoordinateSystem();

            var layers = GisFeature.GetGisFeatureLayers(gisFeature);

            var mapInitJson = new MapInitJson(mapDivID, 10, layers, MapInitJson.GetExternalMapLayersForOtherMaps(), new BoundingBox(feature));
            var viewData = new DetailViewData(CurrentPerson, gisFeature, mapInitJson);
            return RazorView<Detail, DetailViewData>(viewData);
        }
    }
}