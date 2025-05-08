﻿/*-----------------------------------------------------------------------
<copyright file="ProjectExemptReportingYearUpdate.cs" company="Tahoe Regional Planning Agency and Environmental Science Associates">
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
using System.Linq;
using LtInfo.Common;
using ProjectFirma.Web.Common;

namespace ProjectFirma.Web.Models
{
    public partial class ProjectExemptReportingYearUpdate
    {

        public static void CreateExpendituresExemptReportingYearsFromProject(ProjectUpdateBatch projectUpdateBatch)
        {
            var project = projectUpdateBatch.Project;
            foreach (var projectExemptReportingYearUpdate in project.GetExpendituresExemptReportingYears()
                .Select(projectExemptReportingYear => new ProjectExemptReportingYearUpdate(projectUpdateBatch,
                    projectExemptReportingYear.CalendarYear, projectExemptReportingYear.ProjectExemptReportingType))
                .ToList())
            {
                projectUpdateBatch.ProjectExemptReportingYearUpdates.Add(projectExemptReportingYearUpdate);
            }
        }

        public static void CommitChangesToProject(ProjectUpdateBatch projectUpdateBatch, IList<ProjectExemptReportingYear> projectExemptReportingYears)
        {
            var project = projectUpdateBatch.Project;
            var projectExemptReportingYearsFromProjectUpdate =
                projectUpdateBatch.ProjectExemptReportingYearUpdates.Select(x => new ProjectExemptReportingYear(project.ProjectID, x.CalendarYear, x.ProjectExemptReportingTypeID)).ToList();
            project.ProjectExemptReportingYears.Merge(projectExemptReportingYearsFromProjectUpdate,
                projectExemptReportingYears,
                (x, y) => x.ProjectID == y.ProjectID && x.CalendarYear == y.CalendarYear && x.ProjectExemptReportingTypeID == y.ProjectExemptReportingTypeID);
        }

        public string GetCalendarYear()
        {
            return MultiTenantHelpers.FormatReportingYear(CalendarYear);
        }

    }
}
