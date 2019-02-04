namespace ProjectFirma.Web.Models
{
    public partial class AgreementStatus : IAuditableEntity
    {
        public string AuditDescriptionString => AgreementStatusName;
    }
}