using System.Collections.Generic;

namespace ProjectFirma.Web.Models
{
    public static class AuthenticatorHelper
    {
        public static List<Authenticator> GetAuthenticators(string uniqueIdentifier)
        {
            // There is likely a far better way to detect this, but this will work for now.
            if (uniqueIdentifier.Contains("@dnr.wa.lcl") || uniqueIdentifier.Contains("@dnr.wa.gov"))
            {
                return new List<Authenticator> {Authenticator.ADFS};
            }

            return new List<Authenticator> {Authenticator.SAWTEST, Authenticator.SAWPROD };
        }
    }
}