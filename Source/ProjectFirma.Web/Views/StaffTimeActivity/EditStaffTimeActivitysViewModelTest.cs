/*-----------------------------------------------------------------------
<copyright file="EditStaffTimeActivitysViewModelTest.cs" company="Tahoe Regional Planning Agency and Sitka Technology Group">
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

using System;
using System.Collections.Generic;
using System.Linq;
using ProjectFirma.Web.UnitTestCommon;
using NUnit.Framework;
using ProjectFirma.Web.Models;

namespace ProjectFirma.Web.Views.StaffTimeActivity
{
    [TestFixture]
    public class EditStaffTimeActivitysViewModelTest
    {
        [Test]
        public void AllViewModelFieldsAreSetFromConstructorTest()
        {
            // Arrange
            var fundingSource1 = TestFramework.TestFundingSource.Create();
            var fundingSource2 = TestFramework.TestFundingSource.Create();
            var fundingSource3 = TestFramework.TestFundingSource.Create();
            var fundingSource4 = TestFramework.TestFundingSource.Create();

            var project = TestFramework.TestProject.Create();
            TestFramework.TestStaffTimeActivity.Create(project, fundingSource1, 1, 2, 3, DateTime.Now);
            TestFramework.TestStaffTimeActivity.Create(project, fundingSource2, 1, 2, 3, DateTime.Now);
            TestFramework.TestStaffTimeActivity.Create(project, fundingSource3, 1, 2, 3, DateTime.Now);
            TestFramework.TestStaffTimeActivity.Create(project, fundingSource4, 1, 2, 3, DateTime.Now);

            var allFundingSources = new List<Models.FundingSource> {fundingSource1, fundingSource2, fundingSource3, fundingSource4};

            // Act
            var staffTimeActivities = project.StaffTimeActivities.ToList();
            var viewModel = new EditStaffTimeActivitiesViewModel(project, staffTimeActivities.Select(x=>new StaffTimeActivitySimple(x)).ToList());

            // Assert
            Assert.That(viewModel.StaffTimeActivities.Select(x => x.FundingSourceID), Is.EquivalentTo(allFundingSources.Select(x => x.FundingSourceID)));
        }
    }
}
