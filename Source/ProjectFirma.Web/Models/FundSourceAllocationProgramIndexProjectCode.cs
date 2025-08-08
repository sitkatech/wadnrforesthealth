using LtInfo.Common.Views;
using ProjectFirma.Web.Common;

namespace ProjectFirma.Web.Models
{
    public partial class FundSourceAllocationProgramIndexProjectCode : IAuditableEntity
    {

        public FundSourceAllocationProgramIndexProjectCode(int fundSourceAllocationID, int programIndexID, int? projectCodeID) : this()
        {
            this.FundSourceAllocationID = fundSourceAllocationID;
            this.ProgramIndexID = programIndexID;
            this.ProjectCodeID = projectCodeID;
        }

        public string ProgramIndexProjectCodeDisplayString
        {
            get
            {
                // Program Index string
                var programIndex = HttpRequestStorage.DatabaseEntities.ProgramIndices.Find(ProgramIndexID);
                string programIndexName = programIndex != null ? programIndex.AuditDescriptionString : ViewUtilities.NotFoundString;

                // Project Code string
                var projectCode = HttpRequestStorage.DatabaseEntities.ProjectCodes.Find(ProjectCodeID);
                string projectCodeName = projectCode != null ? projectCode.AuditDescriptionString : ViewUtilities.NotFoundString;

                // If someone wants a different format here, have at it!
                return $"{programIndexName}-{projectCodeName}";
            }
        }

        public string FundSourceAllocationProgramIndexProjectCodeDisplayString
        {
            get
            {
                // Program Index string
                var programIndex = HttpRequestStorage.DatabaseEntities.ProgramIndices.Find(ProgramIndexID);
                string programIndexName = programIndex != null ? programIndex.AuditDescriptionString : ViewUtilities.NotFoundString;

                // Project Code string
                var projectCode = HttpRequestStorage.DatabaseEntities.ProjectCodes.Find(ProjectCodeID);
                string projectCodeName = projectCode != null ? projectCode.AuditDescriptionString : ViewUtilities.NotFoundString;

                // If someone wants a different format here, have at it!
                return $"GA{FundSourceAllocationID}~PI{programIndexName}~PC{projectCodeName}";
            }
        }


        public string AuditDescriptionString
        {
            get
            {
                var fundSourceAllocation = HttpRequestStorage.DatabaseEntities.FundSourceAllocations.Find(FundSourceAllocationID);
                string fundSourceAllocationName = fundSourceAllocation != null ? fundSourceAllocation.AuditDescriptionString : ViewUtilities.NotFoundString;

                var programIndex = HttpRequestStorage.DatabaseEntities.ProgramIndices.Find(ProgramIndexID);
                string programIndexDisplayString = programIndex != null ? programIndex.ProgramIndexCode : ViewUtilities.NotFoundString;

                var projectCode = HttpRequestStorage.DatabaseEntities.ProjectCodes.Find(ProjectCodeID);
                string projectCodeDisplayString = projectCode != null ? projectCode.ProjectCodeName : ViewUtilities.NotFoundString;

                return $"FundSourceAllocation: {fundSourceAllocationName} ProgramIndex: {programIndexDisplayString} ProjectCode: {projectCodeDisplayString}";
            }
        }
    }
}