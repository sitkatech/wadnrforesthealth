//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[ProjectTypeProjectCustomAttributeType]
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
        public static ProjectTypeProjectCustomAttributeType GetProjectTypeProjectCustomAttributeType(this IQueryable<ProjectTypeProjectCustomAttributeType> projectTypeProjectCustomAttributeTypes, int projectTypeProjectCustomAttributeTypeID)
        {
            var projectTypeProjectCustomAttributeType = projectTypeProjectCustomAttributeTypes.SingleOrDefault(x => x.ProjectTypeProjectCustomAttributeTypeID == projectTypeProjectCustomAttributeTypeID);
            Check.RequireNotNullThrowNotFound(projectTypeProjectCustomAttributeType, "ProjectTypeProjectCustomAttributeType", projectTypeProjectCustomAttributeTypeID);
            return projectTypeProjectCustomAttributeType;
        }

        // Delete using an IDList (WADNR style)
        public static void DeleteProjectTypeProjectCustomAttributeType(this IQueryable<ProjectTypeProjectCustomAttributeType> projectTypeProjectCustomAttributeTypes, List<int> projectTypeProjectCustomAttributeTypeIDList)
        {
            if(projectTypeProjectCustomAttributeTypeIDList.Any())
            {
                var projectTypeProjectCustomAttributeTypesInSourceCollectionToDelete = projectTypeProjectCustomAttributeTypes.Where(x => projectTypeProjectCustomAttributeTypeIDList.Contains(x.ProjectTypeProjectCustomAttributeTypeID));
                foreach (var projectTypeProjectCustomAttributeTypeToDelete in projectTypeProjectCustomAttributeTypesInSourceCollectionToDelete.ToList())
                {
                    projectTypeProjectCustomAttributeTypeToDelete.Delete(HttpRequestStorage.DatabaseEntities);
                }
            }
        }

        // Delete using an object list (WADNR style)
        public static void DeleteProjectTypeProjectCustomAttributeType(this IQueryable<ProjectTypeProjectCustomAttributeType> projectTypeProjectCustomAttributeTypes, ICollection<ProjectTypeProjectCustomAttributeType> projectTypeProjectCustomAttributeTypesToDelete)
        {
            if(projectTypeProjectCustomAttributeTypesToDelete.Any())
            {
                var projectTypeProjectCustomAttributeTypeIDList = projectTypeProjectCustomAttributeTypesToDelete.Select(x => x.ProjectTypeProjectCustomAttributeTypeID).ToList();
                var projectTypeProjectCustomAttributeTypesToDeleteFromSourceList = projectTypeProjectCustomAttributeTypes.Where(x => projectTypeProjectCustomAttributeTypeIDList.Contains(x.ProjectTypeProjectCustomAttributeTypeID)).ToList();

                foreach (var projectTypeProjectCustomAttributeTypeToDelete in projectTypeProjectCustomAttributeTypesToDeleteFromSourceList)
                {
                    projectTypeProjectCustomAttributeTypeToDelete.Delete(HttpRequestStorage.DatabaseEntities);
                }
            }
        }

        public static void DeleteProjectTypeProjectCustomAttributeType(this IQueryable<ProjectTypeProjectCustomAttributeType> projectTypeProjectCustomAttributeTypes, int projectTypeProjectCustomAttributeTypeID)
        {
            DeleteProjectTypeProjectCustomAttributeType(projectTypeProjectCustomAttributeTypes, new List<int> { projectTypeProjectCustomAttributeTypeID });
        }

        public static void DeleteProjectTypeProjectCustomAttributeType(this IQueryable<ProjectTypeProjectCustomAttributeType> projectTypeProjectCustomAttributeTypes, ProjectTypeProjectCustomAttributeType projectTypeProjectCustomAttributeTypeToDelete)
        {
            DeleteProjectTypeProjectCustomAttributeType(projectTypeProjectCustomAttributeTypes, new List<ProjectTypeProjectCustomAttributeType> { projectTypeProjectCustomAttributeTypeToDelete });
        }
    }
}