/*-----------------------------------------------------------------------
<copyright file="FirmaBaseFeature.cs" company="Tahoe Regional Planning Agency and Environmental Science Associates">
Copyright (c) Tahoe Regional Planning Agency and Environmental Science Associates. All rights reserved.
<author>Environmental Science Associates</author>
</copyright>

<license>
This program is free software: you can redistribute it and/or modify
it under the terms of the GNU Affero General Public License as published by
the Free Software Foundation, either version 3 of the License, or
(at your option) any later version.

This program is distributed in the hope that it will be useful,
but WITHOUT ANY WARRANTY; without even the implied warranty of
MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
GNU Affero General Public License <http://www.gnu.org/licenses/> for more details.

Source code is available upon request via <support@sitkatech.com>.
</license>
-----------------------------------------------------------------------*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ProjectFirma.Web.Common;
using ProjectFirma.Web.Controllers;
using LtInfo.Common;
using LtInfo.Common.DesignByContract;
using ProjectFirma.Web.Models;
using AuthorizationContext = System.Web.Mvc.AuthorizationContext;

namespace ProjectFirma.Web.Security
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
    public abstract class FirmaBaseFeature : RelyingPartyAuthorizeAttribute
    {
        private readonly IList<IRole> _fundSourceedRoles;

        public IList<IRole> FundSourceedRoles => _fundSourceedRoles;

        protected FirmaBaseFeature(IList<IRole> fundSourceedRoles) // params
        {
            // Force user to pass us empty lists to make life simpler
            Check.RequireNotNull(fundSourceedRoles, "Can\'t pass null for FundSourceed Roles.");

            // At least one of these must be set
            //Check.Ensure(fundSourceedRoles.Any(), "Must set at least one Role");

            _fundSourceedRoles = fundSourceedRoles;
        }

        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            Roles = CalculateRoleNameStringFromFeature();

            // This ends up making the calls into the RoleProvider
            base.OnAuthorization(filterContext);
        }

        internal string CalculateRoleNameStringFromFeature()
        {
            return String.Join(", ", FundSourceedRoles.Select(r => r.RoleName).ToList());
        }

        public string FeatureName => GetType().Name;

        public virtual bool HasPermissionByPerson(Person person)
        {
            //AnonymousUnclassifiedFeature case; no roles on feature
            if (!_fundSourceedRoles.Any()) 
            {
                return true;
            }

            //Anonymous/Unassigned user + Unassigned role on feature
            if (person != null
                && person.IsAnonymousOrUnassigned 
                && _fundSourceedRoles.Contains(Role.Unassigned)) 
            {
                return true;
            }

            //Otherwise check if non-anonymous user with any matching role
            return person != null
                   && !person.IsAnonymousUser
                   && _fundSourceedRoles.Any(x => person.HasRoleID(x.RoleID));
        }

        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            return HasPermissionByPerson(HttpRequestStorage.Person);
        }

        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            var redirectToLogin = new RedirectResult(FirmaHelpers.GenerateLogInUrlWithReturnUrl());
            if (!filterContext.HttpContext.User.Identity.IsAuthenticated)
            {
                filterContext.Result = redirectToLogin;
                return;
            }
            throw new SitkaRecordNotAuthorizedException($"You are not authorized for feature \"{FeatureName}\". Log out and log in as a different user or request additional permissions.");
        }

        public static bool IsAllowed<T>(SitkaRoute<T> sitkaRoute, Person currentPerson) where T : Controller
        {
            var firmaFeatureLookupAttribute = sitkaRoute.Body.Method.GetCustomAttributes(typeof(FirmaBaseFeature), true).Cast<FirmaBaseFeature>().SingleOrDefault();
            Check.RequireNotNull(firmaFeatureLookupAttribute, $"Could not find feature for {sitkaRoute.BuildUrlFromExpression()}");
            // ReSharper disable PossibleNullReferenceException
            return firmaFeatureLookupAttribute.HasPermissionByPerson(currentPerson);
            // ReSharper restore PossibleNullReferenceException
        }

        public static FirmaBaseFeature InstantiateFeature(Type featureType)
        {
            return (FirmaBaseFeature)Activator.CreateInstance(featureType);
        }
    }
}
