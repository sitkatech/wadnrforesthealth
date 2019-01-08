using System.Collections.Generic;
using System.Linq;
using System.Web;
using LtInfo.Common;
using ProjectFirma.Web.Common;
using ProjectFirma.Web.Controllers;

namespace ProjectFirma.Web.Models
{
    public partial class FocusArea : IAuditableEntity
    {
        public string DisplayName => FocusAreaName;

        public List<Project> GetAssociatedProjects(Person person)
        {
            return Projects.ToList();
        }

        public string AuditDescriptionString => FocusAreaName;

        public HtmlString GetDisplayNameAsUrl()
        {
            return UrlTemplate.MakeHrefString(GetDetailUrl(), DisplayName);
        }

        public static readonly UrlTemplate<int> DetailUrlTemplate = new UrlTemplate<int>(SitkaRoute<FocusAreaController>.BuildUrlFromExpression(t => t.Detail(UrlTemplate.Parameter1Int)));

        public string GetDetailUrl()
        {
            return DetailUrlTemplate.ParameterReplace(FocusAreaID);
        }
    }
}