/*-----------------------------------------------------------------------
<copyright file="ProjectController.cs" company="Tahoe Regional Planning Agency and Sitka Technology Group">
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

using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using LtInfo.Common.ExcelWorkbookUtilities;
using ProjectFirma.Web.Security;
using ProjectFirma.Web.Common;
using ProjectFirma.Web.Models;
using LtInfo.Common.MvcResults;
using ProjectFirma.Web.Views.Grant;

namespace ProjectFirma.Web.Controllers
{
    public class GrantController : FirmaBaseController
    {
        [GrantsViewFullListFeature]
        public ViewResult Index()
        {
            var firmaPage = FirmaPage.GetFirmaPageByPageType(FirmaPageType.FullProjectList);
            var viewData = new GrantIndexViewData(CurrentPerson, firmaPage);
            return RazorView<GrantIndex, GrantIndexViewData>(viewData);
        }


        [GrantsViewFullListFeature]
        public GridJsonNetJObjectResult<Grant> GrantGridJsonData()
        {
            var gridSpec = new GrantGridSpec(CurrentPerson);
            var grants = HttpRequestStorage.DatabaseEntities.Grants.ToList();
            var gridJsonNetJObjectResult = new GridJsonNetJObjectResult<Grant>(grants, gridSpec);
            return gridJsonNetJObjectResult;
        }

        [GrantsViewFullListFeature]
        public GridJsonNetJObjectResult<GrantAllocation> GrantAllocationGridJsonData()
        {
            var gridSpec = new GrantAllocationGridSpec(CurrentPerson);
            var grantAllocations = HttpRequestStorage.DatabaseEntities.GrantAllocations.ToList();
            var gridJsonNetJObjectResult = new GridJsonNetJObjectResult<GrantAllocation>(grantAllocations, gridSpec);
            return gridJsonNetJObjectResult;
        }

        [GrantsViewFullListFeature]
        public ExcelResult GrantsExcelDownload()
        {
            var grants = HttpRequestStorage.DatabaseEntities.Grants.ToList();
            var grantAllocations = HttpRequestStorage.DatabaseEntities.GrantAllocations.ToList();
            var workbookTitle = FieldDefinition.Grant.GetFieldDefinitionLabelPluralized();
            return GrantsExcelDownloadImpl(grants, grantAllocations, workbookTitle);
        }

        private ExcelResult GrantsExcelDownloadImpl(List<Grant> grants, List<GrantAllocation> grantAllocations, string workbookTitle)
        {
            var workSheets = new List<IExcelWorkbookSheetDescriptor>();

            // Grants
            var grantSpec = new GrantExcelSpec();
            var wsGrants = ExcelWorkbookSheetDescriptorFactory.MakeWorksheet($"{FieldDefinition.Grant.GetFieldDefinitionLabelPluralized()}", grantSpec, grants);
            workSheets.Add(wsGrants);

            // Grant Allocations
            var grantAllocationsSpec = new GrantAllocationExcelSpec();
            var wsGrantAllocations = ExcelWorkbookSheetDescriptorFactory.MakeWorksheet($"{FieldDefinition.GrantAllocation.GetFieldDefinitionLabelPluralized()}", grantAllocationsSpec, grantAllocations);
            workSheets.Add(wsGrantAllocations);

            // Overall excel file
            var wbm = new ExcelWorkbookMaker(workSheets);
            var excelWorkbook = wbm.ToXLWorkbook();

            return new ExcelResult(excelWorkbook, workbookTitle);
        }















    }
}
