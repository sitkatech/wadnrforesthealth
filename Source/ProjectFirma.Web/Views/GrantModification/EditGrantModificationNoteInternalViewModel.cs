/*-----------------------------------------------------------------------
<copyright file="EditGrantModificationNoteInternalViewModel.cs" company="Tahoe Regional Planning Agency and Environmental Science Associates">
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
using System.ComponentModel.DataAnnotations;
using LtInfo.Common;
using LtInfo.Common.Models;
using ProjectFirma.Web.Common;
using ProjectFirma.Web.Models;

namespace ProjectFirma.Web.Views.GrantModification
{
    public class EditGrantModificationNoteInternalViewModel : FormViewModel, IValidatableObject
    {
        public int GrantModificationID { get; set; }

        public int GrantModificationNoteInternalID { get; set; }

        [FieldDefinitionDisplay(FieldDefinitionEnum.GrantModificationNoteInternal)]
        [Required]
        public string GrantModificationNoteInternalText { get; set; }

        /// <summary>
        /// Needed by the ModelBinder
        /// </summary>
        public EditGrantModificationNoteInternalViewModel()
        {
        }

        public EditGrantModificationNoteInternalViewModel(Models.GrantModificationNoteInternal grantModificationNoteInternal)
        {
            GrantModificationNoteInternalText = grantModificationNoteInternal.GrantModificationNoteInternalText;

        }

        public void UpdateModel(Models.GrantModificationNoteInternal grantModificationNoteInternal, Person currentPerson, EditGrantModificationNoteInternalType editGrantModificationNoteType)
        {
            if (editGrantModificationNoteType == EditGrantModificationNoteInternalTypeNewNote.Instance)
            {
                grantModificationNoteInternal.CreatedByPerson = currentPerson;
                grantModificationNoteInternal.CreatedDate = DateTime.Now;
            }
            else
            {
                grantModificationNoteInternal.LastUpdatedByPerson = currentPerson;
                grantModificationNoteInternal.LastUpdatedDate = DateTime.Now;
            }

            grantModificationNoteInternal.GrantModificationNoteInternalText = GrantModificationNoteInternalText;

        }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (string.IsNullOrWhiteSpace(GrantModificationNoteInternalText))
            {
                yield return new SitkaValidationResult<EditGrantModificationNoteInternalViewModel, string>(
                    FirmaValidationMessages.GrantModificationNoteInternalIsEmptyText, m => m.GrantModificationNoteInternalText);
            }
        }
    }
}