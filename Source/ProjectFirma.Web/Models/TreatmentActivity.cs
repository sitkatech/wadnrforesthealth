namespace ProjectFirma.Web.Models
{
    public partial class TreatmentActivity : IAuditableEntity
    {
        public string AuditDescriptionString
        {
            get { return $"Treatment Activity for Project: {ProjectID}"; }
        }


    }
}