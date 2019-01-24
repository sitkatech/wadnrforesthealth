//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[ProjectTypePerformanceMeasure]
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
        public static ProjectTypePerformanceMeasure GetProjectTypePerformanceMeasure(this IQueryable<ProjectTypePerformanceMeasure> projectTypePerformanceMeasures, int projectTypePerformanceMeasureID)
        {
            var projectTypePerformanceMeasure = projectTypePerformanceMeasures.SingleOrDefault(x => x.ProjectTypePerformanceMeasureID == projectTypePerformanceMeasureID);
            Check.RequireNotNullThrowNotFound(projectTypePerformanceMeasure, "ProjectTypePerformanceMeasure", projectTypePerformanceMeasureID);
            return projectTypePerformanceMeasure;
        }

        public static void DeleteProjectTypePerformanceMeasure(this IQueryable<ProjectTypePerformanceMeasure> projectTypePerformanceMeasures, List<int> projectTypePerformanceMeasureIDList)
        {
            if(projectTypePerformanceMeasureIDList.Any())
            {
                projectTypePerformanceMeasures.Where(x => projectTypePerformanceMeasureIDList.Contains(x.ProjectTypePerformanceMeasureID)).Delete();
            }
        }

        public static void DeleteProjectTypePerformanceMeasure(this IQueryable<ProjectTypePerformanceMeasure> projectTypePerformanceMeasures, ICollection<ProjectTypePerformanceMeasure> projectTypePerformanceMeasuresToDelete)
        {
            if(projectTypePerformanceMeasuresToDelete.Any())
            {
                var projectTypePerformanceMeasureIDList = projectTypePerformanceMeasuresToDelete.Select(x => x.ProjectTypePerformanceMeasureID).ToList();
                projectTypePerformanceMeasures.Where(x => projectTypePerformanceMeasureIDList.Contains(x.ProjectTypePerformanceMeasureID)).Delete();
            }
        }

        public static void DeleteProjectTypePerformanceMeasure(this IQueryable<ProjectTypePerformanceMeasure> projectTypePerformanceMeasures, int projectTypePerformanceMeasureID)
        {
            DeleteProjectTypePerformanceMeasure(projectTypePerformanceMeasures, new List<int> { projectTypePerformanceMeasureID });
        }

        public static void DeleteProjectTypePerformanceMeasure(this IQueryable<ProjectTypePerformanceMeasure> projectTypePerformanceMeasures, ProjectTypePerformanceMeasure projectTypePerformanceMeasureToDelete)
        {
            DeleteProjectTypePerformanceMeasure(projectTypePerformanceMeasures, new List<ProjectTypePerformanceMeasure> { projectTypePerformanceMeasureToDelete });
        }
    }
}