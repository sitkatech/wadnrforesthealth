/*-----------------------------------------------------------------------
<copyright file="DetailViewData.cs" company="Tahoe Regional Planning Agency and Environmental Science Associates">
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

using LtInfo.Common.AgGridWrappers;
using ProjectFirma.Web.Controllers;
using ProjectFirma.Web.Models;
using ProjectFirma.Web.Security;
using ProjectFirma.Web.Common;


namespace ProjectFirma.Web.Views.FocusArea
{
    public class DetailViewData : FirmaViewData
    {
        public readonly Models.FocusArea FocusArea;
        public readonly string ProjectFocusAreaGridName;
        public readonly bool UserHasFocusAreaManagePermissions;


        public readonly string ProjectFocusAreaGridDataUrl;
        public readonly string EditFocusAreaUrl;
        public readonly string EditLocationUrl;
        public readonly string DeleteFocusAreaLocationUrl;

        public readonly ProjectsIncludingLeadImplementingGridSpec ProjectsIncludingLeadImplementingGridSpec;

        public readonly string ManageGrantAllocationsUrl;
        public readonly string IndexUrl;

        public readonly MapInitJson MapInitJson;
        public readonly bool HasSpatialData;

        public int NumberOfLeadImplementedProjects { get; }
        public int NumberOfProjectsContributedTo { get; }

        public GrantAllocationAwardGridSpec GrantAllocationAwardGridSpec { get; }
        public string GrantAllocationAwardGridName { get; }
        public string GrantAllocationAwardGridDataUrl { get; }

        public DetailViewData(Person currentPerson,
                              Models.FocusArea focusArea,
                              MapInitJson mapInitJson,
                              bool hasSpatialData,
                              GrantAllocationAwardGridSpec grantAllocationAwardGridSpec
                              ) : base(currentPerson)
        {
            FocusArea = focusArea;
            PageTitle = focusArea.FocusAreaName;
            ProjectFocusAreaGridName = "ProjectFocusAreaGrid";
            ProjectFocusAreaGridDataUrl = SitkaRoute<FocusAreaController>.BuildUrlFromExpression(fac => fac.DetailProjectFocusAreaGridJsonData(focusArea));

            GrantAllocationAwardGridSpec = grantAllocationAwardGridSpec;
            GrantAllocationAwardGridName = "grantAllocationAwardGridSpec";
            GrantAllocationAwardGridDataUrl = SitkaRoute<GrantAllocationAwardController>.BuildUrlFromExpression(gaa => gaa.GrantAllocationAwardByFocusAreaGridJsonData(focusArea));



            //EntityName = $"{Models.FieldDefinition.FocusArea.GetFieldDefinitionLabel()}";
            UserHasFocusAreaManagePermissions = new FocusAreaManageFeature().HasPermissionByPerson(CurrentPerson);

            EditFocusAreaUrl = SitkaRoute<FocusAreaController>.BuildUrlFromExpression(c => c.Edit(focusArea));
            EditLocationUrl =
                SitkaRoute<FocusAreaController>.BuildUrlFromExpression(c => c.EditLocation(focusArea));
            DeleteFocusAreaLocationUrl =
                SitkaRoute<FocusAreaController>.BuildUrlFromExpression(
                    c => c.DeleteFocusAreaLocation(focusArea));

            ProjectsIncludingLeadImplementingGridSpec =
                new ProjectsIncludingLeadImplementingGridSpec(CurrentPerson, false)
                {
                    ObjectNameSingular = $"{Models.FieldDefinition.Project.GetFieldDefinitionLabel()}",
                    ObjectNamePlural = $"{Models.FieldDefinition.Project.GetFieldDefinitionLabelPluralized()} associated with {focusArea.FocusAreaName}",
                    SaveFiltersInCookie = true
                };

            //GrantAllocations are accessed from the Grant Index page
            ManageGrantAllocationsUrl = SitkaRoute<GrantController>.BuildUrlFromExpression(c => c.Index());
            IndexUrl = SitkaRoute<FocusAreaController>.BuildUrlFromExpression(c => c.Index());

            MapInitJson = mapInitJson;
            HasSpatialData = hasSpatialData;


        }
    }
}
