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

namespace ProjectFirma.Web.Views.GrantAllocation
{
    public class EditGrantAllocationViewData : FirmaUserControlViewData
    {
        public IEnumerable<SelectListItem> Organizations { get; }

        public EditGrantAllocationType EditGrantAllocationType { get; set; }
        public IEnumerable<SelectListItem> GrantTypes { get; }
        public IEnumerable<SelectListItem> GrantNumbers { get; }
        public IEnumerable<SelectListItem> GrantModifications { get; }
        public IEnumerable<SelectListItem> Divisions { get; }
        public IEnumerable<SelectListItem> Regions { get; }
        public IEnumerable<SelectListItem> FederalFundCodes { get; }
        public IEnumerable<SelectListItem> ProgramManagersSelectList { get; }
        public IEnumerable<SelectListItem> GrantManagers { get; }
        public IEnumerable<SelectListItem> Sources { get; }
        public string AddContactUrl { get; }
        public bool? HasFundFsps { get; }
        public IEnumerable<SelectListItem> LikelyPeopleSelectList { get; }

        public EditGrantAllocationAngularViewData AngularViewData { get; }



        public EditGrantAllocationViewData(EditGrantAllocationType editGrantAllocationType,
                                        Models.GrantAllocation grantAllocationBeingEdited,
                                        IEnumerable<ProjectFirma.Web.Models.Organization> organizations,
                                        IEnumerable<GrantType> grantTypes,
                                        List<ProjectFirma.Web.Models.Grant> grants,
                                        List<Models.GrantModification> grantModifications,
                                        IEnumerable<Division> divisions,
                                        IEnumerable<Models.DNRUplandRegion> dnrUplandRegions,
                                        IEnumerable<FederalFundCode> federalFundCodes,
                                        IEnumerable<GrantAllocationSource> sources,
                                        List<Person> allPeople)
        {
            Organizations = organizations.ToSelectListWithEmptyFirstRow(x => x.OrganizationID.ToString(CultureInfo.InvariantCulture), y => y.DisplayName);//sorted in the controller
            GrantTypes = grantTypes.ToSelectListWithEmptyFirstRow(x => x.GrantTypeID.ToString(CultureInfo.InvariantCulture), y => y.GrantTypeName);
            GrantNumbers = grants.OrderBy(x => x.GrantNumber).ToSelectList(x => x.GrantID.ToString(CultureInfo.InvariantCulture), y => y.GrantNumber);
            Divisions = divisions.OrderBy(x => x.DivisionDisplayName).ToSelectListWithEmptyFirstRow(x => x.DivisionID.ToString(CultureInfo.InvariantCulture), y => y.DivisionDisplayName);
            Regions = dnrUplandRegions.OrderBy(x => x.DNRUplandRegionName).ToSelectListWithEmptyFirstRow(x => x.DNRUplandRegionID.ToString(CultureInfo.InvariantCulture), y => y.DNRUplandRegionName);
            GrantModifications = grantModifications.OrderByDescending(x => x.GrantModificationNameForDisplay).ToSelectListWithEmptyFirstRow(x => x.GrantModificationID.ToString(CultureInfo.InvariantCulture), y => y.GrantModificationNameForDisplay);
            AngularViewData = new EditGrantAllocationAngularViewData(grantModifications);

            FederalFundCodes = federalFundCodes.OrderBy(x => x.FederalFundCodeAbbrev).ToSelectListWithEmptyFirstRow(
                x => x.FederalFundCodeID.ToString(CultureInfo.InvariantCulture), y => y.FederalFundCodeAbbrev);
            GrantManagers = allPeople.OrderBy(x => x.FullNameLastFirst)
                .ToSelectListWithEmptyFirstRow(x => x.PersonID.ToString(CultureInfo.InvariantCulture),
                    y => y.FullNameFirstLastAndOrgShortName);

            // Include Persons who currently have the right a Program Manager
            List<Person> peopleWhoAreProgramManagers = allPeople.Where(x => x.IsProgramManager == true).ToList();
            // Include anyone who was set to be a Program Manager for this GrantAllocation in the past, but who may no longer have the right on their Person record.
            if (grantAllocationBeingEdited != null)
            {
                peopleWhoAreProgramManagers.AddRange(grantAllocationBeingEdited.GrantAllocationProgramManagers.Select(pm => pm.Person));
            }
            peopleWhoAreProgramManagers = peopleWhoAreProgramManagers.Distinct().ToList();

            ProgramManagersSelectList = peopleWhoAreProgramManagers.OrderBy(x => x.FullNameLastFirst)
                .ToSelectList(x => x.PersonID.ToString(CultureInfo.InvariantCulture),
                    y => y.FullNameFirstLastAndOrgShortName);

            EditGrantAllocationType = editGrantAllocationType;
            AddContactUrl = SitkaRoute<UserController>.BuildUrlFromExpression(x => x.Index((int)IndexGridSpec.UsersStatusFilterTypeEnum.AllActiveUsersAndContacts));
            HasFundFsps = grantAllocationBeingEdited?.HasFundFSPs;
            Sources = sources.ToSelectListWithEmptyFirstRow(
                x => x.GrantAllocationSourceID.ToString(CultureInfo.InvariantCulture), y => y.GrantAllocationSourceDisplayName);

            LikelyPeopleSelectList = allPeople.OrderBy(x => x.FullNameLastFirst)
                .ToSelectList(x => x.PersonID.ToString(CultureInfo.InvariantCulture),
                    y => y.FullNameFirstLastAndOrgShortName);

        }

    }

    public class GrantModificationJson
    {
        public int GrantModificationID { get; set; }
        public int GrantID { get; set; }

        public GrantModificationJson(Models.GrantModification grantModification)
        {
            GrantModificationID = grantModification.GrantModificationID;
            GrantID = grantModification.GrantID;
        }
    }

    public class EditGrantAllocationAngularViewData
    {
        public List<GrantModificationJson> GrantModificationJsons { get; set; }

        public EditGrantAllocationAngularViewData(List<Models.GrantModification> grantModifications)
        {
            GrantModificationJsons = grantModifications.Select(x => new GrantModificationJson(x)).ToList();
        }
    }
}
