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
using LtInfo.Common;
using LtInfo.Common.Models;
using ProjectFirma.Web.Common;
using ProjectFirma.Web.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ProjectFirma.Web.Views.GrantAllocation
{
    public class EditGrantAllocationNoteInternalViewModel : FormViewModel, IValidatableObject
    {
        public int GrantAllocationID { get; set; }

        public int GrantAllocationNoteInternalID { get; set; }

        [FieldDefinitionDisplay(FieldDefinitionEnum.GrantAllocationNoteInternal)]
        [Required]
        public string GrantAllocationNoteInternalText { get; set; }

       



        /// <summary>
        /// Needed by the ModelBinder
        /// </summary>
        public EditGrantAllocationNoteInternalViewModel()
        {
        }

        public EditGrantAllocationNoteInternalViewModel(Models.GrantAllocationNoteInternal grantAllocationNoteInternal)
        {
            GrantAllocationNoteInternalText = grantAllocationNoteInternal.GrantAllocationNoteInternalText;

        }

        public void UpdateModel(Models.GrantAllocationNoteInternal grantAllocationNoteInternal, Person currentPerson, EditGrantAllocationNoteType editGrantAllocationNoteType)
        {
            if (editGrantAllocationNoteType == EditGrantAllocationNoteTypeNewNote.Instance)
            {
                grantAllocationNoteInternal.CreatedByPerson = currentPerson;
                grantAllocationNoteInternal.CreatedDate = DateTime.Now;
            }
            else
            {
                grantAllocationNoteInternal.LastUpdatedByPerson = currentPerson;
                grantAllocationNoteInternal.LastUpdatedDate = DateTime.Now;
            }

            grantAllocationNoteInternal.GrantAllocationNoteInternalText = GrantAllocationNoteInternalText;

        }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (string.IsNullOrWhiteSpace(GrantAllocationNoteInternalText))
            {
                yield return new SitkaValidationResult<EditGrantAllocationNoteInternalViewModel, string>(
                    FirmaValidationMessages.GrantAllocationNoteIsEmptyText, m => m.GrantAllocationNoteInternalText);
            }
        }
    }
}