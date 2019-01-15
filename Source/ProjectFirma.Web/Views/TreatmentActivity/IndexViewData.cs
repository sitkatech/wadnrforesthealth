/*-----------------------------------------------------------------------
<copyright file="IndexViewData.cs" company="Tahoe Regional Planning Agency and Sitka Technology Group">
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

namespace ProjectFirma.Web.Views.TreatmentActivity
{
    public class IndexViewData : FirmaViewData
    {
        public readonly TreatmentActivityIndexGridSpec GridSpec;
        public readonly string GridName;
        public readonly string GridDataUrl;

        public IndexViewData(Person currentPerson) : base(currentPerson)
        {
            PageTitle = "Treatment Activities";
            GridSpec = new TreatmentActivityIndexGridSpec(currentPerson) {ObjectNameSingular = "Treatment Activity", ObjectNamePlural = "Treatment Activities", SaveFiltersInCookie = true};

            GridName = "treatmentActivtiesGrid";
            GridDataUrl = SitkaRoute<TreatmentActivityController>.BuildUrlFromExpression(tac => tac.IndexGridJsonData());
        }
    }
}
