using System.Linq;

namespace ProjectFirma.Web.Models
{
    public static class ProjectUpdateBatchModelExtensions
    {
        public static Organization GetLeadImplementer(this ProjectUpdateBatch projectUpdate)
        {
            return projectUpdate.ProjectOrganizationUpdates.Where(x => x.RelationshipTypeID == RelationshipType.LeadImplementerID).Select(x => x.Organization).SingleOrDefault();
        }
    }
}