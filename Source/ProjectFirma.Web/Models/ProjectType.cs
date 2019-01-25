/*-----------------------------------------------------------------------
<copyright file="ProjectType.cs" company="Tahoe Regional Planning Agency and Sitka Technology Group">
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
using ProjectFirma.Web.Views.Shared.ProjectLocationControls;
using LtInfo.Common;

namespace ProjectFirma.Web.Models
{
    public partial class ProjectType : IAuditableEntity, ITaxonomyTier, IHaveASortOrder
    {
        public const int ProjectTypeIDForestHealth = 2170;
        public const int ProjectTypeIDFuelsReduction = 2220;

        public static List<int> GetAllProjectTypeIDsWhereFocusAreaRequired()
        {
            List<int> requiredProjectTypeIDs = new List<int>()
            {
                ProjectTypeIDForestHealth,
                ProjectTypeIDFuelsReduction
            };

            return requiredProjectTypeIDs;
        }


        public int? SortOrder
        {
            get => ProjectTypeSortOrder;
            set => ProjectTypeSortOrder = value;
        }
        public int ID => ProjectTypeID;

        public string DisplayName
        {
            get
            {
                var taxonomyPrefix = string.IsNullOrWhiteSpace(ProjectTypeCode) ? string.Empty : $"{ProjectTypeCode}: ";
                return $"{taxonomyPrefix}{ProjectTypeName}";
            }
        }

        public HtmlString GetDisplayNameAsUrl()
        {
            return ProjectTypeModelExtensions.GetDisplayNameAsUrl(this);
        }
        public string SummaryUrl => this.GetSummaryUrl();

        public string CustomizedMapUrl => ProjectMapCustomization.BuildCustomizedUrl(ProjectLocationFilterType.ProjectType, ProjectTypeID.ToString(), ProjectColorByType.ProjectStage);

        public int TaxonomyTierID => ProjectTypeID;

        public static bool IsProjectTypeNameUnique(IEnumerable<ProjectType> projectTypes, string projectTypeName, int currentProjectTypeID)
        {
            var projectType =
                projectTypes.SingleOrDefault(
                    x => x.ProjectTypeID != currentProjectTypeID && String.Equals(x.ProjectTypeName, projectTypeName, StringComparison.InvariantCultureIgnoreCase));
            return projectType == null;
        }

        public string AuditDescriptionString => ProjectTypeName;

        public FancyTreeNode ToFancyTreeNode(Person currentPerson)
        {
            var fancyTreeNode = new FancyTreeNode($"{UrlTemplate.MakeHrefString(this.GetSummaryUrl(), DisplayName)}", ProjectTypeID.ToString(), false)
            {
                ThemeColor = string.IsNullOrWhiteSpace(ThemeColor) ? TaxonomyBranch.ThemeColor : ThemeColor,
                MapUrl = CustomizedMapUrl,
                Children = GetAssociatedProjects(currentPerson).Select(x => x.ToFancyTreeNode()).OrderBy(x => x.Title).ToList()
            };
            return fancyTreeNode;
        }

        public List<Project> GetAssociatedProjects(Person currentPerson)
        {
            return Projects.ToList().GetActiveProjectsAndProposals(currentPerson.CanViewProposals);
        }

        public List<IGrouping<PerformanceMeasure, ProjectTypePerformanceMeasure>> GetTaxonomyTierPerformanceMeasures()
        {
            return ProjectTypePerformanceMeasures.GroupBy(x => x.PerformanceMeasure).ToList();
        }
    }
}
