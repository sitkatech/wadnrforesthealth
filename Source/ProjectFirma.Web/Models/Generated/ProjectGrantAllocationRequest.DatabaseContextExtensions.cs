//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[ProjectGrantAllocationRequest]
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
        public static ProjectGrantAllocationRequest GetProjectGrantAllocationRequest(this IQueryable<ProjectGrantAllocationRequest> projectGrantAllocationRequests, int projectGrantAllocationRequestID)
        {
            var projectGrantAllocationRequest = projectGrantAllocationRequests.SingleOrDefault(x => x.ProjectGrantAllocationRequestID == projectGrantAllocationRequestID);
            Check.RequireNotNullThrowNotFound(projectGrantAllocationRequest, "ProjectGrantAllocationRequest", projectGrantAllocationRequestID);
            return projectGrantAllocationRequest;
        }

        // Delete using an IDList (WADNR style)
        public static void DeleteProjectGrantAllocationRequest(this IQueryable<ProjectGrantAllocationRequest> projectGrantAllocationRequests, List<int> projectGrantAllocationRequestIDList)
        {
            if(projectGrantAllocationRequestIDList.Any())
            {
                var projectGrantAllocationRequestsInSourceCollectionToDelete = projectGrantAllocationRequests.Where(x => projectGrantAllocationRequestIDList.Contains(x.ProjectGrantAllocationRequestID));
                foreach (var projectGrantAllocationRequestToDelete in projectGrantAllocationRequestsInSourceCollectionToDelete.ToList())
                {
                    projectGrantAllocationRequestToDelete.Delete(HttpRequestStorage.DatabaseEntities);
                }
            }
        }

        // Delete using an object list (WADNR style)
        public static void DeleteProjectGrantAllocationRequest(this IQueryable<ProjectGrantAllocationRequest> projectGrantAllocationRequests, ICollection<ProjectGrantAllocationRequest> projectGrantAllocationRequestsToDelete)
        {
            if(projectGrantAllocationRequestsToDelete.Any())
            {
                var projectGrantAllocationRequestIDList = projectGrantAllocationRequestsToDelete.Select(x => x.ProjectGrantAllocationRequestID).ToList();
                var projectGrantAllocationRequestsToDeleteFromSourceList = projectGrantAllocationRequests.Where(x => projectGrantAllocationRequestIDList.Contains(x.ProjectGrantAllocationRequestID)).ToList();

                foreach (var projectGrantAllocationRequestToDelete in projectGrantAllocationRequestsToDeleteFromSourceList)
                {
                    projectGrantAllocationRequestToDelete.Delete(HttpRequestStorage.DatabaseEntities);
                }
            }
        }

        public static void DeleteProjectGrantAllocationRequest(this IQueryable<ProjectGrantAllocationRequest> projectGrantAllocationRequests, int projectGrantAllocationRequestID)
        {
            DeleteProjectGrantAllocationRequest(projectGrantAllocationRequests, new List<int> { projectGrantAllocationRequestID });
        }

        public static void DeleteProjectGrantAllocationRequest(this IQueryable<ProjectGrantAllocationRequest> projectGrantAllocationRequests, ProjectGrantAllocationRequest projectGrantAllocationRequestToDelete)
        {
            DeleteProjectGrantAllocationRequest(projectGrantAllocationRequests, new List<ProjectGrantAllocationRequest> { projectGrantAllocationRequestToDelete });
        }
    }
}