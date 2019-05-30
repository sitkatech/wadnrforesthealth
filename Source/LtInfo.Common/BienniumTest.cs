using NUnit.Framework;

namespace LtInfo.Common
{
    [TestFixture]
    public class BienniumTest
    {
        // public static int 

        [Test]
        public void GetBienniumForFiscalYearTest()
        {
            // Prior years
            Assert.That(DateUtilities.GetBienniumForGivenFiscalYear(2015) == 2015);
            Assert.That(DateUtilities.GetBienniumForGivenFiscalYear(2016) == 2015);
            Assert.That(DateUtilities.GetBienniumForGivenFiscalYear(2017) == 2017);
            Assert.That(DateUtilities.GetBienniumForGivenFiscalYear(2018) == 2017);

            // "Current" year (happens to be reference year code uses)
            Assert.That(DateUtilities.GetBienniumForGivenFiscalYear(2019) == 2019);

            // Future years
            Assert.That(DateUtilities.GetBienniumForGivenFiscalYear(2020) == 2019);
            Assert.That(DateUtilities.GetBienniumForGivenFiscalYear(2021) == 2021);
            Assert.That(DateUtilities.GetBienniumForGivenFiscalYear(2022) == 2021);
            Assert.That(DateUtilities.GetBienniumForGivenFiscalYear(2023) == 2023);
            Assert.That(DateUtilities.GetBienniumForGivenFiscalYear(2024) == 2023);
        }
    }
}