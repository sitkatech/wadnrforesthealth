/*-----------------------------------------------------------------------
<copyright file="EditProgramPeopleViewModel.cs" company="Tahoe Regional Planning Agency and Environmental Science Associates">
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
using System.Web;
using DocumentFormat.OpenXml.Bibliography;
using ProjectFirma.Web.Common;
using ProjectFirma.Web.Models;
using LtInfo.Common;
using LtInfo.Common.Models;
using LtInfo.Common.Mvc;
using MoreLinq;
using ProjectFirma.Web.Views.User;
using ProjectFirma.Web.Views.Vendor;
using Person = ProjectFirma.Web.Models.Person;

namespace ProjectFirma.Web.Views.Program
{
    public class EditProgramPeopleViewModel : FormViewModel, IValidatableObject
    {
        public int ProgramID { get; set; }

        public List<int> PersonIDList { get; set; }


        /// <summary>
        /// Needed by the ModelBinder
        /// </summary>
        public EditProgramPeopleViewModel()
        {
        }

        public EditProgramPeopleViewModel(Models.Program program)
        {
            ProgramID = program.ProgramID;
            PersonIDList = program.ProgramPeople.Select(x => x.PersonID).ToList();

        }

        public void UpdateModel(Models.Program program, Person currentPerson)
        {

            var programPeopleToCommit = PersonIDList != null ? PersonIDList.Select(x => new ProgramPerson(program.ProgramID, x)).ToList() : new List<ProgramPerson>();

            program.ProgramPeople.Merge(programPeopleToCommit,
                HttpRequestStorage.DatabaseEntities.ProgramPeople.Local,
                (x, y) => x.ProgramID == y.ProgramID && x.PersonID == y.PersonID);

        }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (PersonIDList != null && PersonIDList.Any())
            {
                var peopleEditors = HttpRequestStorage.DatabaseEntities.People
                    .Where(x => PersonIDList.Contains(x.PersonID)).ToList();
                var peopleWithoutProgramEditorRole = peopleEditors
                    .Where(x => x.PersonRoles.All(pr => pr.RoleID != Models.Role.ProgramEditor.RoleID)).ToList();
                if (peopleWithoutProgramEditorRole.Any())
                {
                    var listOfNames = new List<string>();
                    foreach (var person in peopleWithoutProgramEditorRole)
                    {
                        listOfNames.Add(person.FullNameFirstLastAndOrgShortName);
                    }

                    yield return new SitkaValidationResult<EditProgramPeopleViewModel, List<int>>(
                        $"The following user(s) do not have the Program Editor role and cannot be set as an editor for this program: {string.Join(", ", listOfNames)}. Please add the \"Program Editor\" role on their user page.",
                        m => m.PersonIDList);
                }
            }


        }
    }
}
