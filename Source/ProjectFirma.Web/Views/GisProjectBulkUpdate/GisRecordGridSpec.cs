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

using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using LtInfo.Common;
using LtInfo.Common.AgGridWrappers;
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

        public GisRecordGridSpec(Models.Person currentPerson, List<GisUploadAttemptGisMetadataAttribute> columns, List<Models.GisFeature> gisFeatures, GisUploadAttempt gisUploadAttempt)
        {
            ObjectNameSingular = $"GIS Record";
            ObjectNamePlural = $"GIS Records";
            SaveFiltersInCookie = false;
            var gisFeatureIDs = gisFeatures.Select(x => x.GisFeatureID);
            var allAttributesOnGisUploadAttempt = HttpRequestStorage.DatabaseEntities.GisFeatureMetadataAttributes.Where(x => gisFeatureIDs.Contains(x.GisFeatureID)).ToList();
            var dictionary = allAttributesOnGisUploadAttempt.GroupBy(x => x.GisMetadataAttributeID).ToDictionary(x => x.Key, y => y.ToList());
            var columnsOrdered = columns
                .Where(x =>! string.Equals(x.GisMetadataAttribute.GisMetadataAttributeName, "Shape", StringComparison.InvariantCultureIgnoreCase))
                .Where(x => dictionary.ContainsKey(x.GisMetadataAttributeID))
                .OrderBy(x => x.SortOrder)
                .ToList();


            Add("ID", x => UrlTemplate.MakeHrefString(x.GetDetailUrl(), x.GisFeatureID.ToString()), 90, AgGridColumnFilterType.SelectFilterHtmlStrict);
            Add("Is Valid", x => (x.IsValid ?? false).ToString(), 90, AgGridColumnFilterType.SelectFilterStrict);
            Add("Calculated Area in Acres", x => x.CalculatedArea.ToString(), 90, AgGridColumnFilterType.Numeric);

            foreach (var fGetColumnNamesForTableResult in columnsOrdered)
            {
                Add(fGetColumnNamesForTableResult.GisMetadataAttribute.GisMetadataAttributeName
                    , x => x.GisFeatureMetadataAttributes.SingleOrDefault(y => y.GisMetadataAttributeID == fGetColumnNamesForTableResult.GisMetadataAttributeID)?.GisFeatureMetadataAttributeValue, 90, AgGridColumnFilterType.Text);
            }

        }
    }
}
