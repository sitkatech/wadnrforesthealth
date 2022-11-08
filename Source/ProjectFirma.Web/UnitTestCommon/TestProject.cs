/*-----------------------------------------------------------------------
<copyright file="TestProject.cs" company="Tahoe Regional Planning Agency and Environmental Science Associates">
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
using ProjectFirma.Web.Common;
using ProjectFirma.Web.Models;

namespace ProjectFirma.Web.UnitTestCommon
{
    public static partial class TestFramework
    {
        public static class TestProject
        {

            public static Project Create()
            {
                var projectType = TestProjectType.Create();
                var projectStage = ProjectStage.Planned;
                // TODO: Verify that "Approved" is the correct project state or use the correct value

                var testFocusArea = TestFocusArea.Create();
                var project = Project.CreateNewBlank(projectType, projectStage, ProjectLocationSimpleType.None, ProjectApprovalStatus.Approved);
                project.FocusArea = testFocusArea;
                return project;
            }

            public static Project Create(DatabaseEntities dbContext)
            {
                var projectType = TestProjectType.Create(dbContext);
                var projectStage = ProjectStage.Planned;
                var focusArea = TestFocusArea.Create();

                var project = new Project(projectType,
                    projectStage,
                    $"Test Project Name {Guid.NewGuid()}",
                    false,
                    ProjectLocationSimpleType.None,
                    // TODO: Verify that this is correct or use the correct value
                    ProjectApprovalStatus.Approved,
                    Project.CreateNewFhtProjectNumber()
                    );
                project.FocusArea = focusArea;
                project.ProjectDescription = MakeTestName("Test Project Description");
                dbContext.Projects.Add(project);
                return project;
            }

            public static Project Create(short projectID, string projectName)
            {
                var projectType = TestProjectType.Create();
                var projectStage = ProjectStage.Implementation;
                var focusArea = TestFocusArea.Create();

                // TODO: Verify that "Approved" is the correct project state or use the correct value
                var project = new Project(projectType, projectStage, projectName,  false, ProjectLocationSimpleType.None, ProjectApprovalStatus.Approved, Project.CreateNewFhtProjectNumber())
                {
                    ProjectID = projectID
                };
                project.FocusArea = focusArea;
                project.ProjectDescription = "Some description";
                return project;
            }

            public static Project Insert(DatabaseEntities dbContext)
            {
                var project = Create(dbContext);
                HttpRequestStorage.DatabaseEntities.SaveChanges();
                return project;
            }
        }
    }
}
