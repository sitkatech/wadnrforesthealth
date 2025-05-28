/*-----------------------------------------------------------------------
<copyright file="ProjectCreateHelper.cs" company="Tahoe Regional Planning Agency and Environmental Science Associates">
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
using System.Web;
using LtInfo.Common.BootstrapWrappers;
using LtInfo.Common.ModalDialog;
using ProjectFirma.Web.Common;
using ProjectFirma.Web.Controllers;

namespace ProjectFirma.Web.Views.ProjectCreate
{
    public class ProjectCreateHelper
    {

        private static string GetAddNewProjectButtonText() =>
            $"{BootstrapHtmlHelpers.MakeGlyphIcon("glyphicon-plus")} Add {Models.FieldDefinition.Project.GetFieldDefinitionLabel()}";

        private static readonly string ProjectTypeSelectionContinueButtonText =
            $"Continue {BootstrapHtmlHelpers.MakeGlyphIcon("glyphicon-chevron-right")}";

        public static HtmlString AddProjectButton()
        {
            return ModalDialogFormHelper.ModalDialogFormLink(GetAddNewProjectButtonText(), SitkaRoute<ProjectCreateController>.BuildUrlFromExpression(x => x.InstructionsEnterHistoric(null)),
                $"Add a {Models.FieldDefinition.Project.GetFieldDefinitionLabel()}", 700, ProjectTypeSelectionContinueButtonText, "Cancel",
                new List<string> {"btn", "btn-firma"}, null, null);
        }

    }
}