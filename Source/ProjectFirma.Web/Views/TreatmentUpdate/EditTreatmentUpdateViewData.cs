/*-----------------------------------------------------------------------
<copyright file="EditTreatmentUpdateViewData.cs" company="Tahoe Regional Planning Agency and Sitka Technology Group">
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
using System.Web.Mvc;
using LtInfo.Common.Mvc;
using ProjectFirma.Web.Models;

namespace ProjectFirma.Web.Views.TreatmentUpdate
{
    public class EditTreatmentUpdateViewData : FirmaUserControlViewData
    {
        public IEnumerable<SelectListItem> TreatmentTypeList { get; }
        public IEnumerable<SelectListItem> TreatmentDetailedActivityTypeList { get; }
        public IEnumerable<SelectListItem> TreatmentAreaList { get; }
        public IEnumerable<SelectListItem> TreatmentCodeList { get; }

        public EditTreatmentUpdateViewData(IEnumerable<TreatmentType> treatmentTypesList, IEnumerable<TreatmentDetailedActivityType> treatmentDetailedActivityTypesList, IEnumerable<ProjectLocationUpdate> treatmentAreas, IEnumerable<TreatmentCode> treatmentCodesList)
        {
            TreatmentTypeList = treatmentTypesList.ToSelectList(x => x.TreatmentTypeID.ToString(CultureInfo.InvariantCulture), y => y.TreatmentTypeDisplayName);
            TreatmentDetailedActivityTypeList = treatmentDetailedActivityTypesList.ToSelectList(x => x.TreatmentDetailedActivityTypeID.ToString(CultureInfo.InvariantCulture), y => y.TreatmentDetailedActivityTypeDisplayName);
            TreatmentAreaList = treatmentAreas.ToSelectList(x => x.ProjectLocationUpdateID.ToString(CultureInfo.InvariantCulture), y => y.ProjectLocationUpdateName);
            TreatmentCodeList = treatmentCodesList.ToSelectListWithEmptyFirstRow(x => x.TreatmentCodeID.ToString(CultureInfo.InvariantCulture), y => y.TreatmentCodeDisplayName);
        }
    }

}
