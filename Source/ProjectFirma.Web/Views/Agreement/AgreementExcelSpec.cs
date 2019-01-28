/*-----------------------------------------------------------------------
<copyright file="GrantExcelSpec.cs" company="Tahoe Regional Planning Agency and Sitka Technology Group">
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

using LtInfo.Common;
using LtInfo.Common.ExcelWorkbookUtilities;
using LtInfo.Common.HtmlHelperExtensions;
using LtInfo.Common.Views;

namespace ProjectFirma.Web.Views.Agreement
{
    public class AgreementExcelSpec : ExcelWorksheetSpec<Models.Agreement>
    {
        public AgreementExcelSpec()
        {
            AddColumn("Type", x => x.AgreementType?.AgreementTypeAbbrev);
            AddColumn("Number", x => x.AgreementNumber);
            AddColumn(Models.FieldDefinition.FundingSource.GetFieldDefinitionLabel(), x => x.Grant.GrantTitle);
            AddColumn(Models.FieldDefinition.Organization.GetFieldDefinitionLabel(), x => x.Organization.DisplayName);
            AddColumn("Title", x => x.AgreementTitle);
            AddColumn("Start Date", x => x.StartDateDisplay);
            AddColumn("End Date", x => x.EndDateDisplay);
            AddColumn("Amount", x => x.AgreementAmount.ToStringCurrency());
        }
    }
}
