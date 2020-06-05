using System.Web.Mvc;
using LtInfo.Common.HtmlHelperExtensions;
using LtInfo.Common.Mvc;

namespace ProjectFirma.Web.Views.Grant
{
    public abstract class GrantDetailsFileDetails : TypedWebPartialViewPage<GrantDetailsFileDetailsViewData>
    {
        public static void RenderPartialView(HtmlHelper html, GrantDetailsFileDetailsViewData viewData)
        {
            html.RenderRazorSitkaPartial<GrantDetailsFileDetails, GrantDetailsFileDetailsViewData>(viewData);
        }
    }
}