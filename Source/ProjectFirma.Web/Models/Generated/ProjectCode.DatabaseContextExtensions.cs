//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[ProjectCode]
using System.Collections.Generic;
using System.Linq;
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

        public static void DeleteProjectCode(this List<int> projectCodeIDList)
        {
            if(projectCodeIDList.Any())
            {
                HttpRequestStorage.DatabaseEntities.AllProjectCodes.RemoveRange(HttpRequestStorage.DatabaseEntities.ProjectCodes.Where(x => projectCodeIDList.Contains(x.ProjectCodeID)));
            }
        }

        public static void DeleteProjectCode(this ICollection<ProjectCode> projectCodesToDelete)
        {
            if(projectCodesToDelete.Any())
            {
                HttpRequestStorage.DatabaseEntities.AllProjectCodes.RemoveRange(projectCodesToDelete);
            }
        }

        public static void DeleteProjectCode(this int projectCodeID)
        {
            DeleteProjectCode(new List<int> { projectCodeID });
        }

        public static void DeleteProjectCode(this ProjectCode projectCodeToDelete)
        {
            DeleteProjectCode(new List<ProjectCode> { projectCodeToDelete });
        }
    }
}