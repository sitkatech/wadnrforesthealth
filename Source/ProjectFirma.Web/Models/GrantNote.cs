using LtInfo.Common.Views;
using ProjectFirma.Web.Common;

namespace ProjectFirma.Web.Models
{
    public partial class GrantNote : IAuditableEntity
    {
        public string AuditDescriptionString
        {
            get
            {
                var grant = HttpRequestStorage.DatabaseEntities.Grants.Find(GrantID);
                var grantName = grant != null ? grant.AuditDescriptionString : ViewUtilities.NotFoundString;
                return $"Grant: {grantName}";
            }
        }
    }

    public partial class GrantStatus : IAuditableEntity
    {
        public string AuditDescriptionString
        {
            get { return GrantStatusName; }
        }
    }
}