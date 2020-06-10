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

using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using LtInfo.Common;
using LtInfo.Common.DhtmlWrappers;
using LtInfo.Common.HtmlHelperExtensions;
using LtInfo.Common.ModalDialog;
using LtInfo.Common.Views;
using Microsoft.SqlServer.Types;
using ProjectFirma.Web.Common;
using ProjectFirma.Web.Controllers;
using ProjectFirma.Web.Models;
using ProjectFirma.Web.Security;
using ProjectFirma.Web.Views.Grant;

namespace ProjectFirma.Web.Views.GisProjectBulkUpdate
{
    public class GisRecordGridSpec : GridSpec<Models.GisFeature>
    {

        public GisRecordGridSpec(Models.Person currentPerson, List<GisUploadAttemptGisMetadataAttribute> columns)
        {
            ObjectNameSingular = $"GIS Record";
            ObjectNamePlural = $"GIS Records";
            SaveFiltersInCookie = false;

            var columnsOrdered = columns.Where(x => x.SortOrder != 1).Where(x =>! string.Equals(x.GisMetadataAttribute.GisMetadataAttributeName, "Shape", StringComparison.InvariantCultureIgnoreCase)).OrderBy(x => x.SortOrder).ToList();


            Add("ID", x => UrlTemplate.MakeHrefString(x.GetDetailUrl(), x.GisFeatureID.ToString()), 90, DhtmlxGridColumnFilterType.SelectFilterHtmlStrict);
            Add("Is Valid", x => (x.IsValid ?? false).ToString(), 90, DhtmlxGridColumnFilterType.SelectFilterStrict);

            foreach (var fGetColumnNamesForTableResult in columnsOrdered)
            {
                Add(fGetColumnNamesForTableResult.GisMetadataAttribute.GisMetadataAttributeName, x => x.GisFeatureMetadataAttributes.Single(y => y.GisMetadataAttributeID == fGetColumnNamesForTableResult.GisMetadataAttributeID).GisFeatureMetadataAttributeValue, 90, DhtmlxGridColumnFilterType.Text);
            }

        }
    }
}
