/*-----------------------------------------------------------------------
<copyright file="FactSheetViewData.cs" company="Tahoe Regional Planning Agency">
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
using System.Web;
using LtInfo.Common;
using ProjectFirma.Web.Models;
using ProjectFirma.Web.Views.Map;
using ProjectFirma.Web.Views.Shared;
using LtInfo.Common.Models;
using LtInfo.Common.Views;
using ProjectFirma.Web.Common;
using ProjectFirma.Web.Controllers;
using ProjectFirma.Web.Views.Shared.SortOrder;

namespace ProjectFirma.Web.Views.Project
{
    public class ForwardLookingFactSheetViewData : ProjectViewData
    {
        public List<GooglePieChartSlice> FundSourceAllocationRequestAmountGooglePieChartSlices { get; }
        public Models.ProjectImage KeyPhoto { get; }
        public List<IGrouping<ProjectImageTiming, Models.ProjectImage>> ProjectImagesExceptKeyPhotoGroupedByTiming { get; }
        public int ProjectImagesPerTimingGroup { get; }
        public List<Models.Classification> Classifications { get; }
        public ProjectLocationSummaryMapInitJson ProjectLocationSummaryMapInitJson { get; }
        public GoogleChartJson GoogleChartJson { get; }
        public string EstimatedTotalCost { get; }

        public string NoFundSourceAllocationIdentified { get; }
        public string TotalFunding { get; }

        public string GrandAllocation { get; }
        public int CalculatedChartHeight { get; }
        public string FactSheetPdfUrl { get; }

        public string TaxonomyColor { get; }
        public string ProjectTypeDisplayName { get; }
        public string ProjectTypeName { get; }
        public string TaxonomyBranchName { get; }

        public ViewPageContentViewData CustomFactSheetTextViewData { get; }

        public ForwardLookingFactSheetViewData(Person currentPerson,
            Models.Project project,
            ProjectLocationSummaryMapInitJson projectLocationSummaryMapInitJson,
            GoogleChartJson googleChartJson,
            List<GooglePieChartSlice> fundSourceAllocationRequestAmountGooglePieChartSlices, Models.FirmaPage firmaPageFactSheetCustomText) : base(currentPerson, project)
        {
            PageTitle = project.DisplayName;
            BreadCrumbTitle = "Fact Sheet";

            KeyPhoto = project.KeyPhoto;
            ProjectImagesExceptKeyPhotoGroupedByTiming = project.ProjectImages.Where(x => !x.IsKeyPhoto && x.ProjectImageTiming != ProjectImageTiming.Unknown && !x.ExcludeFromFactSheet)
                .GroupBy(x => x.ProjectImageTiming).OrderBy(x => x.Key.SortOrder).ToList();
            ProjectImagesPerTimingGroup = ProjectImagesExceptKeyPhotoGroupedByTiming.Count == 1 ? 6 : 2;
            Classifications = project.ProjectClassifications.Select(x => x.Classification).ToList().SortByOrderThenName().ToList();

            ProjectLocationSummaryMapInitJson = projectLocationSummaryMapInitJson;
            GoogleChartJson = googleChartJson;
            FundSourceAllocationRequestAmountGooglePieChartSlices = fundSourceAllocationRequestAmountGooglePieChartSlices;

            //Dynamically resize chart based on how much space the legend requires
            CalculatedChartHeight = 350 - (FundSourceAllocationRequestAmountGooglePieChartSlices.Count <= 2
                                        ? FundSourceAllocationRequestAmountGooglePieChartSlices.Count * 24
                                        : FundSourceAllocationRequestAmountGooglePieChartSlices.Count * 20);
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
            EstimatedTotalCost = Project.EstimatedTotalCost.HasValue ? Project.EstimatedTotalCost.ToStringCurrency() : "";
            NoFundSourceAllocationIdentified = project.GetNoFundSourceAllocationIdentifiedAmount() != null ? Project.GetNoFundSourceAllocationIdentifiedAmount().ToStringCurrency() : "";

            GrandAllocation = project.ProjectFundSourceAllocationRequests.Any() ? project.ProjectFundSourceAllocationRequests.Sum(x => x.TotalAmount).ToStringCurrency() : ViewUtilities.Unknown;
            CustomFactSheetTextViewData = new ViewPageContentViewData(firmaPageFactSheetCustomText, false);
        }

        public HtmlString LegendHtml
        {
            get
            {
                var legendHtml = "<div>";
                foreach (var googlePieChartSlice in FundSourceAllocationRequestAmountGooglePieChartSlices.OrderBy(x => x.SortOrder))
                {
                    legendHtml += "<div class='chartLegendColorBox' style='display:inline-block; border: solid 6px " + googlePieChartSlice.Color + "'></div> ";
                    legendHtml += "<div style='display:inline-block' >" + googlePieChartSlice.Label + "</div>";
                    legendHtml += "<br>";
                }
                legendHtml += "</div>";
                return new HtmlString(legendHtml);
            }
        }
    }
}
