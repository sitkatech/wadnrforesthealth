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
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Data.SqlTypes;
using System.Linq;
using System.Web;
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

        [FieldDefinitionDisplay(FieldDefinitionEnum.AgreementStatus)]
        [Required]
        public int? AgreemeentStatusID { get; set; }

        [FieldDefinitionDisplay(FieldDefinitionEnum.AgreementType)]
        [Required]
        public int AgreementTypeID { get; set; }

        [FieldDefinitionDisplay(FieldDefinitionEnum.Grant)]
        public int? GrantID { get; set; }

        [FieldDefinitionDisplay(FieldDefinitionEnum.ProgramIndex)]
        public List<ProgramIndex> ProgramIndices { get; }

        [FieldDefinitionDisplay(FieldDefinitionEnum.ProjectCode)]
        public string ProjectCode { get; }

        [FieldDefinitionDisplay(FieldDefinitionEnum.AgreementAmount)]
        public Money? AgreementAmount { get; set; }

        [FieldDefinitionDisplay(FieldDefinitionEnum.AgreementStartDate)]
        public DateTime? AgreementStartDate { get; set; }

        [FieldDefinitionDisplay(FieldDefinitionEnum.AgreementEndDate)]
        public DateTime? AgreementEndDate { get; set; }


        [FieldDefinitionDisplay(FieldDefinitionEnum.AgreementNotes)]
        public string AgreementNotes { get; set; }

        [DisplayName("Agreement File Upload")]
        //[SitkaFileExtensions("jpg|jpeg|gif|png")]
        public HttpPostedFileBase AgreementFileResourceData { get; set; }

        /// <summary>
        /// Needed by the ModelBinder
        /// </summary>
        public EditAgreementViewModel()
        {
        }

        public EditAgreementViewModel(Models.Agreement agreement)
        {
            AgreementID = agreement.AgreementID;
            AgreementTitle = agreement.AgreementTitle;
            AgreementNumber = agreement.AgreementNumber;
            OrganizationID = agreement.OrganizationID;
            AgreemeentStatusID = agreement.AgreementStatusID;
            AgreementTypeID = agreement.AgreementTypeID;
            GrantID = agreement.GrantID;
            AgreementAmount = agreement.AgreementAmount;
            AgreementStartDate = agreement.StartDate;
            AgreementEndDate = agreement.EndDate;
            AgreementNotes = agreement.Notes;
        }

        public void UpdateModel(Models.Agreement agreement, Person currentPerson)
        {
            agreement.AgreementTitle = AgreementTitle;
            agreement.AgreementNumber = AgreementNumber;
            agreement.OrganizationID = OrganizationID.Value;
            agreement.AgreementStatusID = AgreemeentStatusID.Value;
            agreement.AgreementTypeID = AgreementTypeID;
            agreement.GrantID = GrantID;
            agreement.AgreementAmount = AgreementAmount;
            agreement.StartDate = AgreementStartDate;
            agreement.EndDate = AgreementEndDate;
            agreement.Notes = AgreementNotes;
            if (AgreementFileResourceData != null)
            {
                var currentAgreementFileResource = agreement.AgreementFileResource;
                agreement.AgreementFileResource = null;
                // Delete old Agreement file, if present
                if (currentAgreementFileResource != null)
                {
                    HttpRequestStorage.DatabaseEntities.SaveChanges();
                    HttpRequestStorage.DatabaseEntities.FileResources.DeleteFileResource(currentAgreementFileResource);
                }
                agreement.AgreementFileResource = FileResource.CreateNewFromHttpPostedFileAndSave(AgreementFileResourceData, currentPerson);
            }
        }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var agreementTypes = HttpRequestStorage.DatabaseEntities.AgreementTypes;
            var mouAgreementType = agreementTypes.SingleOrDefault(x => string.Equals(x.AgreementTypeAbbrev, "MOU"));

            if (GrantID.HasValue && mouAgreementType != null && AgreementTypeID == mouAgreementType.AgreementTypeID)
            {
                yield return new SitkaValidationResult<EditAgreementViewModel, int?>(
                    $"If the Agreement Type is set to {mouAgreementType.AgreementTypeName} ({mouAgreementType.AgreementTypeAbbrev}) then Grant must be blank", m => m.GrantID);
            }

            var existingAgreement = HttpRequestStorage.DatabaseEntities.Agreements.FirstOrDefault(x => x.AgreementID == AgreementID);
            if (existingAgreement != null)
            {
                var existingAgreementGrantAllocationsGrantIDs = existingAgreement.AgreementGrantAllocations.Select(x => x.GrantID).Distinct().ToList();
                if (existingAgreementGrantAllocationsGrantIDs.Any())
                {
                    if (!GrantID.HasValue || !existingAgreementGrantAllocationsGrantIDs.Contains(GrantID.Value))
                    {
                        yield return new SitkaValidationResult<EditAgreementViewModel, int?>(
                            $"The Grant can only be changed when no Grant Allocations are associated to it. Please delete associated Grant Allocations",
                            m => m.GrantID);
                    }
                }
            }
            if (AgreementAmount.HasValue && mouAgreementType != null && AgreementTypeID == mouAgreementType.AgreementTypeID)
            {
                yield return new SitkaValidationResult<EditAgreementViewModel, Money?>(
                    $"If the Agreement Type is set to {mouAgreementType.AgreementTypeName} ({mouAgreementType.AgreementTypeAbbrev}) then Agreement Amount must be blank", m => m.AgreementAmount);
            }
            if (!GrantID.HasValue && (mouAgreementType == null || AgreementTypeID != mouAgreementType.AgreementTypeID))
            {
                yield return new SitkaValidationResult<EditAgreementViewModel, int?>(
                    $"A Grant must be selected if the Agreement Type is not MOU", m => m.GrantID);
            }
            if (AgreementAmount.HasValue && AgreementAmount > SqlMoney.MaxValue.Value)
            {
                yield return new SitkaValidationResult<EditAgreementViewModel, Money?>(
                    $"The Agreement Amount you entered exceeds the maximum. Please enter an amount less than ${SqlMoney.MaxValue.Value:C}", m => m.AgreementAmount);
            }
        }
    }
}