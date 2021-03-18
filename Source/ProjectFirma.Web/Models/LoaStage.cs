using ProjectFirma.Web.Models.ExcelUpload;

namespace ProjectFirma.Web.Models
{
    public partial class LoaStage : IAuditableEntity
    {
        public string AuditDescriptionString
        {
            get
            {
                return LoaStageID.ToString();
            }
        }

        public LoaStage(LoaStageImport loaStageImport, bool isNortheast)
        {
            this.ProjectIdentifier = loaStageImport.ProjectID;
            this.ProjectStatus = loaStageImport.Status;
            this.MatchAmount = (decimal?) loaStageImport.MatchAmount;
            this.PayAmount = (decimal?)loaStageImport.PayAmount;
            this.GrantNumber = loaStageImport.GrantNumber;
            this.FocusAreaName = loaStageImport.FocusArea;
            this.ProjectExpirationDate = loaStageImport.ProjectExpirationDate;
            this.LetterDate = loaStageImport.LetterDate;
            this.ProgramIndex = loaStageImport.ProgramIndex;
            this.ProjectCode = loaStageImport.ProjectCode;
            this.IsNortheast = isNortheast;
        }
    }
}