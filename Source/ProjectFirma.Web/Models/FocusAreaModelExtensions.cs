/*-----------------------------------------------------------------------
<copyright file="FocusAreaModelExtensions.cs" company="Tahoe Regional Planning Agency and Environmental Science Associates">
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
using System.Web;
using GeoJSON.Net.Feature;
using ProjectFirma.Web.Controllers;
using LtInfo.Common;
using LtInfo.Common.DesignByContract;
using LtInfo.Common.GeoJson;
using ProjectFirma.Web.Common;

namespace ProjectFirma.Web.Models
{
    public static class FocusAreaModelExtensions
    {
        public static readonly UrlTemplate<int> DeleteUrlTemplate = new UrlTemplate<int>(SitkaRoute<FocusAreaController>.BuildUrlFromExpression(t => t.Delete(UrlTemplate.Parameter1Int)));


        public static HtmlString GetDisplayNameAsUrl(this FocusArea focusArea)
        {          
            return focusArea != null ? UrlTemplate.MakeHrefString(focusArea.GetDetailUrl(), focusArea.FocusAreaName) : new HtmlString("<em>Not identified</em>");
        }

        public static HtmlString GetDisplayName(this FocusArea focusArea)
        {
            return focusArea != null ? focusArea.FocusAreaName.ToHTMLFormattedString() : new HtmlString("<em>Not identified</em>");
        }

        public static readonly UrlTemplate<int> SummaryUrlTemplate = new UrlTemplate<int>(SitkaRoute<FocusAreaController>.BuildUrlFromExpression(t => t.Detail(UrlTemplate.Parameter1Int)));
        public static string GetDetailUrl(this FocusArea focusArea)
        {
            return focusArea == null ? "" : SummaryUrlTemplate.ParameterReplace(focusArea.FocusAreaID);
        }

        public static List<Project> GetAllAssociatedProjects(this FocusArea focusArea)
        {
            return focusArea.Projects.ToList();
        }

        public static List<Project> GetAllActiveProjects(this FocusArea focusArea, Person currentPerson)
        {
            return focusArea.GetAllAssociatedProjects().GetActiveProjectsVisibleToUser(currentPerson);
        }

        public static string GetDeleteFocusAreaUrl(this FocusArea focusArea)
        {
            return SitkaRoute<FocusAreaController>.BuildUrlFromExpression(t => t.Delete(focusArea.PrimaryKey));
        }

        public static bool CanFocusAreaBeDeleted(this FocusArea focusArea)
        {
            return !focusArea.HasDependentObjects();
        }


    }
}
