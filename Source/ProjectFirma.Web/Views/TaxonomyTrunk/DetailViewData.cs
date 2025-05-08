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
using ProjectFirma.Web.Views.PerformanceMeasure;
using ProjectFirma.Web.Views.Shared;

namespace ProjectFirma.Web.Views.TaxonomyTrunk
{
    public class DetailViewData : FirmaViewData
    {
        public Models.TaxonomyTrunk TaxonomyTrunk { get; }
        public bool UserHasTaxonomyTrunkManagePermissions { get; }
        public bool UserHasProjectTaxonomyTrunkExpenditureManagePermissions { get; }
        public string EditTaxonomyTrunkUrl { get; }
        public string TaxonomyBranchIndexUrl { get; }

        public string IndexUrl { get; }
        public BasicProjectInfoGridSpec BasicProjectInfoGridSpec { get; }
        public string BasicProjectInfoGridName { get; }
        public string BasicProjectInfoGridDataUrl { get; }

        public ProjectLocationsMapInitJson ProjectLocationsMapInitJson { get; }
        public ProjectLocationsMapViewData ProjectLocationsMapViewData { get; }
        public string ProjectMapFilteredUrl { get; }

        public ProjectTaxonomyViewData ProjectTaxonomyViewData { get; }

        public string TaxonomyTrunkDisplayName { get; }
        public string TaxonomyTrunkDisplayNamePluralized { get; }
        public string TaxonomyBranchDisplayNamePluralized { get; }
        public string ProjectTypeDisplayNamePluralized { get; }

        public string EditChildrenSortOrderUrl { get; }

        public DetailViewData(Person currentPerson,
            Models.TaxonomyTrunk taxonomyTrunk,
            ProjectLocationsMapInitJson projectLocationsMapInitJson,
            ProjectLocationsMapViewData projectLocationsMapViewData, TaxonomyLevel taxonomyLevel) : base(currentPerson)
        {
            TaxonomyTrunk = taxonomyTrunk;
            TaxonomyTrunkDisplayName = Models.FieldDefinition.TaxonomyTrunk.GetFieldDefinitionLabel();
            TaxonomyTrunkDisplayNamePluralized = Models.FieldDefinition.TaxonomyTrunk.GetFieldDefinitionLabelPluralized();
            TaxonomyBranchDisplayNamePluralized = Models.FieldDefinition.TaxonomyBranch.GetFieldDefinitionLabelPluralized();
            ProjectTypeDisplayNamePluralized = Models.FieldDefinition.ProjectType.GetFieldDefinitionLabelPluralized();

            ProjectLocationsMapInitJson = projectLocationsMapInitJson;
            ProjectLocationsMapViewData = projectLocationsMapViewData;

            ProjectMapFilteredUrl = ProjectLocationsMapInitJson.ProjectMapCustomization.GetCustomizedUrl();

            PageTitle = taxonomyTrunk.DisplayName;
            EntityName = TaxonomyTrunkDisplayName;
            IndexUrl = SitkaRoute<ProgramInfoController>.BuildUrlFromExpression(c => c.Taxonomy());

            UserHasTaxonomyTrunkManagePermissions = new TaxonomyTrunkManageFeature().HasPermissionByPerson(CurrentPerson);
            UserHasProjectTaxonomyTrunkExpenditureManagePermissions = new TaxonomyTrunkManageFeature().HasPermissionByPerson(currentPerson);
            EditTaxonomyTrunkUrl = SitkaRoute<TaxonomyTrunkController>.BuildUrlFromExpression(c => c.Edit(taxonomyTrunk.TaxonomyTrunkID));
            TaxonomyBranchIndexUrl = SitkaRoute<TaxonomyBranchController>.BuildUrlFromExpression(c => c.Index());

            BasicProjectInfoGridName = "taxonomyTrunkProjectListGrid";
            BasicProjectInfoGridSpec = new BasicProjectInfoGridSpec(CurrentPerson, true)
            {
                ObjectNameSingular = $"{Models.FieldDefinition.Project.GetFieldDefinitionLabel()} with this {TaxonomyTrunkDisplayName}",
                ObjectNamePlural = $"{Models.FieldDefinition.Project.GetFieldDefinitionLabelPluralized()} with this {TaxonomyTrunkDisplayName}",
                SaveFiltersInCookie = true
            };

            BasicProjectInfoGridDataUrl = SitkaRoute<TaxonomyTrunkController>.BuildUrlFromExpression(tc => tc.ProjectsGridJsonData(taxonomyTrunk));
            ProjectTaxonomyViewData = new ProjectTaxonomyViewData(taxonomyTrunk, taxonomyLevel);

            EditChildrenSortOrderUrl = SitkaRoute<TaxonomyTrunkController>.BuildUrlFromExpression(x => x.EditChildrenSortOrder(taxonomyTrunk));
        }
    }
}
