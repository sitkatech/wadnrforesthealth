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
using ProjectFirma.Web.Views.Vendor;

namespace ProjectFirma.Web.Views.Program
{
    public class EditViewModel : FormViewModel, IValidatableObject
    {
        public int ProgramID { get; set; }

        [StringLength(Models.Program.FieldLengths.ProgramName)]
        [DisplayName("Name")]
        public string ProgramName { get; set; }

        [StringLength(Models.Organization.FieldLengths.OrganizationShortName)]
        [DisplayName("Short Name")]
        public string ProgramShortName { get; set; }

        [DisplayName("Parent Organization")]
        [Required]
        public int? OrganizationID { get; set; }

        [DisplayName("Primary Contact")]
        public int? PrimaryContactPersonID { get; set; }

        [DisplayName("Is Active")]
        public bool IsActive { get; set; }

        [DisplayName("Is Default for Bulk Import Only")]
        public bool IsDefaultForBulkImportOnly { get; set; }

        /// <summary>
        /// Needed by the ModelBinder
        /// </summary>
        public EditViewModel()
        {
        }

        public EditViewModel(Models.Program program)
        {
            ProgramID = program.ProgramID;
            ProgramName = program.ProgramName;
            ProgramShortName = program.ProgramShortName;
            OrganizationID = program.OrganizationID;
            PrimaryContactPersonID = program.ProgramPrimaryContactPerson?.PersonID;
            IsActive = program.ProgramIsActive;
            IsDefaultForBulkImportOnly = program.IsDefaultProgramForImportOnly;
        }

        public void UpdateModel(Models.Program program, Person currentPerson, bool isNew)
        {
            program.ProgramName = IsDefaultForBulkImportOnly ? null : ProgramName;
            program.ProgramShortName = IsDefaultForBulkImportOnly ? null : ProgramShortName;
            program.OrganizationID = OrganizationID.GetValueOrDefault();
            program.ProgramIsActive = IsActive;
            program.IsDefaultProgramForImportOnly = IsDefaultForBulkImportOnly;
            if (isNew)
            {
                program.ProgramCreateDate = DateTime.Now;
                program.ProgramCreatePersonID = currentPerson.PersonID;
            }
            else
            {
                program.ProgramLastUpdatedDate = DateTime.Now;
                program.ProgramLastUpdatedByPersonID = currentPerson.PersonID;
            }

            program.ProgramPrimaryContactPersonID = PrimaryContactPersonID;

        }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {


            if (!IsDefaultForBulkImportOnly)
            {
                if (string.IsNullOrEmpty(ProgramName))
                {
                    var errorMessage = $"Program Name is required when this program is not set to default for bulk import only";
                    yield return new SitkaValidationResult<EditViewModel, string>(errorMessage,
                        x => x.ProgramName);
                }

                if (string.IsNullOrEmpty(ProgramShortName))
                {
                    var errorMessage = $"Program Short Name is required when this program is not set to default for bulk import only";
                    yield return new SitkaValidationResult<EditViewModel, string>(errorMessage,
                        x => x.ProgramShortName);
                }

                // Get org name and short name
                var existingProgramsWithSameName = HttpRequestStorage.DatabaseEntities.Programs.Where(o => o.OrganizationID == OrganizationID && o.ProgramName == ProgramName && o.ProgramID != ProgramID).ToList();
                if (existingProgramsWithSameName.Any())
                {
                    var errorMessage = $"This {Models.FieldDefinition.Program.GetFieldDefinitionLabel()} name {ProgramName} is taken already.";
                    yield return new SitkaValidationResult<EditViewModel, string>(errorMessage,
                        x => x.ProgramName);
                }
                // Get org ShortName and short ShortName
                var existingProgramsWithSameShortName = HttpRequestStorage.DatabaseEntities.Programs.Where(o => o.OrganizationID == OrganizationID && o.ProgramShortName == ProgramShortName && o.ProgramID != ProgramID).ToList();
                if (existingProgramsWithSameShortName.Any())
                {
                    var errorMessage = $"This {Models.FieldDefinition.Program.GetFieldDefinitionLabel()} short name {ProgramShortName} is taken already.";
                    yield return new SitkaValidationResult<EditViewModel, string>(errorMessage,
                        x => x.ProgramShortName);
                }
            }
            
        }
    }
}
