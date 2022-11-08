/*-----------------------------------------------------------------------
<copyright file="ProjectLocationSummaryViewData.cs" company="Tahoe Regional Planning Agency and Environmental Science Associates">
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
using ProjectFirma.Web.Models;
using ProjectFirma.Web.Views.Map;

namespace ProjectFirma.Web.Views.Shared.ProjectLocationControls
{
    public class ProjectLocationSummaryViewData : FirmaUserControlViewData
    {
        public readonly string ProjectLocationNotes;
        public readonly ProjectLocationSummaryMapInitJson ProjectLocationSummaryMapInitJson;
        public readonly List<Models.PriorityLandscape> PriorityLandscapes;
        public readonly List<Models.DNRUplandRegion> DNRUplandRegions;
        public readonly List<Models.County> Counties;
        public readonly string NoRegionsExplanation;
        public readonly string NoCountiesExplanation;
        public readonly bool HasLocationNotes;
        public readonly bool HasLocationInformation;


        public ProjectLocationSummaryViewData(IProject project,
            ProjectLocationSummaryMapInitJson projectLocationSummaryMapInitJson,
            List<Models.PriorityLandscape> priorityLandscapes, List<Models.DNRUplandRegion> regions,  string noRegionsExplanation,
            string noPriorityLandscapesExplanation, List<Models.County> counties, string noCountiesExplanation)
        {
            ProjectLocationNotes = project.ProjectLocationNotes;
            ProjectLocationSummaryMapInitJson = projectLocationSummaryMapInitJson;
            PriorityLandscapes = priorityLandscapes;
            DNRUplandRegions = regions;
            NoRegionsExplanation = noRegionsExplanation;
            HasLocationNotes = !string.IsNullOrWhiteSpace(project.ProjectLocationNotes);
            HasLocationInformation = project.ProjectLocationSimpleType != ProjectLocationSimpleType.None;
            NoPriorityLandscapesExplanation = noPriorityLandscapesExplanation;
            Counties = counties;
            NoCountiesExplanation = noCountiesExplanation;
        }

        public string NoPriorityLandscapesExplanation { get; }
    }
}
