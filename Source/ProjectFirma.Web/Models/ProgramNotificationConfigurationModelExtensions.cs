using LtInfo.Common;
using ProjectFirma.Web.Common;
using ProjectFirma.Web.Controllers;

namespace ProjectFirma.Web.Models
{
    public static class ProgramNotificationConfigurationModelExtensions
    {
        
        public static readonly UrlTemplate<int> EditUrlTemplate = new UrlTemplate<int>(SitkaRoute<ProgramController>.BuildUrlFromExpression(t => t.EditProgramNotificationConfiguration(UrlTemplate.Parameter1Int)));
        

        public static string GetEditUrl(this ProgramNotificationConfiguration programNotificationConfiguration)
        {
            return EditUrlTemplate.ParameterReplace(programNotificationConfiguration.ProgramNotificationConfigurationID);
        }


    }
}