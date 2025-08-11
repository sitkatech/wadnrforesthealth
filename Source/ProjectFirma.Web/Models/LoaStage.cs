using System;
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
            this.FundSourceNumber = loaStageImport.FundSourceNumber;
            this.FocusAreaName = loaStageImport.FocusArea;
            this.ProjectExpirationDate = loaStageImport.ProjectExpirationDate;
            this.LetterDate = loaStageImport.LetterDate;
            this.ProgramIndex = loaStageImport.ProgramIndex;
            this.ProjectCode = loaStageImport.ProjectCode;
            this.IsNortheast = isNortheast;
            this.ApplicationDate = loaStageImport.ApplicationDate;
            this.DecisionDate = loaStageImport.DecisionDate;

            var foresterSplit = string.IsNullOrEmpty(loaStageImport.Forester) ? Array.Empty<string>() : loaStageImport.Forester.Split(' ');
            if (foresterSplit.Length > 1)
            {
                this.ForesterFirstName = foresterSplit[0];
                this.ForesterLastName = foresterSplit[1];
                for (int index = 2; index < foresterSplit.Length; index++)
                {
                    this.ForesterLastName += foresterSplit[index];
                }

                this.ForesterEmail = loaStageImport.ForesterEmail;
                this.ForesterPhone = loaStageImport.ForesterPhone;

            }

        }
    }
}