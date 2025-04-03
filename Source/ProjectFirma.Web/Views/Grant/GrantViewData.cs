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

namespace ProjectFirma.Web.Views.Grant
{
    public abstract class GrantViewData : FirmaViewData
    {
        public Models.Grant Grant { get; }
        public string EditGrantUrl { get; set; }
        public bool UserHasEditGrantPermissions { get; set; }

        public string BackToGrantsText { get; set; }

        public string GrantsListUrl { get; set; }    

        protected GrantViewData(Person currentPerson, Models.Grant grant) : base(currentPerson, null)
        {
            Grant = grant;
            HtmlPageTitle = grant.GrantTitle;
            EntityName = $"{Models.FieldDefinition.Grant.GetFieldDefinitionLabel()}";
            EditGrantUrl = grant.GetEditUrl();
            UserHasEditGrantPermissions = new GrantEditAsAdminFeature().HasPermissionByPerson(currentPerson);
            BackToGrantsText = $"Back to all {Models.FieldDefinition.Grant.GetFieldDefinitionLabelPluralized()}";
            GrantsListUrl = SitkaRoute<GrantController>.BuildUrlFromExpression(c => c.Index());
        }
    }
}
