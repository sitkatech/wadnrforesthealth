﻿/*-----------------------------------------------------------------------
<copyright file="RelyingPartyAuthorizeAttribute.cs" company="Tahoe Regional Planning Agency and Environmental Science Associates">
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
using System.Linq;
using System.Web.Mvc;
using ProjectFirma.Web.Security;
using ProjectFirma.Web.Security.Shared;

namespace ProjectFirma.Web.Controllers
{
    /// <summary>
    /// Prevent unauthorized access, unless it has been specifically allowed using AllowAnonymousAttribute
    /// </summary>
    public abstract class RelyingPartyAuthorizeAttribute : AuthorizeAttribute
    {
        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            var attributeType = typeof(AnonymousUnclassifiedFeature);
            var skipAuthorization = filterContext.ActionDescriptor.IsDefined(attributeType, true)
                                    || filterContext.ActionDescriptor.ControllerDescriptor.IsDefined(attributeType,
                                        true);

            if (!skipAuthorization)
            {
                var baseFeatureType = typeof(FirmaBaseFeature);
                var baseFeatureAttribute = filterContext.ActionDescriptor.GetCustomAttributes(baseFeatureType, true).SingleOrDefault();
                if (baseFeatureAttribute != null && ((FirmaBaseFeature)baseFeatureAttribute).FundSourceedRoles.Any())
                {
                    base.OnAuthorization(filterContext);
                }
            }
        }
    }
}
