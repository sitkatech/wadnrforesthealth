namespace ProjectFirma.Web.Models
{
    public partial class ProgramPerson : IAuditableEntity
    {
        public string AuditDescriptionString => $"ProgramPerson with Program ID: {ProgramID} and Person ID: {PersonID}";

        
    }
}