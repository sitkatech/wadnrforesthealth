using System;

namespace ProjectFirma.Web.Models
{
    public partial class Treatment : IAuditableEntity, ITreatment
    {
        public Treatment(int projectID, DateTime? treatmentStartDate, DateTime? treatmentEndDate, decimal treatmentFootprintAcres, string treatmentNotes, int treatmentTypeID, decimal? treatmentTreatedAcres, string treatmentTypeImportedText, int? createGisUploadAttemptID, int? updateGisUploadAttemptID, int treatmentDetailedActivityTypeID, string treatmentDetailedActivityTypeImportedText, int? programID, bool? importedFromGis, ProjectLocation projectLocation, int? treatmentCodeID, decimal? costPerAcre) : this()
        {
            ProjectID = projectID;
            TreatmentStartDate = treatmentStartDate;
            TreatmentEndDate = treatmentEndDate;
            TreatmentFootprintAcres = treatmentFootprintAcres;
            TreatmentNotes = treatmentNotes;
            TreatmentTypeID = treatmentTypeID;
            TreatmentTreatedAcres = treatmentTreatedAcres;
            TreatmentTypeImportedText = treatmentTypeImportedText;
            CreateGisUploadAttemptID = createGisUploadAttemptID;
            UpdateGisUploadAttemptID = updateGisUploadAttemptID;
            TreatmentDetailedActivityTypeID = treatmentDetailedActivityTypeID;
            TreatmentDetailedActivityTypeImportedText = treatmentDetailedActivityTypeImportedText;
            ProgramID = programID;
            ImportedFromGis = importedFromGis;
            ProjectLocation = projectLocation;
            TreatmentCodeID = treatmentCodeID;
            CostPerAcre = costPerAcre;
        }

        public string AuditDescriptionString =>  $"TreatmentID:{TreatmentID} -  ProjectLocationID:{ProjectLocationID}  - TreatmentTypeID:{TreatmentTypeID} - :TreatmentStartDate: {this.TreatmentStartDate} - TreatmentEndDate: {TreatmentEndDate}";
    }
}