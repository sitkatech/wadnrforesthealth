﻿/*-----------------------------------------------------------------------
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
using ProjectFirma.Web.Views.Shared;

namespace ProjectFirma.Web.Views.TaxonomyBranch
{
    public class DetailViewData : FirmaViewData
    {
        public Models.TaxonomyBranch TaxonomyBranch { get; }

        public bool UserHasTaxonomyBranchManagePermissions { get; }
        public string EditTaxonomyBranchUrl { get; }

        public string IndexUrl { get; }
        public BasicProjectInfoGridSpec BasicProjectInfoGridSpec { get; }
        public string BasicProjectInfoProjectGridName { get; }
        public string BasicProjectInfoProjectGridDataUrl { get; }

        public ProjectTaxonomyViewData ProjectTaxonomyViewData { get; }

        public ProjectLocationsMapInitJson ProjectLocationsMapInitJson { get; }
        public ProjectLocationsMapViewData ProjectLocationsMapViewData { get; }
        public string ProjectMapFilteredUrl { get; }

        public string TaxonomyBranchDisplayName { get; }
        public string TaxonomyBranchDisplayNamePluralized { get; }
        public string ProjectTypeDisplayNamePluralized { get; }


        public string EditChildrenSortOrderUrl { get; }

        public DetailViewData(Person currentPerson,
            Models.TaxonomyBranch taxonomyBranch,
            ProjectLocationsMapInitJson projectLocationsMapInitJson,
            ProjectLocationsMapViewData projectLocationsMapViewData, TaxonomyLevel taxonomyLevel) : base(currentPerson)
        {
            TaxonomyBranch = taxonomyBranch;
            ProjectLocationsMapViewData = projectLocationsMapViewData;
            ProjectLocationsMapInitJson = projectLocationsMapInitJson;
            PageTitle = taxonomyBranch.DisplayName;
            var taxonomyBranchDisplayName = Models.FieldDefinition.TaxonomyBranch.GetFieldDefinitionLabel();
            TaxonomyBranchDisplayName = taxonomyBranchDisplayName;
            TaxonomyBranchDisplayNamePluralized = Models.FieldDefinition.TaxonomyBranch.GetFieldDefinitionLabelPluralized();
            ProjectTypeDisplayNamePluralized = Models.FieldDefinition.ProjectType.GetFieldDefinitionLabelPluralized();
            EntityName = taxonomyBranchDisplayName;

            ProjectMapFilteredUrl = ProjectLocationsMapInitJson.ProjectMapCustomization.GetCustomizedUrl();

            UserHasTaxonomyBranchManagePermissions = new TaxonomyBranchManageFeature().HasPermissionByPerson(CurrentPerson);
            EditTaxonomyBranchUrl = SitkaRoute<TaxonomyBranchController>.BuildUrlFromExpression(c => c.Edit(taxonomyBranch.TaxonomyBranchID));

            IndexUrl = SitkaRoute<ProgramInfoController>.BuildUrlFromExpression(c => c.Taxonomy());

            BasicProjectInfoProjectGridName = "taxonomyBranchProjectListGrid";
            BasicProjectInfoGridSpec = new BasicProjectInfoGridSpec(CurrentPerson, true)
            {
                ObjectNameSingular = $"{Models.FieldDefinition.Project.GetFieldDefinitionLabel()} with this {taxonomyBranchDisplayName}",
                ObjectNamePlural = $"{Models.FieldDefinition.Project.GetFieldDefinitionLabel()} with this {taxonomyBranchDisplayName}",
                SaveFiltersInCookie = true
            };
            BasicProjectInfoProjectGridDataUrl = SitkaRoute<TaxonomyBranchController>.BuildUrlFromExpression(tc => tc.ProjectsGridJsonData(taxonomyBranch));
            ProjectTaxonomyViewData = new ProjectTaxonomyViewData(taxonomyBranch, taxonomyLevel);

            EditChildrenSortOrderUrl = SitkaRoute<TaxonomyBranchController>.BuildUrlFromExpression(x => x.EditChildrenSortOrder(taxonomyBranch));
        }
    }
}
