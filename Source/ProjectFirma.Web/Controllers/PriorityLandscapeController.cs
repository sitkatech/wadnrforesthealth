using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using GeoJSON.Net.Feature;
using LtInfo.Common.DesignByContract;
using LtInfo.Common.MvcResults;
using ProjectFirma.Web.Common;
using ProjectFirma.Web.Models;
using ProjectFirma.Web.Security;
using ProjectFirma.Web.Security.Shared;
using ProjectFirma.Web.Views.Project;
using ProjectFirma.Web.Views.PriorityLandscape;
using ProjectFirma.Web.Views.Shared;
using ProjectFirma.Web.Views.Shared.FileResourceControls;
using Detail = ProjectFirma.Web.Views.PriorityLandscape.Detail;
using DetailViewData = ProjectFirma.Web.Views.PriorityLandscape.DetailViewData;
using Index = ProjectFirma.Web.Views.PriorityLandscape.Index;
using IndexGridSpec = ProjectFirma.Web.Views.PriorityLandscape.IndexGridSpec;
using IndexViewData = ProjectFirma.Web.Views.PriorityLandscape.IndexViewData;

namespace ProjectFirma.Web.Controllers
{
    public class PriorityLandscapeController : FirmaBaseController
    {
        [PriorityLandscapeViewFeature]
        public ViewResult Index()
        {

            var layerGeoJsons = new List<LayerGeoJson>();
            layerGeoJsons = new List<LayerGeoJson>
            {
                PriorityLandscape.GetPriorityLandscapeWmsLayerGeoJson(0.2m, LayerInitialVisibility.Show, PriorityLandscapeCategory.East),
                PriorityLandscape.GetPriorityLandscapeWmsLayerGeoJson(0.2m, LayerInitialVisibility.Show, PriorityLandscapeCategory.West),
                MapInitJson.GetAllDetailedProjectLocations()
            };

            var mapInitJson = new MapInitJson("priorityLandscapeIndex", 10, layerGeoJsons, MapInitJson.GetExternalMapLayersForPriorityLandscape(), BoundingBox.MakeNewDefaultBoundingBox());
            var firmaPage = FirmaPage.GetFirmaPageByPageType(FirmaPageType.PriorityLandscapesList);
            var viewData = new IndexViewData(CurrentPerson, mapInitJson, firmaPage);
            return RazorView<Index, IndexViewData>(viewData);
        }


        [PriorityLandscapeViewFeature]
        public GridJsonNetJObjectResult<PriorityLandscape> IndexGridJsonData()
        {
            var gridSpec = new IndexGridSpec(CurrentPerson);
            var priorityLandscapes = HttpRequestStorage.DatabaseEntities.PriorityLandscapes.OrderBy(x => x.PriorityLandscapeName).ToList();
            var gridJsonNetJObjectResult = new GridJsonNetJObjectResult<PriorityLandscape>(priorityLandscapes, gridSpec);
            return gridJsonNetJObjectResult;
        }

        [PriorityLandscapeViewFeature]
        public ViewResult Detail(PriorityLandscapePrimaryKey priorityLandscapePrimaryKey)
        {
            var priorityLandscape = priorityLandscapePrimaryKey.EntityObject;
            var mapDivID = $"priorityLandscape_{priorityLandscape.PriorityLandscapeID}_Map";

            var associatedProjects = priorityLandscape.GetAssociatedProjectsVisibleToUser(CurrentPerson);
            var layers = PriorityLandscape.GetPriorityLandscapeAndAssociatedProjectLayers(priorityLandscape, associatedProjects);
            var mapInitJson = new MapInitJson(mapDivID, 10, layers, MapInitJson.GetExternalMapLayersForPriorityLandscape(), new BoundingBox(priorityLandscape.PriorityLandscapeLocation));

            var projectGrantAllocationExpenditures = associatedProjects.SelectMany(x => x.ProjectGrantAllocationExpenditures);
            var organizationTypes = HttpRequestStorage.DatabaseEntities.OrganizationTypes.ToList();

            const string chartTitle = "Reported Expenditures By Organization Type";
            var chartContainerID = chartTitle.Replace(" ", "");
            var googleChart = projectGrantAllocationExpenditures.ToGoogleChart(x => x.GrantAllocation.BottommostOrganization.OrganizationType.OrganizationTypeName,
                organizationTypes.Select(x => x.OrganizationTypeName).ToList(),
                x => x.GrantAllocation.BottommostOrganization.OrganizationType.OrganizationTypeName,
                chartContainerID,
                chartTitle);

            var viewGoogleChartViewData = new ViewGoogleChartViewData(googleChart, chartTitle, 405, true);

            var performanceMeasures = associatedProjects
                .SelectMany(x => x.PerformanceMeasureActuals)
                .Select(x => x.PerformanceMeasure).Distinct()
                .OrderBy(x => x.PerformanceMeasureDisplayName)
                .ToList();

            var viewData = new DetailViewData(CurrentPerson, priorityLandscape, mapInitJson, viewGoogleChartViewData, performanceMeasures);
            return RazorView<Detail, DetailViewData>(viewData);
        }

