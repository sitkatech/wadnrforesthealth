using System.Web;
using LtInfo.Common;
using ProjectFirma.Web.Common;
using ProjectFirma.Web.Controllers;

namespace ProjectFirma.Web.Models
{
    public static class GrantModificationModelExtensions
    {

        public static readonly UrlTemplate<int> DeleteUrlTemplate = new UrlTemplate<int>(SitkaRoute<GrantModificationController>.BuildUrlFromExpression(t => t.DeleteGrantModification(UrlTemplate.Parameter1Int)));
        public static string GetDeleteUrl(this GrantModification grantModification)
        {
            return DeleteUrlTemplate.ParameterReplace(grantModification.GrantModificationID);
        }


        public static readonly UrlTemplate<int> DetailUrlTemplate = new UrlTemplate<int>(SitkaRoute<GrantModificationController>.BuildUrlFromExpression(t => t.GrantModificationDetail(UrlTemplate.Parameter1Int)));
        public static string GetDetailUrl(this GrantModification grantModification)
        {
            return DetailUrlTemplate.ParameterReplace(grantModification.GrantModificationID);
        }

        public static readonly UrlTemplate<int> EditUrlTemplate = new UrlTemplate<int>(SitkaRoute<GrantModificationController>.BuildUrlFromExpression(t => t.EditGrantModification(UrlTemplate.Parameter1Int)));
        public static string GetEditUrl(this GrantModification grantModification)
        {
            return EditUrlTemplate.ParameterReplace(grantModification.GrantModificationID);
        }

        public static readonly UrlTemplate<int> DuplicateUrlTemplate = new UrlTemplate<int>(SitkaRoute<GrantModificationController>.BuildUrlFromExpression(t => t.Duplicate(UrlTemplate.Parameter1Int)));
        public static string GetDuplicateUrl(this GrantModification grantModification)
        {
            return DuplicateUrlTemplate.ParameterReplace(grantModification.GrantModificationID);
        }

        public static HtmlString GetGrantModificationNameAsUrl(this GrantModification grantModification)
        {
            return grantModification != null ? UrlTemplate.MakeHrefString(grantModification.GetDetailUrl(), grantModification.GrantModificationName) : new HtmlString(null);
        }

    }
}
