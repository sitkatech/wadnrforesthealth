/*-----------------------------------------------------------------------
<copyright file="ProjectSimple.cs" company="Tahoe Regional Planning Agency and Environmental Science Associates">
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
using ProjectFirma.Web.Views.Reports;

namespace ProjectFirma.Web.Models
{
    public class ProjectSimple
    {
        public int ProjectID { get; set; }
        public string DisplayName { get; set; }
        public bool IsLoa { get; set; }

        public ProjectSimple(Project project)
        {
            ProjectID = project.ProjectID;
            DisplayName = project.DisplayName;
            IsLoa = project.ProjectPrograms.Any(x=>x.ProgramID == ProjectController.LoaProgramID);
        }

        public ProjectSimple(int projectID, string displayName, bool isLoa)
        {
            ProjectID = projectID;
            DisplayName = displayName;
            IsLoa = isLoa;
        }
    }

    public class ProjectSimpleForGisLogging
    {
        public string ProjectGisIdentifier { get; set; }
        public string ProjectName { get; set; }

        public ProjectSimpleForGisLogging(Project project)
        {
            ProjectGisIdentifier = project.ProjectGisIdentifier;
            ProjectName = project.ProjectName;
        }

        public ProjectSimpleForGisLogging(string projectGisIdentifier, string projectName)
        {
            ProjectGisIdentifier = projectGisIdentifier;
            ProjectName = projectName;
        }
    }

}
