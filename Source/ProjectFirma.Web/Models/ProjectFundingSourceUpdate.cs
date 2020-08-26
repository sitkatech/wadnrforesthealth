using System.Collections.Generic;
using System.Linq;
using LtInfo.Common;

namespace ProjectFirma.Web.Models
{
    public partial class ProjectFundingSourceUpdate : IAuditableEntity
    {

        public static void CreateFromProject(ProjectUpdateBatch projectUpdateBatch)
        {
            var project = projectUpdateBatch.Project;
            projectUpdateBatch.ProjectFundingSourceUpdates = project.ProjectFundingSources.Select(
                pfs =>
                    new ProjectFundingSourceUpdate(projectUpdateBatch, pfs.FundingSource)
            ).ToList();
        }

        public static void CommitChangesToProject(ProjectUpdateBatch projectUpdateBatch, IList<ProjectFundingSource> allProjectFundingSources)
        {
            var project = projectUpdateBatch.Project;
            var projectFundingSourceFromProjectUpdate = projectUpdateBatch
                .ProjectFundingSourceUpdates
                .Select(x => new ProjectFundingSource(project.ProjectID, x.FundingSource.FundingSourceID)).ToList();
            project.ProjectFundingSources.Merge(projectFundingSourceFromProjectUpdate,
                allProjectFundingSources,
                (x, y) => x.ProjectID == y.ProjectID && x.FundingSourceID == y.FundingSourceID);
        }

        public string AuditDescriptionString
        {
            get
            {
                string fundingSourceID = this.FundingSource != null ? this.FundingSource.FundingSourceID.ToString(): "none";
                string fundingSourceName = this.FundingSource != null ? this.FundingSource.FundingSourceDisplayName : "none";
                return $"FundingSourceID: {fundingSourceID}  FundingSourceDisplayName: {fundingSourceName} ProjectUpdateBatchID: {this.ProjectUpdateBatchID}";
            }
        }
    }
}