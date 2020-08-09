/*-----------------------------------------------------------------------
<copyright file="IndexGridSpec.cs" company="Tahoe Regional Planning Agency and Sitka Technology Group">
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
using System.Globalization;
using System.Linq;
using System.Web;
using ProjectFirma.Web.Controllers;
using ProjectFirma.Web.Security;
using ProjectFirma.Web.Models;
using LtInfo.Common;
using LtInfo.Common.DhtmlWrappers;
using LtInfo.Common.HtmlHelperExtensions;
using LtInfo.Common.Views;
using ProjectFirma.Web.Common;

namespace ProjectFirma.Web.Views.Project
{
    public class ProjectIndexGridSpec : GridSpec<Models.Project>
    {
        public ProjectIndexGridSpec(Person currentPerson)
        {
            var userHasTagManagePermissions = new FirmaAdminFeature().HasPermissionByPerson(currentPerson);
            var userHasDeletePermissions = new ProjectDeleteFeature().HasPermissionByPerson(currentPerson);

            if (userHasTagManagePermissions)
            {
                BulkTagModalDialogForm = new BulkTagModalDialogForm(SitkaRoute<TagController>.BuildUrlFromExpression(x => x.BulkTagProjects(null)), $"Tag Checked {Models.FieldDefinition.Project.GetFieldDefinitionLabelPluralized()}", $"Tag {Models.FieldDefinition.Project.GetFieldDefinitionLabelPluralized()}");
                AddCheckBoxColumn();
                Add("ProjectID", x => x.ProjectID, 0);
            }

            if (userHasDeletePermissions)
            {
                Add(string.Empty, x => DhtmlxGridHtmlHelpers.MakeDeleteIconAndLinkBootstrap(x.GetDeleteUrl(), true, true), 30, DhtmlxGridColumnFilterType.None);
            }


            Add(string.Empty, x => MakeFactSheetUrl(x), 30, DhtmlxGridColumnFilterType.None);

            Add(Models.FieldDefinition.FhtProjectNumber.ToGridHeaderString(), x => UrlTemplate.MakeHrefString(x.GetDetailUrl(), x.FhtProjectNumber), 100, DhtmlxGridColumnFilterType.Text);
            Add(Models.FieldDefinition.ProjectName.ToGridHeaderString(), x => UrlTemplate.MakeHrefString(x.GetDetailUrl(), x.ProjectName),200, DhtmlxGridColumnFilterType.Html);
            Add(Models.FieldDefinition.ProjectType.ToGridHeaderString(), x => x.ProjectType.DisplayName, 120, DhtmlxGridColumnFilterType.SelectFilterStrict);
            Add(Models.FieldDefinition.ProjectStage.ToGridHeaderString(), x => x.ProjectStage.ProjectStageDisplayName, 90, DhtmlxGridColumnFilterType.SelectFilterStrict);

            var completedAcresHeaderString = LabelWithSugarForExtensions
                .LabelWithSugarFor(null, Models.FieldDefinition.ProjectTotalCompletedFootprintAcres, "Completed Acres")
                .ToHtmlString();
            Add(completedAcresHeaderString, x => x.TotalCompletedFootprintAcres.ToString(CultureInfo.CurrentUICulture), 100, DhtmlxGridColumnFilterType.FormattedNumeric);
            Add($"{MultiTenantHelpers.GetIsPrimaryContactOrganizationRelationship().RelationshipTypeName} Organization", x => x.GetPrimaryContactOrganization()?.DisplayName, 200, DhtmlxGridColumnFilterType.Text);
            Add($"Associated {Models.FieldDefinition.PriorityLandscape.ToGridHeaderString()}", x => x.ProjectPriorityLandscapes.FirstOrDefault()?.PriorityLandscape?.DisplayName, 125, DhtmlxGridColumnFilterType.SelectFilterStrict);
        }

        private static HtmlString MakeFactSheetUrl(Models.Project project)
        {
            // Only offer FactSheet viewer if one is actually available
            if (ProjectController.FactSheetIsAvailable(project))
            {
                return UrlTemplate.MakeHrefString(project.GetFactSheetUrl(), FirmaDhtmlxGridHtmlHelpers.FactSheetIcon.ToString());
            }

            return new HtmlString(string.Empty);
        }
    }
}
