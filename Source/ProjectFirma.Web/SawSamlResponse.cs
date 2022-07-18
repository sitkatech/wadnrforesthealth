// ReSharper disable CommentTypo
/*	Jitbit's simple SAML 2.0 component for ASP.NET
	https://github.com/jitbit/AspNetSaml/
	(c) Jitbit LP, 2016
	Use this freely under the MIT license (see http://choosealicense.com/licenses/mit/)
	version 1.2
*/
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Security.Cryptography.Xml;
using System.Text;
using System.Xml;
using LtInfo.Common;
using ProjectFirma.Web.Common;
using ProjectFirma.Web.Models;

namespace ProjectFirma.Web
{
    public class SawSamlResponse
    {
        public static SawSamlResponse CreateFromBase64String(string base64EncodedXmlStringSawSamlResponse)
        {
            var utf8Encoding = new UTF8Encoding();
            var xmlStringSawSamlResponse = utf8Encoding.GetString(Convert.FromBase64String(base64EncodedXmlStringSawSamlResponse));
            return CreateFromString(xmlStringSawSamlResponse);
        }

        public static SawSamlResponse CreateFromString(string xmlStringSawSamlResponse)
        {
            return new SawSamlResponse(xmlStringSawSamlResponse);
        }

        private readonly string _originalDecodedResponse;
        private readonly XmlDocument _xmlDoc;
        private readonly XmlNamespaceManager _xmlNameSpaceManager; //we need this one to run our XPath queries on the SAML XML

        private SawSamlResponse(string xmlStringSawSamlResponse)
        {
            _xmlDoc = new XmlDocument { PreserveWhitespace = true, XmlResolver = null };
            _originalDecodedResponse = xmlStringSawSamlResponse;
            _xmlDoc.LoadXml(xmlStringSawSamlResponse);
            _xmlNameSpaceManager = GetNamespaceManager(_xmlDoc); //lets construct a "manager" for XPath queries
        }
        public bool IsValid(out string userDisplayableValidationErrorMessage)
        {
            var nodeList = _xmlDoc.SelectNodes("//ds:Signature", _xmlNameSpaceManager);
            if (nodeList == null || nodeList.Count == 0)
            {
                userDisplayableValidationErrorMessage = "Could not find signature node in SAW xml response.";
                return false;
            }

            var signedXml = new SignedXml(_xmlDoc); 
            signedXml.LoadXml((XmlElement)nodeList[0]);
            
            var hasValidSignatureReference = HasValidSignatureReference(signedXml);
            if (!hasValidSignatureReference)
            {
                userDisplayableValidationErrorMessage = "Could not validate SignatureReference in SAW xml response.";
                return false;
            }

            var checkSignature = CheckSignatureUsingEmbeddedSigningCertificate(out var checkSignatureMessage, signedXml);
            if (!checkSignature)
            {
                userDisplayableValidationErrorMessage = checkSignatureMessage;
                return false;
            }

            var isResponseStillWithinValidTimePeriod = IsResponseStillWithinValidTimePeriod();
            if (!isResponseStillWithinValidTimePeriod)
            {
                userDisplayableValidationErrorMessage = "Current time is past the expiration time for the SAW xml response.";
                return false;
            }

            userDisplayableValidationErrorMessage = string.Empty;
            return true;
        }

        private static bool CheckSignatureUsingEmbeddedSigningCertificate(out string userDisplayableValidationErrorMessage, SignedXml signedXml)
        {
            var keyInfoX509Data = signedXml.Signature.KeyInfo.OfType<KeyInfoX509Data>().FirstOrDefault();
            if (keyInfoX509Data == null)
            {
                userDisplayableValidationErrorMessage =
                    $"SAW xml signature is missing expected {nameof(KeyInfo)} {nameof(KeyInfoX509Data)} section with embedded signing certificate.";
                return false;
            }

            if (keyInfoX509Data.Certificates.Count < 1)
            {
                userDisplayableValidationErrorMessage =
                    "SAW xml signature does not have the expected embedded signing certificate within the xml itself, can't validate.";
                return false;
            }

            var signingCert = keyInfoX509Data.Certificates[0] as X509Certificate2;
            if (signingCert == null)
            {
                userDisplayableValidationErrorMessage = $"Could not retrieve expected embedded signing certificate of type {nameof(X509Certificate2)} in SAW xml signature, certificate may not be present or could be an unexpected type of certificate.";
                return false;
            }

            // Check *signature only* at first to allow for more detailed error messging
            if (!signedXml.CheckSignature(signingCert, true))
            {
                userDisplayableValidationErrorMessage = "SAW xml signature is invalid.";
                return false;
            }

            // Now check signature along with signing cert itself, so that we can give more error detail about which step is having problems (a bit redundant but more informative)
            if (!signedXml.CheckSignature(signingCert, false))
            {
                userDisplayableValidationErrorMessage = "SAW xml signature embedded signing certificate is invalid (date, certificate authority, etc).";
                return false;
            }

            var dnsNameList = CertificateHelpers.ExtractDnsNamesFromSubjectAlternativeName(signingCert);
            if (!dnsNameList.Any(x => string.Equals(x, FirmaWebConfiguration.SAWEndPoint.Host, StringComparison.CurrentCultureIgnoreCase)))
            {
                userDisplayableValidationErrorMessage =
                    $"SAW xml signature signed with unexpected embedded signing certificate, expected certificate to have \"{FirmaWebConfiguration.SAWEndPoint.Host}\" (hostname of SAW endpoint) listed in Subject Alternative Name but only had these names listed: \"{string.Join("\", \"", dnsNameList)}\".";
                return false;
            }

