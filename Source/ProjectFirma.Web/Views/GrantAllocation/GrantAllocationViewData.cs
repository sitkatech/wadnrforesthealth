/*-----------------------------------------------------------------------
<copyright file="GrantViewData.cs" company="Tahoe Regional Planning Agency and Environmental Science Associates">
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

using ProjectFirma.Web.Common;
using ProjectFirma.Web.Controllers;
using ProjectFirma.Web.Models;
using ProjectFirma.Web.Security;

namespace ProjectFirma.Web.Views.GrantAllocation
{
    public abstract class GrantAllocationViewData : FirmaViewData
    {
        public Models.GrantAllocation GrantAllocation { get; }
        public string EditGrantAllocationUrl { get; set; }

        public bool UserHasEditGrantAllocationPermissions { get; set; }

        public string BackToGrantAllocationsText { get; set; }

        public string GrantAllocationsListUrl { get; set; }
        
        

        protected GrantAllocationViewData(Person currentPerson, Models.GrantAllocation grantAllocation) : base(currentPerson, null)
        {
            GrantAllocation = grantAllocation;
            HtmlPageTitle = grantAllocation.GrantAllocationName;
            EntityName = $"{Models.FieldDefinition.GrantAllocation.GetFieldDefinitionLabel()}";
            EditGrantAllocationUrl = grantAllocation.GetEditUrl();

            UserHasEditGrantAllocationPermissions = new GrantAllocationEditAsAdminFeature().HasPermissionByPerson(currentPerson);
            BackToGrantAllocationsText = $"Back to all {Models.FieldDefinition.GrantAllocation.GetFieldDefinitionLabelPluralized()}";
            GrantAllocationsListUrl = SitkaRoute<GrantController>.BuildUrlFromExpression(c => c.Index());
        }
    }
}
