//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[PclBoundaryLine]
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
        public static PclBoundaryLine GetPclBoundaryLine(this IQueryable<PclBoundaryLine> pclBoundaryLines, int pclBoundaryLineID)
        {
            var pclBoundaryLine = pclBoundaryLines.SingleOrDefault(x => x.PclBoundaryLineID == pclBoundaryLineID);
            Check.RequireNotNullThrowNotFound(pclBoundaryLine, "PclBoundaryLine", pclBoundaryLineID);
            return pclBoundaryLine;
        }

        // Delete using an IDList (WADNR style)
        public static void DeletePclBoundaryLine(this IQueryable<PclBoundaryLine> pclBoundaryLines, List<int> pclBoundaryLineIDList)
        {
            if(pclBoundaryLineIDList.Any())
            {
                var pclBoundaryLinesInSourceCollectionToDelete = pclBoundaryLines.Where(x => pclBoundaryLineIDList.Contains(x.PclBoundaryLineID));
                foreach (var pclBoundaryLineToDelete in pclBoundaryLinesInSourceCollectionToDelete.ToList())
                {
                    pclBoundaryLineToDelete.Delete(HttpRequestStorage.DatabaseEntities);
                }
            }
        }

        // Delete using an object list (WADNR style)
        public static void DeletePclBoundaryLine(this IQueryable<PclBoundaryLine> pclBoundaryLines, ICollection<PclBoundaryLine> pclBoundaryLinesToDelete)
        {
            if(pclBoundaryLinesToDelete.Any())
            {
                var pclBoundaryLineIDList = pclBoundaryLinesToDelete.Select(x => x.PclBoundaryLineID).ToList();
                var pclBoundaryLinesToDeleteFromSourceList = pclBoundaryLines.Where(x => pclBoundaryLineIDList.Contains(x.PclBoundaryLineID)).ToList();

                foreach (var pclBoundaryLineToDelete in pclBoundaryLinesToDeleteFromSourceList)
                {
                    pclBoundaryLineToDelete.Delete(HttpRequestStorage.DatabaseEntities);
                }
            }
        }

        public static void DeletePclBoundaryLine(this IQueryable<PclBoundaryLine> pclBoundaryLines, int pclBoundaryLineID)
        {
            DeletePclBoundaryLine(pclBoundaryLines, new List<int> { pclBoundaryLineID });
        }

        public static void DeletePclBoundaryLine(this IQueryable<PclBoundaryLine> pclBoundaryLines, PclBoundaryLine pclBoundaryLineToDelete)
        {
            DeletePclBoundaryLine(pclBoundaryLines, new List<PclBoundaryLine> { pclBoundaryLineToDelete });
        }
    }
}