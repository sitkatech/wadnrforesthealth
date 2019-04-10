//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[ProjectGrantAllocationExpenditure]
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
        public static ProjectGrantAllocationExpenditure GetProjectGrantAllocationExpenditure(this IQueryable<ProjectGrantAllocationExpenditure> projectGrantAllocationExpenditures, int projectGrantAllocationExpenditureID)
        {
            var projectGrantAllocationExpenditure = projectGrantAllocationExpenditures.SingleOrDefault(x => x.ProjectGrantAllocationExpenditureID == projectGrantAllocationExpenditureID);
            Check.RequireNotNullThrowNotFound(projectGrantAllocationExpenditure, "ProjectGrantAllocationExpenditure", projectGrantAllocationExpenditureID);
            return projectGrantAllocationExpenditure;
        }

        // Delete using an IDList (WADNR style)
        public static void DeleteProjectGrantAllocationExpenditure(this IQueryable<ProjectGrantAllocationExpenditure> projectGrantAllocationExpenditures, List<int> projectGrantAllocationExpenditureIDList)
        {
            if(projectGrantAllocationExpenditureIDList.Any())
            {
                var projectGrantAllocationExpendituresInSourceCollectionToDelete = projectGrantAllocationExpenditures.Where(x => projectGrantAllocationExpenditureIDList.Contains(x.ProjectGrantAllocationExpenditureID));
                foreach (var projectGrantAllocationExpenditureToDelete in projectGrantAllocationExpendituresInSourceCollectionToDelete.ToList())
                {
                    projectGrantAllocationExpenditureToDelete.Delete(HttpRequestStorage.DatabaseEntities);
                }
            }
        }

        // Delete using an object list (WADNR style)
        public static void DeleteProjectGrantAllocationExpenditure(this IQueryable<ProjectGrantAllocationExpenditure> projectGrantAllocationExpenditures, ICollection<ProjectGrantAllocationExpenditure> projectGrantAllocationExpendituresToDelete)
        {
            if(projectGrantAllocationExpendituresToDelete.Any())
            {
                var projectGrantAllocationExpenditureIDList = projectGrantAllocationExpendituresToDelete.Select(x => x.ProjectGrantAllocationExpenditureID).ToList();
                var projectGrantAllocationExpendituresToDeleteFromSourceList = projectGrantAllocationExpenditures.Where(x => projectGrantAllocationExpenditureIDList.Contains(x.ProjectGrantAllocationExpenditureID)).ToList();

                foreach (var projectGrantAllocationExpenditureToDelete in projectGrantAllocationExpendituresToDeleteFromSourceList)
                {
                    projectGrantAllocationExpenditureToDelete.Delete(HttpRequestStorage.DatabaseEntities);
                }
            }
        }

        public static void DeleteProjectGrantAllocationExpenditure(this IQueryable<ProjectGrantAllocationExpenditure> projectGrantAllocationExpenditures, int projectGrantAllocationExpenditureID)
        {
            DeleteProjectGrantAllocationExpenditure(projectGrantAllocationExpenditures, new List<int> { projectGrantAllocationExpenditureID });
        }

        public static void DeleteProjectGrantAllocationExpenditure(this IQueryable<ProjectGrantAllocationExpenditure> projectGrantAllocationExpenditures, ProjectGrantAllocationExpenditure projectGrantAllocationExpenditureToDelete)
        {
            DeleteProjectGrantAllocationExpenditure(projectGrantAllocationExpenditures, new List<ProjectGrantAllocationExpenditure> { projectGrantAllocationExpenditureToDelete });
        }
    }
}