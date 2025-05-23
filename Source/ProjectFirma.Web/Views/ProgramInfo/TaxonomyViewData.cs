﻿/*-----------------------------------------------------------------------
<copyright file="TaxonomyViewData.cs" company="Tahoe Regional Planning Agency and Environmental Science Associates">
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
using ProjectFirma.Web.Common;
using ProjectFirma.Web.Models;

namespace ProjectFirma.Web.Views.ProgramInfo
{
    public class TaxonomyViewData : FirmaViewData
    {
        public List<FancyTreeNode> TopLevelTaxonomyTierAsFancyTreeNodes { get; }
        public string TaxonomyTrunkDisplayName { get; }
        public string TaxonomyBranchDisplayName { get; }
        public string ProjectTypeDisplayName { get; }
        public TaxonomyLevel TaxonomyLevel { get; }

        public TaxonomyViewData(Person currentPerson, Models.FirmaPage firmaPage,
            List<FancyTreeNode> topLevelTaxonomyTierAsFancyTreeNodes) : base(currentPerson, firmaPage)
        {
            TopLevelTaxonomyTierAsFancyTreeNodes = topLevelTaxonomyTierAsFancyTreeNodes;
            PageTitle = MultiTenantHelpers.GetTaxonomySystemName();
            TaxonomyTrunkDisplayName = Models.FieldDefinition.TaxonomyTrunk.GetFieldDefinitionLabel();
            TaxonomyBranchDisplayName = Models.FieldDefinition.TaxonomyBranch.GetFieldDefinitionLabel();
            ProjectTypeDisplayName = Models.FieldDefinition.ProjectType.GetFieldDefinitionLabel();
            TaxonomyLevel = MultiTenantHelpers.GetTaxonomyLevel();
        }
    }
}
