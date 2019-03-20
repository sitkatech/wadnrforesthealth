/*-----------------------------------------------------------------------
<copyright file="ContactsValidationResult.cs" company="Tahoe Regional Planning Agency and Sitka Technology Group">
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
using ProjectFirma.Web.Common;
using ProjectFirma.Web.Models;

namespace ProjectFirma.Web.Views.ProjectUpdate
{
    public class ContactsValidationResult
    {
        private readonly List<string> _warningMessages;

        public ContactsValidationResult()
        {
            _warningMessages = new List<string>();
        }


        public ContactsValidationResult(List<ProjectPersonSimple> projectPersonSimples)
            
        {
            _warningMessages = new List<string>();

            if (projectPersonSimples.GroupBy(x => new { x.ProjectPersonRelationshipTypeID, x.PersonID }).Any(x => x.Count() > 1))
            {
                _warningMessages.Add($"Cannot have the same relationship type listed for the same {Models.FieldDefinition.Contact.GetFieldDefinitionLabel()} multiple times.");
            }

            var relationshipTypeThatMustBeRelatedOnceToAProject = ProjectPersonRelationshipType.All.Where(x => x.IsRequired).ToList();

            // no more than one 
            var projectContactsGroupedByRelationshipTypeID =
                projectPersonSimples.GroupBy(x => x.ProjectPersonRelationshipTypeID).ToList();

            _warningMessages.AddRange(relationshipTypeThatMustBeRelatedOnceToAProject
                .Where(rt => projectContactsGroupedByRelationshipTypeID.Count(po => po.Key == rt.ProjectPersonRelationshipTypeID) > 1)
                .Select(relationshipType => 
                    $"Cannot have more than one {Models.FieldDefinition.Contact.GetFieldDefinitionLabel()} with a {Models.FieldDefinition.ProjectRelationshipType.GetFieldDefinitionLabel()} set to \"{relationshipType.ProjectPersonRelationshipTypeDisplayName}\"."));

            // not zero 
            _warningMessages.AddRange(relationshipTypeThatMustBeRelatedOnceToAProject
                .Where(rt => projectContactsGroupedByRelationshipTypeID.Count(po => po.Key == rt.ProjectPersonRelationshipTypeID) == 0)
                .Select(relationshipType => 
                    $"Must have one {Models.FieldDefinition.Contact.GetFieldDefinitionLabel()} with a {Models.FieldDefinition.ProjectRelationshipType.GetFieldDefinitionLabel()} set to \"{relationshipType.ProjectPersonRelationshipTypeDisplayName}\"."));


            
        }

        public List<string> GetWarningMessages()
        {
            return _warningMessages;
        }

        public bool IsValid => !_warningMessages.Any();
    }
}
