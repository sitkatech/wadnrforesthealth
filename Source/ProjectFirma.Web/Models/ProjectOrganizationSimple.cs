﻿/*-----------------------------------------------------------------------
<copyright file="ProjectOrganizationSimple.cs" company="Tahoe Regional Planning Agency and Environmental Science Associates">
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

namespace ProjectFirma.Web.Models
{
    public class ProjectOrganizationSimple
    {
        /// <summary>
        /// Needed by ModelBinder
        /// </summary>
        public ProjectOrganizationSimple()
        {
        }

        /// <summary>
        /// Constructor for building a new simple object with the POCO class
        /// </summary>
        public ProjectOrganizationSimple(ProjectOrganization projectOrganization)
            : this()
        {
            OrganizationID = projectOrganization.OrganizationID;
            OrganizationName = projectOrganization.Organization.OrganizationName;
            RelationshipTypeID = projectOrganization.RelationshipTypeID;
        }

        public ProjectOrganizationSimple(ProjectOrganizationUpdate projectOrganization)
        {
            OrganizationID = projectOrganization.OrganizationID;
            OrganizationName = projectOrganization.Organization.OrganizationName;
            RelationshipTypeID = projectOrganization.RelationshipTypeID;
        }

        public ProjectOrganizationSimple(int organizationID, int relationshipTypeID)
        {
            OrganizationID = organizationID;
            RelationshipTypeID = relationshipTypeID;
        }

        public int OrganizationID { get; set; }
        public int RelationshipTypeID { get; set; }
        public string OrganizationName { get; private set; }

        public ProjectOrganizationUpdate ToProjectOrganizationUpdate(ProjectUpdateBatch projectUpdateBatch)
        {
            return new ProjectOrganizationUpdate(projectUpdateBatch.ProjectUpdateBatchID, OrganizationID, RelationshipTypeID);
        }
    }
}