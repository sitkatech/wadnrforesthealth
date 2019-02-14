using LtInfo.Common;
using ProjectFirma.Web.Common;
using ProjectFirma.Web.Controllers;

namespace ProjectFirma.Web.Models
{
    public static class GrantAllocationNoteModelExtensions
    {
        public static readonly UrlTemplate<int, int> EditNoteUrlTemplate = new UrlTemplate<int, int>(SitkaRoute<GrantAllocationController>.BuildUrlFromExpression(t => t.EditGrantAllocationNote(UrlTemplate.Parameter1Int, UrlTemplate.Parameter2Int)));
        public static string GetEditNoteUrl(this GrantAllocationNote grantAllocationNote, GrantAllocation grantAllocation)
        {
            return EditNoteUrlTemplate.ParameterReplace(grantAllocation.GrantID, grantAllocationNote.GrantAllocationNoteID);
        }

        public static readonly UrlTemplate<int, int> DeleteNoteUrlTemplate = new UrlTemplate<int, int>(SitkaRoute<GrantAllocationController>.BuildUrlFromExpression(t => t.DeleteGrantAllocationNote(UrlTemplate.Parameter1Int, UrlTemplate.Parameter2Int)));
        public static string GetDeleteNoteUrl(this GrantAllocationNote grantAllocationNote, GrantAllocation grantAllocation)
        {
            return DeleteNoteUrlTemplate.ParameterReplace(grantAllocation.GrantID, grantAllocationNote.GrantAllocationNoteID);
        }
    }
}