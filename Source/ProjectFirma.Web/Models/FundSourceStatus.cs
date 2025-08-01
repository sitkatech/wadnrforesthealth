namespace ProjectFirma.Web.Models
{
    public partial class FundSourceStatus : IAuditableEntity
    {
        public string AuditDescriptionString
        {
            get { return GrantStatusName; }
        }
    }
}