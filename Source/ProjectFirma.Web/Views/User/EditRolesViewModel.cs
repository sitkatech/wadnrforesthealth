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
using Newtonsoft.Json;
using ProjectFirma.Web.Views.Shared;

namespace ProjectFirma.Web.Views.User
{
    public class EditRolesViewModel : FormViewModel, IValidatableObject
    {
        [Required]
        public int PersonID { get; set; }


        [DisplayName("Supplemental Roles")]
        public List<RoleSimple> RoleSimples { get; set; }

        [Required]
        [DisplayName("Base Role")]
        public int BaseRoleID { get; set; }

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
            RoleSimples = person.PersonRoles.Where(x => !x.Role.IsBaseRole).Select(x => new RoleSimple(x.Role)).ToList();
            BaseRoleID = person.GetUsersBaseRole().RoleID;
            ShouldReceiveSupportEmails = person.ReceiveSupportEmails;
        }

        public void UpdateModel(Person person, Person currentPerson)
        {

            var downgradingFromSteward = person.HasRole(Models.Role.ProjectSteward) && BaseRoleID != Models.Role.ProjectSteward.RoleID;
            var removingCanEditProgramRole = person.HasRole(Models.Role.CanEditProgram) && RoleSimples == null || (RoleSimples != null && RoleSimples.All(x => x.RoleID != Models.Role.CanEditProgram.RoleID));


            var newPersonRoles = new List<PersonRole>();
            if (RoleSimples != null && RoleSimples.Any())
            {
                foreach (var roleSimple in RoleSimples)
                {
                    var newPersonRole = new PersonRole(person.PersonID, roleSimple.RoleID);
                    newPersonRoles.Add(newPersonRole);
                }
            }
            
            newPersonRoles.Add(new PersonRole(person.PersonID, BaseRoleID));
            

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

            if (removingCanEditProgramRole)
            {
                HttpRequestStorage.DatabaseEntities.ProgramPeople.DeleteProgramPerson(person.ProgramPeople);
            }
        }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var errors = new List<ValidationResult>();

            if (BaseRoleID == Models.Role.Unassigned.RoleID && (RoleSimples != null && RoleSimples.Any()))
            { 
                errors.Add(new SitkaValidationResult<EditRolesViewModel, int>($"Cannot have an unassigned base role with assigned supplemental roles: {string.Join(", ", RoleSimples.Select(y => y.RoleDisplayName).ToList())}", x=> x.BaseRoleID));

            }
            return errors;

        }
    }
}
