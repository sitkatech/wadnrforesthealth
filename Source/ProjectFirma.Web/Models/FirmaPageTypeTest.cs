using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using ApprovalTests;
using ApprovalTests.Reporters;
using NUnit.Framework;
using ProjectFirma.Web.Common;

namespace ProjectFirma.Web.Models
{
    [TestFixture]
    public class FirmaPageTypeTest
    {
        [Test]
        [UseReporter(typeof(DiffReporter))]
        public void CheckForUncreatedFirmaPageForFirmaPageType()
        {
            var allFirmaPageTypes = FirmaPageType.All.ToList();
            HttpRequestStorage.DatabaseEntities.FirmaPages.Load();
            List<int> allFirmaPageTypeIDPresentInFirmaPages = HttpRequestStorage.DatabaseEntities.FirmaPages.Local.Select(fp => fp.FirmaPageTypeID).Distinct().ToList();

            string missingPageTypes = string.Empty;
            foreach (var firmaPageType in allFirmaPageTypes)
            {
                var pageTypeIsPresent = allFirmaPageTypeIDPresentInFirmaPages.Contains(firmaPageType.FirmaPageTypeID);
                if (!pageTypeIsPresent)
                {
                    missingPageTypes += $"Could Not find Firma Page Type '{firmaPageType.FirmaPageTypeName}'({firmaPageType.FirmaPageTypeID}) in Firma Pages\n\r";
                }

            }
            Approvals.Verify(missingPageTypes);
        }
    }
}