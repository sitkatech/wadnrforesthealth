/*-----------------------------------------------------------------------
<copyright file="DuplicateGrantViewData.cs" company="Tahoe Regional Planning Agency and Environmental Science Associates">
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

namespace ProjectFirma.Web.Views.Grant
{
    public class DuplicateGrantViewData : FirmaUserControlViewData
    {
        public IEnumerable<SelectListItem> GrantStatuses { get; }
        public IEnumerable<SelectListItem> GrantAllocations { get; }

        public Models.FundSource FundSourceToDuplicate { get; set; }

        public DuplicateGrantViewData(IEnumerable<Models.FundSourceStatus> grantStatuses, Models.FundSource fundSourceToDuplicate, List<Models.FundSourceAllocation> grantAllocations)
        {
            GrantStatuses = grantStatuses.ToSelectListWithEmptyFirstRow(x => x.GrantStatusID.ToString(CultureInfo.InvariantCulture), y => y.GrantStatusName);
            FundSourceToDuplicate = fundSourceToDuplicate;
            GrantAllocations = grantAllocations.ToSelectList(x => x.GrantAllocationID.ToString(CultureInfo.InvariantCulture), y => y.GrantAllocationName, true);

        }
    }
}
