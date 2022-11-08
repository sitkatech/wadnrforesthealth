/*-----------------------------------------------------------------------
<copyright file="PermissionCheckResult.cs" company="Tahoe Regional Planning Agency and Environmental Science Associates">
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

using LtInfo.Common;
using LtInfo.Common.DesignByContract;

namespace ProjectFirma.Web.Security
{
    public class PermissionCheckResult
    {
        public readonly bool HasPermission;
        public readonly string PermissionDeniedMessage;

        public PermissionCheckResult(bool hasPermission, string permissionDeniedMessage)
        {
            Check.Ensure(hasPermission || !GeneralUtility.IsNullOrEmptyOrOnlyWhitespace(permissionDeniedMessage), "Should have a message on why permission is denied!");

            HasPermission = hasPermission;
            PermissionDeniedMessage = permissionDeniedMessage;
        }

        public static PermissionCheckResult MakeSuccessPermissionCheckResult()
        {
            return new PermissionCheckResult(true, string.Empty);
        }

        public static PermissionCheckResult MakeFailurePermissionCheckResult(string permissionDeniedMessage)
        {
            return new PermissionCheckResult(false, permissionDeniedMessage);
        }

        /// <param name="hasPermission"></param>
        /// <param name="possiblePermissionDeniedMessage">If hasPermission is false, this will be used, otherwise ignored.</param>
        /// <returns></returns>
        public static PermissionCheckResult MakeConditionalPermissionCheckResult(bool hasPermission, string possiblePermissionDeniedMessage)
        {
            if (hasPermission)
            {
                return MakeSuccessPermissionCheckResult();
            }

            return MakeFailurePermissionCheckResult(possiblePermissionDeniedMessage);
        }
    }
}
