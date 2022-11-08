/*-----------------------------------------------------------------------
<copyright file="EditTreatmentsForAProjectViewData.cs" company="Tahoe Regional Planning Agency and Environmental Science Associates">
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

namespace ProjectFirma.Web.Views.Treatment
{
    public class EditTreatmentsForAProjectViewData : FirmaUserControlViewData
    {
        public IEnumerable<SelectListItem> TreatmentTypeList { get; }
        public IEnumerable<SelectListItem> TreatmentAreaList { get; }

        public EditTreatmentsForAProjectViewData(IEnumerable<TreatmentType> treatmentTypesList, IEnumerable<ProjectLocation> treatmentAreas)
        {
            TreatmentTypeList = treatmentTypesList.ToSelectList(x => x.TreatmentTypeID.ToString(CultureInfo.InvariantCulture), y => y.TreatmentTypeDisplayName);
            TreatmentAreaList = treatmentAreas.ToSelectList(x => x.ProjectLocationID.ToString(CultureInfo.InvariantCulture), y => y.ProjectLocationName);
        }
    }

}
