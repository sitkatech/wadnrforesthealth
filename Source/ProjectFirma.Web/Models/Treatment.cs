namespace ProjectFirma.Web.Models
{
    public partial class Treatment : IAuditableEntity
    {
        public string AuditDescriptionString =>  $"TreatmentID:{this.TreatmentID} -  ProjectLocationID:{this.ProjectLocationID}  - TreatmentTypeID:{this.TreatmentTypeID} - :TreatmentStartDate: {this.TreatmentStartDate} - TreatmentEndDate: {this.TreatmentEndDate}";
    }
}