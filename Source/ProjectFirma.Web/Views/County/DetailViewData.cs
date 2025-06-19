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
using System.Linq;
using ProjectFirma.Web.Common;
using ProjectFirma.Web.Controllers;
using ProjectFirma.Web.Models;
using ProjectFirma.Web.Security;
using ProjectFirma.Web.Views.Project;
using ProjectFirma.Web.Views.Shared;

namespace ProjectFirma.Web.Views.County
{
    public class DetailViewData : FirmaViewData
    {
        public readonly Models.County County;
        public readonly bool UserHasCountyManagePermissions;
        public readonly string IndexUrl;
        public readonly BasicProjectInfoGridSpec BasicProjectInfoGridSpec;
        public readonly string BasicProjectInfoGridName;
        public readonly string BasicProjectInfoGridDataUrl;
        public readonly MapInitJson MapInitJson;

        public DetailViewData(Person currentPerson, Models.County county, MapInitJson mapInitJson) : base(currentPerson)
        {
            County = county;
            MapInitJson = mapInitJson;
            PageTitle = county.CountyName + $" {Models.FieldDefinition.County.FieldDefinitionName}";
            EntityName = Models.FieldDefinition.County.GetFieldDefinitionLabel();
            UserHasCountyManagePermissions = new CountyManageFeature().HasPermissionByPerson(currentPerson);
            IndexUrl = SitkaRoute<CountyController>.BuildUrlFromExpression(x => x.Index());

            BasicProjectInfoGridName = "countyProjectListGrid";
            BasicProjectInfoGridSpec = new BasicProjectInfoGridSpec(CurrentPerson, false)
            {
                ObjectNameSingular = $"{Models.FieldDefinition.Project.GetFieldDefinitionLabel()} in this {Models.FieldDefinition.County.GetFieldDefinitionLabel()}",
                ObjectNamePlural = $"{Models.FieldDefinition.Project.GetFieldDefinitionLabelPluralized()} in this {Models.FieldDefinition.County.GetFieldDefinitionLabel()}",
                SaveFiltersInCookie = true
            };
          
            BasicProjectInfoGridDataUrl = SitkaRoute<CountyController>.BuildUrlFromExpression(tc => tc.ProjectsGridJsonData(county));
        }

        
    }
}
