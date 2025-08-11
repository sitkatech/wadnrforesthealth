/*-----------------------------------------------------------------------
<copyright file="EditFundSourceNoteInternalViewModel.cs" company="Tahoe Regional Planning Agency and Environmental Science Associates">
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

namespace ProjectFirma.Web.Views.FundSource
{
    public class EditFundSourceNoteInternalViewModel : FormViewModel, IValidatableObject
    {
        public int FundSourceID { get; set; }

        public int FundSourceNoteInternalID { get; set; }

        [FieldDefinitionDisplay(FieldDefinitionEnum.FundSourceNoteInternal)]
        [Required]
        public string FundSourceNoteText { get; set; }





        /// <summary>
        /// Needed by the ModelBinder
        /// </summary>
        public EditFundSourceNoteInternalViewModel()
        {
        }

        public EditFundSourceNoteInternalViewModel(Models.FundSourceNoteInternal fundSourceNoteInternaal)
        {
            FundSourceNoteText = fundSourceNoteInternaal.FundSourceNoteText;

        }

        public void UpdateModel(Models.FundSourceNoteInternal fundSourceNoteInternal, Person currentPerson, EditFundSourceNoteType editFundSourceNoteType)
        {
            if (editFundSourceNoteType == EditFundSourceNoteTypeNewNote.Instance)
            {
                fundSourceNoteInternal.CreatedByPerson = currentPerson;
                fundSourceNoteInternal.CreatedDate = DateTime.Now;
            }
            else
            {
                fundSourceNoteInternal.LastUpdatedByPerson = currentPerson;
                fundSourceNoteInternal.LastUpdatedDate = DateTime.Now;
            }

            fundSourceNoteInternal.FundSourceNoteText = FundSourceNoteText;

        }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (string.IsNullOrWhiteSpace(FundSourceNoteText))
            {
                yield return new SitkaValidationResult<EditFundSourceNoteViewModel, string>(
                    FirmaValidationMessages.FundSourceNoteIsEmptyText, m => m.FundSourceNoteText);
            }
        }
    }
}