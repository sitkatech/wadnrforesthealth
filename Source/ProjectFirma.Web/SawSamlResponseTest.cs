using System;
using System.Collections.Generic;
using NUnit.Framework;
using ProjectFirma.Web.Models;

namespace ProjectFirma.Web
{

    [TestFixture]
    public class SawSamlResponseTest
    {
        /// <summary>
        /// To update these, log in locally and the AccountController logs the saw saml response, so you can copy from the log file.
        /// After updating these, don't forget to update the testDatTime in the TestSawSamlResponseDateValidity method.
        /// </summary>
        private string _sampleSawSamlPrettyPrinted = @"<samlp:Response xmlns:saml=""urn:oasis:names:tc:SAML:2.0:assertion"" xmlns:samlp=""urn:oasis:names:tc:SAML:2.0:protocol"" xmlns:xs=""http://www.w3.org/2001/XMLSchema"" xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" Destination=""https://wadnrforesthealth.localhost.projectfirma.com/Account/SAWPost"" ID=""FIMRSP_12ca0a32-0190-1e83-a4e5-f8d1295652f1"" IssueInstant=""2024-06-13T18:09:09Z"" Version=""2.0"">
  <saml:Issuer Format=""urn:oasis:names:tc:SAML:2.0:nameid-format:entity"">https://test-secureaccess.wa.gov/FIM2/sps/sawidp/saml20</saml:Issuer>
  <samlp:Status>
    <samlp:StatusCode Value=""urn:oasis:names:tc:SAML:2.0:status:Success"">
    </samlp:StatusCode>
  </samlp:Status>
  <saml:Assertion ID=""Assertion-uuid12ca0a23-0190-10c8-8100-f8d1295652f1"" IssueInstant=""2024-06-13T18:09:09Z"" Version=""2.0"">
    <saml:Issuer Format=""urn:oasis:names:tc:SAML:2.0:nameid-format:entity"">https://test-secureaccess.wa.gov/FIM2/sps/sawidp/saml20</saml:Issuer>
    <ds:Signature xmlns:ds=""http://www.w3.org/2000/09/xmldsig#"" Id=""uuid12ca0a25-0190-173b-8db7-f8d1295652f1"">
      <ds:SignedInfo>
        <ds:CanonicalizationMethod Algorithm=""http://www.w3.org/2001/10/xml-exc-c14n#"">
        </ds:CanonicalizationMethod>
        <ds:SignatureMethod Algorithm=""http://www.w3.org/2001/04/xmldsig-more#rsa-sha256"">
        </ds:SignatureMethod>
        <ds:Reference URI=""#Assertion-uuid12ca0a23-0190-10c8-8100-f8d1295652f1"">
          <ds:Transforms>
            <ds:Transform Algorithm=""http://www.w3.org/2000/09/xmldsig#enveloped-signature"">
            </ds:Transform>
            <ds:Transform Algorithm=""http://www.w3.org/2001/10/xml-exc-c14n#"">
              <ec:InclusiveNamespaces xmlns:ec=""http://www.w3.org/2001/10/xml-exc-c14n#"" PrefixList=""saml xs xsi"">
              </ec:InclusiveNamespaces>
            </ds:Transform>
          </ds:Transforms>
          <ds:DigestMethod Algorithm=""http://www.w3.org/2001/04/xmlenc#sha256"">
          </ds:DigestMethod>
          <ds:DigestValue>1uOiS2zyJBI6uw767KHeaUAtpL51gnXIEgB/ExqfX0U=</ds:DigestValue>
        </ds:Reference>
      </ds:SignedInfo>
      <ds:SignatureValue>mjxCNHtqJodiumPeSqkBkMAGtnXnNBUXLL9YLeMBjFtDRcIAbAIBtDVoYhZrJsOCIf2DZFL/1IzA7Tf42ho4XPlpOWNif+Afa/hgG6dm9fvpyrAsjf6l/KZ3rXdRVNdv78g6sICHSPIJQz51r2RWMSVzn+HKJUnCFwjhQ6GJMcZnTZ3DMyxFptXyfBX60fyEB2ZH7aVWycMyFuE3gK+j7UDBh0/p1XQoVmK3O6wh0JYkijS1A3yry0/HP5HBV+GyEXqhFxOTJ9noLPfYsQgDRhDtkfBzUXoxM1MiUB113NTfNBeMME3yO7aizqGI2xZ2ciaIKAh4fe7O/7mwRvPyEA==</ds:SignatureValue>
      <ds:KeyInfo>
        <ds:X509Data>
          <ds:X509Certificate>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</ds:X509Certificate>
        </ds:X509Data>
      </ds:KeyInfo>
    </ds:Signature>
    <saml:Subject>
      <saml:NameID Format=""urn:oasis:names:tc:SAML:1.1:nameid-format:emailAddress"">DP4MT6TV3ZT7M-3QT5ZL6DM-DD7WV4ZZ8D-1FZ3DD4VZ4</saml:NameID>
      <saml:SubjectConfirmation Method=""urn:oasis:names:tc:SAML:2.0:cm:bearer"">
        <saml:SubjectConfirmationData NotOnOrAfter=""2024-06-13T18:10:09Z"" Recipient=""https://wadnrforesthealth.localhost.projectfirma.com/Account/SAWPost"">
        </saml:SubjectConfirmationData>
      </saml:SubjectConfirmation>
    </saml:Subject>
    <saml:Conditions NotBefore=""2024-06-13T18:08:09Z"" NotOnOrAfter=""2024-06-13T18:10:09Z"">
      <saml:AudienceRestriction>
        <saml:Audience>https://wadnrforesthealth.localhost.projectfirma.com</saml:Audience>
      </saml:AudienceRestriction>
    </saml:Conditions>
    <saml:AuthnStatement AuthnInstant=""2024-06-13T18:09:09Z"" SessionIndex=""uuid872ad435-ba4e-439b-8e1f-ad9b87b28899"" SessionNotOnOrAfter=""2024-06-13T19:09:09Z"">
      <saml:AuthnContext>
        <saml:AuthnContextClassRef>urn:oasis:names:tc:SAML:2.0:ac:classes:Password</saml:AuthnContextClassRef>
      </saml:AuthnContext>
    </saml:AuthnStatement>
    <saml:AttributeStatement>
      <saml:Attribute Name=""name"" NameFormat=""urn:oasis:names:tc:SAML:2.0:assertion"">
        <saml:AttributeValue xsi:type=""xs:string"">tom kamin</saml:AttributeValue>
      </saml:Attribute>
      <saml:Attribute Name=""level"" NameFormat=""urn:oasis:names:tc:SAML:2.0:assertion"">
        <saml:AttributeValue xsi:type=""xs:string"">1</saml:AttributeValue>
      </saml:Attribute>
      <saml:Attribute Name=""groups"" NameFormat=""urn:oasis:names:tc:SAML:2.0:assertion"">
        <saml:AttributeValue xsi:type=""xs:string"">f3_users</saml:AttributeValue>
      </saml:Attribute>
      <saml:Attribute Name=""user"" NameFormat=""urn:oasis:names:tc:SAML:2.0:assertion"">
        <saml:AttributeValue xsi:type=""xs:string"">tom.kamin</saml:AttributeValue>
      </saml:Attribute>
      <saml:Attribute Name=""email"" NameFormat=""urn:oasis:names:tc:SAML:2.0:assertion"">
        <saml:AttributeValue xsi:type=""xs:string"">tom.kamin@sitkatech.com</saml:AttributeValue>
      </saml:Attribute>
      <saml:Attribute Name=""guid"" NameFormat=""urn:oasis:names:tc:SAML:2.0:assertion"">
        <saml:AttributeValue xsi:type=""xs:string"">DP4MT6TV3ZT7M-3QT5ZL6DM-DD7WV4ZZ8D-1FZ3DD4VZ4</saml:AttributeValue>
      </saml:Attribute>
    </saml:AttributeStatement>
  </saml:Assertion>
</samlp:Response>";

