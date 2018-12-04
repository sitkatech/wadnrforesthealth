using System;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using System.Web.Hosting;
using log4net;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Logging;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Cookies;
using Owin;
using ProjectFirma.Web.Models;
using Sustainsys.Saml2;
using Sustainsys.Saml2.Configuration;
using Sustainsys.Saml2.Owin;
using Sustainsys.Saml2.WebSso;

namespace ProjectFirma.Web
{
    public partial class FirmaOwinStartup
    {
        public void ConfigureAuth(IAppBuilder app)
        {
            app.CreatePerOwinContext<CustomUserManager>(CustomUserManager.Create);
            app.CreatePerOwinContext<CustomSignInManager>(CustomSignInManager.Create);

            // Enable the application to use a cookie to store information for the signed in user
            // and to use a cookie to temporarily store information about a user logging in with a third party login provider
            // Configure the sign in cookie
            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                LoginPath = new PathString("/Account/Login"),
                Provider = new CookieAuthenticationProvider
                {
                    // Enables the application to validate the security stamp when the user logs in.
                    // This is a security feature which is used when you change a password or add an external login to your account.  
                    OnValidateIdentity = SecurityStampValidator.OnValidateIdentity<CustomUserManager, Person>(
                        validateInterval: TimeSpan.FromMinutes(30),
                        regenerateIdentity: (manager, user) => user.GenerateUserIdentityAsync(manager))
                }
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
                    AcsCommandResultCreated = (result, response) =>
                    {
                        var claimsIdentity = result.Principal.Identity as ClaimsIdentity;

                        //None of this exists in the result
                        var userEmail = claimsIdentity.Claims.FirstOrDefault(x => x.Type == "User.email");
                        var userFirstName = claimsIdentity.Claims.FirstOrDefault(x => x.Type == "User.FirstName");
                        var userLastName = claimsIdentity.Claims.FirstOrDefault(x => x.Type == "User.LastName");
                    },
                }
            };

            var idp = new IdentityProvider(new System.IdentityModel.Metadata.EntityId("https://test-secureaccess.wa.gov/FIM2/sps/sawidp/saml20"), spOptions)
            {
                AllowUnsolicitedAuthnResponse = true,
                Binding = Saml2BindingType.HttpPost,                
                SingleSignOnServiceUrl = new Uri("https://test-secureaccess.wa.gov/FIM2/sps/sawidp/saml20/logininitial?&NameIdFormat=Email&PartnerId=https://wadnrforesthealth.localhost.projectfirma.com"),
            };

            saml2Options.IdentityProviders.Add(idp);

            return saml2Options;
        }

        private static SPOptions CreateSPOptions()
        {
            //var nz = CultureInfo.GetCultureInfo("en-nz");

            //var organization = new Organization();
            //organization.Names.Add(new LocalizedName("Flink Solutions", nz));
            //organization.DisplayNames.Add(new LocalizedName("Flink Solutions", nz));
            //organization.Urls.Add(new LocalizedUri(new Uri("http://www.Sustainsys.se"), nz));

            var spOptions = new SPOptions
            {
                EntityId = new System.IdentityModel.Metadata.EntityId("https://wadnrforesthealth.localhost.projectfirma.com"),
                ReturnUrl = new Uri("https://wadnrforesthealth.localhost.projectfirma.com/")
//                Organization = organization
            };

            //var attributeConsumingService = new AttributeConsumingService("Saml2")
            //{
            //    IsDefault = true
            //};

            //attributeConsumingService.RequestedAttributes.Add(
            //    new RequestedAttribute("urn:someName")
            //    {
            //        FriendlyName = "Some Name",
            //        IsRequired = true,
            //        NameFormat = RequestedAttribute.AttributeNameFormatUri,
            //    });

            //attributeConsumingService.RequestedAttributes.Add(
            //    new RequestedAttribute("Minimal"));

            //spOptions.AttributeConsumingServices.Add(attributeConsumingService);

            spOptions.ServiceCertificates.Add(new X509Certificate2(HostingEnvironment.MapPath("~/wadnrforesthealth.localhost.projectfirma.com.pfx"), "W@$h1ngt0nDNR"));
            return spOptions;
        }
    }

    public class CustomUserManager : UserManager<Person>
    {
        public CustomUserManager(IUserStore<Person> store)
            : base(store)
        {
        }

        public static CustomUserManager Create()
        {
            var manager = new CustomUserManager(new CustomUserStore());
            return manager;
        }
    }

    public class CustomSignInManager : SignInManager<Person, string>
    {
        public CustomSignInManager(CustomUserManager userManager, IAuthenticationManager authenticationManager)
            : base(userManager, authenticationManager)
        {
        }

        public static CustomSignInManager Create(IdentityFactoryOptions<CustomSignInManager> options, IOwinContext context)
        {
            return new CustomSignInManager(context.GetUserManager<CustomUserManager>(), context.Authentication);
        }
    }

    public class CustomUserStore : IUserStore<Person>
    {
        private readonly DatabaseEntities _database;

        public CustomUserStore()
        {
            this._database = new DatabaseEntities();
        }

        public void Dispose()
        {
            this._database.Dispose();
        }

        public Task CreateAsync(Person user)
        {
            // TODO
            throw new NotImplementedException();
        }

        public Task UpdateAsync(Person user)
        {
            // TODO
            throw new NotImplementedException();
        }

        public Task DeleteAsync(Person user)
        {
            // TODO
            throw new NotImplementedException();
        }

        public async Task<Person> FindByIdAsync(string userId)
        {
            var user = await this._database.People.Where(c => c.PersonUniqueIdentifier.ToString() == userId).FirstOrDefaultAsync();
            return user;
        }

        public async Task<Person> FindByNameAsync(string userName)
        {
            var user = await this._database.People.Where(c => c.UserName == userName).FirstOrDefaultAsync();
            return user;
        }
    }

    //public class CustomClaimsAuthManager : ClaimsAuthenticationManager
    //{
    //    public override ClaimsPrincipal Authenticate(string resourceName, ClaimsPrincipal incomingPrincipal)
    //    {
    //        ClaimsIdentity ident = (ClaimsIdentity)incomingPrincipal.Identity;
    //            //Use incomingPrincipal.Identity.AuthenticationType to determine how they got auth'd
    //            //Use incomingPrincipal.Identity.IsAuthenticated to make sure they are authenticated.
    //            //Use ident.AddClaim to add a new claim to the user
    //            ...
    //        var identity = new Person(id, userId);
    //        // map a claim to a specific property
    //        identity.SomeProperty = ...Claims[IdpSomePropertyKey];
    //        ///...

    //        GenericPrincipal newPrincipal = new GenericPrincipal(identity, null);
    //        return newPrincipal;
    //    }
    //}
}