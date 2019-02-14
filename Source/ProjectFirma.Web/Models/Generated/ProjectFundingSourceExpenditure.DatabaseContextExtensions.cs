//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[ProjectFundingSourceExpenditure]
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
        public static ProjectFundingSourceExpenditure GetProjectFundingSourceExpenditure(this IQueryable<ProjectFundingSourceExpenditure> projectFundingSourceExpenditures, int projectFundingSourceExpenditureID)
        {
            var projectFundingSourceExpenditure = projectFundingSourceExpenditures.SingleOrDefault(x => x.ProjectFundingSourceExpenditureID == projectFundingSourceExpenditureID);
            Check.RequireNotNullThrowNotFound(projectFundingSourceExpenditure, "ProjectFundingSourceExpenditure", projectFundingSourceExpenditureID);
            return projectFundingSourceExpenditure;
        }

        // Delete using an IDList (WADNR style)
        public static void DeleteProjectFundingSourceExpenditure(this IQueryable<ProjectFundingSourceExpenditure> projectFundingSourceExpenditures, List<int> projectFundingSourceExpenditureIDList)
        {
            if(projectFundingSourceExpenditureIDList.Any())
            {
                var projectFundingSourceExpendituresInSourceCollectionToDelete = projectFundingSourceExpenditures.Where(x => projectFundingSourceExpenditureIDList.Contains(x.ProjectFundingSourceExpenditureID));
                foreach (var projectFundingSourceExpenditureToDelete in projectFundingSourceExpendituresInSourceCollectionToDelete.ToList())
                {
                    projectFundingSourceExpenditureToDelete.Delete(HttpRequestStorage.DatabaseEntities);
                }
            }
        }

        // Delete using an object list (WADNR style)
        public static void DeleteProjectFundingSourceExpenditure(this IQueryable<ProjectFundingSourceExpenditure> projectFundingSourceExpenditures, ICollection<ProjectFundingSourceExpenditure> projectFundingSourceExpendituresToDelete)
        {
            if(projectFundingSourceExpendituresToDelete.Any())
            {
                var projectFundingSourceExpenditureIDList = projectFundingSourceExpendituresToDelete.Select(x => x.ProjectFundingSourceExpenditureID).ToList();
                var projectFundingSourceExpendituresToDeleteFromSourceList = projectFundingSourceExpenditures.Where(x => projectFundingSourceExpenditureIDList.Contains(x.ProjectFundingSourceExpenditureID)).ToList();

                foreach (var projectFundingSourceExpenditureToDelete in projectFundingSourceExpendituresToDeleteFromSourceList)
                {
                    projectFundingSourceExpenditureToDelete.Delete(HttpRequestStorage.DatabaseEntities);
                }
            }
        }

        public static void DeleteProjectFundingSourceExpenditure(this IQueryable<ProjectFundingSourceExpenditure> projectFundingSourceExpenditures, int projectFundingSourceExpenditureID)
        {
            DeleteProjectFundingSourceExpenditure(projectFundingSourceExpenditures, new List<int> { projectFundingSourceExpenditureID });
        }

        public static void DeleteProjectFundingSourceExpenditure(this IQueryable<ProjectFundingSourceExpenditure> projectFundingSourceExpenditures, ProjectFundingSourceExpenditure projectFundingSourceExpenditureToDelete)
        {
            DeleteProjectFundingSourceExpenditure(projectFundingSourceExpenditures, new List<ProjectFundingSourceExpenditure> { projectFundingSourceExpenditureToDelete });
        }
    }
}