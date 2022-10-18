using ProjectFirma.Web.Common;
using ProjectFirma.Web.Controllers;

namespace ProjectFirma.Web.Models
{
    public partial class ProjectDocumentType : IAuditableEntity
    {
        public string AuditDescriptionString => ProjectDocumentTypeName;
    }
}
