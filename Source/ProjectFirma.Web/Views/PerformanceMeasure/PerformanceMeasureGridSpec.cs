/*-----------------------------------------------------------------------
<copyright file="PerformanceMeasureGridSpec.cs" company="Tahoe Regional Planning Agency and Environmental Science Associates">
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
using ProjectFirma.Web.Models;
using LtInfo.Common;
using LtInfo.Common.AgGridWrappers;
using LtInfo.Common.HtmlHelperExtensions;
using LtInfo.Common.Views;
using ProjectFirma.Web.Common;
using ProjectFirma.Web.Security;

namespace ProjectFirma.Web.Views.PerformanceMeasure
{
    public class PerformanceMeasureGridSpec : GridSpec<Models.PerformanceMeasure>
    {
        public PerformanceMeasureGridSpec(Person currentPerson)
        {
            var hasDeletePermission = new PerformanceMeasureManageFeature().HasPermissionByPerson(currentPerson);
            if (hasDeletePermission)
            {
                Add("Delete", x => x.PerformanceMeasureDataSourceType.IsCustomCalculation ? new HtmlString("") : AgGridHtmlHelpers.MakeDeleteIconAndLinkBootstrap(x.GetDeleteUrl(), true, true), 30, AgGridColumnFilterType.None);
            }
            Add(Models.FieldDefinition.PerformanceMeasure.ToGridHeaderString(MultiTenantHelpers.GetPerformanceMeasureName()),
                a => UrlTemplate.MakeHrefString(a.GetSummaryUrl(), a.PerformanceMeasureDisplayName),
                300,
                AgGridColumnFilterType.Text);
            Add(Models.FieldDefinition.MeasurementUnit.ToGridHeaderString("Unit"), a => a.MeasurementUnitType.MeasurementUnitTypeDisplayName, 80, AgGridColumnFilterType.SelectFilterStrict);
            Add(Models.FieldDefinition.PerformanceMeasureType.ToGridHeaderString("Type"), a => a.PerformanceMeasureType.PerformanceMeasureTypeDisplayName, 60, AgGridColumnFilterType.SelectFilterStrict);
            Add("Description", a => a.PerformanceMeasureDefinition, 400, AgGridColumnFilterType.Html);
            Add($"# of {Models.FieldDefinition.PerformanceMeasureSubcategory.GetFieldDefinitionLabelPluralized()}", a => a.GetRealSubcategoryCount(), 110);
            Add($"# of {Models.FieldDefinition.Project.GetFieldDefinitionLabelPluralized()}", a => a.ReportedProjectsCount(currentPerson), 80);
            Add($"Primary {MultiTenantHelpers.GetAssociatePerformanceMeasureTaxonomyLevel().GetFieldDefinition().GetFieldDefinitionLabel()}", a => a.GetPrimaryTaxonomyTier() == null ? new HtmlString(string.Empty) : a.GetPrimaryTaxonomyTier().GetDisplayNameAsUrl(), 150);
        }
    }
}
