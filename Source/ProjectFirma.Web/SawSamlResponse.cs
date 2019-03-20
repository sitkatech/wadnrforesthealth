// ReSharper disable CommentTypo
/*	Jitbit's simple SAML 2.0 component for ASP.NET
	https://github.com/jitbit/AspNetSaml/
	(c) Jitbit LP, 2016
	Use this freely under the MIT license (see http://choosealicense.com/licenses/mit/)
	version 1.2
*/
using System;
using System.Security.Cryptography.X509Certificates;
using System.Security.Cryptography.Xml;
using System.Text;
using System.Xml;

namespace ProjectFirma.Web
{
    public class SawSamlResponse
    {
        private XmlDocument _xmlDoc;
        private readonly X509Certificate2 _certificate;
        private XmlNamespaceManager _xmlNameSpaceManager; //we need this one to run our XPath queries on the SAML XML

        public SawSamlResponse(X509Certificate2 certificate)
        {
            _certificate = certificate;
        }

        public void LoadXmlFromBase64(string response)
        {
            var utf8Encoding = new UTF8Encoding();
            _xmlDoc = new XmlDocument {PreserveWhitespace = true, XmlResolver = null};
            _xmlDoc.LoadXml(utf8Encoding.GetString(Convert.FromBase64String(response)));
            _xmlNameSpaceManager = GetNamespaceManager(); //lets construct a "manager" for XPath queries
        }

        public bool IsValid()
        {
            var nodeList = _xmlDoc.SelectNodes("//ds:Signature", _xmlNameSpaceManager);

            var signedXml = new SignedXml(_xmlDoc);

            // ReSharper disable once PossibleNullReferenceException
            if (nodeList.Count == 0)
            {
                return false;
            }

            signedXml.LoadXml((XmlElement)nodeList[0]);
            return ValidateSignatureReference(signedXml) && signedXml.CheckSignature(_certificate, true) && !IsExpired();
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

        public string GetUserName()
        {
            var node = _xmlDoc.SelectSingleNode("/samlp:Response/saml:Assertion/saml:AttributeStatement/saml:Attribute[@Name='user']/saml:AttributeValue", _xmlNameSpaceManager);
            return node?.InnerText;
        }

        public string GetName()
        {
            var node = _xmlDoc.SelectSingleNode("/samlp:Response/saml:Assertion/saml:AttributeStatement/saml:Attribute[@Name='name']/saml:AttributeValue", _xmlNameSpaceManager);
            return node?.InnerText;
        }

        public string GetEmail()
        {
            //some providers (for example Azure AD) put email into an attribute named "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress"
            var node = _xmlDoc.SelectSingleNode("/samlp:Response/saml:Assertion/saml:AttributeStatement/saml:Attribute[@Name='email']/saml:AttributeValue", _xmlNameSpaceManager) ??
                           _xmlDoc.SelectSingleNode("/samlp:Response/saml:Assertion/saml:AttributeStatement/saml:Attribute[@Name='http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress']/saml:AttributeValue", _xmlNameSpaceManager);
            return node?.InnerText;
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
        private XmlNamespaceManager GetNamespaceManager()
        {
            var manager = new XmlNamespaceManager(_xmlDoc.NameTable);
            manager.AddNamespace("ds", SignedXml.XmlDsigNamespaceUrl);
            manager.AddNamespace("saml", "urn:oasis:names:tc:SAML:2.0:assertion");
            manager.AddNamespace("samlp", "urn:oasis:names:tc:SAML:2.0:protocol");
            return manager;
        }
    }
}
