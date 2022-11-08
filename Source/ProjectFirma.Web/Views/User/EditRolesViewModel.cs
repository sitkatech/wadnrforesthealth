/*-----------------------------------------------------------------------
<copyright file="EditRolesViewModel.cs" company="Tahoe Regional Planning Agency and Environmental Science Associates">
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
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using LtInfo.Common;
using ProjectFirma.Web.Models;
using LtInfo.Common.Models;
using ProjectFirma.Web.Common;

namespace ProjectFirma.Web.Views.User
{
    public class EditRolesViewModel : FormViewModel, IValidatableObject
    {
        [Required]
        public int PersonID { get; set; }

        [Required]
        [DisplayName("Role")]
        public List<RoleSimple> RoleSimples { get; set; }

        [Required]
        [DisplayName("Should Receive Support Emails?")]
        public bool ShouldReceiveSupportEmails { get; set; }

        /// <summary>
        /// Needed by the ModelBinder
        /// </summary>
        public EditRolesViewModel()
        {
        }

        public EditRolesViewModel(Person person)
        {
            PersonID = person.PersonID;
            RoleSimples = person.PersonRoles.Select(x => new RoleSimple(x.Role)).ToList();

            ShouldReceiveSupportEmails = person.ReceiveSupportEmails;
        }

        public void UpdateModel(Person person, Person currentPerson)
        {
            var roleSimpleIDs = RoleSimples.Select(x => x.RoleID).ToList();
            var downgradingFromSteward = person.HasRole(Models.Role.ProjectSteward) &&
                                         !roleSimpleIDs.Contains(Models.Role.ProjectSteward.RoleID) &&
                                         !roleSimpleIDs.Contains(Models.Role.Admin.RoleID) && !roleSimpleIDs.Contains(Models.Role.SitkaAdmin.RoleID);

            var downgradingFromProgramEditor = person.HasRole(Models.Role.ProgramEditor) && !roleSimpleIDs.Contains(Models.Role.ProgramEditor.RoleID);


            var newPersonRoles = new List<PersonRole>();
            if (RoleSimples.Any())
            {
                foreach (var roleSimple in RoleSimples)
                {
                    var newPersonRole = new PersonRole(person.PersonID, roleSimple.RoleID);
                    newPersonRoles.Add(newPersonRole);
                }
            }
            else
            {
                // RoleID is required so this should not really happen, but map to unassigned as a safety
                newPersonRoles.Add(new PersonRole(person, Models.Role.Unassigned));
            }

            person.PersonRoles.Merge(newPersonRoles, HttpRequestStorage.DatabaseEntities.PersonRoles.Local, (o1, o2) => o1.PersonID == o2.PersonID && o1.RoleID == o2.RoleID);


            person.ReceiveSupportEmails = ShouldReceiveSupportEmails;

            if (ModelObjectHelpers.IsRealPrimaryKeyValue(person.PersonID))
            {
                person.UpdateDate = DateTime.Now; // Existing person
            }
            else
            {
                person.CreateDate = DateTime.Now; // New person
            }

            if (downgradingFromSteward)
            {                
                HttpRequestStorage.DatabaseEntities.PersonStewardRegions.DeletePersonStewardRegion(person.PersonStewardRegions);
                HttpRequestStorage.DatabaseEntities.PersonStewardTaxonomyBranches.DeletePersonStewardTaxonomyBranch(person.PersonStewardTaxonomyBranches);
                HttpRequestStorage.DatabaseEntities.PersonStewardOrganizations.DeletePersonStewardOrganization(person.PersonStewardOrganizations);
            }

            if (downgradingFromProgramEditor)
            {
                HttpRequestStorage.DatabaseEntities.ProgramPeople.DeleteProgramPerson(person.ProgramPeople);
            }
        }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var roleIDs = this.RoleSimples.Select(x => x.RoleID).ToList();

            //if a user only has unassigned that is okay.
            if (roleIDs.Count == 1 && roleIDs.Contains(Models.Role.Unassigned.RoleID))
            {
                yield break;
            }

            var baseRoleIDs = Models.Role.GetRequiredBaseRoleIDs();
            //They must have at least one of the base roles if not unassigned.
            if (!roleIDs.Intersect(baseRoleIDs).Any())
            {
                yield return new SitkaValidationResult<EditRolesViewModel, List<RoleSimple>>(
                    $"This user needs to have one of the following base roles:  {Models.Role.Normal.RoleDisplayName}, {Models.Role.ProjectSteward.RoleDisplayName}, or {Models.Role.Admin.RoleDisplayName}",
                    m => m.RoleSimples);
            }

        }
    }
}
