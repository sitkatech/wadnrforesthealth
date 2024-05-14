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
using System.Web;
using ProjectFirma.Web.Models;
using ProjectFirma.Web.Security;
using LtInfo.Common;
using LtInfo.Common.AgGridWrappers;
using LtInfo.Common.HtmlHelperExtensions;
using LtInfo.Common.ModalDialog;
using LtInfo.Common.Views;
using ProjectFirma.Web.Common;
using ProjectFirma.Web.Controllers;

namespace ProjectFirma.Web.Views.Program
{
    public class ProgramGridSpec : GridSpec<Models.Program>
    {
        public ProgramGridSpec(Person currentPerson, Models.Organization organization)
        {
            var hasProgramManagePermissions = new ProgramManageFeature().HasPermissionByPerson(currentPerson);

            if (hasProgramManagePermissions)
            {
                var contentUrl = SitkaRoute<ProgramController>.BuildUrlFromExpression(t => t.New());

                if (organization != null)
                {
                    contentUrl = SitkaRoute<ProgramController>.BuildUrlFromExpression(t => t.NewProgram(organization.OrganizationID));
                }

                CreateEntityModalDialogForm = new ModalDialogForm(contentUrl, $"Create a new {Models.FieldDefinition.Program.GetFieldDefinitionLabel()}");
            }

            if (hasProgramManagePermissions)
            {
                Add("Delete", x => AgGridHtmlHelpers.MakeDeleteIconAndLinkBootstrap(x.GetDeleteUrl(), !x.IsDefaultProgramForImportOnly, true), 30, AgGridColumnFilterType.None, true);
            }
            Add("Program", a => UrlTemplate.MakeHrefString(a.GetDetailUrl(), a.ProgramNameDisplay), 400, AgGridColumnFilterType.Html);
            Add(Models.FieldDefinition.Organization.ToGridHeaderString("Parent Organization"), a => UrlTemplate.MakeHrefString(a.Organization.GetDetailUrl(), a.Organization.OrganizationName), 400, AgGridColumnFilterType.Html);
            Add("Short Name", a => a.ProgramShortName, 100);
            Add("Project Count", a => a.ProjectPrograms.Count, 60);
            Add("Is Active", a => a.ProgramIsActive.ToYesNo(), 80, AgGridColumnFilterType.SelectFilterStrict);
            Add("Is Default for Bulk Import Only", a => a.IsDefaultProgramForImportOnly.ToYesNo(), 80, AgGridColumnFilterType.SelectFilterStrict);
        }
    }
}
