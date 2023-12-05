/*-----------------------------------------------------------------------
<copyright file="EditGrantAllocationAwardViewModel.cs" company="Tahoe Regional Planning Agency and Environmental Science Associates">
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
using LtInfo.Common.Models;
using ProjectFirma.Web.Common;
using ProjectFirma.Web.Models;

namespace ProjectFirma.Web.Views.GrantAllocationAward
{
    public class EditGrantAllocationAwardViewModel : FormViewModel, IValidatableObject
    {
        public int GrantAllocationAwardID { get; set; }

        [Required]
        [FieldDefinitionDisplay(FieldDefinitionEnum.FocusArea)]
        public int FocusAreaID { get; set; }

        [FieldDefinitionDisplay(FieldDefinitionEnum.GrantAllocation)]
        [Required]
        public int GrantAllocationID { get; set; }

        [StringLength(Models.GrantAllocationAward.FieldLengths.GrantAllocationAwardName)]
        [FieldDefinitionDisplay(FieldDefinitionEnum.GrantAllocationAwardName)]
        [Required]
        public string GrantAllocationAwardName { get; set; }

        [FieldDefinitionDisplay(FieldDefinitionEnum.GrantAllocationAwardExpirationDate)]
        [Required]
        public DateTime GrantAllocationAwardExpirationDate { get; set; }

        [FieldDefinitionDisplay(FieldDefinitionEnum.GrantAllocationAwardCalendarStartYear)]
        [Required]
        public int GrantAllocationAwardCalendarStartYear { get; set; }



        /// <summary>
        /// Needed by the ModelBinder
        /// </summary>
        public EditGrantAllocationAwardViewModel()
        {
            GrantAllocationAwardExpirationDate = DateTime.Today;
            GrantAllocationAwardCalendarStartYear = DateTime.Today.Year;
        }

        public EditGrantAllocationAwardViewModel(Models.GrantAllocationAward grantAllocationAward)
        {
            GrantAllocationAwardID = grantAllocationAward.GrantAllocationAwardID;
            GrantAllocationAwardName = grantAllocationAward.GrantAllocationAwardName;
            GrantAllocationAwardExpirationDate = grantAllocationAward.GrantAllocationAwardExpirationDate == DateTime.MinValue ? DateTime.Today : grantAllocationAward.GrantAllocationAwardExpirationDate;
            GrantAllocationAwardCalendarStartYear = grantAllocationAward.GrantAllocationAwardCalendarStartYear;


            GrantAllocationID = grantAllocationAward.GrantAllocationID;
            FocusAreaID = grantAllocationAward.FocusAreaID;

        }



        public void UpdateModel(Models.GrantAllocationAward grantAllocationAward)
        {
            grantAllocationAward.GrantAllocationAwardID = GrantAllocationAwardID;
            grantAllocationAward.GrantAllocationAwardName = GrantAllocationAwardName;
            grantAllocationAward.GrantAllocationAwardExpirationDate = GrantAllocationAwardExpirationDate;
            grantAllocationAward.GrantAllocationAwardCalendarStartYear = GrantAllocationAwardCalendarStartYear;

            grantAllocationAward.GrantAllocationID = GrantAllocationID;
            grantAllocationAward.FocusAreaID = FocusAreaID;

        }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (GrantAllocationAwardCalendarStartYear < 1800 || GrantAllocationAwardCalendarStartYear > 2200)
            {
                yield return new ValidationResult($"{Models.FieldDefinition.GrantAllocationAwardCalendarStartYear.GetFieldDefinitionLabel()} must be a valid year.");
            }
        }
    }

}