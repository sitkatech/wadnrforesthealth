/*-----------------------------------------------------------------------
<copyright file="EditProjectRegionsViewModelTest.cs" company="Tahoe Regional Planning Agency and Sitka Technology Group">
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
using System.Linq;
using NUnit.Framework;
using ProjectFirma.Web.Common;
using ProjectFirma.Web.UnitTestCommon;

namespace ProjectFirma.Web.Views.ProjectRegion
{
    [TestFixture]
    public class EditProjectRegionsViewModelTest
    {
        [Test]
        public void AllViewModelFieldsAreSetFromConstructorTest()
        {
            //// Arrange
            //var region1 = TestFramework.TestRegion.Create();
            //var region2 = TestFramework.TestRegion.Create();
            //var region3 = TestFramework.TestRegion.Create();
            //var region4 = TestFramework.TestRegion.Create();

            //var project = TestFramework.TestProject.Create();
            //TestFramework.TestProjectRegion.Create(project, region1);
            //TestFramework.TestProjectRegion.Create(project, region2);
            //TestFramework.TestProjectRegion.Create(project, region3);
            //TestFramework.TestProjectRegion.Create(project, region4);

            //var allRegions = new List<Models.Region> { region1, region2, region3, region4 };

            //// Act
            //var viewModel = new EditProjectRegionsViewModel(project.ProjectRegions.Select(x => x.RegionID).ToList());

            //// Assert
            //Assert.That(viewModel.RegionIDs, Is.EquivalentTo(allRegions.Select(x => x.RegionID)));
        }

        [Test]
        public void UpdateModelTest()
        {
            //// Arrange
            //var region1 = TestFramework.TestRegion.Create();
            //var region2 = TestFramework.TestRegion.Create();
            //var region3 = TestFramework.TestRegion.Create();
            //var region4 = TestFramework.TestRegion.Create();

            //var project = TestFramework.TestProject.Create();
            //var projectRegion1 = TestFramework.TestProjectRegion.Create(project, region1);
            //var projectRegion2 = TestFramework.TestProjectRegion.Create(project, region2);

            //Assert.That(project.ProjectRegions.Select(x => x.RegionID), Is.EquivalentTo(new List<int> { projectRegion1.RegionID, projectRegion2.RegionID }));

            //var regionsSelected = new List<Models.Region> { region1, region3, region4 };

            //var viewModel = new EditProjectRegionsViewModel{ RegionIDs = regionsSelected.Select(x => x.RegionID).ToList() };

            //// Act
            //var currentProjectRegions = project.ProjectRegions.ToList();
            //viewModel.UpdateModel(project, currentProjectRegions, HttpRequestStorage.DatabaseEntities.ProjectRegions.Local);

            //// Assert
            //Assert.That(currentProjectRegions.Select(x => x.RegionID), Is.EquivalentTo(regionsSelected.Select(x => x.RegionID)));
        }
    }
}
