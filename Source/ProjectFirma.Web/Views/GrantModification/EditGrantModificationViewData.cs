/*-----------------------------------------------------------------------
<copyright file="EditGrantModificationViewData.cs" company="Tahoe Regional Planning Agency and Sitka Technology Group">
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

using LtInfo.Common.Mvc;
using ProjectFirma.Web.Models;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web.Mvc;

namespace ProjectFirma.Web.Views.GrantModification
{
    public class EditGrantModificationViewData : FirmaUserControlViewData
    {
        public EditGrantModificationType EditGrantModificationType { get; set; }
        public IEnumerable<SelectListItem> GrantModificationStatuses { get; }
        public IEnumerable<SelectListItem> AllGrantModificationPurposes { get; }

        public EditGrantModificationViewData(IEnumerable<Models.GrantModificationStatus> grantModificationStatuses, IEnumerable<GrantModificationPurpose> grantModificationPurposes, EditGrantModificationType editGrantModificationType)
        {
            GrantModificationStatuses = grantModificationStatuses.ToSelectListWithEmptyFirstRow(k => k.GrantModificationStatusID.ToString(CultureInfo.InvariantCulture), v => v.GrantModificationStatusName);
            AllGrantModificationPurposes = grantModificationPurposes.ToSelectList(k => k.GrantModificationPurposeID.ToString(CultureInfo.InvariantCulture), v => v.GrantModificationPurposeName).OrderBy(x => x.Text);
            EditGrantModificationType = editGrantModificationType;
        }
    }
}
