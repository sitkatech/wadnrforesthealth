/*-----------------------------------------------------------------------
<copyright file="FirmaBaseController.cs" company="Tahoe Regional Planning Agency and Environmental Science Associates">
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
using System.Collections.ObjectModel;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Filters;
using ProjectFirma.Web.Common;
using ProjectFirma.Web.Models;
using log4net;

namespace ProjectFirma.Web.Controllers
{
    [ValidateInput(false)]
    public abstract class FirmaBaseController : SitkaController
    {
        protected ILog Logger = LogManager.GetLogger(typeof(FirmaBaseController));
  
        public static ReadOnlyCollection<MethodInfo> AllControllerActionMethods => AllControllerActionMethodsProtected;

        static FirmaBaseController()
        {
            AllControllerActionMethodsProtected = new ReadOnlyCollection<MethodInfo>(GetAllControllerActionMethods(typeof(FirmaBaseController)));
        }

        protected override bool IsCurrentUserAnonymous()
        {
            return CurrentPerson == null || CurrentPerson.IsAnonymousUser;
        }

        protected override string LoginUrl => FirmaHelpers.GenerateLogInUrlWithReturnUrl();

        protected override ISitkaDbContext SitkaDbContext => HttpRequestStorage.DatabaseEntities;

        protected Person CurrentPerson => HttpRequestStorage.Person;

        /// <summary>
        /// Must run before any code attempt to access <see cref="HttpRequestStorage.Person"/>
        /// </summary>
        protected override void OnAuthentication(AuthenticationContext filterContext)
        {
            HttpRequestStorage.Person = ClaimsIdentityHelper.PersonFromClaimsIdentity(HttpContext.GetOwinContext().Authentication);
            base.OnAuthentication(filterContext);
        }

        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            UpdatePersonLastActivityDate();
            base.OnActionExecuting(filterContext);
        }

        /// <summary>
        /// Update the <see cref="Person.LastActivityDate"/> to record their action in the system
        /// </summary>
        private void UpdatePersonLastActivityDate()
        {
            if (!IsCurrentUserAnonymous())
            {
                CurrentPerson.LastActivityDate = DateTime.Now;
                HttpRequestStorage.DatabaseEntities.SaveChangesWithNoAuditing();
            }
        }
    }
}
