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

        // Delete using an IDList (WADNR style)
        public static void DeleteProjectCustomAttributeType(this IQueryable<ProjectCustomAttributeType> projectCustomAttributeTypes, List<int> projectCustomAttributeTypeIDList)
        {
            if(projectCustomAttributeTypeIDList.Any())
            {
                var projectCustomAttributeTypesInSourceCollectionToDelete = projectCustomAttributeTypes.Where(x => projectCustomAttributeTypeIDList.Contains(x.ProjectCustomAttributeTypeID));
                foreach (var projectCustomAttributeTypeToDelete in projectCustomAttributeTypesInSourceCollectionToDelete.ToList())
                {
                    projectCustomAttributeTypeToDelete.Delete(HttpRequestStorage.DatabaseEntities);
                }
            }
        }

        // Delete using an object list (WADNR style)
        public static void DeleteProjectCustomAttributeType(this IQueryable<ProjectCustomAttributeType> projectCustomAttributeTypes, ICollection<ProjectCustomAttributeType> projectCustomAttributeTypesToDelete)
        {
            if(projectCustomAttributeTypesToDelete.Any())
            {
                var projectCustomAttributeTypeIDList = projectCustomAttributeTypesToDelete.Select(x => x.ProjectCustomAttributeTypeID).ToList();
                var projectCustomAttributeTypesToDeleteFromSourceList = projectCustomAttributeTypes.Where(x => projectCustomAttributeTypeIDList.Contains(x.ProjectCustomAttributeTypeID)).ToList();

                foreach (var projectCustomAttributeTypeToDelete in projectCustomAttributeTypesToDeleteFromSourceList)
                {
                    projectCustomAttributeTypeToDelete.Delete(HttpRequestStorage.DatabaseEntities);
                }
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