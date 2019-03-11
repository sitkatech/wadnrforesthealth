/*-----------------------------------------------------------------------
<copyright file="EditViewModel.cs" company="Tahoe Regional Planning Agency and Sitka Technology Group">
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
using System.Linq;
using System.Web;
using ProjectFirma.Web.Common;
using ProjectFirma.Web.Models;
using LtInfo.Common;
using LtInfo.Common.Models;
using LtInfo.Common.Mvc;

namespace ProjectFirma.Web.Views.Organization
{
    public class EditViewModel : FormViewModel, IValidatableObject
    {
        public const int MaxLogoSizeInBytes = 1024 * 200;

        public int OrganizationID { get; set; }

        [Required]
        [StringLength(Models.Organization.FieldLengths.OrganizationName)]
        [DisplayName("Name")]
        public string OrganizationName { get; set; }

        [Required]
        [StringLength(Models.Organization.FieldLengths.OrganizationShortName)]
        [DisplayName("Short Name")]
        public string OrganizationShortName { get; set; }

        [FieldDefinitionDisplay(FieldDefinitionEnum.OrganizationType)]
        [Required]
        public int? OrganizationTypeID { get; set; }

        [FieldDefinitionDisplay(FieldDefinitionEnum.OrganizationPrimaryContact)]
        public int? PrimaryContactPersonID { get; set; }

        [DisplayName("Vendor")]
        public int? VendorID { get; set; }

        public string VendorDisplayName { get; set; }

        [Url]
        [DisplayName("Home Page")]
        public string OrganizationUrl { get; set; }

        [DisplayName("Is Active")]
        public bool IsActive { get; set; }

        [DisplayName("Logo")]
        [SitkaFileExtensions("jpg|jpeg|gif|png")]
        public HttpPostedFileBase LogoFileResourceData { get; set; }

        /// <summary>
        /// Needed by the ModelBinder
        /// </summary>
        public EditViewModel()
        {
        }

        public EditViewModel(Models.Organization organization)
        {
            OrganizationID = organization.OrganizationID;
            OrganizationName = organization.OrganizationName;
            OrganizationShortName = organization.OrganizationShortName;
            OrganizationTypeID = organization.OrganizationTypeID;
            PrimaryContactPersonID = organization.PrimaryContactPerson?.PersonID;
            OrganizationUrl = organization.OrganizationUrl;
            VendorID = organization.VendorID;
            if (organization.Vendor != null)
            {
                VendorDisplayName = organization.Vendor.GetVendorNameWithFullStatewideVendorNumber();
            }
            IsActive = organization.IsActive;
        }

        public void UpdateModel(Models.Organization organization, Person currentPerson)
        {
            organization.OrganizationName = OrganizationName;
            organization.OrganizationShortName = OrganizationShortName;
            organization.OrganizationTypeID = OrganizationTypeID.GetValueOrDefault(); // can never be null due to RequiredAttribute
            organization.IsActive = IsActive;
            organization.PrimaryContactPersonID = PrimaryContactPersonID;
            organization.OrganizationUrl = OrganizationUrl;
            organization.VendorID = VendorID;
            if (LogoFileResourceData != null)
            {
                organization.LogoFileResource = FileResource.CreateNewFromHttpPostedFileAndSave(LogoFileResourceData, currentPerson);    
            }
        }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (LogoFileResourceData != null && LogoFileResourceData.ContentLength > MaxLogoSizeInBytes)
            {
                var errorMessage = $"Logo is too large - must be less than {FileUtility.FormatBytes(MaxLogoSizeInBytes)}. Your logo was {FileUtility.FormatBytes(LogoFileResourceData.ContentLength)}.";
                yield return new SitkaValidationResult<EditViewModel, HttpPostedFileBase>(errorMessage,
                    x => x.LogoFileResourceData);
            }

            // Get org name and short name
            var existingOrganizationsWithSameName = HttpRequestStorage.DatabaseEntities.Organizations.Where(o => o.OrganizationName == OrganizationName && o.OrganizationID != OrganizationID).ToList();
            if (existingOrganizationsWithSameName.Any())
            {
                var errorMessage = $"This organization name {OrganizationName} is taken already.";
                yield return new SitkaValidationResult<EditViewModel, string>(errorMessage,
                    x => x.OrganizationName);
            }
            // Get org ShortName and short ShortName
            var existingOrganizationsWithSameShortName = HttpRequestStorage.DatabaseEntities.Organizations.Where(o => o.OrganizationShortName == OrganizationShortName && o.OrganizationID != OrganizationID).ToList();
            if (existingOrganizationsWithSameShortName.Any())
            {
                var errorMessage = $"This organization short name {OrganizationShortName} is taken already.";
                yield return new SitkaValidationResult<EditViewModel, string>(errorMessage,
                    x => x.OrganizationShortName);
            }
        }
    }
}
