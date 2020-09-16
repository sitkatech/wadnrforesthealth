namespace ProjectFirma.Web.Models
{
    public partial class Treatment : IAuditableEntity
    {
        public string AuditDescriptionString =>  $"TreatmentID:{this.TreatmentID} -  TreatmentAreaID:{this.TreatmentAreaID}  - TreatmentTypeID:{this.TreatmentTypeID} - :TreatmentStartDate: {this.TreatmentStartDate} - TreatmentEndDate: {this.TreatmentEndDate}";
    }
}