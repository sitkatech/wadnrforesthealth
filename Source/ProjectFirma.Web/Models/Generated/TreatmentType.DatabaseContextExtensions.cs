//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[TreatmentType]
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
        public static TreatmentType GetTreatmentType(this IQueryable<TreatmentType> treatmentTypes, int treatmentTypeID)
        {
            var treatmentType = treatmentTypes.SingleOrDefault(x => x.TreatmentTypeID == treatmentTypeID);
            Check.RequireNotNullThrowNotFound(treatmentType, "TreatmentType", treatmentTypeID);
            return treatmentType;
        }

        public static void DeleteTreatmentType(this IQueryable<TreatmentType> treatmentTypes, List<int> treatmentTypeIDList)
        {
            if(treatmentTypeIDList.Any())
            {
                treatmentTypes.Where(x => treatmentTypeIDList.Contains(x.TreatmentTypeID)).Delete();
            }
        }

        public static void DeleteTreatmentType(this IQueryable<TreatmentType> treatmentTypes, ICollection<TreatmentType> treatmentTypesToDelete)
        {
            if(treatmentTypesToDelete.Any())
            {
                var treatmentTypeIDList = treatmentTypesToDelete.Select(x => x.TreatmentTypeID).ToList();
                treatmentTypes.Where(x => treatmentTypeIDList.Contains(x.TreatmentTypeID)).Delete();
            }
        }

        public static void DeleteTreatmentType(this IQueryable<TreatmentType> treatmentTypes, int treatmentTypeID)
        {
            DeleteTreatmentType(treatmentTypes, new List<int> { treatmentTypeID });
        }

        public static void DeleteTreatmentType(this IQueryable<TreatmentType> treatmentTypes, TreatmentType treatmentTypeToDelete)
        {
            DeleteTreatmentType(treatmentTypes, new List<TreatmentType> { treatmentTypeToDelete });
        }
    }
}