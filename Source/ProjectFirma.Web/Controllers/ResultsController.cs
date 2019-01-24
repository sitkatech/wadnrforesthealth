/*-----------------------------------------------------------------------
<copyright file="ResultsController.cs" company="Tahoe Regional Planning Agency and Sitka Technology Group">
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
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web.Mvc;
using ProjectFirma.Web.Security;
using ProjectFirma.Web.Common;
using ProjectFirma.Web.Models;
using ProjectFirma.Web.Views.Map;
using ProjectFirma.Web.Views.Results;
using ProjectFirma.Web.Views.Shared.ProjectLocationControls;
using LtInfo.Common;
using LtInfo.Common.Mvc;
using LtInfo.Common.MvcResults;
using ProjectFirma.Web.Views.Shared.SortOrder;

namespace ProjectFirma.Web.Controllers
{
    public class ResultsController : FirmaBaseController
    {
        [ProjectLocationsViewFeature]
        public ViewResult ProjectMap()
        {
            List<int> filterValues;
            ProjectLocationFilterType projectLocationFilterType;
            ProjectColorByType colorByValue;

            var currentPersonCanViewProposals = CurrentPerson.CanViewProposals;
            if (!String.IsNullOrEmpty(Request.QueryString[ProjectMapCustomization.FilterByQueryStringParameter]))
            {
                projectLocationFilterType = ProjectLocationFilterType.ToType(Request
                    .QueryString[ProjectMapCustomization.FilterByQueryStringParameter]
                    .ParseAsEnum<ProjectLocationFilterTypeEnum>());
            }
            else
            {
                projectLocationFilterType = ProjectMapCustomization.DefaultLocationFilterType;
            }

            if (!String.IsNullOrEmpty(Request.QueryString[ProjectMapCustomization.FilterValuesQueryStringParameter]))
            {
                var filterValuesAsString = Request.QueryString[ProjectMapCustomization.FilterValuesQueryStringParameter]
                    .Split(',');
                filterValues = filterValuesAsString.Select(Int32.Parse).ToList();
            }
            else
            {
                filterValues = ProjectMapCustomization.GetDefaultLocationFilterValues(currentPersonCanViewProposals);
            }

            if (!String.IsNullOrEmpty(Request.QueryString[ProjectMapCustomization.ColorByQueryStringParameter]))
            {
                colorByValue = ProjectColorByType.ToType(Request
                    .QueryString[ProjectMapCustomization.ColorByQueryStringParameter]
                    .ParseAsEnum<ProjectColorByTypeEnum>());
            }
            else
            {
                colorByValue = ProjectMapCustomization.DefaultColorByType;
            }

            var firmaPage = FirmaPage.GetFirmaPageByPageType(FirmaPageType.ProjectMap);

            var projectsToShow = ProjectMapCustomization.ProjectsForMap(currentPersonCanViewProposals);

            var initialCustomization =
                new ProjectMapCustomization(projectLocationFilterType, filterValues, colorByValue);
            var projectLocationsLayerGeoJson =
                new LayerGeoJson($"{FieldDefinition.ProjectLocation.GetFieldDefinitionLabel()}",
                    Project.MappedPointsToGeoJsonFeatureCollection(projectsToShow, true, true), "red", 1,
                    LayerInitialVisibility.Show);
            var projectLocationsMapInitJson = new ProjectLocationsMapInitJson(projectLocationsLayerGeoJson,
                initialCustomization, "ProjectLocationsMap");

            projectLocationsMapInitJson.Layers.AddRange(HttpRequestStorage.DatabaseEntities.Organizations.GetBoundaryLayerGeoJson());

            var projectLocationsMapViewData = new ProjectLocationsMapViewData(projectLocationsMapInitJson.MapDivID, colorByValue.DisplayName, MultiTenantHelpers.GetTopLevelTaxonomyTiers(), currentPersonCanViewProposals);

            
            var projectLocationFilterTypesAndValues = CreateProjectLocationFilterTypesAndValuesDictionary(currentPersonCanViewProposals);
            var projectLocationsUrl = SitkaRoute<ResultsController>.BuildAbsoluteUrlHttpsFromExpression(x => x.ProjectMap());

            var filteredProjectsWithLocationAreasUrl =
                SitkaRoute<ResultsController>.BuildUrlFromExpression(x => x.FilteredProjectsWithLocationAreas(null));

            var viewData = new ProjectMapViewData(CurrentPerson,
                firmaPage,
                projectLocationsMapInitJson,
                projectLocationsMapViewData,
                projectLocationFilterTypesAndValues,
                projectLocationsUrl, filteredProjectsWithLocationAreasUrl);
            return RazorView<ProjectMap, ProjectMapViewData>(viewData);
        }

        private static Dictionary<ProjectLocationFilterTypeSimple, IEnumerable<SelectListItem>> CreateProjectLocationFilterTypesAndValuesDictionary(bool showProposals)
        {
            var projectLocationFilterTypesAndValues =
                new Dictionary<ProjectLocationFilterTypeSimple, IEnumerable<SelectListItem>>();

            if (MultiTenantHelpers.IsTaxonomyLevelTrunk())
            {
                var taxonomyTrunksAsSelectListItems =
                    HttpRequestStorage.DatabaseEntities.TaxonomyTrunks.AsEnumerable().ToSelectList(
                        x => x.TaxonomyTrunkID.ToString(CultureInfo.InvariantCulture), x => x.DisplayName);
                projectLocationFilterTypesAndValues.Add(new ProjectLocationFilterTypeSimple(ProjectLocationFilterType.TaxonomyTrunk),
                    taxonomyTrunksAsSelectListItems);
            }

            if (!MultiTenantHelpers.IsTaxonomyLevelLeaf())
            {
                var taxonomyBranchesAsSelectListItems =
                    HttpRequestStorage.DatabaseEntities.TaxonomyBranches.AsEnumerable().ToSelectList(
                        x => x.TaxonomyBranchID.ToString(CultureInfo.InvariantCulture), x => x.DisplayName);
                projectLocationFilterTypesAndValues.Add(new ProjectLocationFilterTypeSimple(ProjectLocationFilterType.TaxonomyBranch),
                    taxonomyBranchesAsSelectListItems);
            }

            var projectTypesAsSelectListItems =
                HttpRequestStorage.DatabaseEntities.ProjectTypes.AsEnumerable().ToSelectList(
                    x => x.ProjectTypeID.ToString(CultureInfo.InvariantCulture), x => x.DisplayName);
            projectLocationFilterTypesAndValues.Add(new ProjectLocationFilterTypeSimple(ProjectLocationFilterType.ProjectType),
                projectTypesAsSelectListItems);

            var classificationsAsSelectList = MultiTenantHelpers.GetClassificationSystems().SelectMany(x => x.Classifications).ToSelectList(x => x.ClassificationID.ToString(CultureInfo.InvariantCulture), x => MultiTenantHelpers.GetClassificationSystems().Count > 1 ? $"{x.ClassificationSystem.ClassificationSystemName} - {x.DisplayName}" : x.DisplayName);
            projectLocationFilterTypesAndValues.Add(new ProjectLocationFilterTypeSimple(ProjectLocationFilterType.Classification, string.Join(" & ", MultiTenantHelpers.GetClassificationSystems().Select(x => x.ClassificationSystemName).ToList())), classificationsAsSelectList);

            var projectStagesAsSelectListItems = ProjectMapCustomization.GetProjectStagesForMap(showProposals).ToSelectList(x => x.ProjectStageID.ToString(CultureInfo.InvariantCulture), x => x.ProjectStageDisplayName);
            projectLocationFilterTypesAndValues.Add(new ProjectLocationFilterTypeSimple(ProjectLocationFilterType.ProjectStage), projectStagesAsSelectListItems);

            return projectLocationFilterTypesAndValues;
        }

        [ProjectLocationsViewFeature]
        [HttpGet]
        public ContentResult FilteredProjectsWithLocationAreas()
        {
            return new ContentResult();
        }

        [ProjectLocationsViewFeature]
        [HttpPost]
        public JsonNetJArrayResult FilteredProjectsWithLocationAreas(ProjectMapCustomization projectMapCustomization)
        {
            if (projectMapCustomization.FilterPropertyValues == null ||
                !projectMapCustomization.FilterPropertyValues.Any())
            {
                return new JsonNetJArrayResult(new List<object>());
            }
            var projectLocationFilterTypeFromFilterPropertyName = projectMapCustomization
                .GetProjectLocationFilterTypeFromFilterPropertyName();
            var filterFunction =
                projectLocationFilterTypeFromFilterPropertyName.GetFilterFunction(projectMapCustomization
                    .FilterPropertyValues);
            var allProjectsForMap = ProjectMapCustomization.ProjectsForMap(CurrentPerson.CanViewProposals);
            var filteredProjects = allProjectsForMap.Where(filterFunction.Compile())
                .ToList();

            var filteredProjectsWithLocationAreas = filteredProjects.Where(x => !x.HasProjectLocationPoint).ToList();

            var taxonomyLevel = MultiTenantHelpers.GetTaxonomyLevel();
            var taxonomyTiersAsFancyTreeNodes = taxonomyLevel
                .GetTaxonomyTiers().SortByOrderThenName().Select(x => x.ToFancyTreeNode(CurrentPerson))
                .ToList();
            var projectsIDsThatDoNotHaveSimpleLocation = filteredProjectsWithLocationAreas
                .Select(project => project.FancyTreeNodeKey.ToString()).ToList();
            switch (taxonomyLevel.ToEnum)
            {
                case TaxonomyLevelEnum.Leaf:
                    PruneProjectsFromTaxonomyLeaves(taxonomyTiersAsFancyTreeNodes, projectsIDsThatDoNotHaveSimpleLocation);
                    break;
                case TaxonomyLevelEnum.Branch:
                    PruneTaxonomyBranchesWithNoProjects(taxonomyTiersAsFancyTreeNodes, projectsIDsThatDoNotHaveSimpleLocation);
                    break;
                case TaxonomyLevelEnum.Trunk:
                    foreach (var taxonomyTrunkNode in taxonomyTiersAsFancyTreeNodes)
                    {
                        var taxonomyBranchNodes = taxonomyTrunkNode.Children.ToList();
                        PruneTaxonomyBranchesWithNoProjects(taxonomyBranchNodes, projectsIDsThatDoNotHaveSimpleLocation);
                        taxonomyTrunkNode.Children = taxonomyBranchNodes;
                    }
                    taxonomyTiersAsFancyTreeNodes.RemoveAll(x => !x.Children.Any());
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            return new JsonNetJArrayResult(taxonomyTiersAsFancyTreeNodes);
        }

        private static void PruneTaxonomyBranchesWithNoProjects(List<FancyTreeNode> taxonomyBranchNodes,
            List<string> projectsIDsThatDoNotHaveSimpleLocation)
        {
            foreach (var taxonomyBranchNode in taxonomyBranchNodes)
            {
                var projectTypeNodes = taxonomyBranchNode.Children.ToList();
                PruneProjectsFromTaxonomyLeaves(projectTypeNodes, projectsIDsThatDoNotHaveSimpleLocation);
                taxonomyBranchNode.Children = projectTypeNodes;
            }
            taxonomyBranchNodes.RemoveAll(x => !x.Children.Any());

        }

        private static void PruneProjectsFromTaxonomyLeaves(List<FancyTreeNode> projectTypeNodes,
            List<string> projectsIDsThatDoNotHaveSimpleLocation)
        {
            foreach (var projectTypeNode in projectTypeNodes)
            {
                projectTypeNode.Children.RemoveAll(x => !projectsIDsThatDoNotHaveSimpleLocation.Contains(x.Key));
            }

            projectTypeNodes.RemoveAll(x => !x.Children.Any());
        }
    }
}
