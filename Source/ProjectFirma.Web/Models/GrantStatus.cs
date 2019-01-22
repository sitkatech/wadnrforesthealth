namespace ProjectFirma.Web.Models
{
    public partial class GrantStatus : IAuditableEntity
    {
        public string AuditDescriptionString
        {
            get { return GrantStatusName; }
        }
    }
}