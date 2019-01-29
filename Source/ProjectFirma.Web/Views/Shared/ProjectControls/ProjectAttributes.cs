using System;
using System.Web.Mvc;
using LtInfo.Common.HtmlHelperExtensions;
using LtInfo.Common.Mvc;

namespace ProjectFirma.Web.Views.Shared.ProjectControls
{
    public abstract class ProjectAttributes : TypedWebPartialViewPage<ProjectAttributesViewData>
    {
        public static void RenderPartialView(HtmlHelper html, ProjectAttributesViewData viewData)
        {
            html.RenderRazorSitkaPartial<ProjectAttributes, ProjectAttributesViewData>(viewData);
        }
        public string StringToDateString(string stringDate)
        {
            return DateTime.TryParse(stringDate, out var date) ? date.ToShortDateString() : null;
        }

        public static string StringToDateStringStatic(string stringDate)
        {
            return DateTime.TryParse(stringDate, out var date) ? date.ToShortDateString() : null;
        }

    }
}