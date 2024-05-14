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

using LtInfo.Common.AgGridWrappers;
using LtInfo.Common.HtmlHelperExtensions;
using LtInfo.Common.Views;

namespace ProjectFirma.Web.Views.Job
{
    public class JobIndexGridSpec : GridSpec<Models.vArcOnlineRawJsonImportIndex>
    {
        public JobIndexGridSpec()
        {
        }

        public JobIndexGridSpec(bool hasManageTagPermissions)
        {
            ObjectNameSingular = "Import Job";
            ObjectNamePlural = "Import Jobs";
            SaveFiltersInCookie = true;

            if (hasManageTagPermissions)
            {
                //Add(string.Empty, x => AgGridHtmlHelpers.MakeDeleteIconAndLinkBootstrap(x.DeleteUrl, true, true), 30);
            }

            const int dateWidth = 125;

            Add("RawJSONImportID", a => a.ArcOnlineFinanceApiRawJsonImportID.ToString(), 50, AgGridColumnFilterType.Numeric);
            Add(Models.FieldDefinition.JobImportTableType.ToGridHeaderString(), a => a.ArcOnlineFinanceApiRawJsonImportTableTypeName, 200, AgGridColumnFilterType.SelectFilterStrict);
            Add("Create Date", a => a.CreateDate, dateWidth, AgGridColumnFormatType.Date);
            Add("Biennium Fiscal Year", a => a.BienniumFiscalYear.ToString(), 100, AgGridColumnFilterType.Numeric);
            Add("Json Import Status Type Name", a => a.JsonImportStatusTypeName, 200, AgGridColumnFilterType.SelectFilterStrict);
            Add("Json Import Date", a => a.JsonImportDate, dateWidth, AgGridColumnFormatType.DateTime);
            Add("Finance API Last Load Date", a => a.FinanceApiLastLoadDate, dateWidth, AgGridColumnFormatType.DateTime);
            Add("JSON Data Length", a => a.RawJsonStringLength.Value.ToString(), 100);

            // Would be great to also have metadata about how many records deleted/updated/inserted.

            //Add(Models.FieldDefinition.JobImportTableType.ToGridHeaderString(), a => UrlTemplate.MakeHrefString(a.SummaryUrl, a.DisplayName), 200, AgGridColumnFilterType.Html);

        }
    }
}
