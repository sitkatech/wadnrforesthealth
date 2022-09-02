/*-----------------------------------------------------------------------
<copyright file="CountiesController.cs" company="Tahoe Regional Planning Agency and Sitka Technology Group">
Copyright (c) Tahoe Regional Planning Agency and Sitka Technology Group. All rights reserved.
<author>Sitka Technology Group</author>
</copyright>

<license>
This program is free software: you can redistribute it and/or modify
it under the terms of the GNU Affero General Public License as published by
the Free Software Foundation, either version 3 of the License, or
(at your option) any later version.

This program is distributed in the hope that it will be useful,
but WITHOUT ANY WARRANTY; without even the implied warranty of
MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
GNU Affero General Public License <http://www.gnu.org/licenses/> for more details.

Source code is available upon request via <support@sitkatech.com>.
</license>
-----------------------------------------------------------------------*/
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using ProjectFirma.Web.Views.Project;
using ProjectFirma.Web.Common;
using ProjectFirma.Web.Models;
using ProjectFirma.Web.Security;
using ProjectFirma.Web.Security.Shared;
using ProjectFirma.Web.Views.Shared;
using LtInfo.Common.MvcResults;
using MoreLinq;
using NUnit.Framework;
using ProjectFirma.Web.Views.County;
using Detail = ProjectFirma.Web.Views.County.Detail;
using DetailViewData = ProjectFirma.Web.Views.County.DetailViewData;
using Index = ProjectFirma.Web.Views.County.Index;
using IndexGridSpec = ProjectFirma.Web.Views.County.IndexGridSpec;
using IndexViewData = ProjectFirma.Web.Views.County.IndexViewData;

namespace ProjectFirma.Web.Controllers
{
    public class CountyController : FirmaBaseController
    {
        [CountiesViewFeature]
        public ViewResult Index()
        {
            var layerGeoJsons = new List<LayerGeoJson>
            {
                DNRUplandRegion.GetRegionWmsLayerGeoJson("#59ACFF", 0.2m, LayerInitialVisibility.Show)
            };

            var mapInitJson = new MapInitJson("regionIndex", 10, layerGeoJsons, MapInitJson.GetExternalMapLayersForOtherMaps(), BoundingBox.MakeNewDefaultBoundingBox());
            var firmaPage = FirmaPage.GetFirmaPageByPageType(FirmaPageType.RegionsList);
            var viewData = new IndexViewData(CurrentPerson, mapInitJson, firmaPage);
            return RazorView<Index, IndexViewData>(viewData);
        }

        [CountiesViewFeature]
        public GridJsonNetJObjectResult<County> IndexGridJsonData()
        {
            var gridSpec = new IndexGridSpec(CurrentPerson);
            var countiesList = HttpRequestStorage.DatabaseEntities.Counties.OrderBy(x => x.CountyName).ToList();
            var gridJsonNetJObjectResult = new GridJsonNetJObjectResult<County>(countiesList, gridSpec);
            return gridJsonNetJObjectResult;
        }

        [CountiesViewFeature]
        public ViewResult Detail(CountyPrimaryKey countyPrimaryKey)
        {
            var county = countyPrimaryKey.EntityObject;
            var mapDivID = $"region_{county.CountyID}_Map";

            var associatedProjects = county.GetAssociatedProjects(CurrentPerson);
            var layers = County.GetRegionAndAssociatedProjectLayers(county, associatedProjects);
            var mapInitJson = new MapInitJson(mapDivID, 10, layers, MapInitJson.GetExternalMapLayersForOtherMaps(), new BoundingBox(county.CountyFeature));

            //var grantAllocationExpenditures = new List<GrantAllocationExpenditure>();
            //county.GrantAllocations.ForEach(x => grantAllocationExpenditures.AddRange(x.GrantAllocationExpenditures));
            var costTypes = CostType.GetLineItemCostTypes();

            const string chartTitle = "Grant Allocation Expenditures By Cost Type";
            var chartContainerID = chartTitle.Replace(" ", "");
            //var googleChart = grantAllocationExpenditures.ToGoogleChart(x => x.CostType?.CostTypeDisplayName,
            //    costTypes.Select(ct => ct.CostTypeDisplayName).ToList(),
            //    x => x.CostType?.CostTypeDisplayName,
            //    chartContainerID,
            //    chartTitle);

            //var viewGoogleChartViewData = new ViewGoogleChartViewData(googleChart, chartTitle, 405, true);

            var performanceMeasures = associatedProjects
                .SelectMany(x => x.PerformanceMeasureActuals)
                .Select(x => x.PerformanceMeasure).Distinct()
                .OrderBy(x => x.PerformanceMeasureDisplayName)
                .ToList();

            var viewData = new DetailViewData(CurrentPerson, county, mapInitJson, performanceMeasures);
            return RazorView<Detail, DetailViewData>(viewData);
        }

