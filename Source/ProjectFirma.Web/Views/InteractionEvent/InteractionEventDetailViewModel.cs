/*-----------------------------------------------------------------------
<copyright file="EditPeopleViewModel.cs" company="Tahoe Regional Planning Agency and Sitka Technology Group">
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

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using LtInfo.Common;
using LtInfo.Common.Models;
using ProjectFirma.Web.Models;

namespace ProjectFirma.Web.Views.InteractionEvent
{
    public class InteractionEventContactJson
    {
        public int PersonID { get; set; }

        public InteractionEventContactJson(InteractionEventContact interactionEventContact)
        {
            PersonID = interactionEventContact.PersonID;
        }
    }

    public class InteractionEventDetailViewModel : FormViewModel, IValidatableObject
    {
        public List<InteractionEventContactJson> InteractionEventContacts { get; set; }

        /// <summary>
        /// Needed by Model Binder
        /// </summary>
        public InteractionEventDetailViewModel()
        {
        }

        public InteractionEventDetailViewModel(List<InteractionEventContact> interactionEventContacts)
        {
            InteractionEventContacts = new List<InteractionEventContactJson>(interactionEventContacts.Select(x => new InteractionEventContactJson(x)));
        }

        public void UpdateModel(Models.InteractionEvent interactionEvent, ICollection<InteractionEventContact> interactionEventContacts)
        {
            var interactionEventContactsUpdated = InteractionEventContacts.Where(x => ModelObjectHelpers.IsRealPrimaryKeyValue(x.PersonID)).Select(x =>
                new Models.InteractionEventContact(interactionEvent.InteractionEventID, x.PersonID)).ToList();

            interactionEvent.InteractionEventContacts.Merge(interactionEventContactsUpdated,
                interactionEventContacts,
                (x, y) => x.InteractionEventID == y.InteractionEventID && x.PersonID == y.PersonID);
        }

        public virtual IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            return GetValidationResults();
        }

        private IEnumerable<ValidationResult> GetValidationResults()
        {
            //if (ProjectPersonSimples == null)
            //{
            //    ProjectPersonSimples = new List<ProjectPersonSimple>();
            //}

            //if (ProjectPersonSimples.GroupBy(x => new { RelationshipTypeID = x.ProjectPersonRelationshipTypeID, x.PersonID }).Any(x => x.Count() > 1))
            //{
            //    yield return new ValidationResult(
            //        $"Cannot have the same relationship type listed for the same {Models.FieldDefinition.Contact.GetFieldDefinitionLabel()} multiple times.");
            //}

            //var requiredRelationshipTypes = ProjectPersonRelationshipType.All.Where(x => x.IsRequired);
            //var chosenRelationshipTypeIDs = ProjectPersonSimples.Select(x => x.ProjectPersonRelationshipTypeID).Distinct().ToList();

            //foreach (var relationshipType in requiredRelationshipTypes)
            //{
            //    if (!chosenRelationshipTypeIDs.Contains(relationshipType.ProjectPersonRelationshipTypeID))
            //    {
            //        yield return new ValidationResult(
            //            $"The {relationshipType.ProjectPersonRelationshipTypeDisplayName} contact relationship is required.");
            //    }

            //    if (ProjectPersonSimples.Count(x =>
            //            x.ProjectPersonRelationshipTypeID == relationshipType.ProjectPersonRelationshipTypeID) > 1)
            //    {
            //        yield return new ValidationResult(
            //            $"The {relationshipType.ProjectPersonRelationshipTypeDisplayName} relationship can only be assigned to one contact.");
            //    }
            //}
            return null;
        }
    }
}
