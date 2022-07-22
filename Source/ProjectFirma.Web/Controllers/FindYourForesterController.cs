using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using LtInfo.Common.MvcResults;
using MoreLinq;
using ProjectFirma.Web.Common;
using ProjectFirma.Web.Models;
using ProjectFirma.Web.Security;
using ProjectFirma.Web.Security.Shared;
using ProjectFirma.Web.Views.DNRUplandRegion;
using ProjectFirma.Web.Views.Project;
using ProjectFirma.Web.Views.Shared;
using Detail = ProjectFirma.Web.Views.DNRUplandRegion.Detail;
using DetailViewData = ProjectFirma.Web.Views.DNRUplandRegion.DetailViewData;
using Index = ProjectFirma.Web.Views.DNRUplandRegion.Index;
using IndexViewData = ProjectFirma.Web.Views.DNRUplandRegion.IndexViewData;

namespace ProjectFirma.Web.Controllers
{
    public class FindYourForesterController : FirmaBaseController
    {
        [FindYourForesterViewFeature]
        public ViewResult Index()
        {
            //var layerGeoJsons = new List<LayerGeoJson>
            //{
            //    DNRUplandRegion.GetRegionWmsLayerGeoJson("#59ACFF", 0.2m, LayerInitialVisibility.Show)
            //};

            //var mapInitJson = new MapInitJson("regionIndex", 10, layerGeoJsons, BoundingBox.MakeNewDefaultBoundingBox());
            //var firmaPage = FirmaPage.GetFirmaPageByPageType(FirmaPageType.RegionsList);
            //var viewData = new IndexViewData(CurrentPerson, mapInitJson, firmaPage);
            //return RazorView<Index, IndexViewData>(viewData);
            throw new NotImplementedException();
        }





    }
}