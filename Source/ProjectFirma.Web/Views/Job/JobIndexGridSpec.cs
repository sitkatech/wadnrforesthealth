/*-----------------------------------------------------------------------
<copyright file="IndexGridSpec.cs" company="Tahoe Regional Planning Agency and Sitka Technology Group">
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

using System.Globalization;
using LtInfo.Common.DhtmlWrappers;
using LtInfo.Common.HtmlHelperExtensions;
using LtInfo.Common.Views;

namespace ProjectFirma.Web.Views.Job
{
    public class JobIndexGridSpec : GridSpec<Models.vSocrataDataMartRawJsonImportIndex>
    {
        public JobIndexGridSpec()
        {
        }

        public JobIndexGridSpec(bool hasManageTagPermissions)
        {
            ObjectNameSingular = "Socrata Import Job";
            ObjectNamePlural = "Socrata Import Jobs";
            SaveFiltersInCookie = true;

            if (hasManageTagPermissions)
            {
                //Add(string.Empty, x => DhtmlxGridHtmlHelpers.MakeDeleteIconAndLinkBootstrap(x.DeleteUrl, true, true), 30);
            }

            Add("Create Date", a => a.CreateDate.ToString(CultureInfo.InvariantCulture), 200, DhtmlxGridColumnFilterType.FormattedNumeric);
            Add(Models.FieldDefinition.JobImportTableType.ToGridHeaderString(), a => a.SocrataDataMartRawJsonImportTableTypeName, 200, DhtmlxGridColumnFilterType.SelectFilterStrict);
            Add("JSON Data Length", a => a.RawJsonStringLength.Value.ToString(), 100);

            // Would be great to also have metadata about how many records deleted/updated/inserted.

            //Add(Models.FieldDefinition.JobImportTableType.ToGridHeaderString(), a => UrlTemplate.MakeHrefString(a.SummaryUrl, a.DisplayName), 200, DhtmlxGridColumnFilterType.Html);

        }
    }
}
