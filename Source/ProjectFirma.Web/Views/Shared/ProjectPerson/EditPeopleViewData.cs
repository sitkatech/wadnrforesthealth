/*-----------------------------------------------------------------------
<copyright file="EditPeopleViewData.cs" company="Tahoe Regional Planning Agency and Sitka Technology Group">
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
using System.Linq;
using ProjectFirma.Web.Common;
using ProjectFirma.Web.Controllers;
using ProjectFirma.Web.Models;
using ProjectFirma.Web.Security;

namespace ProjectFirma.Web.Views.Shared.ProjectPerson
{
    public class EditPeopleViewData
    {
        public List<PersonSimple> AllPeople { get; }
        public List<ProjectPersonRelationshipTypeSimple> AllProjectPersonRelationshipTypes { get; }
        public ProjectPersonRelationshipTypeSimple PrimaryContactProjectPersonRelationshipType { get; }
        public bool UserCanManageContacts { get; }
        public string AddContactUrl { get; }

        public EditPeopleViewData(IEnumerable<Person> allPeople, IEnumerable<ProjectPersonRelationshipType> allRelationshipTypes, Person currentPerson)
        {
            AllPeople = allPeople.Select(x => new PersonSimple(x)).ToList();
            AllProjectPersonRelationshipTypes = allRelationshipTypes.Except(new List<ProjectPersonRelationshipType>{
                ProjectPersonRelationshipType.PrimaryContact
            }).Select(x => new ProjectPersonRelationshipTypeSimple(x)).ToList();
            PrimaryContactProjectPersonRelationshipType =
                new ProjectPersonRelationshipTypeSimple(ProjectPersonRelationshipType.PrimaryContact);
            UserCanManageContacts = new ContactManageFeature().HasPermissionByPerson(currentPerson);
            AddContactUrl = SitkaRoute<UserController>.BuildUrlFromExpression(x => x.Index());
        }
    }
}
