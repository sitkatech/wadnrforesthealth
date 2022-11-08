/*-----------------------------------------------------------------------
<copyright file="ProposalSectionsStatus.cs" company="Tahoe Regional Planning Agency and Environmental Science Associates">
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

using System.Linq;
using ProjectFirma.Web.Controllers;
using ProjectFirma.Web.Views.ProjectPriorityLandscape;
using ProjectFirma.Web.Views.ProjectRegion;

namespace ProjectFirma.Web.Views.GisProjectBulkUpdate
{
    public class GisImportSectionStatus
    {
        public bool IsInitialUploadComplete { get; set; }
        public bool IsGeospatialValidationComplete { get; set; }
       

        public static bool AreAllSectionsValidForGisUploadAttempt(Models.GisUploadAttempt gisUploadAttempt)
        {
            //return Models.Project.GetApplicableProposalWizardSections(project, false).All(x => x.IsComplete);
            return false;
        }
       

        public GisImportSectionStatus(Models.GisUploadAttempt gisUploadAttempt)
        {
            //var basicsResults = new BasicsViewModel(project).GetValidationResults();
            //IsBasicsSectionComplete = !basicsResults.Any();

            IsInitialUploadComplete = false;
            IsGeospatialValidationComplete = false;

           
        }

        public GisImportSectionStatus()
        {
            IsInitialUploadComplete = false;
            IsGeospatialValidationComplete = false;
        }
    }
}