        [HttpGet]
        [PriorityLandscapeManageFeature]
        public PartialViewResult DeletePriorityLandscape(PriorityLandscapePrimaryKey priorityLandscapePrimaryKey)
        {
            var priorityLandscape = priorityLandscapePrimaryKey.EntityObject;
            var viewModel = new ConfirmDialogFormViewModel(priorityLandscape.PriorityLandscapeID);
            return ViewDeletePriorityLandscape(priorityLandscape, viewModel);
        }

        private PartialViewResult ViewDeletePriorityLandscape(PriorityLandscape priorityLandscape, ConfirmDialogFormViewModel viewModel)
        {
            var canDelete = !priorityLandscape.HasDependentObjects();
            var confirmMessage = canDelete
                ? $"Are you sure you want to delete this PriorityLandscape '{priorityLandscape.PriorityLandscapeName}'?"
                : ConfirmDialogFormViewData.GetStandardCannotDeleteMessage($"<p>Washington State Department of Natural Resources has&nbsp;six upland priorityLandscape offices&nbsp;that help to&nbsp;provide localized services throughout Washington.</p>", SitkaRoute<PriorityLandscapeController>.BuildLinkFromExpression(x => x.Detail(priorityLandscape), "here"));

            var viewData = new ConfirmDialogFormViewData(confirmMessage, canDelete);
            return RazorPartialView<ConfirmDialogForm, ConfirmDialogFormViewData, ConfirmDialogFormViewModel>(viewData, viewModel);
        }

        [HttpPost]
        [PriorityLandscapeManageFeature]
        [AutomaticallyCallEntityFrameworkSaveChangesWhenModelValid]
        public ActionResult DeletePriorityLandscape(PriorityLandscapePrimaryKey priorityLandscapePrimaryKey, ConfirmDialogFormViewModel viewModel)
        {
            var priorityLandscape = priorityLandscapePrimaryKey.EntityObject;
            if (!ModelState.IsValid)
            {
                return ViewDeletePriorityLandscape(priorityLandscape, viewModel);
            }
            priorityLandscape.DeleteFull(HttpRequestStorage.DatabaseEntities);
            return new ModalDialogFormJsonResult();
        }

        [PriorityLandscapeViewFeature]
        public GridJsonNetJObjectResult<Project> ProjectsGridJsonData(PriorityLandscapePrimaryKey priorityLandscapePrimaryKey)
        {
            var treatmentTotals = HttpRequestStorage.DatabaseEntities.vTotalTreatedAcresByProjects.ToList();
            var treatmentDictionary = treatmentTotals.ToDictionary(x => x.ProjectID, y => y);
            var gridSpec = new ProjectIndexGridSpec(CurrentPerson, false, false, treatmentDictionary);
            var priorityLandscape = priorityLandscapePrimaryKey.EntityObject;
            var projectPriorityLandscapes = priorityLandscape.GetAssociatedProjectsVisibleToUser(CurrentPerson);
            var gridJsonNetJObjectResult = new GridJsonNetJObjectResult<Project>(projectPriorityLandscapes, gridSpec);
            return gridJsonNetJObjectResult;
        }
        
        [AnonymousUnclassifiedFeature]
        public PartialViewResult MapTooltip(PriorityLandscapePrimaryKey priorityLandscapePrimaryKey)
        {
            var viewData = new MapTooltipViewData(CurrentPerson, priorityLandscapePrimaryKey.EntityObject);
            return RazorPartialView<MapTooltip, MapTooltipViewData>(viewData);
        }


        [HttpGet]
        [PriorityLandscapeManageFeature]
        public PartialViewResult EditPriorityLandscape(PriorityLandscapePrimaryKey priorityLandscapePrimaryKey)
        {
            var priorityLandscape = priorityLandscapePrimaryKey.EntityObject;
            var viewModel = new EditPriorityLandscapeViewModel(priorityLandscape);
            return ViewEditPriorityLandscape(viewModel);
        }