        private string _sampleSawSamlBase64Encoded = @"PHNhbWxwOlJlc3BvbnNlIHhtbG5zOnNhbWw9InVybjpvYXNpczpuYW1lczp0YzpTQU1MOjIuMDph
c3NlcnRpb24iIHhtbG5zOnNhbWxwPSJ1cm46b2FzaXM6bmFtZXM6dGM6U0FNTDoyLjA6cHJvdG9j
b2wiIHhtbG5zOnhzPSJodHRwOi8vd3d3LnczLm9yZy8yMDAxL1hNTFNjaGVtYSIgeG1sbnM6eHNp
PSJodHRwOi8vd3d3LnczLm9yZy8yMDAxL1hNTFNjaGVtYS1pbnN0YW5jZSIgRGVzdGluYXRpb249
Imh0dHBzOi8vd2FkbnJmb3Jlc3RoZWFsdGgubG9jYWxob3N0LnByb2plY3RmaXJtYS5jb20vQWNj
b3VudC9TQVdQb3N0IiBJRD0iRklNUlNQXzEyY2EwYTMyLTAxOTAtMWU4My1hNGU1LWY4ZDEyOTU2
NTJmMSIgSXNzdWVJbnN0YW50PSIyMDI0LTA2LTEzVDE4OjA5OjA5WiIgVmVyc2lvbj0iMi4wIj48
c2FtbDpJc3N1ZXIgRm9ybWF0PSJ1cm46b2FzaXM6bmFtZXM6dGM6U0FNTDoyLjA6bmFtZWlkLWZv
cm1hdDplbnRpdHkiPmh0dHBzOi8vdGVzdC1zZWN1cmVhY2Nlc3Mud2EuZ292L0ZJTTIvc3BzL3Nh
d2lkcC9zYW1sMjA8L3NhbWw6SXNzdWVyPjxzYW1scDpTdGF0dXM+PHNhbWxwOlN0YXR1c0NvZGUg
VmFsdWU9InVybjpvYXNpczpuYW1lczp0YzpTQU1MOjIuMDpzdGF0dXM6U3VjY2VzcyI+PC9zYW1s
cDpTdGF0dXNDb2RlPjwvc2FtbHA6U3RhdHVzPjxzYW1sOkFzc2VydGlvbiBJRD0iQXNzZXJ0aW9u
LXV1aWQxMmNhMGEyMy0wMTkwLTEwYzgtODEwMC1mOGQxMjk1NjUyZjEiIElzc3VlSW5zdGFudD0i
MjAyNC0wNi0xM1QxODowOTowOVoiIFZlcnNpb249IjIuMCI+PHNhbWw6SXNzdWVyIEZvcm1hdD0i
dXJuOm9hc2lzOm5hbWVzOnRjOlNBTUw6Mi4wOm5hbWVpZC1mb3JtYXQ6ZW50aXR5Ij5odHRwczov
L3Rlc3Qtc2VjdXJlYWNjZXNzLndhLmdvdi9GSU0yL3Nwcy9zYXdpZHAvc2FtbDIwPC9zYW1sOklz
c3Vlcj48ZHM6U2lnbmF0dXJlIHhtbG5zOmRzPSJodHRwOi8vd3d3LnczLm9yZy8yMDAwLzA5L3ht
bGRzaWcjIiBJZD0idXVpZDEyY2EwYTI1LTAxOTAtMTczYi04ZGI3LWY4ZDEyOTU2NTJmMSI+PGRz
OlNpZ25lZEluZm8+PGRzOkNhbm9uaWNhbGl6YXRpb25NZXRob2QgQWxnb3JpdGhtPSJodHRwOi8v
d3d3LnczLm9yZy8yMDAxLzEwL3htbC1leGMtYzE0biMiPjwvZHM6Q2Fub25pY2FsaXphdGlvbk1l
dGhvZD48ZHM6U2lnbmF0dXJlTWV0aG9kIEFsZ29yaXRobT0iaHR0cDovL3d3dy53My5vcmcvMjAw
MS8wNC94bWxkc2lnLW1vcmUjcnNhLXNoYTI1NiI+PC9kczpTaWduYXR1cmVNZXRob2Q+PGRzOlJl
ZmVyZW5jZSBVUkk9IiNBc3NlcnRpb24tdXVpZDEyY2EwYTIzLTAxOTAtMTBjOC04MTAwLWY4ZDEy
OTU2NTJmMSI+PGRzOlRyYW5zZm9ybXM+PGRzOlRyYW5zZm9ybSBBbGdvcml0aG09Imh0dHA6Ly93
d3cudzMub3JnLzIwMDAvMDkveG1sZHNpZyNlbnZlbG9wZWQtc2lnbmF0dXJlIj48L2RzOlRyYW5z
Zm9ybT48ZHM6VHJhbnNmb3JtIEFsZ29yaXRobT0iaHR0cDovL3d3dy53My5vcmcvMjAwMS8xMC94
bWwtZXhjLWMxNG4jIj48ZWM6SW5jbHVzaXZlTmFtZXNwYWNlcyB4bWxuczplYz0iaHR0cDovL3d3
dy53My5vcmcvMjAwMS8xMC94bWwtZXhjLWMxNG4jIiBQcmVmaXhMaXN0PSJzYW1sIHhzIHhzaSI+
PC9lYzpJbmNsdXNpdmVOYW1lc3BhY2VzPjwvZHM6VHJhbnNmb3JtPjwvZHM6VHJhbnNmb3Jtcz48
ZHM6RGlnZXN0TWV0aG9kIEFsZ29yaXRobT0iaHR0cDovL3d3dy53My5vcmcvMjAwMS8wNC94bWxl
bmMjc2hhMjU2Ij48L2RzOkRpZ2VzdE1ldGhvZD48ZHM6RGlnZXN0VmFsdWU+MXVPaVMyenlKQkk2
dXc3NjdLSGVhVUF0cEw1MWduWElFZ0IvRXhxZlgwVT08L2RzOkRpZ2VzdFZhbHVlPjwvZHM6UmVm
ZXJlbmNlPjwvZHM6U2lnbmVkSW5mbz48ZHM6U2lnbmF0dXJlVmFsdWU+bWp4Q05IdHFKb2RpdW1Q
ZVNxa0JrTUFHdG5Ybk5CVVhMTDlZTGVNQmpGdERSY0lBYkFJQnREVm9ZaFpySnNPQ0lmMkRaRkwv
MUl6QTdUZjQyaG80WFBscE9XTmlmK0FmYS9oZ0c2ZG05ZnZweXJBc2pmNmwvS1ozclhkUlZOZHY3
OGc2c0lDSFNQSUpRejUxcjJSV01TVnpuK0hLSlVuQ0Z3amhRNkdKTWNablRaM0RNeXhGcHRYeWZC
WDYwZnlFQjJaSDdhVld5Y015RnVFM2dLK2o3VURCaDAvcDFYUW9WbUszTzZ3aDBKWWtpalMxQTN5
cnkwL0hQNUhCVitHeUVYcWhGeE9USjlub0xQZllzUWdEUmhEdGtmQnpVWG94TTFNaVVCMTEzTlRm
TkJlTU1FM3lPN2FpenFHSTJ4WjJjaWFJS0FoNGZlN08vN213UnZQeUVBPT08L2RzOlNpZ25hdHVy
ZVZhbHVlPjxkczpLZXlJbmZvPjxkczpYNTA5RGF0YT48ZHM6WDUwOUNlcnRpZmljYXRlPk1JSUdt
akNDQllLZ0F3SUJBZ0lRV3poQWNYOTRienltZ1NycUp6ZUpJVEFOQmdrcWhraUc5dzBCQVFzRkFE
Q0J1akVMTUFrR0ExVUVCaE1DVlZNeEZqQVVCZ05WQkFvVERVVnVkSEoxYzNRc0lFbHVZeTR4S0RB
bUJnTlZCQXNUSDFObFpTQjNkM2N1Wlc1MGNuVnpkQzV1WlhRdmJHVm5ZV3d0ZEdWeWJYTXhPVEEz
QmdOVkJBc1RNQ2hqS1NBeU1ERXlJRVZ1ZEhKMWMzUXNJRWx1WXk0Z0xTQm1iM0lnWVhWMGFHOXlh
WHBsWkNCMWMyVWdiMjVzZVRFdU1Dd0dBMVVFQXhNbFJXNTBjblZ6ZENCRFpYSjBhV1pwWTJGMGFX
OXVJRUYxZEdodmNtbDBlU0F0SUV3eFN6QWVGdzB5TkRBMU1EZ3hPRFU1TkRsYUZ3MHlOVEEyTURN
eE9EVTVORGhhTUhVeEN6QUpCZ05WQkFZVEFsVlRNUk13RVFZRFZRUUlFd3BYWVhOb2FXNW5kRzl1
TVJBd0RnWURWUVFIRXdkUGJIbHRjR2xoTVJ3d0dnWURWUVFLRXhOVGRHRjBaU0J2WmlCWFlYTm9h
VzVuZEc5dU1TRXdId1lEVlFRREV4aDBaWE4wTFhObFkzVnlaV0ZqWTJWemN5NTNZUzVuYjNZd2dn
RWlNQTBHQ1NxR1NJYjNEUUVCQVFVQUE0SUJEd0F3Z2dFS0FvSUJBUUN6VjhVc3Q2aG5zSmRSSlMy
QnhIeVVRRng3N1ZlZkc1Tmk0RWM4V2MxMW9xMWVhZUdGZlFpNUdUTHNBRlJHMmhEdG1JQjJ3V3cv
Vk5RZExrVnZVYm5BKzZyN3RBZm1reWFDRmZQRjltUGRqUlNobHlEV1RUclhIN0VkRGdTUzlkK2hS
RDgwRndyVHdaYXRYT085RnB6eWovQ2ppQzhRc0JZY1NoT3VOckJaYVJwTmR5VmF0dDF1OUtzdVdT
NGdCM2pGWXR4dTN6UzhGYi9RSkJvZ2FxOE1LL3J4dklQeGVBZk5NcDZRR29wbFkzRk0zSVBNeXA4
WFV4aTl2UEp1ejN6UmN6OGN0NjlZcnZ2UzFPZ25XNWNwa3U2bS9iRTU3Y0FwUzN5RmFzNjMyTnVx
VGlOQkl4QWZXQkcvRHhGc3J3M05TK2duQW9XU2ZZcy81dUxmU01ReEFnTUJBQUdqZ2dMZU1JSUMy
akFNQmdOVkhSTUJBZjhFQWpBQU1CMEdBMVVkRGdRV0JCUW9vWUVnRFgxNnNsVGNJb0J5UjZGZlhl
bXV1akFmQmdOVkhTTUVHREFXZ0JTQ29uQjAzYnhUUDg5NzFQZk5mNmRneGdwTXZ6Qm9CZ2dyQmdF
RkJRY0JBUVJjTUZvd0l3WUlLd1lCQlFVSE1BR0dGMmgwZEhBNkx5OXZZM053TG1WdWRISjFjM1F1
Ym1WME1ETUdDQ3NHQVFVRkJ6QUNoaWRvZEhSd09pOHZZV2xoTG1WdWRISjFjM1F1Ym1WMEwyd3hh
eTFqYUdGcGJqSTFOaTVqWlhJd013WURWUjBmQkN3d0tqQW9vQ2FnSklZaWFIUjBjRG92TDJOeWJD
NWxiblJ5ZFhOMExtNWxkQzlzWlhabGJERnJMbU55YkRBakJnTlZIUkVFSERBYWdoaDBaWE4wTFhO
bFkzVnlaV0ZqWTJWemN5NTNZUzVuYjNZd0RnWURWUjBQQVFIL0JBUURBZ1dnTUIwR0ExVWRKUVFX
TUJRR0NDc0dBUVVGQndNQkJnZ3JCZ0VGQlFjREFqQVRCZ05WSFNBRUREQUtNQWdHQm1lQkRBRUNB
akNDQVlBR0Npc0dBUVFCMW5rQ0JBSUVnZ0Z3QklJQmJBRnFBSGNBNXRJeFkwQjNqTUVRUVFiWGNi
bk93ZEpBOXBhRWh2dTZoeklkL1I0M2psQUFBQUdQV1pOK1RnQUFCQU1BU0RCR0FpRUFzdG16ZXBT
cVRGQkIxakhuWmNMYnEwT3k1ZXkvSVhGTXlUdXV4cUx1a2NBQ0lRREVhdW9ha1F4YWJlYmZyUnZ1
azNWaGlkOVhQQTdJY0IrNSttRFZMdHYwR1FCM0FPQ1NzL3dNSGNqbmFEWWYzbUc1bGswS1VuZ1pp
bkxXY3NTd1RhVnRiMVFFQUFBQmoxbVRmajBBQUFRREFFZ3dSZ0loQU10d2ZqZUNEL1BKMXpzT1J5
RXVzS1BqalhHb0t6azZKR0d3U3RwYXdjdGtBaUVBbUdIYURjUERMQjhnQ3pPL2R0dzYrVGI0cmE5
NDM2QkpKMnFGcFlTNjNiSUFkZ0JPZGFNblhKb1F3emhiYk5UZlAxTHJIZkRnamh1TmFjQ3grbVN4
WXBvNTN3QUFBWTlaazM1WkFBQUVBd0JITUVVQ0lRQ1dHaUk0K051NUtXRG1mK1FlOFI1NzY5Z2d5
VG9keC9XMEVOd0RMKy9UQXdJZ1B5a2lGOG1CbXA4Wnl5d211MmdNQ2tyNWdKdll1aE1GbzYxdnZX
QVJlWlF3RFFZSktvWklodmNOQVFFTEJRQURnZ0VCQUNmcGltb3VmdlpueVpDVXYzK3hqRGxaemg5
NFRUMFVTbTlVb0J2WENFaEU3aFJUcTdMdXNtR1NQczhRTy9tWThkc1QwNmJoMDFsRWNRM1FQamJO
SG1HSzJacUJpcDdZSVFhYmw4bUZJUG1mWmlCQllJMzhsMFRYeW80V3NnWElQOXAyajZkZE9YV0xM
NWV6dWY0MXlyYUJwQ294aGlvS1JVc3d6eUhvRUdhZExoUy9JbFVYMHJlcGF2NjRWczdwUFAzcGpU
cmtWcXFzQUtBVkdKQUloUUYrVFB3VEppRTdOc2V2V1U4NlgwNzk1aElYL254K0d3a1g2K0Q2dmls
WUlnK0RmdnNDSCsvemJDN2lNYzA0SGVWd2tLQ3VDUlhoWTExS3NjQi94Qm5ETjg3SWNSb0krRzgz
N200eERIS3NmVnJjTUZYekNaN25FeVRvcFdmTE1pVT08L2RzOlg1MDlDZXJ0aWZpY2F0ZT48L2Rz
Olg1MDlEYXRhPjwvZHM6S2V5SW5mbz48L2RzOlNpZ25hdHVyZT48c2FtbDpTdWJqZWN0PjxzYW1s
Ok5hbWVJRCBGb3JtYXQ9InVybjpvYXNpczpuYW1lczp0YzpTQU1MOjEuMTpuYW1laWQtZm9ybWF0
OmVtYWlsQWRkcmVzcyI+RFA0TVQ2VFYzWlQ3TS0zUVQ1Wkw2RE0tREQ3V1Y0Wlo4RC0xRlozREQ0
Vlo0PC9zYW1sOk5hbWVJRD48c2FtbDpTdWJqZWN0Q29uZmlybWF0aW9uIE1ldGhvZD0idXJuOm9h
c2lzOm5hbWVzOnRjOlNBTUw6Mi4wOmNtOmJlYXJlciI+PHNhbWw6U3ViamVjdENvbmZpcm1hdGlv
bkRhdGEgTm90T25PckFmdGVyPSIyMDI0LTA2LTEzVDE4OjEwOjA5WiIgUmVjaXBpZW50PSJodHRw
czovL3dhZG5yZm9yZXN0aGVhbHRoLmxvY2FsaG9zdC5wcm9qZWN0ZmlybWEuY29tL0FjY291bnQv
U0FXUG9zdCI+PC9zYW1sOlN1YmplY3RDb25maXJtYXRpb25EYXRhPjwvc2FtbDpTdWJqZWN0Q29u
ZmlybWF0aW9uPjwvc2FtbDpTdWJqZWN0PjxzYW1sOkNvbmRpdGlvbnMgTm90QmVmb3JlPSIyMDI0
LTA2LTEzVDE4OjA4OjA5WiIgTm90T25PckFmdGVyPSIyMDI0LTA2LTEzVDE4OjEwOjA5WiI+PHNh
bWw6QXVkaWVuY2VSZXN0cmljdGlvbj48c2FtbDpBdWRpZW5jZT5odHRwczovL3dhZG5yZm9yZXN0
aGVhbHRoLmxvY2FsaG9zdC5wcm9qZWN0ZmlybWEuY29tPC9zYW1sOkF1ZGllbmNlPjwvc2FtbDpB
dWRpZW5jZVJlc3RyaWN0aW9uPjwvc2FtbDpDb25kaXRpb25zPjxzYW1sOkF1dGhuU3RhdGVtZW50
IEF1dGhuSW5zdGFudD0iMjAyNC0wNi0xM1QxODowOTowOVoiIFNlc3Npb25JbmRleD0idXVpZDg3
MmFkNDM1LWJhNGUtNDM5Yi04ZTFmLWFkOWI4N2IyODg5OSIgU2Vzc2lvbk5vdE9uT3JBZnRlcj0i
MjAyNC0wNi0xM1QxOTowOTowOVoiPjxzYW1sOkF1dGhuQ29udGV4dD48c2FtbDpBdXRobkNvbnRl
eHRDbGFzc1JlZj51cm46b2FzaXM6bmFtZXM6dGM6U0FNTDoyLjA6YWM6Y2xhc3NlczpQYXNzd29y
ZDwvc2FtbDpBdXRobkNvbnRleHRDbGFzc1JlZj48L3NhbWw6QXV0aG5Db250ZXh0Pjwvc2FtbDpB
dXRoblN0YXRlbWVudD48c2FtbDpBdHRyaWJ1dGVTdGF0ZW1lbnQ+PHNhbWw6QXR0cmlidXRlIE5h
bWU9Im5hbWUiIE5hbWVGb3JtYXQ9InVybjpvYXNpczpuYW1lczp0YzpTQU1MOjIuMDphc3NlcnRp
b24iPjxzYW1sOkF0dHJpYnV0ZVZhbHVlIHhzaTp0eXBlPSJ4czpzdHJpbmciPnRvbSBrYW1pbjwv
c2FtbDpBdHRyaWJ1dGVWYWx1ZT48L3NhbWw6QXR0cmlidXRlPjxzYW1sOkF0dHJpYnV0ZSBOYW1l
PSJsZXZlbCIgTmFtZUZvcm1hdD0idXJuOm9hc2lzOm5hbWVzOnRjOlNBTUw6Mi4wOmFzc2VydGlv
biI+PHNhbWw6QXR0cmlidXRlVmFsdWUgeHNpOnR5cGU9InhzOnN0cmluZyI+MTwvc2FtbDpBdHRy
aWJ1dGVWYWx1ZT48L3NhbWw6QXR0cmlidXRlPjxzYW1sOkF0dHJpYnV0ZSBOYW1lPSJncm91cHMi
IE5hbWVGb3JtYXQ9InVybjpvYXNpczpuYW1lczp0YzpTQU1MOjIuMDphc3NlcnRpb24iPjxzYW1s
OkF0dHJpYnV0ZVZhbHVlIHhzaTp0eXBlPSJ4czpzdHJpbmciPmYzX3VzZXJzPC9zYW1sOkF0dHJp
YnV0ZVZhbHVlPjwvc2FtbDpBdHRyaWJ1dGU+PHNhbWw6QXR0cmlidXRlIE5hbWU9InVzZXIiIE5h
bWVGb3JtYXQ9InVybjpvYXNpczpuYW1lczp0YzpTQU1MOjIuMDphc3NlcnRpb24iPjxzYW1sOkF0
dHJpYnV0ZVZhbHVlIHhzaTp0eXBlPSJ4czpzdHJpbmciPnRvbS5rYW1pbjwvc2FtbDpBdHRyaWJ1
dGVWYWx1ZT48L3NhbWw6QXR0cmlidXRlPjxzYW1sOkF0dHJpYnV0ZSBOYW1lPSJlbWFpbCIgTmFt
ZUZvcm1hdD0idXJuOm9hc2lzOm5hbWVzOnRjOlNBTUw6Mi4wOmFzc2VydGlvbiI+PHNhbWw6QXR0
cmlidXRlVmFsdWUgeHNpOnR5cGU9InhzOnN0cmluZyI+dG9tLmthbWluQHNpdGthdGVjaC5jb208
L3NhbWw6QXR0cmlidXRlVmFsdWU+PC9zYW1sOkF0dHJpYnV0ZT48c2FtbDpBdHRyaWJ1dGUgTmFt
ZT0iZ3VpZCIgTmFtZUZvcm1hdD0idXJuOm9hc2lzOm5hbWVzOnRjOlNBTUw6Mi4wOmFzc2VydGlv
biI+PHNhbWw6QXR0cmlidXRlVmFsdWUgeHNpOnR5cGU9InhzOnN0cmluZyI+RFA0TVQ2VFYzWlQ3
TS0zUVQ1Wkw2RE0tREQ3V1Y0Wlo4RC0xRlozREQ0Vlo0PC9zYW1sOkF0dHJpYnV0ZVZhbHVlPjwv
c2FtbDpBdHRyaWJ1dGU+PC9zYW1sOkF0dHJpYnV0ZVN0YXRlbWVudD48L3NhbWw6QXNzZXJ0aW9u
Pjwvc2FtbHA6UmVzcG9uc2U+";

