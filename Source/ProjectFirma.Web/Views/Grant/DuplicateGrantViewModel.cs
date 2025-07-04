﻿/*-----------------------------------------------------------------------
<copyright file="DuplicateGrantViewModel.cs" company="Tahoe Regional Planning Agency and Environmental Science Associates">
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
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web;
using ProjectFirma.Web.Common;
using ProjectFirma.Web.Models;
using LtInfo.Common;
using LtInfo.Common.Models;

namespace ProjectFirma.Web.Views.Grant
{
    public class DuplicateGrantViewModel : FormViewModel, IValidatableObject
    {
        public int GrantID { get; set; }

        [FieldDefinitionDisplay(FieldDefinitionEnum.GrantStatus)]
        [Required]
        public int GrantStatusID { get; set; }

        [FieldDefinitionDisplay(FieldDefinitionEnum.GrantName)]
        [StringLength(Models.Grant.FieldLengths.GrantName)]
        [Required]
        public string GrantName { get; set; }

        [FieldDefinitionDisplay(FieldDefinitionEnum.GrantNumber)]
        [StringLength(Models.Grant.FieldLengths.GrantNumber)]
        public string GrantNumber { get; set; }

        [FieldDefinitionDisplay(FieldDefinitionEnum.TotalAwardAmount)]
        [Required]
        public Money? GrantTotalAwardAmount { get; set; }

        [FieldDefinitionDisplay(FieldDefinitionEnum.GrantStartDate)]
        public DateTime? GrantStartDate { get; set; }

        [FieldDefinitionDisplay(FieldDefinitionEnum.GrantEndDate)]
        public DateTime? GrantEndDate { get; set; }

        public List<int> GrantAllocationsToDuplicate { get; set; }

        public int InitialAwardGrantModificationID { get; set; }

        /// <summary>
        /// Needed by the ModelBinder
        /// </summary>
        public DuplicateGrantViewModel()
        {
        }

        public DuplicateGrantViewModel(Models.Grant grantToDuplicate)
        {
            GrantName = $"{grantToDuplicate.GrantName} - Copy";
            GrantStatusID = grantToDuplicate.GrantStatusID;
            GrantNumber = grantToDuplicate.GrantNumber;
            GrantTotalAwardAmount = 0;
            GrantStartDate = grantToDuplicate.StartDate;
            GrantEndDate = grantToDuplicate.EndDate;


        }

        public void UpdateModel(Models.Grant grant)
        {
            grant.GrantNumber = GrantNumber;
            grant.StartDate = GrantStartDate;
            grant.EndDate = GrantEndDate;
            grant.GrantName = GrantName;

        }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            //if (OrganizationID == 0)
            //{
            //    yield return new SitkaValidationResult<DuplicateGrantViewModel, int>(
            //        FirmaValidationMessages.OrganizationNameUnique, m => m.OrganizationID);
            //}
            return new List<ValidationResult>();
        }
    }
}