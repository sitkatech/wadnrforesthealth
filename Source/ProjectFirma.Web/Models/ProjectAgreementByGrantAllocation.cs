using System.Collections.Generic;
using System.Linq;
using LtInfo.Common.DesignByContract;

namespace ProjectFirma.Web.Models
{
    public class ProjectAgreementByGrantAllocation
    {
        public GrantAllocation GrantAllocation { get; }
        public Project Project { get; }
        public Agreement Agreement { get; }

        /// <summary>
        /// Represents the relationship between a Project, Agreement, and GrantAllocation
        /// </summary>
        /// <param name="grantAllocation"></param>
        /// <param name="agreement"></param>
        /// <param name="project"></param>
        public ProjectAgreementByGrantAllocation(GrantAllocation grantAllocation, Agreement agreement, Project project)
        {
            Check.Ensure(grantAllocation.AgreementGrantAllocations.Select(aga => aga.AgreementID).Contains(agreement.AgreementID), $"Agreement {agreement.AgreementID} not found in AgreementGrantAllocations");
            Check.Ensure(grantAllocation.ProjectGrantAllocationRequests.Select(pgar => pgar.ProjectID).Contains(project.ProjectID), $"ProjectID {project.ProjectID} not found in ProjectGrantAllocationRequests");

            GrantAllocation = grantAllocation;
            Project = project;
            Agreement = agreement;
        }


        public static List<ProjectAgreementByGrantAllocation> MakeAgreementProjectsByGrantAllocation(List<AgreementGrantAllocation> agreementGrantAllocationsList)
        {
            List <ProjectAgreementByGrantAllocation > projectAgreementsByGrantAllocationsToReturn = new List<ProjectAgreementByGrantAllocation>();
            foreach (var agreementGrantAllocation in agreementGrantAllocationsList)
            {
                var grantAllocation = agreementGrantAllocation.GrantAllocation;
                foreach (var projectGrantAllocationExpenditures in grantAllocation.ProjectGrantAllocationRequests)
                {
                    projectAgreementsByGrantAllocationsToReturn.Add(new ProjectAgreementByGrantAllocation(grantAllocation, agreementGrantAllocation.Agreement, projectGrantAllocationExpenditures.Project));
                }
            }

            return projectAgreementsByGrantAllocationsToReturn;
        }

        public static List<ProjectAgreementByGrantAllocation> MakeAgreementProjectsByGrantAllocation(List<ProjectGrantAllocationRequest> projectGrantAllocationRequests)
        {
            List<ProjectAgreementByGrantAllocation> projectAgreementsByGrantAllocationsToReturn = new List<ProjectAgreementByGrantAllocation>();
            foreach (var projectGrantAllocationRequest in projectGrantAllocationRequests)
            {
                var grantAllocation = projectGrantAllocationRequest.GrantAllocation;
                foreach (var agreementGrantAllocation in grantAllocation.AgreementGrantAllocations)
                {
                    projectAgreementsByGrantAllocationsToReturn.Add(new ProjectAgreementByGrantAllocation(grantAllocation, agreementGrantAllocation.Agreement, projectGrantAllocationRequest.Project));
                }
            }

            return projectAgreementsByGrantAllocationsToReturn;
        }


    }
}