using Hangfire.Dashboard;
using Microsoft.Owin;
using ProjectFirma.Web.Models;

namespace ProjectFirma.Web.ScheduledJobs
{
    public class HangfireFirmaWebAuthorizationFilter : IDashboardAuthorizationFilter
    {
        public bool Authorize(DashboardContext dashboardContext)
        {
            var owinEnvironment = dashboardContext.GetOwinEnvironment();
            var owinContext = new OwinContext(owinEnvironment);

            bool altIsAuthenticated = owinContext.Authentication.User.Identity.IsAuthenticated;

            var currentPerson = ClaimsIdentityHelper.PersonFromClaimsIdentity(owinContext.Authentication);
            return currentPerson.IsAdministrator();
        }
    }
}
