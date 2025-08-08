/*-----------------------------------------------------------------------
<copyright file="DuplicateFundSourceViewModel.cs" company="Tahoe Regional Planning Agency and Environmental Science Associates">
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

namespace ProjectFirma.Web.Views.FundSource
{
    public class DuplicateFundSourceViewModel : FormViewModel, IValidatableObject
    {
        public int FundSourceID { get; set; }

        [FieldDefinitionDisplay(FieldDefinitionEnum.FundSourceStatus)]
        [Required]
        public int FundSourceStatusID { get; set; }

        [FieldDefinitionDisplay(FieldDefinitionEnum.FundSourceName)]
        [StringLength(Models.FundSource.FieldLengths.FundSourceName)]
        [Required]
        public string FundSourceName { get; set; }

        [FieldDefinitionDisplay(FieldDefinitionEnum.FundSourceNumber)]
        [StringLength(Models.FundSource.FieldLengths.FundSourceNumber)]
        public string FundSourceNumber { get; set; }

        [FieldDefinitionDisplay(FieldDefinitionEnum.TotalAwardAmount)]
        [Required]
        public Money? FundSourceTotalAwardAmount { get; set; }

        [FieldDefinitionDisplay(FieldDefinitionEnum.FundSourceStartDate)]
        public DateTime? FundSourceStartDate { get; set; }

        [FieldDefinitionDisplay(FieldDefinitionEnum.FundSourceEndDate)]
        public DateTime? FundSourceEndDate { get; set; }

        public List<int> FundSourceAllocationsToDuplicate { get; set; }

        public int InitialAwardFundSourceModificationID { get; set; }

        /// <summary>
        /// Needed by the ModelBinder
        /// </summary>
        public DuplicateFundSourceViewModel()
        {
        }

        public DuplicateFundSourceViewModel(Models.FundSource fundSourceToDuplicate)
        {
            FundSourceName = $"{fundSourceToDuplicate.FundSourceName} - Copy";
            FundSourceStatusID = fundSourceToDuplicate.FundSourceStatusID;
            FundSourceNumber = fundSourceToDuplicate.FundSourceNumber;
            FundSourceTotalAwardAmount = 0;
            FundSourceStartDate = fundSourceToDuplicate.StartDate;
            FundSourceEndDate = fundSourceToDuplicate.EndDate;


        }

        public void UpdateModel(Models.FundSource fundSource)
        {
            fundSource.FundSourceNumber = FundSourceNumber;
            fundSource.StartDate = FundSourceStartDate;
            fundSource.EndDate = FundSourceEndDate;
            fundSource.FundSourceName = FundSourceName;

        }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            //if (OrganizationID == 0)
            //{
            //    yield return new SitkaValidationResult<DuplicateFundSourceViewModel, int>(
            //        FirmaValidationMessages.OrganizationNameUnique, m => m.OrganizationID);
            //}
            return new List<ValidationResult>();
        }
    }
}