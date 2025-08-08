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

namespace ProjectFirma.Web.Views.FundSourceAllocation
{
    public class EditFundSourceAllocationNoteInternalViewModel : FormViewModel, IValidatableObject
    {
        public int FundSourceAllocationID { get; set; }

        public int FundSourceAllocationNoteInternalID { get; set; }

        [FieldDefinitionDisplay(FieldDefinitionEnum.FundSourceAllocationNoteInternal)]
        [Required]
        public string FundSourceAllocationNoteInternalText { get; set; }

       



        /// <summary>
        /// Needed by the ModelBinder
        /// </summary>
        public EditFundSourceAllocationNoteInternalViewModel()
        {
        }

        public EditFundSourceAllocationNoteInternalViewModel(Models.FundSourceAllocationNoteInternal fundSourceAllocationNoteInternal)
        {
            FundSourceAllocationNoteInternalText = fundSourceAllocationNoteInternal.FundSourceAllocationNoteInternalText;

        }

        public void UpdateModel(Models.FundSourceAllocationNoteInternal fundSourceAllocationNoteInternal, Person currentPerson, EditFundSourceAllocationNoteType editFundSourceAllocationNoteType)
        {
            if (editFundSourceAllocationNoteType == EditFundSourceAllocationNoteTypeNewNote.Instance)
            {
                fundSourceAllocationNoteInternal.CreatedByPerson = currentPerson;
                fundSourceAllocationNoteInternal.CreatedDate = DateTime.Now;
            }
            else
            {
                fundSourceAllocationNoteInternal.LastUpdatedByPerson = currentPerson;
                fundSourceAllocationNoteInternal.LastUpdatedDate = DateTime.Now;
            }

            fundSourceAllocationNoteInternal.FundSourceAllocationNoteInternalText = FundSourceAllocationNoteInternalText;

        }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (string.IsNullOrWhiteSpace(FundSourceAllocationNoteInternalText))
            {
                yield return new SitkaValidationResult<EditFundSourceAllocationNoteInternalViewModel, string>(
                    FirmaValidationMessages.FundSourceAllocationNoteIsEmptyText, m => m.FundSourceAllocationNoteInternalText);
            }
        }
    }
}