using System;
using System.Collections.Generic;
using NUnit.Framework;
using ProjectFirma.Web.Models;

namespace ProjectFirma.Web
{
    [TestFixture]
    public class SawSamlResponseTest
    {
        private string _sampleSawSamlPrettyPrinted = @"<samlp:Response xmlns:saml=""urn:oasis:names:tc:SAML:2.0:assertion"" xmlns:samlp=""urn:oasis:names:tc:SAML:2.0:protocol"" xmlns:xs=""http://www.w3.org/2001/XMLSchema"" xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" Destination=""https://wadnrforesthealth.localhost.projectfirma.com/Account/SAWPost"" ID=""FIMRSP_3667bc6-0182-1150-b656-ff3dab74b0e5"" IssueInstant=""2022-07-15T19:47:04Z"" Version=""2.0"">
  <saml:Issuer Format=""urn:oasis:names:tc:SAML:2.0:nameid-format:entity"">https://test-secureaccess.wa.gov/FIM2/sps/sawidp/saml20</saml:Issuer>
  <samlp:Status>
    <samlp:StatusCode Value=""urn:oasis:names:tc:SAML:2.0:status:Success"">
    </samlp:StatusCode>
  </samlp:Status>
  <saml:Assertion ID=""Assertion-uuid3667bb5-0182-10f5-b0b4-ff3dab74b0e5"" IssueInstant=""2022-07-15T19:47:04Z"" Version=""2.0"">
    <saml:Issuer Format=""urn:oasis:names:tc:SAML:2.0:nameid-format:entity"">https://test-secureaccess.wa.gov/FIM2/sps/sawidp/saml20</saml:Issuer>
    <ds:Signature xmlns:ds=""http://www.w3.org/2000/09/xmldsig#"" Id=""uuid3667bb8-0182-142e-be8c-ff3dab74b0e5"">
      <ds:SignedInfo>
        <ds:CanonicalizationMethod Algorithm=""http://www.w3.org/2001/10/xml-exc-c14n#"">
        </ds:CanonicalizationMethod>
        <ds:SignatureMethod Algorithm=""http://www.w3.org/2001/04/xmldsig-more#rsa-sha256"">
        </ds:SignatureMethod>
        <ds:Reference URI=""#Assertion-uuid3667bb5-0182-10f5-b0b4-ff3dab74b0e5"">
          <ds:Transforms>
            <ds:Transform Algorithm=""http://www.w3.org/2000/09/xmldsig#enveloped-signature"">
            </ds:Transform>
            <ds:Transform Algorithm=""http://www.w3.org/2001/10/xml-exc-c14n#"">
              <xc14n:InclusiveNamespaces xmlns:xc14n=""http://www.w3.org/2001/10/xml-exc-c14n#"" PrefixList=""saml xs xsi"">
              </xc14n:InclusiveNamespaces>
            </ds:Transform>
          </ds:Transforms>
          <ds:DigestMethod Algorithm=""http://www.w3.org/2001/04/xmlenc#sha256"">
          </ds:DigestMethod>
          <ds:DigestValue>cf1wBxgU0PDnqvvK+8vgShlnc700+muQBOFgoKar0rQ=</ds:DigestValue>
        </ds:Reference>
      </ds:SignedInfo>
      <ds:SignatureValue>Mlolqc9E8+XqZDLi5RKbTDNATRarkaMJ5UmeVWX3T36H19rBQyUGdm5ClKlV8OvZwBiEYQVPgai1EyqY75d4qnt0dw2r1lYbKNfsSgFZ6cuFhfE8jBR2wjtBlDidq7fJf8tXE/u90KiuM5YD/YJfCUjBSm9aCXsWI3PvtS803Mui6rjQOfkhfuRa6WZotLGBzUe7OXOUaCGqoBfmOp7K8Xc/J6bvwmvHX1161fi2ujlDN0PBGjlobq00aHTj/yCAw1MZvI38tlXqEPOtp+hlKo40ZCOy6wThcplI+oHqsL+OrPHf2LSZeoruHylZ0A2mEUuibJskk9bDDFrhOkaHwQ==</ds:SignatureValue>
      <ds:KeyInfo>
        <ds:X509Data>
          <ds:X509Certificate>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</ds:X509Certificate>
        </ds:X509Data>
      </ds:KeyInfo>
    </ds:Signature>
    <saml:Subject>
      <saml:NameID Format=""urn:oasis:names:tc:SAML:1.1:nameid-format:emailAddress"">DP4VD4ZP8QQ6L-1PF0ZV2FF4-D1LW4VZ0FD-DW9QW8ZL9Q</saml:NameID>
      <saml:SubjectConfirmation Method=""urn:oasis:names:tc:SAML:2.0:cm:bearer"">
        <saml:SubjectConfirmationData NotOnOrAfter=""2022-07-15T19:48:04Z"" Recipient=""https://wadnrforesthealth.localhost.projectfirma.com/Account/SAWPost"">
        </saml:SubjectConfirmationData>
      </saml:SubjectConfirmation>
    </saml:Subject>
    <saml:Conditions NotBefore=""2022-07-15T19:46:04Z"" NotOnOrAfter=""2022-07-15T19:48:04Z"">
      <saml:AudienceRestriction>
        <saml:Audience>https://wadnrforesthealth.localhost.projectfirma.com</saml:Audience>
      </saml:AudienceRestriction>
    </saml:Conditions>
    <saml:AuthnStatement AuthnInstant=""2022-07-15T19:47:04Z"" SessionIndex=""uuid890b3ea9-2b9b-4b41-ac95-a53cc8feefc6"" SessionNotOnOrAfter=""2022-07-15T20:47:04Z"">
      <saml:AuthnContext>
        <saml:AuthnContextClassRef>urn:oasis:names:tc:SAML:2.0:ac:classes:Password</saml:AuthnContextClassRef>
      </saml:AuthnContext>
    </saml:AuthnStatement>
    <saml:AttributeStatement>
      <saml:Attribute Name=""name"" NameFormat=""urn:oasis:names:tc:SAML:2.0:assertion"">
        <saml:AttributeValue xsi:type=""xs:string"">Michael Ferrante</saml:AttributeValue>
      </saml:Attribute>
      <saml:Attribute Name=""level"" NameFormat=""urn:oasis:names:tc:SAML:2.0:assertion"">
        <saml:AttributeValue xsi:type=""xs:string"">1</saml:AttributeValue>
      </saml:Attribute>
      <saml:Attribute Name=""groups"" NameFormat=""urn:oasis:names:tc:SAML:2.0:assertion"">
        <saml:AttributeValue xsi:type=""xs:string"">f3_users</saml:AttributeValue>
        <saml:AttributeValue xsi:type=""xs:string"">DNR-ForestHealthTrackerQA-USER-REG</saml:AttributeValue>
        <saml:AttributeValue xsi:type=""xs:string"">DNR-ForestHealthTrackerQA-DEFAULT_UMG-USER-REG</saml:AttributeValue>
      </saml:Attribute>
      <saml:Attribute Name=""user"" NameFormat=""urn:oasis:names:tc:SAML:2.0:assertion"">
        <saml:AttributeValue xsi:type=""xs:string"">michael@sitkatech.com</saml:AttributeValue>
      </saml:Attribute>
      <saml:Attribute Name=""email"" NameFormat=""urn:oasis:names:tc:SAML:2.0:assertion"">
        <saml:AttributeValue xsi:type=""xs:string"">michael@sitkatech.com</saml:AttributeValue>
      </saml:Attribute>
      <saml:Attribute Name=""guid"" NameFormat=""urn:oasis:names:tc:SAML:2.0:assertion"">
        <saml:AttributeValue xsi:type=""xs:string"">DP4VD4ZP8QQ6L-1PF0ZV2FF4-D1LW4VZ0FD-DW9QW8ZL9Q</saml:AttributeValue>
      </saml:Attribute>
    </saml:AttributeStatement>
  </saml:Assertion>
</samlp:Response>";

