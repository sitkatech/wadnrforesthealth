/*-----------------------------------------------------------------------
<copyright file="FundSourceExcelSpec.cs" company="Tahoe Regional Planning Agency and Environmental Science Associates">
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

using LtInfo.Common.ExcelWorkbookUtilities;
using ProjectFirma.Web.Common;
using ProjectFirma.Web.Models;

namespace ProjectFirma.Web.Views.FundSource
{
    public class FundSourceAllocationExcelSpec : ExcelWorksheetSpec<Models.FundSourceAllocation>
    {
        public FundSourceAllocationExcelSpec()
        {
            AddColumn(Models.FieldDefinition.FundSourceNumber.FieldDefinitionDisplayName, x => x.FundSource.FundSourceNumber);
            AddColumn(Models.FieldDefinition.FundSourceAllocationName.FieldDefinitionDisplayName, x => x.FundSourceAllocationName);
            AddColumn(Models.FieldDefinition.ProgramManager.FieldDefinitionDisplayName, x => x.GetAllProgramManagerPersonNamesAsString());
            AddColumn(Models.FieldDefinition.FundSourceStartDate.FieldDefinitionDisplayName, x => x.StartDate);
            AddColumn(Models.FieldDefinition.FundSourceEndDate.FieldDefinitionDisplayName, x => x.EndDate);
            AddColumn($"Parent FundSource {Models.FieldDefinition.FundSourceStatus.FieldDefinitionDisplayName}", x => x.FundSource.FundSourceStatus.FundSourceStatusName);
            AddColumn(Models.FieldDefinition.DNRUplandRegion.FieldDefinitionDisplayName, x => x.DNRUplandRegion?.DNRUplandRegionName ?? string.Empty);
            AddColumn(Models.FieldDefinition.FederalFundCode.FieldDefinitionDisplayName, x => x.FederalFundCodeDisplay);
            AddColumn(Models.FieldDefinition.AllocationAmount.FieldDefinitionDisplayName, x => x.AllocationAmount);
            AddColumn(Models.FieldDefinition.ProgramIndexProjectCode.FieldDefinitionDisplayName, x => x.GetAssociatedProgramIndexProjectCodePairsCommaDelimited());
            AddColumn(Models.FieldDefinition.Organization.FieldDefinitionDisplayName, x => x.Organization?.OrganizationName ?? string.Empty);
        }
    }

    public class FundSourceExcelSpec : ExcelWorksheetSpec<Models.FundSource>
    {
        public FundSourceExcelSpec()
        {
            AddColumn(Models.FieldDefinition.FundSourceNumber.FieldDefinitionDisplayName, x => x.FundSourceNumber);
            AddColumn(Models.FieldDefinition.CFDA.FieldDefinitionDisplayName, x => x.CFDANumber);
            AddColumn(Models.FieldDefinition.FundSourceName.FieldDefinitionDisplayName, x => x.FundSourceName);
            AddColumn(Models.FieldDefinition.TotalAwardAmount.FieldDefinitionDisplayName, x => x.TotalAwardAmount);
            AddColumn(Models.FieldDefinition.FundSourceStartDate.FieldDefinitionDisplayName, x => x.StartDate);
            AddColumn(Models.FieldDefinition.FundSourceEndDate.FieldDefinitionDisplayName, x => x.EndDate);
            AddColumn(Models.FieldDefinition.FundSourceStatus.FieldDefinitionDisplayName, x => x.FundSourceStatus.FundSourceStatusName);
            AddColumn(Models.FieldDefinition.FundSourceType.FieldDefinitionDisplayName, x => x.FundSourceTypeDisplay);
        }
    }
}
