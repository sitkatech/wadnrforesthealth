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
        /// To update these, log in locally and the AccountController logs the saw saml response, so you can copy from the log file
        /// </summary>
        private string _sampleSawSamlPrettyPrinted = @"<samlp:Response xmlns:saml=""urn:oasis:names:tc:SAML:2.0:assertion"" xmlns:samlp=""urn:oasis:names:tc:SAML:2.0:protocol"" xmlns:xs=""http://www.w3.org/2001/XMLSchema"" xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" Destination=""https://wadnrforesthealth.localhost.projectfirma.com/Account/SAWPost"" ID=""FIMRSP_71ebe744-018a-10d2-bba6-c8f1a2834af9"" IssueInstant=""2023-09-07T23:13:20Z"" Version=""2.0"">
  <saml:Issuer Format=""urn:oasis:names:tc:SAML:2.0:nameid-format:entity"">https://test-secureaccess.wa.gov/FIM2/sps/sawidp/saml20</saml:Issuer>
  <samlp:Status>
    <samlp:StatusCode Value=""urn:oasis:names:tc:SAML:2.0:status:Success"">
    </samlp:StatusCode>
  </samlp:Status>
  <saml:Assertion ID=""Assertion-uuid71ebe735-018a-1e50-a12d-c8f1a2834af9"" IssueInstant=""2023-09-07T23:13:20Z"" Version=""2.0"">
    <saml:Issuer Format=""urn:oasis:names:tc:SAML:2.0:nameid-format:entity"">https://test-secureaccess.wa.gov/FIM2/sps/sawidp/saml20</saml:Issuer>
    <ds:Signature xmlns:ds=""http://www.w3.org/2000/09/xmldsig#"" Id=""uuid71ebe738-018a-1189-a8a6-c8f1a2834af9"">
      <ds:SignedInfo>
        <ds:CanonicalizationMethod Algorithm=""http://www.w3.org/2001/10/xml-exc-c14n#"">
        </ds:CanonicalizationMethod>
        <ds:SignatureMethod Algorithm=""http://www.w3.org/2001/04/xmldsig-more#rsa-sha256"">
        </ds:SignatureMethod>
        <ds:Reference URI=""#Assertion-uuid71ebe735-018a-1e50-a12d-c8f1a2834af9"">
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
          <ds:DigestValue>fxXO0IIYXfyVcP/eIlmQbRvJFe5QibgcgSW18luyhYc=</ds:DigestValue>
        </ds:Reference>
      </ds:SignedInfo>
      <ds:SignatureValue>ZizZRpLWZS7TaIVHWMWdSpPq+QKzrsBXakPHApUIoXAyRw/NEHiLLgIgD7Eji1VPNkejQNuIWqRG/c5Xd7DZXw2+L4CLxjTqMS5QhKt0dlNpwl+GO65EktlsKSsMZwiTM+yUjl36dHXuDHCcSjSff0ta7wEKw4wpqEpJsCIc1U/aX64On8E/pTVwjp0sI3q8e9wCgERnFytUrJWBz8x/1dUvA34tVNUQB+JzH0vp8jOvLd7b9ojuRA2rew5s6fJvWDB+OqEk3FhR/QWIjbDzjSCQZwwQnXFDfyHW16aa8HyulV50ts/j3flk3DwgLaqzpCjuD01xqFR9n/TC2ZO28g==</ds:SignatureValue>
      <ds:KeyInfo>
        <ds:X509Data>
          <ds:X509Certificate>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</ds:X509Certificate>
        </ds:X509Data>
      </ds:KeyInfo>
    </ds:Signature>
    <saml:Subject>
      <saml:NameID Format=""urn:oasis:names:tc:SAML:1.1:nameid-format:emailAddress"">DP4MT6TV3ZT7M-3QT5ZL6DM-DD7WV4ZZ8D-1FZ3DD4VZ4</saml:NameID>
      <saml:SubjectConfirmation Method=""urn:oasis:names:tc:SAML:2.0:cm:bearer"">
        <saml:SubjectConfirmationData NotOnOrAfter=""2023-09-07T23:14:20Z"" Recipient=""https://wadnrforesthealth.localhost.projectfirma.com/Account/SAWPost"">
        </saml:SubjectConfirmationData>
      </saml:SubjectConfirmation>
    </saml:Subject>
    <saml:Conditions NotBefore=""2023-09-07T23:12:20Z"" NotOnOrAfter=""2023-09-07T23:14:20Z"">
      <saml:AudienceRestriction>
        <saml:Audience>https://wadnrforesthealth.localhost.projectfirma.com</saml:Audience>
      </saml:AudienceRestriction>
    </saml:Conditions>
    <saml:AuthnStatement AuthnInstant=""2023-09-07T23:13:20Z"" SessionIndex=""uuid17e40c50-7cda-4f76-9350-035c00bd34e1"" SessionNotOnOrAfter=""2023-09-08T00:13:20Z"">
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
b3VudC9TQVdQb3N0IiBJRD0iRklNUlNQXzcxZWJlNzQ0LTAxOGEtMTBkMi1iYmE2LWM4ZjFhMjgz
NGFmOSIgSXNzdWVJbnN0YW50PSIyMDIzLTA5LTA3VDIzOjEzOjIwWiIgVmVyc2lvbj0iMi4wIj48
c2FtbDpJc3N1ZXIgRm9ybWF0PSJ1cm46b2FzaXM6bmFtZXM6dGM6U0FNTDoyLjA6bmFtZWlkLWZv
cm1hdDplbnRpdHkiPmh0dHBzOi8vdGVzdC1zZWN1cmVhY2Nlc3Mud2EuZ292L0ZJTTIvc3BzL3Nh
d2lkcC9zYW1sMjA8L3NhbWw6SXNzdWVyPjxzYW1scDpTdGF0dXM+PHNhbWxwOlN0YXR1c0NvZGUg
VmFsdWU9InVybjpvYXNpczpuYW1lczp0YzpTQU1MOjIuMDpzdGF0dXM6U3VjY2VzcyI+PC9zYW1s
cDpTdGF0dXNDb2RlPjwvc2FtbHA6U3RhdHVzPjxzYW1sOkFzc2VydGlvbiBJRD0iQXNzZXJ0aW9u
LXV1aWQ3MWViZTczNS0wMThhLTFlNTAtYTEyZC1jOGYxYTI4MzRhZjkiIElzc3VlSW5zdGFudD0i
MjAyMy0wOS0wN1QyMzoxMzoyMFoiIFZlcnNpb249IjIuMCI+PHNhbWw6SXNzdWVyIEZvcm1hdD0i
dXJuOm9hc2lzOm5hbWVzOnRjOlNBTUw6Mi4wOm5hbWVpZC1mb3JtYXQ6ZW50aXR5Ij5odHRwczov
L3Rlc3Qtc2VjdXJlYWNjZXNzLndhLmdvdi9GSU0yL3Nwcy9zYXdpZHAvc2FtbDIwPC9zYW1sOklz
c3Vlcj48ZHM6U2lnbmF0dXJlIHhtbG5zOmRzPSJodHRwOi8vd3d3LnczLm9yZy8yMDAwLzA5L3ht
bGRzaWcjIiBJZD0idXVpZDcxZWJlNzM4LTAxOGEtMTE4OS1hOGE2LWM4ZjFhMjgzNGFmOSI+PGRz
OlNpZ25lZEluZm8+PGRzOkNhbm9uaWNhbGl6YXRpb25NZXRob2QgQWxnb3JpdGhtPSJodHRwOi8v
d3d3LnczLm9yZy8yMDAxLzEwL3htbC1leGMtYzE0biMiPjwvZHM6Q2Fub25pY2FsaXphdGlvbk1l
dGhvZD48ZHM6U2lnbmF0dXJlTWV0aG9kIEFsZ29yaXRobT0iaHR0cDovL3d3dy53My5vcmcvMjAw
MS8wNC94bWxkc2lnLW1vcmUjcnNhLXNoYTI1NiI+PC9kczpTaWduYXR1cmVNZXRob2Q+PGRzOlJl
ZmVyZW5jZSBVUkk9IiNBc3NlcnRpb24tdXVpZDcxZWJlNzM1LTAxOGEtMWU1MC1hMTJkLWM4ZjFh
MjgzNGFmOSI+PGRzOlRyYW5zZm9ybXM+PGRzOlRyYW5zZm9ybSBBbGdvcml0aG09Imh0dHA6Ly93
d3cudzMub3JnLzIwMDAvMDkveG1sZHNpZyNlbnZlbG9wZWQtc2lnbmF0dXJlIj48L2RzOlRyYW5z
Zm9ybT48ZHM6VHJhbnNmb3JtIEFsZ29yaXRobT0iaHR0cDovL3d3dy53My5vcmcvMjAwMS8xMC94
bWwtZXhjLWMxNG4jIj48eGMxNG46SW5jbHVzaXZlTmFtZXNwYWNlcyB4bWxuczp4YzE0bj0iaHR0
cDovL3d3dy53My5vcmcvMjAwMS8xMC94bWwtZXhjLWMxNG4jIiBQcmVmaXhMaXN0PSJzYW1sIHhz
IHhzaSI+PC94YzE0bjpJbmNsdXNpdmVOYW1lc3BhY2VzPjwvZHM6VHJhbnNmb3JtPjwvZHM6VHJh
bnNmb3Jtcz48ZHM6RGlnZXN0TWV0aG9kIEFsZ29yaXRobT0iaHR0cDovL3d3dy53My5vcmcvMjAw
MS8wNC94bWxlbmMjc2hhMjU2Ij48L2RzOkRpZ2VzdE1ldGhvZD48ZHM6RGlnZXN0VmFsdWU+ZnhY
TzBJSVlYZnlWY1AvZUlsbVFiUnZKRmU1UWliZ2NnU1cxOGx1eWhZYz08L2RzOkRpZ2VzdFZhbHVl
PjwvZHM6UmVmZXJlbmNlPjwvZHM6U2lnbmVkSW5mbz48ZHM6U2lnbmF0dXJlVmFsdWU+Wml6WlJw
TFdaUzdUYUlWSFdNV2RTcFBxK1FLenJzQlhha1BIQXBVSW9YQXlSdy9ORUhpTExnSWdEN0VqaTFW
UE5rZWpRTnVJV3FSRy9jNVhkN0RaWHcyK0w0Q0x4alRxTVM1UWhLdDBkbE5wd2wrR082NUVrdGxz
S1NzTVp3aVRNK3lVamwzNmRIWHVESENjU2pTZmYwdGE3d0VLdzR3cHFFcEpzQ0ljMVUvYVg2NE9u
OEUvcFRWd2pwMHNJM3E4ZTl3Q2dFUm5GeXRVckpXQno4eC8xZFV2QTM0dFZOVVFCK0p6SDB2cDhq
T3ZMZDdiOW9qdVJBMnJldzVzNmZKdldEQitPcUVrM0ZoUi9RV0lqYkR6alNDUVp3d1FuWEZEZnlI
VzE2YWE4SHl1bFY1MHRzL2ozZmxrM0R3Z0xhcXpwQ2p1RDAxeHFGUjluL1RDMlpPMjhnPT08L2Rz
OlNpZ25hdHVyZVZhbHVlPjxkczpLZXlJbmZvPjxkczpYNTA5RGF0YT48ZHM6WDUwOUNlcnRpZmlj
YXRlPk1JSUcwakNDQmJxZ0F3SUJBZ0lRWWpmVERjcDNwaTU5djJZa1YzQVd1VEFOQmdrcWhraUc5
dzBCQVFzRkFEQ0J1akVMTUFrR0ExVUVCaE1DVlZNeEZqQVVCZ05WQkFvVERVVnVkSEoxYzNRc0lF
bHVZeTR4S0RBbUJnTlZCQXNUSDFObFpTQjNkM2N1Wlc1MGNuVnpkQzV1WlhRdmJHVm5ZV3d0ZEdW
eWJYTXhPVEEzQmdOVkJBc1RNQ2hqS1NBeU1ERXlJRVZ1ZEhKMWMzUXNJRWx1WXk0Z0xTQm1iM0ln
WVhWMGFHOXlhWHBsWkNCMWMyVWdiMjVzZVRFdU1Dd0dBMVVFQXhNbFJXNTBjblZ6ZENCRFpYSjBh
V1pwWTJGMGFXOXVJRUYxZEdodmNtbDBlU0F0SUV3eFN6QWVGdzB5TXpBMU1Ea3hPRFU1TWpCYUZ3
MHlOREEyTURNeE9EVTVNakJhTUhVeEN6QUpCZ05WQkFZVEFsVlRNUk13RVFZRFZRUUlFd3BYWVhO
b2FXNW5kRzl1TVJBd0RnWURWUVFIRXdkUGJIbHRjR2xoTVJ3d0dnWURWUVFLRXhOVGRHRjBaU0J2
WmlCWFlYTm9hVzVuZEc5dU1TRXdId1lEVlFRREV4aDBaWE4wTFhObFkzVnlaV0ZqWTJWemN5NTNZ
UzVuYjNZd2dnRWlNQTBHQ1NxR1NJYjNEUUVCQVFVQUE0SUJEd0F3Z2dFS0FvSUJBUUNVemVLczUz
WnN2ZHZtSXRodmlOTkFmU1BPQjNoekY1YitoWDNXLzhjbHdmZGwvOUFJejE1cXJnWTZTUkhsM1Z4
c1RoT05SNGdZVzdhQmdXcUZleVpyTzVkSU9sU0tyVnBKeW95VUxIYllmWHY4MXBMcjFPKzBtMTE1
dlc5ZlBJRlQ5Z0JRalRpbHY1T05SZ2gyclFmV0ppWjg5VWtCeE11bVN0U3EzVWdISCtMQ0xBYU5S
ZTlISXdaci9QcnRGaWdjeGhDc29pdzI4alBKaDRqb2NXUGVkeFVwOTFSZlZEL2tJaFViK0dlY0Nt
RGFWUFU3QkE1UVV5VmNLRndUZ3lQTlRycFZWVS9sMTBUMzJ3WlRiSXNZdElJcXVMbEJTSC9mbldq
TElsOXZJdTh3ck1GOHdScVExaWFCTUp0WHk3T0xnRVZqdFNuT1BqUEEvNGtzcFVpWEFnTUJBQUdq
Z2dNV01JSURFakFNQmdOVkhSTUJBZjhFQWpBQU1CMEdBMVVkRGdRV0JCVEhwV1VFdVltY2JWaEp5
WGhnU0hqQ1diOFZsekFmQmdOVkhTTUVHREFXZ0JTQ29uQjAzYnhUUDg5NzFQZk5mNmRneGdwTXZ6
Qm9CZ2dyQmdFRkJRY0JBUVJjTUZvd0l3WUlLd1lCQlFVSE1BR0dGMmgwZEhBNkx5OXZZM053TG1W
dWRISjFjM1F1Ym1WME1ETUdDQ3NHQVFVRkJ6QUNoaWRvZEhSd09pOHZZV2xoTG1WdWRISjFjM1F1
Ym1WMEwyd3hheTFqYUdGcGJqSTFOaTVqWlhJd013WURWUjBmQkN3d0tqQW9vQ2FnSklZaWFIUjBj
RG92TDJOeWJDNWxiblJ5ZFhOMExtNWxkQzlzWlhabGJERnJMbU55YkRBakJnTlZIUkVFSERBYWdo
aDBaWE4wTFhObFkzVnlaV0ZqWTJWemN5NTNZUzVuYjNZd0RnWURWUjBQQVFIL0JBUURBZ1dnTUIw
R0ExVWRKUVFXTUJRR0NDc0dBUVVGQndNQkJnZ3JCZ0VGQlFjREFqQk1CZ05WSFNBRVJUQkRNRGNH
Q21DR1NBR0crbXdLQVFVd0tUQW5CZ2dyQmdFRkJRY0NBUlliYUhSMGNITTZMeTkzZDNjdVpXNTBj
blZ6ZEM1dVpYUXZjbkJoTUFnR0JtZUJEQUVDQWpDQ0FYOEdDaXNHQVFRQjFua0NCQUlFZ2dGdkJJ
SUJhd0ZwQUhZQU8xTjNkVDR0dVlCT2l6QmJCdjVBTzJmWVQ4UDB4NzBBRFMxeWIrSDYxQmNBQUFH
SUFlSGhUQUFBQkFNQVJ6QkZBaUVBbGU1ei9jSmhlN0dMSGdlUWNmSGk3NFkxU283cytySnMwL0F3
TG9ZNkR5UUNJQ01ob0N1T2NpTnVmeWlpelh0T3FMWU4xNGg5dHY0b2NqbTkrek5DL3BjbkFIY0Ey
cmEvYXorMXRpS2ZtOEs3WEd2b2NKRnhiTHRSaElVMHZhUTlNRWpYKzZzQUFBR0lBZUhoV0FBQUJB
TUFTREJHQWlFQWp1ZHY4L2dFTTF3SFFlTEhCamtsd1daOFBjVGNNSUV3SFQxZ0JSdSt5bUFDSVFD
dWJrOUdnOWM3MEpNWGtBSERkdjZWNWlpVHFiQ201WE45anIvRno0bXpuQUIyQU83TjBHVFYyeHJP
eFZ5M25iVE5FNkl5aDBaOHZPemV3MUZJV1VaeEg3V2JBQUFCaUFIaDRYVUFBQVFEQUVjd1JRSWdL
eTV5MGR0Zm1reGFURjRKeWI1ZFZjdmxJeDRiWlNlMHJPOXAwa0dPcm1JQ0lRRDUvc0FzQ3AzdW5L
N1lxbGdvWmsyNWhLRGIzdWpKdkVYa2o5eEdPR2huTmpBTkJna3Foa2lHOXcwQkFRc0ZBQU9DQVFF
QWdaTkx1ditHOGI2ei80NHUvYVVnZk44RXBYQUtFZnBMdjlWTzY3VWF1djNUcTljUElmZHZtU2o1
N2cwMjhTNUg1Q1NuVEZYaVpRYVdERlFWYWlsNk5Kek80MFhkRjFBZnpONUN0U0RCWGJ4TW54eGM1
eSs1QmVGYlo0UVpIaHlla0JyWnUzOTREbmM5YWNoV3dWdXE2YkhVOXJuUlczMlN4aGdTYTdidXdV
SGV4SE52TEh0S2l1STlIdXJBMGQ0TlQ2dUhSZXJWUUJLTXdybzhXWlU2aVJMUEYyd1ByWkZYazNI
VktiMVVpM2hVNEFRS2JEcWR2YkFWUHBkdmRmZTMvNzBGeUhBMTkvdVFJU2NJTE1mcHM4dmg3Umha
ZnJqT3dPdmxvQmYyMW1RSGlKNmtudG1reDVWdlNxQ2QwOW5QTjU1UnYrbmJNRWRtT0ZkUTZyRkNJ
Zz09PC9kczpYNTA5Q2VydGlmaWNhdGU+PC9kczpYNTA5RGF0YT48L2RzOktleUluZm8+PC9kczpT
aWduYXR1cmU+PHNhbWw6U3ViamVjdD48c2FtbDpOYW1lSUQgRm9ybWF0PSJ1cm46b2FzaXM6bmFt
ZXM6dGM6U0FNTDoxLjE6bmFtZWlkLWZvcm1hdDplbWFpbEFkZHJlc3MiPkRQNE1UNlRWM1pUN00t
M1FUNVpMNkRNLUREN1dWNFpaOEQtMUZaM0RENFZaNDwvc2FtbDpOYW1lSUQ+PHNhbWw6U3ViamVj
dENvbmZpcm1hdGlvbiBNZXRob2Q9InVybjpvYXNpczpuYW1lczp0YzpTQU1MOjIuMDpjbTpiZWFy
ZXIiPjxzYW1sOlN1YmplY3RDb25maXJtYXRpb25EYXRhIE5vdE9uT3JBZnRlcj0iMjAyMy0wOS0w
N1QyMzoxNDoyMFoiIFJlY2lwaWVudD0iaHR0cHM6Ly93YWRucmZvcmVzdGhlYWx0aC5sb2NhbGhv
c3QucHJvamVjdGZpcm1hLmNvbS9BY2NvdW50L1NBV1Bvc3QiPjwvc2FtbDpTdWJqZWN0Q29uZmly
bWF0aW9uRGF0YT48L3NhbWw6U3ViamVjdENvbmZpcm1hdGlvbj48L3NhbWw6U3ViamVjdD48c2Ft
bDpDb25kaXRpb25zIE5vdEJlZm9yZT0iMjAyMy0wOS0wN1QyMzoxMjoyMFoiIE5vdE9uT3JBZnRl
cj0iMjAyMy0wOS0wN1QyMzoxNDoyMFoiPjxzYW1sOkF1ZGllbmNlUmVzdHJpY3Rpb24+PHNhbWw6
QXVkaWVuY2U+aHR0cHM6Ly93YWRucmZvcmVzdGhlYWx0aC5sb2NhbGhvc3QucHJvamVjdGZpcm1h
LmNvbTwvc2FtbDpBdWRpZW5jZT48L3NhbWw6QXVkaWVuY2VSZXN0cmljdGlvbj48L3NhbWw6Q29u
ZGl0aW9ucz48c2FtbDpBdXRoblN0YXRlbWVudCBBdXRobkluc3RhbnQ9IjIwMjMtMDktMDdUMjM6
MTM6MjBaIiBTZXNzaW9uSW5kZXg9InV1aWQxN2U0MGM1MC03Y2RhLTRmNzYtOTM1MC0wMzVjMDBi
ZDM0ZTEiIFNlc3Npb25Ob3RPbk9yQWZ0ZXI9IjIwMjMtMDktMDhUMDA6MTM6MjBaIj48c2FtbDpB
dXRobkNvbnRleHQ+PHNhbWw6QXV0aG5Db250ZXh0Q2xhc3NSZWY+dXJuOm9hc2lzOm5hbWVzOnRj
OlNBTUw6Mi4wOmFjOmNsYXNzZXM6UGFzc3dvcmQ8L3NhbWw6QXV0aG5Db250ZXh0Q2xhc3NSZWY+
PC9zYW1sOkF1dGhuQ29udGV4dD48L3NhbWw6QXV0aG5TdGF0ZW1lbnQ+PHNhbWw6QXR0cmlidXRl
U3RhdGVtZW50PjxzYW1sOkF0dHJpYnV0ZSBOYW1lPSJuYW1lIiBOYW1lRm9ybWF0PSJ1cm46b2Fz
aXM6bmFtZXM6dGM6U0FNTDoyLjA6YXNzZXJ0aW9uIj48c2FtbDpBdHRyaWJ1dGVWYWx1ZSB4c2k6
dHlwZT0ieHM6c3RyaW5nIj50b20ga2FtaW48L3NhbWw6QXR0cmlidXRlVmFsdWU+PC9zYW1sOkF0
dHJpYnV0ZT48c2FtbDpBdHRyaWJ1dGUgTmFtZT0ibGV2ZWwiIE5hbWVGb3JtYXQ9InVybjpvYXNp
czpuYW1lczp0YzpTQU1MOjIuMDphc3NlcnRpb24iPjxzYW1sOkF0dHJpYnV0ZVZhbHVlIHhzaTp0
eXBlPSJ4czpzdHJpbmciPjE8L3NhbWw6QXR0cmlidXRlVmFsdWU+PC9zYW1sOkF0dHJpYnV0ZT48
c2FtbDpBdHRyaWJ1dGUgTmFtZT0iZ3JvdXBzIiBOYW1lRm9ybWF0PSJ1cm46b2FzaXM6bmFtZXM6
dGM6U0FNTDoyLjA6YXNzZXJ0aW9uIj48c2FtbDpBdHRyaWJ1dGVWYWx1ZSB4c2k6dHlwZT0ieHM6
c3RyaW5nIj5mM191c2Vyczwvc2FtbDpBdHRyaWJ1dGVWYWx1ZT48L3NhbWw6QXR0cmlidXRlPjxz
YW1sOkF0dHJpYnV0ZSBOYW1lPSJ1c2VyIiBOYW1lRm9ybWF0PSJ1cm46b2FzaXM6bmFtZXM6dGM6
U0FNTDoyLjA6YXNzZXJ0aW9uIj48c2FtbDpBdHRyaWJ1dGVWYWx1ZSB4c2k6dHlwZT0ieHM6c3Ry
aW5nIj50b20ua2FtaW48L3NhbWw6QXR0cmlidXRlVmFsdWU+PC9zYW1sOkF0dHJpYnV0ZT48c2Ft
bDpBdHRyaWJ1dGUgTmFtZT0iZW1haWwiIE5hbWVGb3JtYXQ9InVybjpvYXNpczpuYW1lczp0YzpT
QU1MOjIuMDphc3NlcnRpb24iPjxzYW1sOkF0dHJpYnV0ZVZhbHVlIHhzaTp0eXBlPSJ4czpzdHJp
bmciPnRvbS5rYW1pbkBzaXRrYXRlY2guY29tPC9zYW1sOkF0dHJpYnV0ZVZhbHVlPjwvc2FtbDpB
dHRyaWJ1dGU+PHNhbWw6QXR0cmlidXRlIE5hbWU9Imd1aWQiIE5hbWVGb3JtYXQ9InVybjpvYXNp
czpuYW1lczp0YzpTQU1MOjIuMDphc3NlcnRpb24iPjxzYW1sOkF0dHJpYnV0ZVZhbHVlIHhzaTp0
eXBlPSJ4czpzdHJpbmciPkRQNE1UNlRWM1pUN00tM1FUNVpMNkRNLUREN1dWNFpaOEQtMUZaM0RE
NFZaNDwvc2FtbDpBdHRyaWJ1dGVWYWx1ZT48L3NhbWw6QXR0cmlidXRlPjwvc2FtbDpBdHRyaWJ1
dGVTdGF0ZW1lbnQ+PC9zYW1sOkFzc2VydGlvbj48L3NhbWxwOlJlc3BvbnNlPg==";

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
            var testDateTime = DateTimeOffset.Parse("2023-09-07T23:13:20Z");
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