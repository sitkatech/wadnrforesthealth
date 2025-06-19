﻿/*-----------------------------------------------------------------------
<copyright file="ProjectMapPopupViewData.cs" company="Tahoe Regional Planning Agency and Environmental Science Associates">
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
using System.Web;
using ApprovalUtilities.Utilities;
using LtInfo.Common;
using MoreLinq;
using ProjectFirma.Web.Common;
using ProjectFirma.Web.Models;

namespace ProjectFirma.Web.Views.Shared.ProjectLocationControls
{
    public class ProjectMapPopupViewData : FirmaUserControlViewData
    {
        public readonly string DetailLinkDescriptor;

        public string TaxonomyTrunkDisplayName { get; private set; }
        public string TaxonomyBranchDisplayName { get; private set; }
        public string ProjectTypeDisplayName { get; private set; }
        public string ClassificationDisplayNamePluralized { get; private set; }

        public string ProjectDisplayName { get; set; }
        public HtmlString DisplayNameAsUrlBlankTarget { get; set; }
        public Models.ProjectImage KeyPhoto { get; set; }
        public string Duration { get; set; }
        public ProjectStage ProjectStage { get; set; }
        public Models.ProjectType ProjectType { get; set; }
        public string EstimatedTotalCost { get; set; }
        public HtmlString LeadImplementerOrganizationName { get; set; }
        public Dictionary<Models.ClassificationSystem, string> ClassificationsBySystem { get; set; }
        public string FactSheetUrl { get; set; }
        public string ProjectDetailUrl { get; set; }
        public TaxonomyLevel TaxonomyLevel { get; }

        public bool ShowDetailedInformation { get; }

        public ProjectMapPopupViewData(Models.Project project, bool showDetailedInformation)
        {
            //Project = project;
            DisplayNameAsUrlBlankTarget = project.DisplayNameAsUrlBlankTarget;
            KeyPhoto = project.KeyPhoto;
            Duration = project.Duration;
            ProjectStage = project.ProjectStage;
            ProjectType = project.ProjectType;
            EstimatedTotalCost = project.EstimatedTotalCost.HasValue ? project.EstimatedTotalCost.ToStringCurrency() : "Unknown";
            var leadImplementerOrg =
                project.ProjectOrganizations.FirstOrDefault(po => po.RelationshipTypeID == 33);
            LeadImplementerOrganizationName =
                leadImplementerOrg != null ? leadImplementerOrg.Organization.GetDisplayNameAsUrlBlankTarget() : new HtmlString(string.Empty);

            var dict = new Dictionary<Models.ClassificationSystem, string>();
            MoreEnumerable.ForEach(project.ProjectClassifications.Select(x => x.Classification.ClassificationSystem).Distinct(), x => dict.Add(x, string.Join(", ", project.ProjectClassifications.Select(y => y.Classification).Where(y => y.ClassificationSystem == x).Select(y => y.DisplayName).ToList())));
            ClassificationsBySystem = dict;

            ProjectDisplayName = project.DisplayName;
            ProjectDetailUrl = project.GetDetailUrl();
            FactSheetUrl = project.GetFactSheetUrl();
            DetailLinkDescriptor = $"For {Models.FieldDefinition.Project.GetFieldDefinitionLabel()} information & results, see the";
            InitializeDisplayNames();
            TaxonomyLevel = MultiTenantHelpers.GetTaxonomyLevel();

            ShowDetailedInformation = showDetailedInformation;
        }

        private void InitializeDisplayNames()
        {
            TaxonomyTrunkDisplayName = Models.FieldDefinition.TaxonomyTrunk.GetFieldDefinitionLabel();
            TaxonomyBranchDisplayName = Models.FieldDefinition.TaxonomyBranch.GetFieldDefinitionLabel();
            ProjectTypeDisplayName = Models.FieldDefinition.ProjectType.GetFieldDefinitionLabel();
            ClassificationDisplayNamePluralized = Models.FieldDefinition.Classification.GetFieldDefinitionLabelPluralized();
        }
    }
}
