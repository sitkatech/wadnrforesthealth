using System;
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
    }
}