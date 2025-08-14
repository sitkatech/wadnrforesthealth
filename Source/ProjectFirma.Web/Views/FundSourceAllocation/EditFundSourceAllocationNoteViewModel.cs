/*-----------------------------------------------------------------------
<copyright file="EditFundSourceAllocationNoteViewModel.cs" company="Tahoe Regional Planning Agency and Environmental Science Associates">
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

namespace ProjectFirma.Web.Views.FundSourceAllocation
{
    public class EditFundSourceAllocationNoteViewModel : FormViewModel, IValidatableObject
    {
        public int FundSourceAllocationID { get; set; }

        public int FundSourceAllocationNoteID { get; set; }

        [FieldDefinitionDisplay(FieldDefinitionEnum.FundSourceAllocationNote)]
        [Required]
        public string FundSourceAllocationNoteText { get; set; }

       



        /// <summary>
        /// Needed by the ModelBinder
        /// </summary>
        public EditFundSourceAllocationNoteViewModel()
        {
        }

        public EditFundSourceAllocationNoteViewModel(Models.FundSourceAllocationNote fundSourceAllocationNote)
        {
            FundSourceAllocationNoteText = fundSourceAllocationNote.FundSourceAllocationNoteText;

        }

        public void UpdateModel(Models.FundSourceAllocationNote fundSourceAllocationNote, Person currentPerson, EditFundSourceAllocationNoteType editFundSourceAllocationNoteType)
        {
            if (editFundSourceAllocationNoteType == EditFundSourceAllocationNoteTypeNewNote.Instance)
            {
                fundSourceAllocationNote.CreatedByPerson = currentPerson;
                fundSourceAllocationNote.CreatedDate = DateTime.Now;
            }
            else
            {
                fundSourceAllocationNote.LastUpdatedByPerson = currentPerson;
                fundSourceAllocationNote.LastUpdatedDate = DateTime.Now;
            }

            fundSourceAllocationNote.FundSourceAllocationNoteText = FundSourceAllocationNoteText;

        }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (string.IsNullOrWhiteSpace(FundSourceAllocationNoteText))
            {
                yield return new SitkaValidationResult<EditFundSourceAllocationNoteViewModel, string>(
                    FirmaValidationMessages.FundSourceAllocationNoteIsEmptyText, m => m.FundSourceAllocationNoteText);
            }
        }
    }
}