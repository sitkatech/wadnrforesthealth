﻿/*-----------------------------------------------------------------------
<copyright file="CustomPageGridSpec.cs" company="Tahoe Regional Planning Agency and Environmental Science Associates">
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
using LtInfo.Common;
using LtInfo.Common.AgGridWrappers;
using LtInfo.Common.HtmlHelperExtensions;
using LtInfo.Common.ModalDialog;
using LtInfo.Common.Views;
using ProjectFirma.Web.Common;
using ProjectFirma.Web.Controllers;
using ProjectFirma.Web.Models;

namespace ProjectFirma.Web.Views.CustomPage
{
    public class CustomPageGridSpec : GridSpec<Models.CustomPage>
    {
        public CustomPageGridSpec(bool hasManagePermissions)
        {            
            if (hasManagePermissions)
            {               
                Add("Delete", x => AgGridHtmlHelpers.MakeDeleteIconAndLinkBootstrap(x.DeleteUrl, true, true, true), 30, AgGridColumnFilterType.None);
                Add("Edit", a => AgGridHtmlHelpers.MakeLtInfoEditIconAsModalDialogLinkBootstrap(new ModalDialogForm(SitkaRoute<CustomPageController>.BuildUrlFromExpression(t => t.Edit(a)),
                        "Edit")),
                    30, AgGridColumnFilterType.None);
                Add("Edit Content", a => AgGridHtmlHelpers.MakeModalDialogLink("<span>Edit Content</span>",
                    SitkaRoute<CustomPageController>.BuildUrlFromExpression(y => y.EditInDialog(a)),
                    800,
                    $"Edit Content for {a.CustomPageDisplayName}",
                    true,
                    "Save",
                    "Cancel",
                    new List<string> { "gridButton" },
                    null,
                    null), 80, AgGridColumnFilterType.None);
            }
            Add("Page Name", a => a.CustomPageDisplayType != CustomPageDisplayType.Disabled ? UrlTemplate.MakeHrefString(a.AboutPageUrl, a.CustomPageDisplayName) : new HtmlString($"{a.CustomPageDisplayName}"), 180, AgGridColumnFilterType.Text);
            Add("Has Content", a => a.HasPageContent.ToYesNo(), 85, AgGridColumnFilterType.SelectFilterStrict);
            Add(Models.FieldDefinition.CustomPageDisplayType.ToGridHeaderString(), a => a.CustomPageDisplayType.CustomPageDisplayTypeDisplayName, 110, AgGridColumnFilterType.SelectFilterStrict);
            Add("Navigation Section", a => a.CustomPageNavigationSection.CustomPageNavigationSectionName, 110, AgGridColumnFilterType.SelectFilterStrict);
            Add("CustomPageID", a => a.CustomPageID, 0);

        }
    }
}
