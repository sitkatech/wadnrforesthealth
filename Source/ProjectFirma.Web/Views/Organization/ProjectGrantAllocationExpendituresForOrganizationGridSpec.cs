using System;
using System.Collections.Generic;
using System.Globalization;
using LtInfo.Common;
using LtInfo.Common.AgGridWrappers;
using LtInfo.Common.HtmlHelperExtensions;
using LtInfo.Common.Views;
using ProjectFirma.Web.Common;
using ProjectFirma.Web.Models;
using ProjectFirma.Web.Views.Project;

namespace ProjectFirma.Web.Views.Organization
{    
    public class ProjectGrantAllocationExpendituresForOrganizationGridSpec : GridSpec<Models.ProjectGrantAllocationExpenditure>
    {
        public ProjectGrantAllocationExpendituresForOrganizationGridSpec(Models.Organization organization)
        {
            Add(Models.FieldDefinition.Project.ToGridHeaderString(),
                a => new HtmlLinkObject(a.Project.DisplayName,a.Project.GetDetailUrl()).ToJsonObjectForAgGrid(),
                250,
                AgGridColumnFilterType.HtmlLinkJson);
            if (MultiTenantHelpers.HasCanStewardProjectsOrganizationRelationship())
            {
                Add(Models.FieldDefinition.ProjectsStewardOrganizationRelationshipToProject.ToGridHeaderString(), x => x.Project.GetCanStewardProjectsOrganization().GetDisplayNameAsAgGridLink(), 150,
                    AgGridColumnFilterType.HtmlLinkJson);
            }
            Add(Models.FieldDefinition.PrimaryContactOrganization.ToGridHeaderString(), x => x.Project.GetPrimaryContactOrganization().GetDisplayNameAsAgGridLink(), 150, AgGridColumnFilterType.HtmlLinkJson);
            Add(Models.FieldDefinition.ProjectStage.ToGridHeaderString(), x => x.Project.ProjectStage.ProjectStageDisplayName, 90, AgGridColumnFilterType.SelectFilterStrict);
            Add(Models.FieldDefinition.OrganizationType.ToGridHeaderString(), x => x.GrantAllocation.BottommostOrganization.OrganizationType?.OrganizationTypeName, 80, AgGridColumnFilterType.SelectFilterStrict);
            Add(Models.FieldDefinition.GrantAllocation.ToGridHeaderString(), x => UrlTemplate.MakeHrefString(x.GrantAllocation.SummaryUrl, x.GrantAllocation.DisplayName), 120);
            Add(Models.FieldDefinition.Organization.ToGridHeaderString(), x => UrlTemplate.MakeHrefString(x.GrantAllocation.BottommostOrganization.GetDetailUrl(), x.GrantAllocation.BottommostOrganization.DisplayName), 120);
            Add($"Provided by {Models.FieldDefinition.Organization.GetFieldDefinitionLabel()}", x => (x.GrantAllocation.BottommostOrganization == organization).ToYesOrEmpty(), 100, AgGridColumnFilterType.SelectFilterHtmlStrict);
            Add($"Received from other {Models.FieldDefinition.Organization.GetFieldDefinitionLabel()}", x => (x.GrantAllocation.BottommostOrganization != organization).ToYesOrEmpty(), 100, AgGridColumnFilterType.SelectFilterHtmlStrict);
            Add(Models.FieldDefinition.ReportingYear.ToGridHeaderString(), x => x.CalendarYear, 80, AgGridColumnFormatType.Date, AgGridColumnFilterType.SelectFilterStrict);
            Add("Amount", x => x.ExpenditureAmount, 80, AgGridColumnFormatType.CurrencyWithCents, AgGridColumnAggregationType.Total);
        }
    }
}