namespace ProjectFirma.Web.Models
{
    public partial class TreatmentCode : IAuditableEntity
    {
        public string AuditDescriptionString => TreatmentCodeName;
    }
}