//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[GrantAllocationProgramManager]
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
        public static GrantAllocationProgramManager GetGrantAllocationProgramManager(this IQueryable<GrantAllocationProgramManager> grantAllocationProgramManagers, int grantAllocationProgramManagerID)
        {
            var grantAllocationProgramManager = grantAllocationProgramManagers.SingleOrDefault(x => x.GrantAllocationProgramManagerID == grantAllocationProgramManagerID);
            Check.RequireNotNullThrowNotFound(grantAllocationProgramManager, "GrantAllocationProgramManager", grantAllocationProgramManagerID);
            return grantAllocationProgramManager;
        }

        // Delete using an IDList (WADNR style)
        public static void DeleteGrantAllocationProgramManager(this IQueryable<GrantAllocationProgramManager> grantAllocationProgramManagers, List<int> grantAllocationProgramManagerIDList)
        {
            if(grantAllocationProgramManagerIDList.Any())
            {
                var grantAllocationProgramManagersInSourceCollectionToDelete = grantAllocationProgramManagers.Where(x => grantAllocationProgramManagerIDList.Contains(x.GrantAllocationProgramManagerID));
                foreach (var grantAllocationProgramManagerToDelete in grantAllocationProgramManagersInSourceCollectionToDelete.ToList())
                {
                    grantAllocationProgramManagerToDelete.Delete(HttpRequestStorage.DatabaseEntities);
                }
            }
        }

        // Delete using an object list (WADNR style)
        public static void DeleteGrantAllocationProgramManager(this IQueryable<GrantAllocationProgramManager> grantAllocationProgramManagers, ICollection<GrantAllocationProgramManager> grantAllocationProgramManagersToDelete)
        {
            if(grantAllocationProgramManagersToDelete.Any())
            {
                var grantAllocationProgramManagerIDList = grantAllocationProgramManagersToDelete.Select(x => x.GrantAllocationProgramManagerID).ToList();
                var grantAllocationProgramManagersToDeleteFromSourceList = grantAllocationProgramManagers.Where(x => grantAllocationProgramManagerIDList.Contains(x.GrantAllocationProgramManagerID)).ToList();

                foreach (var grantAllocationProgramManagerToDelete in grantAllocationProgramManagersToDeleteFromSourceList)
                {
                    grantAllocationProgramManagerToDelete.Delete(HttpRequestStorage.DatabaseEntities);
                }
            }
        }

        public static void DeleteGrantAllocationProgramManager(this IQueryable<GrantAllocationProgramManager> grantAllocationProgramManagers, int grantAllocationProgramManagerID)
        {
            DeleteGrantAllocationProgramManager(grantAllocationProgramManagers, new List<int> { grantAllocationProgramManagerID });
        }

        public static void DeleteGrantAllocationProgramManager(this IQueryable<GrantAllocationProgramManager> grantAllocationProgramManagers, GrantAllocationProgramManager grantAllocationProgramManagerToDelete)
        {
            DeleteGrantAllocationProgramManager(grantAllocationProgramManagers, new List<GrantAllocationProgramManager> { grantAllocationProgramManagerToDelete });
        }
    }
}