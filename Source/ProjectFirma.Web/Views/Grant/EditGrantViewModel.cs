/*-----------------------------------------------------------------------
<copyright file="EditGrantViewModel.cs" company="Tahoe Regional Planning Agency and Environmental Science Associates">
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
    public class EditGrantViewModel : FormViewModel, IValidatableObject
    {
        public int GrantID { get; set; }

        [FieldDefinitionDisplay(FieldDefinitionEnum.Organization)]
        [Required]
        public int OrganizationID { get; set; }

        [FieldDefinitionDisplay(FieldDefinitionEnum.GrantStatus)]
        [Required]
        public int GrantStatusID { get; set; }

        [FieldDefinitionDisplay(FieldDefinitionEnum.GrantType)]
        public int? GrantTypeID { get; set; }

        [FieldDefinitionDisplay(FieldDefinitionEnum.GrantName)]
        [StringLength(Models.Grant.FieldLengths.GrantName)]
        [Required]
        public string GrantName { get; set; }

        [FieldDefinitionDisplay(FieldDefinitionEnum.GrantShortName)]
        [StringLength(Models.Grant.FieldLengths.ShortName)]
        public string GrantShortName { get; set; }

        [FieldDefinitionDisplay(FieldDefinitionEnum.GrantNumber)]
        [StringLength(Models.Grant.FieldLengths.GrantNumber)]
        public string GrantNumber { get; set; }

        [FieldDefinitionDisplay(FieldDefinitionEnum.CFDA)]
        [StringLength(Models.Grant.FieldLengths.CFDANumber)]
        public string CFDANumber { get; set; }

        [FieldDefinitionDisplay(FieldDefinitionEnum.TotalAwardAmount)]
        [Required]
        public Money? TotalAwardAmount { get; set; }

        [FieldDefinitionDisplay(FieldDefinitionEnum.GrantStartDate)]
        public DateTime? GrantStartDate { get; set; }

        [FieldDefinitionDisplay(FieldDefinitionEnum.GrantEndDate)]
        public DateTime? GrantEndDate { get; set; }

        /// <summary>
        /// Needed by the ModelBinder
        /// </summary>
        public EditGrantViewModel()
        {
        }

        public EditGrantViewModel(Models.Grant grant)
        {
            GrantName = grant.GrantName;
            GrantShortName = grant.ShortName;
            OrganizationID = grant.OrganizationID;
            GrantStatusID = grant.GrantStatusID;
            GrantTypeID = grant.GrantTypeID;
            GrantNumber = grant.GrantNumber;
            CFDANumber = grant.CFDANumber;
            TotalAwardAmount = grant.TotalAwardAmount;
            GrantStartDate = grant.StartDate;
            GrantEndDate = grant.EndDate;
            
        }

        public void UpdateModel(Models.Grant grant, Person currentPerson)
        {
            grant.GrantName = GrantName;
            grant.ShortName = GrantShortName;
            grant.OrganizationID = OrganizationID;
            grant.GrantStatusID = GrantStatusID;
            grant.GrantTypeID = GrantTypeID;
            grant.GrantNumber = GrantNumber;
            grant.CFDANumber = CFDANumber;
            //grant.AwardedFunds = TotalAwardAmount;
            grant.StartDate = GrantStartDate;
            grant.EndDate = GrantEndDate;

        }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (OrganizationID == 0)
            {
                yield return new SitkaValidationResult<EditGrantViewModel, int>(
                    FirmaValidationMessages.OrganizationNameUnique, m => m.OrganizationID);
            }
        }
    }
}