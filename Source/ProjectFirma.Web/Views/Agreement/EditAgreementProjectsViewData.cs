﻿/*-----------------------------------------------------------------------
<copyright file="EditAgreementPersonViewData.cs" company="Tahoe Regional Planning Agency and Environmental Science Associates">
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
using System.Linq;
using ProjectFirma.Web.Common;

namespace ProjectFirma.Web.Views.Agreement
{
    public class EditAgreementProjectsViewData : FirmaUserControlViewData
    {
        public List<ProjectJson> AllPossibleProjectJsons { get; }

        public EditAgreementProjectsViewData()
        {
            var databaseEntitiesGrantAllocations = HttpRequestStorage.DatabaseEntities.Projects.ToList();
            AllPossibleProjectJsons = ProjectJson.MakeProjectJsonsFromProjects(databaseEntitiesGrantAllocations);
        }

    }
}