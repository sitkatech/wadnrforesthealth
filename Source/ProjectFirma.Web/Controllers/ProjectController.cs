/*-----------------------------------------------------------------------
<copyright file="ProjectController.cs" company="Tahoe Regional Planning Agency and Environmental Science Associates">
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
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using Hangfire;
using ProjectFirma.Web.Security;
using ProjectFirma.Web.Common;
using ProjectFirma.Web.Models;
using ProjectFirma.Web.Views.Map;
using ProjectFirma.Web.Views.Project;
using ProjectFirma.Web.Views.ProjectUpdate;
using ProjectFirma.Web.Views.Shared.ExpenditureAndBudgetControls;
using ProjectFirma.Web.Views.Shared.ProjectControls;
using ProjectFirma.Web.Views.Shared.ProjectLocationControls;
using ProjectFirma.Web.Views.Tag;
using ProjectFirma.Web.Security.Shared;
using ProjectFirma.Web.Views.Shared;
using ProjectFirma.Web.Views.Shared.TextControls;
using LtInfo.Common;
using LtInfo.Common.DesignByContract;
using LtInfo.Common.ExcelWorkbookUtilities;
using LtInfo.Common.MvcResults;
using MoreLinq;
using ProjectFirma.Web.Models.ApiJson;
using ProjectFirma.Web.ScheduledJobs;
using ProjectFirma.Web.Views.GrantAllocationAward;
using ProjectFirma.Web.Views.InteractionEvent;
using ProjectFirma.Web.Views.ProjectFunding;
using ProjectFirma.Web.Views.ProjectInvoice;
using ProjectFirma.Web.Views.Shared.PerformanceMeasureControls;
using ProjectFirma.Web.Views.Shared.ProjectImportBlockList;
using ProjectFirma.Web.Views.Shared.ProjectOrganization;
using ProjectFirma.Web.Views.Shared.ProjectPerson;
using Detail = ProjectFirma.Web.Views.Project.Detail;
using DetailViewData = ProjectFirma.Web.Views.Project.DetailViewData;
using Index = ProjectFirma.Web.Views.Project.Index;
using IndexViewData = ProjectFirma.Web.Views.Project.IndexViewData;

namespace ProjectFirma.Web.Controllers
{
    public class ProjectController : FirmaBaseController
    {
        public const int LoaProgramID = 3;

        [HttpGet]
        [ProjectEditAsAdminFeature]
        public PartialViewResult Edit(ProjectPrimaryKey projectPrimaryKey)
        {
            var project = projectPrimaryKey.EntityObject;
            var latestNotApprovedUpdateBatch = project.GetLatestNotApprovedUpdateBatch();
            var viewModel = new EditProjectViewModel(project, latestNotApprovedUpdateBatch != null);
            return ViewEdit(viewModel, project, EditProjectType.ExistingProject, project.ProjectType.DisplayName, project.TotalExpenditures);
        }

        [HttpPost]
        [ProjectEditAsAdminFeature]
        [AutomaticallyCallEntityFrameworkSaveChangesWhenModelValid]
        public ActionResult Edit(ProjectPrimaryKey projectPrimaryKey, EditProjectViewModel viewModel)
        {
            var project = projectPrimaryKey.EntityObject;
            if (!ModelState.IsValid)
            {
                return ViewEdit(viewModel, project, EditProjectType.ExistingProject, project.ProjectType.DisplayName, project.TotalExpenditures);
            }
            viewModel.UpdateModel(project, CurrentPerson);
            return new ModalDialogFormJsonResult();
        }

        private PartialViewResult ViewEdit(EditProjectViewModel viewModel, Project project, EditProjectType editProjectType, string projectTypeDisplayName, decimal? totalExpenditures)
        {
            var organizations = HttpRequestStorage.DatabaseEntities.Organizations.GetActiveOrganizations();
            var projectTypes = HttpRequestStorage.DatabaseEntities.ProjectTypes.ToList().OrderBy(ap => ap.DisplayName).ToList();
            var primaryContactPeople = HttpRequestStorage.DatabaseEntities.People.OrderBy(x => x.LastName).ThenBy(x => x.FirstName);
            var defaultPrimaryContact = project?.GetPrimaryContact() ?? CurrentPerson.Organization.PrimaryContactPerson;
            var focusAreas = HttpRequestStorage.DatabaseEntities.FocusAreas.ToList();
            var projectTypeHasBeenSet = true;
            var allPrograms = HttpRequestStorage.DatabaseEntities.Programs.ToList().Select(x => new ProgramSimple(x)).ToList();
            var viewData = new EditProjectViewData(editProjectType,
                projectTypeDisplayName,
                ProjectStage.All.Except(new[] {ProjectStage.Proposed}), organizations,
                primaryContactPeople,
                defaultPrimaryContact,
                totalExpenditures,
                projectTypes,
                focusAreas,
                projectTypeHasBeenSet,
                allPrograms,
                project.ProjectID,
                project.IsProjectNameImported(),
                project.IsProjectIdentifierImported(),
                project.IsProjectInitiationDateImported(),
                project.IsCompletionDateImported(),
                project.IsProjectStageImported(),
                Models.Project.ImportedFieldWarningMessage
            );
            return RazorPartialView<EditProject, EditProjectViewData, EditProjectViewModel>(viewData, viewModel);
        }

        [CrossAreaRoute]
        [ProjectViewFeature]
        public PartialViewResult ProjectMapPopup(ProjectPrimaryKey primaryKeyProject)
        {
            var project = primaryKeyProject.EntityObject;
            return RazorPartialView<ProjectMapPopup, ProjectMapPopupViewData>(new ProjectMapPopupViewData(project, true));
        }

        [CrossAreaRoute]
        [ProjectViewFeature]
        public PartialViewResult ProjectSimpleMapPopup(ProjectPrimaryKey primaryKeyProject)
        {
            var project = primaryKeyProject.EntityObject;
            return RazorPartialView<ProjectMapPopup, ProjectMapPopupViewData>(new ProjectMapPopupViewData(project, false));
        }

        [ProjectViewFeature]
        public ViewResult Detail(ProjectPrimaryKey projectPrimaryKey)
        {
            var project = projectPrimaryKey.EntityObject;
            var activeProjectStages = GetActiveProjectStages(project);

            bool userHasProjectAdminPermissions = new FirmaAdminFeature().HasPermissionByPerson(CurrentPerson);
            bool userHasEditProjectPermissions = new ProjectEditAsAdminFeature().HasPermission(CurrentPerson, project).HasPermission;
            bool userHasProjectUpdatePermissions = new ProjectUpdateCreateEditSubmitFeature().HasPermission(CurrentPerson, project).HasPermission;
            bool userCanEditProposal = new ProjectCreateFeature().HasPermission(CurrentPerson, project).HasPermission;
            bool userHasPerformanceMeasureActualManagePermissions = new PerformanceMeasureActualFromProjectManageFeature().HasPermission(CurrentPerson, project).HasPermission;
            bool userCanViewProjectDocuments = (!CurrentPerson.IsAnonymousOrUnassigned);

            var editSimpleProjectLocationUrl = SitkaRoute<ProjectLocationController>.BuildUrlFromExpression(c => c.EditProjectLocationSimple(project));
            var editDetailedProjectLocationUrl = SitkaRoute<ProjectLocationController>.BuildUrlFromExpression(c => c.EditProjectLocationDetailed(project));
            var editProjectRegionUrl = SitkaRoute<ProjectRegionController>.BuildUrlFromExpression(c => c.EditProjectRegions(project));
            var editProjectCountyUrl =
                SitkaRoute<ProjectCountyController>.BuildUrlFromExpression(c => c.EditProjectCounties(project));
            var editProjectPriorityLandscapeUrl = SitkaRoute<ProjectPriorityLandscapeController>.BuildUrlFromExpression(c => c.EditProjectPriorityLandscapes(project));
            var editOrganizationsUrl = SitkaRoute<ProjectOrganizationController>.BuildUrlFromExpression(c => c.EditOrganizations(project));
            var editPerformanceMeasureExpectedsUrl = SitkaRoute<PerformanceMeasureExpectedController>.BuildUrlFromExpression(c => c.EditPerformanceMeasureExpectedsForProject(project));
            var editPerformanceMeasureActualsUrl = SitkaRoute<PerformanceMeasureActualController>.BuildUrlFromExpression(c => c.EditPerformanceMeasureActualsForProject(project));
            var editReportedExpendituresUrl = SitkaRoute<ProjectGrantAllocationExpenditureController>.BuildUrlFromExpression(c => c.EditProjectGrantAllocationExpendituresForProject(project));
            var editExternalLinksUrl = SitkaRoute<ProjectExternalLinkController>.BuildUrlFromExpression(c => c.EditProjectExternalLinks(project));

            var priorityLandscapes = project.GetProjectPriorityLandscapes().ToList();
            var projectLocationSummaryMapInitJson = new ProjectLocationSummaryMapInitJson(project, $"project_{project.ProjectID}_Map", false);
            var mapFormID = GenerateEditProjectLocationFormID(project);
            var regions = project.ProjectRegions.Select(x => x.DNRUplandRegion).ToList();
            var counties = project.ProjectCounties.Select(x=>x.County).ToList();
            var projectLocationSummaryViewData = new ProjectLocationSummaryViewData(project, projectLocationSummaryMapInitJson, priorityLandscapes, regions, project.NoRegionsExplanation, project.NoPriorityLandscapesExplanation, counties, project.NoCountiesExplanation);

            var taxonomyLevel = MultiTenantHelpers.GetTaxonomyLevel();
            var projectBasicsViewData = new ProjectBasicsViewData(project, false, taxonomyLevel);
            var projectBasicsTagsViewData = new ProjectBasicsTagsViewData(project, new TagHelper(project.ProjectTags.Select(x => new BootstrapTag(x.Tag)).ToList()));
            var performanceMeasureExpectedsSummaryViewData = new PerformanceMeasureExpectedSummaryViewData(new List<IPerformanceMeasureValue>(project.PerformanceMeasureExpecteds.OrderBy(x=>x.PerformanceMeasure.PerformanceMeasureSortOrder)));
            var performanceMeasureReportedValuesGroupedViewData = BuildPerformanceMeasureReportedValuesGroupedViewData(project);
            var projectExpendituresSummaryViewData = BuildProjectExpendituresDetailViewData(project);
            var projectIsLoa = project.ProjectPrograms.Any(x => x.ProgramID == LoaProgramID);
            var projectFundingDetailViewData = new ProjectFundingDetailViewData(CurrentPerson, new List<IGrantAllocationRequestAmount>(project.ProjectGrantAllocationRequests), projectIsLoa);
            var projectInvoiceDetailViewData = new ProjectInvoiceDetailViewData(CurrentPerson, project);
            var imageGalleryViewData = BuildImageGalleryViewData(project, CurrentPerson);
            var projectNotesViewData = new EntityNotesViewData(
                EntityNote.CreateFromEntityNote(new List<IEntityNote>(project.ProjectNotes)),
                SitkaRoute<ProjectNoteController>.BuildUrlFromExpression(x => x.New(project)),
                project.DisplayName,
                userHasEditProjectPermissions);
            var internalNotesViewData = new EntityNotesViewData(
                EntityNote.CreateFromEntityNote(new List<IEntityNote>(project.ProjectInternalNotes)),
                SitkaRoute<ProjectInternalNoteController>.BuildUrlFromExpression(x => x.New(project)),
                project.DisplayName,
                userHasEditProjectPermissions);
            var entityExternalLinksViewData = new EntityExternalLinksViewData(ExternalLink.CreateFromEntityExternalLink(new List<IEntityExternalLink>(project.ProjectExternalLinks)));

            var auditLogsGridSpec = new AuditLogsGridSpec {ObjectNameSingular = "Change", ObjectNamePlural = "Changes", SaveFiltersInCookie = true};
            var auditLogsGridDataUrl = SitkaRoute<ProjectController>.BuildUrlFromExpression(tc => tc.AuditLogsGridJsonData(project));

            var projectNotificationGridSpec = new ProjectNotificationGridSpec();
            const string projectNotificationGridName = "projectNotifications";
            var projectNotificationGridDataUrl = SitkaRoute<ProjectController>.BuildUrlFromExpression(tc => tc.ProjectNotificationsGridJsonData(project));

            var projectAssociatedOrganizations = project.ProjectOrganizations.Select(x => new ProjectOrganizationRelationship(project, x.Organization, x.RelationshipType)).ToList();
            var projectOrganizationsDetailViewData = new ProjectOrganizationsDetailViewData(projectAssociatedOrganizations, project.GetPrimaryContact());

            var classificationSystems = HttpRequestStorage.DatabaseEntities.ClassificationSystems.ToList();

            var treatmentGroupGridSpec = new TreatmentGroupGridSpec(CurrentPerson, project);
            var treatmentGridSpec = new TreatmentGridSpec(CurrentPerson, project);
            var treatmentAreaGridDataUrl = SitkaRoute<GrantAllocationAwardController>.BuildUrlFromExpression(tc => tc.TreatmentAreaProjectDetailGridJsonData(project));
            var treatmentGridDataUrl = SitkaRoute<GrantAllocationAwardController>.BuildUrlFromExpression(tc => tc.TreatmentProjectDetailGridJsonData(project));

            var projectInteractionEventsGridSpec = new InteractionEventGridSpec(CurrentPerson, project);
            var projectInteractionEventsGridDataUrl =
                SitkaRoute<ProjectController>.BuildUrlFromExpression(pc =>
                    pc.ProjectInteractionEventsGridJsonData(project.PrimaryKey));

            var projectPeopleDetailViewData = new ProjectPeopleDetailViewData(project.ProjectPeople.Select(x=>new ProjectPersonRelationship(project, x.Person, x.ProjectPersonRelationshipType)).ToList(), CurrentPerson);

            var viewData = new DetailViewData(CurrentPerson,
                project,
                activeProjectStages,
                projectBasicsViewData,
                projectLocationSummaryViewData,
                projectFundingDetailViewData,
                projectInvoiceDetailViewData,
                performanceMeasureExpectedsSummaryViewData,
                performanceMeasureReportedValuesGroupedViewData,
                projectExpendituresSummaryViewData,
                imageGalleryViewData,
                projectNotesViewData,
                internalNotesViewData,
                entityExternalLinksViewData,
                projectBasicsTagsViewData,
                userHasProjectAdminPermissions,
                userHasEditProjectPermissions,
                userHasProjectUpdatePermissions,
                userHasPerformanceMeasureActualManagePermissions,
                mapFormID,
                editSimpleProjectLocationUrl,
                editDetailedProjectLocationUrl,
                editOrganizationsUrl,
                editPerformanceMeasureExpectedsUrl,
                editPerformanceMeasureActualsUrl,
                editReportedExpendituresUrl,
                auditLogsGridSpec,
                auditLogsGridDataUrl,
                editExternalLinksUrl,
                projectNotificationGridSpec,
                projectNotificationGridName,
                projectNotificationGridDataUrl,
                userCanEditProposal,
                projectOrganizationsDetailViewData,
                classificationSystems,
                ProjectLocationController.EditProjectBoundingBoxFormID
                , projectPeopleDetailViewData
                , treatmentGroupGridSpec
                , treatmentAreaGridDataUrl
                , treatmentGridSpec
                , treatmentGridDataUrl
                , editProjectRegionUrl
                , editProjectCountyUrl
                ,editProjectPriorityLandscapeUrl,
                projectInteractionEventsGridSpec, projectInteractionEventsGridDataUrl, userCanViewProjectDocuments);
            return RazorView<Detail, DetailViewData>(viewData);
        }

        [ProjectViewFeature]
        public GridJsonNetJObjectResult<InteractionEvent> ProjectInteractionEventsGridJsonData(ProjectPrimaryKey projectPrimaryKey)
        {
            var project = projectPrimaryKey.EntityObject;
            var interactionEvents =
                project.InteractionEventProjects.Where(ie => ie.ProjectID == project.ProjectID).Select(ie => ie.InteractionEvent).Distinct().ToList();
            var gridSpec = new InteractionEventGridSpec(CurrentPerson, project);
            var gridJsonNetJObjectResult = new GridJsonNetJObjectResult<InteractionEvent>(interactionEvents, gridSpec);
            return gridJsonNetJObjectResult;
        }

        private static ProjectExpendituresDetailViewData BuildProjectExpendituresDetailViewData(Project project)
        {
            var projectGrantAllocationExpenditures = project.ProjectGrantAllocationExpenditures.ToList();
            var calendarYearsForGrantAllocationExpenditures = projectGrantAllocationExpenditures.CalculateCalendarYearRangeForExpenditures(project);
            var fromGrantAllocationsAndCalendarYears = GrantAllocationCalendarYearExpenditure.CreateFromGrantAllocationsAndCalendarYears(new List<IGrantAllocationExpenditure>(projectGrantAllocationExpenditures),
                calendarYearsForGrantAllocationExpenditures);
            var projectExpendituresDetailViewData = new ProjectExpendituresDetailViewData(
                fromGrantAllocationsAndCalendarYears,
                calendarYearsForGrantAllocationExpenditures.Select(x => new CalendarYearString(x)).ToList(),
                FirmaHelpers.CalculateYearRanges(project.GetExpendituresExemptReportingYears().Select(x => x.CalendarYear)),
                project.NoExpendituresToReportExplanation);
            return projectExpendituresDetailViewData;
        }

        private static PerformanceMeasureReportedValuesGroupedViewData BuildPerformanceMeasureReportedValuesGroupedViewData(Project project)
        {
            var performanceMeasureReportedValues = project.GetReportedPerformanceMeasures();
            var performanceMeasureSubcategoriesCalendarYearReportedValues =
                PerformanceMeasureSubcategoriesCalendarYearReportedValue.CreateFromPerformanceMeasuresAndCalendarYears(new List<IPerformanceMeasureReportedValue>(performanceMeasureReportedValues.OrderBy(x=>x.PerformanceMeasure.SortOrder).ThenBy(x=>x.PerformanceMeasure.DisplayName)));
            var performanceMeasureReportedValuesGroupedViewData = new PerformanceMeasureReportedValuesGroupedViewData(performanceMeasureSubcategoriesCalendarYearReportedValues,
                FirmaHelpers.CalculateYearRanges(project.GetPerformanceMeasuresExemptReportingYears().Select(x => x.CalendarYear)),
                project.PerformanceMeasureActualYearsExemptionExplanation,
                performanceMeasureReportedValues.Select(x => x.CalendarYear).Distinct().Select(x => new CalendarYearString(x)).ToList(),
                false);
            return performanceMeasureReportedValuesGroupedViewData;
        }

        private static ImageGalleryViewData BuildImageGalleryViewData(Project project, Person currentPerson)
        {
            var userCanAddPhotosToThisProject = new ProjectEditAsAdminFeature().HasPermission(currentPerson, project).HasPermission;
            var newPhotoForProjectUrl = SitkaRoute<ProjectImageController>.BuildUrlFromExpression(x => x.New(project));
            var selectKeyImageUrl = userCanAddPhotosToThisProject
                ? SitkaRoute<ProjectImageController>.BuildUrlFromExpression(x => x.SetKeyPhoto(UrlTemplate.Parameter1Int))
                : string.Empty;
            var galleryName = $"ProjectImage{project.ProjectID}";
            var imageGalleryViewData = new ImageGalleryViewData(currentPerson,
                galleryName,
                project.ProjectImages,
                userCanAddPhotosToThisProject,
                newPhotoForProjectUrl,
                selectKeyImageUrl,
                true,
                x => x.CaptionOnFullView,
                "Photo");
            return imageGalleryViewData;
        }

        private static List<ProjectStage> GetActiveProjectStages(Project project)
        {
            var activeProjectStages = new List<ProjectStage> {ProjectStage.Proposed, ProjectStage.Planned, ProjectStage.Implementation, ProjectStage.Completed};

            if (project.ProjectStage == ProjectStage.Cancelled)
            {
                activeProjectStages.Remove(ProjectStage.Implementation);
                activeProjectStages.Remove(ProjectStage.Completed);
                //activeProjectStages.Remove(ProjectStage.PostImplementation);

                activeProjectStages.Add(project.ProjectStage);
            }
            //else if (project.ProjectStage == ProjectStage.Deferred)
            //{
            //    activeProjectStages.Add(project.ProjectStage);
            //}

            activeProjectStages = activeProjectStages.OrderBy(p => p.SortOrder).ToList();
            return activeProjectStages;
        }

        public static bool FactSheetIsAvailable(Project project)
        {
            // Available for everything except cancelled. Inherited from
            // Firma, not sure if this works for us at all.
            return project.ProjectStage != ProjectStage.Cancelled;
        }

        [ProjectsViewFullListFeature]
        public ViewResult FactSheet(ProjectPrimaryKey projectPrimaryKey)
        {
            var project = projectPrimaryKey.EntityObject;
            // 4/26/24 TK - commenting this out because the app alerts spam is getting to me.
            //bool factSheetIsAvailable = FactSheetIsAvailable(project);
            //Check.Assert(factSheetIsAvailable, $"There is no Fact Sheet available for this {FieldDefinition.Project.GetFieldDefinitionLabel()}.");
            return project.IsBackwardLookingFactSheetRelevant() ? ViewBackwardLookingFactSheet(project) : ViewForwardLookingFactSheet(project);
        }

        private ViewResult ViewBackwardLookingFactSheet(Project project)
        {
            new ProjectViewFeature().DemandPermission(CurrentPerson, project);
            var mapDivID = $"project_{project.ProjectID}_Map";
            var projectLocationDetailMapInitJson = new ProjectLocationSummaryMapInitJson(project, mapDivID, false);
            var chartName = $"ProjectFactSheet{project.ProjectID}PieChart";
            var expenditureGooglePieChartSlices = project.GetExpenditureGooglePieChartSlices();
            var googleChartDataTable = GetProjectFactSheetGoogleChartDataTable(expenditureGooglePieChartSlices);
            var googleChartTitle = $"Investment by Funding Sector for: {project.ProjectName}";
            var googleChartType = GoogleChartType.PieChart;
            var googleChartConfiguration = new GooglePieChartConfiguration(googleChartTitle,
                MeasurementUnitTypeEnum.Dollars, expenditureGooglePieChartSlices, googleChartType,
                googleChartDataTable);
            var googleChartJson = new GoogleChartJson(string.Empty, chartName, googleChartConfiguration,
                googleChartType, googleChartDataTable, null);
            var firmaPageTypeFactSheetCustomText = FirmaPageType.ToType(FirmaPageTypeEnum.FactSheetCustomText);
            var firmaPageFactSheetCustomText = FirmaPage.GetFirmaPageByPageType(firmaPageTypeFactSheetCustomText);
            var viewData = new BackwardLookingFactSheetViewData(CurrentPerson, project, projectLocationDetailMapInitJson,
                googleChartJson, expenditureGooglePieChartSlices, FirmaHelpers.DefaultColorRange, firmaPageFactSheetCustomText);
            return RazorView<BackwardLookingFactSheet, BackwardLookingFactSheetViewData>(viewData);
        }

        private ViewResult ViewForwardLookingFactSheet(Project project)
        {
            new ProjectViewFeature().DemandPermission(CurrentPerson, project);
            var mapDivID = $"project_{project.ProjectID}_Map";
            var projectLocationDetailMapInitJson = new ProjectLocationSummaryMapInitJson(project, mapDivID, false);
            var chartName = $"ProjectFundingRequestSheet{project.ProjectID}PieChart";
            var grantAllocationRequestAmountGooglePieChartSlices = project.GetRequestAmountGooglePieChartSlices();
            var googleChartDataTable =
                GetProjectGrantAllocationRequestSheetGoogleChartDataTable(grantAllocationRequestAmountGooglePieChartSlices);
            var googleChartTitle = $"Funding Request by {FieldDefinition.Organization.GetFieldDefinitionLabel()} for: {project.ProjectName}";
            var googleChartType = GoogleChartType.PieChart;
            var googleChartConfiguration = new GooglePieChartConfiguration(googleChartTitle, MeasurementUnitTypeEnum.Dollars,
                grantAllocationRequestAmountGooglePieChartSlices, googleChartType, googleChartDataTable) {PieSliceText = "value"};
            var googleChartJson = new GoogleChartJson(string.Empty, chartName, googleChartConfiguration,
                googleChartType,
                googleChartDataTable, null);
            var firmaPageTypeFactSheetCustomText = FirmaPageType.ToType(FirmaPageTypeEnum.FactSheetCustomText);
            var firmaPageFactSheetCustomText = FirmaPage.GetFirmaPageByPageType(firmaPageTypeFactSheetCustomText);

            var viewData = new ForwardLookingFactSheetViewData(CurrentPerson, project, projectLocationDetailMapInitJson,
                googleChartJson, grantAllocationRequestAmountGooglePieChartSlices, firmaPageFactSheetCustomText);
            return RazorView<ForwardLookingFactSheet, ForwardLookingFactSheetViewData>(viewData);
        }

        public static GoogleChartDataTable GetProjectGrantAllocationRequestSheetGoogleChartDataTable(List<GooglePieChartSlice> grantAllocationExpenditureGooglePieChartSlices)
        {
            var googleChartColumns = new List<GoogleChartColumn> { new GoogleChartColumn( $"{FieldDefinition.GrantAllocation.GetFieldDefinitionLabel()}", GoogleChartColumnDataType.String, GoogleChartType.PieChart), new GoogleChartColumn("Expenditures", GoogleChartColumnDataType.Number, GoogleChartType.PieChart) };
            var chartRowCs = grantAllocationExpenditureGooglePieChartSlices.OrderBy(x => x.SortOrder).Select(x =>
            {
                var sectorRowV = new GoogleChartRowV(x.Label);
                var formattedValue = GoogleChartJson.GetFormattedValue(x.Value, MeasurementUnitType.Dollars);
                var expenditureRowV = new GoogleChartRowV(x.Value, formattedValue);
                return new GoogleChartRowC(new List<GoogleChartRowV> { sectorRowV, expenditureRowV });
            });
            var googleChartRowCs = new List<GoogleChartRowC>(chartRowCs);

            var googleChartDataTable = new GoogleChartDataTable(googleChartColumns, googleChartRowCs);
            return googleChartDataTable;
        }

        [ProjectsViewFullListFeature]
        public ViewResult Index()
        {
            var firmaPage = FirmaPage.GetFirmaPageByPageType(FirmaPageType.FullProjectList);
            var viewData = new IndexViewData(CurrentPerson, firmaPage);
            return RazorView<Index, IndexViewData>(viewData);
        }

        [ProjectsViewFullListFeature]
        public GridJsonNetJObjectResult<Project> IndexGridJsonData()
        {
            var treatmentTotals = HttpRequestStorage.DatabaseEntities.vTotalTreatedAcresByProjects.ToList();
            var treatmentDictionary = treatmentTotals.ToDictionary(x => x.ProjectID, y => y);
            var gridSpec = new ProjectIndexGridSpec(CurrentPerson, true, true, treatmentDictionary);
            var allProjectsVisibleToUser = GetListOfActiveProjectsVisibleToUser(CurrentPerson);
            var gridJsonNetJObjectResult = new GridJsonNetJObjectResult<Project>(allProjectsVisibleToUser, gridSpec);
            return gridJsonNetJObjectResult;
        }

        public List<Project> GetListOfActiveProjectsVisibleToUser(Person currentPerson)
        {
            var allActiveProjectsWithIncludes = HttpRequestStorage.DatabaseEntities.Projects
                .Include(x => x.PerformanceMeasureActuals)
                .Include(x => x.ProjectPrograms)
                .Include(x => x.ProjectCounties)
                .Include(x => x.ProjectRegions).Include(x => x.ProjectPriorityLandscapes)
                .Include(x => x.ProjectOrganizations).ToList()
                .GetActiveProjectsVisibleToUser(currentPerson);
            
            return allActiveProjectsWithIncludes;
        }

        [ProjectsInProposalStageViewListFeature]
        public ViewResult Proposed()
        {
            var firmaPage = FirmaPage.GetFirmaPageByPageType(FirmaPageType.Proposals);
            var viewData = new ProposedViewData(CurrentPerson, firmaPage);
            return RazorView<Proposed, ProposedViewData>(viewData);
        }

        [ProjectsInProposalStageViewListFeature]
        public GridJsonNetJObjectResult<Project> ProposedGridJsonData()
        {
            var gridSpec = new ProposalsGridSpec(CurrentPerson);
            var proposals = HttpRequestStorage.DatabaseEntities.Projects.ToList().GetProposalsVisibleToUser(CurrentPerson);
            var gridJsonNetJObjectResult = new GridJsonNetJObjectResult<Project>(proposals, gridSpec);
            return gridJsonNetJObjectResult;
        }


        [PendingProjectsViewListFeature]
        public ViewResult Pending()
        {
            var firmaPage = FirmaPage.GetFirmaPageByPageType(FirmaPageType.PendingProjects);
            var viewData = new PendingViewData(CurrentPerson, firmaPage);
            return RazorView<Pending, PendingViewData>(viewData);
        }

        [PendingProjectsViewListFeature]
        public GridJsonNetJObjectResult<Project> PendingGridJsonData()
        {
            var gridSpec = new PendingGridSpec(CurrentPerson);
            var pendingProjects = HttpRequestStorage.DatabaseEntities.Projects.ToList().GetPendingProjectsVisibleToUser(CurrentPerson);
            List<Project> filteredProposals;
            var organizationFieldDefinitionLabelPluralized = FieldDefinition.Organization.GetFieldDefinitionLabelPluralized();
            var organizationFieldDefinitionLabelSingle = FieldDefinition.Organization.GetFieldDefinitionLabel();

            var allProjectGrantAllocationExpenditures =
                HttpRequestStorage.DatabaseEntities.ProjectGrantAllocationExpenditures.ToList();
            var projectGrantAllocationExpenditureDict = allProjectGrantAllocationExpenditures.GroupBy(x => x.ProjectID).ToDictionary(x => x.Key, y => y.ToList());


            var elevatedRoles = new List<IRole> {Role.Admin, Role.EsaAdmin, Role.ProjectSteward};
            if (CurrentPerson.HasAnyOfTheseRoles(elevatedRoles))
            {
                filteredProposals = pendingProjects;
                
            }
            else
            {
                filteredProposals = pendingProjects.Where(x =>
                    {

                        return x.GetAssociatedOrganizations(organizationFieldDefinitionLabelSingle,
                                organizationFieldDefinitionLabelPluralized, projectGrantAllocationExpenditureDict).Select(y => y.Organization)
                            .Contains(CurrentPerson.Organization);
                    })
                    .ToList();
            }
            var gridJsonNetJObjectResult = new GridJsonNetJObjectResult<Project>(filteredProposals, gridSpec);
            return gridJsonNetJObjectResult;
        }

        [ProjectsViewFullListFeature]
        public ExcelResult IndexExcelDownload()
        {
            var activeProjects = HttpRequestStorage.DatabaseEntities.Projects.ToList().GetActiveProjectsVisibleToUser(CurrentPerson);
            var activeProjectsVisibleToUser = new ProjectViewFeature().FilterProjectListToThoseVisibleToUser(activeProjects, CurrentPerson).ToList();
            return FullDatabaseExcelDownloadImpl(activeProjectsVisibleToUser, FieldDefinition.Project.GetFieldDefinitionLabelPluralized());
        }

        [ProjectsViewFullListFeature]
        public ExcelResult ProposalsExcelDownload()
        {
            return FullDatabaseExcelDownloadImpl(
                HttpRequestStorage.DatabaseEntities.Projects.ToList()
                    .GetProposalsVisibleToUser(CurrentPerson),
                FieldDefinition.Application.GetFieldDefinitionLabelPluralized());
        }

        [ProjectsViewFullListFeature]
        public ExcelResult PendingExcelDownload()
        {
            return FullDatabaseExcelDownloadImpl(
                HttpRequestStorage.DatabaseEntities.Projects.ToList()
                .GetPendingProjectsVisibleToUser(CurrentPerson),
                "Pending Projects");
        }

        private ExcelResult FullDatabaseExcelDownloadImpl(List<Project> projects, string workbookTitleWithoutDate)
        {
            var projectsSpec = new ProjectExcelSpec();
            var wsProjects = ExcelWorkbookSheetDescriptorFactory.MakeWorksheet($"{FieldDefinition.Project.GetFieldDefinitionLabelPluralized()}", projectsSpec, projects);

            var workSheets = new List<IExcelWorkbookSheetDescriptor>
            {
                wsProjects
            };


            var projectsDescriptionSpec = new ProjectDescriptionExcelSpec();
            var wsProjectDescriptions = ExcelWorkbookSheetDescriptorFactory.MakeWorksheet($"{FieldDefinition.ProjectDescription.GetFieldDefinitionLabelPluralized()}", projectsDescriptionSpec, projects);
            workSheets.Add(wsProjectDescriptions);

            var organizationsSpec = new ProjectImplementingOrganizationOrProjectFundingOrganizationExcelSpec();
            var organizationFieldDefinitionLabelSingle = FieldDefinition.Organization.GetFieldDefinitionLabel();
            var organizationFieldDefinitionLabelPluralized = FieldDefinition.Organization.GetFieldDefinitionLabelPluralized();
            var allProjectGrantAllocationExpenditures =
                HttpRequestStorage.DatabaseEntities.ProjectGrantAllocationExpenditures.ToList();
            var projectGrantAllocationExpenditureDict = allProjectGrantAllocationExpenditures.GroupBy(x => x.ProjectID).ToDictionary(x => x.Key, y => y.ToList());

            var projectOrganizations = projects.SelectMany(p =>
            {
                
                return p.GetAssociatedOrganizations(organizationFieldDefinitionLabelSingle,
                        organizationFieldDefinitionLabelPluralized, projectGrantAllocationExpenditureDict);
            }).ToList();
            var wsOrganizations = ExcelWorkbookSheetDescriptorFactory.MakeWorksheet($"{FieldDefinition.Project.GetFieldDefinitionLabel()} {organizationFieldDefinitionLabelPluralized}", organizationsSpec, projectOrganizations);
            workSheets.Add(wsOrganizations);

            var projectNoteSpec = new ProjectNoteExcelSpec();
            var projectNotes = (projects.SelectMany(p => p.ProjectNotes)).ToList();
            var wsProjectNotes = ExcelWorkbookSheetDescriptorFactory.MakeWorksheet($"{FieldDefinition.ProjectNote.GetFieldDefinitionLabelPluralized()}", projectNoteSpec, projectNotes);
            workSheets.Add(wsProjectNotes);

            var performanceMeasureExpectedExcelSpec = new PerformanceMeasureExpectedExcelSpec();
            var performanceMeasureExpecteds = (projects.SelectMany(p => p.PerformanceMeasureExpecteds)).ToList();
            var wsPerformanceMeasureExpecteds = ExcelWorkbookSheetDescriptorFactory.MakeWorksheet(
                $"Expected {MultiTenantHelpers.GetPerformanceMeasureNamePluralized()}s",
                performanceMeasureExpectedExcelSpec,
                performanceMeasureExpecteds);
            workSheets.Add(wsPerformanceMeasureExpecteds);

            var performanceMeasureActualExcelSpec = new PerformanceMeasureActualExcelSpec();
            var performanceMeasureActuals = (projects.SelectMany(p => p.GetReportedPerformanceMeasures())).ToList();
            var wsPerformanceMeasureActuals = ExcelWorkbookSheetDescriptorFactory.MakeWorksheet(
                $"Reported {MultiTenantHelpers.GetPerformanceMeasureNamePluralized()}", performanceMeasureActualExcelSpec, performanceMeasureActuals);
            workSheets.Add(wsPerformanceMeasureActuals);

            var projectGrantAllocationExpenditureExcelSpec = new ProjectGrantAllocationExpenditureExcelSpec();
            var projectGrantAllocationExpenditures = (projects.SelectMany(p => p.ProjectGrantAllocationExpenditures)).ToList();
            var wsProjectGrantAllocationExpenditures = ExcelWorkbookSheetDescriptorFactory.MakeWorksheet($"{FieldDefinition.ReportedExpenditure.GetFieldDefinitionLabelPluralized()}", projectGrantAllocationExpenditureExcelSpec, projectGrantAllocationExpenditures);
            workSheets.Add(wsProjectGrantAllocationExpenditures);

            MultiTenantHelpers.GetClassificationSystems().ForEach(c =>
            {
                var projectClassificationSpec = new ProjectClassificationExcelSpec();
                var projectClassifications = projects.SelectMany(p => p.ProjectClassifications).Where(x => x.Classification.ClassificationSystem == c).ToList();
                var wsProjectClassifications = ExcelWorkbookSheetDescriptorFactory.MakeWorksheet(
                    c.ClassificationSystemNamePluralized, projectClassificationSpec, projectClassifications);
                workSheets.Add(wsProjectClassifications);
            });

            var wbm = new ExcelWorkbookMaker(workSheets);
            var excelWorkbook = wbm.ToXLWorkbook();

            return new ExcelResult(excelWorkbook, $"{workbookTitleWithoutDate}-{DateTime.Now.ToStringDateTimeForFileName()}");
        }

        [HttpGet]
        [ProjectDeleteFeature]
        public PartialViewResult DeleteProject(ProjectPrimaryKey projectPrimaryKey)
        {
            var viewModel = new ConfirmDialogFormViewModel(projectPrimaryKey.PrimaryKeyValue);
            return ViewDeleteProject(projectPrimaryKey.EntityObject, viewModel);
        }

        private PartialViewResult ViewDeleteProject(Project project, ConfirmDialogFormViewModel viewModel)
        {
            var confirmMessage = $"Are you sure you want to delete this {FieldDefinition.Project.GetFieldDefinitionLabel()} '{project.DisplayName}'?";
            var viewData = new ConfirmDialogFormViewData(confirmMessage, true);
            return RazorPartialView<ConfirmDialogForm, ConfirmDialogFormViewData, ConfirmDialogFormViewModel>(viewData, viewModel);
        }

        [HttpPost]
        [ProjectDeleteFeature]
        [AutomaticallyCallEntityFrameworkSaveChangesWhenModelValid]
        public ActionResult DeleteProject(ProjectPrimaryKey projectPrimaryKey, ConfirmDialogFormViewModel viewModel)
        {
            var project = projectPrimaryKey.EntityObject;
            if (!ModelState.IsValid)
            {
                return ViewDeleteProject(project, viewModel);
            }

            //Unlink ProjectImportBlockLists before delete
            foreach (var blockListEntry in project.ProjectImportBlockLists)
            {
                blockListEntry.ProjectID = null;
            }
            project.ProjectImportBlockLists.Clear();

            var message = $"{FieldDefinition.Project.GetFieldDefinitionLabel()} \"{project.DisplayName}\" successfully deleted.";
            project.DeleteFull(HttpRequestStorage.DatabaseEntities);
            SetMessageForDisplay(message);
            return new ModalDialogFormJsonResult();
        }

        [FirmaAdminFeature]
        public GridJsonNetJObjectResult<AuditLog> AuditLogsGridJsonData(ProjectPrimaryKey projectPrimaryKey)
        {
            var gridSpec = new AuditLogsGridSpec();
            var auditLogs = HttpRequestStorage.DatabaseEntities.AuditLogs.GetAuditLogEntriesForProject(projectPrimaryKey.EntityObject).OrderByDescending(x => x.AuditLogDate).ToList();
            var gridJsonNetJObjectResult = new GridJsonNetJObjectResult<AuditLog>(auditLogs, gridSpec);
            return gridJsonNetJObjectResult;
        }

        [AnonymousUnclassifiedFeature]
        public ActionResult Search(string searchCriteria)
        {
            if (string.IsNullOrWhiteSpace(searchCriteria))
            {
                searchCriteria = String.Empty;
            }
            var projectsFound = GetViewableProjectsFromSearchCriteria(searchCriteria);
            return SearchImpl(searchCriteria, projectsFound);
        }

        private List<Project> GetViewableProjectsFromSearchCriteria(string searchCriteria)
        {
            var projectIDsFound = HttpRequestStorage.DatabaseEntities.Projects.GetProjectFindResultsForProjectNameAndDescriptionAndNumber(searchCriteria).Select(x => x.ProjectID);
            var projectsFound =
                HttpRequestStorage.DatabaseEntities.Projects.Where(x => projectIDsFound.Contains(x.ProjectID))
                    .ToList().GetActiveProjectsAndProposalsVisibleToUser(CurrentPerson);
            return projectsFound;
        }

        [AnonymousUnclassifiedFeature]
        public ActionResult SearchImpl(string searchCriteria, List<Project> projectsFound)
        {
            if (projectsFound.Count == 1)
            {
                return RedirectToAction(new SitkaRoute<ProjectController>(x => x.Detail(projectsFound.Single())));
            }

            var viewData = new SearchResultsViewData(CurrentPerson, projectsFound, searchCriteria);
            return RazorView<SearchResults, SearchResultsViewData>(viewData);
        }

        [AnonymousUnclassifiedFeature]
        public JsonResult Find(string term)
        {
            var projectFindResults = GetViewableProjectsFromSearchCriteria(term.Trim());
            var results = projectFindResults.Take(ProjectsCountLimit).Select(p => new ListItem(p.DisplayName.ToEllipsifiedString(100), p.GetDetailUrl())).ToList();
            if (projectFindResults.Count > ProjectsCountLimit)
            {
                results.Add(
                    new ListItem(
                        $"<span style='font-weight:bold'>Displaying {ProjectsCountLimit} of {projectFindResults.Count}</span><span style='color:blue; margin-left:8px'>See All Results</span>",
                        SitkaRoute<ProjectController>.BuildUrlFromExpression(x => x.Search(term))));
            }
            return Json(results.Select(pfr => new {label = pfr.Text, value = pfr.Value}), JsonRequestBehavior.AllowGet);
        }

        private const int ProjectsCountLimit = 20;

        [ProjectManageFeaturedFeature]
        public ViewResult FeaturedList()
        {
            var firmaPage = FirmaPage.GetFirmaPageByPageType(FirmaPageType.FeaturedProjectList);
            var viewData = new FeaturedListViewData(CurrentPerson, firmaPage);
            return RazorView<FeaturedList, FeaturedListViewData>(viewData);
        }

        [ProjectManageFeaturedFeature]
        public GridJsonNetJObjectResult<Project> FeaturedListGridJsonData()
        {
            var gridSpec = new FeaturesListProjectGridSpec(CurrentPerson);
            var taxonomyBranches = HttpRequestStorage.DatabaseEntities.Projects.Where(p => p.IsFeatured).ToList().GetActiveProjectsVisibleToUser(CurrentPerson);
            var gridJsonNetJObjectResult = new GridJsonNetJObjectResult<Project>(taxonomyBranches, gridSpec);
            return gridJsonNetJObjectResult;
        }

        [HttpGet]
        [ProjectManageFeaturedFeature]
        public PartialViewResult EditFeaturedProjects()
        {
            var featuredProjectIDs = HttpRequestStorage.DatabaseEntities.Projects.Where(x => x.IsFeatured).ToList().GetActiveProjectsVisibleToUser(CurrentPerson).Select(x => x.ProjectID).ToList();
            var viewModel = new EditFeaturedProjectsViewModel(featuredProjectIDs);
            return ViewEditFeaturedProjects(viewModel);
        }

        [HttpPost]
        [ProjectManageFeaturedFeature]
        [AutomaticallyCallEntityFrameworkSaveChangesWhenModelValid]
        public ActionResult EditFeaturedProjects(EditFeaturedProjectsViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return ViewEditFeaturedProjects(viewModel);
            }
            var currentFeaturedProjects = HttpRequestStorage.DatabaseEntities.Projects.Where(x => x.IsFeatured).ToList();
            currentFeaturedProjects.ForEach(x => x.IsFeatured = false);
            if (viewModel.ProjectIDList != null)
            {
                var newlyFearturedProjects = HttpRequestStorage.DatabaseEntities.Projects.Where(x => viewModel.ProjectIDList.Contains(x.ProjectID)).ToList();
                newlyFearturedProjects.ForEach(x => x.IsFeatured = true);
            }
            return new ModalDialogFormJsonResult();
        }

        private PartialViewResult ViewEditFeaturedProjects(EditFeaturedProjectsViewModel viewModel)
        {
            var allProjects = HttpRequestStorage.DatabaseEntities.Projects.ToList().GetActiveProjectsVisibleToUser(CurrentPerson).Select(x => new ProjectSimple(x)).OrderBy(p => p.DisplayName).ToList();
            var viewData = new EditFeaturedProjectsViewData(allProjects);
            return RazorPartialView<EditFeaturedProjects, EditFeaturedProjectsViewData, EditFeaturedProjectsViewModel>(viewData, viewModel);
        }

        [ProjectsViewFullListFeature]
        public ViewResult FullProjectListSimple()
        {
            var firmaPage = FirmaPage.GetFirmaPageByPageType(FirmaPageType.FullProjectListSimple);
            var projects = HttpRequestStorage.DatabaseEntities.Projects.ToList().GetActiveProjectsVisibleToUser(CurrentPerson);
            var viewData = new FullProjectListSimpleViewData(CurrentPerson, firmaPage, projects);
            return RazorView<FullProjectListSimple, FullProjectListSimpleViewData>(viewData);
        }

        private static string GenerateEditProjectLocationFormID(Project project)
        {
            return $"editMapForProject{project.ProjectID}";
        }

        [HttpGet]
        [ProjectUpdateCreateEditSubmitFeature]
        public PartialViewResult ConfirmNonMandatoryUpdate(ProjectPrimaryKey projectPrimaryKey)
        {
            var project = projectPrimaryKey.EntityObject;
            var viewModel = new ConfirmDialogFormViewModel();
            
            var dateDisplayText = string.Empty;
            var latestUpdateSubmittalDate = project.GetLatestUpdateSubmittalDate();
            if (latestUpdateSubmittalDate.HasValue)
            {
                dateDisplayText =
                    $" on <span style='font-weight: bold'>{latestUpdateSubmittalDate.Value.ToShortDateString()}</span>";
            }

            var viewData = new ConfirmDialogFormViewData($@"
<div>
An update for this {FieldDefinition.Project.GetFieldDefinitionLabel()} was already submitted for this {
                    FieldDefinition.ReportingYear.GetFieldDefinitionLabel()
                } {dateDisplayText}. If {FieldDefinition.Project.GetFieldDefinitionLabel()} information has changed, 
any new information you'd like to provide will be added to the {
                    FieldDefinition.Project.GetFieldDefinitionLabel()
                }. Thanks for being proactive!
</div>
<div>
<hr />
Continue with a new {FieldDefinition.Project.GetFieldDefinitionLabel()} update?
</div>");
            return RazorPartialView<ConfirmDialogForm, ConfirmDialogFormViewData, ConfirmDialogFormViewModel>(viewData, viewModel);
        }

        [HttpPost]
        [ProjectUpdateCreateEditSubmitFeature]
        public ActionResult ConfirmNonMandatoryUpdate(ProjectPrimaryKey projectPrimaryKey, ConfirmDialogFormViewModel viewModel)
        {
            var project = projectPrimaryKey.EntityObject;
            return new ModalDialogFormJsonResult(project.GetProjectUpdateUrl());
        }

        [FirmaAdminFeature]
        public GridJsonNetJObjectResult<Notification> ProjectNotificationsGridJsonData(ProjectPrimaryKey projectPrimaryKey)
        {
            var project = projectPrimaryKey.EntityObject;
            var gridSpec = new ProjectNotificationGridSpec();
            var notifications = project.NotificationProjects.Select(x => x.Notification).OrderByDescending(x => x.NotificationDate).ToList();
            var gridJsonNetJObjectResult = new GridJsonNetJObjectResult<Notification>(notifications, gridSpec);
            return gridJsonNetJObjectResult;
        }

        [FirmaAdminFeature]
        public GridJsonNetJObjectResult<ProjectUpdateBatch> ProjectUpdateBatchGridJsonData(ProjectPrimaryKey projectPrimaryKey)
        {
            var project = projectPrimaryKey.EntityObject;
            var gridSpec = new ProjectUpdateBatchGridSpec();
            var gridJsonNetJObjectResult = new GridJsonNetJObjectResult<ProjectUpdateBatch>(project.ProjectUpdateBatches.OrderBy(x => x.LastUpdateDate).ToList(), gridSpec);
            return gridJsonNetJObjectResult;
        }

        public static Dictionary<int, GooglePieChartSlice> GetSlicesForGoogleChart(Dictionary<string, decimal> grantAllocationExpenditures)
        {
            var indexMapping = GetConsistentGrantAllocationExpendituresIndexDictionary(grantAllocationExpenditures);
            return grantAllocationExpenditures.Select(fund => indexMapping[fund.Key]).ToDictionary(index => index, index => new GooglePieChartSlice {Color = FirmaHelpers.DefaultColorRange[index]});
        }

        public static Dictionary<string, int> GetConsistentGrantAllocationExpendituresIndexDictionary(Dictionary<string,decimal> grantAllocationExpenditures)
        {
            var results = new Dictionary<string, int>();
            var index = 0;
            foreach (var fund in grantAllocationExpenditures)
            {
                results.Add(fund.Key, index);
                index++;
            }
            return results;
        }

        public static GoogleChartDataTable GetProjectFactSheetGoogleChartDataTable(List<GooglePieChartSlice> grantAllocationExpenditures)
        {
            var googleChartColumns = new List<GoogleChartColumn> { new GoogleChartColumn($"{FieldDefinition.GrantAllocation.GetFieldDefinitionLabel()}", GoogleChartColumnDataType.String, GoogleChartType.PieChart), new GoogleChartColumn("Expenditures", GoogleChartColumnDataType.Number, GoogleChartType.PieChart) };
            var chartRowCs = grantAllocationExpenditures.Select(x =>
            {
                var organizationTypeRowV = new GoogleChartRowV(x.Label);
                var formattedValue = GoogleChartJson.GetFormattedValue(x.Value, MeasurementUnitType.Dollars);
                var expenditureRowV = new GoogleChartRowV(x.Value, formattedValue);
                return new GoogleChartRowC(new List<GoogleChartRowV> { organizationTypeRowV, expenditureRowV });
            });
            var googleChartRowCs = new List<GoogleChartRowC>(chartRowCs);

            var googleChartDataTable = new GoogleChartDataTable(googleChartColumns, googleChartRowCs);
            return googleChartDataTable;
        }

        [LoggedInAndNotUnassignedRoleUnclassifiedFeature]
        public GridJsonNetJObjectResult<Project> MyOrganizationsProjectsGridJsonData()
        {
            var gridSpec = new BasicProjectInfoGridSpec(CurrentPerson, true);
            var organization = CurrentPerson.Organization;
            var taxonomyBranches = HttpRequestStorage.DatabaseEntities.Projects.ToList().GetActiveProjectsVisibleToUser(CurrentPerson).Where(p => organization.IsLeadImplementingOrganizationForProject(p) ||
                                                                                                                                     organization.IsProjectStewardOrganizationForProject(p)).OrderBy(x => x.DisplayName).ToList();
            var gridJsonNetJObjectResult = new GridJsonNetJObjectResult<Project>(taxonomyBranches, gridSpec);
            return gridJsonNetJObjectResult;
        }

        [ProjectsInProposalStageViewListFeature]
        public GridJsonNetJObjectResult<Project> MyOrganizationsProposalsGridJsonData()
        {
            var gridSpec = new ProposalsGridSpec(CurrentPerson);

            var proposals = HttpRequestStorage.DatabaseEntities.Projects.ToList()
                .GetProposalsVisibleToUser(CurrentPerson)
                .Where(x => x.ProposingPerson.OrganizationID == CurrentPerson.OrganizationID)
                .ToList();

            var gridJsonNetJObjectResult = new GridJsonNetJObjectResult<Project>(proposals, gridSpec);
            return gridJsonNetJObjectResult;
        }
        
        [ProjectsViewFullListFeature]
        public PartialViewResult DenyCreateProject()
        {
            var projectStewardLabel = FieldDefinition.ProjectSteward.GetFieldDefinitionLabel();
            var projectLabel = FieldDefinition.Project.GetFieldDefinitionLabel();
            var organizationLabel = FieldDefinition.Organization.GetFieldDefinitionLabel();
            var projectRelationshipTypeLabel = FieldDefinition.ProjectRelationshipType.GetFieldDefinitionLabel();

            var confirmMessage = CurrentPerson.HasRole(Role.ProjectSteward)
                ? $"Although you are a {projectStewardLabel}, you do not have the ability to create a {projectLabel} because your {organizationLabel} does not have a \"Can Steward {projectLabel}\" {projectRelationshipTypeLabel}."
                : $"You don't have permission to edit {projectLabel}.";

            var viewData = new ConfirmDialogFormViewData(confirmMessage, false);
            var viewModel = new ConfirmDialogFormViewModel();
            return RazorPartialView<ConfirmDialogForm, ConfirmDialogFormViewData, ConfirmDialogFormViewModel>(viewData, viewModel);
        }
        
        [ProjectCreateNewFeature]
        public PartialViewResult ProjectStewardCannotEdit()
        {
            var projectStewardLabel = FieldDefinition.ProjectSteward.GetFieldDefinitionLabel();
            var projectLabel = FieldDefinition.Project.GetFieldDefinitionLabel();
            var organizationLabel = FieldDefinition.Organization.GetFieldDefinitionLabel();

            var confirmMessage = CurrentPerson.HasRole(Role.ProjectSteward)
                ? $"Although you are a {projectStewardLabel}, you do not have permission to edit this {projectLabel} because it does not belong to your {organizationLabel}."
                : $"You don't have permission to edit this {projectLabel}.";

            var viewData = new ConfirmDialogFormViewData(confirmMessage, false);
            var viewModel = new ConfirmDialogFormViewModel();
            return RazorPartialView<ConfirmDialogForm, ConfirmDialogFormViewData, ConfirmDialogFormViewModel>(viewData, viewModel);
        }
        
        [ProjectCreateNewFeature]
        public PartialViewResult ProjectStewardCannotEditPendingApproval(ProjectPrimaryKey projectPrimaryKey)
        {
            var projectCreateUrl =
                SitkaRoute<ProjectCreateController>.BuildUrlFromExpression(x => x.EditBasics(projectPrimaryKey));
            var projectStewardLabel = FieldDefinition.ProjectSteward.GetFieldDefinitionLabel();
            var proposalLabel = FieldDefinition.Application.GetFieldDefinitionLabel();

            var confirmMessage = CurrentPerson.HasRole(Role.ProjectSteward)
                ? $"Although you are a {projectStewardLabel}, you do not have permission to edit this {proposalLabel} through this page because it is pending approval. You can <a href='{projectCreateUrl}'>review, edit, or approve</a> the proposal."
                : $"You don't have permission to edit this {proposalLabel}.";

            var viewData = new ConfirmDialogFormViewData(confirmMessage, false);
            var viewModel = new ConfirmDialogFormViewModel();
            return RazorPartialView<ConfirmDialogForm, ConfirmDialogFormViewData, ConfirmDialogFormViewModel>(viewData, viewModel);
        }

        [ProjectsViewFullListFeature]
        public FileContentResult FactSheetPdf(ProjectPrimaryKey projectPrimaryKey)
        {
            var project = projectPrimaryKey.EntityObject;
            using (var outputFile = new DisposableTempFile())
            {
                var pdfConversionSettings = new PDFUtility.PdfConversionSettings(new HttpCookieCollection()) { Zoom = 0.9 };
                PDFUtility.ConvertURLToPDF(
                    new Uri(new SitkaRoute<ProjectController>(c => c.FactSheet(project)).BuildAbsoluteUrlHttpsFromExpression()),
                    outputFile.FileInfo,
                    pdfConversionSettings);

                string fileContents = FileUtility.FileToString(outputFile.FileInfo);
                Check.Assert(fileContents.StartsWith("%PDF-"), "Should be a PDF file and have the starting bytes for PDF");
                Check.Assert(fileContents.Contains("wkhtmltopdf") || fileContents.Contains("\0w\0k\0h\0t\0m\0l\0t\0o\0p\0d\0f"), "Should be a PDF file produced by wkhtmltopdf.");

                var fileName = $"{project.ProjectName.ToLower().Replace(" ", "-")}-fact-sheet.pdf";
                byte[] content = System.IO.File.ReadAllBytes(outputFile.FileInfo.FullName);
                return File(content, "application/pdf", fileName);
            }
        }

        #region Project JSON API

        [ProjectsViewJsonApiFeature]
        public JsonNetJArrayResult ProjectJsonApi()
        {
            var projects = HttpRequestStorage.DatabaseEntities.Projects.ToList();
            var jsonApiProjects = ProjectApiJson.MakeProjectApiJsonsFromProjects(projects, false);
            return new JsonNetJArrayResult(jsonApiProjects);
        }

        #endregion

        [HttpGet]
        [ProgramManageFeature]
        public PartialViewResult BlockListProject(ProjectPrimaryKey projectPrimaryKey)
        {
            var project = projectPrimaryKey.EntityObject;

            var viewModel = new EditProjectImportBlockListViewModel()
            {
                ProgramID = -1,
                ProjectID = project.ProjectID,
                ProjectGisIdentifier = project.ProjectGisIdentifier,
                ProjectName = project.ProjectName
            };
            var viewData = new EditProjectImportBlockListViewData(CurrentPerson, project);
            return RazorPartialView<EditProjectImportBlockList, EditProjectImportBlockListViewData, EditProjectImportBlockListViewModel>(viewData, viewModel);
        }

        [HttpPost]
        [ProgramManageFeature]
        [AutomaticallyCallEntityFrameworkSaveChangesWhenModelValid]
        public ActionResult BlockListProject(ProjectPrimaryKey projectPrimaryKey, EditProjectImportBlockListViewModel viewModel)
        {
            var project = projectPrimaryKey.EntityObject;

            if (string.IsNullOrEmpty(viewModel.ProjectName) && string.IsNullOrEmpty(viewModel.ProjectGisIdentifier))
            {
                var validationMessage = "You must provide Project Name and/or Project GIS Identifier.";
                this.ModelState.AddModelError("Required", validationMessage);
            }

            if (!ModelState.IsValid)
            {
                return BlockListProject(project);
            }

            foreach (var projectProgram in project.ProjectPrograms)
            {
                var projectImportBlockList = new ProjectImportBlockList(projectProgram.Program)
                {
                    ProgramID = projectProgram.ProgramID,
                    ProjectGisIdentifier = viewModel.ProjectGisIdentifier,
                    ProjectName = viewModel.ProjectName,
                    ProjectID = viewModel.ProjectID,
                    Notes = viewModel.Notes
                };

                HttpRequestStorage.DatabaseEntities.ProjectImportBlockLists.Add(projectImportBlockList);
            }

            HttpRequestStorage.DatabaseEntities.SaveChanges();

            return new ModalDialogFormJsonResult();
        }

        [HttpGet]
        [ProjectDeleteFeature]
        public ContentResult BulkDeleteProjects()
        {
            return new ContentResult();
        }

        [HttpPost]
        [ProjectDeleteFeature]
        public PartialViewResult BulkDeleteProjects(BulkDeleteProjectsViewModel viewModel)
        {
            var projectDisplayNames = new List<string>();

            if (viewModel.ProjectIDList != null)
            {
                var projects = HttpRequestStorage.DatabaseEntities.Projects.Where(x => viewModel.ProjectIDList.Contains(x.ProjectID)).ToList();
                projectDisplayNames = projects.Select(x => x.DisplayName).Take(50).ToList();
            }
            var viewData = new BulkDeleteProjectsViewData(projectDisplayNames);
            return RazorPartialView<BulkDeleteProjects, BulkDeleteProjectsViewData, BulkDeleteProjectsViewModel>(viewData, viewModel);
        }

        /// <summary>
        /// Dummy get signature so that it can find the post action
        /// </summary>
        [HttpGet]
        [ProjectDeleteFeature]
        public ContentResult BulkDeleteProjectsModal()
        {
            return new ContentResult();
        }

        [HttpPost]
        [ProjectDeleteFeature]
        [AutomaticallyCallEntityFrameworkSaveChangesWhenModelValid]
        public ActionResult BulkDeleteProjectsModal(BulkDeleteProjectsViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return new ModalDialogFormJsonResult();
            }
            BulkDeleteProjectsImpl(viewModel);
            return new ModalDialogFormJsonResult();
        }

        private void BulkDeleteProjectsImpl(BulkDeleteProjectsViewModel viewModel)
        {
            if (viewModel.ProjectIDList != null)
            {

                string bulkProjectDeleteProc = "pBulkDeleteProjects";
                using (SqlConnection sqlConnection = SqlHelpers.CreateAndOpenSqlConnection())
                {
                    using (var cmd = new SqlCommand(bulkProjectDeleteProc, sqlConnection))
                    {

                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@ProjectIDList", SqlDbType.Structured);
                        cmd.Parameters["@ProjectIDList"].Direction = ParameterDirection.Input;
                        cmd.Parameters["@ProjectIDList"].TypeName = "dbo.IDList";
                        cmd.Parameters["@ProjectIDList"].Value = SqlHelpers.IntListToDataTable(viewModel.ProjectIDList);
                        cmd.ExecuteNonQuery();
                    }
                }

                SetMessageForDisplay($"Successfully deleted {viewModel.ProjectIDList.Count} {FieldDefinition.Project.GetFieldDefinitionLabelPluralized()}.");

            }
        }

    }
}
