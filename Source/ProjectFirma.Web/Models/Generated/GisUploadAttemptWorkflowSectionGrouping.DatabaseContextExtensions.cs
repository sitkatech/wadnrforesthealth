//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[GisUploadAttemptWorkflowSectionGrouping]
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
        public static GisUploadAttemptWorkflowSectionGrouping GetGisUploadAttemptWorkflowSectionGrouping(this IQueryable<GisUploadAttemptWorkflowSectionGrouping> gisUploadAttemptWorkflowSectionGroupings, int gisUploadAttemptWorkflowSectionGroupingID)
        {
            var gisUploadAttemptWorkflowSectionGrouping = gisUploadAttemptWorkflowSectionGroupings.SingleOrDefault(x => x.GisUploadAttemptWorkflowSectionGroupingID == gisUploadAttemptWorkflowSectionGroupingID);
            Check.RequireNotNullThrowNotFound(gisUploadAttemptWorkflowSectionGrouping, "GisUploadAttemptWorkflowSectionGrouping", gisUploadAttemptWorkflowSectionGroupingID);
            return gisUploadAttemptWorkflowSectionGrouping;
        }

        // Delete using an IDList (WADNR style)
        public static void DeleteGisUploadAttemptWorkflowSectionGrouping(this IQueryable<GisUploadAttemptWorkflowSectionGrouping> gisUploadAttemptWorkflowSectionGroupings, List<int> gisUploadAttemptWorkflowSectionGroupingIDList)
        {
            if(gisUploadAttemptWorkflowSectionGroupingIDList.Any())
            {
                var gisUploadAttemptWorkflowSectionGroupingsInSourceCollectionToDelete = gisUploadAttemptWorkflowSectionGroupings.Where(x => gisUploadAttemptWorkflowSectionGroupingIDList.Contains(x.GisUploadAttemptWorkflowSectionGroupingID));
                foreach (var gisUploadAttemptWorkflowSectionGroupingToDelete in gisUploadAttemptWorkflowSectionGroupingsInSourceCollectionToDelete.ToList())
                {
                    gisUploadAttemptWorkflowSectionGroupingToDelete.Delete(HttpRequestStorage.DatabaseEntities);
                }
            }
        }

        // Delete using an object list (WADNR style)
        public static void DeleteGisUploadAttemptWorkflowSectionGrouping(this IQueryable<GisUploadAttemptWorkflowSectionGrouping> gisUploadAttemptWorkflowSectionGroupings, ICollection<GisUploadAttemptWorkflowSectionGrouping> gisUploadAttemptWorkflowSectionGroupingsToDelete)
        {
            if(gisUploadAttemptWorkflowSectionGroupingsToDelete.Any())
            {
                var gisUploadAttemptWorkflowSectionGroupingIDList = gisUploadAttemptWorkflowSectionGroupingsToDelete.Select(x => x.GisUploadAttemptWorkflowSectionGroupingID).ToList();
                var gisUploadAttemptWorkflowSectionGroupingsToDeleteFromSourceList = gisUploadAttemptWorkflowSectionGroupings.Where(x => gisUploadAttemptWorkflowSectionGroupingIDList.Contains(x.GisUploadAttemptWorkflowSectionGroupingID)).ToList();

                foreach (var gisUploadAttemptWorkflowSectionGroupingToDelete in gisUploadAttemptWorkflowSectionGroupingsToDeleteFromSourceList)
                {
                    gisUploadAttemptWorkflowSectionGroupingToDelete.Delete(HttpRequestStorage.DatabaseEntities);
                }
            }
        }

        public static void DeleteGisUploadAttemptWorkflowSectionGrouping(this IQueryable<GisUploadAttemptWorkflowSectionGrouping> gisUploadAttemptWorkflowSectionGroupings, int gisUploadAttemptWorkflowSectionGroupingID)
        {
            DeleteGisUploadAttemptWorkflowSectionGrouping(gisUploadAttemptWorkflowSectionGroupings, new List<int> { gisUploadAttemptWorkflowSectionGroupingID });
        }

        public static void DeleteGisUploadAttemptWorkflowSectionGrouping(this IQueryable<GisUploadAttemptWorkflowSectionGrouping> gisUploadAttemptWorkflowSectionGroupings, GisUploadAttemptWorkflowSectionGrouping gisUploadAttemptWorkflowSectionGroupingToDelete)
        {
            DeleteGisUploadAttemptWorkflowSectionGrouping(gisUploadAttemptWorkflowSectionGroupings, new List<GisUploadAttemptWorkflowSectionGrouping> { gisUploadAttemptWorkflowSectionGroupingToDelete });
        }
    }
}