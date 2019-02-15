﻿/*-----------------------------------------------------------------------
<copyright file="ProjectCreateController.cs" company="Tahoe Regional Planning Agency and Sitka Technology Group">
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
using System.Data.Entity;
using System.Data.Entity.Spatial;
using System.Linq;
using System.Web.Mvc;
using ProjectFirma.Web.Security;
using ProjectFirma.Web.Common;
using ProjectFirma.Web.Models;
using ProjectFirma.Web.Views.Map;
using ProjectFirma.Web.Views.ProjectCreate;
using ProjectFirma.Web.Views.Shared.ProjectControls;
using ProjectFirma.Web.Views.Shared.ProjectLocationControls;
using ProjectFirma.Web.Views.Shared;
using ProjectFirma.Web.Views.Shared.TextControls;
using LtInfo.Common;
using LtInfo.Common.DbSpatial;
using LtInfo.Common.DesignByContract;
using LtInfo.Common.Models;
using LtInfo.Common.MvcResults;
using ProjectFirma.Web.Views.Project;
using ProjectFirma.Web.Views.ProjectPriorityArea;
using ProjectFirma.Web.Views.ProjectRegion;
using ProjectFirma.Web.Views.Shared.ExpenditureAndBudgetControls;
using ProjectFirma.Web.Views.Shared.PerformanceMeasureControls;
using ProjectFirma.Web.Views.Shared.ProjectDocument;
using ProjectFirma.Web.Views.Shared.ProjectOrganization;
using ProjectFirma.Web.Views.Shared.ProjectPerson;
using ProjectFirma.Web.Views.Shared.SortOrder;
using Basics = ProjectFirma.Web.Views.ProjectCreate.Basics;
using BasicsViewData = ProjectFirma.Web.Views.ProjectCreate.BasicsViewData;
using BasicsViewModel = ProjectFirma.Web.Views.ProjectCreate.BasicsViewModel;
using ExpectedFunding = ProjectFirma.Web.Views.ProjectCreate.ExpectedFunding;
using ExpectedFundingViewData = ProjectFirma.Web.Views.ProjectCreate.ExpectedFundingViewData;
using ExpectedFundingViewModel = ProjectFirma.Web.Views.ProjectCreate.ExpectedFundingViewModel;
using Expenditures = ProjectFirma.Web.Views.ProjectCreate.Expenditures;
using ExpendituresViewData = ProjectFirma.Web.Views.ProjectCreate.ExpendituresViewData;
using ExpendituresViewModel = ProjectFirma.Web.Views.ProjectCreate.ExpendituresViewModel;
using LocationDetailed = ProjectFirma.Web.Views.ProjectCreate.LocationDetailed;
using LocationDetailedViewData = ProjectFirma.Web.Views.ProjectCreate.LocationDetailedViewData;
using LocationDetailedViewModel = ProjectFirma.Web.Views.ProjectCreate.LocationDetailedViewModel;
using LocationSimple = ProjectFirma.Web.Views.ProjectCreate.LocationSimple;
using LocationSimpleViewData = ProjectFirma.Web.Views.ProjectCreate.LocationSimpleViewData;
using LocationSimpleViewModel = ProjectFirma.Web.Views.ProjectCreate.LocationSimpleViewModel;
using Organizations = ProjectFirma.Web.Views.ProjectCreate.Organizations;
using OrganizationsViewData = ProjectFirma.Web.Views.ProjectCreate.OrganizationsViewData;
using OrganizationsViewModel = ProjectFirma.Web.Views.ProjectCreate.OrganizationsViewModel;
using PerformanceMeasures = ProjectFirma.Web.Views.ProjectCreate.PerformanceMeasures;
using PerformanceMeasuresViewData = ProjectFirma.Web.Views.ProjectCreate.PerformanceMeasuresViewData;
using PerformanceMeasuresViewModel = ProjectFirma.Web.Views.ProjectCreate.PerformanceMeasuresViewModel;
using Photos = ProjectFirma.Web.Views.ProjectCreate.Photos;

namespace ProjectFirma.Web.Controllers
{
    public class ProjectCreateController : FirmaBaseController
    {
        [HttpGet]
        [LoggedInAndNotUnassignedRoleUnclassifiedFeature]
        public PartialViewResult ProjectTypeSelection()
        {
            var viewData = new ProjectTypeSelectionViewData();
            var viewModel = new ProjectTypeSelectionViewModel();
            return RazorPartialView<ProjectTypeSelection, ProjectTypeSelectionViewData, ProjectTypeSelectionViewModel>(viewData, viewModel);
        }

        [HttpPost]
        [LoggedInAndNotUnassignedRoleUnclassifiedFeature]
        [AutomaticallyCallEntityFrameworkSaveChangesWhenModelValid]
        public ActionResult ProjectTypeSelection(ProjectTypeSelectionViewModel viewModel)
        {
            var viewData = new ProjectTypeSelectionViewData();

            if (!ModelState.IsValid)
            {
                return RazorPartialView<ProjectTypeSelection, ProjectTypeSelectionViewData, ProjectTypeSelectionViewModel>(viewData, viewModel);
            }

            switch (viewModel.CreateType)
            {
                case ProjectTypeSelectionViewModel.ProjectCreateType.Application:
                    return new ModalDialogFormJsonResult(
                        SitkaRoute<ProjectCreateController>.BuildUrlFromExpression(x => x.InstructionsProposal(null)));
                case ProjectTypeSelectionViewModel.ProjectCreateType.Existing:
                    return new ModalDialogFormJsonResult(
                        SitkaRoute<ProjectCreateController>.BuildUrlFromExpression(x => x.InstructionsEnterHistoric(null)));
                default:
                    throw new ArgumentException();
            }
        }

        [LoggedInAndNotUnassignedRoleUnclassifiedFeature]
        public ActionResult InstructionsProposal(int? projectID)
        {
            var firmaPageType = FirmaPageType.ToType(FirmaPageTypeEnum.ProposeProjectInstructions);
            var firmaPage = FirmaPage.GetFirmaPageByPageType(firmaPageType);

            if (projectID.HasValue)
            {
                var project = HttpRequestStorage.DatabaseEntities.Projects.GetProject(projectID.Value);
                var proposalSectionsStatus = GetProposalSectionsStatus(project);
                var viewData = new InstructionsProposalViewData(CurrentPerson, project, proposalSectionsStatus, firmaPage, false);

                return RazorView<InstructionsProposal, InstructionsProposalViewData>(viewData);
            }
            else
            {
                var viewData = new InstructionsProposalViewData(CurrentPerson, firmaPage, true);
                return RazorView<InstructionsProposal, InstructionsProposalViewData>(viewData);
            }
        }

        private static ProposalSectionsStatus GetProposalSectionsStatus(Project project)
        {
            return new ProposalSectionsStatus(project);
        }

        [LoggedInAndNotUnassignedRoleUnclassifiedFeature]
        public ActionResult InstructionsEnterHistoric(int? projectID)
        {
            var firmaPageType = FirmaPageType.ToType(FirmaPageTypeEnum.EnterHistoricProjectInstructions);
            var firmaPage = FirmaPage.GetFirmaPageByPageType(firmaPageType);

            if (projectID.HasValue)
            {
                var project = HttpRequestStorage.DatabaseEntities.Projects.GetProject(projectID.Value);
                var proposalSectionsStatus = GetProposalSectionsStatus(project);
                var viewData = new InstructionsEnterHistoricViewData(CurrentPerson, project, proposalSectionsStatus, firmaPage, false);

                return RazorView<InstructionsEnterHistoric, InstructionsEnterHistoricViewData>(viewData);
            }
            else
            {
                var viewData = new InstructionsEnterHistoricViewData(CurrentPerson, firmaPage, true);
                return RazorView<InstructionsEnterHistoric, InstructionsEnterHistoricViewData>(viewData);
            }
        }

        [HttpGet]
        [LoggedInAndNotUnassignedRoleUnclassifiedFeature]
        public ActionResult CreateAndEditBasics(bool newProjectIsProposal)
        {
            var basicsViewModel = new BasicsViewModel();
            if (newProjectIsProposal)
            {
                basicsViewModel.ProjectStageID = ProjectStage.Application.ProjectStageID;
            }
            
            return ViewCreateAndEditBasics(basicsViewModel, !newProjectIsProposal);
        }

        [HttpPost]
        [LoggedInAndNotUnassignedRoleUnclassifiedFeature]
        [AutomaticallyCallEntityFrameworkSaveChangesWhenModelValid]
        public ActionResult CreateAndEditBasics(bool newProjectIsProposal, BasicsViewModel viewModel)
        {
            return CreateAndEditBasicsPostImpl(viewModel);
        }

        private ActionResult CreateAndEditBasicsPostImpl(BasicsViewModel viewModel)
        {
            var project = new Project(viewModel.ProjectTypeID ?? ModelObjectHelpers.NotYetAssignedID,
                viewModel.ProjectStageID ?? ModelObjectHelpers.NotYetAssignedID,
                viewModel.ProjectName,
                viewModel.ProjectDescription,
                false,
                ProjectLocationSimpleType.None.ProjectLocationSimpleTypeID,
                ProjectApprovalStatus.Draft.ProjectApprovalStatusID)
            {
                ProposingPerson = CurrentPerson,
                ProposingDate = DateTime.Now
            };

            return SaveProjectAndCreateAuditEntry(project, viewModel);
        }

        private ViewResult ViewCreateAndEditBasics(BasicsViewModel viewModel, bool newProjectIsHistoric)
        {
            var projectTypes = HttpRequestStorage.DatabaseEntities.ProjectTypes;
            var instructionsPageUrl = newProjectIsHistoric
                ? SitkaRoute<ProjectCreateController>.BuildUrlFromExpression(x => x.InstructionsEnterHistoric(null))
                : SitkaRoute<ProjectCreateController>.BuildUrlFromExpression(x => x.InstructionsProposal(null));
            var viewData = new BasicsViewData(CurrentPerson, projectTypes, newProjectIsHistoric, instructionsPageUrl);

            return RazorView<Basics, BasicsViewData, BasicsViewModel>(viewData, viewModel);
        }

        [HttpGet]
        [ProjectCreateFeature]
        public ViewResult EditProjectCustomAttributes(ProjectPrimaryKey projectPrimaryKey)
        {
            var project = projectPrimaryKey.EntityObject;
            var viewModel = new CustomAttributesViewModel(project);
            return ViewEditProjectCustomAttributes(project, viewModel);
        }

        private ViewResult ViewEditProjectCustomAttributes(Project project, CustomAttributesViewModel viewModel)
        {
            var proposalSectionsStatus = GetProposalSectionsStatus(project);
            proposalSectionsStatus.IsCustomAttributesSectionComplete = ModelState.IsValid && proposalSectionsStatus.IsCustomAttributesSectionComplete;
            var projectCustomAttributeTypes = project.GetProjectCustomAttributeTypesForThisProject();
            var viewData = new CustomAttributesViewData(CurrentPerson, project, proposalSectionsStatus, projectCustomAttributeTypes);

            return RazorView<CustomAttributes, CustomAttributesViewData, CustomAttributesViewModel>(viewData, viewModel);
        }

        [HttpPost]
        [ProjectCreateFeature]
        [AutomaticallyCallEntityFrameworkSaveChangesWhenModelValid]
        public ActionResult EditProjectCustomAttributes(ProjectPrimaryKey projectPrimaryKey, CustomAttributesViewModel viewModel)
        {
            var project = projectPrimaryKey.EntityObject;
            if (!ModelState.IsValid)
            {
                return ViewEditProjectCustomAttributes(project, viewModel);
            }
            viewModel.UpdateModel(project, CurrentPerson);
            SetMessageForDisplay($"{FieldDefinition.Project.GetFieldDefinitionLabel()} {FieldDefinition.ProjectCustomAttribute.GetFieldDefinitionLabelPluralized()} successfully saved.");
            return GoToNextSection(viewModel, project, ProjectCreateSection.ProjectAttributes.ProjectCreateSectionDisplayName);
        }

        [HttpGet]
        [ProjectCreateFeature]
        public ViewResult EditBasics(ProjectPrimaryKey projectPrimaryKey)
        {
            var project = projectPrimaryKey.EntityObject;
            var viewModel = new BasicsViewModel(project);
            return ViewEditBasics(project, viewModel);
        }

        [HttpPost]
        [ProjectCreateFeature]
        [AutomaticallyCallEntityFrameworkSaveChangesWhenModelValid]
        public ActionResult EditBasics(ProjectPrimaryKey projectPrimaryKey, BasicsViewModel viewModel)
        {
            var project = projectPrimaryKey.EntityObject;
            return SaveProjectAndCreateAuditEntry(project, viewModel);
        }

        private ViewResult ViewEditBasics(Project project, BasicsViewModel viewModel)
        {
            var proposalSectionsStatus = GetProposalSectionsStatus(project);
            proposalSectionsStatus.IsBasicsSectionComplete = ModelState.IsValid && proposalSectionsStatus.IsBasicsSectionComplete;
            
            var projectTypes = HttpRequestStorage.DatabaseEntities.ProjectTypes;
            var viewData = new BasicsViewData(CurrentPerson, project, proposalSectionsStatus, projectTypes);

            return RazorView<Basics, BasicsViewData, BasicsViewModel>(viewData, viewModel);
        }

        private ActionResult SaveProjectAndCreateAuditEntry(Project project, BasicsViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                var showProjectStageDropDown = viewModel.ProjectStageID != ProjectStage.Application.ProjectStageID;
                return ModelObjectHelpers.IsRealPrimaryKeyValue(project.PrimaryKey) ? ViewEditBasics(project, viewModel) : ViewCreateAndEditBasics(viewModel, showProjectStageDropDown);
            }

            if (!ModelObjectHelpers.IsRealPrimaryKeyValue(project.PrimaryKey))
            {
                HttpRequestStorage.DatabaseEntities.Projects.Add(project);
            }

            viewModel.UpdateModel(project, CurrentPerson);

            if (project.ProjectStage == ProjectStage.Application)
            {
                DeletePerformanceMeasureActuals(project);
                foreach (var projectExemptReportingYear in project.ProjectExemptReportingYears)
                {
                    projectExemptReportingYear.DeleteFull(HttpRequestStorage.DatabaseEntities);
                }
                foreach (var projectFundingSourceExpenditure in project.ProjectFundingSourceExpenditures)
                {
                    projectFundingSourceExpenditure.DeleteFull(HttpRequestStorage.DatabaseEntities);
                }
            }

            if (project.ProjectStage == ProjectStage.Planned)
            {
                DeletePerformanceMeasureActuals(project);
                foreach (var projectExemptReportingYear in project.ProjectExemptReportingYears.Where(x => x.ProjectExemptReportingType == ProjectExemptReportingType.PerformanceMeasures))
                {
                    projectExemptReportingYear.DeleteFull(HttpRequestStorage.DatabaseEntities);
                }
            }

            HttpRequestStorage.DatabaseEntities.SaveChanges();

            var auditLog = new AuditLog(CurrentPerson,
                DateTime.Now,
                AuditLogEventType.Added,
                "Project",
                project.ProjectID,
                "ProjectID",
                project.ProjectID.ToString())
            {
                ProjectID = project.ProjectID,
                AuditDescription = $"Project: created {project.DisplayName}"
            };
            HttpRequestStorage.DatabaseEntities.AuditLogs.Add(auditLog);
            SetMessageForDisplay($"{FieldDefinition.Project.GetFieldDefinitionLabel()} successfully saved.");

            return GoToNextSection(viewModel, project, ProjectCreateSection.Basics.ProjectCreateSectionDisplayName);
        }

        [HttpGet]
        [PerformanceMeasureExpectedProposedFeature]
        public ViewResult EditExpectedPerformanceMeasureValues(ProjectPrimaryKey projectPrimaryKey)
        {
            var project = projectPrimaryKey.EntityObject;
            var viewModel = new ExpectedPerformanceMeasureValuesViewModel(project);
            return ViewEditExpectedPerformanceMeasureValues(project, viewModel);
        }

        private ViewResult ViewEditExpectedPerformanceMeasureValues(Project project, ExpectedPerformanceMeasureValuesViewModel viewModel)
        {
            var performanceMeasures = HttpRequestStorage.DatabaseEntities.PerformanceMeasures.ToList().SortByOrderThenName().ToList();
            var proposalSectionsStatus = GetProposalSectionsStatus(project);
            proposalSectionsStatus.IsPerformanceMeasureSectionComplete = ModelState.IsValid && proposalSectionsStatus.IsPerformanceMeasureSectionComplete;

            var editPerformanceMeasureExpectedsViewData = new EditPerformanceMeasureExpectedViewData(
                new List<ProjectSimple> {new ProjectSimple(project)}, performanceMeasures, project.ProjectID, false);
            var viewData = new ExpectedPerformanceMeasureValuesViewData(CurrentPerson, project, proposalSectionsStatus, editPerformanceMeasureExpectedsViewData);
            return RazorView<ExpectedPerformanceMeasureValues, ExpectedPerformanceMeasureValuesViewData, ExpectedPerformanceMeasureValuesViewModel>(viewData, viewModel);
        }

        [HttpPost]
        [PerformanceMeasureExpectedProposedFeature]
        [AutomaticallyCallEntityFrameworkSaveChangesWhenModelValid]
        public ActionResult EditExpectedPerformanceMeasureValues(ProjectPrimaryKey projectPrimaryKey, ExpectedPerformanceMeasureValuesViewModel viewModel)
        {
            var project = projectPrimaryKey.EntityObject;
            if (!ModelState.IsValid)
            {
                return ViewEditExpectedPerformanceMeasureValues(project, viewModel);
            }
            var performanceMeasureExpecteds = project.PerformanceMeasureExpecteds.ToList();

            HttpRequestStorage.DatabaseEntities.PerformanceMeasureExpecteds.Load();
            var allPerformanceMeasureExpecteds = HttpRequestStorage.DatabaseEntities.PerformanceMeasureExpecteds.Local;

            HttpRequestStorage.DatabaseEntities.PerformanceMeasureExpectedSubcategoryOptions.Load();
            var allPerformanceMeasureExpectedSubcategoryOptions = HttpRequestStorage.DatabaseEntities.PerformanceMeasureExpectedSubcategoryOptions.Local;

            viewModel.UpdateModel(performanceMeasureExpecteds, allPerformanceMeasureExpecteds, allPerformanceMeasureExpectedSubcategoryOptions, project);
            HttpRequestStorage.DatabaseEntities.SaveChanges();

            SetMessageForDisplay($"{FieldDefinition.Project.GetFieldDefinitionLabel()} {MultiTenantHelpers.GetPerformanceMeasureNamePluralized()} successfully saved.");
            return GoToNextSection(viewModel, project,ProjectCreateSection.ExpectedPerformanceMeasures.ProjectCreateSectionDisplayName);
        }

        [HttpGet]
        [ProjectCreateFeature]
        public ActionResult PerformanceMeasures(ProjectPrimaryKey projectPrimaryKey)
        {
            var project = projectPrimaryKey.EntityObject;
            if (project == null)
            {
                return RedirectToAction(new SitkaRoute<ProjectCreateController>(x => x.InstructionsProposal(project.ProjectID)));
            }
            var performanceMeasureActualSimples =
                project.PerformanceMeasureActuals.OrderBy(pam => pam.PerformanceMeasure.PerformanceMeasureSortOrder).ThenBy(x=>x.PerformanceMeasure.DisplayName)
                    .ThenByDescending(x => x.CalendarYear)
                    .Select(x => new PerformanceMeasureActualSimple(x))
                    .ToList();
            var projectExemptReportingYears = project.GetPerformanceMeasuresExemptReportingYears().Select(x => new ProjectExemptReportingYearSimple(x)).ToList();
            var currentExemptedYears = projectExemptReportingYears.Select(x => x.CalendarYear).ToList();
            var possibleYearsToExempt = project.GetProjectUpdateImplementationStartToCompletionDateRange();
            projectExemptReportingYears.AddRange(
                possibleYearsToExempt.Where(x => !currentExemptedYears.Contains(x))
                    .Select((x, index) => new ProjectExemptReportingYearSimple(-(index + 1), project.ProjectID, x)));

            var viewModel = new PerformanceMeasuresViewModel(performanceMeasureActualSimples,
                project.PerformanceMeasureActualYearsExemptionExplanation,
                projectExemptReportingYears.OrderBy(x => x.CalendarYear).ToList())
            {ProjectID = projectPrimaryKey.PrimaryKeyValue};
            return ViewPerformanceMeasures(project, viewModel);
        }

        [HttpPost]
        [ProjectCreateFeature]
        [AutomaticallyCallEntityFrameworkSaveChangesWhenModelValid]
        public ActionResult PerformanceMeasures(ProjectPrimaryKey projectPrimaryKey, PerformanceMeasuresViewModel viewModel)
        {
            var project = projectPrimaryKey.EntityObject;
            if (project == null)
            {
                return RedirectToAction(new SitkaRoute<ProjectCreateController>(x => x.InstructionsProposal(project.ProjectID)));
            }
            if (!ModelState.IsValid)
            {
                return ViewPerformanceMeasures(project, viewModel);
            }
            var performanceMeasureActuals = project.PerformanceMeasureActuals.ToList();
            HttpRequestStorage.DatabaseEntities.PerformanceMeasureActuals.Load();
            var allPerformanceMeasureActuals = HttpRequestStorage.DatabaseEntities.PerformanceMeasureActuals.Local;
            HttpRequestStorage.DatabaseEntities.PerformanceMeasureActualSubcategoryOptions.Load();
            var performanceMeasureActualSubcategoryOptions = HttpRequestStorage.DatabaseEntities.PerformanceMeasureActualSubcategoryOptions.Local;
            viewModel.UpdateModel(performanceMeasureActuals, allPerformanceMeasureActuals, performanceMeasureActualSubcategoryOptions, project);

            return GoToNextSection(viewModel, project, ProjectCreateSection.ReportedPerformanceMeasures.ProjectCreateSectionDisplayName);
        }

        private ViewResult ViewPerformanceMeasures(Project project, PerformanceMeasuresViewModel viewModel)
        {
            var performanceMeasures =
                HttpRequestStorage.DatabaseEntities.PerformanceMeasures.ToList().SortByOrderThenName().ToList();
            var showExemptYears = project.GetPerformanceMeasuresExemptReportingYears().Any() ||
                                  ModelState.Values.SelectMany(x => x.Errors)
                                      .Any(
                                          x =>
                                              x.ErrorMessage == FirmaValidationMessages.ExplanationNotNecessaryForProjectExemptYears ||
                                              x.ErrorMessage == FirmaValidationMessages.ExplanationNecessaryForProjectExemptYears);

            var performanceMeasureSubcategories = performanceMeasures.SelectMany(x => x.PerformanceMeasureSubcategories).Distinct(new HavePrimaryKeyComparer<PerformanceMeasureSubcategory>()).ToList();
            var performanceMeasureSimples = performanceMeasures.Select(x => new PerformanceMeasureSimple(x)).ToList();
            var performanceMeasureSubcategorySimples = performanceMeasureSubcategories.Select(y => new PerformanceMeasureSubcategorySimple(y)).ToList();

            var performanceMeasureSubcategoryOptionSimples = performanceMeasureSubcategories.SelectMany(y => y.PerformanceMeasureSubcategoryOptions.Select(z => new PerformanceMeasureSubcategoryOptionSimple(z))).ToList();

            var calendarYearStrings = FirmaDateUtilities.ReportingYearsForUserInput().OrderByDescending(x => x.CalendarYear).ToList();

            var viewDataForAngularEditor = new PerformanceMeasuresViewData.ViewDataForAngularEditor(project.ProjectID,
                performanceMeasureSimples,
                performanceMeasureSubcategorySimples,
                performanceMeasureSubcategoryOptionSimples,
                calendarYearStrings,
                showExemptYears);
            var proposalSectionsStatus = GetProposalSectionsStatus(project);
            var viewData =
                new PerformanceMeasuresViewData(CurrentPerson, project, viewDataForAngularEditor, proposalSectionsStatus);
            return RazorView<PerformanceMeasures, PerformanceMeasuresViewData, PerformanceMeasuresViewModel>(viewData, viewModel);
        }

        [HttpGet]
        [ProjectCreateFeature]
        public ViewResult ExpectedFunding(ProjectPrimaryKey projectPrimaryKey)
        {
            var project = projectPrimaryKey.EntityObject;
            var viewModel = new ExpectedFundingViewModel(project.ProjectFundingSourceRequests.ToList(), project.EstimatedTotalCost);
            return ViewExpectedFunding(project, viewModel);
        }

        private ViewResult ViewExpectedFunding(Project project, ExpectedFundingViewModel viewModel)
        {
            var allFundingSources = HttpRequestStorage.DatabaseEntities.FundingSources.ToList().Select(x => new FundingSourceSimple(x)).OrderBy(p => p.DisplayName).ToList();
            var estimatedTotalCost = project.EstimatedTotalCost ?? 0;
            var viewDataForAngularEditor = new ExpectedFundingViewData.ViewDataForAngularClass(project, allFundingSources, estimatedTotalCost);

            var proposalSectionsStatus = GetProposalSectionsStatus(project);
            proposalSectionsStatus.IsExpectedFundingSectionComplete = ModelState.IsValid && proposalSectionsStatus.IsPerformanceMeasureSectionComplete;

            var viewData = new ExpectedFundingViewData(CurrentPerson, project, proposalSectionsStatus, viewDataForAngularEditor);
            return RazorView<ExpectedFunding, ExpectedFundingViewData, ExpectedFundingViewModel>(viewData, viewModel);
        }

        [HttpPost]
        [ProjectCreateFeature]
        [AutomaticallyCallEntityFrameworkSaveChangesWhenModelValid]
        public ActionResult ExpectedFunding(ProjectPrimaryKey projectPrimaryKey, ExpectedFundingViewModel viewModel)
        {
            var project = projectPrimaryKey.EntityObject;
            if (!ModelState.IsValid)
            {
                return ViewExpectedFunding(project, viewModel);
            }
            HttpRequestStorage.DatabaseEntities.ProjectFundingSourceRequests.Load();
            var projectFundingSourceRequests = project.ProjectFundingSourceRequests.ToList();
            var allProjectFundingSourceRequests = HttpRequestStorage.DatabaseEntities.ProjectFundingSourceRequests.Local;
            viewModel.UpdateModel(project, projectFundingSourceRequests, allProjectFundingSourceRequests);
            SetMessageForDisplay("Expected Funding successfully saved.");
            return GoToNextSection(viewModel, project, ProjectCreateSection.ExpectedFunding.ProjectCreateSectionDisplayName);
        }

        [HttpGet]
        [ProjectCreateFeature]
        public ActionResult Expenditures(ProjectPrimaryKey projectPrimaryKey)
        {
            var project = projectPrimaryKey.EntityObject;
            
            if (project == null)
            {
                return RedirectToAction(new SitkaRoute<ProjectCreateController>(x => x.InstructionsProposal(projectPrimaryKey.PrimaryKeyValue)));
            }
            var projectFundingSourceExpenditures = project.ProjectFundingSourceExpenditures.ToList();
            var calendarYearRange = projectFundingSourceExpenditures.CalculateCalendarYearRangeForExpenditures(project);

            var projectExemptReportingYears = project.GetExpendituresExemptReportingYears().Select(x => new ProjectExemptReportingYearSimple(x)).ToList();
            var currentExemptedYears = projectExemptReportingYears.Select(x => x.CalendarYear).ToList();
            projectExemptReportingYears.AddRange(
                calendarYearRange.Where(x => !currentExemptedYears.Contains(x))
                    .Select((x, index) => new ProjectExemptReportingYearSimple(-(index + 1), project.ProjectID, x)));

            var viewModel = new ExpendituresViewModel(projectFundingSourceExpenditures, calendarYearRange, project, projectExemptReportingYears) {ProjectID = project.ProjectID};
            return ViewExpenditures(project, calendarYearRange, viewModel);
        }

        [HttpPost]
        [ProjectCreateFeature]
        [AutomaticallyCallEntityFrameworkSaveChangesWhenModelValid]
        public ActionResult Expenditures(ProjectPrimaryKey projectPrimaryKey, ExpendituresViewModel viewModel)
        {
            var project = projectPrimaryKey.EntityObject;
            
            if (project == null)
            {
                return RedirectToAction(new SitkaRoute<ProjectCreateController>(x => x.InstructionsProposal(projectPrimaryKey.PrimaryKeyValue)));
            }

            viewModel.ProjectID = project.ProjectID;

            var projectFundingSourceExpenditureUpdates = project.ProjectFundingSourceExpenditures.ToList();
            var calendarYearRange = projectFundingSourceExpenditureUpdates.CalculateCalendarYearRangeForExpenditures(project);
            if (!ModelState.IsValid)
            {
                return ViewExpenditures(project, calendarYearRange, viewModel);
            }
            HttpRequestStorage.DatabaseEntities.ProjectFundingSourceExpenditureUpdates.Load();
            var allProjectFundingSourceExpenditures = HttpRequestStorage.DatabaseEntities.ProjectFundingSourceExpenditures.Local;
            viewModel.UpdateModel(project, projectFundingSourceExpenditureUpdates, allProjectFundingSourceExpenditures);            

            return GoToNextSection(viewModel, project, ProjectCreateSection.ReportedExpenditures.ProjectCreateSectionDisplayName);
        }

        private ViewResult ViewExpenditures(Project project, List<int> calendarYearRange, ExpendituresViewModel viewModel)
        {
            var allFundingSources = HttpRequestStorage.DatabaseEntities.FundingSources.ToList().Select(x => new FundingSourceSimple(x)).OrderBy(p => p.DisplayName).ToList();
            var expendituresExemptReportingYears = project.GetExpendituresExemptReportingYears();
            var showNoExpendituresExplanation = expendituresExemptReportingYears.Any();
            var viewDataForAngularEditor = new ExpendituresViewData.ViewDataForAngularClass(project,
                allFundingSources,
                calendarYearRange, showNoExpendituresExplanation);
            var projectFundingSourceExpenditures = project.ProjectFundingSourceExpenditures.ToList();
            var fromFundingSourcesAndCalendarYears = FundingSourceCalendarYearExpenditure.CreateFromFundingSourcesAndCalendarYears(
                new List<IFundingSourceExpenditure>(projectFundingSourceExpenditures),
                calendarYearRange);
            var projectExpendituresSummaryViewData = new ProjectExpendituresDetailViewData(
                fromFundingSourcesAndCalendarYears, calendarYearRange.Select(x => new CalendarYearString(x)).ToList(),
                FirmaHelpers.CalculateYearRanges(expendituresExemptReportingYears.Select(x => x.CalendarYear)),
                project.NoExpendituresToReportExplanation);
            var proposalSectionsStatus = GetProposalSectionsStatus(project);
            var viewData = new ExpendituresViewData(CurrentPerson, project, viewDataForAngularEditor, projectExpendituresSummaryViewData, proposalSectionsStatus);
            return RazorView<Expenditures, ExpendituresViewData, ExpendituresViewModel>(viewData, viewModel);
        }

        [HttpGet]
        [ProjectCreateFeature]
        public ViewResult EditClassifications(ProjectPrimaryKey projectPrimaryKey)
        {
            var project = projectPrimaryKey.EntityObject;
            var projectClassificationSimples = GetProjectClassificationSimples(project);
            var viewModel = new EditProposalClassificationsViewModel(projectClassificationSimples);
            return ViewEditClassifications(project, viewModel);
        }

        public static List<ProjectClassificationSimple> GetProjectClassificationSimples(Project project)
        {
            var selectedProjectClassifications = project.ProjectClassifications;

            var projectClassificationSimples =
                HttpRequestStorage.DatabaseEntities.ClassificationSystems.OrderBy(x => x.ClassificationSystemName).SelectMany(x => x.Classifications).OrderBy(x => x.DisplayName).Select(x => new ProjectClassificationSimple
                {
                    ClassificationID = x.ClassificationID,
                    ClassificationSystemID = x.ClassificationSystemID,
                    ProjectID = project.ProjectID
                }).ToList();
 
            foreach (var selectedClassification in selectedProjectClassifications)
            {
                var selectedSimple = projectClassificationSimples.Single(x => x.ClassificationID == selectedClassification.ClassificationID);
                selectedSimple.Selected = true;
                selectedSimple.ProjectClassificationNotes = selectedClassification.ProjectClassificationNotes;
            }

            return projectClassificationSimples;
        }

        private ViewResult ViewEditClassifications(Project project, EditProposalClassificationsViewModel viewModel)
        {
            var allClassificationSystems = HttpRequestStorage.DatabaseEntities.ClassificationSystems.ToList().Where(x => x.HasClassifications).OrderBy(p => p.ClassificationSystemName).ToList();
            var proposalSectionsStatus = GetProposalSectionsStatus(project);
            proposalSectionsStatus.IsClassificationsComplete = ModelState.IsValid && proposalSectionsStatus.IsClassificationsComplete;

            var viewData = new EditProposalClassificationsViewData(CurrentPerson, project, allClassificationSystems, ProjectCreateSection.Classifications.ProjectCreateSectionDisplayName, proposalSectionsStatus);
            return RazorView<EditProposalClassifications, EditProposalClassificationsViewData, EditProposalClassificationsViewModel>(viewData, viewModel);
        }

        [HttpPost]
        [ProjectCreateFeature]
        [AutomaticallyCallEntityFrameworkSaveChangesWhenModelValid]
        public ActionResult EditClassifications(ProjectPrimaryKey projectPrimaryKey, EditProposalClassificationsViewModel viewModel)
        {
            var project = projectPrimaryKey.EntityObject;
            if (!ModelState.IsValid)
            {
                return ViewEditClassifications(project, viewModel);
            }
            var currentProjectClassifications = viewModel.ProjectClassificationSimples;
            HttpRequestStorage.DatabaseEntities.ProjectClassifications.Load();
            viewModel.UpdateModel(project, currentProjectClassifications);

            SetMessageForDisplay($"{FieldDefinition.Project.GetFieldDefinitionLabel()} {FieldDefinition.Classification.GetFieldDefinitionLabelPluralized()} successfully saved.");
            return GoToNextSection(viewModel, project, ProjectCreateSection.Classifications.ProjectCreateSectionDisplayName);
        }

        [HttpGet]
        [ProjectCreateFeature]
        public ViewResult EditLocationSimple(ProjectPrimaryKey projectPrimaryKey)
        {
            var project = projectPrimaryKey.EntityObject;
            var viewModel = new LocationSimpleViewModel(project);
            return ViewEditLocationSimple(project, viewModel);
        }

        private ViewResult ViewEditLocationSimple(Project project, LocationSimpleViewModel viewModel)
        {
            var layerGeoJsons = MapInitJson.GetAllGeospatialAreaMapLayers(LayerInitialVisibility.Hide);
            var mapInitJson = new MapInitJson($"project_{project.ProjectID}_EditMap", 10, layerGeoJsons, BoundingBox.MakeNewDefaultBoundingBox(), false) {AllowFullScreen = false, DisablePopups = true };
            
            var mapPostUrl = SitkaRoute<ProjectCreateController>.BuildUrlFromExpression(c => c.EditLocationSimple(project, null));
            var mapFormID = GenerateEditProjectLocationSimpleFormID(project);
            var wmsLayerNames = FirmaWebConfiguration.GetWmsLayerNames();
            var editProjectLocationViewData = new ProjectLocationSimpleViewData(CurrentPerson, mapInitJson, wmsLayerNames, null, mapPostUrl, mapFormID, FirmaWebConfiguration.WebMapServiceUrl);

            var proposalSectionsStatus = GetProposalSectionsStatus(project);
            proposalSectionsStatus.IsProjectLocationSimpleSectionComplete = ModelState.IsValid && proposalSectionsStatus.IsProjectLocationSimpleSectionComplete;
            var viewData = new LocationSimpleViewData(CurrentPerson, project, proposalSectionsStatus, editProjectLocationViewData);

            return RazorView<LocationSimple, LocationSimpleViewData, LocationSimpleViewModel>(viewData, viewModel);
        }

        [HttpPost]
        [ProjectCreateFeature]
        [AutomaticallyCallEntityFrameworkSaveChangesWhenModelValid]
        public ActionResult EditLocationSimple(ProjectPrimaryKey projectPrimaryKey, LocationSimpleViewModel viewModel)
        {
            var project = projectPrimaryKey.EntityObject;
            if (!ModelState.IsValid)
            {
                return ViewEditLocationSimple(project, viewModel);
            }

            viewModel.UpdateModel(project);
            SetMessageForDisplay($"{FieldDefinition.Project.GetFieldDefinitionLabel()} Location successfully saved.");
            return GoToNextSection(viewModel, project, ProjectCreateSection.LocationSimple.ProjectCreateSectionDisplayName);
        }


        private static string GenerateEditProjectLocationSimpleFormID(Project project)
        {
            return $"editMapForProject{project.ProjectID}";
        }

        [HttpGet]
        [ProjectCreateFeature]
        public ViewResult EditLocationDetailed(ProjectPrimaryKey projectPrimaryKey)
        {
            var project = projectPrimaryKey.EntityObject;
            var viewModel = new LocationDetailedViewModel(project.ProjectLocations);
            return ViewEditLocationDetailed(project, viewModel);
        }

        private ViewResult ViewEditLocationDetailed(Project project, LocationDetailedViewModel viewModel)
        {
            var mapDivID = $"project_{project.EntityID}_EditDetailedMap";
            var detailedLocationGeoJsonFeatureCollection = project.AllDetailedLocationsToGeoJsonFeatureCollection();
            var editableLayerGeoJson = new LayerGeoJson($"Proposed {FieldDefinition.ProjectLocation.GetFieldDefinitionLabel()}- Detail", detailedLocationGeoJsonFeatureCollection, "red", 1, LayerInitialVisibility.Show);

            var boundingBox = ProjectLocationSummaryMapInitJson.GetProjectBoundingBox(project);
            var layers = MapInitJson.GetAllGeospatialAreaMapLayers(LayerInitialVisibility.Show);
            layers.AddRange(MapInitJson.GetProjectLocationSimpleMapLayer(project));
            var mapInitJson = new MapInitJson(mapDivID, 10, layers, boundingBox) { AllowFullScreen = false, DisablePopups = true};

            var mapFormID = GenerateEditProjectLocationFormID(project.ProjectID);
            var uploadGisFileUrl = SitkaRoute<ProjectCreateController>.BuildUrlFromExpression(c => c.ImportGdbFile(project.EntityID));
            var saveFeatureCollectionUrl = SitkaRoute<ProjectCreateController>.BuildUrlFromExpression(x => x.EditLocationDetailed(project, null));

            var hasSimpleLocationPoint = project.ProjectLocationPoint != null;

            var proposalSectionsStatus = GetProposalSectionsStatus(project);
            var projectLocationDetailViewData = new ProjectLocationDetailViewData(project.ProjectID, mapInitJson, editableLayerGeoJson, uploadGisFileUrl, mapFormID, saveFeatureCollectionUrl, ProjectLocation.FieldLengths.ProjectLocationNotes, hasSimpleLocationPoint);
            var viewData = new LocationDetailedViewData(CurrentPerson, project, proposalSectionsStatus, projectLocationDetailViewData);
            return RazorView<LocationDetailed, LocationDetailedViewData, LocationDetailedViewModel>(viewData, viewModel);
        }

        [HttpPost]
        [ProjectCreateFeature]
        [AutomaticallyCallEntityFrameworkSaveChangesWhenModelValid]
        public ActionResult EditLocationDetailed(ProjectPrimaryKey projectPrimaryKey, LocationDetailedViewModel viewModel)
        {
            var project = projectPrimaryKey.EntityObject;
            if (!ModelState.IsValid)
            {
                return ViewEditLocationDetailed(project, viewModel);
            }
            SaveDetailedLocations(viewModel, project);
            return GoToNextSection(viewModel, project, ProjectCreateSection.LocationDetailed.ProjectCreateSectionDisplayName);
        }

        [HttpGet]
        [ProjectCreateFeature]
        public PartialViewResult ImportGdbFile(ProjectPrimaryKey projectPrimaryKey)
        {
            var project = projectPrimaryKey.EntityObject;
            var viewModel = new ImportGdbFileViewModel();
            return ViewImportGdbFile(viewModel, project.ProjectID);
        }

        private PartialViewResult ViewImportGdbFile(ImportGdbFileViewModel viewModel, int projectID)
        {
            var mapFormID = GenerateEditProjectLocationFormID(projectID);
            var newGisUploadUrl = SitkaRoute<ProjectCreateController>.BuildUrlFromExpression(x => x.ImportGdbFile(projectID, null));
            var approveGisUploadUrl = SitkaRoute<ProjectCreateController>.BuildUrlFromExpression(x => x.ApproveGisUpload(projectID, null));
            var viewData = new ImportGdbFileViewData(mapFormID, newGisUploadUrl, approveGisUploadUrl);
            return RazorPartialView<ImportGdbFile, ImportGdbFileViewData, ImportGdbFileViewModel>(viewData, viewModel);
        }

        [HttpPost]
        [ProjectCreateFeature]
        [AutomaticallyCallEntityFrameworkSaveChangesWhenModelValid]
        public ActionResult ImportGdbFile(ProjectPrimaryKey projectPrimaryKey, ImportGdbFileViewModel viewModel)
        {
            var project = projectPrimaryKey.EntityObject;
            if (!ModelState.IsValid)
            {
                return ViewImportGdbFile(viewModel, project.ProjectID);
            }

            var httpPostedFileBase = viewModel.FileResourceData;
            var fileEnding = ".gdb.zip";
            using (var disposableTempFile = DisposableTempFile.MakeDisposableTempFileEndingIn(fileEnding))
            {
                var gdbFile = disposableTempFile.FileInfo;
                httpPostedFileBase.SaveAs(gdbFile.FullName);
                foreach (var projectLocationStaging in project.ProjectLocationStagings)
                {
                    projectLocationStaging.DeleteFull(HttpRequestStorage.DatabaseEntities);
                }
                project.ProjectLocationStagings.Clear();
                ProjectLocationStaging.CreateProjectLocationStagingListFromGdb(gdbFile, project, CurrentPerson);
            }
            return ApproveGisUpload(project);
        }

        [HttpGet]
        [ProjectCreateFeature]
        public PartialViewResult ApproveGisUpload(ProjectPrimaryKey projectPrimaryKey)
        {
            var project = projectPrimaryKey.EntityObject;
            var viewModel = new ProjectLocationDetailViewModel(project.ProjectLocations);
            return ViewApproveGisUpload(project, viewModel);
        }

        private PartialViewResult ViewApproveGisUpload(Project project, ProjectLocationDetailViewModel viewModel)
        {
            var projectLocationStagings = project.ProjectLocationStagings.ToList();
            var layerGeoJsons =
                projectLocationStagings.Select(
                    (projectLocationStaging, i) =>
                        new LayerGeoJson(projectLocationStaging.FeatureClassName,
                            projectLocationStaging.ToGeoJsonFeatureCollection(),
                            FirmaHelpers.DefaultColorRange[i],
                            1,
                            LayerInitialVisibility.Show)).ToList();

            var boundingBox = BoundingBox.MakeBoundingBoxFromLayerGeoJsonList(layerGeoJsons);

            var mapInitJson = new MapInitJson($"project_{project.ProjectID}_PreviewMap", 10, layerGeoJsons, boundingBox, false) {AllowFullScreen = false, DisablePopups = true};
            var mapFormID = GenerateEditProjectLocationFormID(project.ProjectID);
            var approveGisUploadUrl = SitkaRoute<ProjectCreateController>.BuildUrlFromExpression(x => x.ApproveGisUpload(project, null));

            var viewData = new ApproveGisUploadViewData(new List<IProjectLocationStaging>(projectLocationStagings), mapInitJson, mapFormID, approveGisUploadUrl);
            return RazorPartialView<ApproveGisUpload, ApproveGisUploadViewData, ProjectLocationDetailViewModel>(viewData, viewModel);
        }

        [HttpPost]
        [ProjectCreateFeature]
        [AutomaticallyCallEntityFrameworkSaveChangesWhenModelValid]
        public ActionResult ApproveGisUpload(ProjectPrimaryKey projectPrimaryKey, ProjectLocationDetailViewModel viewModel)
        {
            var project = projectPrimaryKey.EntityObject;
            if (!ModelState.IsValid)
            {
                return ViewApproveGisUpload(project, viewModel);
            }
            SaveDetailedLocations(viewModel, project);
            DbSpatialHelper.Reduce(new List<IHaveSqlGeometry>(project.ProjectLocations.ToList()));
            return new ModalDialogFormJsonResult();
        }

        private static void SaveDetailedLocations(ProjectLocationDetailViewModel viewModel, Project project)
        {
            var projectLocations = project.ProjectLocations.ToList();
            foreach (var projectLocation in projectLocations)
            {
                projectLocation.DeleteFull(HttpRequestStorage.DatabaseEntities);
            }
            project.ProjectLocations.Clear();
            if (viewModel.ProjectLocationJsons != null)
            {
                foreach (var projectLocationJson in viewModel.ProjectLocationJsons)
                {
                    var projectLocationGeometry = DbGeometry.FromText(projectLocationJson.ProjectLocationGeometryWellKnownText, FirmaWebConfiguration.GeoSpatialReferenceID);
                    var projectLocation = new ProjectLocation(project, projectLocationJson.ProjectLocationName, projectLocationGeometry, projectLocationJson.ProjectLocationTypeID, projectLocationJson.ProjectLocationNotes);
                    project.ProjectLocations.Add(projectLocation);
                }
            }
        }

        public static string GenerateEditProjectLocationFormID(int projectID)
        {
            return $"editMapForProject{projectID}";
        }

        [HttpGet]
        [ProjectCreateFeature]
        public ViewResult Regions(ProjectPrimaryKey projectPrimaryKey)
        {
            var project = projectPrimaryKey.EntityObject;
            var regionIDs = project.ProjectRegions.Select(x => x.RegionID).ToList();
            var noRegionsExplanation = project.NoRegionsExplanation;
            var viewModel = new RegionsViewModel(regionIDs, noRegionsExplanation);
            return ViewEditRegion(project, viewModel);
        }

        private ViewResult ViewEditRegion(Project project, RegionsViewModel viewModel)
        {
            var boundingBox = ProjectLocationSummaryMapInitJson.GetProjectBoundingBox(project);
            var layers = MapInitJson.GetRegionMapLayers(LayerInitialVisibility.Show);
            layers.AddRange(MapInitJson.GetProjectLocationSimpleAndDetailedMapLayers(project));
            var mapInitJson = new MapInitJson("projectRegionMap", 0, layers, boundingBox) { AllowFullScreen = false, DisablePopups = true};
            var regionIDs = viewModel.RegionIDs ?? new List<int>();
            var regionsInViewModel = HttpRequestStorage.DatabaseEntities.Regions.Where(x => regionIDs.Contains(x.RegionID)).ToList();
            var editProjectRegionsPostUrl = SitkaRoute<ProjectCreateController>.BuildUrlFromExpression(c => c.Regions(project, null));
            var editProjectRegionsFormId = GenerateEditProjectRegionsFormID(project);

            var editProjectLocationViewData = new EditProjectRegionsViewData(CurrentPerson, mapInitJson, regionsInViewModel, editProjectRegionsPostUrl, editProjectRegionsFormId, project.HasProjectLocationPoint, project.HasProjectLocationDetail);

            var proposalSectionsStatus = GetProposalSectionsStatus(project);
            proposalSectionsStatus.IsRegionSectionComplete = ModelState.IsValid && proposalSectionsStatus.IsRegionSectionComplete;
            var viewData = new RegionsViewData(CurrentPerson, project, proposalSectionsStatus, editProjectLocationViewData);

            return RazorView<Views.ProjectCreate.Regions, RegionsViewData, RegionsViewModel>(viewData, viewModel);
        }

        [HttpPost]
        [ProjectCreateFeature]
        [AutomaticallyCallEntityFrameworkSaveChangesWhenModelValid]
        public ActionResult Regions(ProjectPrimaryKey projectPrimaryKey, RegionsViewModel viewModel)
        {
            var project = projectPrimaryKey.EntityObject;
            if (!ModelState.IsValid)
            {
                return ViewEditRegion(project, viewModel);
            }

            var currentProjectRegions = project.ProjectRegions.ToList();
            var allProjectRegions = HttpRequestStorage.DatabaseEntities.ProjectRegions.Local;
            viewModel.UpdateModel(project, currentProjectRegions, allProjectRegions);

            project.NoRegionsExplanation = viewModel.NoRegionsExplanation;
            SetMessageForDisplay($"{FieldDefinition.Project.GetFieldDefinitionLabel()} Regions successfully saved.");
            return GoToNextSection(viewModel, project, "Regions");
        }

        [HttpGet]
        [ProjectCreateFeature]
        public ViewResult PriorityAreas(ProjectPrimaryKey projectPrimaryKey)
        {
            var project = projectPrimaryKey.EntityObject;
            var priorityAreaIDs = project.ProjectPriorityAreas.Select(x => x.PriorityAreaID).ToList();
            var noPriorityAreasExplanation = project.NoPriorityAreasExplanation;
            var viewModel = new PriorityAreasViewModel(priorityAreaIDs, noPriorityAreasExplanation);
            return ViewEditPriorityArea(project, viewModel);
        }

        private ViewResult ViewEditPriorityArea(Project project, PriorityAreasViewModel viewModel)
        {
            var boundingBox = ProjectLocationSummaryMapInitJson.GetProjectBoundingBox(project);
            var layers = MapInitJson.GetPriorityAreaMapLayers(LayerInitialVisibility.Show);
            layers.AddRange(MapInitJson.GetProjectLocationSimpleAndDetailedMapLayers(project));
            var mapInitJson = new MapInitJson("projectPriorityAreaMap", 0, layers, boundingBox) { AllowFullScreen = false, DisablePopups = true };
            var priorityAreaIDs = viewModel.PriorityAreaIDs ?? new List<int>();
            var priorityAreasInViewModel = HttpRequestStorage.DatabaseEntities.PriorityAreas.Where(x => priorityAreaIDs.Contains(x.PriorityAreaID)).ToList();
            var editProjectPriorityAreasPostUrl = SitkaRoute<ProjectCreateController>.BuildUrlFromExpression(c => c.PriorityAreas(project, null));
            var editProjectPriorityAreasFormId = GenerateEditProjectPriorityAreasFormID(project);

            var editProjectLocationViewData = new EditProjectPriorityAreasViewData(CurrentPerson, mapInitJson, priorityAreasInViewModel, editProjectPriorityAreasPostUrl, editProjectPriorityAreasFormId, project.HasProjectLocationPoint, project.HasProjectLocationDetail);

            var proposalSectionsStatus = GetProposalSectionsStatus(project);
            proposalSectionsStatus.IsPriorityAreaSectionComplete = ModelState.IsValid && proposalSectionsStatus.IsPriorityAreaSectionComplete;
            var viewData = new PriorityAreasViewData(CurrentPerson, project, proposalSectionsStatus, editProjectLocationViewData);

            return RazorView<Views.ProjectCreate.PriorityAreas, PriorityAreasViewData, PriorityAreasViewModel>(viewData, viewModel);
        }

        [HttpPost]
        [ProjectCreateFeature]
        [AutomaticallyCallEntityFrameworkSaveChangesWhenModelValid]
        public ActionResult PriorityAreas(ProjectPrimaryKey projectPrimaryKey, PriorityAreasViewModel viewModel)
        {
            var project = projectPrimaryKey.EntityObject;
            if (!ModelState.IsValid)
            {
                return ViewEditPriorityArea(project, viewModel);
            }

            var currentProjectPriorityAreas = project.ProjectPriorityAreas.ToList();
            var allProjectPriorityAreas = HttpRequestStorage.DatabaseEntities.ProjectPriorityAreas.Local;
            viewModel.UpdateModel(project, currentProjectPriorityAreas, allProjectPriorityAreas);

            project.NoPriorityAreasExplanation = viewModel.NoPriorityAreasExplanation;
            SetMessageForDisplay($"{FieldDefinition.Project.GetFieldDefinitionLabel()} Priority Areas successfully saved.");
            return GoToNextSection(viewModel, project, ProjectCreateSection.PriorityAreas.ProjectCreateSectionDisplayName);
        }

        private static string GenerateEditProjectGeospatialAreaFormID(Project project)
        {
            return $"editMapForProject{project.ProjectID}";
        }

        private static string GenerateEditProjectPriorityAreasFormID(Project project)
        {
            return $"editMapForProjectPriorityAreas{project.ProjectID}";
        }

        private static string GenerateEditProjectRegionsFormID(Project project)
        {
            return $"editMapForProjectRegions{project.ProjectID}";
        }

        [ProjectCreateFeature]
        public ViewResult DocumentsAndNotes(ProjectPrimaryKey projectPrimaryKey)
        {
            var project = projectPrimaryKey.EntityObject;
            var entityNotes = new List<IEntityNote>(project.ProjectNotes);
            var addNoteUrl = SitkaRoute<ProjectCreateController>.BuildUrlFromExpression(x => x.NewNote(project));
            var canEditNotesAndDocuments = new ProjectCreateFeature().HasPermission(CurrentPerson, project).HasPermission;
            var entityNotesViewData = new EntityNotesViewData(EntityNote.CreateFromEntityNote(entityNotes), addNoteUrl, $"{FieldDefinition.Project.GetFieldDefinitionLabel()}", canEditNotesAndDocuments);

            var proposalSectionsStatus = GetProposalSectionsStatus(project);
            var projectDocumentsDetailViewData = new ProjectDocumentsDetailViewData(
                EntityDocument.CreateFromEntityDocument(new List<IEntityDocument>(project.ProjectDocuments)),
                SitkaRoute<ProjectCreateController>.BuildUrlFromExpression(x => x.NewDocument(project)), project.ProjectName,
                canEditNotesAndDocuments);
            var viewData = new DocumentsAndNotesViewData(CurrentPerson, project, proposalSectionsStatus, entityNotesViewData, projectDocumentsDetailViewData);
            return RazorView<DocumentsAndNotes, DocumentsAndNotesViewData>(viewData);
        }

        [HttpGet]
        [ProjectCreateFeature]
        public PartialViewResult NewNote(ProjectPrimaryKey projectPrimaryKey)
        {
            var viewModel = new EditNoteViewModel();
            return ViewEditNote(viewModel);
        }

        [HttpPost]
        [ProjectCreateFeature]
        [AutomaticallyCallEntityFrameworkSaveChangesWhenModelValid]
        public ActionResult NewNote(ProjectPrimaryKey projectPrimaryKey, EditNoteViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return ViewEditNote(viewModel);
            }
            var project = projectPrimaryKey.EntityObject;
            var projectNote = ProjectNote.CreateNewBlank(project);
            viewModel.UpdateModel(projectNote, CurrentPerson);
            HttpRequestStorage.DatabaseEntities.ProjectNotes.Add(projectNote);
            return new ModalDialogFormJsonResult();
        }

        [HttpGet]
        [ProjectCreateFeature]
        public PartialViewResult EditNote(ProjectNotePrimaryKey projectNotePrimaryKey)
        {
            var projectNote = projectNotePrimaryKey.EntityObject;
            var viewModel = new EditNoteViewModel(projectNote.Note);
            return ViewEditNote(viewModel);
        }

        [HttpPost]
        [ProjectCreateFeature]
        [AutomaticallyCallEntityFrameworkSaveChangesWhenModelValid]
        public ActionResult EditNote(ProjectNotePrimaryKey projectNotePrimaryKey, EditNoteViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return ViewEditNote(viewModel);
            }
            var projectNote = projectNotePrimaryKey.EntityObject;
            viewModel.UpdateModel(projectNote, CurrentPerson);
            return new ModalDialogFormJsonResult();
        }

        private PartialViewResult ViewEditNote(EditNoteViewModel viewModel)
        {
            var viewData = new EditNoteViewData();
            return RazorPartialView<EditNote, EditNoteViewData, EditNoteViewModel>(viewData, viewModel);
        }

        [HttpGet]
        [ProjectCreateFeature]
        public PartialViewResult DeleteNote(ProjectNotePrimaryKey projectNotePrimaryKey)
        {
            var projectNote = projectNotePrimaryKey.EntityObject;
            var viewModel = new ConfirmDialogFormViewModel(projectNote.ProjectNoteID);
            return ViewDeleteNote(projectNote, viewModel);
        }

        private PartialViewResult ViewDeleteNote(ProjectNote projectNote, ConfirmDialogFormViewModel viewModel)
        {
            var canDelete = !projectNote.HasDependentObjects();
            var confirmMessage = canDelete
                ? $"Are you sure you want to delete this note for {FieldDefinition.Project.GetFieldDefinitionLabel()} '{projectNote.Project.DisplayName}'?"
                : ConfirmDialogFormViewData.GetStandardCannotDeleteMessage($"Proposed {FieldDefinition.ProjectNote.GetFieldDefinitionLabel()}");

            var viewData = new ConfirmDialogFormViewData(confirmMessage, canDelete);

            return RazorPartialView<ConfirmDialogForm, ConfirmDialogFormViewData, ConfirmDialogFormViewModel>(viewData, viewModel);
        }

        [HttpPost]
        [ProjectCreateFeature]
        [AutomaticallyCallEntityFrameworkSaveChangesWhenModelValid]
        public ActionResult DeleteNote(ProjectNotePrimaryKey projectNotePrimaryKey, ConfirmDialogFormViewModel viewModel)
        {
            var projectNote = projectNotePrimaryKey.EntityObject;
            if (!ModelState.IsValid)
            {
                return ViewDeleteNote(projectNote, viewModel);
            }
            projectNote.DeleteFull(HttpRequestStorage.DatabaseEntities);
            return new ModalDialogFormJsonResult();
        }

        [HttpGet]
        [ProjectCreateFeature]
        public PartialViewResult NewDocument(ProjectPrimaryKey projectPrimaryKey)
        {
            var viewModel = new NewProjectDocumentViewModel();
            return ViewNewDocument(viewModel);
        }

        [HttpPost]
        [ProjectCreateFeature]
        [AutomaticallyCallEntityFrameworkSaveChangesWhenModelValid]
        public ActionResult NewDocument(ProjectPrimaryKey projectPrimaryKey, NewProjectDocumentViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return ViewNewDocument(viewModel);
            }
            var project = projectPrimaryKey.EntityObject;
            viewModel.UpdateModel(project, CurrentPerson);
            return new ModalDialogFormJsonResult();
        }

        [HttpGet]
        [ProjectCreateFeature]
        public PartialViewResult EditDocument(ProjectDocumentPrimaryKey projectDocumentPrimaryKey)
        {
            var projectDocument = projectDocumentPrimaryKey.EntityObject;
            var viewModel = new EditProjectDocumentsViewModel(projectDocument);
            return ViewEditDocument(viewModel);
        }

        [HttpPost]
        [ProjectCreateFeature]
        [AutomaticallyCallEntityFrameworkSaveChangesWhenModelValid]
        public ActionResult EditDocument(ProjectDocumentPrimaryKey projectDocumentPrimaryKey, EditProjectDocumentsViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return ViewEditDocument(viewModel);
            }
            var projectDocument = projectDocumentPrimaryKey.EntityObject;
            viewModel.UpdateModel(projectDocument);
            return new ModalDialogFormJsonResult();
        }

        private PartialViewResult ViewEditDocument(EditProjectDocumentsViewModel viewModel)
        {
            var viewData = new EditProjectDocumentsViewData();
            return RazorPartialView<EditProjectDocuments, EditProjectDocumentsViewData, EditProjectDocumentsViewModel>(viewData, viewModel);
        }

        private PartialViewResult ViewNewDocument(NewProjectDocumentViewModel viewModel)
        {
            var viewData = new NewProjectDocumentViewData();
            return RazorPartialView<NewProjectDocument, NewProjectDocumentViewData, NewProjectDocumentViewModel>(viewData, viewModel);
        }

        [HttpGet]
        [ProjectCreateFeature]
        public PartialViewResult DeleteDocument(ProjectDocumentPrimaryKey projectDocumentPrimaryKey)
        {
            var projectDocument = projectDocumentPrimaryKey.EntityObject;
            var viewModel = new ConfirmDialogFormViewModel(projectDocument.ProjectDocumentID);
            return ViewDeleteDocument(projectDocument, viewModel);
        }

        private PartialViewResult ViewDeleteDocument(ProjectDocument projectDocument, ConfirmDialogFormViewModel viewModel)
        {
            var canDelete = !projectDocument.HasDependentObjects();
            var confirmMessage = canDelete
                ? $"Are you sure you want to delete \"{projectDocument.DisplayName}\" from this {FieldDefinition.Project.GetFieldDefinitionLabel()}?"
                : ConfirmDialogFormViewData.GetStandardCannotDeleteMessage($"Proposed {FieldDefinition.Project.GetFieldDefinitionLabel()} Document");

            var viewData = new ConfirmDialogFormViewData(confirmMessage, canDelete);

            return RazorPartialView<ConfirmDialogForm, ConfirmDialogFormViewData, ConfirmDialogFormViewModel>(viewData, viewModel);
        }

        [HttpPost]
        [ProjectCreateFeature]
        [AutomaticallyCallEntityFrameworkSaveChangesWhenModelValid]
        public ActionResult DeleteDocument(ProjectDocumentPrimaryKey projectDocumentPrimaryKey, ConfirmDialogFormViewModel viewModel)
        {
            var projectDocument = projectDocumentPrimaryKey.EntityObject;
            if (!ModelState.IsValid)
            {
                return ViewDeleteDocument(projectDocument, viewModel);
            }
            projectDocument.DeleteFull(HttpRequestStorage.DatabaseEntities);
            return new ModalDialogFormJsonResult();
        }

        [ProjectCreateFeature]
        public ViewResult Photos(ProjectPrimaryKey projectPrimaryKey)
        {
            var project = projectPrimaryKey.EntityObject;
            var viewData = BuildImageGalleryViewData(project, CurrentPerson);
            return RazorView<Photos, PhotoViewData>(viewData);
        }

        private static PhotoViewData BuildImageGalleryViewData(Project project, Person currentPerson)
        {
            var proposalSectionsStatus = GetProposalSectionsStatus(project);
            var newPhotoForProjectUrl = SitkaRoute<ProjectImageController>.BuildUrlFromExpression(x => x.NewFromProposal(project));
            var galleryName = $"ProjectImage{project.ProjectID}";
            var projectImages = project.ProjectImages.ToList();
            var imageGalleryViewData = new PhotoViewData(currentPerson, galleryName, projectImages, newPhotoForProjectUrl, x => x.CaptionOnFullView, project, proposalSectionsStatus);
            return imageGalleryViewData;
        }

        [HttpGet]
        [ProjectDeleteProposalFeature]
        public PartialViewResult DeleteProjectProposal(ProjectPrimaryKey projectPrimaryKey)
        {
            var viewModel = new ConfirmDialogFormViewModel(projectPrimaryKey.PrimaryKeyValue);
            return ViewDeleteProject(projectPrimaryKey.EntityObject, viewModel);
        }

        private PartialViewResult ViewDeleteProject(Project project, ConfirmDialogFormViewModel viewModel)
        {
            var confirmMessage = $"Are you sure you want to delete {FieldDefinition.Project.GetFieldDefinitionLabel()} \"{project.DisplayName}\"?";
            var viewData = new ConfirmDialogFormViewData(confirmMessage, true);
            return RazorPartialView<ConfirmDialogForm, ConfirmDialogFormViewData, ConfirmDialogFormViewModel>(viewData, viewModel);
        }

        [HttpPost]
        [ProjectDeleteProposalFeature]
        [AutomaticallyCallEntityFrameworkSaveChangesWhenModelValid]
        public ActionResult DeleteProjectProposal(ProjectPrimaryKey projectPrimaryKey, ConfirmDialogFormViewModel viewModel)
        {
            var project = projectPrimaryKey.EntityObject;
            if (!ModelState.IsValid)
            {
                return ViewDeleteProject(project, viewModel);
            }
            var message = $"{FieldDefinition.Project.GetFieldDefinitionLabel()} \"{project.DisplayName}\" successfully deleted.";
            project.DeleteFull(HttpRequestStorage.DatabaseEntities);
            SetMessageForDisplay(message);
            return new ModalDialogFormJsonResult();
        }


        [HttpGet]
        [ProjectCreateFeature]
        public PartialViewResult Submit(ProjectPrimaryKey projectPrimaryKey)
        {
            var project = projectPrimaryKey.EntityObject;
            var viewModel = new ConfirmDialogFormViewModel(project.ProjectID);
            //TODO: Change "reviewer" to specific reviewer as determined by tenant review 
            var viewData = new ConfirmDialogFormViewData($"Are you sure you want to submit {FieldDefinition.Project.GetFieldDefinitionLabel()} \"{project.DisplayName}\" to the reviewer?");
            return RazorPartialView<ConfirmDialogForm, ConfirmDialogFormViewData, ConfirmDialogFormViewModel>(viewData, viewModel);
        }

        [HttpPost]
        [ProjectCreateFeature]
        [AutomaticallyCallEntityFrameworkSaveChangesWhenModelValid]
        public ActionResult Submit(ProjectPrimaryKey projectPrimaryKey, ConfirmDialogFormViewModel viewModel)
        {
            var project = projectPrimaryKey.EntityObject;
            project.ProjectApprovalStatusID = ProjectApprovalStatus.PendingApproval.ProjectApprovalStatusID;
            project.SubmissionDate = DateTime.Now;
            NotificationProject.SendSubmittedMessage(project);
            SetMessageForDisplay($"{FieldDefinition.Project.GetFieldDefinitionLabel()} successfully submitted for review.");
            return new ModalDialogFormJsonResult(project.GetDetailUrl());
        }

        [HttpGet]
        [ProjectApproveFeature]
        public PartialViewResult Approve(ProjectPrimaryKey projectPrimaryKey)
        {
            var project = projectPrimaryKey.EntityObject;
            var viewModel = new ConfirmDialogFormViewModel(project.ProjectID);
            return ViewApprove(viewModel);
        }


        [HttpPost]
        [ProjectApproveFeature]
        [AutomaticallyCallEntityFrameworkSaveChangesWhenModelValid]
        public ActionResult Approve(ProjectPrimaryKey projectPrimaryKey, ConfirmDialogFormViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return ViewApprove(viewModel);
            }
            var project = projectPrimaryKey.EntityObject;
            Check.Assert(project.ProjectApprovalStatus == ProjectApprovalStatus.PendingApproval,
                $"{FieldDefinition.Project.GetFieldDefinitionLabel()} is not in Submitted state. Actual state is: " + project.ProjectApprovalStatus.ProjectApprovalStatusDisplayName);

            Check.Assert(ProposalSectionsStatus.AreAllSectionsValidForProject(project), "Proposal is not ready for submittal.");
            
            HttpRequestStorage.DatabaseEntities.SaveChanges();
            project.ProjectApprovalStatusID = ProjectApprovalStatus.Approved.ProjectApprovalStatusID;
            project.ApprovalDate = DateTime.Now;
            project.ReviewedByPerson = CurrentPerson;

            // Business logic: An approved Proposal becomes an active project in the Planning and Design stage
            if (project.ProjectStageID == ProjectStage.Application.ProjectStageID)
            {
                project.ProjectStageID = ProjectStage.Planned.ProjectStageID;
            }

            GenerateApprovalAuditLogEntries(project);

            NotificationProject.SendApprovalMessage(project);

            SetMessageForDisplay($"{FieldDefinition.Project.GetFieldDefinitionLabel()} \"{UrlTemplate.MakeHrefString(project.GetDetailUrl(), project.DisplayName)}\" successfully approved.");

            return new ModalDialogFormJsonResult(project.GetDetailUrl());
        }

        private void GenerateApprovalAuditLogEntries(Project project)
        {
            var auditLog = new AuditLog(CurrentPerson, DateTime.Now, AuditLogEventType.Added, "Project", project.ProjectID, "ProjectID", project.ProjectID.ToString())
            {
                ProjectID = project.ProjectID,
                AuditDescription = $"{FieldDefinition.Application.GetFieldDefinitionLabel()} {project.DisplayName} approved."
            };
            HttpRequestStorage.DatabaseEntities.AuditLogs.Add(auditLog);
        }

        private PartialViewResult ViewApprove(ConfirmDialogFormViewModel viewModel)
        {
            var viewData = new ConfirmDialogFormViewData($"Are you sure you want to approve this {FieldDefinition.Project.GetFieldDefinitionLabel()}?");
            return RazorPartialView<ConfirmDialogForm, ConfirmDialogFormViewData, ConfirmDialogFormViewModel>(viewData, viewModel);
        }

        [HttpGet]
        [ProjectCreateFeature]
        public PartialViewResult Withdraw(ProjectPrimaryKey projectPrimaryKey)
        {
            var project = projectPrimaryKey.EntityObject;
            var viewModel = new ConfirmDialogFormViewModel(project.ProjectID);
            //TODO: Change "reviewer" to specific reviewer as determined by tenant review 
            var viewData = new ConfirmDialogFormViewData($"Are you sure you want to withdraw {FieldDefinition.Project.GetFieldDefinitionLabel()} \"{project.DisplayName}\" from review?");
            return RazorPartialView<ConfirmDialogForm, ConfirmDialogFormViewData, ConfirmDialogFormViewModel>(viewData, viewModel);
        }

        [HttpPost]
        [ProjectCreateFeature]
        [AutomaticallyCallEntityFrameworkSaveChangesWhenModelValid]
        public ActionResult Withdraw(ProjectPrimaryKey projectPrimaryKey, ConfirmDialogFormViewModel viewModel)
        {
            var project = projectPrimaryKey.EntityObject;
            project.ProjectApprovalStatusID = ProjectApprovalStatus.Draft.ProjectApprovalStatusID;
            //TODO: Change "reviewer" to specific reviewer as determined by tenant review 
            SetMessageForDisplay($"{FieldDefinition.Project.GetFieldDefinitionLabel()} withdrawn from review.");
            return new ModalDialogFormJsonResult(project.GetDetailUrl());
        }

        [HttpGet]
        [ProjectApproveFeature]
        public PartialViewResult Return(ProjectPrimaryKey projectPrimaryKey)
        {
            var project = projectPrimaryKey.EntityObject;
            var viewModel = new ConfirmDialogFormViewModel(project.ProjectID);
            var viewData = new ConfirmDialogFormViewData($"Are you sure you want to return {FieldDefinition.Project.GetFieldDefinitionLabel()} \"{project.DisplayName}\" to Submitter?");
            return RazorPartialView<ConfirmDialogForm, ConfirmDialogFormViewData, ConfirmDialogFormViewModel>(viewData, viewModel);
        }

        [HttpPost]
        [ProjectApproveFeature]
        [AutomaticallyCallEntityFrameworkSaveChangesWhenModelValid]
        public ActionResult Return(ProjectPrimaryKey projectPrimaryKey, ConfirmDialogFormViewModel viewModel)
        {
            var project = projectPrimaryKey.EntityObject;
            project.ProjectApprovalStatusID = ProjectApprovalStatus.Returned.ProjectApprovalStatusID;
            project.ReviewedByPerson = CurrentPerson;
            NotificationProject.SendReturnedMessage(project);
            SetMessageForDisplay($"{FieldDefinition.Project.GetFieldDefinitionLabel()} returned to Submitter for additional clarifactions/corrections.");
            return new ModalDialogFormJsonResult(project.GetDetailUrl());
        }

        [HttpGet]
        [ProjectApproveFeature]
        public PartialViewResult Reject(ProjectPrimaryKey projectPrimaryKey)
        {
            var project = projectPrimaryKey.EntityObject;
            var viewModel = new ConfirmDialogFormViewModel(project.ProjectID);
            var viewData = new ConfirmDialogFormViewData($"Are you sure you want to reject {FieldDefinition.Project.GetFieldDefinitionLabel()} \"{project.DisplayName}\"?");
            return RazorPartialView<ConfirmDialogForm, ConfirmDialogFormViewData, ConfirmDialogFormViewModel>(viewData, viewModel);
        }

        [HttpPost]
        [ProjectApproveFeature]
        [AutomaticallyCallEntityFrameworkSaveChangesWhenModelValid]
        public ActionResult Reject(ProjectPrimaryKey projectPrimaryKey, ConfirmDialogFormViewModel viewModel)
        {
            var project = projectPrimaryKey.EntityObject;
            project.ProjectApprovalStatusID = ProjectApprovalStatus.Rejected.ProjectApprovalStatusID;
            SetMessageForDisplay($"{FieldDefinition.Project.GetFieldDefinitionLabel()} was rejected.");
            return new ModalDialogFormJsonResult(project.GetDetailUrl());
        }

        [ProjectViewFeature]
        public GridJsonNetJObjectResult<AuditLog> AuditLogsGridJsonData(ProjectPrimaryKey projectPrimaryKey)
        {
            var gridSpec = new AuditLogsGridSpec();
            var auditLogs = HttpRequestStorage.DatabaseEntities.AuditLogs.GetAuditLogEntriesForProject(projectPrimaryKey.EntityObject);
            var gridJsonNetJObjectResult = new GridJsonNetJObjectResult<AuditLog>(auditLogs, gridSpec);
            return gridJsonNetJObjectResult;
        }

        private ActionResult GoToNextSection(FormViewModel viewModel, Project project, string currentSectionName)
        {
            var applicableWizardSections = Project.GetApplicableProposalWizardSections(project, true);
            var currentSection = applicableWizardSections.Single(x => x.SectionDisplayName.Equals(currentSectionName, StringComparison.InvariantCultureIgnoreCase));
            var nextProjectUpdateSection = applicableWizardSections.Where(x => x.SortOrder > currentSection.SortOrder).OrderBy(x => x.SortOrder).FirstOrDefault();
            var nextSection = viewModel.AutoAdvance && nextProjectUpdateSection != null ? nextProjectUpdateSection.SectionUrl : currentSection.SectionUrl;
            return Redirect(nextSection);
        }

        private void DeletePerformanceMeasureActuals(Project project)
        {
            foreach (var performanceMeasureActual in project.PerformanceMeasureActuals)
            {
                performanceMeasureActual.DeleteFull(HttpRequestStorage.DatabaseEntities);
            }
            project.PerformanceMeasureActualYearsExemptionExplanation = null;
        }

        [HttpGet]
        [ProjectCreateFeature]
        public ViewResult Organizations(ProjectPrimaryKey projectPrimaryKey)
        {
            var project = projectPrimaryKey.EntityObject;
            var viewModel = new OrganizationsViewModel(project, CurrentPerson);
            return ViewOrganizations(project, viewModel);
        }

        private ViewResult ViewOrganizations(Project project, OrganizationsViewModel viewModel)
        {
            var allOrganizations = HttpRequestStorage.DatabaseEntities.Organizations.GetActiveOrganizations();
            var allPeople = HttpRequestStorage.DatabaseEntities.People.ToList().Where(x=>x.IsFullUser()).OrderBy(p => p.FullNameFirstLastAndOrg).ToList();
            if (!allPeople.Contains(CurrentPerson))
            {
                allPeople.Add(CurrentPerson);
            }
            var allRelationshipTypes = HttpRequestStorage.DatabaseEntities.RelationshipTypes.ToList();
            var defaultPrimaryContact = project?.GetPrimaryContact() ?? CurrentPerson.Organization.PrimaryContactPerson;
            
            var editOrganizationsViewData = new EditOrganizationsViewData(allOrganizations, allRelationshipTypes, defaultPrimaryContact);

            var proposalSectionsStatus = GetProposalSectionsStatus(project);
            proposalSectionsStatus.IsProjectOrganizationsSectionComplete = ModelState.IsValid && proposalSectionsStatus.IsProjectOrganizationsSectionComplete;
            var viewData = new OrganizationsViewData(CurrentPerson, project, proposalSectionsStatus, editOrganizationsViewData);

            return RazorView<Organizations, OrganizationsViewData, OrganizationsViewModel>(viewData, viewModel);
        }

        [HttpPost]
        [ProjectCreateFeature]
        [AutomaticallyCallEntityFrameworkSaveChangesWhenModelValid]
        public ActionResult Organizations(ProjectPrimaryKey projectPrimaryKey, OrganizationsViewModel viewModel)
        {
            var project = projectPrimaryKey.EntityObject;
            if (!ModelState.IsValid)
            {
                return ViewOrganizations(project, viewModel);
            }

            HttpRequestStorage.DatabaseEntities.ProjectOrganizations.Load();
            var allProjectOrganizations = HttpRequestStorage.DatabaseEntities.ProjectOrganizations.Local;

            viewModel.UpdateModel(project, allProjectOrganizations);
            HttpRequestStorage.DatabaseEntities.SaveChanges();

            SetMessageForDisplay($"{FieldDefinition.Project.GetFieldDefinitionLabel()} {FieldDefinition.Organization.GetFieldDefinitionLabelPluralized()} successfully saved.");
            return GoToNextSection(viewModel, project, ProjectCreateSection.Organizations.ProjectCreateSectionDisplayName);
        }

        [HttpGet]
        [ProjectCreateFeature]
        public ViewResult Contacts(ProjectPrimaryKey projectPrimaryKey)
        {
            var project = projectPrimaryKey.EntityObject;
            var viewModel = new ContactsViewModel(project, CurrentPerson);
            return ViewContacts(project, viewModel);
        }

        private ViewResult ViewContacts(Project project, ContactsViewModel viewModel)
        {
            var allPeople = HttpRequestStorage.DatabaseEntities.People.ToList().OrderBy(p => p.FullNameFirstLastAndOrg).ToList();
            if (!allPeople.Contains(CurrentPerson))
            {
                allPeople.Add(CurrentPerson);
            }


            var editContactsViewData = new EditPeopleViewData(allPeople, ProjectPersonRelationshipType.All, CurrentPerson);

            var proposalSectionsStatus = GetProposalSectionsStatus(project);
            proposalSectionsStatus.IsProjectContactsSectionComplete = ModelState.IsValid && proposalSectionsStatus.IsProjectContactsSectionComplete;
            var viewData = new ContactsViewData(CurrentPerson, project, proposalSectionsStatus, editContactsViewData);

            return RazorView<Contacts, ContactsViewData, ContactsViewModel>(viewData, viewModel);
        }

        [HttpPost]
        [ProjectCreateFeature]
        [AutomaticallyCallEntityFrameworkSaveChangesWhenModelValid]
        public ActionResult Contacts(ProjectPrimaryKey projectPrimaryKey, ContactsViewModel viewModel)
        {
            var project = projectPrimaryKey.EntityObject;
            if (!ModelState.IsValid)
            {
                return ViewContacts(project, viewModel);
            }

            HttpRequestStorage.DatabaseEntities.ProjectPeople.Load();
            var allProjectContacts = HttpRequestStorage.DatabaseEntities.ProjectPeople.Local;

            viewModel.UpdateModel(project, allProjectContacts);
            HttpRequestStorage.DatabaseEntities.SaveChanges();

            SetMessageForDisplay($"{FieldDefinition.Project.GetFieldDefinitionLabel()} {FieldDefinition.Contact.GetFieldDefinitionLabelPluralized()} successfully saved.");
            return GoToNextSection(viewModel, project, ProjectCreateSection.Contacts.ProjectCreateSectionDisplayName);
        }
    }
}
