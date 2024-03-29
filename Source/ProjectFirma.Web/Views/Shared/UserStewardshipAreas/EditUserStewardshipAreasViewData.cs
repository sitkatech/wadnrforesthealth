﻿/*-----------------------------------------------------------------------
<copyright file="EditUserStewardshipAreasViewData.cs" company="Tahoe Regional Planning Agency">
Copyright (c) Tahoe Regional Planning Agency. All rights reserved.
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
using System.Linq;
using ProjectFirma.Web.Models;

namespace ProjectFirma.Web.Views.Shared.UserStewardshipAreas
{
    public class EditUserStewardshipAreasViewData : FirmaViewData
    {
        
        public EditViewDataForAngular ViewDataForAngular { get; }

        public bool Standalone { get; }

        public EditUserStewardshipAreasViewData(Person currentPerson, List<Models.Organization> allOrganizations,  bool standalone)
            : base(currentPerson)
        {
            ViewDataForAngular = new EditViewDataForAngular(allOrganizations);
            Standalone = standalone;
        }

        public EditUserStewardshipAreasViewData(Person currentPerson, List<Models.TaxonomyBranch> allTaxonomyBranches, bool standalone) : base(currentPerson)
        {
            ViewDataForAngular = new EditViewDataForAngular(allTaxonomyBranches);
            Standalone = standalone;
        }
        public EditUserStewardshipAreasViewData(Person currentPerson, List<Models.DNRUplandRegion> allDNRUplandRegions, bool standalone) : base(currentPerson)
        {
            ViewDataForAngular = new EditViewDataForAngular(allDNRUplandRegions);
            Standalone = standalone;
        }

        public class EditViewDataForAngular
        {
            public List<StewardshipAreaSimple> AllStewardshipAreas{ get; }

            public EditViewDataForAngular(List<Models.Organization> allOrganizations)
            {
                AllStewardshipAreas = allOrganizations.OrderBy(x => x.DisplayName).Select(x => new StewardshipAreaSimple(x)).ToList();
            }

            public EditViewDataForAngular(List<Models.TaxonomyBranch> allTaxonomyBranches)
            {
                AllStewardshipAreas = allTaxonomyBranches.OrderBy(x => x.DisplayName).Select(x => new StewardshipAreaSimple(x)).ToList();
            }
            public EditViewDataForAngular(List<Models.DNRUplandRegion> allDNRUplandRegions)
            {
                AllStewardshipAreas = allDNRUplandRegions.OrderBy(x => x.DisplayName).Select(x => new StewardshipAreaSimple(x)).ToList();
            }
        }
    }
}