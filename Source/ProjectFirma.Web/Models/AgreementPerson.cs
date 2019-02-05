namespace ProjectFirma.Web.Models
{
    public partial class AgreementPerson : IAuditableEntity
    {
        public string AuditDescriptionString => $"{Person?.FullNameFirstLastAndOrg ?? "(Person Not Found)"} in role {AgreementPersonRole.AgreementPersonRoleDisplayName} on {Agreement?.AgreementTitle ?? "(Agreement Not Found)"}";
    }
}