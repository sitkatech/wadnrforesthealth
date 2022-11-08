/*-----------------------------------------------------------------------
<copyright file="EditPeopleViewModel.cs" company="Tahoe Regional Planning Agency and Environmental Science Associates">
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
using System.ComponentModel.DataAnnotations;
using System.Linq;
using LtInfo.Common;
using LtInfo.Common.Models;
using ProjectFirma.Web.Common;
using ProjectFirma.Web.Models;

namespace ProjectFirma.Web.Views.Shared.ProjectPerson
{
    public class EditPeopleViewModel : FormViewModel, IValidatableObject
    {
        public List<ProjectPersonSimple> ProjectPersonSimples { get; set; }

        /// <summary>
        /// Needed by Model Binder
        /// </summary>
        public EditPeopleViewModel()
        {
        }

        public EditPeopleViewModel(List<Models.ProjectPerson> projectPeople)
        {
            ProjectPersonSimples = projectPeople.Select(x => new ProjectPersonSimple(x)).ToList();
        }

        public void UpdateModel(Models.Project project, ICollection<Models.ProjectPerson> allProjectPeople)
        {
            var projectPeopleUpdated = ProjectPersonSimples.Where(x => ModelObjectHelpers.IsRealPrimaryKeyValue(x.PersonID)).Select(x =>
                new Models.ProjectPerson(project.ProjectID, x.PersonID, x.ProjectPersonRelationshipTypeID)).ToList();

            project.ProjectPeople.Merge(projectPeopleUpdated,
                allProjectPeople,
                (x, y) => x.ProjectID == y.ProjectID && x.PersonID == y.PersonID && x.ProjectPersonRelationshipTypeID == y.ProjectPersonRelationshipTypeID);
        }

        public virtual IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            return GetValidationResults();
        }

        public IEnumerable<ValidationResult> GetValidationResults()
        {
            if (ProjectPersonSimples == null)
            {
                ProjectPersonSimples = new List<ProjectPersonSimple>();
            }

            if (ProjectPersonSimples.GroupBy(x => new { RelationshipTypeID = x.ProjectPersonRelationshipTypeID, x.PersonID }).Any(x => x.Count() > 1))
            {
                yield return new ValidationResult(
                    $"Cannot have the same relationship type listed for the same {Models.FieldDefinition.Contact.GetFieldDefinitionLabel()} multiple times.");
            }

            var requiredRelationshipTypes = ProjectPersonRelationshipType.All.Where(x => x.IsRequired);
            var chosenRelationshipTypeIDs = ProjectPersonSimples.Select(x => x.ProjectPersonRelationshipTypeID).Distinct().ToList();

            foreach (var relationshipType in requiredRelationshipTypes)
            {
                if (!chosenRelationshipTypeIDs.Contains(relationshipType.ProjectPersonRelationshipTypeID))
                {
                    yield return new ValidationResult(
                        $"The {relationshipType.ProjectPersonRelationshipTypeDisplayName} contact relationship is required.");
                }

                if (ProjectPersonSimples.Count(x =>
                        x.ProjectPersonRelationshipTypeID == relationshipType.ProjectPersonRelationshipTypeID) > 1)
                {
                    yield return new ValidationResult(
                        $"The {relationshipType.ProjectPersonRelationshipTypeDisplayName} relationship can only be assigned to one contact.");
                }
            }
        }
    }
}
