namespace ProjectFirma.Web.Models
{
    public partial class Treatment : IAuditableEntity
    {
        public string AuditDescriptionString =>  $"TreatmentID:{this.TreatmentID} -  TreatmentAreaID:{this.TreatmentArea.TreatmentAreaID}  - TreatmentType:{this.TreatmentType.TreatmentTypeDisplayName} - {this.TreatmentStartDate} - {this.TreatmentEndDate}";
    }
}