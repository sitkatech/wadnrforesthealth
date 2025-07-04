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

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using ProjectFirma.Web.Common;
using ProjectFirma.Web.Controllers;
using ProjectFirma.Web.Models;
using ProjectFirma.Web.Security;
using ProjectFirma.Web.Views.Project;
using ProjectFirma.Web.Views.Shared;

namespace ProjectFirma.Web.Views.DNRUplandRegion
{
    public class DetailViewData : FirmaViewData
    {
        public readonly Models.DNRUplandRegion DNRUplandRegion;
        public readonly bool UserHasRegionManagePermissions;
        public readonly string IndexUrl;
        public readonly ProjectInfoGridSpecForRegionDetail BasicProjectInfoGridSpec;
        public readonly AssociatedGrantAllocationsGridSpec AssociatedGrantAllocationsGridSpec;
        public readonly string BasicProjectInfoGridName;
        public readonly string AssociatedGrantAllocationsGridName;
        public readonly string BasicProjectInfoGridDataUrl;
        public readonly string AssociatedGrantAllocationsGridDataUrl;
        public readonly MapInitJson MapInitJson;
        public readonly ViewGoogleChartViewData ViewGoogleChartViewData;

        public string EditContactUrl { get; }
        public string EditPageContentUrl { get; }
        public bool UserHasAccountWithRole { get; }

        public DetailViewData(Person currentPerson, Models.DNRUplandRegion dnrUplandRegion, MapInitJson mapInitJson, ViewGoogleChartViewData viewGoogleChartViewData) : base(currentPerson)
        {
            DNRUplandRegion = dnrUplandRegion;
            MapInitJson = mapInitJson;
            ViewGoogleChartViewData = viewGoogleChartViewData;
            PageTitle = dnrUplandRegion.DNRUplandRegionName;
            EntityName = Models.FieldDefinition.DNRUplandRegion.GetFieldDefinitionLabel();
            UserHasRegionManagePermissions = new DNRUplandRegionManageFeature().HasPermissionByPerson(currentPerson);
            UserHasAccountWithRole = !currentPerson.IsAnonymousOrUnassigned;
            IndexUrl = SitkaRoute<DNRUplandRegionController>.BuildUrlFromExpression(x => x.Index());

            BasicProjectInfoGridName = "regionProjectListGrid";
            BasicProjectInfoGridSpec = new ProjectInfoGridSpecForRegionDetail(CurrentPerson, false)
            {
                ObjectNameSingular = $"{Models.FieldDefinition.Project.GetFieldDefinitionLabel()} in this {Models.FieldDefinition.DNRUplandRegion.GetFieldDefinitionLabel()}",
                ObjectNamePlural = $"{Models.FieldDefinition.Project.GetFieldDefinitionLabelPluralized()} in this {Models.FieldDefinition.DNRUplandRegion.GetFieldDefinitionLabel()}",
                SaveFiltersInCookie = true
            };
            BasicProjectInfoGridDataUrl = SitkaRoute<DNRUplandRegionController>.BuildUrlFromExpression(tc => tc.ProjectsGridJsonData(dnrUplandRegion));

            AssociatedGrantAllocationsGridName = "regionGrantAllocationsListGrid";
            AssociatedGrantAllocationsGridSpec = new AssociatedGrantAllocationsGridSpec()
            {
                ObjectNameSingular = $"{Models.FieldDefinition.GrantAllocation.GetFieldDefinitionLabel()} in this {Models.FieldDefinition.DNRUplandRegion.GetFieldDefinitionLabel()}",
                ObjectNamePlural = $"{Models.FieldDefinition.GrantAllocation.GetFieldDefinitionLabelPluralized()} in this {Models.FieldDefinition.DNRUplandRegion.GetFieldDefinitionLabel()}",
                SaveFiltersInCookie = true
            };
            AssociatedGrantAllocationsGridDataUrl = SitkaRoute<DNRUplandRegionController>.BuildUrlFromExpression(tc => tc.GrantAllocationsGridJsonData(dnrUplandRegion));

            EditContactUrl = SitkaRoute<DNRUplandRegionController>.BuildUrlFromExpression(x => x.EditContact(dnrUplandRegion));
            EditPageContentUrl = SitkaRoute<DNRUplandRegionController>.BuildUrlFromExpression(x => x.EditPageContent(dnrUplandRegion));

        }


    }
}
