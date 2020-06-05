//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[GisUploadAttemptWorkflowSection]
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
        public static GisUploadAttemptWorkflowSection GetGisUploadAttemptWorkflowSection(this IQueryable<GisUploadAttemptWorkflowSection> gisUploadAttemptWorkflowSections, int gisUploadAttemptWorkflowSectionID)
        {
            var gisUploadAttemptWorkflowSection = gisUploadAttemptWorkflowSections.SingleOrDefault(x => x.GisUploadAttemptWorkflowSectionID == gisUploadAttemptWorkflowSectionID);
            Check.RequireNotNullThrowNotFound(gisUploadAttemptWorkflowSection, "GisUploadAttemptWorkflowSection", gisUploadAttemptWorkflowSectionID);
            return gisUploadAttemptWorkflowSection;
        }

        // Delete using an IDList (WADNR style)
        public static void DeleteGisUploadAttemptWorkflowSection(this IQueryable<GisUploadAttemptWorkflowSection> gisUploadAttemptWorkflowSections, List<int> gisUploadAttemptWorkflowSectionIDList)
        {
            if(gisUploadAttemptWorkflowSectionIDList.Any())
            {
                var gisUploadAttemptWorkflowSectionsInSourceCollectionToDelete = gisUploadAttemptWorkflowSections.Where(x => gisUploadAttemptWorkflowSectionIDList.Contains(x.GisUploadAttemptWorkflowSectionID));
                foreach (var gisUploadAttemptWorkflowSectionToDelete in gisUploadAttemptWorkflowSectionsInSourceCollectionToDelete.ToList())
                {
                    gisUploadAttemptWorkflowSectionToDelete.Delete(HttpRequestStorage.DatabaseEntities);
                }
            }
        }

        // Delete using an object list (WADNR style)
        public static void DeleteGisUploadAttemptWorkflowSection(this IQueryable<GisUploadAttemptWorkflowSection> gisUploadAttemptWorkflowSections, ICollection<GisUploadAttemptWorkflowSection> gisUploadAttemptWorkflowSectionsToDelete)
        {
            if(gisUploadAttemptWorkflowSectionsToDelete.Any())
            {
                var gisUploadAttemptWorkflowSectionIDList = gisUploadAttemptWorkflowSectionsToDelete.Select(x => x.GisUploadAttemptWorkflowSectionID).ToList();
                var gisUploadAttemptWorkflowSectionsToDeleteFromSourceList = gisUploadAttemptWorkflowSections.Where(x => gisUploadAttemptWorkflowSectionIDList.Contains(x.GisUploadAttemptWorkflowSectionID)).ToList();

                foreach (var gisUploadAttemptWorkflowSectionToDelete in gisUploadAttemptWorkflowSectionsToDeleteFromSourceList)
                {
                    gisUploadAttemptWorkflowSectionToDelete.Delete(HttpRequestStorage.DatabaseEntities);
                }
            }
        }

        public static void DeleteGisUploadAttemptWorkflowSection(this IQueryable<GisUploadAttemptWorkflowSection> gisUploadAttemptWorkflowSections, int gisUploadAttemptWorkflowSectionID)
        {
            DeleteGisUploadAttemptWorkflowSection(gisUploadAttemptWorkflowSections, new List<int> { gisUploadAttemptWorkflowSectionID });
        }

        public static void DeleteGisUploadAttemptWorkflowSection(this IQueryable<GisUploadAttemptWorkflowSection> gisUploadAttemptWorkflowSections, GisUploadAttemptWorkflowSection gisUploadAttemptWorkflowSectionToDelete)
        {
            DeleteGisUploadAttemptWorkflowSection(gisUploadAttemptWorkflowSections, new List<GisUploadAttemptWorkflowSection> { gisUploadAttemptWorkflowSectionToDelete });
        }
    }
}