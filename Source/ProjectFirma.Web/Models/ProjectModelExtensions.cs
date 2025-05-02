/*-----------------------------------------------------------------------
<copyright file="ProjectModelExtensions.cs" company="Tahoe Regional Planning Agency and Environmental Science Associates">
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
using System.Linq;
using System.Web;
using ProjectFirma.Web.Controllers;
using LtInfo.Common;
using LtInfo.Common.AgGridWrappers;
using LtInfo.Common.DesignByContract;
using LtInfo.Common.Models;
using Microsoft.Ajax.Utilities;
using ProjectFirma.Web.Common;

namespace ProjectFirma.Web.Models
{
    public static class ProjectModelExtensions
    {
        public static readonly UrlTemplate<int> DetailUrlTemplate = new UrlTemplate<int>(SitkaRoute<ProjectController>.BuildUrlFromExpression(t => t.Detail(UrlTemplate.Parameter1Int)));
        public static readonly UrlTemplate<int> DetailUrlTemplateAbsolute = new UrlTemplate<int>(SitkaRoute<ProjectController>.BuildAbsoluteUrlHttpsFromExpression(t => t.Detail(UrlTemplate.Parameter1Int)));
        public static string GetDetailUrl(this Project project)
        {
            return DetailUrlTemplate.ParameterReplace(project.ProjectID);
        }
        public static string GetDetailUrlAbsolute(this Project project)
        {
            return DetailUrlTemplateAbsolute.ParameterReplace(project.ProjectID);
        }

        public static readonly UrlTemplate<int> EditUrlTemplate = new UrlTemplate<int>(SitkaRoute<ProjectController>.BuildUrlFromExpression(t => t.Edit(UrlTemplate.Parameter1Int)));
        public static string GetEditUrl(this Project project)
        {
            return project.IsProposal() ? SitkaRoute<ProjectCreateController>.BuildUrlFromExpression(t => t.EditBasics(project.ProjectID)) : EditUrlTemplate.ParameterReplace(project.ProjectID);
        }

        public static readonly UrlTemplate<int> ProjectCreateUrlTemplate = new UrlTemplate<int>(SitkaRoute<ProjectCreateController>.BuildUrlFromExpression(t => t.EditBasics(UrlTemplate.Parameter1Int)));
        public static string GetProjectCreateUrl(this Project project)
        {
            return ProjectCreateUrlTemplate.ParameterReplace(project.ProjectID);
        }

        public static readonly UrlTemplate<int> FactSheetUrlTemplate = new UrlTemplate<int>(SitkaRoute<ProjectController>.BuildUrlFromExpression(t => t.FactSheet(UrlTemplate.Parameter1Int)));
        public static string GetFactSheetUrl(this Project project)
        {
            return FactSheetUrlTemplate.ParameterReplace(project.ProjectID);
        }

        public static readonly UrlTemplate<int> DownloadLandOwnerAssistanceApprovalLetter = new UrlTemplate<int>(SitkaRoute<ReportsController>.BuildUrlFromExpression(t => t.DownloadLandOwnerAssistanceApprovalLetter(UrlTemplate.Parameter1Int)));
        public static string GetDownloadLandOwnerAssistanceApprovalLetterUrl(this Project project)
        {
            return DownloadLandOwnerAssistanceApprovalLetter.ParameterReplace(project.ProjectID);
        }

        public static readonly UrlTemplate<int> DeleteUrlTemplate = new UrlTemplate<int>(SitkaRoute<ProjectController>.BuildUrlFromExpression(t => t.DeleteProject(UrlTemplate.Parameter1Int)));
        public static string GetDeleteUrl(this Project project)
        {
            return DeleteUrlTemplate.ParameterReplace(project.ProjectID);
        }

        public static readonly UrlTemplate<int> DeleteProposalUrlTemplate = new UrlTemplate<int>(SitkaRoute<ProjectCreateController>.BuildUrlFromExpression(t => t.DeleteProjectProposal(UrlTemplate.Parameter1Int)));
        public static string GetDeleteProposalUrl(this Project project)
        {
            return DeleteProposalUrlTemplate.ParameterReplace(project.ProjectID);
        }

        public static readonly UrlTemplate<int> ProjectUpdateUrlTemplate = new UrlTemplate<int>(SitkaRoute<ProjectUpdateController>.BuildUrlFromExpression(t => t.Instructions(UrlTemplate.Parameter1Int)));
        public static string GetProjectUpdateUrl(this Project project)
        {
            return ProjectUpdateUrlTemplate.ParameterReplace(project.ProjectID);
        }

        public static readonly UrlTemplate<int> ProjectMapPopuUrlTemplate = new UrlTemplate<int>(SitkaRoute<ProjectController>.BuildUrlFromExpression(t => t.ProjectMapPopup(UrlTemplate.Parameter1Int)));
        public static string GetProjectMapPopupUrl(this Project project)
        {
            return ProjectMapPopuUrlTemplate.ParameterReplace(project.ProjectID);
        }

        public static readonly UrlTemplate<int> ProjectMapSimplePopuUrlTemplate = new UrlTemplate<int>(SitkaRoute<ProjectController>.BuildUrlFromExpression(t => t.ProjectSimpleMapPopup(UrlTemplate.Parameter1Int)));
        public static string GetProjectSimpleMapPopupUrl(this Project project)
        {
            return ProjectMapSimplePopuUrlTemplate.ParameterReplace(project.ProjectID);
        }

        public static List<int> GetProjectUpdateImplementationStartToCompletionDateRange(this IProject projectUpdate)
        {
            var startYear = projectUpdate.GetImplementationStartYear();
            return GetYearRangesImpl(projectUpdate, !startYear.HasValue ?  null : (DateTime?) new DateTime(startYear.Value, 1, 1) );
        }

        public static List<int> GetProjectUpdatePlanningDesignStartToCompletionDateRange(this IProject projectUpdate)
        {
            var startYear = projectUpdate?.PlannedDate;
            return GetYearRangesImpl(projectUpdate, startYear);
        }

        public static List<ProjectExemptReportingYear> GetPerformanceMeasuresExemptReportingYears(this Project project)
        {
            return project.ProjectExemptReportingYears
                .Where(x => x.ProjectExemptReportingType == ProjectExemptReportingType.PerformanceMeasures)
                .OrderBy(x => x.CalendarYear).ToList();
        }
        public static List<ProjectExemptReportingYear> GetExpendituresExemptReportingYears(this Project project)
        {
            return project.ProjectExemptReportingYears
                .Where(x => x.ProjectExemptReportingType == ProjectExemptReportingType.Expenditures)
                .OrderBy(x => x.CalendarYear).ToList();
        }

        private static List<int> GetYearRangesImpl(IProject projectUpdate, DateTime? startDate)
        {
            var currentYearToUse = FirmaDateUtilities.CalculateCurrentYearToUseForUpToAllowableInputInReporting();
            if (projectUpdate != null)
            {
                if (startDate.HasValue && startDate.Value.Year < MultiTenantHelpers.GetMinimumYear() &&
                    (projectUpdate.GetCompletionYear().HasValue && projectUpdate.GetCompletionYear().Value < MultiTenantHelpers.GetMinimumYear()))
                {
                    // both start and completion year are before the minimum year, so no year range required
                    return new List<int>();
                }

                if (startDate.HasValue && startDate.Value.Year > currentYearToUse && (projectUpdate.GetCompletionYear().HasValue && projectUpdate.GetCompletionYear().Value > currentYearToUse))
                {
                    return new List<int>();
                }

                if (startDate.HasValue && projectUpdate.GetCompletionYear().HasValue && startDate.Value.Year > projectUpdate.GetCompletionYear().Value)
                {
                    return new List<int>();
                }
            }
            return FirmaDateUtilities.CalculateCalendarYearRangeAccountingForExistingYears(new List<int>(),
                startDate,
                projectUpdate.GetCompletionYear(),
                currentYearToUse,
                MultiTenantHelpers.GetMinimumYear(),
                currentYearToUse);
        }

        /// <summary>
        /// Returns the organizations that appear in this project's Expected Funding or Reported Funding
        /// Returns as ProjectOrganization with a dummy "Funder" RelationshipType, which lives as a static property of the RelationshipType class
        /// </summary>
        /// <returns></returns>
        public static List<ProjectOrganizationRelationship> GetFundingOrganizations(this Project project, string organizationFieldDefinitionLabelSingle, string organizationFieldDefinitionLabelPluralized, Dictionary<int, List<ProjectGrantAllocationExpenditure>> projectGrantAllocationExpenditureDict)
        {
           
            var thisListOfProjectGrantAllocationExpenditures =
                projectGrantAllocationExpenditureDict.ContainsKey(project.ProjectID)
                    ? projectGrantAllocationExpenditureDict[project.ProjectID]
                    : new List<ProjectGrantAllocationExpenditure>();
            var relationshipTypeFunder = new RelationshipType(ModelObjectHelpers.NotYetAssignedID, "Funder", false, false, false, string.Empty, true, true);
            var fundingOrganizations = thisListOfProjectGrantAllocationExpenditures.Select(x => x.GrantAllocation.BottommostOrganization)
                .Union(project.ProjectGrantAllocationRequests.Select(x => x.GrantAllocation.BottommostOrganization)).Distinct()
                .Select(x => new ProjectOrganizationRelationship(project, x, relationshipTypeFunder));
            Check.Ensure(fundingOrganizations.All(fo => fo.Organization != null), $"Must have {organizationFieldDefinitionLabelSingle} set for all Funding {organizationFieldDefinitionLabelPluralized}");
            return fundingOrganizations.ToList();
        }

        public static List<ProjectOrganizationRelationship> GetAssociatedOrganizations(this Project project
            , string organizationFieldDefinitionLabelSingle
            , string organizationFieldDefinitionLabelPluralized
            , Dictionary<int, List<ProjectGrantAllocationExpenditure>> projectGrantAllocationExpenditureDict)
        {

 

            var explicitOrganizations = project.ProjectOrganizations.Select(x => new ProjectOrganizationRelationship(project, x.Organization, x.RelationshipType)).ToList();
            explicitOrganizations.AddRange(project.GetFundingOrganizations(organizationFieldDefinitionLabelSingle, organizationFieldDefinitionLabelPluralized, projectGrantAllocationExpenditureDict));
            return explicitOrganizations.DistinctBy(x => new {x.Project.ProjectID, x.Organization.OrganizationID})
                .ToList();
        }

        public static string GetProjectFundingSourcesAsCommaSeparatedList(this Project project)
        {
            if (project.ProjectFundingSources.Any())
            {
                var fundingSourceDisplayNames = project.ProjectFundingSources.Select(pfs => pfs.FundingSource.FundingSourceDisplayName);
                var fundingSourcesForDisplay = string.Join(", ", fundingSourceDisplayNames);
                return fundingSourcesForDisplay;
            }

            return $"No {FieldDefinition.FundingSource.FieldDefinitionDisplayName} selected.";
        }

        public static Organization GetLeadImplementer(this Project project)
        {
            return project.ProjectOrganizations.Where(x => x.RelationshipTypeID == RelationshipType.LeadImplementerID).Select(x => x.Organization).SingleOrDefault();
        }

        public static string GetExpectedFundingGrantAllocationsAsCommaDelimitedListForAgGrid(this Project project)
        {
            var grantAllocations = project.ProjectGrantAllocationRequests.Select(x => x.GrantAllocation);
            return grantAllocations.Select(x => new HtmlLinkObject(x.DisplayName, x.SummaryUrl)).ToJsonArrayForAgGrid();
        }
    }
}
