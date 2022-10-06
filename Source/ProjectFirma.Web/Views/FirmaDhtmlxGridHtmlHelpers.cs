/*-----------------------------------------------------------------------
<copyright file="FirmaDhtmlxGridHtmlHelpers.cs" company="Tahoe Regional Planning Agency and Sitka Technology Group">
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
using System.Web;
using System.Web.Mvc;
using ProjectFirma.Web.Controllers;
using LtInfo.Common;
using LtInfo.Common.BootstrapWrappers;
using LtInfo.Common.DhtmlWrappers;
using ProjectFirma.Web.Common;

namespace ProjectFirma.Web.Views
{
    public static class FirmaDhtmlxGridHtmlHelpers
    {
        public static readonly HtmlString PlusIcon = BootstrapHtmlHelpers.MakeGlyphIcon("glyphicon-plus-sign gi-1x blue");
        public static readonly HtmlString FactSheetIcon = BootstrapHtmlHelpers.MakeGlyphIcon("glyphicon-search gi-1x blue");
        public static readonly HtmlString CheckIcon = BootstrapHtmlHelpers.MakeGlyphIcon("glyphicon-ok gi-1x blue");
        public static readonly HtmlString BanIcon = BootstrapHtmlHelpers.MakeGlyphIcon("glyphicon-ban-circle gi-1x red");
        public static readonly UrlTemplate<string> ExcelDownloadWithFooterUrl =
            new UrlTemplate<string>(SitkaRoute<HomeController>.BuildUrlFromExpression(x => x.ExportGridToExcel(UrlTemplate.Parameter1String)));
        public static readonly UrlTemplate<string> ExcelDownloadWithoutFooterUrl =
            new UrlTemplate<string>(SitkaRoute<HomeController>.BuildUrlFromExpression(x => x.ExportGridToExcel(UrlTemplate.Parameter1String)));

        /// <summary>
        /// All grids use this
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="html"></param>
        /// <param name="gridSpec"></param>
        /// <param name="gridName"></param>
        /// <param name="optionalGridDataUrl"></param>
        /// <param name="styleString"></param>
        /// <param name="dhtmlxGridResizeType"></param>
        /// <returns></returns>
        public static HtmlString DhtmlxGrid<T>(this HtmlHelper html, GridSpec<T> gridSpec, string gridName, string optionalGridDataUrl, string styleString, DhtmlxGridResizeType dhtmlxGridResizeType)
        {
            var dhtmlxGridHeader = DhtmlxGridHtmlHelpers.BuildDhtmlxGridHeader(gridSpec, gridName, ExcelDownloadWithFooterUrl, ExcelDownloadWithoutFooterUrl);

            var dhtmlxGrid = DhtmlxGridHtmlHelpers.DhtmlxGridImpl(gridSpec,
                gridName,
                optionalGridDataUrl,
                $"background-color:white;{styleString}",
                null, dhtmlxGridHeader, dhtmlxGridResizeType);

            return new HtmlString(dhtmlxGrid);
        }

        /// <summary>
        /// Method creates grids that exclude a download filtered grid option for WADNR-1992
        /// This method will probably go away - long term solution solution is to allow downloading the grid with filters. 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="html"></param>
        /// <param name="gridSpec"></param>
        /// <param name="gridName"></param>
        /// <param name="optionalGridDataUrl"></param>
        /// <param name="styleString"></param>
        /// <param name="dhtmlxGridResizeType"></param>
        /// <returns></returns>
        public static HtmlString DhtmlxGridCustomGridHeadersOnly<T>(this HtmlHelper html, GridSpec<T> gridSpec, string gridName, string optionalGridDataUrl, string styleString, DhtmlxGridResizeType dhtmlxGridResizeType)
        {
            var dhtmlxGridHeader = DhtmlxGridHtmlHelpers.BuildDhtmlxGridHeader(gridSpec, gridName, null, null);

            var dhtmlxGrid = DhtmlxGridHtmlHelpers.DhtmlxGridImpl(gridSpec,
                gridName,
                optionalGridDataUrl,
                $"background-color:white;{styleString}",
                null, dhtmlxGridHeader, dhtmlxGridResizeType);

            return new HtmlString(dhtmlxGrid);
        }
    }
}