            userDisplayableValidationErrorMessage = string.Empty;
            return true;
        }

        /// <summary>
        /// an XML signature can "cover" not the whole document, but only a part of it
        /// .NET's built in "CheckSignature" does not cover this case, it will validate to true.
        /// We should check the signature reference, so it "references" the id of the root document element! If not - it's a hack
        /// </summary>
        private bool HasValidSignatureReference(SignedXml signedXml)
        {
            if (signedXml.SignedInfo.References.Count != 1) //no ref at all
            {
                return false;
            }

            var reference = (Reference)signedXml.SignedInfo.References[0];
            var id = reference.Uri.Substring(1);

            var idElement = signedXml.GetIdElement(_xmlDoc, id);

            if (idElement == _xmlDoc.DocumentElement)
            {
                return true;
            }

            var assertionNode = _xmlDoc.SelectSingleNode("/samlp:Response/saml:Assertion", _xmlNameSpaceManager) as XmlElement;
            return assertionNode == idElement;
        }
        
        public string GetIssuer()
        {
            var node = _xmlDoc.SelectSingleNode("/samlp:Response/saml:Issuer", _xmlNameSpaceManager);
            return node?.InnerText;
        }

        private string GetName()
        {
            var node = _xmlDoc.SelectSingleNode("/samlp:Response/saml:Assertion/saml:AttributeStatement/saml:Attribute[@Name='name']/saml:AttributeValue", _xmlNameSpaceManager);
            return node?.InnerText;
        }

        public string GetFirstName()
        {
            var fullName = GetName();
            var names = fullName.Split(' ');
            var firstName = names.Length == 2 ? names[0] : fullName;
            return firstName;
        }

        public string GetLastName()
        {
            var fullName = GetName();
            var names = fullName.Split(' ');
            var lastName = names.Length == 2 ? names[1] : "";
            return lastName;
        }

        public string GetEmail()
        {
            //some providers (for example Azure AD) put email into an attribute named "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress"
            var node = _xmlDoc.SelectSingleNode("/samlp:Response/saml:Assertion/saml:AttributeStatement/saml:Attribute[@Name='email']/saml:AttributeValue", _xmlNameSpaceManager) ??
                           _xmlDoc.SelectSingleNode("/samlp:Response/saml:Assertion/saml:AttributeStatement/saml:Attribute[@Name='http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress']/saml:AttributeValue", _xmlNameSpaceManager);
            return node?.InnerText;
        }

        public List<string> GetRoleGroups()
        {
            var nodes = _xmlDoc.SelectNodes("/samlp:Response/saml:Assertion/saml:AttributeStatement/saml:Attribute[@Name='groups']/saml:AttributeValue", _xmlNameSpaceManager);
            if (nodes != null && nodes.Count > 0)
            {
                return nodes.Cast<XmlNode>().Select(x => x.InnerText).ToList();
            }
            return new List<string>();
        }

        public Authenticator GetWhichSawAuthenticator()
        {
            // ReSharper disable once StringLiteralTypo
            if (GetIssuer().Contains("test-secureaccess.wa.gov", StringComparison.InvariantCultureIgnoreCase))
            {
                return Authenticator.SAWTEST;
            }
            return Authenticator.SAWPROD;
        }

        private bool IsResponseStillWithinValidTimePeriod()
        {
            var dateTimeOffsetNow = DateTimeOffset.Now;
            return IsResponseStillWithinValidTimePeriod(dateTimeOffsetNow);
        }

        internal bool IsResponseStillWithinValidTimePeriod(DateTimeOffset dateTimeOffsetNow)
        {
            var expirationDateTimeOffset = DateTimeOffset.MaxValue;
            var node = _xmlDoc.SelectSingleNode(
                "/samlp:Response/saml:Assertion/saml:Subject/saml:SubjectConfirmation/saml:SubjectConfirmationData",
                _xmlNameSpaceManager);
            if (node?.Attributes?["NotOnOrAfter"] != null)
            {
                DateTimeOffset.TryParse(node.Attributes["NotOnOrAfter"].Value, out expirationDateTimeOffset);
            }
            return dateTimeOffsetNow < expirationDateTimeOffset;
        }

        private static XmlNamespaceManager GetNamespaceManager(XmlDocument xmlDocument)
        {
            //returns namespace manager, we need one b/c MS says so... Otherwise XPath doesnt work in an XML doc with namespaces
            //see https://stackoverflow.com/questions/7178111/why-is-xmlnamespacemanager-necessary
            var manager = new XmlNamespaceManager(xmlDocument.NameTable);
            manager.AddNamespace("ds", SignedXml.XmlDsigNamespaceUrl);
            manager.AddNamespace("saml", "urn:oasis:names:tc:SAML:2.0:assertion");
            manager.AddNamespace("samlp", "urn:oasis:names:tc:SAML:2.0:protocol");
            return manager;
        }

        public string GetSamlAsPrettyPrintXml()
        {
            try
            {
                var stringWriter = new StringWriter();
                var xmlTextWriter = new XmlTextWriter(stringWriter);
                xmlTextWriter.Formatting = Formatting.Indented;
                _xmlDoc.WriteTo(xmlTextWriter);
                return stringWriter.ToString();
            }
            catch (Exception ex)
            {
                // At least show something if we have problems here
                return $"Problem pretty printing XML: {ex}.\r\nOriginal SAW Response: {_originalDecodedResponse}";
            }
        }
    }
}
