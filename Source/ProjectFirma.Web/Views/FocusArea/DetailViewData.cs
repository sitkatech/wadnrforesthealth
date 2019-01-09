/*-----------------------------------------------------------------------
<copyright file="DetailViewData.cs" company="Tahoe Regional Planning Agency and Sitka Technology Group">
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
using ProjectFirma.Web.Controllers;
using ProjectFirma.Web.Models;
using ProjectFirma.Web.Security;
using ProjectFirma.Web.Common;
using ProjectFirma.Web.Views.PerformanceMeasure;
using ProjectFirma.Web.Views.Shared;

namespace ProjectFirma.Web.Views.FocusArea
{
    public class DetailViewData : FirmaViewData
    {
        public readonly Models.FocusArea FocusArea;
        public readonly bool UserHasFocusAreaManagePermissions;
        public readonly string EditFocusAreaUrl;
        public readonly string EditBoundaryUrl;
        public readonly string DeleteFocusAreaBoundaryUrl;

        public readonly ProjectsIncludingLeadImplementingGridSpec ProjectsIncludingLeadImplementingGridSpec;
        public readonly string ProjectFocusAreasGridName;
        public readonly string ProjectFocusAreasGridDataUrl;

        public readonly ProjectsIncludingLeadImplementingGridSpec ProposalsGridSpec;
        public readonly string ProposalsGridName;
        public readonly string ProposalsGridDataUrl;

        public readonly ProjectsIncludingLeadImplementingGridSpec PendingProjectsGridSpec;
        public readonly string PendingProjectsGridName;
        public readonly string PendingProjectsGridDataUrl;

        public readonly ViewGoogleChartViewData ExpendituresDirectlyFromFocusAreaViewGoogleChartViewData;
        public readonly ViewGoogleChartViewData ExpendituresReceivedFromOtherFocusAreasViewGoogleChartViewData;
        public readonly ProjectFundingSourceExpendituresForFocusAreaGridSpec ProjectFundingSourceExpendituresForFocusAreaGridSpec;
        public readonly string ProjectFundingSourceExpendituresForFocusAreaGridName;
        public readonly string ProjectFundingSourceExpendituresForFocusAreaGridDataUrl;

        public readonly string ManageFundingSourcesUrl;
        public readonly string IndexUrl;

        public readonly MapInitJson MapInitJson;
        public readonly bool HasSpatialData;

        public readonly List<PerformanceMeasureChartViewData> PerformanceMeasureChartViewDatas;
        public readonly string NewFundingSourceUrl;
        public readonly bool CanCreateNewFundingSource;

        public readonly string ProjectStewardOrLeadImplementorFieldDefinitionName;

        public readonly bool ShowProposals;
        public readonly string ProposalsPanelHeader;

        public readonly bool ShowPendingProjects;

        public bool TenantHasCanStewardProjectsFocusAreaRelationship { get; }
        public int NumberOfStewardedProjects { get; }
        public int NumberOfLeadImplementedProjects { get; }
        public int NumberOfProjectsContributedTo { get; }

        public DetailViewData(Person currentPerson,
            Models.FocusArea focusArea,
            MapInitJson mapInitJson,
            bool hasSpatialData,
            List<Models.PerformanceMeasure> performanceMeasures, 
            ViewGoogleChartViewData expendituresDirectlyFromFocusAreaViewGoogleChartViewData,
            ViewGoogleChartViewData expendituresReceivedFromOtherFocusAreasViewGoogleChartViewData) : base(currentPerson)
        {
            FocusArea = focusArea;
            PageTitle = focusArea.DisplayName;
            EntityName = $"{Models.FieldDefinition.FocusArea.GetFieldDefinitionLabel()}";
            UserHasFocusAreaManagePermissions = new FocusAreaManageFeature().HasPermissionByPerson(CurrentPerson);

            EditFocusAreaUrl = SitkaRoute<FocusAreaController>.BuildUrlFromExpression(c => c.Edit(focusArea));
            EditBoundaryUrl =
                SitkaRoute<FocusAreaController>.BuildUrlFromExpression(c => c.EditBoundary(focusArea));
            DeleteFocusAreaBoundaryUrl =
                SitkaRoute<FocusAreaController>.BuildUrlFromExpression(
                    c => c.DeleteFocusAreaBoundary(focusArea));

            ProjectsIncludingLeadImplementingGridSpec =
                new ProjectsIncludingLeadImplementingGridSpec(focusArea, CurrentPerson, false)
                {
                    ObjectNameSingular = $"{Models.FieldDefinition.Project.GetFieldDefinitionLabel()}",
                    ObjectNamePlural = $"{Models.FieldDefinition.Project.GetFieldDefinitionLabelPluralized()} associated with {focusArea.DisplayName}",
                    SaveFiltersInCookie = true
                };

            ProjectFocusAreasGridName = "projectFocusAreasFromFocusAreaGrid";
            ProjectFocusAreasGridDataUrl =
                SitkaRoute<FocusAreaController>.BuildUrlFromExpression(
                    tc => tc.ProjectsIncludingLeadImplementingGridJsonData(focusArea));

            ProjectStewardOrLeadImplementorFieldDefinitionName = MultiTenantHelpers.HasCanStewardProjectsFocusAreaRelationship()
                ? Models.FieldDefinition.ProjectsStewardFocusAreaRelationshipToProject.GetFieldDefinitionLabel()
                : "Lead Implementer";

            ProjectFundingSourceExpendituresForFocusAreaGridSpec =
                new ProjectFundingSourceExpendituresForFocusAreaGridSpec(focusArea)
                {
                    ObjectNameSingular = $"{Models.FieldDefinition.Project.GetFieldDefinitionLabel()} {Models.FieldDefinition.ReportedExpenditure.GetFieldDefinitionLabel()}",
                    ObjectNamePlural = $"{Models.FieldDefinition.Project.GetFieldDefinitionLabel()} {Models.FieldDefinition.ReportedExpenditure.GetFieldDefinitionLabelPluralized()}",
                    SaveFiltersInCookie = true
                };

            ProjectFundingSourceExpendituresForFocusAreaGridName = "projectCalendarYearExpendituresForFocusAreaGrid";
            ProjectFundingSourceExpendituresForFocusAreaGridDataUrl =
                SitkaRoute<FocusAreaController>.BuildUrlFromExpression(
                    tc => tc.ProjectFundingSourceExpendituresForFocusAreaGridJsonData(focusArea));

            ManageFundingSourcesUrl = SitkaRoute<FundingSourceController>.BuildUrlFromExpression(c => c.Index());
            IndexUrl = SitkaRoute<FocusAreaController>.BuildUrlFromExpression(c => c.Index());

            MapInitJson = mapInitJson;
            HasSpatialData = hasSpatialData;
            ExpendituresDirectlyFromFocusAreaViewGoogleChartViewData = expendituresDirectlyFromFocusAreaViewGoogleChartViewData;
            ExpendituresReceivedFromOtherFocusAreasViewGoogleChartViewData = expendituresReceivedFromOtherFocusAreasViewGoogleChartViewData;

            PerformanceMeasureChartViewDatas = performanceMeasures.Select(x => focusArea.GetPerformanceMeasureChartViewData(x, currentPerson)).ToList();

            NewFundingSourceUrl = SitkaRoute<FundingSourceController>.BuildUrlFromExpression(c => c.New());
            CanCreateNewFundingSource = new FundingSourceCreateFeature().HasPermissionByPerson(CurrentPerson) &&
                                        (CurrentPerson.RoleID != Models.Role.ProjectSteward.RoleID || // If person is project steward, they can only create funding sources for their focusArea
                                         CurrentPerson.FocusAreaID == focusArea.FocusAreaID);
            ShowProposals = currentPerson.CanViewProposals;
            ProposalsPanelHeader = MultiTenantHelpers.ShowApplicationsToThePublic()
                ? Models.FieldDefinition.Application.GetFieldDefinitionLabelPluralized()
                : $"{Models.FieldDefinition.Application.GetFieldDefinitionLabelPluralized()} (Not Visible to the Public)";

            ProposalsGridSpec =
                new ProjectsIncludingLeadImplementingGridSpec(focusArea, CurrentPerson, true)
                {
                    ObjectNameSingular = $"{Models.FieldDefinition.Application.GetFieldDefinitionLabel()}",
                    ObjectNamePlural = $"{Models.FieldDefinition.Application.GetFieldDefinitionLabelPluralized()} associated with {focusArea.DisplayName}",
                    SaveFiltersInCookie = true
                };

            ProposalsGridName = "proposalsGrid";
            ProposalsGridDataUrl =
                SitkaRoute<FocusAreaController>.BuildUrlFromExpression(
                    tc => tc.ProposalsGridJsonData(focusArea));

            ShowPendingProjects = currentPerson.CanViewPendingProjects;

            PendingProjectsGridSpec =
                new ProjectsIncludingLeadImplementingGridSpec(focusArea, CurrentPerson, true)
                {
                    ObjectNameSingular = $"{Models.FieldDefinition.Project.GetFieldDefinitionLabel()}",
                    ObjectNamePlural = $"{Models.FieldDefinition.Project.GetFieldDefinitionLabelPluralized()} associated with {focusArea.DisplayName}",
                    SaveFiltersInCookie = true
                };

            PendingProjectsGridName = "pendingProjectsGrid";
            PendingProjectsGridDataUrl =
                SitkaRoute<FocusAreaController>.BuildUrlFromExpression(
                    tc => tc.PendingProjectsGridJsonData(focusArea));

            TenantHasCanStewardProjectsFocusAreaRelationship = MultiTenantHelpers.HasCanStewardProjectsFocusAreaRelationship();
            var allAssociatedProjects = FocusArea.GetAllAssociatedProjects();
            NumberOfStewardedProjects = allAssociatedProjects
                .Distinct()
                .Count(x => x.IsActiveProject() && x.GetCanStewardProjectsFocusArea() == FocusArea);
            NumberOfLeadImplementedProjects = allAssociatedProjects
                .Distinct()
                .Count(x => x.IsActiveProject() && x.GetPrimaryContactFocusArea() == FocusArea);
            NumberOfProjectsContributedTo = allAssociatedProjects.Distinct().ToList().GetActiveProjects().Count;
        }
    }
}
