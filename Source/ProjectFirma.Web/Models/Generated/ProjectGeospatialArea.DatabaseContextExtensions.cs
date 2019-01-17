//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[ProjectGeospatialArea]
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
        public static ProjectGeospatialArea GetProjectGeospatialArea(this IQueryable<ProjectGeospatialArea> projectGeospatialAreas, int projectGeospatialAreaID)
        {
            var projectGeospatialArea = projectGeospatialAreas.SingleOrDefault(x => x.ProjectGeospatialAreaID == projectGeospatialAreaID);
            Check.RequireNotNullThrowNotFound(projectGeospatialArea, "ProjectGeospatialArea", projectGeospatialAreaID);
            return projectGeospatialArea;
        }

        public static void DeleteProjectGeospatialArea(this IQueryable<ProjectGeospatialArea> projectGeospatialAreas, List<int> projectGeospatialAreaIDList)
        {
            if(projectGeospatialAreaIDList.Any())
            {
                projectGeospatialAreas.Where(x => projectGeospatialAreaIDList.Contains(x.ProjectGeospatialAreaID)).Delete();
            }
        }

        public static void DeleteProjectGeospatialArea(this IQueryable<ProjectGeospatialArea> projectGeospatialAreas, ICollection<ProjectGeospatialArea> projectGeospatialAreasToDelete)
        {
            if(projectGeospatialAreasToDelete.Any())
            {
                var projectGeospatialAreaIDList = projectGeospatialAreasToDelete.Select(x => x.ProjectGeospatialAreaID).ToList();
                projectGeospatialAreas.Where(x => projectGeospatialAreaIDList.Contains(x.ProjectGeospatialAreaID)).Delete();
            }
        }

        public static void DeleteProjectGeospatialArea(this IQueryable<ProjectGeospatialArea> projectGeospatialAreas, int projectGeospatialAreaID)
        {
            DeleteProjectGeospatialArea(projectGeospatialAreas, new List<int> { projectGeospatialAreaID });
        }

        public static void DeleteProjectGeospatialArea(this IQueryable<ProjectGeospatialArea> projectGeospatialAreas, ProjectGeospatialArea projectGeospatialAreaToDelete)
        {
            DeleteProjectGeospatialArea(projectGeospatialAreas, new List<ProjectGeospatialArea> { projectGeospatialAreaToDelete });
        }
    }
}