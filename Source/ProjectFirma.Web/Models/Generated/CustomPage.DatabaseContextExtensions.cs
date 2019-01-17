//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[CustomPage]
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
        public static CustomPage GetCustomPage(this IQueryable<CustomPage> customPages, int customPageID)
        {
            var customPage = customPages.SingleOrDefault(x => x.CustomPageID == customPageID);
            Check.RequireNotNullThrowNotFound(customPage, "CustomPage", customPageID);
            return customPage;
        }

        public static void DeleteCustomPage(this IQueryable<CustomPage> customPages, List<int> customPageIDList)
        {
            if(customPageIDList.Any())
            {
                customPages.Where(x => customPageIDList.Contains(x.CustomPageID)).Delete();
            }
        }

        public static void DeleteCustomPage(this IQueryable<CustomPage> customPages, ICollection<CustomPage> customPagesToDelete)
        {
            if(customPagesToDelete.Any())
            {
                var customPageIDList = customPagesToDelete.Select(x => x.CustomPageID).ToList();
                customPages.Where(x => customPageIDList.Contains(x.CustomPageID)).Delete();
            }
        }

        public static void DeleteCustomPage(this IQueryable<CustomPage> customPages, int customPageID)
        {
            DeleteCustomPage(customPages, new List<int> { customPageID });
        }

        public static void DeleteCustomPage(this IQueryable<CustomPage> customPages, CustomPage customPageToDelete)
        {
            DeleteCustomPage(customPages, new List<CustomPage> { customPageToDelete });
        }
    }
}