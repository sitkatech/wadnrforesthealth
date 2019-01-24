namespace ProjectFirma.Web.Models
{
    public partial class ProjectCode : IAuditableEntity
    {

        public string AuditDescriptionString => ProjectCodeAbbrev;

    }
}