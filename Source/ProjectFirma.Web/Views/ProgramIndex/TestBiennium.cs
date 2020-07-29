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

using System;
using System.Linq;
using LtInfo.Common;
using NUnit.Framework;
using ProjectFirma.Web.Common;
using ProjectFirma.Web.Models;

namespace ProjectFirma.Web.Views.ProgramIndex
{
    [TestFixture]
    public class BienniumTest
    {
        [Test]
        public void CheckBienniumFiscalYearCalculation()
        {
            int currentFiscalYear = LtInfo.Common.DateUtilities.GetCurrentFiscalYear();

            Assert.That( CurrentBiennium.GetBienniumFiscalYearForDate(new DateTime(2019, 7, 1)) == 2019);
            Assert.That( CurrentBiennium.GetBienniumFiscalYearForDate(new DateTime(2020, 1, 1)) == 2019);
            Assert.That( CurrentBiennium.GetBienniumFiscalYearForDate(new DateTime(2020, 7, 1)) == 2019);
            Assert.That( CurrentBiennium.GetBienniumFiscalYearForDate(new DateTime(2021, 1, 1)) == 2019);

            Assert.That(CurrentBiennium.GetBienniumFiscalYearForDate(new DateTime(2021, 7, 1)) == 2021);
            Assert.That(CurrentBiennium.GetBienniumFiscalYearForDate(new DateTime(2022, 1, 1)) == 2021);
            Assert.That(CurrentBiennium.GetBienniumFiscalYearForDate(new DateTime(2022, 7, 1)) == 2021);
            Assert.That(CurrentBiennium.GetBienniumFiscalYearForDate(new DateTime(2023, 1, 1)) == 2021);
        }

        [Test]
        public void CheckFiscalYearCalculation()
        {
            int currentFiscalYear = LtInfo.Common.DateUtilities.GetCurrentFiscalYear();

            Assert.That(new DateTime(2019, 7, 1).GetWadnrFiscalYear() == 2019);
            Assert.That(new DateTime(2020, 1, 1).GetWadnrFiscalYear() == 2019);

            Assert.That(new DateTime(2020, 7, 1).GetWadnrFiscalYear() == 2020);
            Assert.That(new DateTime(2021, 1, 1).GetWadnrFiscalYear() == 2020);
                        
            Assert.That(new DateTime(2021, 7, 1).GetWadnrFiscalYear() == 2021);
            Assert.That(new DateTime(2022, 1, 1).GetWadnrFiscalYear() == 2021);

            Assert.That(new DateTime(2022, 7, 1).GetWadnrFiscalYear() == 2022);
            Assert.That(new DateTime(2023, 1, 1).GetWadnrFiscalYear() == 2022);
        }

        [Test]
        public void EnsureBienniumCalculationIsLikelyCorrectTest()
        {
            int currentBienniumAccordingToTheDatabase = CurrentBiennium.GetCurrentBienniumFiscalYearFromDatabase();
            int currentFiscalYear = LtInfo.Common.DateUtilities.GetCurrentFiscalYear();

            Assert.That(currentFiscalYear - currentBienniumAccordingToTheDatabase < 2, $"We now appear to be outside the current biennium, which is [{currentBienniumAccordingToTheDatabase}-{currentBienniumAccordingToTheDatabase + 1}]. Current Fiscal year is {currentFiscalYear}.");
        }
    }
}
