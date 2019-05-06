using LtInfo.Common.Views;
using ProjectFirma.Web.Common;

namespace ProjectFirma.Web.Models
{
    public partial class GrantAllocationProgramIndexProjectCode : IAuditableEntity
    {
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
                return $"PI{programIndexName}~PC{projectCodeName}";
            }
        }


        public string AuditDescriptionString
        {
            get
            {
                var grantAllocation = HttpRequestStorage.DatabaseEntities.GrantAllocations.Find(GrantAllocationID);
                string grantAllocationName = grantAllocation != null ? grantAllocation.AuditDescriptionString : ViewUtilities.NotFoundString;

                var programIndex = HttpRequestStorage.DatabaseEntities.ProgramIndices.Find(ProgramIndexID);
                string programIndexDisplayString = programIndex != null ? programIndex.ProgramIndexCode : ViewUtilities.NotFoundString;

                var projectCode = HttpRequestStorage.DatabaseEntities.ProjectCodes.Find(ProjectCodeID);
                string projectCodeDisplayString = projectCode != null ? projectCode.ProjectCodeName : ViewUtilities.NotFoundString;

                return $"GrantAllocation: {grantAllocationName} ProgramIndex: {programIndexDisplayString} ProjectCode: {projectCodeDisplayString}";
            }
        }
    }
}