using LtInfo.Common;
using ProjectFirma.Web.Common;
using ProjectFirma.Web.Controllers;

namespace ProjectFirma.Web.Models
{
    public static class GrantNoteModelExtensions
    {
        public static readonly UrlTemplate<int, int> EditNoteUrlTemplate = new UrlTemplate<int, int>(SitkaRoute<GrantController>.BuildUrlFromExpression(t => t.EditGrantNote(UrlTemplate.Parameter1Int, UrlTemplate.Parameter2Int)));
        public static string GetEditNoteUrl(this GrantNote grantNote, Grant grant)
        {
            return EditNoteUrlTemplate.ParameterReplace(grant.GrantID, grantNote.GrantNoteID);
        }

        public static readonly UrlTemplate<int, int> DeleteNoteUrlTemplate = new UrlTemplate<int, int>(SitkaRoute<GrantController>.BuildUrlFromExpression(t => t.DeleteGrantNote(UrlTemplate.Parameter1Int, UrlTemplate.Parameter2Int)));
        public static string GetDeleteNoteUrl(this GrantNote grantNote, Grant grant)
        {
            return DeleteNoteUrlTemplate.ParameterReplace(grant.GrantID, grantNote.GrantNoteID);
        }
    }
}