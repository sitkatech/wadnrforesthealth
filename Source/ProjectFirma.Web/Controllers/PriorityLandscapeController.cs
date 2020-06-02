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
using ProjectFirma.Web.Views.PriorityLandscape;
using ProjectFirma.Web.Views.Shared;
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
                PriorityLandscape.GetPriorityLandscapeWmsLayerGeoJson("#59ACFF", 0.2m, LayerInitialVisibility.Show)
            };

            var mapInitJson = new MapInitJson("priorityLandscapeIndex", 10, layerGeoJsons, BoundingBox.MakeNewDefaultBoundingBox());
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

            var associatedProjects = priorityLandscape.GetAssociatedProjects(CurrentPerson);
            var layers = PriorityLandscape.GetPriorityLandscapeAndAssociatedProjectLayers(priorityLandscape, associatedProjects);
            var mapInitJson = new MapInitJson(mapDivID, 10, layers, new BoundingBox(priorityLandscape.PriorityLandscapeLocation));

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
            var gridSpec = new BasicProjectInfoGridSpec(CurrentPerson, false);
            var projectPriorityLandscapes = priorityLandscapePrimaryKey.EntityObject.GetAssociatedProjects(CurrentPerson);
            var gridJsonNetJObjectResult = new GridJsonNetJObjectResult<Project>(projectPriorityLandscapes, gridSpec);
            return gridJsonNetJObjectResult;
        }
        
        [AnonymousUnclassifiedFeature]
        public PartialViewResult MapTooltip(PriorityLandscapePrimaryKey priorityLandscapePrimaryKey)
        {
            var viewData = new MapTooltipViewData(CurrentPerson, priorityLandscapePrimaryKey.EntityObject);
            return RazorPartialView<MapTooltip, MapTooltipViewData>(viewData);
        }
    }
}