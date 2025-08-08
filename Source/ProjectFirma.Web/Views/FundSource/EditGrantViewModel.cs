/*-----------------------------------------------------------------------
<copyright file="EditFundSourceViewModel.cs" company="Tahoe Regional Planning Agency and Environmental Science Associates">
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
    public class EditFundSourceViewModel : FormViewModel, IValidatableObject
    {
        public int FundSourceID { get; set; }

        [FieldDefinitionDisplay(FieldDefinitionEnum.Organization)]
        [Required]
        public int OrganizationID { get; set; }

        [FieldDefinitionDisplay(FieldDefinitionEnum.FundSourceStatus)]
        [Required]
        public int FundSourceStatusID { get; set; }

        [FieldDefinitionDisplay(FieldDefinitionEnum.FundSourceType)]
        public int? FundSourceTypeID { get; set; }

        [FieldDefinitionDisplay(FieldDefinitionEnum.FundSourceName)]
        [StringLength(Models.FundSource.FieldLengths.FundSourceName)]
        [Required]
        public string FundSourceName { get; set; }

        [FieldDefinitionDisplay(FieldDefinitionEnum.FundSourceShortName)]
        [StringLength(Models.FundSource.FieldLengths.ShortName)]
        public string FundSourceShortName { get; set; }

        [FieldDefinitionDisplay(FieldDefinitionEnum.FundSourceNumber)]
        [StringLength(Models.FundSource.FieldLengths.FundSourceNumber)]
        public string FundSourceNumber { get; set; }

        [FieldDefinitionDisplay(FieldDefinitionEnum.CFDA)]
        [StringLength(Models.FundSource.FieldLengths.CFDANumber)]
        public string CFDANumber { get; set; }

        [FieldDefinitionDisplay(FieldDefinitionEnum.TotalAwardAmount)]
        [Required]
        public Money? TotalAwardAmount { get; set; }

        [FieldDefinitionDisplay(FieldDefinitionEnum.FundSourceStartDate)]
        public DateTime? FundSourceStartDate { get; set; }

        [FieldDefinitionDisplay(FieldDefinitionEnum.FundSourceEndDate)]
        public DateTime? FundSourceEndDate { get; set; }

        /// <summary>
        /// Needed by the ModelBinder
        /// </summary>
        public EditFundSourceViewModel()
        {
        }

        public EditFundSourceViewModel(Models.FundSource fundSource)
        {
            FundSourceName = fundSource.FundSourceName;
            FundSourceShortName = fundSource.ShortName;
            OrganizationID = fundSource.OrganizationID;
            FundSourceStatusID = fundSource.FundSourceStatusID;
            FundSourceTypeID = fundSource.FundSourceTypeID;
            FundSourceNumber = fundSource.FundSourceNumber;
            CFDANumber = fundSource.CFDANumber;
            TotalAwardAmount = fundSource.TotalAwardAmount;
            FundSourceStartDate = fundSource.StartDate;
            FundSourceEndDate = fundSource.EndDate;
            
        }

        public void UpdateModel(Models.FundSource fundSource, Person currentPerson)
        {
            fundSource.FundSourceName = FundSourceName;
            fundSource.ShortName = FundSourceShortName;
            fundSource.OrganizationID = OrganizationID;
            fundSource.FundSourceStatusID = FundSourceStatusID;
            fundSource.FundSourceTypeID = FundSourceTypeID;
            fundSource.FundSourceNumber = FundSourceNumber;
            fundSource.CFDANumber = CFDANumber;
            //fundSource.AwardedFunds = TotalAwardAmount;
            fundSource.StartDate = FundSourceStartDate;
            fundSource.EndDate = FundSourceEndDate;

        }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (OrganizationID == 0)
            {
                yield return new SitkaValidationResult<EditFundSourceViewModel, int>(
                    FirmaValidationMessages.OrganizationNameUnique, m => m.OrganizationID);
            }
        }
    }
}