        [HttpPost]
        [PriorityLandscapeManageFeature]
        [AutomaticallyCallEntityFrameworkSaveChangesWhenModelValid]
        public ActionResult EditPriorityLandscape(PriorityLandscapePrimaryKey priorityLandscapePrimaryKey, EditPriorityLandscapeViewModel viewModel)
        {
            var priorityLandscape = priorityLandscapePrimaryKey.EntityObject;
            if (!ModelState.IsValid)
            {
                return ViewEditPriorityLandscape(viewModel);
            }

            viewModel.UpdateModel(priorityLandscape);
            SetMessageForDisplay($"{FieldDefinition.PriorityLandscape.GetFieldDefinitionLabel()} \"{priorityLandscape.PriorityLandscapeName}\" has been updated.");
            return new ModalDialogFormJsonResult();
        }

        private PartialViewResult ViewEditPriorityLandscape(EditPriorityLandscapeViewModel viewModel)
        {
            var viewData = new EditPriorityLandscapeViewData();
            return RazorPartialView<EditPriorityLandscape, EditPriorityLandscapeViewData, EditPriorityLandscapeViewModel>(viewData, viewModel);
        }


        [HttpGet]
        [PriorityLandscapeManageFeature]
        public PartialViewResult EditPriorityLandscapeAboveMapText(PriorityLandscapePrimaryKey priorityLandscapePrimaryKey)
        {
            var priorityLandscape = priorityLandscapePrimaryKey.EntityObject;
            var viewModel = new EditPriorityLandscapeAboveMapTextViewModel(priorityLandscape);
            return ViewEditPriorityLandscapeAboveMapText(viewModel);
        }

        [HttpPost]
        [PriorityLandscapeManageFeature]
        [AutomaticallyCallEntityFrameworkSaveChangesWhenModelValid]
        public ActionResult EditPriorityLandscapeAboveMapText(PriorityLandscapePrimaryKey priorityLandscapePrimaryKey, EditPriorityLandscapeAboveMapTextViewModel viewModel)
        {
            var priorityLandscape = priorityLandscapePrimaryKey.EntityObject;
            if (!ModelState.IsValid)
            {
                return ViewEditPriorityLandscapeAboveMapText(viewModel);
            }

            viewModel.UpdateModel(priorityLandscape);
            SetMessageForDisplay($"{FieldDefinition.PriorityLandscape.GetFieldDefinitionLabel()} \"{priorityLandscape.PriorityLandscapeName}\" has been updated.");
            return new ModalDialogFormJsonResult();
        }

        private PartialViewResult ViewEditPriorityLandscapeAboveMapText(EditPriorityLandscapeAboveMapTextViewModel viewModel)
        {
            var viewData = new EditPriorityLandscapeAboveMapTextViewData();
            return RazorPartialView<EditPriorityLandscapeAboveMapText, EditPriorityLandscapeAboveMapTextViewData, EditPriorityLandscapeAboveMapTextViewModel>(viewData, viewModel);
        }


        #region  FileResources

        [HttpGet]
        [PriorityLandscapeManageFeature]
        public PartialViewResult NewPriorityLandscapeFiles(PriorityLandscapePrimaryKey priorityLandscapePrimaryKey)
        {
            Check.EnsureNotNull(priorityLandscapePrimaryKey.EntityObject);
            var viewModel = new NewFileViewModel();
            return ViewNewPriorityLandscapeFiles(viewModel);
        }

        [HttpPost]
        [PriorityLandscapeManageFeature]
        [AutomaticallyCallEntityFrameworkSaveChangesWhenModelValid]
        public ActionResult NewPriorityLandscapeFiles(PriorityLandscapePrimaryKey priorityLandscapePrimaryKey, NewFileViewModel viewModel)
        {
            var priorityLandscape = priorityLandscapePrimaryKey.EntityObject;
            if (!ModelState.IsValid)
            {
                return ViewNewPriorityLandscapeFiles(new NewFileViewModel());
            }

            viewModel.UpdateModel(priorityLandscape, CurrentPerson);
            SetMessageForDisplay($"Successfully created {viewModel.FileResourcesData.Count} new files(s) for {FieldDefinition.PriorityLandscape.GetFieldDefinitionLabel()} \"{priorityLandscape.PriorityLandscapeName}\".");
            return new ModalDialogFormJsonResult();
        }

