/*-----------------------------------------------------------------------
<copyright file="EditProjectViewModel.cs" company="Tahoe Regional Planning Agency and Environmental Science Associates">
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
using LtInfo.Common;
using LtInfo.Common.Models;
using ProjectFirma.Web.Common;
using ProjectFirma.Web.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ProjectFirma.Web.Views.GrantAllocation
{
    public class EditGrantAllocationNoteViewModel : FormViewModel, IValidatableObject
    {
        public int GrantAllocationID { get; set; }

        public int GrantAllocationNoteID { get; set; }

        [FieldDefinitionDisplay(FieldDefinitionEnum.GrantAllocationNote)]
        [Required]
        public string GrantAllocationNoteText { get; set; }

       



        /// <summary>
        /// Needed by the ModelBinder
        /// </summary>
        public EditGrantAllocationNoteViewModel()
        {
        }

        public EditGrantAllocationNoteViewModel(Models.GrantAllocationNote grantAllocationNote)
        {
            GrantAllocationNoteText = grantAllocationNote.GrantAllocationNoteText;

        }

        public void UpdateModel(Models.GrantAllocationNote grantAllocationNote, Person currentPerson, EditGrantAllocationNoteType editGrantAllocationNoteType)
        {
            if (editGrantAllocationNoteType == EditGrantAllocationNoteTypeNewNote.Instance)
            {
                grantAllocationNote.CreatedByPerson = currentPerson;
                grantAllocationNote.CreatedDate = DateTime.Now;
            }
            else
            {
                grantAllocationNote.LastUpdatedByPerson = currentPerson;
                grantAllocationNote.LastUpdatedDate = DateTime.Now;
            }

            grantAllocationNote.GrantAllocationNoteText = GrantAllocationNoteText;

        }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (string.IsNullOrWhiteSpace(GrantAllocationNoteText))
            {
                yield return new SitkaValidationResult<EditGrantAllocationNoteViewModel, string>(
                    FirmaValidationMessages.GrantAllocationNoteIsEmptyText, m => m.GrantAllocationNoteText);
            }
        }
    }
}