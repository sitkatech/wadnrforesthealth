//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[ProjectCustomGridColumn]
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
        public static ProjectCustomGridColumn GetProjectCustomGridColumn(this IQueryable<ProjectCustomGridColumn> projectCustomGridColumns, int projectCustomGridColumnID)
        {
            var projectCustomGridColumn = projectCustomGridColumns.SingleOrDefault(x => x.ProjectCustomGridColumnID == projectCustomGridColumnID);
            Check.RequireNotNullThrowNotFound(projectCustomGridColumn, "ProjectCustomGridColumn", projectCustomGridColumnID);
            return projectCustomGridColumn;
        }

        // Delete using an IDList (WADNR style)
        public static void DeleteProjectCustomGridColumn(this IQueryable<ProjectCustomGridColumn> projectCustomGridColumns, List<int> projectCustomGridColumnIDList)
        {
            if(projectCustomGridColumnIDList.Any())
            {
                var projectCustomGridColumnsInSourceCollectionToDelete = projectCustomGridColumns.Where(x => projectCustomGridColumnIDList.Contains(x.ProjectCustomGridColumnID));
                foreach (var projectCustomGridColumnToDelete in projectCustomGridColumnsInSourceCollectionToDelete.ToList())
                {
                    projectCustomGridColumnToDelete.Delete(HttpRequestStorage.DatabaseEntities);
                }
            }
        }

        // Delete using an object list (WADNR style)
        public static void DeleteProjectCustomGridColumn(this IQueryable<ProjectCustomGridColumn> projectCustomGridColumns, ICollection<ProjectCustomGridColumn> projectCustomGridColumnsToDelete)
        {
            if(projectCustomGridColumnsToDelete.Any())
            {
                var projectCustomGridColumnIDList = projectCustomGridColumnsToDelete.Select(x => x.ProjectCustomGridColumnID).ToList();
                var projectCustomGridColumnsToDeleteFromSourceList = projectCustomGridColumns.Where(x => projectCustomGridColumnIDList.Contains(x.ProjectCustomGridColumnID)).ToList();

                foreach (var projectCustomGridColumnToDelete in projectCustomGridColumnsToDeleteFromSourceList)
                {
                    projectCustomGridColumnToDelete.Delete(HttpRequestStorage.DatabaseEntities);
                }
            }
        }

        public static void DeleteProjectCustomGridColumn(this IQueryable<ProjectCustomGridColumn> projectCustomGridColumns, int projectCustomGridColumnID)
        {
            DeleteProjectCustomGridColumn(projectCustomGridColumns, new List<int> { projectCustomGridColumnID });
        }

        public static void DeleteProjectCustomGridColumn(this IQueryable<ProjectCustomGridColumn> projectCustomGridColumns, ProjectCustomGridColumn projectCustomGridColumnToDelete)
        {
            DeleteProjectCustomGridColumn(projectCustomGridColumns, new List<ProjectCustomGridColumn> { projectCustomGridColumnToDelete });
        }
    }
}