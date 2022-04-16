//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[PclLandscapeTreatmentPriority]
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
        public static PclLandscapeTreatmentPriority GetPclLandscapeTreatmentPriority(this IQueryable<PclLandscapeTreatmentPriority> pclLandscapeTreatmentPriorities, int pclLandscapeTreatmentPriorityID)
        {
            var pclLandscapeTreatmentPriority = pclLandscapeTreatmentPriorities.SingleOrDefault(x => x.PclLandscapeTreatmentPriorityID == pclLandscapeTreatmentPriorityID);
            Check.RequireNotNullThrowNotFound(pclLandscapeTreatmentPriority, "PclLandscapeTreatmentPriority", pclLandscapeTreatmentPriorityID);
            return pclLandscapeTreatmentPriority;
        }

        // Delete using an IDList (WADNR style)
        public static void DeletePclLandscapeTreatmentPriority(this IQueryable<PclLandscapeTreatmentPriority> pclLandscapeTreatmentPriorities, List<int> pclLandscapeTreatmentPriorityIDList)
        {
            if(pclLandscapeTreatmentPriorityIDList.Any())
            {
                var pclLandscapeTreatmentPrioritiesInSourceCollectionToDelete = pclLandscapeTreatmentPriorities.Where(x => pclLandscapeTreatmentPriorityIDList.Contains(x.PclLandscapeTreatmentPriorityID));
                foreach (var pclLandscapeTreatmentPriorityToDelete in pclLandscapeTreatmentPrioritiesInSourceCollectionToDelete.ToList())
                {
                    pclLandscapeTreatmentPriorityToDelete.Delete(HttpRequestStorage.DatabaseEntities);
                }
            }
        }

        // Delete using an object list (WADNR style)
        public static void DeletePclLandscapeTreatmentPriority(this IQueryable<PclLandscapeTreatmentPriority> pclLandscapeTreatmentPriorities, ICollection<PclLandscapeTreatmentPriority> pclLandscapeTreatmentPrioritiesToDelete)
        {
            if(pclLandscapeTreatmentPrioritiesToDelete.Any())
            {
                var pclLandscapeTreatmentPriorityIDList = pclLandscapeTreatmentPrioritiesToDelete.Select(x => x.PclLandscapeTreatmentPriorityID).ToList();
                var pclLandscapeTreatmentPrioritiesToDeleteFromSourceList = pclLandscapeTreatmentPriorities.Where(x => pclLandscapeTreatmentPriorityIDList.Contains(x.PclLandscapeTreatmentPriorityID)).ToList();

                foreach (var pclLandscapeTreatmentPriorityToDelete in pclLandscapeTreatmentPrioritiesToDeleteFromSourceList)
                {
                    pclLandscapeTreatmentPriorityToDelete.Delete(HttpRequestStorage.DatabaseEntities);
                }
            }
        }

        public static void DeletePclLandscapeTreatmentPriority(this IQueryable<PclLandscapeTreatmentPriority> pclLandscapeTreatmentPriorities, int pclLandscapeTreatmentPriorityID)
        {
            DeletePclLandscapeTreatmentPriority(pclLandscapeTreatmentPriorities, new List<int> { pclLandscapeTreatmentPriorityID });
        }

        public static void DeletePclLandscapeTreatmentPriority(this IQueryable<PclLandscapeTreatmentPriority> pclLandscapeTreatmentPriorities, PclLandscapeTreatmentPriority pclLandscapeTreatmentPriorityToDelete)
        {
            DeletePclLandscapeTreatmentPriority(pclLandscapeTreatmentPriorities, new List<PclLandscapeTreatmentPriority> { pclLandscapeTreatmentPriorityToDelete });
        }
    }
}