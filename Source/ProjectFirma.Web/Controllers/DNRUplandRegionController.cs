/*-----------------------------------------------------------------------
<copyright file="DNRUplandRegionController.cs" company="Tahoe Regional Planning Agency and Environmental Science Associates">
Copyright (c) Tahoe Regional Planning Agency and Environmental Science Associates. All rights reserved.
<author>Environmental Science Associates</author>
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
using LtInfo.Common.MvcResults;
using MoreLinq;
using ProjectFirma.Web.Common;
using ProjectFirma.Web.Models;
using ProjectFirma.Web.Security;
using ProjectFirma.Web.Security.Shared;
using ProjectFirma.Web.Views.DNRUplandRegion;
using ProjectFirma.Web.Views.Project;
using ProjectFirma.Web.Views.Shared;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Detail = ProjectFirma.Web.Views.DNRUplandRegion.Detail;
using DetailViewData = ProjectFirma.Web.Views.DNRUplandRegion.DetailViewData;
using Index = ProjectFirma.Web.Views.DNRUplandRegion.Index;
using IndexGridSpec = ProjectFirma.Web.Views.DNRUplandRegion.IndexGridSpec;
using IndexViewData = ProjectFirma.Web.Views.DNRUplandRegion.IndexViewData;

namespace ProjectFirma.Web.Controllers
{
    public class DNRUplandRegionController : FirmaBaseController
    {
        [DNRUplandRegionViewFeature]
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

        [DNRUplandRegionViewFeature]
        public GridJsonNetJObjectResult<DNRUplandRegion> IndexGridJsonData()
        {
            var gridSpec = new IndexGridSpec(CurrentPerson);
            var regions = HttpRequestStorage.DatabaseEntities.DNRUplandRegions.OrderBy(x => x.DNRUplandRegionName).ToList();
            var gridJsonNetJObjectResult = new GridJsonNetJObjectResult<DNRUplandRegion>(regions, gridSpec);
            return gridJsonNetJObjectResult;
        }

        [DNRUplandRegionViewFeature]
        public ViewResult Detail(DNRUplandRegionPrimaryKey dnrUplandRegionPrimaryKey)
        {
            var region = dnrUplandRegionPrimaryKey.EntityObject;
            var mapDivID = $"region_{region.DNRUplandRegionID}_Map";

            var associatedProjects = region.GetAssociatedProjects(CurrentPerson);
            var layers = DNRUplandRegion.GetRegionAndAssociatedProjectLayers(region, associatedProjects);
            var mapInitJson = new MapInitJson(mapDivID, 10, layers, MapInitJson.GetExternalMapLayersForOtherMaps(), new BoundingBox(region.DNRUplandRegionLocation));

            var grantAllocationExpenditures = new List<GrantAllocationExpenditure>();
            region.GrantAllocations.ForEach(x => grantAllocationExpenditures.AddRange(x.GrantAllocationExpenditures));
            var costTypes = CostType.GetLineItemCostTypes();

            string chartTitle = $"{FieldDefinition.GrantAllocation.GetFieldDefinitionLabel()} Expenditures By Cost Type";
            var chartContainerID = chartTitle.Replace(" ", "");
            var googleChart = grantAllocationExpenditures.ToGoogleChart(x => x.CostType?.CostTypeDisplayName,
                costTypes.Select(ct => ct.CostTypeDisplayName).ToList(),
                x => x.CostType?.CostTypeDisplayName,
                chartContainerID,
                chartTitle);

            var viewGoogleChartViewData = new ViewGoogleChartViewData(googleChart, chartTitle, 405, true);

            var viewData = new DetailViewData(CurrentPerson, region, mapInitJson, viewGoogleChartViewData);
            return RazorView<Detail, DetailViewData>(viewData);
        }

        [HttpGet]
        [DNRUplandRegionManageFeature]
        public PartialViewResult DeleteRegion(DNRUplandRegionPrimaryKey dnrUplandRegionPrimaryKey)
        {
            var region = dnrUplandRegionPrimaryKey.EntityObject;
            var viewModel = new ConfirmDialogFormViewModel(region.DNRUplandRegionID);
            return ViewDeleteRegion(region, viewModel);
        }

        private PartialViewResult ViewDeleteRegion(DNRUplandRegion dnrUplandRegion, ConfirmDialogFormViewModel viewModel)
        {
            var canDelete = !dnrUplandRegion.HasDependentObjects();
            var confirmMessage = canDelete
                ? $"Are you sure you want to delete this {FieldDefinition.DNRUplandRegion.FieldDefinitionDisplayName} '{dnrUplandRegion.DNRUplandRegionName}'?"
                : ConfirmDialogFormViewData.GetStandardCannotDeleteMessage($"<p>Washington State Department of Natural Resources has&nbsp;six upland region offices&nbsp;that help to&nbsp;provide localized services throughout Washington.</p>", SitkaRoute<DNRUplandRegionController>.BuildLinkFromExpression(x => x.Detail(dnrUplandRegion), "here"));

            var viewData = new ConfirmDialogFormViewData(confirmMessage, canDelete);
            return RazorPartialView<ConfirmDialogForm, ConfirmDialogFormViewData, ConfirmDialogFormViewModel>(viewData, viewModel);
        }

        [HttpPost]
        [DNRUplandRegionManageFeature]
        [AutomaticallyCallEntityFrameworkSaveChangesWhenModelValid]
        public ActionResult DeleteRegion(DNRUplandRegionPrimaryKey dnrUplandRegionPrimaryKey, ConfirmDialogFormViewModel viewModel)
        {
            var region = dnrUplandRegionPrimaryKey.EntityObject;
            if (!ModelState.IsValid)
            {
                return ViewDeleteRegion(region, viewModel);
            }
            region.DeleteFull(HttpRequestStorage.DatabaseEntities);
            return new ModalDialogFormJsonResult();
        }

        [DNRUplandRegionViewFeature]
        public GridJsonNetJObjectResult<Project> ProjectsGridJsonData(DNRUplandRegionPrimaryKey dnrUplandRegionPrimaryKey)
        {
            var gridSpec = new ProjectInfoGridSpecForRegionDetail(CurrentPerson, false);
            var projectRegions = dnrUplandRegionPrimaryKey.EntityObject.GetAssociatedProjects(CurrentPerson);
            var gridJsonNetJObjectResult = new GridJsonNetJObjectResult<Project>(projectRegions, gridSpec);
            return gridJsonNetJObjectResult;
        }


        [DNRUplandRegionViewFeature]
        public GridJsonNetJObjectResult<FundSourceAllocation> GrantAllocationsGridJsonData(DNRUplandRegionPrimaryKey dnrUplandRegionPrimaryKey)
        {
            var gridSpec = new AssociatedGrantAllocationsGridSpec();
            var grantAllocations = dnrUplandRegionPrimaryKey.EntityObject.GetAssociatedGrantAllocations(CurrentPerson).Where(x=>x.FundSource.FundSourceStatus.GrantStatusID == FundSourceStatus.Active.GrantStatusID).OrderByDescending(x=>x.GrantAllocationPriority?.GrantAllocationPriorityNumber).ToList();
            var gridJsonNetJObjectResult = new GridJsonNetJObjectResult<FundSourceAllocation>(grantAllocations, gridSpec);
            return gridJsonNetJObjectResult;
        }

        [AnonymousUnclassifiedFeature]
        public PartialViewResult MapTooltip(DNRUplandRegionPrimaryKey dnrUplandRegionPrimaryKey)
        {
            var viewData = new MapTooltipViewData(CurrentPerson, dnrUplandRegionPrimaryKey.EntityObject);
            return RazorPartialView<MapTooltip, MapTooltipViewData>(viewData);
        }

        [HttpGet]
        [DNRUplandRegionManageFeature]
        public ActionResult EditContact(DNRUplandRegionPrimaryKey dnrUplandRegionPrimaryKey)
        {
            var region = dnrUplandRegionPrimaryKey.EntityObject;
            var viewModel = new EditContactViewModel(region);
            return ViewAddContact(viewModel, region);
        }

        [HttpPost]
        [DNRUplandRegionManageFeature]
        [AutomaticallyCallEntityFrameworkSaveChangesWhenModelValid]
        public ActionResult EditContact(DNRUplandRegionPrimaryKey dnrUplandRegionPrimaryKey, EditContactViewModel viewModel)
        {
            var region = dnrUplandRegionPrimaryKey.EntityObject;
            if (!ModelState.IsValid)
            {
                return ViewAddContact(viewModel, region);
            }

            viewModel.UpdateModel(region);

            SetMessageForDisplay($"Successfully updated {region.GetDisplayNameAsUrl()}");

            return new ModalDialogFormJsonResult();
        }

        private PartialViewResult ViewAddContact(EditContactViewModel viewModel, DNRUplandRegion region)
        {
            var viewData = new EditContactViewData(region);
            return RazorPartialView<EditContact, EditContactViewData, EditContactViewModel>(viewData, viewModel);
        }



        [HttpGet]
        [DNRUplandRegionManageFeature]
        public PartialViewResult EditPageContent(DNRUplandRegionPrimaryKey regionPrimaryKey)
        {
            var region = regionPrimaryKey.EntityObject;
            var viewModel = new EditPageContentViewModel(region);
            return ViewEditPageContent(viewModel, region);
        }

        [HttpPost]
        [DNRUplandRegionManageFeature]
        [AutomaticallyCallEntityFrameworkSaveChangesWhenModelValid]
        public ActionResult EditPageContent(DNRUplandRegionPrimaryKey regionPrimaryKey, EditPageContentViewModel viewModel)
        {
            var region = regionPrimaryKey.EntityObject;
            if (!ModelState.IsValid)
            {
                return ViewEditPageContent(viewModel, region);
            }
            viewModel.UpdateModel(region);
            return new ModalDialogFormJsonResult();
        }

        private PartialViewResult ViewEditPageContent(EditPageContentViewModel viewModel, DNRUplandRegion region)
        {
            var editorToolbar = TinyMCEExtension.TinyMCEToolbarStyle.AllOnOneRowNoMaximize;
            var viewData = new EditPageContentViewData(editorToolbar);
            return RazorPartialView<EditPageContent, EditPageContentViewData, EditPageContentViewModel>(viewData, viewModel);
        }


    }
}
