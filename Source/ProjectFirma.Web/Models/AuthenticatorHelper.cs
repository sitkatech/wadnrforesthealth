namespace ProjectFirma.Web.Models
{
    public static class AuthenticatorHelper
    {
        public static Authenticator GetAuthenticator(string uniqueIdentifier)
        {
            // There is likely a far better way to detect this, but this will work for now.
            if (uniqueIdentifier.Contains("@dnr.wa.lcl") || uniqueIdentifier.Contains("@dnr.wa.gov"))
            {
                return Authenticator.ADFS;
            }

            // INCOMPLETE!!
            // Assume SAW if not ADFS
            return Authenticator.SAWTEST;
        }
    }
}