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
using ProjectFirma.Web.Models;
using LtInfo.Common.Mvc;

namespace ProjectFirma.Web.Views.Shared.ProjectControls
{
    public class EditProjectViewData : FirmaUserControlViewData
    {
        public IEnumerable<SelectListItem> ProjectTypes { get; }
        public IEnumerable<SelectListItem> StartYearRange { get; }
        public IEnumerable<SelectListItem> CompletionDateRange { get; }
        public IEnumerable<SelectListItem> ProjectStages { get; }
        public IEnumerable<SelectListItem> Organizations { get; }
        public IEnumerable<SelectListItem> PrimaryContactPeople { get; }
        public IEnumerable<SelectListItem> FocusAreas { get; }
        public string EditProjectTypeIntroductoryText { get; }
        public string ProjectTypeDisplayName { get; }
        public string DefaultPrimaryContactPersonName { get; }
        public bool HasThreeTierTaxonomy { get; }
        public bool ProjectTypeHasBeenSet { get; }
        public List<ProgramSimple> AllPrograms { get; }
        public int ProjectID { get; set; }

        public bool IsProjectNameImported { get; set; }
        public bool IsProjectIdentifierImported { get; set; }
        public bool IsProjectInitiationDateImported { get; set; }
        public bool IsCompletionDateImported { get; set; }
        public bool IsProjectStageImported { get; set; }
        public string ImportedFieldWarningMessage { get; set; }


        public EditProjectViewData(EditProjectType editProjectType,
            string projectTypeDisplayName,
            IEnumerable<ProjectStage> projectStages,
            IEnumerable<Models.Organization> organizations,
            IEnumerable<Person> primaryContactPeople,
            Person defaultPrimaryContactPerson,
            List<Models.ProjectType> projectTypes,
            IEnumerable<Models.FocusArea> focusAreas,
            bool projectTypeHasBeenSet,
            List<ProgramSimple> allPrograms,
            int projectID,
            bool isProjectNameImported,
            bool isProjectIdentifierImported,
            bool isProjectInitiationDateImported,
            bool isCompletionDateImported,
            bool isProjectStageImported,
            string importedFieldWarningMessage
            )
        {
            EditProjectTypeIntroductoryText = editProjectType.IntroductoryText;
            ProjectTypeDisplayName = projectTypeDisplayName;

            ProjectStages = projectStages.OrderBy(x => x.SortOrder).ToSelectListWithEmptyFirstRow(x => x.ProjectStageID.ToString(CultureInfo.InvariantCulture), y => y.ProjectStageDisplayName);
            
            Organizations = organizations.ToSelectListWithEmptyFirstRow(x => x.OrganizationID.ToString(CultureInfo.InvariantCulture), y => y.DisplayName);
            PrimaryContactPeople = primaryContactPeople.ToSelectListWithEmptyFirstRow(
                    x => x.PersonID.ToString(CultureInfo.InvariantCulture), y => y.FullNameFirstLastAndOrgShortName,
                    $"<Set Based on {Models.FieldDefinition.Project.GetFieldDefinitionLabel()}'s Associated {Models.FieldDefinition.Organization.GetFieldDefinitionLabelPluralized()}>");
            ProjectTypes = projectTypes.ToGroupedSelectList();
            StartYearRange = FirmaDateUtilities.YearsForUserInput().ToSelectListWithEmptyFirstRow(x => x.CalendarYear.ToString(CultureInfo.InvariantCulture), x => x.CalendarYearDisplay);
            CompletionDateRange = FirmaDateUtilities.YearsForUserInput().ToSelectListWithEmptyFirstRow(x => x.CalendarYear.ToString(CultureInfo.InvariantCulture), x => x.CalendarYearDisplay);
            HasThreeTierTaxonomy = MultiTenantHelpers.IsTaxonomyLevelTrunk();
            DefaultPrimaryContactPersonName = defaultPrimaryContactPerson?.FullNameFirstLastAndOrgShortName ?? "nobody";
            FocusAreas = focusAreas.OrderBy(x => x.FocusAreaName).ToSelectListWithEmptyFirstRow(x => x.FocusAreaID.ToString(CultureInfo.InvariantCulture), y => y.FocusAreaName);
            ProjectTypeHasBeenSet = projectTypeHasBeenSet;
            AllPrograms = allPrograms;
            ProjectID = projectID;
            IsProjectNameImported = isProjectNameImported;
            IsProjectIdentifierImported = isProjectIdentifierImported;
            IsProjectInitiationDateImported = isProjectInitiationDateImported;
            IsCompletionDateImported = isCompletionDateImported;
            IsProjectStageImported = isProjectStageImported;
            ImportedFieldWarningMessage = importedFieldWarningMessage;
        }
    }
}
