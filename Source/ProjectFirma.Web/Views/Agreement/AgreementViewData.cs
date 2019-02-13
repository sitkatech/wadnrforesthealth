/*-----------------------------------------------------------------------
<copyright file="GrantViewData.cs" company="Tahoe Regional Planning Agency and Sitka Technology Group">
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

using ProjectFirma.Web.Common;
using ProjectFirma.Web.Controllers;
using ProjectFirma.Web.Models;
using ProjectFirma.Web.Security;

namespace ProjectFirma.Web.Views.Agreement
{
    public abstract class AgreementViewData : FirmaViewData
    {
        public Models.Agreement Agreement { get; }
        public string EditAgreementUrl { get; set; }

        public string BackToAgreementsText { get; set; }

        public string AgreementsListUrl { get; set; }

        protected AgreementViewData(Person currentPerson, Models.Agreement agreement) : base(currentPerson, null)
        {
            Agreement = agreement;
            HtmlPageTitle = agreement.AgreementTitle;
            EntityName = $"{Models.FieldDefinition.Agreement.GetFieldDefinitionLabel()}";
            EditAgreementUrl = agreement.GetEditUrl();
            BackToAgreementsText = "Back to all Agreements";
            AgreementsListUrl = SitkaRoute<AgreementController>.BuildUrlFromExpression(c => c.Index());
        }
    }
}
