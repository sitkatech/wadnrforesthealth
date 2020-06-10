using System.Web.Mvc;
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
            var layers = GisFeature.GetGisFeatureLayers(gisFeature);
            var mapInitJson = new MapInitJson(mapDivID, 10, layers, new BoundingBox(gisFeature.GisFeatureGeometry));
            var viewData = new DetailViewData(CurrentPerson, gisFeature, mapInitJson);
            return RazorView<Detail, DetailViewData>(viewData);
        }
    }
}