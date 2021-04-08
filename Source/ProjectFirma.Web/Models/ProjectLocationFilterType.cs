/*-----------------------------------------------------------------------
<copyright file="ProjectLocationFilterType.cs" company="Tahoe Regional Planning Agency and Sitka Technology Group">
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
using System.Linq.Expressions;

namespace ProjectFirma.Web.Models
{
    public partial class ProjectLocationFilterType
    {
        public abstract Expression<Func<Project, bool>> GetFilterFunction(List<int> filterValues);
        public abstract string DisplayName { get; }
    }

    public partial class ProjectLocationFilterTypeTaxonomyTrunk
    {
        public override Expression<Func<Project, bool>> GetFilterFunction(List<int> filterValues)
        {
            return project => filterValues.Contains(project.ProjectType.TaxonomyBranch.TaxonomyTrunkID);
        }

        public override string DisplayName => FieldDefinition.TaxonomyTrunk.GetFieldDefinitionLabel();
    }

    public partial class ProjectLocationFilterTypeTaxonomyBranch
    {
        public override Expression<Func<Project, bool>> GetFilterFunction(List<int> filterValues)
        {
            return project => filterValues.Contains(project.ProjectType.TaxonomyBranchID);
        }

        public override string DisplayName => FieldDefinition.TaxonomyBranch.GetFieldDefinitionLabel();
    }

    public partial class ProjectLocationFilterTypeProjectType
    {
        public override Expression<Func<Project, bool>> GetFilterFunction(List<int> filterValues)
        {
            return project => filterValues.Contains(project.ProjectType.ProjectTypeID);
        }

        public override string DisplayName => FieldDefinition.ProjectType.GetFieldDefinitionLabel();
    }

    public partial class ProjectLocationFilterTypeClassification
    {
        public override Expression<Func<Project, bool>> GetFilterFunction(List<int> filterValues)
        {
            return project => filterValues.Intersect(project.ProjectClassificationsForMap.Select(x => x.ClassificationID)).Any();
        }

        public override string DisplayName => FieldDefinition.Classification.GetFieldDefinitionLabel();
    }

    public partial class ProjectLocationFilterTypeProjectStage
    {
        public override Expression<Func<Project, bool>> GetFilterFunction(List<int> filterValues)
        {
            return project => filterValues.Contains(project.ProjectStage.ProjectStageID);
        }

        public override string DisplayName => FieldDefinition.ProjectStage.GetFieldDefinitionLabel();
    }

    public partial class ProjectLocationFilterTypeLeadImplementer
    {
        public override Expression<Func<Project, bool>> GetFilterFunction(List<int> filterValues)
        {
            return project => filterValues.Intersect(project.ProjectOrganizations.Select(x => x.RelationshipTypeID)).Any();
        }

        public override string DisplayName => "Lead Implementer";
    }

    public partial class ProjectLocationFilterTypeProgram
    {
        public override Expression<Func<Project, bool>> GetFilterFunction(List<int> filterValues)
        {
            return project => filterValues.Intersect(project.ProjectPrograms.Select(x => x.ProgramID)).Any();
        }

        public override string DisplayName => FieldDefinition.Program.GetFieldDefinitionLabel();
    }

}
