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
using ProjectFirma.Web.Views.Project;
using ProjectFirma.Web.Views.Shared.ProjectPerson;

namespace ProjectFirma.Web.Views.ProjectCreate
{
    public class TreatmentsViewData : ProjectCreateViewData
    {
        public TreatmentGridSpec TreatmentGridSpec { get; }
        public string TreatmentGrid { get; }
        public string TreatmentGridDataUrl { get; }

        public TreatmentsViewData(Person currentPerson,
            Models.Project project,
            ProposalSectionsStatus proposalSectionsStatus, TreatmentGridSpec treatmentGridSpec, string treatmentGridDataUrl ) : base(currentPerson, project, ProjectCreateSection.Treatments.ProjectCreateSectionDisplayName, proposalSectionsStatus)
        {
            TreatmentGridSpec = treatmentGridSpec;
            TreatmentGrid = "treatmentGridCreateWorkflow";
            TreatmentGridDataUrl = treatmentGridDataUrl;
        }
    }
}