        private string _sampleSawSamlBase64Encoded = @"PHNhbWxwOlJlc3BvbnNlIHhtbG5zOnNhbWw9InVybjpvYXNpczpuYW1lczp0YzpTQU1MOjIuMDph
c3NlcnRpb24iIHhtbG5zOnNhbWxwPSJ1cm46b2FzaXM6bmFtZXM6dGM6U0FNTDoyLjA6cHJvdG9j
b2wiIHhtbG5zOnhzPSJodHRwOi8vd3d3LnczLm9yZy8yMDAxL1hNTFNjaGVtYSIgeG1sbnM6eHNp
PSJodHRwOi8vd3d3LnczLm9yZy8yMDAxL1hNTFNjaGVtYS1pbnN0YW5jZSIgRGVzdGluYXRpb249
Imh0dHBzOi8vd2FkbnJmb3Jlc3RoZWFsdGgubG9jYWxob3N0LnByb2plY3RmaXJtYS5jb20vQWNj
b3VudC9TQVdQb3N0IiBJRD0iRklNUlNQXzM2NjdiYzYtMDE4Mi0xMTUwLWI2NTYtZmYzZGFiNzRi
MGU1IiBJc3N1ZUluc3RhbnQ9IjIwMjItMDctMTVUMTk6NDc6MDRaIiBWZXJzaW9uPSIyLjAiPjxz
YW1sOklzc3VlciBGb3JtYXQ9InVybjpvYXNpczpuYW1lczp0YzpTQU1MOjIuMDpuYW1laWQtZm9y
bWF0OmVudGl0eSI+aHR0cHM6Ly90ZXN0LXNlY3VyZWFjY2Vzcy53YS5nb3YvRklNMi9zcHMvc2F3
aWRwL3NhbWwyMDwvc2FtbDpJc3N1ZXI+PHNhbWxwOlN0YXR1cz48c2FtbHA6U3RhdHVzQ29kZSBW
YWx1ZT0idXJuOm9hc2lzOm5hbWVzOnRjOlNBTUw6Mi4wOnN0YXR1czpTdWNjZXNzIj48L3NhbWxw
OlN0YXR1c0NvZGU+PC9zYW1scDpTdGF0dXM+PHNhbWw6QXNzZXJ0aW9uIElEPSJBc3NlcnRpb24t
dXVpZDM2NjdiYjUtMDE4Mi0xMGY1LWIwYjQtZmYzZGFiNzRiMGU1IiBJc3N1ZUluc3RhbnQ9IjIw
MjItMDctMTVUMTk6NDc6MDRaIiBWZXJzaW9uPSIyLjAiPjxzYW1sOklzc3VlciBGb3JtYXQ9InVy
bjpvYXNpczpuYW1lczp0YzpTQU1MOjIuMDpuYW1laWQtZm9ybWF0OmVudGl0eSI+aHR0cHM6Ly90
ZXN0LXNlY3VyZWFjY2Vzcy53YS5nb3YvRklNMi9zcHMvc2F3aWRwL3NhbWwyMDwvc2FtbDpJc3N1
ZXI+PGRzOlNpZ25hdHVyZSB4bWxuczpkcz0iaHR0cDovL3d3dy53My5vcmcvMjAwMC8wOS94bWxk
c2lnIyIgSWQ9InV1aWQzNjY3YmI4LTAxODItMTQyZS1iZThjLWZmM2RhYjc0YjBlNSI+PGRzOlNp
Z25lZEluZm8+PGRzOkNhbm9uaWNhbGl6YXRpb25NZXRob2QgQWxnb3JpdGhtPSJodHRwOi8vd3d3
LnczLm9yZy8yMDAxLzEwL3htbC1leGMtYzE0biMiPjwvZHM6Q2Fub25pY2FsaXphdGlvbk1ldGhv
ZD48ZHM6U2lnbmF0dXJlTWV0aG9kIEFsZ29yaXRobT0iaHR0cDovL3d3dy53My5vcmcvMjAwMS8w
NC94bWxkc2lnLW1vcmUjcnNhLXNoYTI1NiI+PC9kczpTaWduYXR1cmVNZXRob2Q+PGRzOlJlZmVy
ZW5jZSBVUkk9IiNBc3NlcnRpb24tdXVpZDM2NjdiYjUtMDE4Mi0xMGY1LWIwYjQtZmYzZGFiNzRi
MGU1Ij48ZHM6VHJhbnNmb3Jtcz48ZHM6VHJhbnNmb3JtIEFsZ29yaXRobT0iaHR0cDovL3d3dy53
My5vcmcvMjAwMC8wOS94bWxkc2lnI2VudmVsb3BlZC1zaWduYXR1cmUiPjwvZHM6VHJhbnNmb3Jt
PjxkczpUcmFuc2Zvcm0gQWxnb3JpdGhtPSJodHRwOi8vd3d3LnczLm9yZy8yMDAxLzEwL3htbC1l
eGMtYzE0biMiPjx4YzE0bjpJbmNsdXNpdmVOYW1lc3BhY2VzIHhtbG5zOnhjMTRuPSJodHRwOi8v
d3d3LnczLm9yZy8yMDAxLzEwL3htbC1leGMtYzE0biMiIFByZWZpeExpc3Q9InNhbWwgeHMgeHNp
Ij48L3hjMTRuOkluY2x1c2l2ZU5hbWVzcGFjZXM+PC9kczpUcmFuc2Zvcm0+PC9kczpUcmFuc2Zv
cm1zPjxkczpEaWdlc3RNZXRob2QgQWxnb3JpdGhtPSJodHRwOi8vd3d3LnczLm9yZy8yMDAxLzA0
L3htbGVuYyNzaGEyNTYiPjwvZHM6RGlnZXN0TWV0aG9kPjxkczpEaWdlc3RWYWx1ZT5jZjF3Qnhn
VTBQRG5xdnZLKzh2Z1NobG5jNzAwK211UUJPRmdvS2FyMHJRPTwvZHM6RGlnZXN0VmFsdWU+PC9k
czpSZWZlcmVuY2U+PC9kczpTaWduZWRJbmZvPjxkczpTaWduYXR1cmVWYWx1ZT5NbG9scWM5RTgr
WHFaRExpNVJLYlRETkFUUmFya2FNSjVVbWVWV1gzVDM2SDE5ckJReVVHZG01Q2xLbFY4T3Zad0Jp
RVlRVlBnYWkxRXlxWTc1ZDRxbnQwZHcycjFsWWJLTmZzU2dGWjZjdUZoZkU4akJSMndqdEJsRGlk
cTdmSmY4dFhFL3U5MEtpdU01WUQvWUpmQ1VqQlNtOWFDWHNXSTNQdnRTODAzTXVpNnJqUU9ma2hm
dVJhNldab3RMR0J6VWU3T1hPVWFDR3FvQmZtT3A3SzhYYy9KNmJ2d212SFgxMTYxZmkydWpsRE4w
UEJHamxvYnEwMGFIVGoveUNBdzFNWnZJMzh0bFhxRVBPdHAraGxLbzQwWkNPeTZ3VGhjcGxJK29I
cXNMK09yUEhmMkxTWmVvcnVIeWxaMEEybUVVdWliSnNrazliRERGcmhPa2FId1E9PTwvZHM6U2ln
bmF0dXJlVmFsdWU+PGRzOktleUluZm8+PGRzOlg1MDlEYXRhPjxkczpYNTA5Q2VydGlmaWNhdGU+
TUlJRzBqQ0NCYnFnQXdJQkFnSVFHME4vQmZiR2NSQ3lldnFXbzhMd1hUQU5CZ2txaGtpRzl3MEJB
UXNGQURDQnVqRUxNQWtHQTFVRUJoTUNWVk14RmpBVUJnTlZCQW9URFVWdWRISjFjM1FzSUVsdVl5
NHhLREFtQmdOVkJBc1RIMU5sWlNCM2QzY3VaVzUwY25WemRDNXVaWFF2YkdWbllXd3RkR1Z5YlhN
eE9UQTNCZ05WQkFzVE1DaGpLU0F5TURFeUlFVnVkSEoxYzNRc0lFbHVZeTRnTFNCbWIzSWdZWFYw
YUc5eWFYcGxaQ0IxYzJVZ2IyNXNlVEV1TUN3R0ExVUVBeE1sUlc1MGNuVnpkQ0JEWlhKMGFXWnBZ
MkYwYVc5dUlFRjFkR2h2Y21sMGVTQXRJRXd4U3pBZUZ3MHlNakExTVRFeE5qQTJOVGxhRncweU16
QTJNRFl4TmpBMk5UbGFNSFV4Q3pBSkJnTlZCQVlUQWxWVE1STXdFUVlEVlFRSUV3cFhZWE5vYVc1
bmRHOXVNUkF3RGdZRFZRUUhFd2RQYkhsdGNHbGhNUnd3R2dZRFZRUUtFeE5UZEdGMFpTQnZaaUJY
WVhOb2FXNW5kRzl1TVNFd0h3WURWUVFERXhoMFpYTjBMWE5sWTNWeVpXRmpZMlZ6Y3k1M1lTNW5i
M1l3Z2dFaU1BMEdDU3FHU0liM0RRRUJBUVVBQTRJQkR3QXdnZ0VLQW9JQkFRQzl6QU5xQWszQ0RD
clQ0bWpDR2xGb3g1OU5xL1JsVDV2OGVPcnZYdURaK2E1US9FdkszbW1lMUpSckJMM2trczhwWTRD
amd0dXNEbUExU1F0c0Vuc0RzYUlrZTZVVW43TXU3VktEZXdVTHZKSXhsREpGbDFlTlk3RFV3bytD
cUZYT0cyMzBtUm9lNko5MjN2ZXBzelhuWDlVRWZGcFEyV3BUL3JOMU1xUThFY1ZabW1xSitpZXJY
SjllODIxUGlpcHRaakpUSHdTc2oxcjRtZVVFd2tZSzBmOG1SNTVsdDlTUEg5UWltZFdsaloyZmZQ
S1pEM0FoVnNQbFB4TFE1V3JXd1dEM2lpbitSQ3d2bCtTZXVWRVdTc2F6aklzSGRzdHZJTEkrMmhE
b0dMem5FcnJPZE9ncFZicWd0SUQ1eFhDTnhLcWlqZDdKY1hYTzIvZi9MWDZ6QWdNQkFBR2pnZ01X
TUlJREVqQU1CZ05WSFJNQkFmOEVBakFBTUIwR0ExVWREZ1FXQkJRaUdiakw5MldjdXl2NnFIOEdw
OHJKenIzQXZqQWZCZ05WSFNNRUdEQVdnQlNDb25CMDNieFRQODk3MVBmTmY2ZGd4Z3BNdnpCb0Jn
Z3JCZ0VGQlFjQkFRUmNNRm93SXdZSUt3WUJCUVVITUFHR0YyaDBkSEE2THk5dlkzTndMbVZ1ZEhK
MWMzUXVibVYwTURNR0NDc0dBUVVGQnpBQ2hpZG9kSFJ3T2k4dllXbGhMbVZ1ZEhKMWMzUXVibVYw
TDJ3eGF5MWphR0ZwYmpJMU5pNWpaWEl3TXdZRFZSMGZCQ3d3S2pBb29DYWdKSVlpYUhSMGNEb3ZM
Mk55YkM1bGJuUnlkWE4wTG01bGRDOXNaWFpsYkRGckxtTnliREFqQmdOVkhSRUVIREFhZ2hoMFpY
TjBMWE5sWTNWeVpXRmpZMlZ6Y3k1M1lTNW5iM1l3RGdZRFZSMFBBUUgvQkFRREFnV2dNQjBHQTFV
ZEpRUVdNQlFHQ0NzR0FRVUZCd01CQmdnckJnRUZCUWNEQWpCTUJnTlZIU0FFUlRCRE1EY0dDbUNH
U0FHRyttd0tBUVV3S1RBbkJnZ3JCZ0VGQlFjQ0FSWWJhSFIwY0hNNkx5OTNkM2N1Wlc1MGNuVnpk
QzV1WlhRdmNuQmhNQWdHQm1lQkRBRUNBakNDQVg4R0Npc0dBUVFCMW5rQ0JBSUVnZ0Z2QklJQmF3
RnBBSGNBVllIVXdoYVFOZ0ZLNmd1YlZ6eFQ4TURrT0hod0pRZ1hMNk9xSFFjVDB3d0FBQUdBczkr
bjh3QUFCQU1BU0RCR0FpRUEyQmlxQXVCYUVzc3Y0Q0VZSnRkdThpQVlCemVqdnI1cTJ0VnhLSUJw
dm1zQ0lRRFRmV2JMTE5HYXhWdW15b0FOVjBSOUZMd0RyYzgyaFIvbytZdW03bVJzVndCMkFMTnpk
d2ZoaEZENFk0YldCYW5jRVFsS2VTMnhad3dMaDl6d0F3NTVOcVdhQUFBQmdMUGZwL1lBQUFRREFF
Y3dSUUlnV3VxNmQ0Z2w3R093ME9xUXlFTVQ1RkFQVElNKzRqa1FRMFg1K29OejRBZ0NJUUNzbVlk
ZzVLTC84YzM3MWkxOTRobXFtWS9vL0p2VnYwTE5WWjhrb0Rua3V3QjJBT2crME5vKzlRWTFNdWRY
S0x5SmE4a0QwOHZSRVd2czYybmhkMzF0QnIxdUFBQUJnTFBmcUUwQUFBUURBRWN3UlFJaEFKN0Jy
R0hWNlJ5cGdJZjV0cldzcHd2eVdRSzJ5K2g2MGZDcVZmNmFSZUVpQWlBZ2QwVE1hYkpLR1JJYisy
ajJOS1BGaklTV0NpcFZGM0QydkFtZEg4MDZjVEFOQmdrcWhraUc5dzBCQVFzRkFBT0NBUUVBd2VO
M0JZZGFXbi9BTXNlemp2UEovcjRDbi9mU012OUFNZ1NtWGJhQURkUENOSEFmc0NpemNmTEJmcEtV
MWxKWlE5bWhaVk9nU1lLL2Z0WmRqOEJFaTk4ZTBYQStzMlJTaE91L3JtK1hxWXhmajR3eXZMY2Zm
ZjVkM0gwSnJ3NHh6RUQyV0pWYk45Vkk3eGZ2Ym5zTjd3L0o2MXY3NEtKTXh6NjJVOE0yRGEyMHN2
Y3BsRDBiV1VmVFI2TGZ1T3hIZGVvV0pDdXZPbzJZSmRxMGdYYjVPUlR6NGtMb3JGeGkwbUdHWjJY
Yldza0JYMzNlbklJYmxLLzlNcWpOaUFXenhUcEFPVnNIWkE0U3VmY1JMUDBaNG9XaWhYQ0VSZ1l0
ampsZlI5ZWJFMjFXbTVmRmQxMm5Rb3pWZ2FMTnBWazd4ZXdTSENpdm1aOURnR0pUQkExbG5nPT08
L2RzOlg1MDlDZXJ0aWZpY2F0ZT48L2RzOlg1MDlEYXRhPjwvZHM6S2V5SW5mbz48L2RzOlNpZ25h
dHVyZT48c2FtbDpTdWJqZWN0PjxzYW1sOk5hbWVJRCBGb3JtYXQ9InVybjpvYXNpczpuYW1lczp0
YzpTQU1MOjEuMTpuYW1laWQtZm9ybWF0OmVtYWlsQWRkcmVzcyI+RFA0VkQ0WlA4UVE2TC0xUEYw
WlYyRkY0LUQxTFc0VlowRkQtRFc5UVc4Wkw5UTwvc2FtbDpOYW1lSUQ+PHNhbWw6U3ViamVjdENv
bmZpcm1hdGlvbiBNZXRob2Q9InVybjpvYXNpczpuYW1lczp0YzpTQU1MOjIuMDpjbTpiZWFyZXIi
PjxzYW1sOlN1YmplY3RDb25maXJtYXRpb25EYXRhIE5vdE9uT3JBZnRlcj0iMjAyMi0wNy0xNVQx
OTo0ODowNFoiIFJlY2lwaWVudD0iaHR0cHM6Ly93YWRucmZvcmVzdGhlYWx0aC5sb2NhbGhvc3Qu
cHJvamVjdGZpcm1hLmNvbS9BY2NvdW50L1NBV1Bvc3QiPjwvc2FtbDpTdWJqZWN0Q29uZmlybWF0
aW9uRGF0YT48L3NhbWw6U3ViamVjdENvbmZpcm1hdGlvbj48L3NhbWw6U3ViamVjdD48c2FtbDpD
b25kaXRpb25zIE5vdEJlZm9yZT0iMjAyMi0wNy0xNVQxOTo0NjowNFoiIE5vdE9uT3JBZnRlcj0i
MjAyMi0wNy0xNVQxOTo0ODowNFoiPjxzYW1sOkF1ZGllbmNlUmVzdHJpY3Rpb24+PHNhbWw6QXVk
aWVuY2U+aHR0cHM6Ly93YWRucmZvcmVzdGhlYWx0aC5sb2NhbGhvc3QucHJvamVjdGZpcm1hLmNv
bTwvc2FtbDpBdWRpZW5jZT48L3NhbWw6QXVkaWVuY2VSZXN0cmljdGlvbj48L3NhbWw6Q29uZGl0
aW9ucz48c2FtbDpBdXRoblN0YXRlbWVudCBBdXRobkluc3RhbnQ9IjIwMjItMDctMTVUMTk6NDc6
MDRaIiBTZXNzaW9uSW5kZXg9InV1aWQ4OTBiM2VhOS0yYjliLTRiNDEtYWM5NS1hNTNjYzhmZWVm
YzYiIFNlc3Npb25Ob3RPbk9yQWZ0ZXI9IjIwMjItMDctMTVUMjA6NDc6MDRaIj48c2FtbDpBdXRo
bkNvbnRleHQ+PHNhbWw6QXV0aG5Db250ZXh0Q2xhc3NSZWY+dXJuOm9hc2lzOm5hbWVzOnRjOlNB
TUw6Mi4wOmFjOmNsYXNzZXM6UGFzc3dvcmQ8L3NhbWw6QXV0aG5Db250ZXh0Q2xhc3NSZWY+PC9z
YW1sOkF1dGhuQ29udGV4dD48L3NhbWw6QXV0aG5TdGF0ZW1lbnQ+PHNhbWw6QXR0cmlidXRlU3Rh
dGVtZW50PjxzYW1sOkF0dHJpYnV0ZSBOYW1lPSJuYW1lIiBOYW1lRm9ybWF0PSJ1cm46b2FzaXM6
bmFtZXM6dGM6U0FNTDoyLjA6YXNzZXJ0aW9uIj48c2FtbDpBdHRyaWJ1dGVWYWx1ZSB4c2k6dHlw
ZT0ieHM6c3RyaW5nIj5NaWNoYWVsIEZlcnJhbnRlPC9zYW1sOkF0dHJpYnV0ZVZhbHVlPjwvc2Ft
bDpBdHRyaWJ1dGU+PHNhbWw6QXR0cmlidXRlIE5hbWU9ImxldmVsIiBOYW1lRm9ybWF0PSJ1cm46
b2FzaXM6bmFtZXM6dGM6U0FNTDoyLjA6YXNzZXJ0aW9uIj48c2FtbDpBdHRyaWJ1dGVWYWx1ZSB4
c2k6dHlwZT0ieHM6c3RyaW5nIj4xPC9zYW1sOkF0dHJpYnV0ZVZhbHVlPjwvc2FtbDpBdHRyaWJ1
dGU+PHNhbWw6QXR0cmlidXRlIE5hbWU9Imdyb3VwcyIgTmFtZUZvcm1hdD0idXJuOm9hc2lzOm5h
bWVzOnRjOlNBTUw6Mi4wOmFzc2VydGlvbiI+PHNhbWw6QXR0cmlidXRlVmFsdWUgeHNpOnR5cGU9
InhzOnN0cmluZyI+ZjNfdXNlcnM8L3NhbWw6QXR0cmlidXRlVmFsdWU+PHNhbWw6QXR0cmlidXRl
VmFsdWUgeHNpOnR5cGU9InhzOnN0cmluZyI+RE5SLUZvcmVzdEhlYWx0aFRyYWNrZXJRQS1VU0VS
LVJFRzwvc2FtbDpBdHRyaWJ1dGVWYWx1ZT48c2FtbDpBdHRyaWJ1dGVWYWx1ZSB4c2k6dHlwZT0i
eHM6c3RyaW5nIj5ETlItRm9yZXN0SGVhbHRoVHJhY2tlclFBLURFRkFVTFRfVU1HLVVTRVItUkVH
PC9zYW1sOkF0dHJpYnV0ZVZhbHVlPjwvc2FtbDpBdHRyaWJ1dGU+PHNhbWw6QXR0cmlidXRlIE5h
bWU9InVzZXIiIE5hbWVGb3JtYXQ9InVybjpvYXNpczpuYW1lczp0YzpTQU1MOjIuMDphc3NlcnRp
b24iPjxzYW1sOkF0dHJpYnV0ZVZhbHVlIHhzaTp0eXBlPSJ4czpzdHJpbmciPm1pY2hhZWxAc2l0
a2F0ZWNoLmNvbTwvc2FtbDpBdHRyaWJ1dGVWYWx1ZT48L3NhbWw6QXR0cmlidXRlPjxzYW1sOkF0
dHJpYnV0ZSBOYW1lPSJlbWFpbCIgTmFtZUZvcm1hdD0idXJuOm9hc2lzOm5hbWVzOnRjOlNBTUw6
Mi4wOmFzc2VydGlvbiI+PHNhbWw6QXR0cmlidXRlVmFsdWUgeHNpOnR5cGU9InhzOnN0cmluZyI+
bWljaGFlbEBzaXRrYXRlY2guY29tPC9zYW1sOkF0dHJpYnV0ZVZhbHVlPjwvc2FtbDpBdHRyaWJ1
dGU+PHNhbWw6QXR0cmlidXRlIE5hbWU9Imd1aWQiIE5hbWVGb3JtYXQ9InVybjpvYXNpczpuYW1l
czp0YzpTQU1MOjIuMDphc3NlcnRpb24iPjxzYW1sOkF0dHJpYnV0ZVZhbHVlIHhzaTp0eXBlPSJ4
czpzdHJpbmciPkRQNFZENFpQOFFRNkwtMVBGMFpWMkZGNC1EMUxXNFZaMEZELURXOVFXOFpMOVE8
L3NhbWw6QXR0cmlidXRlVmFsdWU+PC9zYW1sOkF0dHJpYnV0ZT48L3NhbWw6QXR0cmlidXRlU3Rh
dGVtZW50Pjwvc2FtbDpBc3NlcnRpb24+PC9zYW1scDpSZXNwb25zZT4=";

