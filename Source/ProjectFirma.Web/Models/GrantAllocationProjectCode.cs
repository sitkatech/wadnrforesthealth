using LtInfo.Common.Views;
using ProjectFirma.Web.Common;

namespace ProjectFirma.Web.Models
{
    public partial class GrantAllocationProgramIndexProjectCode : IAuditableEntity
    {
        public string AuditDescriptionString
        {
            get
            {
                var grantAllocation = HttpRequestStorage.DatabaseEntities.GrantAllocations.Find(GrantAllocationID);
                string grantAllocationName = grantAllocation != null ? grantAllocation.AuditDescriptionString : ViewUtilities.NotFoundString;

                var programIndexProjectCode = HttpRequestStorage.DatabaseEntities.ProgramIndexProjectCodes.Find(ProgramIndexProjectCodeID);
                string programIndexProjectCodeDisplayString = programIndexProjectCode != null ? programIndexProjectCode.ProgramIndexProjectCodeDisplayString : ViewUtilities.NotFoundString;

                return $"GrantAllocation: {grantAllocationName} ProgramIndexProjectCode: {programIndexProjectCodeDisplayString}";
            }
        }
    }
}