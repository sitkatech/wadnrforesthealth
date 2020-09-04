using System.Linq;
using LtInfo.Common.Views;
using ProjectFirma.Web.Common;

namespace ProjectFirma.Web.Models
{
    public partial class ProjectFundingSource : IAuditableEntity
    {
        public string AuditDescriptionString
        {
            get
            {
                var project = HttpRequestStorage.DatabaseEntities.Projects.Find(ProjectID);
                var projectName = project != null ? project.AuditDescriptionString : ViewUtilities.NotFoundString;
                var fundingSource = FundingSource.All.Single(fs => fs.FundingSourceID == FundingSourceID);
                var fundingSourceName = fundingSource != null ? fundingSource.FundingSourceDisplayName : ViewUtilities.NotFoundString;
                return $"Project: {projectName}, Funding Source: {fundingSourceName}";
            }
        }

    }
}