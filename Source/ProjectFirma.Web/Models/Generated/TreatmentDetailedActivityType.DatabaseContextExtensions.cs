//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[TreatmentDetailedActivityType]
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
        public static TreatmentDetailedActivityType GetTreatmentDetailedActivityType(this IQueryable<TreatmentDetailedActivityType> treatmentDetailedActivityTypes, int treatmentDetailedActivityTypeID)
        {
            var treatmentDetailedActivityType = treatmentDetailedActivityTypes.SingleOrDefault(x => x.TreatmentDetailedActivityTypeID == treatmentDetailedActivityTypeID);
            Check.RequireNotNullThrowNotFound(treatmentDetailedActivityType, "TreatmentDetailedActivityType", treatmentDetailedActivityTypeID);
            return treatmentDetailedActivityType;
        }

        // Delete using an IDList (WADNR style)
        public static void DeleteTreatmentDetailedActivityType(this IQueryable<TreatmentDetailedActivityType> treatmentDetailedActivityTypes, List<int> treatmentDetailedActivityTypeIDList)
        {
            if(treatmentDetailedActivityTypeIDList.Any())
            {
                var treatmentDetailedActivityTypesInSourceCollectionToDelete = treatmentDetailedActivityTypes.Where(x => treatmentDetailedActivityTypeIDList.Contains(x.TreatmentDetailedActivityTypeID));
                foreach (var treatmentDetailedActivityTypeToDelete in treatmentDetailedActivityTypesInSourceCollectionToDelete.ToList())
                {
                    treatmentDetailedActivityTypeToDelete.Delete(HttpRequestStorage.DatabaseEntities);
                }
            }
        }

        // Delete using an object list (WADNR style)
        public static void DeleteTreatmentDetailedActivityType(this IQueryable<TreatmentDetailedActivityType> treatmentDetailedActivityTypes, ICollection<TreatmentDetailedActivityType> treatmentDetailedActivityTypesToDelete)
        {
            if(treatmentDetailedActivityTypesToDelete.Any())
            {
                var treatmentDetailedActivityTypeIDList = treatmentDetailedActivityTypesToDelete.Select(x => x.TreatmentDetailedActivityTypeID).ToList();
                var treatmentDetailedActivityTypesToDeleteFromSourceList = treatmentDetailedActivityTypes.Where(x => treatmentDetailedActivityTypeIDList.Contains(x.TreatmentDetailedActivityTypeID)).ToList();

                foreach (var treatmentDetailedActivityTypeToDelete in treatmentDetailedActivityTypesToDeleteFromSourceList)
                {
                    treatmentDetailedActivityTypeToDelete.Delete(HttpRequestStorage.DatabaseEntities);
                }
            }
        }

        public static void DeleteTreatmentDetailedActivityType(this IQueryable<TreatmentDetailedActivityType> treatmentDetailedActivityTypes, int treatmentDetailedActivityTypeID)
        {
            DeleteTreatmentDetailedActivityType(treatmentDetailedActivityTypes, new List<int> { treatmentDetailedActivityTypeID });
        }

        public static void DeleteTreatmentDetailedActivityType(this IQueryable<TreatmentDetailedActivityType> treatmentDetailedActivityTypes, TreatmentDetailedActivityType treatmentDetailedActivityTypeToDelete)
        {
            DeleteTreatmentDetailedActivityType(treatmentDetailedActivityTypes, new List<TreatmentDetailedActivityType> { treatmentDetailedActivityTypeToDelete });
        }
    }
}