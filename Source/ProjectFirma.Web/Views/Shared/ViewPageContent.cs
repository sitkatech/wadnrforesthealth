﻿
using System.Web.Mvc;
using LtInfo.Common.HtmlHelperExtensions;
using LtInfo.Common.Mvc;
using ProjectFirma.Web.Models;

namespace ProjectFirma.Web.Views.Shared
{
   
    public abstract class ViewPageContent : TypedWebPartialViewPage<ViewPageContentViewData>
    {
        public static void RenderPartialView(HtmlHelper html, ViewPageContentViewData viewData)
        {
            html.RenderRazorSitkaPartial<ViewPageContent, ViewPageContentViewData>(viewData);
        }
    }
}
