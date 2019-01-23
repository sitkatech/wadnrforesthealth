//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[FirmaHomePageImage]
using System.Collections.Generic;
using System.Linq;
using Z.EntityFramework.Plus;
using LtInfo.Common.DesignByContract;
using LtInfo.Common.Models;
using ProjectFirma.Web.Common;

namespace ProjectFirma.Web.Models
{
    public static partial class DatabaseContextExtensions
    {
        public static FirmaHomePageImage GetFirmaHomePageImage(this IQueryable<FirmaHomePageImage> firmaHomePageImages, int firmaHomePageImageID)
        {
            var firmaHomePageImage = firmaHomePageImages.SingleOrDefault(x => x.FirmaHomePageImageID == firmaHomePageImageID);
            Check.RequireNotNullThrowNotFound(firmaHomePageImage, "FirmaHomePageImage", firmaHomePageImageID);
            return firmaHomePageImage;
        }

        public static void DeleteFirmaHomePageImage(this IQueryable<FirmaHomePageImage> firmaHomePageImages, List<int> firmaHomePageImageIDList)
        {
            if(firmaHomePageImageIDList.Any())
            {
                firmaHomePageImages.Where(x => firmaHomePageImageIDList.Contains(x.FirmaHomePageImageID)).Delete();
            }
        }

        public static void DeleteFirmaHomePageImage(this IQueryable<FirmaHomePageImage> firmaHomePageImages, ICollection<FirmaHomePageImage> firmaHomePageImagesToDelete)
        {
            if(firmaHomePageImagesToDelete.Any())
            {
                var firmaHomePageImageIDList = firmaHomePageImagesToDelete.Select(x => x.FirmaHomePageImageID).ToList();
                firmaHomePageImages.Where(x => firmaHomePageImageIDList.Contains(x.FirmaHomePageImageID)).Delete();
            }
        }

        public static void DeleteFirmaHomePageImage(this IQueryable<FirmaHomePageImage> firmaHomePageImages, int firmaHomePageImageID)
        {
            DeleteFirmaHomePageImage(firmaHomePageImages, new List<int> { firmaHomePageImageID });
        }

        public static void DeleteFirmaHomePageImage(this IQueryable<FirmaHomePageImage> firmaHomePageImages, FirmaHomePageImage firmaHomePageImageToDelete)
        {
            DeleteFirmaHomePageImage(firmaHomePageImages, new List<FirmaHomePageImage> { firmaHomePageImageToDelete });
        }
    }
}