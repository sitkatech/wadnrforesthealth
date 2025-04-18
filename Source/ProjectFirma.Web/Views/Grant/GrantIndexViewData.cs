/*-----------------------------------------------------------------------
<copyright file="GrantIndexViewData.cs" company="Tahoe Regional Planning Agency and Environmental Science Associates">
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

namespace ProjectFirma.Web.Views.Grant
{
    public class GrantIndexViewData : FirmaViewData
    {
        public readonly GrantGridSpec GrantGridSpec;
        public readonly string GrantGridName;
        public readonly string GrantGridDataUrl;

        public readonly GrantAllocationGridSpec GrantAllocationGridSpec;
        public readonly string GrantAllocationGridName;
        public readonly string GrantAllocationGridDataUrl;

        public readonly GrantAllocationGridSpec GrantAllocationNoDataGridSpec;
        public readonly string GrantAllocationNoDataGridName;
        public readonly string GrantAllocationNoDataGridDataUrl;

        public readonly bool userIsLoggedIn;

        public GrantIndexViewData(Person currentPerson, Models.FirmaPage firmaPage) : base(currentPerson, firmaPage)
        {
            PageTitle = $"Full {Models.FieldDefinition.Grant.GetFieldDefinitionLabel()} List";

            GrantGridSpec = new GrantGridSpec(currentPerson);
            GrantGridName = "grantsGridName";
            GrantGridDataUrl = SitkaRoute<GrantController>.BuildUrlFromExpression(tc => tc.GrantGridJsonData());

            GrantAllocationGridSpec = new GrantAllocationGridSpec(currentPerson, GrantAllocationGridSpec.GrantAllocationGridCreateButtonType.Hidden, null);
            GrantAllocationGridName = "grantAllocationsGridName";
            GrantAllocationGridDataUrl = SitkaRoute<GrantController>.BuildUrlFromExpression(tc => tc.AllGrantAllocationGridJsonData());

            GrantAllocationNoDataGridSpec = new GrantAllocationGridSpec(currentPerson, GrantAllocationGridSpec.GrantAllocationGridCreateButtonType.Hidden, null);
            GrantAllocationNoDataGridName = "grantAllocationsNoDataGridName";
            GrantAllocationNoDataGridDataUrl = SitkaRoute<GrantController>.BuildUrlFromExpression(tc => tc.GrantAllocationGridWithoutAnyJsonData());

            userIsLoggedIn = !currentPerson.IsAnonymousUser;
        }
    }
}
