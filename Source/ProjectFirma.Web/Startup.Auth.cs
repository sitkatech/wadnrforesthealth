using System;
using System.Security.Cryptography.X509Certificates;
using System.Web.Hosting;
using Microsoft.AspNet.Identity;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Owin;
using Sustainsys.Saml2;
using Sustainsys.Saml2.Configuration;
using Sustainsys.Saml2.Metadata;
using Sustainsys.Saml2.Owin;
using Sustainsys.Saml2.WebSso;

namespace ProjectFirma.Web
{
    public partial class FirmaOwinStartup
    {
        public void ConfigureAuth(IAppBuilder app)
        {
            // Enable the application to use a cookie to store information for the signed in user
            // and to use a cookie to temporarily store information about a user logging in with a third party login provider
            // Configure the sign in cookie
            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                LoginPath = new PathString("/Account/Login")
            });
            app.UseExternalSignInCookie(DefaultAuthenticationTypes.ExternalCookie);

            app.UseSaml2Authentication(CreateSaml2Options());
        }

        private static Saml2AuthenticationOptions CreateSaml2Options()
        {
            var spOptions = CreateSPOptions();
            var saml2Options = new Saml2AuthenticationOptions(false)
            {
                SPOptions = spOptions,
                AuthenticationType = "wadnr",
                Notifications = new Saml2Notifications()
                {
                    LogoutCommandResultCreated = result =>
                    {

                    }
                }
            };

            var idp = new IdentityProvider(new EntityId("https://test-secureaccess.wa.gov/FIM2/sps/sawidp/saml20"), spOptions)
            {
                AllowUnsolicitedAuthnResponse = true,
                Binding = Saml2BindingType.HttpPost,
                SingleSignOnServiceUrl = new Uri("https://test-secureaccess.wa.gov/FIM2/sps/sawidp/saml20/logininitial?&NameIdFormat=Email&PartnerId=https://wadnrforesthealth.localhost.projectfirma.com"),
                SingleLogoutServiceUrl = new Uri("https://test-secureaccess.wa.gov/FIM2/sps/sawidp/saml20/slo"),
            };

            saml2Options.IdentityProviders.Add(idp);

            return saml2Options;
        }

        private static SPOptions CreateSPOptions()
        {
            var spOptions = new SPOptions
            {
                EntityId = new EntityId("https://wadnrforesthealth.localhost.projectfirma.com"),
                ReturnUrl = new Uri("https://wadnrforesthealth.localhost.projectfirma.com/")
            };
            spOptions.ServiceCertificates.Add(new X509Certificate2(HostingEnvironment.MapPath("~/wadnrforesthealth.localhost.projectfirma.com.pfx"), "W@$h1ngt0nDNR"));
            return spOptions;
        }
    }
}