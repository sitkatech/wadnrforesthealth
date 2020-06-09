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
using System.Linq;
using LtInfo.Common;
using LtInfo.Common.DhtmlWrappers;
using LtInfo.Common.HtmlHelperExtensions;
using LtInfo.Common.ModalDialog;
using LtInfo.Common.Views;
using ProjectFirma.Web.Common;
using ProjectFirma.Web.Controllers;
using ProjectFirma.Web.Models;
using ProjectFirma.Web.Security;
using ProjectFirma.Web.Views.Grant;

namespace ProjectFirma.Web.Views.GisProjectBulkUpdate
{
    public class GisRecordGridSpec : GridSpec<GisProjectBulkUpdateController.GisRecord>
    {

        public GisRecordGridSpec(Models.Person currentPerson, List<fGetColumnNamesForTable_Result> columns)
        {
            ObjectNameSingular = $"GIS Record";
            ObjectNamePlural = $"GIS Records";
            SaveFiltersInCookie = false;

            var columnsOrdered = columns.Where(x => x.PrimaryKey != 1).Where(x =>! string.Equals(x.ColumnName, "Shape", StringComparison.InvariantCultureIgnoreCase)).OrderBy(x => x.PrimaryKey).ToList();


            Add("ID", x => x.ID.ToString(), 90, DhtmlxGridColumnFilterType.SelectFilterStrict);
            Add("Is Valid", x => x.Feature.STIsValid().ToString(), 90, DhtmlxGridColumnFilterType.SelectFilterStrict);

            foreach (var fGetColumnNamesForTableResult in columnsOrdered)
            {
                Add(fGetColumnNamesForTableResult.ColumnName, x => x.GisColumnNames.Single(y => string.Equals(y.ColumnnName, fGetColumnNamesForTableResult.ColumnName,StringComparison.InvariantCultureIgnoreCase)).ColumnValue, 90, DhtmlxGridColumnFilterType.Text);
            }

        }
    }
}
