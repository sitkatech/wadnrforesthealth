/*-----------------------------------------------------------------------
<copyright file="EditViewModelTest.cs" company="Tahoe Regional Planning Agency and Sitka Technology Group">
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
using System.ComponentModel.DataAnnotations;
using System.Web;
using ProjectFirma.Web.UnitTestCommon;
using LtInfo.Common;
using LtInfo.Common.Mvc;
using NUnit.Framework;

namespace ProjectFirma.Web.Views.ProjectType
{
    [TestFixture]
    public class EditViewModelTest
    {
        [Test]
        public void AllViewModelFieldsAreSetFromConstructorTest()
        {
            // Arrange
            var projectType = TestFramework.TestProjectType.Create();

            // Act
            var viewModel = new EditViewModel(projectType);

            // Assert
            Assert.That(viewModel.ProjectTypeID, Is.EqualTo(projectType.ProjectTypeID));
            Assert.That(viewModel.ProjectTypeName, Is.EqualTo(projectType.ProjectTypeName));
        }

        [Test]
        public void UpdateModelTest()
        {
            // Arrange
            var projectType = TestFramework.TestProjectType.Create();
            var viewModel = new EditViewModel(projectType);
            viewModel.ProjectTypeName = TestFramework.MakeTestName(GeneralUtility.NameOf(() => viewModel.ProjectTypeName), Models.ProjectType.FieldLengths.ProjectTypeName);

            // Act
            viewModel.UpdateModel(projectType, TestFramework.TestPerson.Create());

            // Assert
            Assert.That(projectType.ProjectTypeName, Is.EqualTo(viewModel.ProjectTypeName));
        }

        [Test]
        public void CanValidateModelTest()
        {
            // Arrange
            var projectType = TestFramework.TestProjectType.Create();
            var viewModel = new EditViewModel(projectType);
            var nameOfProjectTypeName = GeneralUtility.NameOf(() => viewModel.ProjectTypeName);

            ICollection<ValidationResult> validationResults;

            // Act
            DataAnnotationsValidator.TryValidate(viewModel, out validationResults);

            // Assert
            Assert.That(validationResults.Count, Is.EqualTo(2), "Expecting certain number of errors");
            TestFramework.AssertFieldRequired(validationResults, nameOfProjectTypeName);

            // Act
            // Set string fields to string longer than their max lengths
            viewModel.ProjectTypeName = TestFramework.MakeTestNameLongerThan(nameOfProjectTypeName, Models.ProjectType.FieldLengths.ProjectTypeName);
            viewModel.ProjectTypeDescription = new HtmlString("Test Description");
            DataAnnotationsValidator.TryValidate(viewModel, out validationResults);

            // Assert
            Assert.That(validationResults.Count, Is.EqualTo(1), "Expecting certain number of errors");
            TestFramework.AssertFieldStringLength(validationResults, nameOfProjectTypeName, Models.ProjectType.FieldLengths.ProjectTypeName);

            // Act
            // Happy path
            viewModel.ProjectTypeName = TestFramework.MakeTestName(nameOfProjectTypeName, Models.ProjectType.FieldLengths.ProjectTypeName);
            var isValid = DataAnnotationsValidator.TryValidate(viewModel, out validationResults);

            // Assert
            Assert.That(isValid, Is.True, "Should pass validation");
        }
    }
}
