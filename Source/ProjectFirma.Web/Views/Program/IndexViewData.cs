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
using ProjectFirma.Web.Security;
using ProjectFirma.Web.Controllers;
using ProjectFirma.Web.Models;
using LtInfo.Common.ModalDialog;
using ProjectFirma.Web.Common;

namespace ProjectFirma.Web.Views.Program
{
    public class IndexViewData : FirmaViewData
    {
        public IndexGridSpec GridSpec { get; }
        public string GridName { get; }
        public string GridDataUrl { get; }

        public IndexViewData(Person currentPerson, Models.FirmaPage firmaPage)
            : base(currentPerson, firmaPage)
        {
            PageTitle = $"{Models.FieldDefinition.Program.GetFieldDefinitionLabelPluralized()}";

            var hasProgramManagePermissions = new ProgramManageFeature().HasPermissionByPerson(currentPerson);
            GridSpec = new IndexGridSpec(currentPerson, hasProgramManagePermissions)
            {
                ObjectNameSingular = $"{Models.FieldDefinition.Program.GetFieldDefinitionLabel()}",
                ObjectNamePlural = $"{Models.FieldDefinition.Program.GetFieldDefinitionLabelPluralized()}",
                SaveFiltersInCookie = true
            };

            //if (hasProgramManagePermissions)
            //{
            //    var contentUrl = SitkaRoute<ProgramController>.BuildUrlFromExpression(t => t.New());
            //    GridSpec.CreateEntityModalDialogForm = new ModalDialogForm(contentUrl, $"Create a new {Models.FieldDefinition.Organization.GetFieldDefinitionLabel()}");
            //}

            GridName = "programsGrid";
            GridDataUrl = SitkaRoute<ProgramController>.BuildUrlFromExpression(tc => tc.IndexGridJsonData());
        }
    }
}
