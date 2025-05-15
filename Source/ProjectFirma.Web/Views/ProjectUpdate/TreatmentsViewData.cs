/*-----------------------------------------------------------------------
<copyright file="TreatmentsViewData.cs" company="Tahoe Regional Planning Agency and Environmental Science Associates">
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

using ProjectFirma.Web.Models;
using System.Collections.Generic;
using ProjectFirma.Web.Controllers;
using ProjectFirma.Web.Common;

namespace ProjectFirma.Web.Views.ProjectUpdate
{
    public class TreatmentsViewData : ProjectUpdateViewData
    {
        public TreatmentUpdateGridSpec TreatmentUpdateGridSpec { get; }
        public string TreatmentUpdateGrid { get; }
        public string TreatmentUpdateGridDataUrl { get; }
        public readonly string RefreshUrl;
        public readonly string ContinueUrl;

        public TreatmentsViewData(Person currentPerson,
            ProjectUpdateBatch projectUpdateBatch,
            TreatmentUpdateGridSpec treatmentUpdateGridSpec,
            string treatmentUpdateGridDataUrl,
            UpdateStatus updateStatus) : base(currentPerson, projectUpdateBatch, updateStatus, new List<string>(), ProjectUpdateSection.Treatments.ProjectUpdateSectionDisplayName)
        {
            TreatmentUpdateGridSpec = treatmentUpdateGridSpec;
            TreatmentUpdateGrid = "treatmentUpdateGridUpdateWorkflow";
            TreatmentUpdateGridDataUrl = treatmentUpdateGridDataUrl;
            RefreshUrl = SitkaRoute<ProjectUpdateController>.BuildUrlFromExpression(x => x.RefreshTreatments(projectUpdateBatch.Project.ProjectID));
            ContinueUrl = SitkaRoute<ProjectUpdateController>.BuildUrlFromExpression(x => x.Photos(projectUpdateBatch.Project));
        }
    }
}
