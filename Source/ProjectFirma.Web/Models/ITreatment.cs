/*-----------------------------------------------------------------------
<copyright file="ITreatment.cs" company="Tahoe Regional Planning Agency and Environmental Science Associates">
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

namespace ProjectFirma.Web.Models
{
    public interface ITreatment
    {
        DateTime? TreatmentStartDate { get; set; }
        DateTime? TreatmentEndDate { get; set; }
        decimal TreatmentFootprintAcres { get; set; }
        string TreatmentNotes { get; set; }
        int TreatmentTypeID { get; set; }
        decimal? TreatmentTreatedAcres { get; set; }
        string TreatmentTypeImportedText { get; set; }
        int? CreateGisUploadAttemptID { get; set; }
        int? UpdateGisUploadAttemptID { get; set; }
        int TreatmentDetailedActivityTypeID { get; set; }
        string TreatmentDetailedActivityTypeImportedText { get; set; }
        int? ProgramID { get; set; }
        bool? ImportedFromGis { get; set; }
    }
}
