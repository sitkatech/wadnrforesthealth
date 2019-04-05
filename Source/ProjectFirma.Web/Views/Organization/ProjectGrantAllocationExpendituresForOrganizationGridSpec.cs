using System;
using System.Collections.Generic;
using System.Globalization;
using LtInfo.Common;
using LtInfo.Common.DhtmlWrappers;
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
                a => UrlTemplate.MakeHrefString(a.Project.GetDetailUrl(), a.Project.DisplayName),
                250,
                DhtmlxGridColumnFilterType.Html);
            if (MultiTenantHelpers.HasCanStewardProjectsOrganizationRelationship())
            {
                Add(Models.FieldDefinition.ProjectsStewardOrganizationRelationshipToProject.ToGridHeaderString(), x => x.Project.GetCanStewardProjectsOrganization().GetDisplayNameAsUrl(), 150,
                    DhtmlxGridColumnFilterType.Html);
            }
            Add(Models.FieldDefinition.PrimaryContactOrganization.ToGridHeaderString(), x => x.Project.GetPrimaryContactOrganization().GetDisplayNameAsUrl(), 150, DhtmlxGridColumnFilterType.Html);
            Add(Models.FieldDefinition.ProjectStage.ToGridHeaderString(), x => x.Project.ProjectStage.ProjectStageDisplayName, 90, DhtmlxGridColumnFilterType.SelectFilterStrict);
            Add(Models.FieldDefinition.OrganizationType.ToGridHeaderString(), x => x.GrantAllocation.BottommostOrganization.OrganizationType?.OrganizationTypeName, 80, DhtmlxGridColumnFilterType.SelectFilterStrict);
            Add(Models.FieldDefinition.GrantAllocation.ToGridHeaderString(), x => UrlTemplate.MakeHrefString(x.GrantAllocation.SummaryUrl, x.GrantAllocation.DisplayName), 120);
            Add(Models.FieldDefinition.Organization.ToGridHeaderString(), x => UrlTemplate.MakeHrefString(x.GrantAllocation.BottommostOrganization.GetDetailUrl(), x.GrantAllocation.BottommostOrganization.DisplayName), 120);
            Add($"Provided by {Models.FieldDefinition.Organization.GetFieldDefinitionLabel()}", x => (x.GrantAllocation.BottommostOrganization == organization).ToYesOrEmpty(), 100, DhtmlxGridColumnFilterType.SelectFilterHtmlStrict);
            Add($"Received from other {Models.FieldDefinition.Organization.GetFieldDefinitionLabel()}", x => (x.GrantAllocation.BottommostOrganization != organization).ToYesOrEmpty(), 100, DhtmlxGridColumnFilterType.SelectFilterHtmlStrict);
            Add(Models.FieldDefinition.ReportingYear.ToGridHeaderString(), x => x.CalendarYear, 80, DhtmlxGridColumnFormatType.Date, DhtmlxGridColumnFilterType.SelectFilterStrict);
            Add("Amount", x => x.ExpenditureAmount, 80, DhtmlxGridColumnFormatType.Currency, DhtmlxGridColumnAggregationType.Total);
        }
    }
}