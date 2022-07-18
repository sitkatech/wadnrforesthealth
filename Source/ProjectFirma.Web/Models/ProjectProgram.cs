namespace ProjectFirma.Web.Models
{
    public partial class ProjectProgram : IAuditableEntity, IEntityProgram
    {
        public string AuditDescriptionString => $"ProjectProgram: {ProjectProgramID}";
    }
}