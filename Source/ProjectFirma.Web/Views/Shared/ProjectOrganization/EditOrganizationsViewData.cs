/*-----------------------------------------------------------------------
<copyright file="EditOrganizationsViewData.cs" company="Tahoe Regional Planning Agency and Environmental Science Associates">
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
using LtInfo.Common.Mvc;
using ProjectFirma.Web.Common;
using ProjectFirma.Web.Controllers;
using ProjectFirma.Web.Models;

namespace ProjectFirma.Web.Views.Shared.ProjectOrganization
{
    public class EditOrganizationsViewData
    {
        public List<OrganizationSimple> AllOrganizations { get; }
        public List<RelationshipTypeSimple> AllRelationshipTypes { get; }
        public RelationshipTypeSimple PrimaryContactRelationshipTypeSimple { get; }
        public int? DefaultPrimaryContactPersonID { get; }
        public string DefaultPrimaryContactPersonName { get; }
        public string AddOrganizationUrl { get; }

        public bool IsLeadImplementerImported { get; }
        public bool IsCreateWorkflow { get; }

        public EditOrganizationsViewData(IEnumerable<Models.Organization> organizations, List<RelationshipType> allRelationshipTypes, Person defaultPrimaryContactPerson, bool isLeadImplementerImported, bool isCreateWorkflow)
        {
            AllOrganizations = organizations.Where(x => x.OrganizationType.OrganizationTypeRelationshipTypes.Any()).Select(x => new OrganizationSimple(x)).ToList();

            var primaryContactRelationshipType = MultiTenantHelpers.GetIsPrimaryContactOrganizationRelationship();
            PrimaryContactRelationshipTypeSimple = primaryContactRelationshipType != null
                ? new RelationshipTypeSimple(primaryContactRelationshipType)
                : null;
            AllRelationshipTypes = allRelationshipTypes.Except(new[] { primaryContactRelationshipType }).Select(x => new RelationshipTypeSimple(x)).ToList();
            DefaultPrimaryContactPersonID = defaultPrimaryContactPerson?.PersonID;
            DefaultPrimaryContactPersonName = defaultPrimaryContactPerson?.FullNameFirstLastAndOrgShortName ?? "nobody";
            AddOrganizationUrl = SitkaRoute<HelpController>.BuildUrlFromExpression(x => x.MissingOrganization());
            IsLeadImplementerImported = isLeadImplementerImported;
            IsCreateWorkflow = isCreateWorkflow;
        }
    }
}
