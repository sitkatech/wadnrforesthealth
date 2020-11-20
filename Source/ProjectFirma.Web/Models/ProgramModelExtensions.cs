using LtInfo.Common;
using ProjectFirma.Web.Common;
using ProjectFirma.Web.Controllers;

namespace ProjectFirma.Web.Models
{
    public static class ProgramModelExtensions
    {
        public static readonly UrlTemplate<int> DeleteUrlTemplate = new UrlTemplate<int>(SitkaRoute<ProgramController>.BuildUrlFromExpression(t => t.DeleteProgram(UrlTemplate.Parameter1Int)));
        public static readonly UrlTemplate<int> EditUrlTemplate = new UrlTemplate<int>(SitkaRoute<ProgramController>.BuildUrlFromExpression(t => t.Edit(UrlTemplate.Parameter1Int)));

        public static string GetDeleteUrl(this Program program)
        {
            return DeleteUrlTemplate.ParameterReplace(program.ProgramID);
        }

        public static string GetEditUrl(this Program program)
        {
            return EditUrlTemplate.ParameterReplace(program.ProgramID);
        }

        public static readonly UrlTemplate<int> SummaryUrlTemplate = new UrlTemplate<int>(SitkaRoute<ProgramController>.BuildUrlFromExpression(t => t.Detail(UrlTemplate.Parameter1Int)));
        public static string GetDetailUrl(this Program program)
        {
            return program == null ? "" : SummaryUrlTemplate.ParameterReplace(program.ProgramID);
        }
    }
}