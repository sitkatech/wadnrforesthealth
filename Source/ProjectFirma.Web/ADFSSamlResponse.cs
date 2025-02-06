using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using System.Security.Cryptography.Xml;
using System.Linq;

namespace ProjectFirma.Web
{
    // ReSharper disable once InconsistentNaming
    public class ADFSSamlResponse
    {
        private string _originalDecodedResponse;
        private const string BaseAttributeStatementXPath = "/samlp:Response/saml:EncryptedAssertion/saml:Assertion/saml:AttributeStatement";
        private const string BaseAttributeStatementXPathAzure = "/samlp:Response/saml:Assertion/saml:AttributeStatement";
        private XmlDocument _xmlDoc;
        private XmlNamespaceManager _xmlNameSpaceManager; //we need this one to run our XPath queries on the SAML XML

        public void LoadXmlFromBase64(string base64AdfsSamlResponse)
        {
            var utf8Encoding = new System.Text.UTF8Encoding();
            var xmlStringAdfsSamlResponse = utf8Encoding.GetString(Convert.FromBase64String(base64AdfsSamlResponse));
            _originalDecodedResponse = xmlStringAdfsSamlResponse;
            _xmlDoc = new XmlDocument {PreserveWhitespace = true, XmlResolver = null};
            _xmlDoc.LoadXml(xmlStringAdfsSamlResponse);
            _xmlNameSpaceManager = GetNamespaceManager(_xmlDoc); //lets construct a "manager" for XPath queries
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

            if(node == null)
            {
                node = _xmlDoc.SelectSingleNode($"{BaseAttributeStatementXPathAzure}/saml:Attribute[@Name=\'http://schemas.xmlsoap.org/ws/2005/05/identity/claims/Firstname\']/saml:AttributeValue", _xmlNameSpaceManager);
            }

            return node?.InnerText;
        }

        public string GetLastName()
        {
            var node = _xmlDoc.SelectSingleNode($"{BaseAttributeStatementXPath}/saml:Attribute[@Name='http://schemas.xmlsoap.org/ws/2005/05/identity/claims/lastname']/saml:AttributeValue", _xmlNameSpaceManager);

            if (node == null)
            {
                node = _xmlDoc.SelectSingleNode($"{BaseAttributeStatementXPathAzure}/saml:Attribute[@Name='http://schemas.xmlsoap.org/ws/2005/05/identity/claims/Lastname']/saml:AttributeValue", _xmlNameSpaceManager);
            }

            return node?.InnerText;
        }

        public string GetEmail()
        {
            var node = _xmlDoc.SelectSingleNode($"{BaseAttributeStatementXPath}/saml:Attribute[@Name='http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress']/saml:AttributeValue", _xmlNameSpaceManager);
            if (node == null)
            {
                node = _xmlDoc.SelectSingleNode($"{BaseAttributeStatementXPathAzure}/saml:Attribute[@Name='http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress']/saml:AttributeValue", _xmlNameSpaceManager);
            }
            return node?.InnerText;
        }

        public List<string> GetRoleGroups()
        {
            var nodes = _xmlDoc.SelectNodes($"{BaseAttributeStatementXPath}/saml:Attribute[@Name='http://schemas.xmlsoap.org/claims/Group']/saml:AttributeValue", _xmlNameSpaceManager);
            if (nodes != null && nodes.Count > 0)
            {
                return nodes.Cast<XmlNode>().Select(x => x.InnerText).ToList();
            }
            return new List<string>();
        }

        //returns namespace manager, we need one b/c MS says so... Otherwise XPath doesn't work in an XML doc with namespaces
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
                return $"Problem pretty printing XML: {e.Message}. Original ADFS Response: {_originalDecodedResponse}";
            }
        }

    }
}
