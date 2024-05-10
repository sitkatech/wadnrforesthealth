/*-----------------------------------------------------------------------
<copyright file="IndexGridSpec.cs" company="Tahoe Regional Planning Agency and Environmental Science Associates">
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
using System.Globalization;
using System.Linq;
using System.Web;
using ProjectFirma.Web.Controllers;
using ProjectFirma.Web.Security;
using ProjectFirma.Web.Models;
using LtInfo.Common;
using LtInfo.Common.AgGridWrappers;
using LtInfo.Common.HtmlHelperExtensions;
using LtInfo.Common.Views;
using Microsoft.Ajax.Utilities;
using ProjectFirma.Web.Common;

namespace ProjectFirma.Web.Views.Project
{
    public class ProjectIndexGridSpec : GridSpec<Models.Project>
    {
        public ProjectIndexGridSpec(Person currentPerson
            , bool allowTaggingFunctionality
            , bool allowDeleteFunctionality
            , Dictionary<int, vTotalTreatedAcresByProject> totalTreatedAcresByProjectDictionary)
        {
            var userHasTagManagePermissions = new FirmaAdminFeature().HasPermissionByPerson(currentPerson);

            if (userHasTagManagePermissions && allowTaggingFunctionality)
            {
                AddMasterCheckBoxColumn();
                Add("ProjectID", x => x.ProjectID, 0);
                BulkTagModalDialogForm = new BulkTagModalDialogForm(SitkaRoute<TagController>.BuildUrlFromExpression(x => x.BulkTagProjects(null)), $"Tag Checked {Models.FieldDefinition.Project.GetFieldDefinitionLabelPluralized()}", $"Tag {Models.FieldDefinition.Project.GetFieldDefinitionLabelPluralized()}");
                
            }


            Add(string.Empty, x => MakeFactSheetUrl(x), 30, AgGridColumnFilterType.None);

            Add(Models.FieldDefinition.FhtProjectNumber.ToGridHeaderString(), x => $"{{ \"link\":\"{x.GetDetailUrl()}\",\"displayText\":\"{x.FhtProjectNumber}\" }}", 125, AgGridColumnFilterType.HtmlLinkJson);
            Add(Models.FieldDefinition.ProjectName.ToGridHeaderString(), x => $"{{ \"link\":\"{x.GetDetailUrl()}\",\"displayText\":\"{x.ProjectName}\" }}",225, AgGridColumnFilterType.HtmlLinkJson);
            Add(Models.FieldDefinition.ProjectType.ToGridHeaderString(), x => x.ProjectType.DisplayName, 120, AgGridColumnFilterType.SelectFilterStrict);
            Add(Models.FieldDefinition.ProjectStage.ToGridHeaderString(), x => x.ProjectStage.ProjectStageDisplayName, 90, AgGridColumnFilterType.SelectFilterStrict);

            Add(Models.FieldDefinition.ProjectTotalCompletedTreatmentAcres.ToGridHeaderString(), x => TotalTreatedAcres(x,totalTreatedAcresByProjectDictionary), 100, AgGridColumnFormatType.Decimal );
            Add($"{MultiTenantHelpers.GetIsPrimaryContactOrganizationRelationship().RelationshipTypeName} Organization", x => x.GetPrimaryContactOrganization()?.DisplayName, 200, AgGridColumnFilterType.SelectFilterStrict);
            Add(Models.FieldDefinition.Program.ToGridHeaderStringPlural("Programs"), x => x.ProjectPrograms.ToProgramListDisplay(false), 90, AgGridColumnFilterType.SelectFilterHtmlStrict);
            Add($"Associated {Models.FieldDefinition.PriorityLandscape.ToGridHeaderString()}", x => x.ProjectPriorityLandscapes.FirstOrDefault()?.PriorityLandscape?.DisplayName, 125, AgGridColumnFilterType.SelectFilterStrict);
            Add($"Associated {Models.FieldDefinition.County.ToGridHeaderString()}", x => x.ProjectCounties.FirstOrDefault()?.County?.CountyName, 125, AgGridColumnFilterType.SelectFilterStrict);
        }

        private static HtmlString MakeFactSheetUrl(Models.Project project)
        {
            // Only offer FactSheet viewer if one is actually available
            if (ProjectController.FactSheetIsAvailable(project))
            {
                return UrlTemplate.MakeHrefString(project.GetFactSheetUrl(), AgGridHtmlHelpers.FactSheetIcon.ToString());
            }

            return new HtmlString(string.Empty);
        }
        

        private decimal TotalTreatedAcres(Models.Project project,
            Dictionary<int, vTotalTreatedAcresByProject> totalTreatedAcresByProjectDictionary)
        {
            var total = totalTreatedAcresByProjectDictionary.ContainsKey(project.ProjectID) && totalTreatedAcresByProjectDictionary[project.ProjectID].TotalTreatedAcres.HasValue
                ?  totalTreatedAcresByProjectDictionary[project.ProjectID].TotalTreatedAcres.Value
                : (decimal) 0.0;
           return Math.Round(total, 2);
        }
    }
}