﻿/*-----------------------------------------------------------------------
<copyright file="TestProjectType.cs" company="Tahoe Regional Planning Agency and Environmental Science Associates">
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

namespace ProjectFirma.Web.UnitTestCommon
{
    public static partial class TestFramework
    {
        public static class TestProjectType
        {
            public static ProjectType Create()
            {
                var taxonomyBranch = TestTaxonomyBranch.Create();
                var projectType = ProjectType.CreateNewBlank(taxonomyBranch);
                return projectType;
            }

            /// <summary>
            /// Create new ProjectType and attach it to the given context
            /// </summary>
            public static ProjectType Create(DatabaseEntities dbContext)
            {
                var taxonomyBranch = TestTaxonomyBranch.Create(dbContext);
                string projectTypeTestName = MakeTestName("Test Taxonomy Tier One", ProjectType.FieldLengths.ProjectTypeName);
                var projectType = new ProjectType(taxonomyBranch, projectTypeTestName, false);
                var newProjectType = projectType;
                dbContext.ProjectTypes.Add(newProjectType);
                return newProjectType;
            }
        }

        public static class TestFocusArea
        {
            public static FocusArea Create()
            {
                //var testRegion = Region.CreateNewBlank();
                var testRegion = TestRegion.Create();

                var focusArea = FocusArea.CreateNewBlank(FocusAreaStatus.InProgress, testRegion);
                focusArea.FocusAreaName = MakeTestName("Test Focus Area");
                focusArea.DNRUplandRegion.DNRUplandRegionName = MakeTestName("Test Region Name", 100);
                return focusArea;
            }
        }

        public static class TestRegion
        {
            public static DNRUplandRegion Create()
            {
                var testRegion = DNRUplandRegion.CreateNewBlank();
                testRegion.DNRUplandRegionName = MakeTestName("Test Region", 100);
                return testRegion;
            }
        }

    }
}
