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
using System.Security.Cryptography.X509Certificates;
using System.Security.Cryptography.Xml;
using System.Text;
using System.Xml;
using LtInfo.Common;
using ProjectFirma.Web.Models;

namespace ProjectFirma.Web
{
    public class SawSamlResponse
    {
        private string _originalDecodedResponse;
        private XmlDocument _xmlDoc;
        private readonly X509Certificate2 _certificate;
        private XmlNamespaceManager _xmlNameSpaceManager; //we need this one to run our XPath queries on the SAML XML

        public SawSamlResponse(X509Certificate2 certificate)
        {
            _certificate = certificate;
        }

        public void LoadXmlFromBase64(string base64SawSamlResponse)
        {
            var utf8Encoding = new UTF8Encoding();
            var xmlStringSawSamlResponse = utf8Encoding.GetString(Convert.FromBase64String(base64SawSamlResponse));
            _originalDecodedResponse = xmlStringSawSamlResponse;
            _xmlDoc = new XmlDocument {PreserveWhitespace = true, XmlResolver = null};
            _xmlDoc.LoadXml(xmlStringSawSamlResponse);
            _xmlNameSpaceManager = GetNamespaceManager(_xmlDoc); //lets construct a "manager" for XPath queries
        }

        public bool IsValid()
        {
            var nodeList = _xmlDoc.SelectNodes("//ds:Signature", _xmlNameSpaceManager);
            if (nodeList == null || nodeList.Count == 0)
            {
                return false;
            }
            var signedXml = new SignedXml(_xmlDoc);
            signedXml.LoadXml((XmlElement)nodeList[0]);
            bool validateSignatureReference = ValidateSignatureReference(signedXml);
            if (!validateSignatureReference)
            {
                SitkaHttpApplication.Logger.Error("Error during SAW Login attempt, could not validate SignatureReference.");
            }
            bool checkSignature = signedXml.CheckSignature(_certificate, true);
            if (!checkSignature)
            {
                SitkaHttpApplication.Logger.Error("Error during SAW Login attempt, xml signature is invalid.");
            }
            bool isNotExpired = !IsExpired();
            return validateSignatureReference && checkSignature && isNotExpired;
        }

        //an XML signature can "cover" not the whole document, but only a part of it
        //.NET's built in "CheckSignature" does not cover this case, it will validate to true.
        //We should check the signature reference, so it "references" the id of the root document element! If not - it's a hack
        private bool ValidateSignatureReference(SignedXml signedXml)
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
            // We're not using any group claims from SAW, so leave it blank
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

        private bool IsExpired()
        {
            var expirationDate = DateTime.MaxValue;
            var node = _xmlDoc.SelectSingleNode("/samlp:Response/saml:Assertion/saml:Subject/saml:SubjectConfirmation/saml:SubjectConfirmationData", _xmlNameSpaceManager);
            if (node?.Attributes?["NotOnOrAfter"] != null)
            {
                DateTime.TryParse(node.Attributes["NotOnOrAfter"].Value, out expirationDate);
            }
            return DateTime.UtcNow > expirationDate.ToUniversalTime();
        }

        //returns namespace manager, we need one b/c MS says so... Otherwise XPath doesnt work in an XML doc with namespaces


        //see https://stackoverflow.com/questions/7178111/why-is-xmlnamespacemanager-necessary


        private static XmlNamespaceManager GetNamespaceManager(XmlDocument xmlDocument)
        {
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
            catch (Exception e)
            {
                // At least show something if we have problems here
                return $"Problem pretty printing XML: {e.Message}. Original SAW Response: {_originalDecodedResponse}";
            }
        }
    }
}
