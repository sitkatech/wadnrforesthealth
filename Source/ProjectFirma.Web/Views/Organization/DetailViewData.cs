﻿/*-----------------------------------------------------------------------
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
using ProjectFirma.Web.Views.Agreement;
using ProjectFirma.Web.Views.PerformanceMeasure;
using ProjectFirma.Web.Views.Shared;

namespace ProjectFirma.Web.Views.Organization
{
    public class DetailViewData : FirmaViewData
    {
        public readonly Models.Organization Organization;
        public readonly bool UserHasOrganizationManagePermissions;
        public readonly string EditOrganizationUrl;
        public readonly string EditBoundaryUrl;
        public readonly string DeleteOrganizationBoundaryUrl;

        public readonly ProjectsIncludingLeadImplementingGridSpec ProjectsIncludingLeadImplementingGridSpec;
        public readonly string ProjectOrganizationsGridName;
        public readonly string ProjectOrganizationsGridDataUrl;

        public readonly AgreementGridSpec AgreementOrganizationsGridSpec;
        public readonly string AgreementOrganizationsGridName;
        public readonly string AgreementOrganizationsGridDataUrl;

        public readonly ProjectsIncludingLeadImplementingGridSpec ProposalsGridSpec;
        public readonly string ProposalsGridName;
        public readonly string ProposalsGridDataUrl;

        public readonly ProjectsIncludingLeadImplementingGridSpec PendingProjectsGridSpec;
        public readonly string PendingProjectsGridName;
        public readonly string PendingProjectsGridDataUrl;

        public readonly ViewGoogleChartViewData ExpendituresDirectlyFromOrganizationViewGoogleChartViewData;
        public readonly ViewGoogleChartViewData ExpendituresReceivedFromOtherOrganizationsViewGoogleChartViewData;
        public readonly ProjectFundingSourceExpendituresForOrganizationGridSpec ProjectFundingSourceExpendituresForOrganizationGridSpec;
        public readonly string ProjectFundingSourceExpendituresForOrganizationGridName;
        public readonly string ProjectFundingSourceExpendituresForOrganizationGridDataUrl;

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

        public bool TenantHasCanStewardProjectsOrganizationRelationship { get; }
        public int NumberOfStewardedProjects { get; }
        public int NumberOfLeadImplementedProjects { get; }
        public int NumberOfProjectsContributedTo { get; }

        public DetailViewData(Person currentPerson,
            Models.Organization organization,
            MapInitJson mapInitJson,
            bool hasSpatialData,
            List<Models.PerformanceMeasure> performanceMeasures, 
            ViewGoogleChartViewData expendituresDirectlyFromOrganizationViewGoogleChartViewData,
            ViewGoogleChartViewData expendituresReceivedFromOtherOrganizationsViewGoogleChartViewData,
            bool atLeastOneAgreementHasFile) : base(currentPerson)
        {
            Organization = organization;
            PageTitle = organization.DisplayName;
            EntityName = $"{Models.FieldDefinition.Organization.GetFieldDefinitionLabel()}";
            UserHasOrganizationManagePermissions = new OrganizationManageFeature().HasPermissionByPerson(CurrentPerson);

            EditOrganizationUrl = SitkaRoute<OrganizationController>.BuildUrlFromExpression(c => c.Edit(organization));
            EditBoundaryUrl =
                SitkaRoute<OrganizationController>.BuildUrlFromExpression(c => c.EditBoundary(organization));
            DeleteOrganizationBoundaryUrl =
                SitkaRoute<OrganizationController>.BuildUrlFromExpression(
                    c => c.DeleteOrganizationBoundary(organization));

            ProjectsIncludingLeadImplementingGridSpec =
                new ProjectsIncludingLeadImplementingGridSpec(organization, CurrentPerson, false)
                {
                    ObjectNameSingular = $"{Models.FieldDefinition.Project.GetFieldDefinitionLabel()}",
                    ObjectNamePlural = $"{Models.FieldDefinition.Project.GetFieldDefinitionLabelPluralized()} associated with {organization.DisplayName}",
                    SaveFiltersInCookie = true
                };
            ProjectOrganizationsGridName = "projectOrganizationsFromOrganizationGrid";
            ProjectOrganizationsGridDataUrl =
                SitkaRoute<OrganizationController>.BuildUrlFromExpression(
                    tc => tc.ProjectsIncludingLeadImplementingGridJsonData(organization));

            AgreementOrganizationsGridSpec = new AgreementGridSpec(CurrentPerson, atLeastOneAgreementHasFile, false, false)
            {
                CustomExcelDownloadUrl = SitkaRoute<OrganizationController>.BuildUrlFromExpression(tc => tc.AgreementsExcelDownload(organization.PrimaryKey))
            };
            AgreementOrganizationsGridName = "agreementOrganizationsFromOrganizationGrid";
            AgreementOrganizationsGridDataUrl =
                SitkaRoute<OrganizationController>.BuildUrlFromExpression(
                    tc => tc.AgreementOrganizationGridJsonData(organization));

            ProjectStewardOrLeadImplementorFieldDefinitionName = MultiTenantHelpers.HasCanStewardProjectsOrganizationRelationship()
                ? Models.FieldDefinition.ProjectsStewardOrganizationRelationshipToProject.GetFieldDefinitionLabel()
                : "Lead Implementer";

            ProjectFundingSourceExpendituresForOrganizationGridSpec =
                new ProjectFundingSourceExpendituresForOrganizationGridSpec(organization)
                {
                    ObjectNameSingular = $"{Models.FieldDefinition.Project.GetFieldDefinitionLabel()} {Models.FieldDefinition.ReportedExpenditure.GetFieldDefinitionLabel()}",
                    ObjectNamePlural = $"{Models.FieldDefinition.Project.GetFieldDefinitionLabel()} {Models.FieldDefinition.ReportedExpenditure.GetFieldDefinitionLabelPluralized()}",
                    SaveFiltersInCookie = true
                };

            ProjectFundingSourceExpendituresForOrganizationGridName = "projectCalendarYearExpendituresForOrganizationGrid";
            ProjectFundingSourceExpendituresForOrganizationGridDataUrl =
                SitkaRoute<OrganizationController>.BuildUrlFromExpression(
                    tc => tc.ProjectFundingSourceExpendituresForOrganizationGridJsonData(organization));

            ManageFundingSourcesUrl = SitkaRoute<FundingSourceController>.BuildUrlFromExpression(c => c.Index());
            IndexUrl = SitkaRoute<OrganizationController>.BuildUrlFromExpression(c => c.Index());

            MapInitJson = mapInitJson;
            HasSpatialData = hasSpatialData;
            ExpendituresDirectlyFromOrganizationViewGoogleChartViewData = expendituresDirectlyFromOrganizationViewGoogleChartViewData;
            ExpendituresReceivedFromOtherOrganizationsViewGoogleChartViewData = expendituresReceivedFromOtherOrganizationsViewGoogleChartViewData;

            PerformanceMeasureChartViewDatas = performanceMeasures.Select(x => organization.GetPerformanceMeasureChartViewData(x, currentPerson)).ToList();

            NewFundingSourceUrl = SitkaRoute<FundingSourceController>.BuildUrlFromExpression(c => c.New());
            CanCreateNewFundingSource = new FundingSourceCreateFeature().HasPermissionByPerson(CurrentPerson) &&
                                        (CurrentPerson.RoleID != Models.Role.ProjectSteward.RoleID || // If person is project steward, they can only create funding sources for their organization
                                         CurrentPerson.OrganizationID == organization.OrganizationID);
            ShowProposals = currentPerson.CanViewProposals;
            ProposalsPanelHeader = MultiTenantHelpers.ShowApplicationsToThePublic()
                ? Models.FieldDefinition.Application.GetFieldDefinitionLabelPluralized()
                : $"{Models.FieldDefinition.Application.GetFieldDefinitionLabelPluralized()} (Not Visible to the Public)";

            ProposalsGridSpec =
                new ProjectsIncludingLeadImplementingGridSpec(organization, CurrentPerson, true)
                {
                    ObjectNameSingular = $"{Models.FieldDefinition.Application.GetFieldDefinitionLabel()}",
                    ObjectNamePlural = $"{Models.FieldDefinition.Application.GetFieldDefinitionLabelPluralized()} associated with {organization.DisplayName}",
                    SaveFiltersInCookie = true
                };

            ProposalsGridName = "proposalsGrid";
            ProposalsGridDataUrl =
                SitkaRoute<OrganizationController>.BuildUrlFromExpression(
                    tc => tc.ProposalsGridJsonData(organization));

            ShowPendingProjects = currentPerson.CanViewPendingProjects;

            PendingProjectsGridSpec =
                new ProjectsIncludingLeadImplementingGridSpec(organization, CurrentPerson, true)
                {
                    ObjectNameSingular = $"{Models.FieldDefinition.Project.GetFieldDefinitionLabel()}",
                    ObjectNamePlural = $"{Models.FieldDefinition.Project.GetFieldDefinitionLabelPluralized()} associated with {organization.DisplayName}",
                    SaveFiltersInCookie = true
                };

            PendingProjectsGridName = "pendingProjectsGrid";
            PendingProjectsGridDataUrl =
                SitkaRoute<OrganizationController>.BuildUrlFromExpression(
                    tc => tc.PendingProjectsGridJsonData(organization));

            TenantHasCanStewardProjectsOrganizationRelationship = MultiTenantHelpers.HasCanStewardProjectsOrganizationRelationship();
            var allAssociatedProjects = Organization.GetAllAssociatedProjects();
            NumberOfStewardedProjects = allAssociatedProjects
                .Distinct()
                .Count(x => x.IsActiveProject() && x.GetCanStewardProjectsOrganization() == Organization);
            NumberOfLeadImplementedProjects = allAssociatedProjects
                .Distinct()
                .Count(x => x.IsActiveProject() && x.GetPrimaryContactOrganization() == Organization);
            NumberOfProjectsContributedTo = allAssociatedProjects.Distinct().ToList().GetActiveProjects().Count;
        }
    }
}
