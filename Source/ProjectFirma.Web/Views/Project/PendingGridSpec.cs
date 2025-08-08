/*-----------------------------------------------------------------------
<copyright file="PendingGridSpec.cs" company="Tahoe Regional Planning Agency and Environmental Science Associates">
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

using LtInfo.Common;
using LtInfo.Common.AgGridWrappers;
using LtInfo.Common.HtmlHelperExtensions;
using LtInfo.Common.Views;
using ProjectFirma.Web.Common;
using ProjectFirma.Web.Models;
using ProjectFirma.Web.Security;

namespace ProjectFirma.Web.Views.Project
{
    public class PendingGridSpec : GridSpec<Models.Project>
    {
        public PendingGridSpec(Person currentPerson)
        {
            // todo: fulfill "Include standard project grid with columns for “Stage” and “Approval Status”
            Add("Delete", x => AgGridHtmlHelpers.MakeDeleteIconAndLinkBootstrap(x.GetDeleteProposalUrl(), new ProjectDeleteProposalFeature().HasPermission(currentPerson, x).HasPermission, true, true), 30, AgGridColumnFilterType.None);
            Add("Edit", x => AgGridHtmlHelpers.MakeEditIconAsHyperlinkBootstrap(x.GetProjectCreateUrl(), new ProjectCreateFeature().HasPermission(currentPerson, x).HasPermission), 30, AgGridColumnFilterType.None);
            Add(Models.FieldDefinition.FhtProjectNumber.ToGridHeaderString(), x => UrlTemplate.MakeHrefString(x.GetDetailUrl(), x.FhtProjectNumber), 100, AgGridColumnFilterType.Text);
            Add(Models.FieldDefinition.ProjectName.ToGridHeaderString(), x => new HtmlLinkObject(x.ProjectName,x.GetDetailUrl()).ToJsonObjectForAgGrid(), 300, AgGridColumnFilterType.HtmlLinkJson);
            Add("Submittal Status", a => a.ProjectApprovalStatus.ProjectApprovalStatusDisplayName, 110, AgGridColumnFilterType.SelectFilterStrict);
            Add(Models.FieldDefinition.ProjectStage.ToGridHeaderString(), x => x.ProjectStage.ProjectStageDisplayName, 90, AgGridColumnFilterType.SelectFilterStrict);
            if (MultiTenantHelpers.HasCanStewardProjectsOrganizationRelationship())
            {
                Add(Models.FieldDefinition.ProjectsStewardOrganizationRelationshipToProject.ToGridHeaderString(), x => x.GetCanStewardProjectsOrganization().GetShortNameAsAgGridLinkJson(), 150,
                    AgGridColumnFilterType.HtmlLinkJson);
            }
            Add(Models.FieldDefinition.PrimaryContactOrganization.ToGridHeaderString(), x => x.GetPrimaryContactOrganization().GetShortNameAsAgGridLinkJson(), 150, AgGridColumnFilterType.HtmlLinkJson);
            Add(Models.FieldDefinition.ProjectType.ToGridHeaderString(), x => x.ProjectType == null ? string.Empty : x.ProjectType.DisplayName, 300, AgGridColumnFilterType.Html);
            Add(Models.FieldDefinition.ProjectInitiationDate.ToGridHeaderString(), x => x.GetPlannedDate(), 90, AgGridColumnFilterType.SelectFilterStrict);
            Add(Models.FieldDefinition.ExpirationDate.ToGridHeaderString(), x => x.GetExpirationDateFormatted(), 115, AgGridColumnFilterType.SelectFilterStrict);
            Add(Models.FieldDefinition.CompletionDate.ToGridHeaderString(), x => x.GetCompletionDateFormatted(), 90, AgGridColumnFilterType.SelectFilterStrict);
            Add(Models.FieldDefinition.EstimatedTotalCost.ToGridHeaderString(), x => x.EstimatedTotalCost, 100, AgGridColumnFormatType.CurrencyWithCents, AgGridColumnAggregationType.Total);
            Add(Models.FieldDefinition.ProjectFundSourceAllocationRequestTotalAmount.ToGridHeaderString(), x => x.GetTotalFunding(), 100, AgGridColumnFormatType.CurrencyWithCents, AgGridColumnAggregationType.Total);
            
            Add("Submitted Date", a => a.SubmissionDate, 120);
            Add("Last Updated", a => a.LastUpdateDate, 120);
            Add(Models.FieldDefinition.ProjectDescription.ToGridHeaderString(), x => x.ProjectDescription, 300);
        }
    }
}
