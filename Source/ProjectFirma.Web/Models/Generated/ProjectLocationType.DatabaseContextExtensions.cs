//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[ProjectLocationType]
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
        public static ProjectLocationType GetProjectLocationType(this IQueryable<ProjectLocationType> projectLocationTypes, int projectLocationTypeID)
        {
            var projectLocationType = projectLocationTypes.SingleOrDefault(x => x.ProjectLocationTypeID == projectLocationTypeID);
            Check.RequireNotNullThrowNotFound(projectLocationType, "ProjectLocationType", projectLocationTypeID);
            return projectLocationType;
        }

        public static void DeleteProjectLocationType(this IQueryable<ProjectLocationType> projectLocationTypes, List<int> projectLocationTypeIDList)
        {
            if(projectLocationTypeIDList.Any())
            {
                projectLocationTypes.Where(x => projectLocationTypeIDList.Contains(x.ProjectLocationTypeID)).Delete();
            }
        }

        public static void DeleteProjectLocationType(this IQueryable<ProjectLocationType> projectLocationTypes, ICollection<ProjectLocationType> projectLocationTypesToDelete)
        {
            if(projectLocationTypesToDelete.Any())
            {
                var projectLocationTypeIDList = projectLocationTypesToDelete.Select(x => x.ProjectLocationTypeID).ToList();
                projectLocationTypes.Where(x => projectLocationTypeIDList.Contains(x.ProjectLocationTypeID)).Delete();
            }
        }

        public static void DeleteProjectLocationType(this IQueryable<ProjectLocationType> projectLocationTypes, int projectLocationTypeID)
        {
            DeleteProjectLocationType(projectLocationTypes, new List<int> { projectLocationTypeID });
        }

        public static void DeleteProjectLocationType(this IQueryable<ProjectLocationType> projectLocationTypes, ProjectLocationType projectLocationTypeToDelete)
        {
            DeleteProjectLocationType(projectLocationTypes, new List<ProjectLocationType> { projectLocationTypeToDelete });
        }
    }
}