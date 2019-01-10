using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web.Mvc;
using GeoJSON.Net.Feature;
using LtInfo.Common;
using LtInfo.Common.Models;
using LtInfo.Common.Mvc;
using LtInfo.Common.MvcResults;
using ProjectFirma.Web.Common;
using ProjectFirma.Web.Models;
using ProjectFirma.Web.Security;
using ProjectFirma.Web.Views.FocusArea;
using ProjectFirma.Web.Views.Shared;
using Index = ProjectFirma.Web.Views.FocusArea.Index;
using IndexGridSpec = ProjectFirma.Web.Views.FocusArea.IndexGridSpec;
using IndexViewData = ProjectFirma.Web.Views.FocusArea.IndexViewData;

namespace ProjectFirma.Web.Controllers
{
    public class FocusAreaController : FirmaBaseController
    {

        [HttpGet]
        [FocusAreaManageFeature]
        public PartialViewResult DeleteFocusArea(FocusAreaPrimaryKey focusAreaPrimaryKey)
        {
            var focusArea = focusAreaPrimaryKey.EntityObject;
            var viewModel = new ConfirmDialogFormViewModel(focusArea.FocusAreaID);
            return ViewDeleteFocusArea(focusArea, viewModel);
        }

        [HttpGet]
        [FocusAreaManageFeature]
        public PartialViewResult New()
        {
            var viewModel = new EditViewModel();
            return ViewEdit(viewModel);
        }

        [HttpPost]
        [FocusAreaManageFeature]
        [AutomaticallyCallEntityFrameworkSaveChangesWhenModelValid]
        public ActionResult New(EditViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return ViewEdit(viewModel);
            }
            var focusArea = new FocusArea(string.Empty, ModelObjectHelpers.NotYetAssignedID);
            viewModel.UpdateModel(focusArea);
            HttpRequestStorage.DatabaseEntities.AllFocusAreas.Add(focusArea);
            HttpRequestStorage.DatabaseEntities.SaveChanges();
            SetMessageForDisplay($"FocusArea {focusArea.FocusAreaName} successfully created.");

            return new ModalDialogFormJsonResult();
        }

        [HttpGet]
        [FocusAreaManageFeature]
        public PartialViewResult Edit(FocusAreaPrimaryKey focusAreaPrimaryKey)
        {
            var focusArea = focusAreaPrimaryKey.EntityObject;
            var viewModel = new EditViewModel(focusArea);
            return ViewEdit(viewModel);
        }

        [HttpPost]
        [FocusAreaManageFeature]
        [AutomaticallyCallEntityFrameworkSaveChangesWhenModelValid]
        public ActionResult Edit(FocusAreaPrimaryKey focusAreaPrimaryKey, EditViewModel viewModel)
        {
            var focusArea = focusAreaPrimaryKey.EntityObject;
            if (!ModelState.IsValid)
            {
                return ViewEdit(viewModel);
            }
            //viewModel.UpdateModel(focusArea, CurrentPerson);
            return new ModalDialogFormJsonResult();
        }

        [FocusAreaViewFeature]
        public GridJsonNetJObjectResult<Project> DetailProjectFocusAreaGridJsonData(FocusAreaPrimaryKey focusAreaPrimaryKey)
        {
            var focusArea = focusAreaPrimaryKey.EntityObject;
            var gridSpec = new ProjectsIncludingLeadImplementingGridSpec(CurrentPerson, false);
            var projects = focusArea.Projects.ToList();
            var gridJsonNetJObjectResult = new GridJsonNetJObjectResult<Project>(projects, gridSpec);
            return gridJsonNetJObjectResult;
        }

        [FocusAreaViewFeature]
        public ViewResult Index()
        {
            var layerGeoJsons = new List<LayerGeoJson>();
            
            var mapInitJson = new MapInitJson("focusAreaIndex", 10, layerGeoJsons, BoundingBox.MakeNewDefaultBoundingBox());

            var firmaPage = Models.FirmaPage.GetFirmaPageByPageType(FirmaPageType.FocusAreasList);
            var viewData = new IndexViewData(CurrentPerson, mapInitJson, firmaPage);
            return RazorView<Index, IndexViewData>(viewData);
        }

        [FocusAreaViewFeature]
        public GridJsonNetJObjectResult<FocusArea> IndexGridJsonData()
        {
            var gridSpec = new IndexGridSpec(CurrentPerson);
            var focusAreas = HttpRequestStorage.DatabaseEntities.FocusAreas.OrderBy(x => x.FocusAreaName).ToList();
            var gridJsonNetJObjectResult = new GridJsonNetJObjectResult<FocusArea>(focusAreas, gridSpec);
            return gridJsonNetJObjectResult;
        }

        [FocusAreaViewFeature]
        public ViewResult Detail(FocusAreaPrimaryKey focusAreaPrimaryKey)
        {
            var focusArea = focusAreaPrimaryKey.EntityObject;

            var mapInitJson = GetMapInitJson(focusArea, out var hasSpatialData, CurrentPerson);



            var viewData = new DetailViewData(CurrentPerson, focusArea, mapInitJson, hasSpatialData);
            return RazorView<Detail, DetailViewData>(viewData);
        }



        private PartialViewResult ViewDeleteFocusArea(FocusArea focusArea, ConfirmDialogFormViewModel viewModel)
        {
            var confirmMessage = $"FocusArea \"{focusArea.FocusAreaName}\" has been deleted";
            var viewData = new ConfirmDialogFormViewData(confirmMessage, true);
            return RazorPartialView<ConfirmDialogForm, ConfirmDialogFormViewData, ConfirmDialogFormViewModel>(viewData, viewModel);
        }
        private PartialViewResult ViewEdit(EditViewModel viewModel)
        {
            var focusAreaStatusAsSelectListItems =
                FocusAreaStatus.All.ToSelectListWithEmptyFirstRow(v => v.FocusAreaStatusID.ToString(), m => m.FocusAreaStatusDisplayName);
            var isSitkaAdmin = new SitkaAdminFeature().HasPermissionByPerson(CurrentPerson);
            var viewData = new EditViewData(focusAreaStatusAsSelectListItems, isSitkaAdmin);
            return RazorPartialView<Edit, EditViewData, EditViewModel>(viewData, viewModel);
        }

        private static MapInitJson GetMapInitJson(FocusArea focusArea, out bool hasSpatialData, Person person)
        {

            hasSpatialData = false;

            var layers = new List<LayerGeoJson>();

            var boundingBox = BoundingBox.MakeBoundingBoxFromLayerGeoJsonList(layers);
            layers.AddRange(MapInitJson.GetAllGeospatialAreaMapLayers(LayerInitialVisibility.Show));

            return new MapInitJson($"focusArea_{focusArea.FocusAreaID}_Map", 10, layers, boundingBox);
        }
    }
}