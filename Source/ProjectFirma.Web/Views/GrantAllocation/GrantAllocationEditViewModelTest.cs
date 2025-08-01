/*-----------------------------------------------------------------------
<copyright file="EditViewModelTest.cs" company="Tahoe Regional Planning Agency and Environmental Science Associates">
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

namespace ProjectFirma.Web.Views.GrantAllocation
{
    [TestFixture]
    public class GrantAllocationEditViewModelTest
    {
        [Test]
        public void AllViewModelFieldsAreSetFromConstructorTest()
        {
            // Arrange
            var grantAllocation = TestFramework.TestFundSourceAllocation.Create();

            // Act
            var viewModel = new EditGrantAllocationViewModel(grantAllocation);

            // Assert
            Assert.That(viewModel.GrantAllocationID, Is.EqualTo(grantAllocation.GrantAllocationID));
            Assert.That(viewModel.GrantAllocationName, Is.EqualTo(grantAllocation.GrantAllocationName));
            Assert.That(viewModel.OrganizationID, Is.EqualTo(grantAllocation.OrganizationID));
            Assert.That(viewModel.SourceID, Is.EqualTo(grantAllocation.GrantAllocationSourceID));
            //Assert.That(viewModel.IsActive, Is.EqualTo(grantAllocation.IsActive));
        }

        [Test]
        public void UpdateModelTest()
        {
            // Arrange
            var organization = TestFramework.TestOrganization.Create();
            var grantAllocation = TestFramework.TestFundSourceAllocation.Create();
            var viewModel = new EditGrantAllocationViewModel(grantAllocation);
            viewModel.GrantAllocationName = TestFramework.MakeTestName(GeneralUtility.NameOf(() => viewModel.GrantAllocationName), Models.FundSourceAllocation.FieldLengths.GrantAllocationName);
            viewModel.OrganizationID = organization.OrganizationID;
            //viewModel.IsActive = true;

            // Act
            viewModel.UpdateModel(grantAllocation, TestFramework.TestPerson.Create());

            // Assert
            Assert.That(grantAllocation.GrantAllocationName, Is.EqualTo(viewModel.GrantAllocationName));
            Assert.That(grantAllocation.OrganizationID, Is.EqualTo(viewModel.OrganizationID));
            //Assert.That(grantAllocation.IsActive, Is.EqualTo(viewModel.IsActive));
        }

        [Test]
        [Ignore]
        public void CanValidateModelTest()
        {
            // Arrange
            var grantAllocation = TestFramework.TestFundSourceAllocation.Create();
            var viewModel = new EditGrantAllocationViewModel(grantAllocation);
            var nameOfGrantAllocationName = GeneralUtility.NameOf(() => viewModel.GrantAllocationName);

            ICollection<ValidationResult> validationResults;

            // Act
            DataAnnotationsValidator.TryValidate(viewModel, out validationResults);

            // Assert
            Assert.That(validationResults.Count, Is.EqualTo(1), "Expecting certain number of errors");
            TestFramework.AssertFieldRequired(validationResults, nameOfGrantAllocationName);

            // Act
            // Set string fields to string longer than their max lengths
            viewModel.GrantAllocationName = TestFramework.MakeTestNameLongerThan(nameOfGrantAllocationName, Models.FundSourceAllocation.FieldLengths.GrantAllocationName);
            DataAnnotationsValidator.TryValidate(viewModel, out validationResults);

            // Assert
            Assert.That(validationResults.Count, Is.EqualTo(1), "Expecting certain number of errors");
            TestFramework.AssertFieldStringLength(validationResults, nameOfGrantAllocationName, Models.FundSourceAllocation.FieldLengths.GrantAllocationName);

            // Act
            // Happy path
            viewModel.GrantAllocationName = TestFramework.MakeTestName(nameOfGrantAllocationName, Models.FundSourceAllocation.FieldLengths.GrantAllocationName);
            var isValid = DataAnnotationsValidator.TryValidate(viewModel, out validationResults);

            // Assert
            Assert.That(isValid, Is.True, "Should pass validation");
        }
    }
    
}
