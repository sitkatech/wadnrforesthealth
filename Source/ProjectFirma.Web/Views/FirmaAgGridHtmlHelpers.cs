﻿/*-----------------------------------------------------------------------
<copyright file="FirmaAgGridHtmlHelpers.cs" company="Tahoe Regional Planning Agency and Environmental Science Associates">
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
using System.Web.Mvc;
using ProjectFirma.Web.Controllers;
using LtInfo.Common;
using LtInfo.Common.BootstrapWrappers;
using LtInfo.Common.AgGridWrappers;
using LtInfo.Common.Mvc;
using ProjectFirma.Web.Common;

namespace ProjectFirma.Web.Views
{
    public static class FirmaAgGridHtmlHelpers
    {
        public static readonly HtmlString PlusIcon = BootstrapHtmlHelpers.MakeGlyphIcon("glyphicon-plus-sign gi-1x blue");
        public static readonly HtmlString FactSheetIcon = BootstrapHtmlHelpers.MakeGlyphIcon("glyphicon-search gi-1x blue");
        public static readonly HtmlString CheckIcon = BootstrapHtmlHelpers.MakeGlyphIcon("glyphicon-ok gi-1x blue");
        public static readonly HtmlString BanIcon = BootstrapHtmlHelpers.MakeGlyphIcon("glyphicon-ban-circle gi-1x red");
        public static readonly HtmlString EnvelopeIcon = BootstrapHtmlHelpers.MakeGlyphIcon("glyphicon-envelope gi-1x blue");
        public static readonly HtmlString EditIcon = BootstrapHtmlHelpers.MakeGlyphIcon("glyphicon-edit gi-1x blue");
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
        /// <param name="agGridResizeType"></param>
        /// <returns></returns>
        public static HtmlString AgGrid<T>(this HtmlHelper html, GridSpec<T> gridSpec, string gridName, string optionalGridDataUrl, string styleString, AgGridResizeType agGridResizeType)
        {


            var dhtmlxGrid = AgGridHtmlHelpers.AgGridImpl(gridSpec,
                gridName,
                optionalGridDataUrl,
                $"background-color:white;{styleString}",
                agGridResizeType);

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
        public static HtmlString AgGridCustomGridHeadersOnly<T>(this HtmlHelper html, GridSpec<T> gridSpec, string gridName, string optionalGridDataUrl, string styleString, AgGridResizeType dhtmlxGridResizeType)
        {

            var dhtmlxGrid = AgGridHtmlHelpers.AgGridImpl(gridSpec,
                gridName,
                optionalGridDataUrl,
                $"background-color:white;{styleString}",
                dhtmlxGridResizeType);

            return new HtmlString(dhtmlxGrid);
        }


    }
}
