//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[tmpChildrenGrantsInGrantsTab]
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
        public static tmpChildrenGrantsInGrantsTab GettmpChildrenGrantsInGrantsTab(this IQueryable<tmpChildrenGrantsInGrantsTab> tmpChildrenGrantsInGrantsTabs, int childGrantID)
        {
            var tmpChildrenGrantsInGrantsTab = tmpChildrenGrantsInGrantsTabs.SingleOrDefault(x => x.ChildGrantID == childGrantID);
            Check.RequireNotNullThrowNotFound(tmpChildrenGrantsInGrantsTab, "tmpChildrenGrantsInGrantsTab", childGrantID);
            return tmpChildrenGrantsInGrantsTab;
        }

        public static void DeletetmpChildrenGrantsInGrantsTab(this IQueryable<tmpChildrenGrantsInGrantsTab> tmpChildrenGrantsInGrantsTabs, List<int> childGrantIDList)
        {
            if(childGrantIDList.Any())
            {
                tmpChildrenGrantsInGrantsTabs.Where(x => childGrantIDList.Contains(x.ChildGrantID)).Delete();
            }
        }

        public static void DeletetmpChildrenGrantsInGrantsTab(this IQueryable<tmpChildrenGrantsInGrantsTab> tmpChildrenGrantsInGrantsTabs, ICollection<tmpChildrenGrantsInGrantsTab> tmpChildrenGrantsInGrantsTabsToDelete)
        {
            if(tmpChildrenGrantsInGrantsTabsToDelete.Any())
            {
                var childGrantIDList = tmpChildrenGrantsInGrantsTabsToDelete.Select(x => x.ChildGrantID).ToList();
                tmpChildrenGrantsInGrantsTabs.Where(x => childGrantIDList.Contains(x.ChildGrantID)).Delete();
            }
        }

        public static void DeletetmpChildrenGrantsInGrantsTab(this IQueryable<tmpChildrenGrantsInGrantsTab> tmpChildrenGrantsInGrantsTabs, int childGrantID)
        {
            DeletetmpChildrenGrantsInGrantsTab(tmpChildrenGrantsInGrantsTabs, new List<int> { childGrantID });
        }

        public static void DeletetmpChildrenGrantsInGrantsTab(this IQueryable<tmpChildrenGrantsInGrantsTab> tmpChildrenGrantsInGrantsTabs, tmpChildrenGrantsInGrantsTab tmpChildrenGrantsInGrantsTabToDelete)
        {
            DeletetmpChildrenGrantsInGrantsTab(tmpChildrenGrantsInGrantsTabs, new List<tmpChildrenGrantsInGrantsTab> { tmpChildrenGrantsInGrantsTabToDelete });
        }
    }
}