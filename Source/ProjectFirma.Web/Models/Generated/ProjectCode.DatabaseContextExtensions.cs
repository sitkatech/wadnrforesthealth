//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[ProjectCode]
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
        public static ProjectCode GetProjectCode(this IQueryable<ProjectCode> projectCodes, int projectCodeID)
        {
            var projectCode = projectCodes.SingleOrDefault(x => x.ProjectCodeID == projectCodeID);
            Check.RequireNotNullThrowNotFound(projectCode, "ProjectCode", projectCodeID);
            return projectCode;
        }

        public static void DeleteProjectCode(this IQueryable<ProjectCode> projectCodes, List<int> projectCodeIDList)
        {
            if(projectCodeIDList.Any())
            {
                projectCodes.Where(x => projectCodeIDList.Contains(x.ProjectCodeID)).Delete();
            }
        }

        public static void DeleteProjectCode(this IQueryable<ProjectCode> projectCodes, ICollection<ProjectCode> projectCodesToDelete)
        {
            if(projectCodesToDelete.Any())
            {
                var projectCodeIDList = projectCodesToDelete.Select(x => x.ProjectCodeID).ToList();
                projectCodes.Where(x => projectCodeIDList.Contains(x.ProjectCodeID)).Delete();
            }
        }

        public static void DeleteProjectCode(this IQueryable<ProjectCode> projectCodes, int projectCodeID)
        {
            DeleteProjectCode(projectCodes, new List<int> { projectCodeID });
        }

        public static void DeleteProjectCode(this IQueryable<ProjectCode> projectCodes, ProjectCode projectCodeToDelete)
        {
            DeleteProjectCode(projectCodes, new List<ProjectCode> { projectCodeToDelete });
        }
    }
}