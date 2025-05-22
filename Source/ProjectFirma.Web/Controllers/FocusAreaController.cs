using LtInfo.Common.Models;
using LtInfo.Common.Mvc;
using LtInfo.Common.MvcResults;
using ProjectFirma.Web.Common;
using ProjectFirma.Web.Models;
using ProjectFirma.Web.Security;
using ProjectFirma.Web.Views.FocusArea;
using ProjectFirma.Web.Views.Shared;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using GeoJSON.Net.Feature;
using Index = ProjectFirma.Web.Views.FocusArea.Index;
using IndexGridSpec = ProjectFirma.Web.Views.FocusArea.IndexGridSpec;
using IndexViewData = ProjectFirma.Web.Views.FocusArea.IndexViewData;

namespace ProjectFirma.Web.Controllers
{
    public class FocusAreaController : FirmaBaseController
    {

        [HttpGet]
        [FocusAreaManageFeature]
        public PartialViewResult Delete(FocusAreaPrimaryKey focusAreaPrimaryKey)
        {
            var focusArea = focusAreaPrimaryKey.EntityObject;
            var viewModel = new ConfirmDialogFormViewModel(focusArea.FocusAreaID);
            return ViewDeleteFocusArea(focusArea, viewModel);
        }

        [HttpPost]
        [FocusAreaManageFeature]
        [AutomaticallyCallEntityFrameworkSaveChangesWhenModelValid]
        public ActionResult Delete(FocusAreaPrimaryKey focusAreaPrimaryKey,
            ConfirmDialogFormViewModel viewModel)
        {
            var focusArea = focusAreaPrimaryKey.EntityObject;
            if (!ModelState.IsValid)
            {
                return ViewDeleteFocusArea(focusArea, viewModel);
            }

            focusArea.DeleteFull(HttpRequestStorage.DatabaseEntities);

            return new ModalDialogFormJsonResult();
        }

        private PartialViewResult ViewDeleteFocusArea(FocusArea focusArea, ConfirmDialogFormViewModel viewModel)
        {
            var canDelete = !focusArea.HasDependentObjects();
            var confirmMessage = canDelete
                ? $"Are you sure you want to delete this {Models.FieldDefinition.FocusArea.GetFieldDefinitionLabel()} '{focusArea.FocusAreaName}'?"
                : ConfirmDialogFormViewData.GetStandardCannotDeleteMessage(Models.FieldDefinition.FocusArea.GetFieldDefinitionLabel(), SitkaRoute<FocusAreaController>.BuildLinkFromExpression(x => x.Detail(focusArea), "here"));

            var viewData = new ConfirmDialogFormViewData(confirmMessage, canDelete);
            return RazorPartialView<ConfirmDialogForm, ConfirmDialogFormViewData, ConfirmDialogFormViewModel>(viewData, viewModel);
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
            var focusArea = new FocusArea(string.Empty, ModelObjectHelpers.NotYetAssignedID, ModelObjectHelpers.NotYetAssignedID);
            viewModel.UpdateModel(focusArea);
            HttpRequestStorage.DatabaseEntities.FocusAreas.Add(focusArea);
            HttpRequestStorage.DatabaseEntities.SaveChanges();
            SetMessageForDisplay($"{FieldDefinition.FocusArea.GetFieldDefinitionLabel()} {focusArea.GetDisplayNameAsUrl()} successfully created.");

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
            viewModel.UpdateModel(focusArea);
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
            var mapInitJson = GetMapInitJson(out var hasSpatialData);

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
            var mapInitJson = GetMapInitJsonWithProjects(focusArea, out var hasSpatialData, CurrentPerson);

            var viewData = new DetailViewData(CurrentPerson, focusArea, mapInitJson, hasSpatialData);
            return RazorView<Detail, DetailViewData>(viewData);
        }

        [HttpGet]
        [FocusAreaManageFeature]
        public ViewResult EditLocation(FocusAreaPrimaryKey focusAreaPrimaryKey)
        {
            var viewModel = new EditLocationViewModel();
            var viewData = new EditLocationViewData(CurrentPerson, focusAreaPrimaryKey.EntityObject);
            return RazorView<EditLocation, EditLocationViewData, EditLocationViewModel>(viewData, viewModel);
        }