        [Test]
        [Description("For this test we want the base64 and sample pretty printed to be the same so we can debug the tests more easily")]
        public void SampleBase64AndPrettyPrintAreTheSame()
        {
            var sawSamlResponse = SawSamlResponse.CreateFromBase64String(_sampleSawSamlBase64Encoded);
            Assert.That(sawSamlResponse.GetSamlAsPrettyPrintXml(), Is.EqualTo(_sampleSawSamlPrettyPrinted));

        }

        /// <summary>
        /// testDateTime needs to be updated with a time between the SawSamlResponse's valid time, between the NotBefore and NotOnOrAfter
        /// </summary>
        [Test]
        public void TestSawSamlResponseDateValidity()
        {
            var sawSamlResponse = SawSamlResponse.CreateFromString(_sampleSawSamlPrettyPrinted);
            var testDateTime = DateTimeOffset.Parse("2024-06-13T18:09:09Z");
            Assert.That(sawSamlResponse.IsResponseStillWithinValidTimePeriod(testDateTime), Is.True, "Should be valid date");
            Assert.That(sawSamlResponse.IsResponseStillWithinValidTimePeriod(testDateTime.AddMinutes(5)), Is.False, "Should be invalid date too old");
        }

        [Test]
        public void FieldParsingShouldBeAccurate()
        {
            var sawSamlResponse = SawSamlResponse.CreateFromString(_sampleSawSamlPrettyPrinted);
            var sawSamlResponse1 = SawSamlResponse.CreateFromBase64String(_sampleSawSamlBase64Encoded);

            var listOfResponses = new List<SawSamlResponse>() { sawSamlResponse, sawSamlResponse1 };

            foreach (var samlResponse in listOfResponses)
            {
                Assert.That(samlResponse.GetEmail(), Is.EqualTo("tom.kamin@sitkatech.com"));
                Assert.That(samlResponse.GetFirstName(), Is.EqualTo("tom"));
                Assert.That(samlResponse.GetIssuer(), Is.EqualTo("https://test-secureaccess.wa.gov/FIM2/sps/sawidp/saml20"));
                Assert.That(samlResponse.GetLastName(), Is.EqualTo("kamin"));
                Assert.That(samlResponse.GetWhichSawAuthenticator().AuthenticatorFullName, Is.EqualTo(Authenticator.SAWTEST.AuthenticatorFullName));
                Assert.That(samlResponse.GetRoleGroups(), Is.EquivalentTo(new List<string> { "f3_users" }));// , "DNR-ForestHealthTrackerQA-USER-REG", "DNR-ForestHealthTrackerQA-DEFAULT_UMG-USER-REG"
            }
        }

        [Test]
        public void ValidationShouldMostlyWork()
        {
            var sawSamlResponse = SawSamlResponse.CreateFromBase64String(_sampleSawSamlBase64Encoded);
            sawSamlResponse.IsValid(out string userDisplayableValidationErrorMessage);
            Assert.That(userDisplayableValidationErrorMessage, Is.EqualTo("Current time is past the expiration time for the SAW xml response."), "Should get most of the way through validation");
        }
    }
}