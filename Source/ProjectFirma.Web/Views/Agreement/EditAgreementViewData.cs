/*-----------------------------------------------------------------------
<copyright file="EditProjectViewData.cs" company="Tahoe Regional Planning Agency and Sitka Technology Group">
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
using System.Globalization;
using System.Linq;
using System.Web.Mvc;
using ProjectFirma.Web.Common;
using ProjectFirma.Web.Models;
using LtInfo.Common.Mvc;

namespace ProjectFirma.Web.Views.Agreement
{
    public class EditAgreementViewData : FirmaUserControlViewData
    {
        public IEnumerable<SelectListItem> OrganizationList { get; }
        public IEnumerable<SelectListItem> AgreementTypeList { get; }
        public IEnumerable<SelectListItem> AgreementStatusList { get; }
        public IEnumerable<SelectListItem> GrantList { get; }

        public EditAgreementType EditAgreementType { get; set; }

        public int? MOUAgreementTypeID { get; set; }

        public EditAgreementViewData(EditAgreementType editAgreementType, IEnumerable<Models.Organization> organizations, IEnumerable<Models.Grant> grants, IEnumerable<Models.AgreementType> agreementTypes, IEnumerable<Models.AgreementStatus> agreementStatuses)
        {
            OrganizationList = organizations.ToSelectListWithEmptyFirstRow(x => x.OrganizationID.ToString(CultureInfo.InvariantCulture), y => y.DisplayName);
            GrantList = grants.ToSelectListWithEmptyFirstRow(x => x.GrantID.ToString(CultureInfo.InvariantCulture), y => y.GrantTitle);
            var agreementTypesAsList = agreementTypes.ToList();
            AgreementTypeList = agreementTypesAsList.ToSelectListWithEmptyFirstRow(x => x.AgreementTypeID.ToString(CultureInfo.InvariantCulture), y => y.AgreementTypeName);
            AgreementStatusList = agreementStatuses.ToSelectListWithEmptyFirstRow(x => x.AgreementStatusID.ToString(CultureInfo.InvariantCulture), y => y.AgreementStatusName);
            EditAgreementType = editAgreementType;
            MOUAgreementTypeID = agreementTypesAsList.SingleOrDefault(x => string.Equals(x.AgreementTypeAbbrev, "MOU"))
                ?.AgreementTypeID;

        }
    }
}
