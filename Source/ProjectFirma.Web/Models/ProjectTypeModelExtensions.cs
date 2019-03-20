/*-----------------------------------------------------------------------
<copyright file="ProjectTypeModelExtensions.cs" company="Tahoe Regional Planning Agency and Sitka Technology Group">
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
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ProjectFirma.Web.Controllers;
using LtInfo.Common;
using LtInfo.Common.Mvc;
using ProjectFirma.Web.Common;
using ProjectFirma.Web.Views.Shared.SortOrder;

namespace ProjectFirma.Web.Models
{
    public static class ProjectTypeModelExtensions
    {
        public static HtmlString GetDisplayNameAsUrl(this ProjectType projectType)
        {
            return UrlTemplate.MakeHrefString(projectType.GetSummaryUrl(), projectType.DisplayName);
        }

        public static string GetSummaryUrl(this ProjectType projectType)
        {
            return SitkaRoute<ProjectTypeController>.BuildUrlFromExpression(x =>
                x.Detail(projectType.ProjectTypeID));
        }

        public static string GetDeleteUrl(this ProjectType projectType)
        {
            return SitkaRoute<ProjectTypeController>.BuildUrlFromExpression(c =>
                c.DeleteProjectType(projectType.ProjectTypeID));
        }

        public static IEnumerable<SelectListItem> ToGroupedSelectList(this List<ProjectType> projectTypes)
        {
            var selectListItems = new List<SelectListItem>();
            var groups = new Dictionary<string, SelectListGroup>();

            if (MultiTenantHelpers.IsTaxonomyLevelTrunk())
            {
                BuildThreeTierSelectList(projectTypes, groups, selectListItems);
            }
            else if (MultiTenantHelpers.IsTaxonomyLevelBranch())
            {
                foreach (var taxonomyBranchGrouping in projectTypes.GroupBy(x => x.TaxonomyBranch)
                    .OrderBy(x => x.Key.TaxonomyBranchSortOrder).ThenBy(x=>x.Key.DisplayName))
                {
                    var taxonomyBranch = taxonomyBranchGrouping.Key;
                    var selectListGroup = new SelectListGroup {Name = taxonomyBranch.DisplayName};
                    groups.Add(taxonomyBranch.DisplayName, selectListGroup);

                    foreach (var projectType in taxonomyBranchGrouping.OrderBy(x => x.ProjectTypeSortOrder).ThenBy(x=>x.DisplayName))
                    {
                        selectListItems.Add(new SelectListItem
                        {
                            Value = projectType.ProjectTypeID.ToString(),
                            Text = projectType.DisplayName,
                            Group = selectListGroup
                        });
                    }
                }
            }
            else
            {
                return projectTypes.SortByOrderThenName().ToSelectListWithEmptyFirstRow(m => m.ProjectTypeID.ToString(), m => m.DisplayName,
                    $"Select the {MultiTenantHelpers.GetProjectTypeDisplayNameForProject()}");
            }

            return selectListItems;
        }

        private static void BuildThreeTierSelectList(List<ProjectType> projectTypes,
            Dictionary<string, SelectListGroup> groups, List<SelectListItem> selectListItems)
        {
            foreach (var taxonomyTrunkGrouping in projectTypes.GroupBy(x => x.TaxonomyBranch.TaxonomyTrunk)
                .OrderBy(x => x.Key.TaxonomyTrunkSortOrder).ThenBy(x=>x.Key.DisplayName))
            {
                foreach (var taxonomyBranchGrouping in taxonomyTrunkGrouping.GroupBy(x => x.TaxonomyBranch)
                    .OrderBy(x => x.Key.TaxonomyBranchSortOrder).ThenBy(x => x.Key.DisplayName))
                {
                    var taxonomyBranch = taxonomyBranchGrouping.Key;
                    var displayName = $"{taxonomyBranch.TaxonomyTrunk.DisplayName} - {taxonomyBranch.DisplayName}";
                    var selectListGroup = new SelectListGroup() {Name = displayName};
                    groups.Add(displayName, selectListGroup);

                    foreach (var projectType in taxonomyBranchGrouping.OrderBy(x => x.ProjectTypeSortOrder).ThenBy(x => x.DisplayName))
                    {
                        selectListItems.Add(new SelectListItem()
                        {
                            Value = projectType.ProjectTypeID.ToString(),
                            Text = projectType.DisplayName,
                            Group = selectListGroup
                        });
                    }
                }
            }
        }

        public static IEnumerable<ProjectType> OrderTaxonomyLeaves(this List<ProjectType> taxonomyLeaves)
        {
            switch (MultiTenantHelpers.GetTaxonomyLevel().ToEnum)
            {
                case TaxonomyLevelEnum.Trunk:
                    return taxonomyLeaves.OrderBy(x => x.TaxonomyBranch.TaxonomyTrunk.TaxonomyTrunkSortOrder)
                        .ThenBy(x => x.TaxonomyBranch.TaxonomyTrunk.DisplayName)
                        .ThenBy(x => x.TaxonomyBranch.TaxonomyBranchSortOrder).ThenBy(x => x.TaxonomyBranch.DisplayName)
                        .ThenBy(x => x.ProjectTypeSortOrder).ThenBy(x => x.DisplayName);
                case TaxonomyLevelEnum.Branch:
                    return taxonomyLeaves.OrderBy(x => x.TaxonomyBranch.TaxonomyBranchSortOrder)
                        .ThenBy(x => x.TaxonomyBranch.DisplayName)
                        .ThenBy(x => x.ProjectTypeSortOrder).ThenBy(x => x.DisplayName);
                case TaxonomyLevelEnum.Leaf:
                    return taxonomyLeaves.OrderBy(x => x.ProjectTypeSortOrder).ThenBy(x => x.DisplayName);
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public static IEnumerable<TaxonomyBranch> OrderTaxonomyBranches(this List<TaxonomyBranch> taxonomyBranches)
        {
            switch (MultiTenantHelpers.GetTaxonomyLevel().ToEnum)
            {
                case TaxonomyLevelEnum.Trunk:
                    return taxonomyBranches.OrderBy(x => x.TaxonomyTrunk.TaxonomyTrunkSortOrder)
                        .ThenBy(x => x.TaxonomyTrunk.DisplayName)
                        .ThenBy(x => x.TaxonomyBranchSortOrder).ThenBy(x => x.DisplayName);
                case TaxonomyLevelEnum.Branch:
                    return taxonomyBranches.OrderBy(x => x.TaxonomyBranchSortOrder)
                        .ThenBy(x => x.DisplayName);
                default:
                    // why are u sorting taxonomy branches if the taxonomy level is leaf?
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}