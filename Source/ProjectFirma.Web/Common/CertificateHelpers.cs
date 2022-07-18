using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Text.RegularExpressions;
using log4net;
using LtInfo.Common.DesignByContract;

namespace ProjectFirma.Web.Common
{
    public static class CertificateHelpers
    {
        static readonly ILog Logger = LogManager.GetLogger(typeof(CertificateHelpers));

        public static X509Certificate2 GetX509Certificate2FromStore(string certificateSerialNumber)
        {
            using (var store = new X509Store(StoreName.My, StoreLocation.LocalMachine))
            {
                store.Open(OpenFlags.ReadOnly);
                var certificateCollection = store.Certificates.Find(X509FindType.FindByThumbprint, certificateSerialNumber, false);

                if (certificateCollection.Count == 1)
                {
                    return certificateCollection[0];
                }
                throw new Exception($"Trouble finding certificate {certificateSerialNumber} Local Machine Root Store, found {certificateCollection.Count} certificates expected to find exactly 1!");
            }
        }

        public static X509Certificate2 GetX509Certificate2FromUri(Uri uriOfWebsiteFromWhichToGetCertificate)
        {
            Check.RequireNotNull(uriOfWebsiteFromWhichToGetCertificate, "Need a proper uri but was null");
            Check.Require(uriOfWebsiteFromWhichToGetCertificate.IsAbsoluteUri, $"Must be an absolute uri to get a Certificate but was {uriOfWebsiteFromWhichToGetCertificate.OriginalString}");
            Check.Require(string.Equals(uriOfWebsiteFromWhichToGetCertificate.Scheme, Uri.UriSchemeHttps, StringComparison.InvariantCultureIgnoreCase), $"Must be a secure url to have a Certificate (expected scheme {Uri.UriSchemeHttps} got scheme {uriOfWebsiteFromWhichToGetCertificate.Scheme}) for url {uriOfWebsiteFromWhichToGetCertificate.OriginalString}");
            Logger.Info($"about to make web request to get x509 cert. url is: {uriOfWebsiteFromWhichToGetCertificate.OriginalString}");

            // Make actual web GET request to retrieve cert
            var request = (HttpWebRequest)WebRequest.Create(uriOfWebsiteFromWhichToGetCertificate);
            using (var response = (HttpWebResponse) request.GetResponse())
            {
                response.Close();
            }

            // Convert the X509Certificate to an X509Certificate2 object by passing it into the constructor
            return new X509Certificate2(request.ServicePoint.Certificate ?? throw new InvalidOperationException($"There was not Certificate available when accessing url {uriOfWebsiteFromWhichToGetCertificate.AbsoluteUri}"));
        }

        public static List<string> ExtractDnsNamesFromSubjectAlternativeName(X509Certificate2 signingCert)
        {
            const string oidForSubjectAlternativeName = "2.5.29.17";
            var subjectAlternativeNameExtension = signingCert.Extensions[oidForSubjectAlternativeName];
            if (subjectAlternativeNameExtension == null)
            {
                return new List<string>();
            }

            // Subject Alternative Name field will look like this:
            // DNS Name=secureaccess.wa.gov
            // DNS Name=support.secureaccess.wa.gov
            // DNS Name=help.secureaccess.wa.gov
            // DNS Name=aa-secureaccess.wa.gov
            var subjectAlternativeNameAsString = subjectAlternativeNameExtension.Format(true);
            var regexForDnsName = new Regex(@"DNS Name=([a-zA-Z0-9\.\-]+)");
            var matches = regexForDnsName.Matches(subjectAlternativeNameAsString);
            var dnsNameList = matches.Cast<Match>().Select(m => m.Groups[1].Value).ToList();
            return dnsNameList;
        }
    }
}