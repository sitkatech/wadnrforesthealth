using System;
using ProjectFirma.Web.Common;

namespace ProjectFirma.Web.Models
{
    public static class AuthenticatorHelper
    {
        public static DeploymentEnvironment GetDeploymentEnvironment()
        {
            switch (FirmaWebConfiguration.FirmaEnvironment.FirmaEnvironmentType)
            {
                case FirmaEnvironmentType.Local:
                    return DeploymentEnvironment.Local;
                case FirmaEnvironmentType.Qa:
                    return DeploymentEnvironment.QA;
                case FirmaEnvironmentType.Prod:
                    return DeploymentEnvironment.Prod;
                default:
                    throw new Exception(
                        $"Unhandled case: {FirmaWebConfiguration.FirmaEnvironment.FirmaEnvironmentType}");
            }
        }

        public static Authenticator GetAuthenticator(string uniqueIdentifier)
        {
            // There is likely a far better way to detect this, but this will work for now.
            if (uniqueIdentifier.Contains("@dnr.wa.lcl") || uniqueIdentifier.Contains("@dnr.wa.gov"))
            {
                return Authenticator.ADFS;
            }

            // Assume SAW if not ADFS
            return Authenticator.SAW;
        }
    }
}