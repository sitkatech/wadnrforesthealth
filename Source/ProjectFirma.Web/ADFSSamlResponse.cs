using System;
using System.Xml;
using System.Security.Cryptography.Xml;
using System.Linq;

namespace ProjectFirma.Web
{
    public class ADFSSamlResponse
    {
        private const string BaseAttributeStatementXPath = "/samlp:Response/saml:EncryptedAssertion/saml:Assertion/saml:AttributeStatement";
        private XmlDocument _xmlDoc;
        private XmlNamespaceManager _xmlNameSpaceManager; //we need this one to run our XPath queries on the SAML XML

        public string Xml => _xmlDoc.OuterXml;

        public void LoadXml(string xml)
        {
            _xmlDoc = new XmlDocument();
            _xmlDoc.PreserveWhitespace = true;
            _xmlDoc.XmlResolver = null;
            _xmlDoc.LoadXml(xml);

            _xmlNameSpaceManager = GetNamespaceManager(); //lets construct a "manager" for XPath queries
        }

        public void LoadXmlFromBase64(string response)
        {
            System.Text.UTF8Encoding enc = new System.Text.UTF8Encoding();
            LoadXml(enc.GetString(Convert.FromBase64String(response)));
        }

        public void Decrypt()
        {
            /* first step: make sure encrypted xml contains all these items.
             * EncryptedKey                (present in many documents)
             *   EncryptionMethod          (present in many documents)
             *     KeyInfo                 (absent in some documents)
             *       X509Data                 ditto
             *         X509Certificate        ditto with x.509 cert in Base4
             *  
             * because that's the way dotnet EncryptedXML figures out
             * under the covers which key to use to decode the document,
             * bless its pointed little head.  */

            /* handle the decrypt */
            var encryptedXml = new EncryptedXml(_xmlDoc);
            encryptedXml.DecryptDocument();
        }

        public string GetFirstName()
        {
            var node = _xmlDoc.SelectSingleNode($"{BaseAttributeStatementXPath}/saml:Attribute[@Name=\'http://schemas.xmlsoap.org/ws/2005/05/identity/claims/firstname\']/saml:AttributeValue", _xmlNameSpaceManager);
            return node?.InnerText;
        }

        public string GetLastName()
        {
            var node = _xmlDoc.SelectSingleNode($"{BaseAttributeStatementXPath}/saml:Attribute[@Name='http://schemas.xmlsoap.org/ws/2005/05/identity/claims/lastname']/saml:AttributeValue", _xmlNameSpaceManager);
            return node?.InnerText;
        }

        public string GetUPN()
        {
            var node = _xmlDoc.SelectSingleNode($"{BaseAttributeStatementXPath}/saml:Attribute[@Name='http://schemas.xmlsoap.org/ws/2005/05/identity/claims/upn']/saml:AttributeValue", _xmlNameSpaceManager);
            return node?.InnerText;
        }

        public string GetEmail()
        {
            var node = _xmlDoc.SelectSingleNode($"{BaseAttributeStatementXPath}/saml:Attribute[@Name='http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress']/saml:AttributeValue", _xmlNameSpaceManager);
            return node?.InnerText;
        }

        public string GetRoleGroups()
        {
            var nodes = _xmlDoc.SelectNodes($"{BaseAttributeStatementXPath}/saml:Attribute[@Name='http://schemas.xmlsoap.org/claims/Group']/saml:AttributeValue", _xmlNameSpaceManager);
            if (nodes != null && nodes.Count > 0)
            {
                return string.Join(",", nodes.Cast<XmlNode>().Select(x => x.InnerText));
            }

            return null;
        }

        //returns namespace manager, we need one b/c MS says so... Otherwise XPath doesnt work in an XML doc with namespaces
        //see https://stackoverflow.com/questions/7178111/why-is-xmlnamespacemanager-necessary
        private XmlNamespaceManager GetNamespaceManager()
        {
            XmlNamespaceManager manager = new XmlNamespaceManager(_xmlDoc.NameTable);
            manager.AddNamespace("ds", SignedXml.XmlDsigNamespaceUrl);
            manager.AddNamespace("saml", "urn:oasis:names:tc:SAML:2.0:assertion");
            manager.AddNamespace("samlp", "urn:oasis:names:tc:SAML:2.0:protocol");

            return manager;
        }
    }
}
