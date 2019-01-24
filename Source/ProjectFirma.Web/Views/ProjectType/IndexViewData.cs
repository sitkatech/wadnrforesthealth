/*-----------------------------------------------------------------------
<copyright file="IndexViewData.cs" company="Tahoe Regional Planning Agency and Sitka Technology Group">
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
using ProjectFirma.Web.Controllers;
using ProjectFirma.Web.Security;
using ProjectFirma.Web.Models;
using LtInfo.Common.ModalDialog;
using ProjectFirma.Web.Common;

namespace ProjectFirma.Web.Views.ProjectType
{
    public class IndexViewData : FirmaViewData
    {
        public IndexGridSpec GridSpec{get;}
        public string GridName{get;}
        public string GridDataUrl{get;}
        public string EditSortOrderUrl { get; }

        public IndexViewData(Person currentPerson, Models.FirmaPage firmaPage) : base(currentPerson, firmaPage)
        {
            var projectTypeDisplayNamePluralized = Models.FieldDefinition.ProjectType.GetFieldDefinitionLabelPluralized();
            PageTitle = projectTypeDisplayNamePluralized;

            var hasProjectTypeManagePermissions = new ProjectTypeManageFeature().HasPermissionByPerson(currentPerson);
            var projectTypeDisplayName = Models.FieldDefinition.ProjectType.GetFieldDefinitionLabel();
            GridSpec = new IndexGridSpec(currentPerson)
            {
                ObjectNameSingular = projectTypeDisplayName,
                ObjectNamePlural = projectTypeDisplayNamePluralized,
                SaveFiltersInCookie = true
            };

            if (hasProjectTypeManagePermissions)
            {
                GridSpec.CreateEntityModalDialogForm = new ModalDialogForm(SitkaRoute<ProjectTypeController>.BuildUrlFromExpression(t => t.New()), string.Format("Create a new {0}", projectTypeDisplayName));
            }

            GridName = "projectTypesGrid";
            GridDataUrl = SitkaRoute<ProjectTypeController>.BuildUrlFromExpression(tc => tc.IndexGridJsonData());
            EditSortOrderUrl = SitkaRoute<ProjectTypeController>.BuildUrlFromExpression(tc => tc.EditSortOrder());
        }
    }
}
