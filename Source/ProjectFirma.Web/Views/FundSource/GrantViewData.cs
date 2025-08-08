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

namespace ProjectFirma.Web.Views.FundSource
{
    public abstract class FundSourceViewData : FirmaViewData
    {
        public Models.FundSource FundSource { get; }
        public string EditFundSourceUrl { get; set; }
        public bool UserHasEditFundSourcePermissions { get; set; }

        public string BackToFundSourcesText { get; set; }

        public string FundSourcesListUrl { get; set; }    

        protected FundSourceViewData(Person currentPerson, Models.FundSource fundSource) : base(currentPerson, null)
        {
            FundSource = fundSource;
            HtmlPageTitle = fundSource.FundSourceTitle;
            EntityName = $"{Models.FieldDefinition.FundSource.GetFieldDefinitionLabel()}";
            EditFundSourceUrl = fundSource.GetEditUrl();
            UserHasEditFundSourcePermissions = new FundSourceEditAsAdminFeature().HasPermissionByPerson(currentPerson);
            BackToFundSourcesText = $"Back to all {Models.FieldDefinition.FundSource.GetFieldDefinitionLabelPluralized()}";
            FundSourcesListUrl = SitkaRoute<FundSourceController>.BuildUrlFromExpression(c => c.Index());
        }
    }
}
