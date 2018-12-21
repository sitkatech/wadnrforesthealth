/*-----------------------------------------------------------------------
<copyright file="ProjectPeopleDetailViewData.cs" company="Tahoe Regional Planning Agency and Sitka Technology Group">
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
using ProjectFirma.Web.Models;

namespace ProjectFirma.Web.Views.Shared.ProjectPerson
{
    public class ProjectPeopleDetailViewData : FirmaUserControlViewData
    {
        public List<ProjectPersonRelationship> AllProjectPeople { get; }
        public List<ProjectPersonRelationshipType> SetRelationshipTypes { get; }
        public Person CurrentPerson { get; }

        public ProjectPeopleDetailViewData(List<ProjectPersonRelationship> allProjectPeople, Person currentPerson)
        {
            AllProjectPeople = allProjectPeople;
            CurrentPerson = currentPerson;
            SetRelationshipTypes = AllProjectPeople.Select(x=>x.ProjectPersonRelationshipType).Distinct().ToList();
        }
    }
}
