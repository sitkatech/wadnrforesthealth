using System;
using System.Security.Cryptography.X509Certificates;

namespace ProjectFirma.Web.Common
{
    public static class CertificateHelpers
    {
        public static X509Certificate2 GetX509Certificate2FromStore(string certificateSerialNumber)
        {
            X509Certificate2 x509Certificate2;
            var store = new X509Store(StoreName.My, StoreLocation.LocalMachine);
            store.Open(OpenFlags.ReadOnly);
            var certificateCollection =
                store.Certificates.Find(X509FindType.FindBySerialNumber, certificateSerialNumber, true);
            if (certificateCollection.Count == 1)
            {
                x509Certificate2 = certificateCollection[0];
            }
            else
            {
                throw new Exception($"Certficate {certificateSerialNumber} not found in Local Machine Root Store!");
            }

            return x509Certificate2;
        }
    }
}