        //[HttpGet]
        //[DNRUplandRegionManageFeature]
        //public PartialViewResult DeleteRegion(CountyPrimaryKey countyPrimaryKey)
        //{
        //    var counties = countyPrimaryKey.EntityObject;
        //    var viewModel = new ConfirmDialogFormViewModel(counties.CountyID);
        //    return ViewDeleteRegion(counties, viewModel);
        //}

        //private PartialViewResult ViewDeleteRegion(DNRUplandRegion dnrUplandRegion, ConfirmDialogFormViewModel viewModel)
        //{
        //    var canDelete = !dnrUplandRegion.HasDependentObjects();
        //    var confirmMessage = canDelete
        //        ? $"Are you sure you want to delete this {FieldDefinition.DNRUplandRegion.FieldDefinitionDisplayName} '{dnrUplandRegion.DNRUplandRegionName}'?"
        //        : ConfirmDialogFormViewData.GetStandardCannotDeleteMessage($"<p>Washington State Department of Natural Resources has&nbsp;six upland region offices&nbsp;that help to&nbsp;provide localized services throughout Washington.</p>", SitkaRoute<CountiesController>.BuildLinkFromExpression(x => x.Detail(dnrUplandRegion), "here"));

        //    var viewData = new ConfirmDialogFormViewData(confirmMessage, canDelete);
        //    return RazorPartialView<ConfirmDialogForm, ConfirmDialogFormViewData, ConfirmDialogFormViewModel>(viewData, viewModel);
        //}

        //[HttpPost]
        //[DNRUplandRegionManageFeature]
        //[AutomaticallyCallEntityFrameworkSaveChangesWhenModelValid]
        //public ActionResult DeleteRegion(DNRUplandRegionPrimaryKey dnrUplandRegionPrimaryKey, ConfirmDialogFormViewModel viewModel)
        //{
        //    var region = dnrUplandRegionPrimaryKey.EntityObject;
        //    if (!ModelState.IsValid)
        //    {
        //        return ViewDeleteRegion(region, viewModel);
        //    }
        //    region.DeleteFull(HttpRequestStorage.DatabaseEntities);
        //    return new ModalDialogFormJsonResult();
        //}

        [CountiesViewFeature]
        public GridJsonNetJObjectResult<Project> ProjectsGridJsonData(CountyPrimaryKey countyPrimaryKey)
        {
            var gridSpec = new BasicProjectInfoGridSpec(CurrentPerson, false);
            var projectRegions = countyPrimaryKey.EntityObject.GetAssociatedProjects(CurrentPerson);
            var gridJsonNetJObjectResult = new GridJsonNetJObjectResult<Project>(projectRegions, gridSpec);
            return gridJsonNetJObjectResult;
        }
        
        [AnonymousUnclassifiedFeature]
        public PartialViewResult MapTooltip(CountyPrimaryKey countiesPrimaryKey)
        {
            var viewData = new MapTooltipViewData(CurrentPerson, countiesPrimaryKey.EntityObject);
            return RazorPartialView<MapTooltip, MapTooltipViewData>(viewData);
        }
    }
}
