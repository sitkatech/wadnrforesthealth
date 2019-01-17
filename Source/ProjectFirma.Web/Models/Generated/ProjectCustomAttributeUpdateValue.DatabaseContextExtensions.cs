//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[ProjectCustomAttributeUpdateValue]
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
        public static ProjectCustomAttributeUpdateValue GetProjectCustomAttributeUpdateValue(this IQueryable<ProjectCustomAttributeUpdateValue> projectCustomAttributeUpdateValues, int projectCustomAttributeUpdateValueID)
        {
            var projectCustomAttributeUpdateValue = projectCustomAttributeUpdateValues.SingleOrDefault(x => x.ProjectCustomAttributeUpdateValueID == projectCustomAttributeUpdateValueID);
            Check.RequireNotNullThrowNotFound(projectCustomAttributeUpdateValue, "ProjectCustomAttributeUpdateValue", projectCustomAttributeUpdateValueID);
            return projectCustomAttributeUpdateValue;
        }

        public static void DeleteProjectCustomAttributeUpdateValue(this IQueryable<ProjectCustomAttributeUpdateValue> projectCustomAttributeUpdateValues, List<int> projectCustomAttributeUpdateValueIDList)
        {
            if(projectCustomAttributeUpdateValueIDList.Any())
            {
                projectCustomAttributeUpdateValues.Where(x => projectCustomAttributeUpdateValueIDList.Contains(x.ProjectCustomAttributeUpdateValueID)).Delete();
            }
        }

        public static void DeleteProjectCustomAttributeUpdateValue(this IQueryable<ProjectCustomAttributeUpdateValue> projectCustomAttributeUpdateValues, ICollection<ProjectCustomAttributeUpdateValue> projectCustomAttributeUpdateValuesToDelete)
        {
            if(projectCustomAttributeUpdateValuesToDelete.Any())
            {
                var projectCustomAttributeUpdateValueIDList = projectCustomAttributeUpdateValuesToDelete.Select(x => x.ProjectCustomAttributeUpdateValueID).ToList();
                projectCustomAttributeUpdateValues.Where(x => projectCustomAttributeUpdateValueIDList.Contains(x.ProjectCustomAttributeUpdateValueID)).Delete();
            }
        }

        public static void DeleteProjectCustomAttributeUpdateValue(this IQueryable<ProjectCustomAttributeUpdateValue> projectCustomAttributeUpdateValues, int projectCustomAttributeUpdateValueID)
        {
            DeleteProjectCustomAttributeUpdateValue(projectCustomAttributeUpdateValues, new List<int> { projectCustomAttributeUpdateValueID });
        }

        public static void DeleteProjectCustomAttributeUpdateValue(this IQueryable<ProjectCustomAttributeUpdateValue> projectCustomAttributeUpdateValues, ProjectCustomAttributeUpdateValue projectCustomAttributeUpdateValueToDelete)
        {
            DeleteProjectCustomAttributeUpdateValue(projectCustomAttributeUpdateValues, new List<ProjectCustomAttributeUpdateValue> { projectCustomAttributeUpdateValueToDelete });
        }
    }
}