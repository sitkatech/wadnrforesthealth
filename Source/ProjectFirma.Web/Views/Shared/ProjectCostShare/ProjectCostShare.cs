using System.Web.Mvc;
using LtInfo.Common.HtmlHelperExtensions;
using LtInfo.Common.Mvc;

namespace ProjectFirma.Web.Views.Shared.ProjectCostShare
{
    public abstract class ProjectCostShare : TypedWebPartialViewPage<ProjectCostShareViewData>
    {
        public static void RenderPartialView(HtmlHelper html, ProjectCostShareViewData viewData)
        {
            html.RenderRazorSitkaPartial<ProjectCostShare, ProjectCostShareViewData>(viewData);
        }
    }
}