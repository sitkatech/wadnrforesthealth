﻿/*-----------------------------------------------------------------------
<copyright file="ManageFindYourForesterViewData.cs" company="Tahoe Regional Planning Agency and Environmental Science Associates">
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

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web.Mvc;
using LtInfo.Common.Mvc;
using ProjectFirma.Web.Common;
using ProjectFirma.Web.Controllers;
using ProjectFirma.Web.Models;

namespace ProjectFirma.Web.Views.FindYourForester
{
    public class BulkAssignForestersViewData
    {
        public readonly List<ForesterWorkUnit> ForesterWorkUnitIDList;
        public readonly IEnumerable<SelectListItem> AllPossibleForesters;

        public BulkAssignForestersViewData(Person currentPerson, List<Person> allPossibleForesters, List<ForesterWorkUnit> foresterWorkUnitIDList)
        {
            ForesterWorkUnitIDList = foresterWorkUnitIDList;
            AllPossibleForesters = allPossibleForesters.ToSelectListWithEmptyFirstRowCustom(x => x.PersonID.ToString(CultureInfo.InvariantCulture), y => y.FullNameFirstLastAndOrgShortName, "Unassigned");
        }

    }
}
