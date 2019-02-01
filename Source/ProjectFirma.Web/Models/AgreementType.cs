namespace ProjectFirma.Web.Models
{
    public partial class AgreementType : IAuditableEntity
    {
        public string AuditDescriptionString => AgreementTypeName;
    }
}