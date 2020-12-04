using System;
using NUnit.Framework;
using ProjectFirma.Web.Common;

namespace ProjectFirma.Web
{
    [TestFixture]
    public class SawSamlResponseTest
    {
        [Test]
        public void TestSawSamlResponseDateValidity()
        {
            var samlXml = @"
<samlp:Response xmlns:saml=""urn:oasis:names:tc:SAML:2.0:assertion"" xmlns:samlp=""urn:oasis:names:tc:SAML:2.0:protocol"" xmlns:xs=""http://www.w3.org/2001/XMLSchema"" xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" Destination=""https://wadnrforesthealth.qa.projectfirma.com/Account/SAWPost"" ID=""FIMRSP_d38b3f33-0175-192a-8f59-a9b3208be3fe"" IssueInstant=""2020-11-17T00:11:59Z"" Version=""2.0"">
  <saml:Issuer Format=""urn:oasis:names:tc:SAML:2.0:nameid-format:entity"">https://test-secureaccess.wa.gov/FIM2/sps/sawidp/saml20</saml:Issuer>
  <samlp:Status>
    <samlp:StatusCode Value=""urn:oasis:names:tc:SAML:2.0:status:Success"">
    </samlp:StatusCode>
  </samlp:Status>
  <saml:Assertion ID=""Assertion-uuidd38b3f13-0175-16c6-b3b5-a9b3208be3fe"" IssueInstant=""2020-11-17T00:11:59Z"" Version=""2.0"">
    <saml:Issuer Format=""urn:oasis:names:tc:SAML:2.0:nameid-format:entity"">https://test-secureaccess.wa.gov/FIM2/sps/sawidp/saml20</saml:Issuer>
    <ds:Signature xmlns:ds=""http://www.w3.org/2000/09/xmldsig#"" Id=""uuidd38b3f18-0175-1072-a058-a9b3208be3fe"">
      <ds:SignedInfo>
        <ds:CanonicalizationMethod Algorithm=""http://www.w3.org/2001/10/xml-exc-c14n#"">
        </ds:CanonicalizationMethod>
        <ds:SignatureMethod Algorithm=""http://www.w3.org/2001/04/xmldsig-more#rsa-sha256"">
        </ds:SignatureMethod>
        <ds:Reference URI=""#Assertion-uuidd38b3f13-0175-16c6-b3b5-a9b3208be3fe"">
          <ds:Transforms>
            <ds:Transform Algorithm=""http://www.w3.org/2000/09/xmldsig#enveloped-signature"">
            </ds:Transform>
            <ds:Transform Algorithm=""http://www.w3.org/2001/10/xml-exc-c14n#"">
              <xc14n:InclusiveNamespaces xmlns:xc14n=""http://www.w3.org/2001/10/xml-exc-c14n#"" PrefixList=""xs saml xsi"">
              </xc14n:InclusiveNamespaces>
            </ds:Transform>
          </ds:Transforms>
          <ds:DigestMethod Algorithm=""http://www.w3.org/2001/04/xmlenc#sha256"">
          </ds:DigestMethod>
          <ds:DigestValue>4JDoIy8WFOLjFGGNcJB/fUTA9rLVU8RtCMx9VJZzXU0=</ds:DigestValue>
        </ds:Reference>
      </ds:SignedInfo>
      <ds:SignatureValue>RUkq+y/ccsfJVrlvTZuoSirJktnSUhiK7NYeJ/oO8kScR7E8ZcVViSlvjbSYZOTc5dlDbCqdNlDpEu/s/FGjBcGHSLnBrbdJid/8aJhdn2h05l/2vGrbhKnP3X3b7PHwMGC2HCARr0w9VxT/yoXwfoYi9caaJ2UhrEfZNQ4QBHAv3uYHumvEEmkMepW+RfRCGVmn84Hx0DyAN98Q8E2e9+r+UT2Tsv0jIpCocFXiVl4hvSCdLZE88hr8HSJWryvgh4DCdi4bsxWusXjPA1VeULwslH7EPWBWOLMt+vejE3Q7s3TiEwDF0eFoqYOQXsTiCIfu/ZjYzfY5RR20aBUehg==</ds:SignatureValue>
      <ds:KeyInfo>
        <ds:X509Data>
          <ds:X509Certificate>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</ds:X509Certificate>
        </ds:X509Data>
      </ds:KeyInfo>
    </ds:Signature>
    <saml:Subject>
      <saml:NameID Format=""urn:oasis:names:tc:SAML:1.1:nameid-format:emailAddress"">DP4VD4ZP8QQ6L-1PF0ZV2FF4-D1LW4VZ0FD-DW9QW8ZL9Q</saml:NameID>
      <saml:SubjectConfirmation Method=""urn:oasis:names:tc:SAML:2.0:cm:bearer"">
        <saml:SubjectConfirmationData NotOnOrAfter=""2020-11-17T00:12:59Z"" Recipient=""https://wadnrforesthealth.qa.projectfirma.com/Account/SAWPost"">
        </saml:SubjectConfirmationData>
      </saml:SubjectConfirmation>
    </saml:Subject>
    <saml:Conditions NotBefore=""2020-11-17T00:10:59Z"" NotOnOrAfter=""2020-11-17T00:12:59Z"">
      <saml:AudienceRestriction>
        <saml:Audience>https://wadnrforesthealth.qa.projectfirma.com</saml:Audience>
      </saml:AudienceRestriction>
    </saml:Conditions>
    <saml:AuthnStatement AuthnInstant=""2020-11-17T00:11:59Z"" SessionIndex=""uuidd35793e5-0175-1dc9-afb8-a9b3208be3fe"" SessionNotOnOrAfter=""2020-11-17T01:11:59Z"">
      <saml:AuthnContext>
        <saml:AuthnContextClassRef>urn:oasis:names:tc:SAML:2.0:ac:classes:Password</saml:AuthnContextClassRef>
      </saml:AuthnContext>
    </saml:AuthnStatement>
    <saml:AttributeStatement>
      <saml:Attribute Name=""level"" NameFormat=""urn:oasis:names:tc:SAML:2.0:assertion"">
        <saml:AttributeValue xsi:type=""xs:string"">1</saml:AttributeValue>
      </saml:Attribute>
      <saml:Attribute Name=""groups"" NameFormat=""urn:oasis:names:tc:SAML:2.0:assertion"">
        <saml:AttributeValue xsi:type=""xs:string"">f3_users</saml:AttributeValue>
        <saml:AttributeValue xsi:type=""xs:string"">DNR-ForestHealthTrackerQA-USER-REG</saml:AttributeValue>
        <saml:AttributeValue xsi:type=""xs:string"">DNR-ForestHealthTrackerQA-DEFAULT_UMG-USER-REG</saml:AttributeValue>
      </saml:Attribute>
      <saml:Attribute Name=""guid"" NameFormat=""urn:oasis:names:tc:SAML:2.0:assertion"">
        <saml:AttributeValue xsi:type=""xs:string"">DP4VD4ZP8QQ6L-1PF0ZV2FF4-D1LW4VZ0FD-DW9QW8ZL9Q</saml:AttributeValue>
      </saml:Attribute>
      <saml:Attribute Name=""user"" NameFormat=""urn:oasis:names:tc:SAML:2.0:assertion"">
        <saml:AttributeValue xsi:type=""xs:string"">michael@sitkatech.com</saml:AttributeValue>
      </saml:Attribute>
      <saml:Attribute Name=""name"" NameFormat=""urn:oasis:names:tc:SAML:2.0:assertion"">
        <saml:AttributeValue xsi:type=""xs:string"">Michael Ferrante</saml:AttributeValue>
      </saml:Attribute>
      <saml:Attribute Name=""email"" NameFormat=""urn:oasis:names:tc:SAML:2.0:assertion"">
        <saml:AttributeValue xsi:type=""xs:string"">michael@sitkatech.com</saml:AttributeValue>
      </saml:Attribute>
    </saml:AttributeStatement>
  </saml:Assertion>
</samlp:Response>
";
            using (var sawSamlResponse = new SawSamlResponse(CertificateHelpers.GetX509Certificate2FromUri(FirmaWebConfiguration.SAWEndPoint)))
            {
                sawSamlResponse.LoadXmlFromString(samlXml);
                Console.Write(DateTimeOffset.Now);
                //  NotOnOrAfter=""2020-11-17T00:12:59Z"" 
                var now = DateTimeOffset.Parse("11/16/2020 4:11:59 PM -08:00");
                var isResponseStillWithinValidTimePeriod = sawSamlResponse.IsResponseStillWithinValidTimePeriod(now);
                Assert.That(sawSamlResponse.IsResponseStillWithinValidTimePeriod(now), Is.True, "Should be valid date");
                Assert.That(sawSamlResponse.IsResponseStillWithinValidTimePeriod(now.AddMinutes(5)), Is.False, "Should be invalid date too old");
            }
        }
    }
}