//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[FocusArea]
using System.Collections.Generic;
using System.Linq;
using LtInfo.Common.DesignByContract;
using LtInfo.Common.Models;
using ProjectFirma.Web.Common;

namespace ProjectFirma.Web.Models
{
    public static partial class DatabaseContextExtensions
    {
        public static FocusArea GetFocusArea(this IQueryable<FocusArea> focusAreas, int focusAreaID)
        {
            var focusArea = focusAreas.SingleOrDefault(x => x.FocusAreaID == focusAreaID);
            Check.RequireNotNullThrowNotFound(focusArea, "FocusArea", focusAreaID);
            return focusArea;
        }

        public static void DeleteFocusArea(this List<int> focusAreaIDList)
        {
            if(focusAreaIDList.Any())
            {
                HttpRequestStorage.DatabaseEntities.AllFocusAreas.RemoveRange(HttpRequestStorage.DatabaseEntities.FocusAreas.Where(x => focusAreaIDList.Contains(x.FocusAreaID)));
            }
        }

        public static void DeleteFocusArea(this ICollection<FocusArea> focusAreasToDelete)
        {
            if(focusAreasToDelete.Any())
            {
                HttpRequestStorage.DatabaseEntities.AllFocusAreas.RemoveRange(focusAreasToDelete);
            }
        }

        public static void DeleteFocusArea(this int focusAreaID)
        {
            DeleteFocusArea(new List<int> { focusAreaID });
        }

        public static void DeleteFocusArea(this FocusArea focusAreaToDelete)
        {
            DeleteFocusArea(new List<FocusArea> { focusAreaToDelete });
        }
    }
}