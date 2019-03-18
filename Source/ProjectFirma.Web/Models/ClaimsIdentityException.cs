using LtInfo.Common;

namespace ProjectFirma.Web.Models
{
    public class ClaimsIdentityException : SitkaDisplayErrorException
    {
        public ClaimsIdentityException(string message) : base(message)
        {
        }
    }
}