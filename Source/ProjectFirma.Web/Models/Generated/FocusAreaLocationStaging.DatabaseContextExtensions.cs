//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[FocusAreaLocationStaging]
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
        public static FocusAreaLocationStaging GetFocusAreaLocationStaging(this IQueryable<FocusAreaLocationStaging> focusAreaLocationStagings, int focusAreaLocationStaggingID)
        {
            var focusAreaLocationStaging = focusAreaLocationStagings.SingleOrDefault(x => x.FocusAreaLocationStaggingID == focusAreaLocationStaggingID);
            Check.RequireNotNullThrowNotFound(focusAreaLocationStaging, "FocusAreaLocationStaging", focusAreaLocationStaggingID);
            return focusAreaLocationStaging;
        }

        public static void DeleteFocusAreaLocationStaging(this IQueryable<FocusAreaLocationStaging> focusAreaLocationStagings, List<int> focusAreaLocationStaggingIDList)
        {
            if(focusAreaLocationStaggingIDList.Any())
            {
                focusAreaLocationStagings.Where(x => focusAreaLocationStaggingIDList.Contains(x.FocusAreaLocationStaggingID)).Delete();
            }
        }

        public static void DeleteFocusAreaLocationStaging(this IQueryable<FocusAreaLocationStaging> focusAreaLocationStagings, ICollection<FocusAreaLocationStaging> focusAreaLocationStagingsToDelete)
        {
            if(focusAreaLocationStagingsToDelete.Any())
            {
                var focusAreaLocationStaggingIDList = focusAreaLocationStagingsToDelete.Select(x => x.FocusAreaLocationStaggingID).ToList();
                focusAreaLocationStagings.Where(x => focusAreaLocationStaggingIDList.Contains(x.FocusAreaLocationStaggingID)).Delete();
            }
        }

        public static void DeleteFocusAreaLocationStaging(this IQueryable<FocusAreaLocationStaging> focusAreaLocationStagings, int focusAreaLocationStaggingID)
        {
            DeleteFocusAreaLocationStaging(focusAreaLocationStagings, new List<int> { focusAreaLocationStaggingID });
        }

        public static void DeleteFocusAreaLocationStaging(this IQueryable<FocusAreaLocationStaging> focusAreaLocationStagings, FocusAreaLocationStaging focusAreaLocationStagingToDelete)
        {
            DeleteFocusAreaLocationStaging(focusAreaLocationStagings, new List<FocusAreaLocationStaging> { focusAreaLocationStagingToDelete });
        }
    }
}