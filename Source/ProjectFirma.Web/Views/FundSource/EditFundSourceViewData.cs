/*-----------------------------------------------------------------------
<copyright file="EditFundSourceViewData.cs" company="Tahoe Regional Planning Agency and Environmental Science Associates">
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
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web.Mvc;
using ProjectFirma.Web.Common;
using ProjectFirma.Web.Models;
using LtInfo.Common.Mvc;

namespace ProjectFirma.Web.Views.FundSource
{
    public class EditFundSourceViewData : FirmaUserControlViewData
    {
        public IEnumerable<SelectListItem> Organizations { get; }
        public IEnumerable<SelectListItem> FundSourceTypes { get; }
        public IEnumerable<SelectListItem> FundSourceStatuses { get; }

        public EditFundSourceType EditFundSourceType { get; set; }

        public EditFundSourceViewData(EditFundSourceType editFundSourceType, IEnumerable<Models.Organization> organizations, IEnumerable<Models.FundSourceStatus> fundSourceStatuses, IEnumerable<Models.FundSourceType> fundSourceTypes)
        {
            Organizations = organizations.ToSelectListWithEmptyFirstRow(x => x.OrganizationID.ToString(CultureInfo.InvariantCulture), y => y.DisplayName);
            FundSourceStatuses = fundSourceStatuses.ToSelectListWithEmptyFirstRow(x => x.FundSourceStatusID.ToString(CultureInfo.InvariantCulture), y => y.FundSourceStatusName);
            FundSourceTypes = fundSourceTypes.ToSelectListWithEmptyFirstRow(x => x.FundSourceTypeID.ToString(CultureInfo.InvariantCulture), y => y.FundSourceTypeName);
            EditFundSourceType = editFundSourceType;
        }
    }
}
