/*-----------------------------------------------------------------------
< copyright file = "EditFileResourceViewModel.cs" company = "Tahoe Regional Planning Agency and Sitka Technology Group" >
Copyright(c) Tahoe Regional Planning Agency and Sitka Technology Group. All rights reserved.
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

using ProjectFirma.Web.Models;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ProjectFirma.Web.Views.Shared.FileResourceControls
{
    public class EditFileResourceViewModel: IValidatableObject
    {
        [Required]
        [DisplayName("Display Name")]
        [StringLength(Models.ProjectDocument.FieldLengths.DisplayName, ErrorMessage = "200 character maximum")]
        public string DisplayName { get; set; }
        
        [DisplayName("Description")]
        [StringLength(Models.ProjectDocument.FieldLengths.Description, ErrorMessage = "1000 character maximum.")]
        public string Description { get; set; }

        public EditFileResourceViewModel() { }

        public EditFileResourceViewModel(IEntityDocument entityDocument)
        {
            DisplayName = entityDocument.DisplayName;
            Description = entityDocument.Description;
        }

        public void UpdateModel(IEntityDocument entityDocument)
        {
            entityDocument.DisplayName = DisplayName;
            entityDocument.Description = Description;
        }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            yield return null;
        }
    }
}
