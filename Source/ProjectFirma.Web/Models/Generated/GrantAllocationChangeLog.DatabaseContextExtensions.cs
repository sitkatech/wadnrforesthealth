//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[GrantAllocationChangeLog]
using System.Collections.Generic;
using System.Linq;
using Z.EntityFramework.Plus;
using CodeFirstStoreFunctions;
using LtInfo.Common.DesignByContract;
using LtInfo.Common.Models;
using ProjectFirma.Web.Common;

namespace ProjectFirma.Web.Models
{
    public static partial class DatabaseContextExtensions
    {
        public static GrantAllocationChangeLog GetGrantAllocationChangeLog(this IQueryable<GrantAllocationChangeLog> grantAllocationChangeLogs, int grantAllocationChangeLogID)
        {
            var grantAllocationChangeLog = grantAllocationChangeLogs.SingleOrDefault(x => x.GrantAllocationChangeLogID == grantAllocationChangeLogID);
            Check.RequireNotNullThrowNotFound(grantAllocationChangeLog, "GrantAllocationChangeLog", grantAllocationChangeLogID);
            return grantAllocationChangeLog;
        }

        // Delete using an IDList (WADNR style)
        public static void DeleteGrantAllocationChangeLog(this IQueryable<GrantAllocationChangeLog> grantAllocationChangeLogs, List<int> grantAllocationChangeLogIDList)
        {
            if(grantAllocationChangeLogIDList.Any())
            {
                var grantAllocationChangeLogsInSourceCollectionToDelete = grantAllocationChangeLogs.Where(x => grantAllocationChangeLogIDList.Contains(x.GrantAllocationChangeLogID));
                foreach (var grantAllocationChangeLogToDelete in grantAllocationChangeLogsInSourceCollectionToDelete.ToList())
                {
                    grantAllocationChangeLogToDelete.Delete(HttpRequestStorage.DatabaseEntities);
                }
            }
        }

        // Delete using an object list (WADNR style)
        public static void DeleteGrantAllocationChangeLog(this IQueryable<GrantAllocationChangeLog> grantAllocationChangeLogs, ICollection<GrantAllocationChangeLog> grantAllocationChangeLogsToDelete)
        {
            if(grantAllocationChangeLogsToDelete.Any())
            {
                var grantAllocationChangeLogIDList = grantAllocationChangeLogsToDelete.Select(x => x.GrantAllocationChangeLogID).ToList();
                var grantAllocationChangeLogsToDeleteFromSourceList = grantAllocationChangeLogs.Where(x => grantAllocationChangeLogIDList.Contains(x.GrantAllocationChangeLogID)).ToList();

                foreach (var grantAllocationChangeLogToDelete in grantAllocationChangeLogsToDeleteFromSourceList)
                {
                    grantAllocationChangeLogToDelete.Delete(HttpRequestStorage.DatabaseEntities);
                }
            }
        }

        public static void DeleteGrantAllocationChangeLog(this IQueryable<GrantAllocationChangeLog> grantAllocationChangeLogs, int grantAllocationChangeLogID)
        {
            DeleteGrantAllocationChangeLog(grantAllocationChangeLogs, new List<int> { grantAllocationChangeLogID });
        }

        public static void DeleteGrantAllocationChangeLog(this IQueryable<GrantAllocationChangeLog> grantAllocationChangeLogs, GrantAllocationChangeLog grantAllocationChangeLogToDelete)
        {
            DeleteGrantAllocationChangeLog(grantAllocationChangeLogs, new List<GrantAllocationChangeLog> { grantAllocationChangeLogToDelete });
        }
    }
}