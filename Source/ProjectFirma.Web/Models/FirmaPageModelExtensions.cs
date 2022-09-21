using System;
using System.Linq;
using ProjectFirma.Web.Common;
using ProjectFirmaModels.Models;

namespace ProjectFirma.Web.Models
{
    public static class FirmaPageModelExtensions
    {

        public static FirmaPage GetFirmaPage(this FirmaPageType firmaPageType)
        {
            var firmaPage = HttpRequestStorage.DatabaseEntities.FirmaPages.SingleOrDefault(x => x.FirmaPageTypeID == firmaPageType.FirmaPageTypeID) ??
                            MakePlaceholderFirmaPageForDisplay(firmaPageType);
            return firmaPage;
        }

        /// <summary>
        /// If there is no FirmaPage defined where we expect one, we return a placeholder that indicates one needs to be created, instead of just crashing.
        /// </summary>
        /// <param name="firmaPageType"></param>
        /// <returns></returns>
        private static FirmaPage MakePlaceholderFirmaPageForDisplay(FirmaPageType firmaPageType)
        {
            var firmaPage = new FirmaPage(firmaPageType.FirmaPageTypeID);
            // Comment
            firmaPage.FirmaPageContent = $"[No FirmaPage defined yet for FirmaPageTypeID: {firmaPageType.FirmaPageTypeID} \"{firmaPageType.FirmaPageTypeDisplayName}\"";
            return firmaPage;
        }

    }
}