        [HttpPost]
        [FocusAreaManageFeature]
        [AutomaticallyCallEntityFrameworkSaveChangesWhenModelValid]
        public ActionResult EditLocation(FocusAreaPrimaryKey focusAreaPrimaryKey, EditLocationViewModel viewModel)
        {
            var focusArea = focusAreaPrimaryKey.EntityObject;
            if (!ModelState.IsValid)
            {
                var viewData = new EditLocationViewData(CurrentPerson, focusArea);
                return RazorPartialView<EditLocationErrors, EditLocationViewData, EditLocationViewModel>(viewData, viewModel);
            }

            viewModel.UpdateModel(focusArea);

            return RedirectToAction(new SitkaRoute<FocusAreaController>(c => c.ApproveUploadGis(focusAreaPrimaryKey)));
        }

        [HttpGet]
        [FocusAreaManageFeature]
        public PartialViewResult ApproveUploadGis(FocusAreaPrimaryKey focusAreaPrimaryKey)
        {
            var focusArea = focusAreaPrimaryKey.EntityObject;
            var viewModel = new ApproveUploadGisViewModel();
            return ViewApproveUploadGis(viewModel, focusArea);
        }

        [HttpPost]
        [FocusAreaManageFeature]
        [AutomaticallyCallEntityFrameworkSaveChangesWhenModelValid]
        public ActionResult ApproveUploadGis(FocusAreaPrimaryKey focusAreaPrimaryKey, ApproveUploadGisViewModel viewModel)
        {
            var focusArea = focusAreaPrimaryKey.EntityObject;
            if (!ModelState.IsValid)
            {
                return ViewApproveUploadGis(viewModel, focusArea);
            }

            viewModel.UpdateModel(focusArea);
            HttpRequestStorage.DatabaseEntities.FocusAreaLocationStagings.RemoveRange(focusArea
                .FocusAreaLocationStagings);

            SetMessageForDisplay($"{FieldDefinition.FocusArea.GetFieldDefinitionLabel()} Location for {focusArea.GetDisplayNameAsUrl()} successfully updated.");
            return new ContentResult();
        }
        private PartialViewResult ViewApproveUploadGis(ApproveUploadGisViewModel viewModel, FocusArea focusArea)
        {
            var layers = focusArea.FocusAreaLocationStagings.Select((x, index) => new LayerGeoJson(
                x.FeatureClassName, x.ToGeoJsonFeatureCollection(),
                FirmaHelpers.DefaultColorRange[index], 0.8m,
                index == 0 ? LayerInitialVisibility.Show : LayerInitialVisibility.Hide)).ToList();
            var mapInitJson = new MapInitJson("focusAreaLocationApproveUploadGisMap", 10, layers, MapInitJson.GetExternalMapLayersForOtherMaps(), BoundingBox.MakeBoundingBoxFromLayerGeoJsonList(layers));

            var viewData = new ApproveUploadGisViewData(CurrentPerson, focusArea, mapInitJson);
            return RazorPartialView<ApproveUploadGis, ApproveUploadGisViewData, ApproveUploadGisViewModel>(viewData, viewModel);
        }

        [HttpGet]
        [FocusAreaManageFeature]
        public PartialViewResult DeleteFocusAreaLocation(FocusAreaPrimaryKey focusAreaPrimaryKey)
        {
            var focusArea = focusAreaPrimaryKey.EntityObject;
            var viewModel = new ConfirmDialogFormViewModel(focusArea.FocusAreaID);
            return ViewDeleteFocusAreaLocation(focusArea, viewModel);
        }

        [HttpPost]
        [FocusAreaManageFeature]
        [AutomaticallyCallEntityFrameworkSaveChangesWhenModelValid]
        public ActionResult DeleteFocusAreaLocation(FocusAreaPrimaryKey focusAreaPrimaryKey,
            ConfirmDialogFormViewModel viewModel)
        {
            var focusArea = focusAreaPrimaryKey.EntityObject;
            if (!ModelState.IsValid)
            {
                return ViewDeleteFocusAreaLocation(focusArea, viewModel);
            }

            focusArea.FocusAreaLocation = null;

            return new ModalDialogFormJsonResult();
        }

