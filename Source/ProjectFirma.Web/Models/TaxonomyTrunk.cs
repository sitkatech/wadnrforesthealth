﻿/*-----------------------------------------------------------------------
<copyright file="TaxonomyTrunk.cs" company="Tahoe Regional Planning Agency and Environmental Science Associates">
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
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ProjectFirma.Web.Controllers;
using ProjectFirma.Web.Views.Shared.ProjectLocationControls;
using LtInfo.Common;
using ProjectFirma.Web.Common;
using ProjectFirma.Web.Views.Shared.SortOrder;

namespace ProjectFirma.Web.Models
{
    public partial class TaxonomyTrunk : IAuditableEntity, ITaxonomyTier, IHaveASortOrder
    {
        public int? SortOrder
        {
            get => TaxonomyTrunkSortOrder;
            set => TaxonomyTrunkSortOrder = value;
        }
        public int ID => TaxonomyTrunkID;

        public string DeleteUrl
        {
            get { return SitkaRoute<TaxonomyTrunkController>.BuildUrlFromExpression(c => c.DeleteTaxonomyTrunk(TaxonomyTrunkID)); }
        }

        public int TaxonomyTierID => TaxonomyTrunkID;

        public string DisplayName
        {
            get
            {
                var taxonomyPrefix = string.IsNullOrWhiteSpace(TaxonomyTrunkCode) ? string.Empty : string.Format("{0}: ", TaxonomyTrunkCode);
                return string.Format("{0}{1}", taxonomyPrefix, TaxonomyTrunkName);
            }
        }

        public HtmlString GetDisplayNameAsUrl()
        {
            return UrlTemplate.MakeHrefString(SummaryUrl, DisplayName);
        }

        public string SummaryUrl
        {
            get { return SitkaRoute<TaxonomyTrunkController>.BuildUrlFromExpression(x => x.Detail(TaxonomyTrunkID)); }
        }

        public string CustomizedMapUrl
        {
            get { return ProjectMapCustomization.BuildCustomizedUrl(ProjectLocationFilterType.TaxonomyTrunk, TaxonomyTrunkID.ToString(), ProjectColorByType.ProjectStage); }
        }

        public List<Project> GetAssociatedProjects(Person currentPerson)
        {
            return TaxonomyBranches.SelectMany(x => x.ProjectTypes.SelectMany(y => y.Projects)).ToList().GetActiveProjectsVisibleToUser(currentPerson);
        }

        public static bool IsTaxonomyTrunkNameUnique(IEnumerable<TaxonomyTrunk> taxonomyTrunks, string taxonomyTrunkName, int currentTaxonomyTrunkID)
        {
            var taxonomyTrunk = taxonomyTrunks.SingleOrDefault(x => x.TaxonomyTrunkID != currentTaxonomyTrunkID && String.Equals(x.TaxonomyTrunkName, taxonomyTrunkName, StringComparison.InvariantCultureIgnoreCase));
            return taxonomyTrunk == null;
        }

        public string AuditDescriptionString
        {
            get { return DisplayName; }
        }

        public List<ProjectType> ProjectTypes
        {
            get { return TaxonomyBranches.SelectMany(x => x.ProjectTypes).OrderBy(x => x.ProjectTypeName).ToList(); }
        }


        public FancyTreeNode ToFancyTreeNode(Person currentPerson)
        {
            var fancyTreeNode = new FancyTreeNode(string.Format("{0}", UrlTemplate.MakeHrefString(SummaryUrl, DisplayName)), TaxonomyTrunkID.ToString(), true)
            {
                ThemeColor = ThemeColor,
                MapUrl = CustomizedMapUrl,
                Children = TaxonomyBranches.ToList().SortByOrderThenName().Select(x => x.ToFancyTreeNode(currentPerson)).ToList()
            };
            return fancyTreeNode;
        }
    }
}
