/*-----------------------------------------------------------------------
<copyright file="DetailViewData.cs" company="Tahoe Regional Planning Agency and Sitka Technology Group">
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
        public readonly string EditFocusAreaUrl;
        public readonly string EditBoundaryUrl;
        public readonly string DeleteFocusAreaBoundaryUrl;

        public readonly ProjectsIncludingLeadImplementingGridSpec ProjectsIncludingLeadImplementingGridSpec;

        //public readonly ProjectFundingSourceExpendituresForFocusAreaGridSpec ProjectFundingSourceExpendituresForFocusAreaGridSpec;
        public readonly string ProjectFundingSourceExpendituresForFocusAreaGridName;

        public readonly string ManageFundingSourcesUrl;
        public readonly string IndexUrl;

        public readonly MapInitJson MapInitJson;
        public readonly bool HasSpatialData;

        public int NumberOfLeadImplementedProjects { get; }
        public int NumberOfProjectsContributedTo { get; }

        public DetailViewData(Person currentPerson,
            Models.FocusArea focusArea,
            MapInitJson mapInitJson,
            bool hasSpatialData) : base(currentPerson)
        {
            FocusArea = focusArea;
            PageTitle = focusArea.DisplayName;
            ProjectFocusAreaGridName = "Project Focus Area Grid";
            //EntityName = $"{Models.FieldDefinition.FocusArea.GetFieldDefinitionLabel()}";
            UserHasFocusAreaManagePermissions = new FocusAreaManageFeature().HasPermissionByPerson(CurrentPerson);

            EditFocusAreaUrl = SitkaRoute<FocusAreaController>.BuildUrlFromExpression(c => c.Edit(focusArea));
            //EditBoundaryUrl =
                //SitkaRoute<FocusAreaController>.BuildUrlFromExpression(c => c.EditBoundary(focusArea));
            //DeleteFocusAreaBoundaryUrl =
                //SitkaRoute<FocusAreaController>.BuildUrlFromExpression(
                    //c => c.DeleteFocusAreaBoundary(focusArea));

            ProjectsIncludingLeadImplementingGridSpec =
                new ProjectsIncludingLeadImplementingGridSpec(focusArea, CurrentPerson, false)
                {
                    ObjectNameSingular = $"{Models.FieldDefinition.Project.GetFieldDefinitionLabel()}",
                    ObjectNamePlural = $"{Models.FieldDefinition.Project.GetFieldDefinitionLabelPluralized()} associated with {focusArea.DisplayName}",
                    SaveFiltersInCookie = true
                };

            ManageFundingSourcesUrl = SitkaRoute<FundingSourceController>.BuildUrlFromExpression(c => c.Index());
            IndexUrl = SitkaRoute<FocusAreaController>.BuildUrlFromExpression(c => c.Index());

            MapInitJson = mapInitJson;
            HasSpatialData = hasSpatialData;

        }
    }
}