        private PartialViewResult ViewNewPriorityLandscapeFiles(NewFileViewModel viewModel)
        {
            var viewData = new NewFileViewData(FieldDefinition.PriorityLandscape.GetFieldDefinitionLabel());
            return RazorPartialView<NewFile, NewFileViewData, NewFileViewModel>(viewData, viewModel);
        }

        [HttpGet]
        [PriorityLandscapeManageFileResourceAsAdminFeature]
        public PartialViewResult EditPriorityLandscapeFile(PriorityLandscapeFileResourcePrimaryKey priorityLandscapeFileResourcePrimaryKey)
        {
            var fileResource = priorityLandscapeFileResourcePrimaryKey.EntityObject;
            var viewModel = new EditFileResourceViewModel(fileResource);
            return ViewEditPriorityLandscapeFile(viewModel);
        }

        [HttpPost]
        [PriorityLandscapeManageFileResourceAsAdminFeature]
        [AutomaticallyCallEntityFrameworkSaveChangesWhenModelValid]
        public ActionResult EditPriorityLandscapeFile(PriorityLandscapeFileResourcePrimaryKey priorityLandscapeFileResourcePrimaryKey, EditFileResourceViewModel viewModel)
        {
            var fileResource = priorityLandscapeFileResourcePrimaryKey.EntityObject;
            if (!ModelState.IsValid)
            {
                return ViewEditPriorityLandscapeFile(viewModel);
            }

            viewModel.UpdateModel(fileResource);
            SetMessageForDisplay($"Successfully updated file \"{fileResource.DisplayName}\".");
            return new ModalDialogFormJsonResult();
        }

        private PartialViewResult ViewEditPriorityLandscapeFile(EditFileResourceViewModel viewModel)
        {
            var viewData = new EditFileResourceViewData();
            return RazorPartialView<EditFileResource, EditFileResourceViewData, EditFileResourceViewModel>(viewData, viewModel);
        }

        [HttpGet]
        [PriorityLandscapeManageFileResourceAsAdminFeature]
        public PartialViewResult DeletePriorityLandscapeFile(PriorityLandscapeFileResourcePrimaryKey priorityLandscapeFileResourcePrimaryKey)
        {
            var viewModel = new ConfirmDialogFormViewModel(priorityLandscapeFileResourcePrimaryKey.PrimaryKeyValue);
            return ViewDeletePriorityLandscapeFile(priorityLandscapeFileResourcePrimaryKey.EntityObject, viewModel);
        }

        [HttpPost]
        [PriorityLandscapeManageFileResourceAsAdminFeature]
        [AutomaticallyCallEntityFrameworkSaveChangesWhenModelValid]
        public ActionResult DeletePriorityLandscapeFile(PriorityLandscapeFileResourcePrimaryKey priorityLandscapeFileResourcePrimaryKey, ConfirmDialogFormViewModel viewModel)
        {
            var priorityLandscapeFileResource = priorityLandscapeFileResourcePrimaryKey.EntityObject;
            if (!ModelState.IsValid)
            {
                return ViewDeletePriorityLandscapeFile(priorityLandscapeFileResource, viewModel);
            }

            var message = $"{FieldDefinition.PriorityLandscape.GetFieldDefinitionLabel()} file \"{priorityLandscapeFileResource.DisplayName}\" created on '{priorityLandscapeFileResource.FileResource.CreateDate}' by '{priorityLandscapeFileResource.FileResource.CreatePerson.FullNameFirstLast}' successfully deleted.";
            priorityLandscapeFileResource.DeleteFullAndChildless(HttpRequestStorage.DatabaseEntities);
            SetMessageForDisplay(message);
            return new ModalDialogFormJsonResult();
        }

        private PartialViewResult ViewDeletePriorityLandscapeFile(PriorityLandscapeFileResource priorityLandscapeFileResource, ConfirmDialogFormViewModel viewModel)
        {
            var confirmMessage = $"Are you sure you want to delete this \"{priorityLandscapeFileResource.DisplayName}\" file created on '{priorityLandscapeFileResource.FileResource.CreateDate}' by '{priorityLandscapeFileResource.FileResource.CreatePerson.FullNameFirstLast}'?";
            var viewData = new ConfirmDialogFormViewData(confirmMessage, true);
            return RazorPartialView<ConfirmDialogForm, ConfirmDialogFormViewData, ConfirmDialogFormViewModel>(viewData, viewModel);
        }

        #endregion


    }
}