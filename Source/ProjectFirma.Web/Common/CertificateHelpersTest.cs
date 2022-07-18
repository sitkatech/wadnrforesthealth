using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using LtInfo.Common;
using NUnit.Framework;

namespace ProjectFirma.Web.Common
{
    [TestFixture]
    public class CertificateHelpersTest
    {
        [Test]
        public void CertFromUrlWorksWithSecureUrl()
        {
            var cert = CertificateHelpers.GetX509Certificate2FromUri(new Uri("https://www.example.org"));
            Assert.That(cert, Is.Not.Null, "should get a cert");
            Assert.That(cert.Subject, Is.StringContaining("example.org"), "Should get cert particular to the url");
        }

        [Test]
        public void CertFromUrlWorksWithSecureUrlForWashingtonSaw()
        {
            var cert = CertificateHelpers.GetX509Certificate2FromUri(FirmaWebConfiguration.SAWEndPoint);
            Assert.That(cert, Is.Not.Null, "should get a cert");
            Assert.That(cert.Subject, Is.StringContaining(FirmaWebConfiguration.SAWEndPoint.Host), "Should get cert particular to the url");
        }

        [Test]
        public void CertFromUrlGivenBadUrlHasExceptions()
        {
            Assert.Catch(() => CertificateHelpers.GetX509Certificate2FromUri(null), "Should have exception with null url");
            Assert.Catch(() => CertificateHelpers.GetX509Certificate2FromUri(new Uri("/relative")), "Should have exception with relative url");
            Assert.Catch(() => CertificateHelpers.GetX509Certificate2FromUri(new Uri("http://www.example.org")), "Should have exception with non secure url");
        }

        [Test]
        public void TestExtractDnsNamesFromSubjectAlternativeName()
        {
            // Arrange
            // -------

            // Sample certificate file from WA SAW website
            // ReSharper disable StringLiteralTypo
            var certificateFileInfo = FileUtility.FirstMatchingFileUpDirectoryTree(@"Common\wadnr-saw-saml-response-test.cer");
            // ReSharper restore StringLiteralTypo
            var certificate2 = new X509Certificate2(certificateFileInfo.FullName);

            // Act
            // ---
            var dnsNameResponse = CertificateHelpers.ExtractDnsNamesFromSubjectAlternativeName(certificate2);

            // Assert
            // -------
            // ReSharper disable StringLiteralTypo
            Assert.That(dnsNameResponse, Is.EquivalentTo(new List<string>() { @"secureaccess.wa.gov", "support.secureaccess.wa.gov", "help.secureaccess.wa.gov", "aa-secureaccess.wa.gov" }), "Should be able to properly parse out the DNS Name entries from Subject Alternative Name certificate field.");
            // ReSharper restore StringLiteralTypo
        }

    }
}