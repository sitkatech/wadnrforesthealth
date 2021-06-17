namespace ProjectFirma.Web.Models
{
    public partial class ProjectProgram : IAuditableEntity
    {
        public string AuditDescriptionString => $"ProjectProgram: {ProjectProgramID}";
    }
}