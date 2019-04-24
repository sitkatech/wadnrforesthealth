namespace ProjectFirma.Web.Models
{
    public partial class GrantModificationGrantModificationPurpose : IAuditableEntity
    {

        public string AuditDescriptionString => $"GrantModificationGrantModificationPurpose with Grant Modification ID: {GrantModificationID} and Grant Modification Purpose ID: {GrantModificationPurposeID}";

    }
}