        [Test]
        [Description("For this test we want the base64 and sample pretty printed to be the same so we can debug the tests more easily")]
        public void SampleBase64AndPrettyPrintAreTheSame()
        {
            var sawSamlResponse = SawSamlResponse.CreateFromBase64String(_sampleSawSamlBase64Encoded);
            Assert.That(sawSamlResponse.GetSamlAsPrettyPrintXml(), Is.EqualTo(_sampleSawSamlPrettyPrinted));

        }

        [Test]
        public void TestSawSamlResponseDateValidity()
        {
            var sawSamlResponse = SawSamlResponse.CreateFromString(_sampleSawSamlPrettyPrinted);
            var testDateTime = DateTimeOffset.Parse("2022-07-15T19:46:04Z");
            Assert.That(sawSamlResponse.IsResponseStillWithinValidTimePeriod(testDateTime), Is.True, "Should be valid date");
            Assert.That(sawSamlResponse.IsResponseStillWithinValidTimePeriod(testDateTime.AddMinutes(5)), Is.False, "Should be invalid date too old");
        }

        [Test]
        public void FieldParsingShouldBeAccurate()
        {
            var sawSamlResponse = SawSamlResponse.CreateFromString(_sampleSawSamlPrettyPrinted);
            Assert.That(sawSamlResponse.GetEmail(), Is.EqualTo("michael@sitkatech.com"));
            Assert.That(sawSamlResponse.GetFirstName(), Is.EqualTo("Michael"));
            Assert.That(sawSamlResponse.GetIssuer(), Is.EqualTo("https://test-secureaccess.wa.gov/FIM2/sps/sawidp/saml20"));
            Assert.That(sawSamlResponse.GetLastName(), Is.EqualTo("Ferrante"));
            Assert.That(sawSamlResponse.GetWhichSawAuthenticator().AuthenticatorFullName, Is.EqualTo(Authenticator.SAWTEST.AuthenticatorFullName));
            Assert.That(sawSamlResponse.GetRoleGroups(), Is.EquivalentTo(new List<string> { "f3_users", "DNR-ForestHealthTrackerQA-USER-REG", "DNR-ForestHealthTrackerQA-DEFAULT_UMG-USER-REG" }));

            var sawSamlResponse1 = SawSamlResponse.CreateFromBase64String(_sampleSawSamlBase64Encoded);
            Assert.That(sawSamlResponse1.GetEmail(), Is.EqualTo("michael@sitkatech.com"));
            Assert.That(sawSamlResponse1.GetFirstName(), Is.EqualTo("Michael"));
            Assert.That(sawSamlResponse1.GetIssuer(), Is.EqualTo("https://test-secureaccess.wa.gov/FIM2/sps/sawidp/saml20"));
            Assert.That(sawSamlResponse1.GetLastName(), Is.EqualTo("Ferrante"));
            Assert.That(sawSamlResponse1.GetWhichSawAuthenticator().AuthenticatorFullName, Is.EqualTo(Authenticator.SAWTEST.AuthenticatorFullName));
            Assert.That(sawSamlResponse1.GetRoleGroups(), Is.EquivalentTo(new List<string> { "f3_users", "DNR-ForestHealthTrackerQA-USER-REG", "DNR-ForestHealthTrackerQA-DEFAULT_UMG-USER-REG" }));
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