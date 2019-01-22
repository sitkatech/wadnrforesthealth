//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[ProjectPriorityArea]
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
        public static ProjectPriorityArea GetProjectPriorityArea(this IQueryable<ProjectPriorityArea> projectPriorityAreas, int projectPriorityAreaID)
        {
            var projectPriorityArea = projectPriorityAreas.SingleOrDefault(x => x.ProjectPriorityAreaID == projectPriorityAreaID);
            Check.RequireNotNullThrowNotFound(projectPriorityArea, "ProjectPriorityArea", projectPriorityAreaID);
            return projectPriorityArea;
        }

        public static void DeleteProjectPriorityArea(this IQueryable<ProjectPriorityArea> projectPriorityAreas, List<int> projectPriorityAreaIDList)
        {
            if(projectPriorityAreaIDList.Any())
            {
                projectPriorityAreas.Where(x => projectPriorityAreaIDList.Contains(x.ProjectPriorityAreaID)).Delete();
            }
        }

        public static void DeleteProjectPriorityArea(this IQueryable<ProjectPriorityArea> projectPriorityAreas, ICollection<ProjectPriorityArea> projectPriorityAreasToDelete)
        {
            if(projectPriorityAreasToDelete.Any())
            {
                var projectPriorityAreaIDList = projectPriorityAreasToDelete.Select(x => x.ProjectPriorityAreaID).ToList();
                projectPriorityAreas.Where(x => projectPriorityAreaIDList.Contains(x.ProjectPriorityAreaID)).Delete();
            }
        }

        public static void DeleteProjectPriorityArea(this IQueryable<ProjectPriorityArea> projectPriorityAreas, int projectPriorityAreaID)
        {
            DeleteProjectPriorityArea(projectPriorityAreas, new List<int> { projectPriorityAreaID });
        }

        public static void DeleteProjectPriorityArea(this IQueryable<ProjectPriorityArea> projectPriorityAreas, ProjectPriorityArea projectPriorityAreaToDelete)
        {
            DeleteProjectPriorityArea(projectPriorityAreas, new List<ProjectPriorityArea> { projectPriorityAreaToDelete });
        }
    }
}