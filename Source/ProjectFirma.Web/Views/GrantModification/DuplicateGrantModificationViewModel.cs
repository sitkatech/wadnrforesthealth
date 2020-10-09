/*-----------------------------------------------------------------------
<copyright file="DuplicateGrantModificationViewModel.cs" company="Tahoe Regional Planning Agency and Sitka Technology Group">
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

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using LtInfo.Common.Models;
using ProjectFirma.Web.Common;
using ProjectFirma.Web.Models;

namespace ProjectFirma.Web.Views.GrantModification
{
    public class DuplicateGrantModificationViewModel : FormViewModel, IValidatableObject
    {
        public int GrantID { get; set; }

        public int GrantModificationID { get; set; }

        [FieldDefinitionDisplay(FieldDefinitionEnum.GrantModificationStatus)]
        [Required]
        public int GrantModificationStatusID { get; set; }

        [FieldDefinitionDisplay(FieldDefinitionEnum.GrantModificationName)]
        [StringLength(Models.GrantModification.FieldLengths.GrantModificationName)]
        [Required]
        public string GrantModificationName { get; set; }

        [FieldDefinitionDisplay(FieldDefinitionEnum.GrantModificationAmount)]
        [Required]
        public Money? GrantModificationAmount { get; set; }

        //[FieldDefinitionDisplay(FieldDefinitionEnum.GrantStartDate)]
        //public DateTime? GrantStartDate { get; set; }

        //[FieldDefinitionDisplay(FieldDefinitionEnum.GrantEndDate)]
        //public DateTime? GrantEndDate { get; set; }

        [FieldDefinitionDisplay(FieldDefinitionEnum.GrantModificationPurpose)]
        [Required]
        public List<int> GrantModificationPurpose { get; set; }

        public List<int> GrantAllocationsToDuplicate { get; set; }


        /// <summary>
        /// Needed by the ModelBinder
        /// </summary>
        public DuplicateGrantModificationViewModel()
        {
        }

        public DuplicateGrantModificationViewModel(Models.GrantModification grantModificationToDuplicate)
        {
            GrantID = grantModificationToDuplicate.GrantID;
            GrantModificationStatusID = grantModificationToDuplicate.GrantModificationStatusID;
            GrantModificationName = $"{grantModificationToDuplicate.GrantModificationName} - Copy";
            GrantModificationAmount = 0;
            GrantModificationPurpose = grantModificationToDuplicate.GrantModificationGrantModificationPurposes.Select(x => x.GrantModificationPurposeID).ToList();

        }

        public void UpdateModel(Models.GrantModification grantModification)
        {
            //grantModification.GrantNumber = GrantNumber;
            //grantModification.StartDate = GrantStartDate;
            //grantModification.EndDate = GrantEndDate;
            //grantModification.GrantName = GrantName;

        }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            //if (OrganizationID == 0)
            //{
            //    yield return new SitkaValidationResult<DuplicateGrantModificationViewModel, int>(
            //        FirmaValidationMessages.OrganizationNameUnique, m => m.OrganizationID);
            //}
            return new List<ValidationResult>();
        }
    }
}