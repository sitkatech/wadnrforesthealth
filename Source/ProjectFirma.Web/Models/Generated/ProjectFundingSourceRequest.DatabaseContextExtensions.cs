//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[ProjectFundingSourceRequest]
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
        public static ProjectFundingSourceRequest GetProjectFundingSourceRequest(this IQueryable<ProjectFundingSourceRequest> projectFundingSourceRequests, int projectFundingSourceRequestID)
        {
            var projectFundingSourceRequest = projectFundingSourceRequests.SingleOrDefault(x => x.ProjectFundingSourceRequestID == projectFundingSourceRequestID);
            Check.RequireNotNullThrowNotFound(projectFundingSourceRequest, "ProjectFundingSourceRequest", projectFundingSourceRequestID);
            return projectFundingSourceRequest;
        }

        public static void DeleteProjectFundingSourceRequest(this IQueryable<ProjectFundingSourceRequest> projectFundingSourceRequests, List<int> projectFundingSourceRequestIDList)
        {
            if(projectFundingSourceRequestIDList.Any())
            {
                projectFundingSourceRequests.Where(x => projectFundingSourceRequestIDList.Contains(x.ProjectFundingSourceRequestID)).Delete();
            }
        }

        public static void DeleteProjectFundingSourceRequest(this IQueryable<ProjectFundingSourceRequest> projectFundingSourceRequests, ICollection<ProjectFundingSourceRequest> projectFundingSourceRequestsToDelete)
        {
            if(projectFundingSourceRequestsToDelete.Any())
            {
                var projectFundingSourceRequestIDList = projectFundingSourceRequestsToDelete.Select(x => x.ProjectFundingSourceRequestID).ToList();
                projectFundingSourceRequests.Where(x => projectFundingSourceRequestIDList.Contains(x.ProjectFundingSourceRequestID)).Delete();
            }
        }

        public static void DeleteProjectFundingSourceRequest(this IQueryable<ProjectFundingSourceRequest> projectFundingSourceRequests, int projectFundingSourceRequestID)
        {
            DeleteProjectFundingSourceRequest(projectFundingSourceRequests, new List<int> { projectFundingSourceRequestID });
        }

        public static void DeleteProjectFundingSourceRequest(this IQueryable<ProjectFundingSourceRequest> projectFundingSourceRequests, ProjectFundingSourceRequest projectFundingSourceRequestToDelete)
        {
            DeleteProjectFundingSourceRequest(projectFundingSourceRequests, new List<ProjectFundingSourceRequest> { projectFundingSourceRequestToDelete });
        }
    }
}