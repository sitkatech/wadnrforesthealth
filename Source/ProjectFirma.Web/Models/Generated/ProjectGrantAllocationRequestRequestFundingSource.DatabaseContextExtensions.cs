//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[ProjectGrantAllocationRequestRequestFundingSource]
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
        public static ProjectGrantAllocationRequestRequestFundingSource GetProjectGrantAllocationRequestRequestFundingSource(this IQueryable<ProjectGrantAllocationRequestRequestFundingSource> projectGrantAllocationRequestRequestFundingSources, int projectGrantAllocationRequestRequestFundingSourceID)
        {
            var projectGrantAllocationRequestRequestFundingSource = projectGrantAllocationRequestRequestFundingSources.SingleOrDefault(x => x.ProjectGrantAllocationRequestRequestFundingSourceID == projectGrantAllocationRequestRequestFundingSourceID);
            Check.RequireNotNullThrowNotFound(projectGrantAllocationRequestRequestFundingSource, "ProjectGrantAllocationRequestRequestFundingSource", projectGrantAllocationRequestRequestFundingSourceID);
            return projectGrantAllocationRequestRequestFundingSource;
        }

        // Delete using an IDList (WADNR style)
        public static void DeleteProjectGrantAllocationRequestRequestFundingSource(this IQueryable<ProjectGrantAllocationRequestRequestFundingSource> projectGrantAllocationRequestRequestFundingSources, List<int> projectGrantAllocationRequestRequestFundingSourceIDList)
        {
            if(projectGrantAllocationRequestRequestFundingSourceIDList.Any())
            {
                var projectGrantAllocationRequestRequestFundingSourcesInSourceCollectionToDelete = projectGrantAllocationRequestRequestFundingSources.Where(x => projectGrantAllocationRequestRequestFundingSourceIDList.Contains(x.ProjectGrantAllocationRequestRequestFundingSourceID));
                foreach (var projectGrantAllocationRequestRequestFundingSourceToDelete in projectGrantAllocationRequestRequestFundingSourcesInSourceCollectionToDelete.ToList())
                {
                    projectGrantAllocationRequestRequestFundingSourceToDelete.Delete(HttpRequestStorage.DatabaseEntities);
                }
            }
        }

        // Delete using an object list (WADNR style)
        public static void DeleteProjectGrantAllocationRequestRequestFundingSource(this IQueryable<ProjectGrantAllocationRequestRequestFundingSource> projectGrantAllocationRequestRequestFundingSources, ICollection<ProjectGrantAllocationRequestRequestFundingSource> projectGrantAllocationRequestRequestFundingSourcesToDelete)
        {
            if(projectGrantAllocationRequestRequestFundingSourcesToDelete.Any())
            {
                var projectGrantAllocationRequestRequestFundingSourceIDList = projectGrantAllocationRequestRequestFundingSourcesToDelete.Select(x => x.ProjectGrantAllocationRequestRequestFundingSourceID).ToList();
                var projectGrantAllocationRequestRequestFundingSourcesToDeleteFromSourceList = projectGrantAllocationRequestRequestFundingSources.Where(x => projectGrantAllocationRequestRequestFundingSourceIDList.Contains(x.ProjectGrantAllocationRequestRequestFundingSourceID)).ToList();

                foreach (var projectGrantAllocationRequestRequestFundingSourceToDelete in projectGrantAllocationRequestRequestFundingSourcesToDeleteFromSourceList)
                {
                    projectGrantAllocationRequestRequestFundingSourceToDelete.Delete(HttpRequestStorage.DatabaseEntities);
                }
            }
        }

        public static void DeleteProjectGrantAllocationRequestRequestFundingSource(this IQueryable<ProjectGrantAllocationRequestRequestFundingSource> projectGrantAllocationRequestRequestFundingSources, int projectGrantAllocationRequestRequestFundingSourceID)
        {
            DeleteProjectGrantAllocationRequestRequestFundingSource(projectGrantAllocationRequestRequestFundingSources, new List<int> { projectGrantAllocationRequestRequestFundingSourceID });
        }

        public static void DeleteProjectGrantAllocationRequestRequestFundingSource(this IQueryable<ProjectGrantAllocationRequestRequestFundingSource> projectGrantAllocationRequestRequestFundingSources, ProjectGrantAllocationRequestRequestFundingSource projectGrantAllocationRequestRequestFundingSourceToDelete)
        {
            DeleteProjectGrantAllocationRequestRequestFundingSource(projectGrantAllocationRequestRequestFundingSources, new List<ProjectGrantAllocationRequestRequestFundingSource> { projectGrantAllocationRequestRequestFundingSourceToDelete });
        }
    }
}