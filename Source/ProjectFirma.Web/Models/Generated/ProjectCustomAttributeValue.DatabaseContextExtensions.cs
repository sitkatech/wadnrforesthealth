//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[ProjectCustomAttributeValue]
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
        public static ProjectCustomAttributeValue GetProjectCustomAttributeValue(this IQueryable<ProjectCustomAttributeValue> projectCustomAttributeValues, int projectCustomAttributeValueID)
        {
            var projectCustomAttributeValue = projectCustomAttributeValues.SingleOrDefault(x => x.ProjectCustomAttributeValueID == projectCustomAttributeValueID);
            Check.RequireNotNullThrowNotFound(projectCustomAttributeValue, "ProjectCustomAttributeValue", projectCustomAttributeValueID);
            return projectCustomAttributeValue;
        }

        // Delete using an IDList (WADNR style)
        public static void DeleteProjectCustomAttributeValue(this IQueryable<ProjectCustomAttributeValue> projectCustomAttributeValues, List<int> projectCustomAttributeValueIDList)
        {
            if(projectCustomAttributeValueIDList.Any())
            {
                var projectCustomAttributeValuesInSourceCollectionToDelete = projectCustomAttributeValues.Where(x => projectCustomAttributeValueIDList.Contains(x.ProjectCustomAttributeValueID));
                foreach (var projectCustomAttributeValueToDelete in projectCustomAttributeValuesInSourceCollectionToDelete.ToList())
                {
                    projectCustomAttributeValueToDelete.Delete(HttpRequestStorage.DatabaseEntities);
                }
            }
        }

        // Delete using an object list (WADNR style)
        public static void DeleteProjectCustomAttributeValue(this IQueryable<ProjectCustomAttributeValue> projectCustomAttributeValues, ICollection<ProjectCustomAttributeValue> projectCustomAttributeValuesToDelete)
        {
            if(projectCustomAttributeValuesToDelete.Any())
            {
                var projectCustomAttributeValueIDList = projectCustomAttributeValuesToDelete.Select(x => x.ProjectCustomAttributeValueID).ToList();
                var projectCustomAttributeValuesToDeleteFromSourceList = projectCustomAttributeValues.Where(x => projectCustomAttributeValueIDList.Contains(x.ProjectCustomAttributeValueID)).ToList();

                foreach (var projectCustomAttributeValueToDelete in projectCustomAttributeValuesToDeleteFromSourceList)
                {
                    projectCustomAttributeValueToDelete.Delete(HttpRequestStorage.DatabaseEntities);
                }
            }
        }

        public static void DeleteProjectCustomAttributeValue(this IQueryable<ProjectCustomAttributeValue> projectCustomAttributeValues, int projectCustomAttributeValueID)
        {
            DeleteProjectCustomAttributeValue(projectCustomAttributeValues, new List<int> { projectCustomAttributeValueID });
        }

        public static void DeleteProjectCustomAttributeValue(this IQueryable<ProjectCustomAttributeValue> projectCustomAttributeValues, ProjectCustomAttributeValue projectCustomAttributeValueToDelete)
        {
            DeleteProjectCustomAttributeValue(projectCustomAttributeValues, new List<ProjectCustomAttributeValue> { projectCustomAttributeValueToDelete });
        }
    }
}