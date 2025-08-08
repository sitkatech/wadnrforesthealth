/*-----------------------------------------------------------------------
<copyright file="FundSourceIndexViewData.cs" company="Tahoe Regional Planning Agency and Environmental Science Associates">
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
using ProjectFirma.Web.Common;

namespace ProjectFirma.Web.Views.FundSource
{
    public class FundSourceIndexViewData : FirmaViewData
    {
        public readonly FundSourceGridSpec FundSourceGridSpec;
        public readonly string FundSourceGridName;
        public readonly string FundSourceGridDataUrl;

        public readonly FundSourceAllocationGridSpec FundSourceAllocationGridSpec;
        public readonly string FundSourceAllocationGridName;
        public readonly string FundSourceAllocationGridDataUrl;

        public readonly FundSourceAllocationGridSpec FundSourceAllocationNoDataGridSpec;
        public readonly string FundSourceAllocationNoDataGridName;
        public readonly string FundSourceAllocationNoDataGridDataUrl;

        public readonly bool userIsLoggedIn;

        public FundSourceIndexViewData(Person currentPerson, Models.FirmaPage firmaPage) : base(currentPerson, firmaPage)
        {
            PageTitle = $"Full {Models.FieldDefinition.FundSource.GetFieldDefinitionLabel()} List";

            FundSourceGridSpec = new FundSourceGridSpec(currentPerson);
            FundSourceGridName = "fundSourcesGridName";
            FundSourceGridDataUrl = SitkaRoute<FundSourceController>.BuildUrlFromExpression(tc => tc.FundSourceGridJsonData());

            FundSourceAllocationGridSpec = new FundSourceAllocationGridSpec(currentPerson, FundSourceAllocationGridSpec.FundSourceAllocationGridCreateButtonType.Shown, null);
            FundSourceAllocationGridName = "fundSourceAllocationsGridName";
            FundSourceAllocationGridDataUrl = SitkaRoute<FundSourceController>.BuildUrlFromExpression(tc => tc.AllFundSourceAllocationGridJsonData());

            FundSourceAllocationNoDataGridSpec = new FundSourceAllocationGridSpec(currentPerson, FundSourceAllocationGridSpec.FundSourceAllocationGridCreateButtonType.Shown, null);
            FundSourceAllocationNoDataGridName = "fundSourceAllocationsNoDataGridName";
            FundSourceAllocationNoDataGridDataUrl = SitkaRoute<FundSourceController>.BuildUrlFromExpression(tc => tc.FundSourceAllocationGridWithoutAnyJsonData());

            userIsLoggedIn = !currentPerson.IsAnonymousUser;
        }
    }
}
