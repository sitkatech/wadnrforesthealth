//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[FocusAreaLocationStaging]
using System.Collections.Generic;
using System.Linq;
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

        public static void DeleteFocusAreaLocationStaging(this List<int> focusAreaLocationStaggingIDList)
        {
            if(focusAreaLocationStaggingIDList.Any())
            {
                HttpRequestStorage.DatabaseEntities.AllFocusAreaLocationStagings.RemoveRange(HttpRequestStorage.DatabaseEntities.FocusAreaLocationStagings.Where(x => focusAreaLocationStaggingIDList.Contains(x.FocusAreaLocationStaggingID)));
            }
        }

        public static void DeleteFocusAreaLocationStaging(this ICollection<FocusAreaLocationStaging> focusAreaLocationStagingsToDelete)
        {
            if(focusAreaLocationStagingsToDelete.Any())
            {
                HttpRequestStorage.DatabaseEntities.AllFocusAreaLocationStagings.RemoveRange(focusAreaLocationStagingsToDelete);
            }
        }

        public static void DeleteFocusAreaLocationStaging(this int focusAreaLocationStaggingID)
        {
            DeleteFocusAreaLocationStaging(new List<int> { focusAreaLocationStaggingID });
        }

        public static void DeleteFocusAreaLocationStaging(this FocusAreaLocationStaging focusAreaLocationStagingToDelete)
        {
            DeleteFocusAreaLocationStaging(new List<FocusAreaLocationStaging> { focusAreaLocationStagingToDelete });
        }
    }
}