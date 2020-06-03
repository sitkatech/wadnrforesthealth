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

using LtInfo.Common.ModalDialog;
using ProjectFirma.Web.Controllers;
using ProjectFirma.Web.Models;
using ProjectFirma.Web.Common;
using ProjectFirma.Web.Security;

namespace ProjectFirma.Web.Views.FocusArea
{
    public class IndexViewData : FirmaViewData
    {
        public readonly MapInitJson MapInitJson;
        public readonly IndexGridSpec GridSpec;
        public readonly string GridName;
        public readonly string GridDataUrl;

        public IndexViewData(Person currentPerson, MapInitJson mapInitJson, Models.FirmaPage firmaPage) : base(currentPerson, firmaPage)
        {
            PageTitle = Models.FieldDefinition.FocusArea.GetFieldDefinitionLabelPluralized();
            MapInitJson = mapInitJson;
            GridSpec = new IndexGridSpec(currentPerson) {ObjectNameSingular = Models.FieldDefinition.FocusArea.GetFieldDefinitionLabel(), ObjectNamePlural = Models.FieldDefinition.FocusArea.GetFieldDefinitionLabelPluralized(), SaveFiltersInCookie = true};
            var hasFocusAreaManagePermissions = new FocusAreaManageFeature().HasPermissionByPerson(currentPerson);


            if (hasFocusAreaManagePermissions)
            {
                var contentUrl = SitkaRoute<FocusAreaController>.BuildUrlFromExpression(t => t.New());
                GridSpec.CreateEntityModalDialogForm = new ModalDialogForm(contentUrl, $"Create a new {Models.FieldDefinition.FocusArea.GetFieldDefinitionLabel()}");
            }


            GridName = "focusAreasGrid";
            GridDataUrl = SitkaRoute<FocusAreaController>.BuildUrlFromExpression(tc => tc.IndexGridJsonData());
        }
    }
}
