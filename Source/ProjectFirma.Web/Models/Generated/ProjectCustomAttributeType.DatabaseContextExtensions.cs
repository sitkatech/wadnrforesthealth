//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[ProjectCustomAttributeType]
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
        public static ProjectCustomAttributeType GetProjectCustomAttributeType(this IQueryable<ProjectCustomAttributeType> projectCustomAttributeTypes, int projectCustomAttributeTypeID)
        {
            var projectCustomAttributeType = projectCustomAttributeTypes.SingleOrDefault(x => x.ProjectCustomAttributeTypeID == projectCustomAttributeTypeID);
            Check.RequireNotNullThrowNotFound(projectCustomAttributeType, "ProjectCustomAttributeType", projectCustomAttributeTypeID);
            return projectCustomAttributeType;
        }

        public static void DeleteProjectCustomAttributeType(this IQueryable<ProjectCustomAttributeType> projectCustomAttributeTypes, List<int> projectCustomAttributeTypeIDList)
        {
            if(projectCustomAttributeTypeIDList.Any())
            {
                projectCustomAttributeTypes.Where(x => projectCustomAttributeTypeIDList.Contains(x.ProjectCustomAttributeTypeID)).Delete();
            }
        }

        public static void DeleteProjectCustomAttributeType(this IQueryable<ProjectCustomAttributeType> projectCustomAttributeTypes, ICollection<ProjectCustomAttributeType> projectCustomAttributeTypesToDelete)
        {
            if(projectCustomAttributeTypesToDelete.Any())
            {
                var projectCustomAttributeTypeIDList = projectCustomAttributeTypesToDelete.Select(x => x.ProjectCustomAttributeTypeID).ToList();
                projectCustomAttributeTypes.Where(x => projectCustomAttributeTypeIDList.Contains(x.ProjectCustomAttributeTypeID)).Delete();
            }
        }

        public static void DeleteProjectCustomAttributeType(this IQueryable<ProjectCustomAttributeType> projectCustomAttributeTypes, int projectCustomAttributeTypeID)
        {
            DeleteProjectCustomAttributeType(projectCustomAttributeTypes, new List<int> { projectCustomAttributeTypeID });
        }

        public static void DeleteProjectCustomAttributeType(this IQueryable<ProjectCustomAttributeType> projectCustomAttributeTypes, ProjectCustomAttributeType projectCustomAttributeTypeToDelete)
        {
            DeleteProjectCustomAttributeType(projectCustomAttributeTypes, new List<ProjectCustomAttributeType> { projectCustomAttributeTypeToDelete });
        }
    }
}