/*-----------------------------------------------------------------------
<copyright file="DetailViewData.cs" company="Tahoe Regional Planning Agency and Sitka Technology Group">
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
using LtInfo.Common;
using LtInfo.Common.DhtmlWrappers;
using ProjectFirma.Web.Common;
using ProjectFirma.Web.Controllers;
using ProjectFirma.Web.Models;
using ProjectFirma.Web.Security;
using ProjectFirma.Web.Views.GrantAllocation;
using ProjectFirma.Web.Views.Shared;
using ProjectFirma.Web.Views.Shared.GrantAllocationControls;
using ProjectFirma.Web.Views.Shared.TextControls;

namespace ProjectFirma.Web.Views.GrantAllocationAward
{
    public class DetailViewData : FirmaViewData
    {
        public string BackButtonUrl { get; private set; }
        public string BackButtonText { get; set; }
        public string EditGrantAllocationAwardUrl { get; set; }
        public Models.GrantAllocationAward GrantAllocationAward { get; private set; }
        public bool UserHasEditGrantAllocationAwardPermissions { get; private set; }
        public string EditIndirectCostUrl { get; private set; }
        public string EditSuppliesUrl { get; private set; }
        public string EditPersonnelAndBenefitsUrl { get; private set; }
        public string EditTravelUrl { get; private set; }


        public DetailViewData(Person currentPerson, Models.GrantAllocationAward grantAllocationAward, string backButtonUrl, string backButtonText) : base(currentPerson)
        {
            PageTitle = $"{Models.FieldDefinition.GrantAllocationAward.GetFieldDefinitionLabel()}: {grantAllocationAward.GrantAllocationAwardName}";
            BreadCrumbTitle = $"{Models.FieldDefinition.GrantAllocationAward.GetFieldDefinitionLabel()} Detail";

            BackButtonUrl = backButtonUrl;
            BackButtonText = backButtonText;

            GrantAllocationAward = grantAllocationAward;
            UserHasEditGrantAllocationAwardPermissions = new GrantAllocationAwardEditAsAdminFeature().HasPermissionByPerson(currentPerson);

            EditGrantAllocationAwardUrl = SitkaRoute<GrantAllocationAwardController>.BuildUrlFromExpression(x => x.Edit(grantAllocationAward.PrimaryKey));


        }

        
    }
}
