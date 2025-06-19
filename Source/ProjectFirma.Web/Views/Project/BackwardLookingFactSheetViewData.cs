/*-----------------------------------------------------------------------
<copyright file="FactSheetViewData.cs" company="Tahoe Regional Planning Agency and Environmental Science Associates">
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
using ProjectFirma.Web.Controllers;
using ProjectFirma.Web.Models;
using ProjectFirma.Web.Views.Map;
using ProjectFirma.Web.Views.Shared;
using ProjectFirma.Web.Common;
using ProjectFirma.Web.Views.Shared.SortOrder;
using LtInfo.Common;


namespace ProjectFirma.Web.Views.Project
{
    public class BackwardLookingFactSheetViewData : ProjectViewData
    {

        public Models.ProjectImage KeyPhoto { get; }
        public List<IGrouping<ProjectImageTiming, Models.ProjectImage>> ProjectImagesExceptKeyPhotoGroupedByTiming { get; }
        public int ProjectImagesPerTimingGroup { get; }

        public List<Models.Classification> Classifications { get; }
        public ProjectLocationSummaryMapInitJson ProjectLocationSummaryMapInitJson { get; }

        public string FactSheetPdfUrl { get; }

        public string TaxonomyColor { get; }
        public string ProjectTypeName { get; }
        public string TaxonomyBranchName { get; }

        public string ProjectTypeDisplayName { get; }
        public ViewPageContentViewData CustomFactSheetPageTextViewData { get; }

        public BackwardLookingFactSheetViewData(Person currentPerson, Models.Project project,
            ProjectLocationSummaryMapInitJson projectLocationSummaryMapInitJson,
            Models.FirmaPage firmaPageFactSheet) : base(currentPerson, project)
        {
            PageTitle = project.DisplayName;
            BreadCrumbTitle = "Fact Sheet";


            KeyPhoto = project.KeyPhoto;
            ProjectImagesExceptKeyPhotoGroupedByTiming =
                project.ProjectImages.Where(x => !x.IsKeyPhoto && x.ProjectImageTiming != ProjectImageTiming.Unknown && !x.ExcludeFromFactSheet)
                    .GroupBy(x => x.ProjectImageTiming)
                    .OrderBy(x => x.Key.SortOrder)
                    .ToList();
            ProjectImagesPerTimingGroup = ProjectImagesExceptKeyPhotoGroupedByTiming.Count == 1 ? 6 : 2;
            Classifications = project.ProjectClassifications.Select(x => x.Classification).ToList().SortByOrderThenName().ToList();

            ProjectLocationSummaryMapInitJson = projectLocationSummaryMapInitJson;

            FactSheetPdfUrl = SitkaRoute<ProjectController>.BuildUrlFromExpression(c => c.FactSheetPdf(project));

            if (project.ProjectType == null)
            {
                TaxonomyColor = "blue";
            }
            else
            {
                switch (MultiTenantHelpers.GetTaxonomyLevel().ToEnum)
                {
                    case TaxonomyLevelEnum.Leaf:
                        TaxonomyColor = project.ProjectType.ThemeColor;
                        break;
                    case TaxonomyLevelEnum.Branch:
                        TaxonomyColor = project.ProjectType.TaxonomyBranch.ThemeColor;
                        break;
                    case TaxonomyLevelEnum.Trunk:
                        TaxonomyColor = project.ProjectType.TaxonomyBranch.TaxonomyTrunk.ThemeColor;
                        break;
                }
            }
            ProjectTypeName = project.ProjectType == null ? $"{Models.FieldDefinition.Project.GetFieldDefinitionLabel()} Taxonomy Not Set" : project.ProjectType.DisplayName;
            TaxonomyBranchName = project.ProjectType == null ? $"{Models.FieldDefinition.Project.GetFieldDefinitionLabel()} Taxonomy Not Set" : project.ProjectType.TaxonomyBranch.DisplayName;
            ProjectTypeDisplayName = Models.FieldDefinition.ProjectType.GetFieldDefinitionLabel();
            CustomFactSheetPageTextViewData = new ViewPageContentViewData(firmaPageFactSheet, false);
        }
    }
}
