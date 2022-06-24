namespace ProjectFirma.Web.Models
{
    public partial class ProjectUpdateProgram : IAuditableEntity
    {
        public string AuditDescriptionString => $"ProjectUpdateProgram: {ProjectUpdateProgramID}";
    }
}