        private PartialViewResult ViewDeleteFocusAreaLocation(FocusArea focusArea,
            ConfirmDialogFormViewModel viewModel)
        {
            var viewData = new ConfirmDialogFormViewData(
                $"Are you sure you want to delete the location for this {FieldDefinition.FocusArea.GetFieldDefinitionLabel()} '{focusArea.FocusAreaName}'?");
            return RazorPartialView<ConfirmDialogForm, ConfirmDialogFormViewData, ConfirmDialogFormViewModel>(viewData,
                viewModel);
        }


        
        private PartialViewResult ViewEdit(EditViewModel viewModel)
        {
            var focusAreaStatusAsSelectListItems =
                FocusAreaStatus.All.ToSelectListWithEmptyFirstRow(k => k.FocusAreaStatusID.ToString(), v => v.FocusAreaStatusDisplayName);
            var regions =
                HttpRequestStorage.DatabaseEntities.DNRUplandRegions.ToList().OrderBy(x => x.DisplayName).ToSelectListWithEmptyFirstRow(k => k.DNRUplandRegionID.ToString(),
                    v => v.DNRUplandRegionName);
            var isSitkaAdmin = new SitkaAdminFeature().HasPermissionByPerson(CurrentPerson);
            var viewData = new EditViewData(focusAreaStatusAsSelectListItems, regions, isSitkaAdmin);
            return RazorPartialView<Edit, EditViewData, EditViewModel>(viewData, viewModel);
        }

        private static MapInitJson GetMapInitJsonWithProjects(FocusArea focusArea, out bool hasSpatialData, Person currentPerson)
        {
            hasSpatialData = false;

            var layers = new List<LayerGeoJson>();

            if (focusArea.FocusAreaLocation != null)
            {
                hasSpatialData = true;
                layers.Add(new LayerGeoJson($"{FieldDefinition.FocusArea.GetFieldDefinitionLabel()} Location",
                    focusArea.FocusAreaLocationToFeatureCollection(), "blue", 1,
                    LayerInitialVisibility.Show));
            }

            var allActiveProjectsAndProposals = focusArea.GetAllActiveProjects(currentPerson).Where(x => x.ProjectStage.ShouldShowOnMap()).ToList();

            var projectsAsSimpleLocations = allActiveProjectsAndProposals.Where(x => x.ProjectLocationSimpleType != ProjectLocationSimpleType.None).ToList();
            var projectSimpleLocationsFeatureCollection = new FeatureCollection();

            projectSimpleLocationsFeatureCollection.Features.AddRange(projectsAsSimpleLocations.Select(x =>
            {
                var feature = x.MakePointFeatureWithRelevantProperties(x.ProjectLocationPoint, true, true, FieldDefinition.Organization.GetFieldDefinitionLabel(), FieldDefinition.Organization.GetFieldDefinitionLabelPluralized());
                feature.Properties["FeatureColor"] = "#99b3ff";
                return feature;
            }).ToList());

            if (projectSimpleLocationsFeatureCollection.Features.Any())
            {
                hasSpatialData = true;
                layers.Add(new LayerGeoJson("Projects", projectSimpleLocationsFeatureCollection, "yellow", 1, LayerInitialVisibility.Show));
            }

            var boundingBox = BoundingBox.MakeBoundingBoxFromLayerGeoJsonList(layers);

            return new MapInitJson($"focusArea_{focusArea.FocusAreaID}_Map", 10, layers,  MapInitJson.GetExternalMapLayersForOtherMaps(), boundingBox);
        }

        private static MapInitJson GetMapInitJson(out bool hasSpatialData)
        {
            hasSpatialData = false;

            var layers = new List<LayerGeoJson>();
            var focusAreas = HttpRequestStorage.DatabaseEntities.FocusAreas.ToList();
            var locationFeatures = new List<Feature>();

            foreach (var focusArea in focusAreas)
            {
                if (focusArea.FocusAreaLocation != null)
                {
                    hasSpatialData = true;
                    locationFeatures.AddRange(focusArea.FocusAreaLocationToFeature());
                }
            }

            if (locationFeatures.Any())
            {
                layers.Add(new LayerGeoJson($"{FieldDefinition.FocusArea.GetFieldDefinitionLabel()} Location",
                    new FeatureCollection(locationFeatures), "blue", 1,
                    LayerInitialVisibility.Show));
            }


            var boundingBox = BoundingBox.MakeBoundingBoxFromLayerGeoJsonList(layers);

            return new MapInitJson("focusAreaIndex", 10, layers, MapInitJson.GetExternalMapLayersForOtherMaps(), boundingBox);
        }
    }
}