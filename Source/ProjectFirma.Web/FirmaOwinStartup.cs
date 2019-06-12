using System;
using Microsoft.AspNet.Identity;
using ProjectFirma.Web;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Owin;
using ProjectFirma.Web.Common;
using ProjectFirma.Web.Controllers;
using ProjectFirma.Web.Models;
using ProjectFirma.Web.ScheduledJobs;

// This is how Owin figures out the class to call on startup
[assembly: OwinStartupAttribute(typeof(FirmaOwinStartup))]

namespace ProjectFirma.Web
{
    /// <summary>
    /// Owin Startup for Corral
    /// </summary>
    public class FirmaOwinStartup
    {
        /// <summary>
        /// Function required by <see cref="OwinStartupAttribute"/>
        /// </summary>
        public void Configuration(IAppBuilder app)
        {
            ConfigureHttpsProtocolForOutboundWebClientConnections();
            ConfigureApplicationAuthorization(app);
            ScheduledBackgroundJobBootstrapper.ConfigureHangfireAndScheduledBackgroundJobs(app);
        }

        private static void ConfigureHttpsProtocolForOutboundWebClientConnections()
        {
            // Setup for WebClient to be able to use better protocols for HTTPS as specified by operating system
            // Include the Tls12 TLS v1.2 protocol so that we don't get exception like this: System.Net.WebClient "The underlying connection was closed: An unexpected error occurred on a send."
            // Equivalent to: ServicePointManager.SecurityProtocol |= SecurityProtocolType.Tls12;
            AppContext.SetSwitch("Switch.System.Net.DontEnableSchUseStrongCrypto", false);
        }

        private static void ConfigureApplicationAuthorization(IAppBuilder app)
        {
            // Enable the application to use a cookie to store information for the signed in user
            // and to use a cookie to temporarily store information about a user logging in with a third party login provider
            // Configure the sign in cookie
            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                LoginPath = new PathString(SitkaRoute<AccountController>.BuildUrlFromExpression(x => x.Login(null))),
                CookieName = ClaimsIdentityHelper.AuthenticationApplicationCookieName
            });
            app.UseExternalSignInCookie(DefaultAuthenticationTypes.ExternalCookie);
        }
    }
}
