/*-----------------------------------------------------------------------
<copyright file="EditProjectViewData.cs" company="Tahoe Regional Planning Agency and Sitka Technology Group">
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

using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web.Mvc;
using ProjectFirma.Web.Common;
using LtInfo.Common.Mvc;
using ProjectFirma.Web.Controllers;

namespace ProjectFirma.Web.Views.GrantAllocation
{
    public class EditGrantAllocationViewData : FirmaUserControlViewData
    {
        public IEnumerable<SelectListItem> Organizations { get; }

        public EditGrantAllocationType EditGrantAllocationType { get; set; }
        public IEnumerable<SelectListItem> GrantTypes { get; }
        public IEnumerable<SelectListItem> GrantNumbers { get; }
        public IEnumerable<SelectListItem> Divisions { get; }
        public IEnumerable<SelectListItem> Regions { get; }
        public IEnumerable<SelectListItem> FederalFundCodes { get; }
        public IEnumerable<SelectListItem> ProgramManagers { get; }
        public IEnumerable<SelectListItem> GrantManagers { get; }
        public string AddContactUrl { get; }



        public EditGrantAllocationViewData(EditGrantAllocationType editGrantAllocationType, IEnumerable<Models.Organization> organizations, IEnumerable<Models.GrantType> grantTypes, List<Models.Grant> grants, IEnumerable<Models.Division> divisions, IEnumerable<Models.Region> regions, IEnumerable<Models.FederalFundCode> federalFundCodes, List<Models.Person> people)
        {
            Organizations = organizations.ToSelectListWithEmptyFirstRow(x => x.OrganizationID.ToString(CultureInfo.InvariantCulture), y => y.DisplayName);//sorted in the controller
            GrantTypes = grantTypes.ToSelectListWithEmptyFirstRow(x => x.GrantTypeID.ToString(CultureInfo.InvariantCulture), y => y.GrantTypeName);
            GrantNumbers = grants.OrderBy(x => x.GrantNumber).ToSelectListWithEmptyFirstRow(x => x.GrantID.ToString(CultureInfo.InvariantCulture), y => y.GrantNumber);
            Divisions = divisions.OrderBy(x => x.DivisionDisplayName).ToSelectListWithEmptyFirstRow(x => x.DivisionID.ToString(CultureInfo.InvariantCulture), y => y.DivisionDisplayName);
            Regions = regions.OrderBy(x => x.RegionName).ToSelectListWithEmptyFirstRow(x => x.RegionID.ToString(CultureInfo.InvariantCulture), y => y.RegionName);
            
            FederalFundCodes = federalFundCodes.OrderBy(x => x.FederalFundCodeAbbrev).ToSelectListWithEmptyFirstRow(
                x => x.FederalFundCodeID.ToString(CultureInfo.InvariantCulture), y => y.FederalFundCodeAbbrev);
            GrantManagers = people.OrderBy(x => x.FullNameLastFirst)
                .ToSelectListWithEmptyFirstRow(x => x.PersonID.ToString(CultureInfo.InvariantCulture),
                    y => y.FullNameFirstLastAndOrgShortName);
            ProgramManagers = people.Where(x => x.RoleID == Models.Role.ProjectSteward.RoleID).OrderBy(x => x.FullNameLastFirst)
                .ToSelectListWithEmptyFirstRow(x => x.PersonID.ToString(CultureInfo.InvariantCulture),
                    y => y.FullNameFirstLastAndOrgShortName);


            EditGrantAllocationType = editGrantAllocationType;
            AddContactUrl = SitkaRoute<UserController>.BuildUrlFromExpression(x => x.Index());
        }

    }
}
