/*-----------------------------------------------------------------------
<copyright file="BasicsViewData.cs" company="Tahoe Regional Planning Agency and Environmental Science Associates">
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
using ProjectFirma.Web.Models;
using LtInfo.Common.Mvc;
using ProjectFirma.Web.Views.Shared;
using ProjectFirma.Web.Security;

namespace ProjectFirma.Web.Views.ProjectCreate
{
    public class BasicsViewData : ProjectCreateViewData
    {
        public IEnumerable<SelectListItem> ProjectTypes { get; private set; }
        public IEnumerable<SelectListItem> StartYearRange { get; private set; }
        public IEnumerable<SelectListItem> CompletionDateRange { get; private set; }
        public bool HasCanStewardProjectsOrganizationRelationship { get; private set; }
        public bool HasThreeTierTaxonomy { get; private set; }
        private string ProjectDisplayName { get; }
        public bool IsEditable = true;
        public bool ProjectTypeHasBeenSet { get; set; }
        public ViewPageContentViewData InstructionsViewPageContentViewData { get; set; }

        public IEnumerable<SelectListItem> ProjectStages = ProjectStage.All.OrderBy(x => x.SortOrder).ToSelectListWithEmptyFirstRow(x => x.ProjectStageID.ToString(CultureInfo.InvariantCulture), y => y.ProjectStageDisplayName);

        public IEnumerable<SelectListItem> FocusAreas = HttpRequestStorage.DatabaseEntities.FocusAreas
            .OrderBy(x => x.FocusAreaName)
            .ToSelectListWithEmptyFirstRow(x => x.FocusAreaID.ToString(CultureInfo.InvariantCulture),
                y => y.FocusAreaName);


        public BasicsViewDataForAngular BasicsViewDataForAngular { get; set; }

		public BasicsViewData(Person currentPerson,
            IEnumerable<Models.ProjectType> projectTypes, Models.FirmaPage firmaPage)
            : base(currentPerson, ProjectCreateSection.Basics.ProjectCreateSectionDisplayName)
        {
            AssignParameters(projectTypes, -1);
            InstructionsViewPageContentViewData = new ViewPageContentViewData(firmaPage, new FirmaPageManageFeature().HasPermissionByPerson(currentPerson));
        }

        public BasicsViewData(Person currentPerson,
            Models.Project project,
            ProposalSectionsStatus proposalSectionsStatus,
            IEnumerable<Models.ProjectType> projectTypes, Models.FirmaPage firmaPage)
            : base(currentPerson, project, ProjectCreateSection.Basics.ProjectCreateSectionDisplayName, proposalSectionsStatus)
        {
            InstructionsViewPageContentViewData = new ViewPageContentViewData(firmaPage, new FirmaPageManageFeature().HasPermissionByPerson(currentPerson));
            ProjectDisplayName = project.DisplayName;
            AssignParameters(projectTypes, project.ProjectID);
            ProjectTypeHasBeenSet = project?.ProjectType != null;
        }

        private void AssignParameters(IEnumerable<Models.ProjectType> projectTypes, int projectID)
        {
            ProjectTypes = projectTypes.ToList().OrderTaxonomyLeaves().ToList().ToGroupedSelectList();
            
            
            StartYearRange =
                FirmaDateUtilities.YearsForUserInput()
                    .ToSelectListWithEmptyFirstRow(x => x.CalendarYear.ToString(CultureInfo.InvariantCulture), x => x.CalendarYearDisplay);
            CompletionDateRange = FirmaDateUtilities.YearsForUserInput().ToSelectListWithEmptyFirstRow(x => x.CalendarYear.ToString(CultureInfo.InvariantCulture), x => x.CalendarYearDisplay);
            HasCanStewardProjectsOrganizationRelationship = MultiTenantHelpers.HasCanStewardProjectsOrganizationRelationship();

            HasThreeTierTaxonomy = MultiTenantHelpers.IsTaxonomyLevelTrunk();

            var pagetitle = $"Add {Models.FieldDefinition.Project.GetFieldDefinitionLabel()}";
            PageTitle = $"{pagetitle}";
            if (ProjectDisplayName != null)
            {
                PageTitle += $": {ProjectDisplayName}";
            }

            var allPrograms = HttpRequestStorage.DatabaseEntities.Programs.ToList().Select(x => new ProgramSimple(x)).ToList();
            BasicsViewDataForAngular = new BasicsViewDataForAngular(allPrograms, projectID);
        }
    }

    public class BasicsViewDataForAngular
    {
        public BasicsViewDataForAngular(List<ProgramSimple> allPrograms, int projectID)
        {
            this.AllPrograms = allPrograms;
            this.ProjectID = projectID;
        }
        public List<ProgramSimple> AllPrograms { get; }
        public int ProjectID { get; set; }

    }
}
