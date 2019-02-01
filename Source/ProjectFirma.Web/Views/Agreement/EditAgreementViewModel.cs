/*-----------------------------------------------------------------------
<copyright file="EditProjectViewModel.cs" company="Tahoe Regional Planning Agency and Sitka Technology Group">
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
using ProjectFirma.Web.Common;
using ProjectFirma.Web.Models;
using LtInfo.Common;
using LtInfo.Common.Models;

namespace ProjectFirma.Web.Views.Agreement
{
    public class EditAgreementViewModel : FormViewModel, IValidatableObject
    {
        public int AgreementID { get; set; }

        [FieldDefinitionDisplay(FieldDefinitionEnum.AgreementTitle)]
        [Required]
        [StringLength(Models.Agreement.FieldLengths.AgreementTitle)]
        public string AgreementTitle { get; set; }

        [FieldDefinitionDisplay(FieldDefinitionEnum.AgreementNumber)]
        [Required]
        [StringLength(Models.Agreement.FieldLengths.AgreementNumber)]
        public string AgreementNumber { get; set; }

        [FieldDefinitionDisplay(FieldDefinitionEnum.Organization)]
        [Required]
        public int? OrganizationID { get; set; }

        [FieldDefinitionDisplay(FieldDefinitionEnum.AgreementType)]
        [Required]
        public int? AgreementTypeID { get; set; }

        [FieldDefinitionDisplay(FieldDefinitionEnum.Grant)]
        [Required]
        public int? GrantID { get; set; }

        [FieldDefinitionDisplay(FieldDefinitionEnum.AgreementAmount)]
        public Money? AgreementAmount { get; set; }

        [FieldDefinitionDisplay(FieldDefinitionEnum.AgreementStartDate)]
        public DateTime? AgreementStartDate { get; set; }

        [FieldDefinitionDisplay(FieldDefinitionEnum.AgreementEndDate)]
        public DateTime? AgreementEndDate { get; set; }

       



        /// <summary>
        /// Needed by the ModelBinder
        /// </summary>
        public EditAgreementViewModel()
        {
        }

        public EditAgreementViewModel(Models.Agreement agreement)
        {
            AgreementTitle = agreement.AgreementTitle;
            AgreementNumber = agreement.AgreementNumber;
            OrganizationID = agreement.OrganizationID;
            AgreementTypeID = agreement.AgreementTypeID;
            GrantID = agreement.GrantID;
            AgreementAmount = agreement.AgreementAmount;
            AgreementStartDate = agreement.StartDate;
            AgreementEndDate = agreement.EndDate;
        }

        public void UpdateModel(Models.Agreement agreement, Person currentPerson)
        {
            agreement.AgreementTitle = AgreementTitle;
            agreement.AgreementNumber = AgreementNumber;
            agreement.OrganizationID = OrganizationID.Value;
            agreement.AgreementTypeID = AgreementTypeID;
            agreement.GrantID = GrantID.Value;
            agreement.AgreementAmount = AgreementAmount;
            agreement.StartDate = AgreementStartDate;
            agreement.EndDate = AgreementEndDate;
        }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (OrganizationID == 0)
            {
                yield return new SitkaValidationResult<EditAgreementViewModel, int?>(
                    FirmaValidationMessages.OrganizationNameUnique, m => m.OrganizationID);
            }
        }
    }
}