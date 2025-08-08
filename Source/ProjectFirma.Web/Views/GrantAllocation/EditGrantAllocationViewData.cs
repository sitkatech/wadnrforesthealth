/*-----------------------------------------------------------------------
<copyright file="EditProjectViewData.cs" company="Tahoe Regional Planning Agency and Environmental Science Associates">
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

using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web.Mvc;
using ProjectFirma.Web.Common;
using LtInfo.Common.Mvc;
using ProjectFirma.Web.Controllers;
using ProjectFirma.Web.Models;
using ProjectFirma.Web.Views.User;

namespace ProjectFirma.Web.Views.FundSourceAllocation
{
    public class EditFundSourceAllocationViewData : FirmaUserControlViewData
    {
        public IEnumerable<SelectListItem> Organizations { get; }

        public EditFundSourceAllocationType EditFundSourceAllocationType { get; set; }
        public IEnumerable<SelectListItem> FundSourceTypes { get; }
        public IEnumerable<SelectListItem> FundSourceNumbers { get; }
        public IEnumerable<SelectListItem> Divisions { get; }
        public IEnumerable<SelectListItem> Regions { get; }
        public IEnumerable<SelectListItem> FederalFundCodes { get; }
        public IEnumerable<SelectListItem> ProgramManagersSelectList { get; }
        public IEnumerable<SelectListItem> FundSourceManagers { get; }
        public IEnumerable<SelectListItem> Priorities { get; }

        public IEnumerable<SelectListItem> Sources { get; }
        public string AddContactUrl { get; }
        public bool? HasFundFsps { get; }
        public IEnumerable<SelectListItem> LikelyPeopleSelectList { get; }

        public EditFundSourceAllocationAngularViewData AngularViewData { get; }



        public EditFundSourceAllocationViewData(EditFundSourceAllocationType editFundSourceAllocationType,
                                        Models.FundSourceAllocation fundSourceAllocationBeingEdited,
                                        IEnumerable<ProjectFirma.Web.Models.Organization> organizations,
                                        IEnumerable<FundSourceType> fundSourceTypes,
                                        List<ProjectFirma.Web.Models.FundSource> fundSources,
                                        IEnumerable<Division> divisions,
                                        IEnumerable<Models.DNRUplandRegion> dnrUplandRegions,
                                        IEnumerable<FederalFundCode> federalFundCodes,
                                        IEnumerable<FundSourceAllocationSource> sources,
                                        IEnumerable<FundSourceAllocationPriority> priorities,
                                        List<Person> allPeople)
        {
            Organizations = organizations.ToSelectListWithEmptyFirstRow(x => x.OrganizationID.ToString(CultureInfo.InvariantCulture), y => y.DisplayName);//sorted in the controller
            FundSourceTypes = fundSourceTypes.ToSelectListWithEmptyFirstRow(x => x.FundSourceTypeID.ToString(CultureInfo.InvariantCulture), y => y.FundSourceTypeName);
            FundSourceNumbers = fundSources.OrderBy(x => x.FundSourceNumber).ToSelectList(x => x.FundSourceID.ToString(CultureInfo.InvariantCulture), y => y.FundSourceNumber);
            Divisions = divisions.OrderBy(x => x.DivisionDisplayName).ToSelectListWithEmptyFirstRow(x => x.DivisionID.ToString(CultureInfo.InvariantCulture), y => y.DivisionDisplayName);
            Regions = dnrUplandRegions.OrderBy(x => x.DNRUplandRegionName).ToSelectListWithEmptyFirstRow(x => x.DNRUplandRegionID.ToString(CultureInfo.InvariantCulture), y => y.DNRUplandRegionName);

            AngularViewData = new EditFundSourceAllocationAngularViewData();

            FederalFundCodes = federalFundCodes.OrderBy(x => x.FederalFundCodeAbbrev).ToSelectListWithEmptyFirstRow(
                x => x.FederalFundCodeID.ToString(CultureInfo.InvariantCulture), y => y.FederalFundCodeAbbrev);
            FundSourceManagers = allPeople.OrderBy(x => x.FullNameLastFirst)
                .ToSelectListWithEmptyFirstRow(x => x.PersonID.ToString(CultureInfo.InvariantCulture),
                    y => y.FullNameFirstLastAndOrgShortName);

            Priorities = priorities.OrderBy(x => x.FundSourceAllocationPriorityNumber)
                .ToSelectListWithEmptyFirstRow(x => x.FundSourceAllocationPriorityID.ToString(CultureInfo.InvariantCulture),
                    y => y.FundSourceAllocationPriorityNumber.ToString());

            ProgramManagersSelectList = allPeople.OrderBy(x => x.FullNameLastFirst)

                .ToSelectList(x => x.PersonID.ToString(CultureInfo.InvariantCulture),
            y => y.FullNameFirstLastAndOrgShortName);

            EditFundSourceAllocationType = editFundSourceAllocationType;
            AddContactUrl = SitkaRoute<UserController>.BuildUrlFromExpression(x => x.Index((int)IndexGridSpec.UsersStatusFilterTypeEnum.AllActiveUsersAndContacts));
            HasFundFsps = fundSourceAllocationBeingEdited?.HasFundFSPs;
            Sources = sources.ToSelectListWithEmptyFirstRow(
                x => x.FundSourceAllocationSourceID.ToString(CultureInfo.InvariantCulture), y => y.FundSourceAllocationSourceDisplayName);

            LikelyPeopleSelectList = allPeople.OrderBy(x => x.FullNameLastFirst)
                .ToSelectList(x => x.PersonID.ToString(CultureInfo.InvariantCulture),
                    y => y.FullNameFirstLastAndOrgShortName);

        }

    }



    public class EditFundSourceAllocationAngularViewData
    {
        

        public EditFundSourceAllocationAngularViewData()
        {
            
        }
    }
}
