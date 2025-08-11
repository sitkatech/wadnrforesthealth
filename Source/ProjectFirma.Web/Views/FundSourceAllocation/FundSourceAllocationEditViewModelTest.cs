/*-----------------------------------------------------------------------
<copyright file="FundSourceAllocationEditViewModelTest.cs" company="Tahoe Regional Planning Agency and Environmental Science Associates">
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
using System.ComponentModel.DataAnnotations;
using LtInfo.Common;
using LtInfo.Common.Mvc;
using NUnit.Framework;
using ProjectFirma.Web.UnitTestCommon;

namespace ProjectFirma.Web.Views.FundSourceAllocation
{
    [TestFixture]
    public class FundSourceAllocationEditViewModelTest
    {
        [Test]
        public void AllViewModelFieldsAreSetFromConstructorTest()
        {
            // Arrange
            var fundSourceAllocation = TestFramework.TestFundSourceAllocation.Create();

            // Act
            var viewModel = new EditFundSourceAllocationViewModel(fundSourceAllocation);

            // Assert
            Assert.That(viewModel.FundSourceAllocationID, Is.EqualTo(fundSourceAllocation.FundSourceAllocationID));
            Assert.That(viewModel.FundSourceAllocationName, Is.EqualTo(fundSourceAllocation.FundSourceAllocationName));
            Assert.That(viewModel.OrganizationID, Is.EqualTo(fundSourceAllocation.OrganizationID));
            Assert.That(viewModel.SourceID, Is.EqualTo(fundSourceAllocation.FundSourceAllocationSourceID));
            //Assert.That(viewModel.IsActive, Is.EqualTo(fundSourceAllocation.IsActive));
        }

        [Test]
        public void UpdateModelTest()
        {
            // Arrange
            var organization = TestFramework.TestOrganization.Create();
            var fundSourceAllocation = TestFramework.TestFundSourceAllocation.Create();
            var viewModel = new EditFundSourceAllocationViewModel(fundSourceAllocation);
            viewModel.FundSourceAllocationName = TestFramework.MakeTestName(GeneralUtility.NameOf(() => viewModel.FundSourceAllocationName), Models.FundSourceAllocation.FieldLengths.FundSourceAllocationName);
            viewModel.OrganizationID = organization.OrganizationID;
            //viewModel.IsActive = true;

            // Act
            viewModel.UpdateModel(fundSourceAllocation, TestFramework.TestPerson.Create());

            // Assert
            Assert.That(fundSourceAllocation.FundSourceAllocationName, Is.EqualTo(viewModel.FundSourceAllocationName));
            Assert.That(fundSourceAllocation.OrganizationID, Is.EqualTo(viewModel.OrganizationID));
            //Assert.That(fundSourceAllocation.IsActive, Is.EqualTo(viewModel.IsActive));
        }

        [Test]
        [Ignore]
        public void CanValidateModelTest()
        {
            // Arrange
            var fundSourceAllocation = TestFramework.TestFundSourceAllocation.Create();
            var viewModel = new EditFundSourceAllocationViewModel(fundSourceAllocation);
            var nameOfFundSourceAllocationName = GeneralUtility.NameOf(() => viewModel.FundSourceAllocationName);

            ICollection<ValidationResult> validationResults;

            // Act
            DataAnnotationsValidator.TryValidate(viewModel, out validationResults);

            // Assert
            Assert.That(validationResults.Count, Is.EqualTo(1), "Expecting certain number of errors");
            TestFramework.AssertFieldRequired(validationResults, nameOfFundSourceAllocationName);

            // Act
            // Set string fields to string longer than their max lengths
            viewModel.FundSourceAllocationName = TestFramework.MakeTestNameLongerThan(nameOfFundSourceAllocationName, Models.FundSourceAllocation.FieldLengths.FundSourceAllocationName);
            DataAnnotationsValidator.TryValidate(viewModel, out validationResults);

            // Assert
            Assert.That(validationResults.Count, Is.EqualTo(1), "Expecting certain number of errors");
            TestFramework.AssertFieldStringLength(validationResults, nameOfFundSourceAllocationName, Models.FundSourceAllocation.FieldLengths.FundSourceAllocationName);

            // Act
            // Happy path
            viewModel.FundSourceAllocationName = TestFramework.MakeTestName(nameOfFundSourceAllocationName, Models.FundSourceAllocation.FieldLengths.FundSourceAllocationName);
            var isValid = DataAnnotationsValidator.TryValidate(viewModel, out validationResults);

            // Assert
            Assert.That(isValid, Is.True, "Should pass validation");
        }
    }
    
}
