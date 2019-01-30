/*-----------------------------------------------------------------------
<copyright file="EditProjectViewModel.cs" company="Tahoe Regional Planning Agency and Sitka Technology Group">
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
using System.ComponentModel.DataAnnotations;
using System.Linq;
using ProjectFirma.Web.Common;
using ProjectFirma.Web.Models;
using LtInfo.Common;
using LtInfo.Common.Models;

namespace ProjectFirma.Web.Views.Shared.ProjectControls
{
    public class EditProjectAttributesViewModel : FormViewModel, IValidatableObject
    {
        public int ProjectID { get; set; }
        public int ProjectTypeID { get; set; }

        public bool HasExistingProjectUpdate { get; set; }

        public ProjectCustomAttributes ProjectCustomAttributes { get; set; }


        /// <summary>
        /// Needed by the ModelBinder
        /// </summary>
        public EditProjectAttributesViewModel()
        {
        }

        public EditProjectAttributesViewModel(Models.Project project, bool hasExistingProjectUpdate)
        {
            ProjectTypeID = project.ProjectTypeID;
            ProjectID = project.ProjectID;
            HasExistingProjectUpdate = hasExistingProjectUpdate;
            ProjectCustomAttributes = new ProjectCustomAttributes(project);
        }

        public void UpdateModel(Models.Project project, Person currentPerson)
        {
            ProjectCustomAttributes?.UpdateModel(project, currentPerson);
        }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var outList = new List<string>();
            return ProjectCustomAttributes?.GetValidationResults(out outList);
        }
    }
}