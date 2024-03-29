//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[ProjectCustomAttribute]
using System.Collections.Generic;
using System.Linq;
using Z.EntityFramework.Plus;
using CodeFirstStoreFunctions;
using LtInfo.Common.DesignByContract;
using LtInfo.Common.Models;
using ProjectFirma.Web.Common;

namespace ProjectFirma.Web.Models
{
    public static partial class DatabaseContextExtensions
    {
        public static ProjectCustomAttribute GetProjectCustomAttribute(this IQueryable<ProjectCustomAttribute> projectCustomAttributes, int projectCustomAttributeID)
        {
            var projectCustomAttribute = projectCustomAttributes.SingleOrDefault(x => x.ProjectCustomAttributeID == projectCustomAttributeID);
            Check.RequireNotNullThrowNotFound(projectCustomAttribute, "ProjectCustomAttribute", projectCustomAttributeID);
            return projectCustomAttribute;
        }

        // Delete using an IDList (WADNR style)
        public static void DeleteProjectCustomAttribute(this IQueryable<ProjectCustomAttribute> projectCustomAttributes, List<int> projectCustomAttributeIDList)
        {
            if(projectCustomAttributeIDList.Any())
            {
                var projectCustomAttributesInSourceCollectionToDelete = projectCustomAttributes.Where(x => projectCustomAttributeIDList.Contains(x.ProjectCustomAttributeID));
                foreach (var projectCustomAttributeToDelete in projectCustomAttributesInSourceCollectionToDelete.ToList())
                {
                    projectCustomAttributeToDelete.Delete(HttpRequestStorage.DatabaseEntities);
                }
            }
        }

        // Delete using an object list (WADNR style)
        public static void DeleteProjectCustomAttribute(this IQueryable<ProjectCustomAttribute> projectCustomAttributes, ICollection<ProjectCustomAttribute> projectCustomAttributesToDelete)
        {
            if(projectCustomAttributesToDelete.Any())
            {
                var projectCustomAttributeIDList = projectCustomAttributesToDelete.Select(x => x.ProjectCustomAttributeID).ToList();
                var projectCustomAttributesToDeleteFromSourceList = projectCustomAttributes.Where(x => projectCustomAttributeIDList.Contains(x.ProjectCustomAttributeID)).ToList();

                foreach (var projectCustomAttributeToDelete in projectCustomAttributesToDeleteFromSourceList)
                {
                    projectCustomAttributeToDelete.Delete(HttpRequestStorage.DatabaseEntities);
                }
            }
        }

        public static void DeleteProjectCustomAttribute(this IQueryable<ProjectCustomAttribute> projectCustomAttributes, int projectCustomAttributeID)
        {
            DeleteProjectCustomAttribute(projectCustomAttributes, new List<int> { projectCustomAttributeID });
        }

        public static void DeleteProjectCustomAttribute(this IQueryable<ProjectCustomAttribute> projectCustomAttributes, ProjectCustomAttribute projectCustomAttributeToDelete)
        {
            DeleteProjectCustomAttribute(projectCustomAttributes, new List<ProjectCustomAttribute> { projectCustomAttributeToDelete });
        }
    }
}