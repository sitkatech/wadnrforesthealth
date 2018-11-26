namespace ProjectFirma.Web.Models
{
    public partial class StaffTimeActivity : IAuditableEntity
    {
        public string AuditDescriptionString
        {
            get { return $"Staff Time Activity for Project: {ProjectID}"; }
        }

        public decimal TotalAmount => StaffTimeActivityHours * StaffTimeActivityRate;
    }
}
