/*-----------------------------------------------------------------------
<copyright file="DetailViewData.cs" company="Tahoe Regional Planning Agency and Environmental Science Associates">
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

using System.Collections.Generic;
using ProjectFirma.Web.Controllers;
using ProjectFirma.Web.Security;
using ProjectFirma.Web.Models;
using ProjectFirma.Web.Views.Map;
using ProjectFirma.Web.Views.Project;
using ProjectFirma.Web.Views.Shared.ProjectControls;
using ProjectFirma.Web.Views.Shared.ProjectLocationControls;
using ProjectFirma.Web.Common;
using ProjectFirma.Web.Views.PerformanceMeasure;
using ProjectFirma.Web.Views.Shared;

namespace ProjectFirma.Web.Views.ProjectType
{
    public class DetailViewData : FirmaViewData
    {
        public Models.ProjectType ProjectType { get; }
        public bool UserHasProjectTypeManagePermissions { get; }
        public string EditProjectTypeUrl { get; }
        public string IndexUrl { get; }

        public BasicProjectInfoGridSpec BasicProjectInfoGridSpec { get; }
        public string BasicProjectInfoGridName { get; }
        public string BasicProjectInfoGridDataUrl { get; }
        public ProjectTaxonomyViewData ProjectTaxonomyViewData { get; }

        public ProjectLocationsMapInitJson ProjectLocationsMapInitJson { get; }
        public ProjectLocationsMapViewData ProjectLocationsMapViewData { get; }
        public string ProjectMapFilteredUrl { get; }
        public string ProjectTypeDisplayName { get; }
        public string ProjectTypeDisplayNamePluralized { get; }


        public DetailViewData(Person currentPerson,
            Models.ProjectType projectType,
            ProjectLocationsMapInitJson projectLocationsMapInitJson,
            ProjectLocationsMapViewData projectLocationsMapViewData,
            TaxonomyLevel taxonomyLevel) : base(currentPerson)
        {
            ProjectType = projectType;
            PageTitle = projectType.DisplayName;
            var fieldDefinitionProjectType = Models.FieldDefinition.ProjectType;
            var projectTypeDisplayName = fieldDefinitionProjectType.GetFieldDefinitionLabel();
            EntityName = projectTypeDisplayName;

            ProjectLocationsMapInitJson = projectLocationsMapInitJson;
            ProjectLocationsMapViewData = projectLocationsMapViewData;
            ProjectMapFilteredUrl = ProjectLocationsMapInitJson.ProjectMapCustomization.GetCustomizedUrl();

            UserHasProjectTypeManagePermissions = new ProjectTypeManageFeature().HasPermissionByPerson(CurrentPerson);
            EditProjectTypeUrl = SitkaRoute<ProjectTypeController>.BuildUrlFromExpression(c => c.Edit(projectType));
            IndexUrl = SitkaRoute<ProgramInfoController>.BuildUrlFromExpression(x => x.Taxonomy());

            BasicProjectInfoGridName = "projectTypeProjectListGrid";
            BasicProjectInfoGridSpec = new BasicProjectInfoGridSpec(CurrentPerson, true)
            {
                ObjectNameSingular = $"{Models.FieldDefinition.Project.GetFieldDefinitionLabel()} with this {projectTypeDisplayName}",
                ObjectNamePlural = $"{Models.FieldDefinition.Project.GetFieldDefinitionLabelPluralized()} with this {projectTypeDisplayName}",
                SaveFiltersInCookie = true
            };

            BasicProjectInfoGridDataUrl = SitkaRoute<ProjectTypeController>.BuildUrlFromExpression(tc => tc.ProjectsGridJsonData(projectType));
            ProjectTaxonomyViewData = new ProjectTaxonomyViewData(projectType, taxonomyLevel);

            ProjectTypeDisplayName = projectTypeDisplayName;
            ProjectTypeDisplayNamePluralized = fieldDefinitionProjectType.GetFieldDefinitionLabelPluralized();

        }
    }
}
