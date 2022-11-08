/*-----------------------------------------------------------------------
<copyright file="TestClassification.cs" company="Tahoe Regional Planning Agency and Environmental Science Associates">
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
        public static class TestClassification
        {
            public static Classification Create()
            {
                var classification = Classification.CreateNewBlank(ClassificationSystem.CreateNewBlank());
                classification.ClassificationDescription = MakeTestName("New ClassificationDescription");
                classification.ThemeColor = "blue";
                classification.DisplayName = MakeTestName("Test Classification Display Name", 50);
                classification.ClassificationSystem.ClassificationSystemName = MakeTestName("Fake Test Name for ClassificationSystem object");
                return classification;
            }

            public static Classification Create(DatabaseEntities dbContext)
            {
                var classification = Create();
                dbContext.Classifications.Add(classification);
                return classification;
            }
        }
    }
}
