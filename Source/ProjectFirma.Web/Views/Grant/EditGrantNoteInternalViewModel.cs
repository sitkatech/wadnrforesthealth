/*-----------------------------------------------------------------------
<copyright file="EditGrantNoteInternalViewModel.cs" company="Tahoe Regional Planning Agency and Sitka Technology Group">
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

namespace ProjectFirma.Web.Views.Grant
{
    public class EditGrantNoteInternalViewModel : FormViewModel, IValidatableObject
    {
        public int GrantID { get; set; }

        public int GrantNoteInternalID { get; set; }

        [FieldDefinitionDisplay(FieldDefinitionEnum.GrantNoteInternal)]
        [Required]
        public string GrantNoteText { get; set; }





        /// <summary>
        /// Needed by the ModelBinder
        /// </summary>
        public EditGrantNoteInternalViewModel()
        {
        }

        public EditGrantNoteInternalViewModel(Models.GrantNoteInternal grantNoteInternaal)
        {
            GrantNoteText = grantNoteInternaal.GrantNoteText;

        }

        public void UpdateModel(Models.GrantNoteInternal grantNoteInternal, Person currentPerson, EditGrantNoteType editGrantNoteType)
        {
            if (editGrantNoteType == EditGrantNoteTypeNewNote.Instance)
            {
                grantNoteInternal.CreatedByPerson = currentPerson;
                grantNoteInternal.CreatedDate = DateTime.Now;
            }
            else
            {
                grantNoteInternal.LastUpdatedByPerson = currentPerson;
                grantNoteInternal.LastUpdatedDate = DateTime.Now;
            }

            grantNoteInternal.GrantNoteText = GrantNoteText;

        }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (string.IsNullOrWhiteSpace(GrantNoteText))
            {
                yield return new SitkaValidationResult<EditGrantNoteViewModel, string>(
                    FirmaValidationMessages.GrantNoteIsEmptyText, m => m.GrantNoteText);
            }
        }
    }
}