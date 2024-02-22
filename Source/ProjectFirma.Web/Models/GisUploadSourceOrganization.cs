using System.Collections.Generic;

namespace ProjectFirma.Web.Models
{
    public partial class GisUploadSourceOrganization : IAuditableEntity
    {
        public string AuditDescriptionString => GisUploadSourceOrganizationName;


        public bool RequiresCompletionDate()
        {
            return ProjectStageDefault == ProjectStage.Completed && !DataDeriveProjectStage;
        }
    }
}