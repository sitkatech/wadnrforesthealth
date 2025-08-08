/*-----------------------------------------------------------------------
<copyright file="FundSourceViewData.cs" company="Tahoe Regional Planning Agency and Environmental Science Associates">
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

namespace ProjectFirma.Web.Views.FundSourceAllocation
{
    public abstract class FundSourceAllocationViewData : FirmaViewData
    {
        public Models.FundSourceAllocation FundSourceAllocation { get; }
        public string EditFundSourceAllocationUrl { get; set; }

        public bool UserHasEditFundSourceAllocationPermissions { get; set; }

        public string BackToFundSourceAllocationsText { get; set; }

        public string FundSourceAllocationsListUrl { get; set; }
        
        

        protected FundSourceAllocationViewData(Person currentPerson, Models.FundSourceAllocation fundSourceAllocation) : base(currentPerson, null)
        {
            FundSourceAllocation = fundSourceAllocation;
            HtmlPageTitle = fundSourceAllocation.FundSourceAllocationName;
            EntityName = $"{Models.FieldDefinition.FundSourceAllocation.GetFieldDefinitionLabel()}";
            EditFundSourceAllocationUrl = fundSourceAllocation.GetEditUrl();

            UserHasEditFundSourceAllocationPermissions = new FundSourceAllocationEditAsAdminFeature().HasPermissionByPerson(currentPerson);
            BackToFundSourceAllocationsText = $"Back to all {Models.FieldDefinition.FundSourceAllocation.GetFieldDefinitionLabelPluralized()}";
            FundSourceAllocationsListUrl = SitkaRoute<FundSourceController>.BuildUrlFromExpression(c => c.Index());
        }
    }
}
