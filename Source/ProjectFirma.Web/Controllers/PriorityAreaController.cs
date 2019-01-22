using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using GeoJSON.Net.Feature;
using LtInfo.Common.MvcResults;
using ProjectFirma.Web.Common;
using ProjectFirma.Web.Models;
using ProjectFirma.Web.Security;
using ProjectFirma.Web.Security.Shared;
using ProjectFirma.Web.Views.Project;
using ProjectFirma.Web.Views.PriorityArea;
using ProjectFirma.Web.Views.Shared;
using Detail = ProjectFirma.Web.Views.PriorityArea.Detail;
using DetailViewData = ProjectFirma.Web.Views.PriorityArea.DetailViewData;
using Index = ProjectFirma.Web.Views.PriorityArea.Index;
using IndexGridSpec = ProjectFirma.Web.Views.PriorityArea.IndexGridSpec;
using IndexViewData = ProjectFirma.Web.Views.PriorityArea.IndexViewData;

namespace ProjectFirma.Web.Controllers
{
    public class PriorityAreaController : FirmaBaseController
    {
        [PriorityAreaViewFeature]
        public ViewResult Index()
        {

            var layerGeoJsons = new List<LayerGeoJson>();
            layerGeoJsons = new List<LayerGeoJson>
            {
                PriorityArea.GetPriorityAreaWmsLayerGeoJson("#59ACFF", 0.2m, LayerInitialVisibility.Show)
            };

            var mapInitJson = new MapInitJson("priorityAreaIndex", 10, layerGeoJsons, BoundingBox.MakeNewDefaultBoundingBox());
            var firmaPage = FirmaPage.GetFirmaPageByPageType(FirmaPageType.TagList);
            var viewData = new IndexViewData(CurrentPerson, mapInitJson, firmaPage);
            return RazorView<Index, IndexViewData>(viewData);
        }


        [PriorityAreaViewFeature]
        public GridJsonNetJObjectResult<PriorityArea> IndexGridJsonData()
        {
            var gridSpec = new IndexGridSpec(CurrentPerson);
            var priorityAreas = HttpRequestStorage.DatabaseEntities.PriorityAreas.OrderBy(x => x.PriorityAreaName).ToList();
            var gridJsonNetJObjectResult = new GridJsonNetJObjectResult<PriorityArea>(priorityAreas, gridSpec);
            return gridJsonNetJObjectResult;
        }

        [PriorityAreaViewFeature]
        public ViewResult Detail(PriorityAreaPrimaryKey priorityAreaPrimaryKey)
        {
            var priorityArea = priorityAreaPrimaryKey.EntityObject;
            var mapDivID = $"priorityArea_{priorityArea.PriorityAreaID}_Map";

            var associatedProjects = priorityArea.GetAssociatedProjects(CurrentPerson);
            var layers = PriorityArea.GetPriorityAreaAndAssociatedProjectLayers(priorityArea, associatedProjects);
            var mapInitJson = new MapInitJson(mapDivID, 10, layers, new BoundingBox(priorityArea.PriorityAreaLocation));

            var projectFundingSourceExpenditures = associatedProjects.SelectMany(x => x.ProjectFundingSourceExpenditures);
            var organizationTypes = HttpRequestStorage.DatabaseEntities.OrganizationTypes.ToList();

            const string chartTitle = "Reported Expenditures By Organization Type";
            var chartContainerID = chartTitle.Replace(" ", "");
            var googleChart = projectFundingSourceExpenditures.ToGoogleChart(x => x.FundingSource.Organization.OrganizationType.OrganizationTypeName,
                organizationTypes.Select(x => x.OrganizationTypeName).ToList(),
                x => x.FundingSource.Organization.OrganizationType.OrganizationTypeName,
                chartContainerID,
                chartTitle);

            var viewGoogleChartViewData = new ViewGoogleChartViewData(googleChart, chartTitle, 405, true);

            var performanceMeasures = associatedProjects
                .SelectMany(x => x.PerformanceMeasureActuals)
                .Select(x => x.PerformanceMeasure).Distinct()
                .OrderBy(x => x.PerformanceMeasureDisplayName)
                .ToList();

            var viewData = new DetailViewData(CurrentPerson, priorityArea, mapInitJson, viewGoogleChartViewData, performanceMeasures);
            return RazorView<Detail, DetailViewData>(viewData);
        }

        [HttpGet]
        [PriorityAreaManageFeature]
        public PartialViewResult DeletePriorityArea(PriorityAreaPrimaryKey priorityAreaPrimaryKey)
        {
            var priorityArea = priorityAreaPrimaryKey.EntityObject;
            var viewModel = new ConfirmDialogFormViewModel(priorityArea.PriorityAreaID);
            return ViewDeletePriorityArea(priorityArea, viewModel);
        }

        private PartialViewResult ViewDeletePriorityArea(PriorityArea priorityArea, ConfirmDialogFormViewModel viewModel)
        {
            var canDelete = !priorityArea.HasDependentObjects();
            var confirmMessage = canDelete
                ? $"Are you sure you want to delete this PriorityArea '{priorityArea.PriorityAreaName}'?"
                : ConfirmDialogFormViewData.GetStandardCannotDeleteMessage($"<p>Washington State Department of Natural Resources has&nbsp;six upland priorityArea offices&nbsp;that help to&nbsp;provide localized services throughout Washington.</p>", SitkaRoute<PriorityAreaController>.BuildLinkFromExpression(x => x.Detail(priorityArea), "here"));

            var viewData = new ConfirmDialogFormViewData(confirmMessage, canDelete);
            return RazorPartialView<ConfirmDialogForm, ConfirmDialogFormViewData, ConfirmDialogFormViewModel>(viewData, viewModel);
        }

        [HttpPost]
        [PriorityAreaManageFeature]
        [AutomaticallyCallEntityFrameworkSaveChangesWhenModelValid]
        public ActionResult DeletePriorityArea(PriorityAreaPrimaryKey priorityAreaPrimaryKey, ConfirmDialogFormViewModel viewModel)
        {
            var priorityArea = priorityAreaPrimaryKey.EntityObject;
            if (!ModelState.IsValid)
            {
                return ViewDeletePriorityArea(priorityArea, viewModel);
            }
            priorityArea.DeleteFull(HttpRequestStorage.DatabaseEntities);
            return new ModalDialogFormJsonResult();
        }

        [PriorityAreaViewFeature]
        public GridJsonNetJObjectResult<Project> ProjectsGridJsonData(PriorityAreaPrimaryKey priorityAreaPrimaryKey)
        {
            var gridSpec = new BasicProjectInfoGridSpec(CurrentPerson, false);
            var projectPriorityAreas = priorityAreaPrimaryKey.EntityObject.GetAssociatedProjects(CurrentPerson);
            var gridJsonNetJObjectResult = new GridJsonNetJObjectResult<Project>(projectPriorityAreas, gridSpec);
            return gridJsonNetJObjectResult;
        }
        
        [AnonymousUnclassifiedFeature]
        public PartialViewResult MapTooltip(PriorityAreaPrimaryKey priorityAreaPrimaryKey)
        {
            var viewData = new MapTooltipViewData(CurrentPerson, priorityAreaPrimaryKey.EntityObject);
            return RazorPartialView<MapTooltip, MapTooltipViewData>(viewData);
        }
    }
}