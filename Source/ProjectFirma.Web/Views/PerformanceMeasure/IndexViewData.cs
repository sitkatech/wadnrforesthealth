﻿/*-----------------------------------------------------------------------
<copyright file="IndexViewData.cs" company="Tahoe Regional Planning Agency and Environmental Science Associates">
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
using ProjectFirma.Web.Controllers;
using ProjectFirma.Web.Models;
using LtInfo.Common.ModalDialog;
using ProjectFirma.Web.Common;
using ProjectFirma.Web.Security;

namespace ProjectFirma.Web.Views.PerformanceMeasure
{
    public class IndexViewData : FirmaViewData
    {        
        public PerformanceMeasureGridSpec PerformanceMeasureGridSpec{ get; }
        public string PerformanceMeasureGridName{ get; }
        public string PerformanceMeasureGridDataUrl{ get; }
        public string EditSortOrderUrl { get; }
        public bool HasPerformanceMeasureManagePermissions { get; }

        public IndexViewData(Person currentPerson, Models.FirmaPage firmaPage) : base(currentPerson, firmaPage)
        {
            PageTitle = MultiTenantHelpers.GetPerformanceMeasureNamePluralized();

            var hasPerformanceMeasureManagePermissions = new PerformanceMeasureManageFeature().HasPermissionByPerson(currentPerson);

            HasPerformanceMeasureManagePermissions = hasPerformanceMeasureManagePermissions;
            PerformanceMeasureGridSpec = new PerformanceMeasureGridSpec(currentPerson) {
                ObjectNameSingular = MultiTenantHelpers.GetPerformanceMeasureName(),
                ObjectNamePlural = MultiTenantHelpers.GetPerformanceMeasureNamePluralized(),
                SaveFiltersInCookie = true
            };

            if (HasPerformanceMeasureManagePermissions)
            {
                var contentUrl = SitkaRoute<PerformanceMeasureController>.BuildUrlFromExpression(c => c.New());
                PerformanceMeasureGridSpec.CreateEntityModalDialogForm = new ModalDialogForm(contentUrl, $"Create a new {Models.FieldDefinition.PerformanceMeasure.GetFieldDefinitionLabel()}");

            }
            PerformanceMeasureGridSpec.CustomExcelDownloadLinkText = $"Download with {Models.FieldDefinition.PerformanceMeasureSubcategory.GetFieldDefinitionLabelPluralized()}";
            PerformanceMeasureGridSpec.CustomExcelDownloadUrl = SitkaRoute<PerformanceMeasureController>.BuildUrlFromExpression(tc => tc.IndexExcelDownload());

            PerformanceMeasureGridName = "performanceMeasuresGrid";
            PerformanceMeasureGridDataUrl = SitkaRoute<PerformanceMeasureController>.BuildUrlFromExpression(c => c.PerformanceMeasureGridJsonData());
            EditSortOrderUrl = SitkaRoute<PerformanceMeasureController>.BuildUrlFromExpression(x => x.EditSortOrder());
        }
    }
}
