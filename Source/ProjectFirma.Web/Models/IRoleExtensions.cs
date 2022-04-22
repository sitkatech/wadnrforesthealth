/*-----------------------------------------------------------------------
<copyright file="IRoleExtensions.cs" company="Tahoe Regional Planning Agency and Sitka Technology Group">
Copyright (c) Tahoe Regional Planning Agency and Sitka Technology Group. All rights reserved.
<author>Sitka Technology Group</author>
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
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using log4net;
using ProjectFirma.Web.Controllers;
using ProjectFirma.Web.Security;
using ProjectFirma.Web.Common;

namespace ProjectFirma.Web.Models
{
    public static class IRoleExtensions
    {
        private static ILog Logger = LogManager.GetLogger(typeof(IRoleExtensions));

        /// <summary>
        /// Note AnonymousRole should not use this!
        /// </summary>
        public static string GetSummaryUrl(this IRole role)
        {
            if (role is AnonymousRole)
            {
                return SitkaRoute<RoleController>.BuildUrlFromExpression(t => t.Anonymous());
            }
            else
            {
                return SitkaRoute<RoleController>.BuildUrlFromExpression(t => t.Detail(role.RoleID));
            }

        }

        public static List<FeaturePermission> GetFeaturePermissions(this IRole role, Type baseFeatureType)
        {
            var featurePermissions = new List<FeaturePermission>();

            try
            {
                var types = Assembly.GetExecutingAssembly().GetTypes().Where(p => baseFeatureType.IsAssignableFrom(p) && p.Name != baseFeatureType.Name && !p.IsAbstract);
                foreach (var type in types)
                {
                    string featureDisplayName = FirmaBaseFeatureHelpers.GetDisplayName(type);
                    var firmaBaseFeature = FirmaBaseFeature.InstantiateFeature(type);
                    var hasPermission = FirmaBaseFeatureHelpers.DoesRoleHavePermissionsForFeature(role, firmaBaseFeature);
                    var needsContext = FirmaBaseFeatureHelpers.IsContextFeatureByInheritance(firmaBaseFeature);
                    var contextObjectType = FirmaBaseFeatureHelpers.GetFeatureWithContextType(firmaBaseFeature);

                    // Don't add duplicates to the list
                    if (featurePermissions.All(x => x.FeatureName != featureDisplayName))
                    {
                        featurePermissions.Add(new FeaturePermission(featureDisplayName, hasPermission, needsContext, contextObjectType?.Name ?? string.Empty));
                    }
                }
            }
            catch (ReflectionTypeLoadException ex)
            {
                StringBuilder sb = new StringBuilder();
                foreach (Exception exSub in ex.LoaderExceptions)
                {
                    sb.AppendLine(exSub.Message);
                    FileNotFoundException exFileNotFound = exSub as FileNotFoundException;
                    if (exFileNotFound != null)
                    {
                        if (!string.IsNullOrEmpty(exFileNotFound.FusionLog))
                        {
                            sb.AppendLine("Fusion Log:");
                            sb.AppendLine(exFileNotFound.FusionLog);
                        }
                    }
                    sb.AppendLine();
                }
                string errorMessage = sb.ToString();
                Logger.Error($"Reflection error message: {errorMessage}");

                // re-throw; this detour is just to try to fish out the extra logging info
                throw ex;
            }
            return featurePermissions;
        }

    }
}
