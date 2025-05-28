/*-----------------------------------------------------------------------
<copyright file="IndexViewData.cs" company="Tahoe Regional Planning Agency">
Copyright (c) Tahoe Regional Planning Agency. All rights reserved.
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

using ProjectFirma.Web.Common;
using ProjectFirma.Web.Controllers;
using ProjectFirma.Web.Models;
using ProjectFirma.Web.Security;

namespace ProjectFirma.Web.Views.Project
{
    public class PendingViewData : FirmaViewData
    {
        public readonly PendingGridSpec GridSpec;
        public readonly string GridName;
        public readonly string GridDataUrl;
        public readonly bool HasCreateProjectPermissions;

        public PendingViewData(Person currentPerson, Models.FirmaPage firmaPage) : base(currentPerson, firmaPage)
        {
            PageTitle = $"Pending {Models.FieldDefinition.Project.GetFieldDefinitionLabelPluralized()}";

            HasCreateProjectPermissions = new ProjectCreateFeature().HasPermissionByPerson(CurrentPerson);

            GridSpec = new PendingGridSpec(currentPerson) {ObjectNameSingular = $"Pending {Models.FieldDefinition.Project.GetFieldDefinitionLabel()}", ObjectNamePlural = $"Pending {Models.FieldDefinition.Project.GetFieldDefinitionLabelPluralized()}", SaveFiltersInCookie = true};

            if (new ProjectCreateNewFeature().HasPermission(CurrentPerson))
            {
                GridSpec.CustomExcelDownloadUrl = SitkaRoute<ProjectController>.BuildUrlFromExpression(tc => tc.PendingExcelDownload());
            }
            if (new ProjectCreateFeature().HasPermissionByPerson(CurrentPerson))
            {
                GridSpec.CreateEntityActionPhrase = $"Propose a New {Models.FieldDefinition.Project.GetFieldDefinitionLabel()}";
                GridSpec.CreateEntityModalDialogForm = null;
            }
            GridName = "proposalsGrid";
            GridDataUrl = SitkaRoute<ProjectController>.BuildUrlFromExpression(tc => tc.PendingGridJsonData());
        }
    }
}
