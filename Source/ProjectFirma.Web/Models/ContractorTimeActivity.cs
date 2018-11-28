namespace ProjectFirma.Web.Models
{
    public partial class ContractorTimeActivity : IAuditableEntity
    {
        public string AuditDescriptionString
        {
            get { return $"Staff Time Activity for Project: {ProjectID}"; }
        }

        public decimal TotalAmount => ContractorTimeActivityHours * ContractorTimeActivityRate;
    }
}
