/*-----------------------------------------------------------------------
<copyright file="BasicsViewModel.cs" company="Tahoe Regional Planning Agency and Sitka Technology Group">
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
using LtInfo.Common;
using ProjectFirma.Web.Common;
using ProjectFirma.Web.Models;
using LtInfo.Common.Models;

namespace ProjectFirma.Web.Views.ProjectUpdate
{
    public class CustomAttributesViewModel : FormViewModel, IValidatableObject
    {
        public int ProjectUpdateID { get; set; }

        [FieldDefinitionDisplay(FieldDefinitionEnum.ProjectType)]
        [Required]
        public int ProjectTypeID { get; set; }

        public ProjectCustomAttributes ProjectCustomAttributes { get; set; }

        [DisplayName("Comments")]
        [StringLength(ProjectUpdateBatch.FieldLengths.ProjectAttributesComment)]
        public string Comments { get; set; }

        /// <summary>
        /// Needed by the ModelBinder
        /// </summary>
        public CustomAttributesViewModel()
        {
        }

        public CustomAttributesViewModel(Models.ProjectUpdate projectUpdate)
        {
            ProjectTypeID = projectUpdate.ProjectUpdateBatch.Project.ProjectTypeID;
            ProjectUpdateID = projectUpdate.ProjectUpdateID;
            ProjectCustomAttributes = new ProjectCustomAttributes(projectUpdate);
        }

        public void UpdateModel(Models.ProjectUpdate projectUpdate, Person person)
        {
            ProjectCustomAttributes?.UpdateModel(projectUpdate, person);
        }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var listWarnings = new List<string>();
           return GetValidationResults(out listWarnings);
        }

        public IEnumerable<ValidationResult> GetValidationResults(out List<string> errorMessages)
        {
            return ProjectCustomAttributes.GetValidationResults(out errorMessages);
        }
    }
}
