/*-----------------------------------------------------------------------
<copyright file="ProjectCalendarYearExpenditureTest.cs" company="Tahoe Regional Planning Agency and Sitka Technology Group">
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

using System.Linq;
using NUnit.Framework;
using ProjectFirma.Web.Common;

namespace ProjectFirma.Web.Views.ProgramIndex
{
    [TestFixture]
    public class BienniumTest
    {
        [Test]
        public void EnsureBienniumInDatabaseIsLikelyCorrectTest()
        {
            var currentBiennium = HttpRequestStorage.DatabaseEntities.CurrentBiennia.FirstOrDefault();
            Assert.That(currentBiennium != null, "There should be an entry in the CurrentBiennium table, corresponding to the current fiscal year biennium");
            Assert.That(HttpRequestStorage.DatabaseEntities.CurrentBiennia.Count() == 1, "There should not be multiple entries in the CurrentBiennium table. The single entry should correspond to the current fiscal year biennium");

            int currentBienniumFiscalYear = HttpRequestStorage.DatabaseEntities.CurrentBiennia.First().CurrentBienniumFiscalYear;
            int currentFiscalYear = LtInfo.Common.DateUtilities.GetCurrentFiscalYear();

            Assert.That(currentFiscalYear - currentBienniumFiscalYear < 2, $"We now appear to be outside the current biennium, which is [{currentBienniumFiscalYear}-{currentBienniumFiscalYear+1}]. Current Fiscal year is {currentFiscalYear}.");
        }
    }
}
