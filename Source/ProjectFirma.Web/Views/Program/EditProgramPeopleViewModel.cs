/*-----------------------------------------------------------------------
<copyright file="EditProgramPeopleViewModel.cs" company="Tahoe Regional Planning Agency and Sitka Technology Group">
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
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using ProjectFirma.Web.Common;
using ProjectFirma.Web.Models;
using LtInfo.Common;
using LtInfo.Common.Models;
using LtInfo.Common.Mvc;
using ProjectFirma.Web.Views.Vendor;

namespace ProjectFirma.Web.Views.Program
{
    public class EditProgramPeopleViewModel : FormViewModel, IValidatableObject
    {
        public int ProgramID { get; set; }

        public IEnumerable<int> PeopleIDs { get; set; }


        /// <summary>
        /// Needed by the ModelBinder
        /// </summary>
        public EditProgramPeopleViewModel()
        {
        }

        public EditProgramPeopleViewModel(Models.Program program)
        {
            ProgramID = program.ProgramID;

        }

        public void UpdateModel(Models.Program program, Person currentPerson)
        {

        }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {

            return new List<ValidationResult>();
        }
    }
}
