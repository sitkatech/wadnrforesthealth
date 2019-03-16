namespace ProjectFirma.Web.Models
{
    public partial class CostType : IAuditableEntity
    {
        public string AuditDescriptionString => $"{CostTypeDescription}";
    }
}