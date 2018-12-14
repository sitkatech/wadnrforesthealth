/*-----------------------------------------------------------------------
<copyright file="CostTest.cs" company="Tahoe Regional Planning Agency and Sitka Technology Group">
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
using ProjectFirma.Web.Common;
using ProjectFirma.Web.UnitTestCommon;
using NUnit.Framework;

namespace ProjectFirma.Web.Models
{
    [TestFixture]
    public class CostTest : FirmaTestWithContext
    {
        [Test]
        public void TestFutureValueFunction()
        {
            var pv = 1000m;
            var i = 0.04m;
            var t = 10;

            var fv = pv*(1 + i).Pow(t);

            fv.AssertThatIsWithinOnePennyOf(1480.24m, "Future value calculation implemented incorrectly.");
        }

        [Test]
        public void TestCostInYearOfExpenditureFunction()
        {
            var currentYear = 2013;
            var expenditureYear = 2020;

            var currentYearCost = 2000000m;

            var inflationRate = 0.02m;

            var expectedExpenditureYearCostCalculated = FirmaMathUtilities.FutureValueOfPresentSum(currentYearCost, inflationRate, currentYear, expenditureYear);

            var expectedExpenditureYearCostExpected = 2297371.34m;

            expectedExpenditureYearCostCalculated.AssertThatIsWithinOnePennyOf(expectedExpenditureYearCostExpected, "Future value calculation does not match expected value.");
        }

        [Test]
        public void TestProjectExpectedValue()
        {
            var project = TestFramework.TestProject.Create();
            project.EstimatedTotalCost = 2000000m;
            project.ApprovalStartDate = new DateTime(2013,1,1);
            project.CompletionDate = new DateTime(2020,1,1);

            var inflationRate = 0.02m;
            var expectedExpenditureYearCostCalculated = CostParameterSet.CalculateCapitalCostInYearOfExpenditureImpl(project, inflationRate, 2016) ?? 0;

            var expectedExpenditureYearCostFromExpected = 2164864.32m;
            expectedExpenditureYearCostCalculated.AssertThatIsWithinOnePennyOf(expectedExpenditureYearCostFromExpected, "Project Cost in Year of Expenditure calculated incorrectly.");
        }

        [Test]
        public void TestCostInYearOfExpenditureHandlesNull()
        {
            var project = TestFramework.TestProject.Create();

            project.EstimatedTotalCost = 2000000m;
            project.ApprovalStartDate = null;
            project.CompletionDate = null;

            var inflationRate = 0.02m;

            Assert.That(CostParameterSet.CalculateCapitalCostInYearOfExpenditureImpl(project, inflationRate, 2016), Is.Null, "Incorrect implementation of nullable type.");

            project.EstimatedTotalCost = null;
            project.ApprovalStartDate = new DateTime(2013, 1, 1);
            project.CompletionDate = new DateTime(2020, 1, 1);

            Assert.That(CostParameterSet.CalculateCapitalCostInYearOfExpenditureImpl(project, inflationRate, 2016), Is.Null, "Incorrect implementation of nullable type.");
        }
    }
}
