/*-----------------------------------------------------------------------
<copyright file="DuplicateGrantModificationViewData.cs" company="Tahoe Regional Planning Agency and Environmental Science Associates">
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
using System.Web.Mvc;
using LtInfo.Common.Mvc;
using ProjectFirma.Web.Models;

namespace ProjectFirma.Web.Views.GrantModification
{
    public class DuplicateGrantModificationViewData : FirmaUserControlViewData
    {
        public IEnumerable<SelectListItem> GrantModificationStatuses { get; }
        public IEnumerable<SelectListItem> GrantAllocations { get; }
        public IEnumerable<SelectListItem> GrantModificationPurposes { get; }

        public Models.GrantModification GrantModificationToDuplicate { get; set; }

        public DuplicateGrantModificationViewData(IEnumerable<Models.GrantModificationStatus> grantModificationStatuses, List<GrantModificationPurpose> grantModificationPurposes, Models.GrantModification grantModificationToDuplicate, List<Models.GrantAllocation> grantAllocations)
        {
            GrantModificationStatuses = grantModificationStatuses.ToSelectListWithEmptyFirstRow(x => x.GrantModificationStatusID.ToString(CultureInfo.InvariantCulture), y => y.GrantModificationStatusName);
            GrantModificationPurposes = grantModificationPurposes.ToSelectList(x => x.GrantModificationPurposeID.ToString(CultureInfo.InvariantCulture), y => y.GrantModificationPurposeName);
            GrantModificationToDuplicate = grantModificationToDuplicate;
            GrantAllocations = grantAllocations.ToSelectList(x => x.GrantAllocationID.ToString(CultureInfo.InvariantCulture), y => y.GrantAllocationName, true);

        }
    }
}
