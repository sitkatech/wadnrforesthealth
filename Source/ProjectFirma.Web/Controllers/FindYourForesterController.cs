using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using LtInfo.Common.MvcResults;
using MoreLinq;
using ProjectFirma.Web.Common;
using ProjectFirma.Web.Models;
using ProjectFirma.Web.Security;
using ProjectFirma.Web.Security.Shared;
using ProjectFirma.Web.Views.FindYourForester;
using ProjectFirma.Web.Views.Invoice;
using ProjectFirma.Web.Views.Project;
using ProjectFirma.Web.Views.Shared;

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



        [FindYourForesterManageFeature]
        public ViewResult Manage()
        {
            
            var layerGeoJsons = new List<LayerGeoJson>();
            var layerVisibility = LayerInitialVisibility.Show;
            foreach (var role in ForesterRole.All)
            {
                if (HttpRequestStorage.DatabaseEntities.ForesterWorkUnits.Any(x => x.ForesterRoleID == role.ForesterRoleID))
                {
                    var tempLayer = new LayerGeoJson(role.ForesterRoleDisplayName, FirmaWebConfiguration.WebMapServiceUrl,
                        FirmaWebConfiguration.GetFindYourForesterWmsLayerName(), "#59ACFF", 0.2m, layerVisibility, $"ForesterRoleID={role.ForesterRoleID}", true);
                    if (layerVisibility == LayerInitialVisibility.Show)
                    {
                        layerVisibility = LayerInitialVisibility.Hide;
                    }
                    layerGeoJsons.Add(tempLayer);
                }
            }
            

            var mapInitJson = new MapInitJson("manageFindYourForester", 10, layerGeoJsons, BoundingBox.MakeNewDefaultBoundingBox());
            var firmaPage = FirmaPage.GetFirmaPageByPageType(FirmaPageType.ManageFindYourForester);
            var viewData = new ManageFindYourForesterViewData(CurrentPerson, mapInitJson, firmaPage);
            return RazorView<ManageFindYourForester, ManageFindYourForesterViewData>(viewData);
        }


        [FindYourForesterManageFeature]
        public GridJsonNetJObjectResult<FindYourForesterGridObject> ManageFindYourForesterGridJsonData()
        {
            var gridSpec = new ManageFindYourForesterGridSpec(CurrentPerson);
            var findYourForesterGridObjects = HttpRequestStorage.DatabaseEntities.ForesterWorkUnits.ToList().Select(x => new FindYourForesterGridObject(x)).ToList();
            var gridJsonNetJObjectResult = new GridJsonNetJObjectResult<FindYourForesterGridObject>(findYourForesterGridObjects, gridSpec);
            return gridJsonNetJObjectResult;
        }

        [HttpGet]
        public PartialViewResult UnassignForester(ForesterWorkUnitPrimaryKey foresterWorkUnitPrimaryKey)
        {
            /*var foresterWorkUnit = foresterWorkUnitPrimaryKey.EntityObject;
            var viewModel = new ConfirmDialogFormViewModel(foresterWorkUnit.ForesterWorkUnitID);
            return ViewUnassignForester(foresterWorkUnit, viewModel);
            */
            throw new NotImplementedException();
        }

        [HttpPost]
        [AutomaticallyCallEntityFrameworkSaveChangesWhenModelValid]
        public ActionResult UnassignForester(ForesterWorkUnitPrimaryKey foresterWorkUnitPrimaryKey,
            ConfirmDialogFormViewModel viewModel)
        {
            /*var foresterWorkUnit = foresterWorkUnitPrimaryKey.EntityObject;
            if (!ModelState.IsValid)
            {
                return ViewUnassignForester(foresterWorkUnit, viewModel);
            }

            foresterWorkUnit.PersonID = null;

            return new ModalDialogFormJsonResult();*/
            throw new NotImplementedException();
        }
        private PartialViewResult ViewUnassignForester(ForesterWorkUnit foresterWorkUnit, ConfirmDialogFormViewModel viewModel)
        {
            var confirmMessage = $"Are you sure you want to delete this '{foresterWorkUnit.Person.FirstName}'?";
            var viewData = new ConfirmDialogFormViewData(confirmMessage);
            return RazorPartialView<ConfirmDialogForm, ConfirmDialogFormViewData, ConfirmDialogFormViewModel>(viewData, viewModel);
        }

    }
}