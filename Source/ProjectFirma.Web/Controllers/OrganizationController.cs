﻿/*-----------------------------------------------------------------------
<copyright file="OrganizationController.cs" company="Tahoe Regional Planning Agency and Sitka Technology Group">
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
using ProjectFirma.Web.Views.Agreement;
using ProjectFirma.Web.Views.Organization;
using ProjectFirma.Web.Views.Shared;
using Detail = ProjectFirma.Web.Views.Organization.Detail;
using DetailViewData = ProjectFirma.Web.Views.Organization.DetailViewData;
using Index = ProjectFirma.Web.Views.Organization.Index;
using IndexGridSpec = ProjectFirma.Web.Views.Organization.IndexGridSpec;
using IndexViewData = ProjectFirma.Web.Views.Organization.IndexViewData;
using Organization = ProjectFirma.Web.Models.Organization;

namespace ProjectFirma.Web.Controllers
{
    public class OrganizationController : FirmaBaseController
    {
        [OrganizationViewFeature]
        public ViewResult Index()
        {
            var firmaPage = FirmaPage.GetFirmaPageByPageType(FirmaPageType.OrganizationsList);
            var viewData = new IndexViewData(CurrentPerson, firmaPage);
            return RazorView<Index, IndexViewData>(viewData);
        }

        [OrganizationViewFeature]
        public GridJsonNetJObjectResult<Organization> IndexGridJsonData()
        {
            var hasDeleteOrganizationPermission = new OrganizationManageFeature().HasPermissionByPerson(CurrentPerson);
            var gridSpec = new IndexGridSpec(CurrentPerson, hasDeleteOrganizationPermission);
            var organizations = HttpRequestStorage.DatabaseEntities.Organizations.ToList().OrderBy(x => x.DisplayName).ToList();
            var gridJsonNetJObjectResult = new GridJsonNetJObjectResult<Organization>(organizations, gridSpec);
            return gridJsonNetJObjectResult;
        }

        [HttpGet]
        [OrganizationManageFeature]
        public PartialViewResult New()
        {
            var viewModel = new EditViewModel {IsActive = true};
            return ViewEdit(viewModel, false, null);
        }

        [HttpPost]
        [OrganizationManageFeature]
        [AutomaticallyCallEntityFrameworkSaveChangesWhenModelValid]
        public ActionResult New(EditViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return ViewEdit(viewModel, true, null);
            }
            var organization = new Organization(String.Empty, true, ModelObjectHelpers.NotYetAssignedID);
            viewModel.UpdateModel(organization, CurrentPerson);
            HttpRequestStorage.DatabaseEntities.Organizations.Add(organization);
            HttpRequestStorage.DatabaseEntities.SaveChanges();
            SetMessageForDisplay($"Organization {organization.DisplayName} successfully created.");

            return new ModalDialogFormJsonResult();
        }

        [HttpGet]
        [OrganizationManageFeature]
        public PartialViewResult Edit(OrganizationPrimaryKey organizationPrimaryKey)
        {
            var organization = organizationPrimaryKey.EntityObject;
            var viewModel = new EditViewModel(organization);
            return ViewEdit(viewModel, organization.IsInKeystone, organization.PrimaryContactPerson);
        }

        [HttpPost]
        [OrganizationManageFeature]
        [AutomaticallyCallEntityFrameworkSaveChangesWhenModelValid]
        public ActionResult Edit(OrganizationPrimaryKey organizationPrimaryKey, EditViewModel viewModel)
        {
            var organization = organizationPrimaryKey.EntityObject;
            if (!ModelState.IsValid)
            {
                return ViewEdit(viewModel, organization.IsInKeystone, organization.PrimaryContactPerson);
            }
            viewModel.UpdateModel(organization, CurrentPerson);
            return new ModalDialogFormJsonResult();
        }

        private PartialViewResult ViewEdit(EditViewModel viewModel, bool isInKeystone, Person currentPrimaryContactPerson)
        {
            var organizationTypesAsSelectListItems = HttpRequestStorage.DatabaseEntities.OrganizationTypes
                .OrderBy(x => x.OrganizationTypeName)
                .ToSelectListWithEmptyFirstRow(x => x.OrganizationTypeID.ToString(CultureInfo.InvariantCulture),
                    x => x.OrganizationTypeName);
            var activePeople = HttpRequestStorage.DatabaseEntities.People.GetActivePeople().Where(x => x.IsFullUser()).ToList();
            if (currentPrimaryContactPerson != null && !activePeople.Contains(currentPrimaryContactPerson))
            {
                activePeople.Add(currentPrimaryContactPerson);
            }
            var people = activePeople.OrderBy(x => x.FullNameLastFirst).ToSelectListWithEmptyFirstRow(x => x.PersonID.ToString(CultureInfo.InvariantCulture),
                x => x.FullNameFirstLastAndOrg);
            var isSitkaAdmin = new SitkaAdminFeature().HasPermissionByPerson(CurrentPerson);
            var viewData = new EditViewData(organizationTypesAsSelectListItems, people, isInKeystone, SitkaRoute<HelpController>.BuildUrlFromExpression(x => x.RequestOrganizationNameChange()), isSitkaAdmin);
            return RazorPartialView<Edit, EditViewData, EditViewModel>(viewData, viewModel);
        }

        [OrganizationViewFeature]
        public ViewResult Detail(OrganizationPrimaryKey organizationPrimaryKey)
        {
            var organization = organizationPrimaryKey.EntityObject;
            var expendituresDirectlyFromOrganizationViewGoogleChartViewData = GetCalendarYearExpendituresFromOrganizationFundingSourcesLineChartViewData(organization);
            var expendituresReceivedFromOtherOrganizationsViewGoogleChartViewData = GetCalendarYearExpendituresFromProjectFundingSourcesLineChartViewData(organization, CurrentPerson);

            var mapInitJson = GetMapInitJson(organization, out var hasSpatialData, CurrentPerson);

            var performanceMeasures = organization.GetAllActiveProjectsAndProposals(CurrentPerson).ToList()
                .SelectMany(x => x.PerformanceMeasureActuals)
                .Select(x => x.PerformanceMeasure).Distinct()
                .OrderBy(x => x.PerformanceMeasureDisplayName)
                .ToList();
            var agreements = GetAgreementsByOrgPeople(organization);
            var viewData = new DetailViewData(CurrentPerson, organization, mapInitJson, hasSpatialData, performanceMeasures, expendituresDirectlyFromOrganizationViewGoogleChartViewData, expendituresReceivedFromOtherOrganizationsViewGoogleChartViewData, agreements.Any(x => x.AgreementFileResourceID.HasValue));
            return RazorView<Detail, DetailViewData>(viewData);
        }

        [OrganizationViewFeature]
        public ExcelResult AgreementsExcelDownload(OrganizationPrimaryKey organizationPrimaryKey)
        {
            var organization = organizationPrimaryKey.EntityObject;
            var agreements = GetAgreementsByOrgPeople(organization);
            var workbookTitle = $"{FieldDefinition.Organization.GetFieldDefinitionLabel()} {FieldDefinition.Agreement.GetFieldDefinitionLabelPluralized()} for {organization.DisplayName}";
            return AgreementController.AgreementsExcelDownloadImpl(agreements, workbookTitle);
        }

        private static MapInitJson GetMapInitJson(Organization organization, out bool hasSpatialData, Person person)
        {
            hasSpatialData = false;
            
            var layers = new List<LayerGeoJson>();

            if (organization.OrganizationBoundary != null)
            {
                hasSpatialData = true;
                layers.Add(new LayerGeoJson("Organization Boundary",
                    organization.OrganizationBoundaryToFeatureCollection(), organization.OrganizationType?.LegendColor ?? FirmaHelpers.DefaultColorRange.First(), 1,
                    LayerInitialVisibility.Show));
            }

            var allActiveProjectsAndProposals = organization.GetAllActiveProjectsAndProposals(person).Where(x => x.ProjectStage.ShouldShowOnMap()).ToList();

            var projectsAsSimpleLocations = allActiveProjectsAndProposals.Where(x => x.ProjectLocationSimpleType != ProjectLocationSimpleType.None).ToList();
            var projectSimpleLocationsFeatureCollection = new FeatureCollection();
            projectSimpleLocationsFeatureCollection.Features.AddRange(projectsAsSimpleLocations.Select(x =>
            {
                var feature = x.MakePointFeatureWithRelevantProperties(x.ProjectLocationPoint, true, true);
                feature.Properties["FeatureColor"] = "#99b3ff";
                return feature;
            }).ToList());

            if (projectSimpleLocationsFeatureCollection.Features.Any())
            {
                hasSpatialData = true;
                layers.Add(new LayerGeoJson("Projects", projectSimpleLocationsFeatureCollection, "blue", 1, LayerInitialVisibility.Show));
            }

            var projectDetailLocationsFeatureCollection = allActiveProjectsAndProposals.SelectMany(x => x.GetProjectLocationDetails()).ToGeoJsonFeatureCollection();
            if (projectDetailLocationsFeatureCollection.Features.Any())
            {
                hasSpatialData = true;
                layers.Add(new LayerGeoJson($"{FieldDefinition.Project.GetFieldDefinitionLabel()} Detailed Mapping", projectDetailLocationsFeatureCollection, "blue", 1, LayerInitialVisibility.Hide));
            }

            var boundingBox = BoundingBox.MakeBoundingBoxFromLayerGeoJsonList(layers);
            layers.AddRange(MapInitJson.GetAllGeospatialAreaMapLayers(LayerInitialVisibility.Show));

            return new MapInitJson($"organization_{organization.OrganizationID}_Map", 10, layers, boundingBox);
        }

        private static ViewGoogleChartViewData GetCalendarYearExpendituresFromOrganizationFundingSourcesLineChartViewData(Organization organization)
        {
            var yearRange = FirmaDateUtilities.GetRangeOfYearsForReporting();
            var projectFundingSourceExpenditures =
                organization.FundingSources.SelectMany(x => x.ProjectFundingSourceExpenditures);

            var chartTitle = $"{FieldDefinition.ReportedExpenditure.GetFieldDefinitionLabelPluralized()} By {FieldDefinition.FundingSource.GetFieldDefinitionLabel()}";
            var chartContainerID = chartTitle.Replace(" ", "");
            var filterValues = organization.FundingSources.Select(x => x.FundingSourceName).ToList();
            var googleChart = projectFundingSourceExpenditures.ToGoogleChart(x => x.FundingSource.FundingSourceName,
                filterValues,
                x => x.FundingSource.FundingSourceName,
                yearRange,
                chartContainerID,
                chartTitle,
                GoogleChartType.AreaChart,
                true);

            return new ViewGoogleChartViewData(googleChart, chartTitle, 400, true);
        }

        private static ViewGoogleChartViewData GetCalendarYearExpendituresFromProjectFundingSourcesLineChartViewData(Organization organization, Person currentPerson)
        {
            var yearRange = FirmaDateUtilities.GetRangeOfYearsForReporting();

            var projects = organization.GetAllActiveProjectsAndProposalsWhereOrganizationIsStewardOrPrimaryContact(currentPerson).ToList();
            var projectFundingSourceExpenditures = projects.SelectMany(x => x.ProjectFundingSourceExpenditures).Where(x => x.FundingSource.Organization != organization);
            
            var chartTitle = $"{FieldDefinition.ReportedExpenditure.GetFieldDefinitionLabelPluralized()} By {FieldDefinition.OrganizationType.GetFieldDefinitionLabel()}";
            var chartContainerID = chartTitle.Replace(" ", "");
            var filterValues = projects.SelectMany(x => x.ProjectFundingSourceExpenditures).Where(x => x.FundingSource.Organization != organization).Select(x => x.FundingSource.Organization.OrganizationType.OrganizationTypeName).Distinct().ToList();

            var googleChart = projectFundingSourceExpenditures.ToGoogleChart(x => x.FundingSource.Organization.OrganizationType.OrganizationTypeName,
                filterValues,
                x => x.FundingSource.Organization.OrganizationType.OrganizationTypeName,
                yearRange,
                chartContainerID,
                chartTitle,
                GoogleChartType.AreaChart,
                true);

            return new ViewGoogleChartViewData(googleChart, chartTitle, 400, true);
        }

        [HttpGet]
        [OrganizationManageFeature]
        public PartialViewResult DeleteOrganization(OrganizationPrimaryKey organizationPrimaryKey)
        {
            var organization = organizationPrimaryKey.EntityObject;
            var viewModel = new ConfirmDialogFormViewModel(organization.OrganizationID);
            return ViewDeleteOrganization(organization, viewModel);
        }

        private PartialViewResult ViewDeleteOrganization(Organization organization, ConfirmDialogFormViewModel viewModel)
        {
            var projectFundingSourceExpenditureTotal = organization.FundingSources.Sum(x => x.ProjectFundingSourceExpenditures.Sum(y => y.ExpenditureAmount)).ToStringCurrency();
            var confirmMessage = $"Organization \"{organization.OrganizationName}\" is related to {organization.ProjectOrganizations.Count} projects and has {organization.FundingSources.Count} funding sources that fund a total of {projectFundingSourceExpenditureTotal} across various projects.<br /><br />Are you sure you want to delete this Organization?";
            var viewData = new ConfirmDialogFormViewData(confirmMessage, true);
            return RazorPartialView<ConfirmDialogForm, ConfirmDialogFormViewData, ConfirmDialogFormViewModel>(viewData, viewModel);
        }

        [HttpPost]
        [OrganizationManageFeature]
        [AutomaticallyCallEntityFrameworkSaveChangesWhenModelValid]
        public ActionResult DeleteOrganization(OrganizationPrimaryKey organizationPrimaryKey, ConfirmDialogFormViewModel viewModel)
        {
            var organization = organizationPrimaryKey.EntityObject;
            if (!ModelState.IsValid)
            {
                return ViewDeleteOrganization(organization, viewModel);
            }
            var message = $"Organization \"{organization.OrganizationName}\" successfully deleted.";
            organization.DeleteFull(HttpRequestStorage.DatabaseEntities);
            SetMessageForDisplay(message);

            return new ModalDialogFormJsonResult();
        }

        [OrganizationViewFeature]
        public GridJsonNetJObjectResult<Project> ProjectsIncludingLeadImplementingGridJsonData(OrganizationPrimaryKey organizationPrimaryKey)
        {
            var organization = organizationPrimaryKey.EntityObject;
            var gridSpec = new ProjectsIncludingLeadImplementingGridSpec(organization, CurrentPerson, false);            
            var gridJsonNetJObjectResult = new GridJsonNetJObjectResult<Project>(organization.GetAllActiveProjects(CurrentPerson), gridSpec);
            return gridJsonNetJObjectResult;
        }

        [OrganizationViewFeature]
        public GridJsonNetJObjectResult<Agreement> AgreementOrganizationGridJsonData(OrganizationPrimaryKey organizationPrimaryKey)
        {
            var organization = organizationPrimaryKey.EntityObject;
            var agreements = GetAgreementsByOrgPeople(organization);
            var gridSpec = new AgreementGridSpec( CurrentPerson, agreements.Any(x => x.AgreementFileResourceID.HasValue), false, false)
            {
                CustomExcelDownloadUrl = SitkaRoute<OrganizationController>.BuildUrlFromExpression(tc => tc.AgreementsExcelDownload(organizationPrimaryKey))
            };
            
            var gridJsonNetJObjectResult = new GridJsonNetJObjectResult<Agreement>(agreements, gridSpec);
            return gridJsonNetJObjectResult;
        }

        private static List<Agreement> GetAgreementsByOrgPeople(Organization organization)
        {
            var orgContacts = organization.People;
            var orgContactAgreements =
                orgContacts.SelectMany(x => x.AgreementPeople).Select(x => x.AgreementID).Distinct();
            var agreements = HttpRequestStorage.DatabaseEntities.Agreements
                .Where(a => orgContactAgreements.Contains(a.AgreementID)).OrderBy(a => a.AgreementNumber).ToList();
            return agreements;
        }

        [OrganizationViewFeature]
        public GridJsonNetJObjectResult<Project> ProposalsGridJsonData(OrganizationPrimaryKey organizationPrimaryKey)
        {
            var organization = organizationPrimaryKey.EntityObject;
            var gridSpec = new ProjectsIncludingLeadImplementingGridSpec(organization, CurrentPerson, true);
            var gridJsonNetJObjectResult = new GridJsonNetJObjectResult<Project>(organization.GetProposalsVisibleToUser(CurrentPerson), gridSpec);
            return gridJsonNetJObjectResult;
        }

        [OrganizationViewFeature]
        public GridJsonNetJObjectResult<Project> PendingProjectsGridJsonData(OrganizationPrimaryKey organizationPrimaryKey)
        {
            var organization = organizationPrimaryKey.EntityObject;
            var gridSpec = new ProjectsIncludingLeadImplementingGridSpec(organization, CurrentPerson, true);
            var gridJsonNetJObjectResult = new GridJsonNetJObjectResult<Project>(organization.GetAllPendingProjects(CurrentPerson), gridSpec);
            return gridJsonNetJObjectResult;
        }

        [OrganizationViewFeature]
        public GridJsonNetJObjectResult<ProjectFundingSourceExpenditure> ProjectFundingSourceExpendituresForOrganizationGridJsonData(OrganizationPrimaryKey organizationPrimaryKey)
        {
            var organization = organizationPrimaryKey.EntityObject;
            
            // received
            var projects = organization.GetAllActiveProjectsAndProposalsWhereOrganizationIsStewardOrPrimaryContact(CurrentPerson).ToList();
            var projectFundingSourceExpenditures = projects.SelectMany(x => x.ProjectFundingSourceExpenditures).Where(x => x.FundingSource.Organization != organization).ToList();

            // provided
            projectFundingSourceExpenditures.AddRange(organization.FundingSources.SelectMany(x => x.ProjectFundingSourceExpenditures));

            var projectFundingSourceExpendituresToShow =
                projectFundingSourceExpenditures.Where(x => x.ExpenditureAmount > 0).ToList();
            var gridSpec = new ProjectFundingSourceExpendituresForOrganizationGridSpec(organization);
            var gridJsonNetJObjectResult = new GridJsonNetJObjectResult<ProjectFundingSourceExpenditure>(projectFundingSourceExpendituresToShow, gridSpec);
            return gridJsonNetJObjectResult;
        }

        [HttpGet]
        [OrganizationManageFeature]
        public ViewResult EditBoundary(OrganizationPrimaryKey organizationPrimaryKey) {
            var viewModel = new EditBoundaryViewModel();
            var viewData = new EditBoundaryViewData(CurrentPerson, organizationPrimaryKey.EntityObject);
            return RazorView<EditBoundary, EditBoundaryViewData, EditBoundaryViewModel>(viewData, viewModel);
        }

        [HttpPost]
        [OrganizationManageFeature]
        [AutomaticallyCallEntityFrameworkSaveChangesWhenModelValid]
        public ActionResult EditBoundary(OrganizationPrimaryKey organizationPrimaryKey, EditBoundaryViewModel viewModel)
        {
            var organization = organizationPrimaryKey.EntityObject;
            if (!ModelState.IsValid)
            {
                var viewData = new EditBoundaryViewData(CurrentPerson, organization);
                return RazorPartialView<EditBoundaryErrors, EditBoundaryViewData, EditBoundaryViewModel>(viewData, viewModel);
            }

            viewModel.UpdateModel(organization);

            return RedirectToAction(new SitkaRoute<OrganizationController>(c => c.ApproveUploadGis(organizationPrimaryKey)));
        }

        [HttpGet]
        [OrganizationManageFeature]
        public PartialViewResult ApproveUploadGis(OrganizationPrimaryKey organizationPrimaryKey)
        {
            var organization = organizationPrimaryKey.EntityObject;
            var viewModel = new ApproveUploadGisViewModel();
            return ViewApproveUploadGis(viewModel, organization);
        }

        [HttpPost]
        [OrganizationManageFeature]
        [AutomaticallyCallEntityFrameworkSaveChangesWhenModelValid]
        public ActionResult ApproveUploadGis(OrganizationPrimaryKey organizationPrimaryKey, ApproveUploadGisViewModel viewModel)
        {
            var organization = organizationPrimaryKey.EntityObject;
            if (!ModelState.IsValid)
            {
                return ViewApproveUploadGis(viewModel, organization);
            }

            viewModel.UpdateModel(organization);
            HttpRequestStorage.DatabaseEntities.OrganizationBoundaryStagings.RemoveRange(organization
                .OrganizationBoundaryStagings);

            SetMessageForDisplay($"Organization Boundary for {organization.GetDisplayNameAsUrl()} successfully updated.");
            return new ContentResult();
        }
        private PartialViewResult ViewApproveUploadGis(ApproveUploadGisViewModel viewModel, Organization organization)
        {
            var layers = organization.OrganizationBoundaryStagings.Select((x, index) => new LayerGeoJson(
                x.FeatureClassName, x.ToGeoJsonFeatureCollection(),
                FirmaHelpers.DefaultColorRange[index], 0.8m,
                index == 0 ? LayerInitialVisibility.Show : LayerInitialVisibility.Hide)).ToList();
            var mapInitJson = new MapInitJson("organizationBoundaryApproveUploadGisMap", 10, layers, BoundingBox.MakeBoundingBoxFromLayerGeoJsonList(layers));

            var viewData = new ApproveUploadGisViewData(CurrentPerson, organization, mapInitJson);
            return RazorPartialView<ApproveUploadGis, ApproveUploadGisViewData, ApproveUploadGisViewModel>(viewData, viewModel);
        }

        [HttpGet]
        [OrganizationManageFeature]
        public PartialViewResult DeleteOrganizationBoundary(OrganizationPrimaryKey organizationPrimaryKey)
        {
            var organization = organizationPrimaryKey.EntityObject;
            var viewModel = new ConfirmDialogFormViewModel(organization.OrganizationID);
            return ViewDeleteOrganizationBoundary(organization, viewModel);
        }

        [HttpPost]
        [OrganizationManageFeature]
        [AutomaticallyCallEntityFrameworkSaveChangesWhenModelValid]
        public ActionResult DeleteOrganizationBoundary(OrganizationPrimaryKey organizationPrimaryKey,
            ConfirmDialogFormViewModel viewModel)
        {
            var organization = organizationPrimaryKey.EntityObject;
            if (!ModelState.IsValid)
            {
                return ViewDeleteOrganizationBoundary(organization, viewModel);
            }

            organization.OrganizationBoundary = null;

            return new ModalDialogFormJsonResult();
        }

        private PartialViewResult ViewDeleteOrganizationBoundary(Organization organization,
            ConfirmDialogFormViewModel viewModel)
        {
            var viewData = new ConfirmDialogFormViewData(
                $"Are you sure you want to delete the boundary for this {FieldDefinition.Organization.GetFieldDefinitionLabel()} '{organization.OrganizationName}'?");
            return RazorPartialView<ConfirmDialogForm, ConfirmDialogFormViewData, ConfirmDialogFormViewModel>(viewData,
                viewModel);
        }
    }
}
