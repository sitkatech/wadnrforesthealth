/*-----------------------------------------------------------------------
<copyright file="EditImportBasicsViewModel.cs" company="Tahoe Regional Planning Agency and Environmental Science Associates">
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

using LtInfo.Common;
using LtInfo.Common.Models;
using ProjectFirma.Web.Common;
using ProjectFirma.Web.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ProjectFirma.Web.Views.Program
{
    public class EditImportBasicsViewModel : FormViewModel, IValidatableObject
    {


        public int GisUploadSourceOrganizationID { get; set; }

        [StringLength(GisUploadSourceOrganization.FieldLengths.ProjectTypeDefaultName)]
        [DisplayName("Project Type Default Name")]
        public string ProjectTypeDefaultName { get; set; }

        [StringLength(GisUploadSourceOrganization.FieldLengths.TreatmentTypeDefaultName)]
        [DisplayName("Treatment Type Default Name")]
        public string TreatmentTypeDefaultName { get; set; }

        [DisplayName("Import Is Flattened")]
        public bool ImportIsFlattened { get; set; }

        [DisplayName("Adjust Project Type Based On Treatment Type")]
        public bool AdjustProjectTypeBasedOnTreatmentTypes { get; set; }

        [DisplayName("Project Stage Default")]
        [Required]
        public int ProjectStageDefaultID { get; set; }

        [DisplayName("Data Derive Project Stage")]
        public bool DataDeriveProjectStage { get; set; }

        [DisplayName("Default Lead Implementer Organization")]
        [Required]
        public int DefaultLeadImplementerOrganizationID { get; set; }

        [DisplayName("Relationship Type For Default Organization")]
        [Required]
        public int RelationshipTypeForDefaultOrganizationID { get; set; }

        [DisplayName("Import As Detailed Location Instead Of Treatments")]
        [Required]
        public bool ImportAsDetailedLocationInsteadOfTreatments { get; set; }

        [StringLength(GisUploadSourceOrganization.FieldLengths.ProjectDescriptionDefaultText)]
        [DisplayName("Project Description Default Text")]
        public string ProjectDescriptionDefaultText { get; set; }

        [DisplayName("Apply Completed Date To Project")]
        public bool ApplyCompletedDateToProject { get; set; }

        [DisplayName("Apply Start Date To Treatments")]
        public bool ApplyStartDateToTreatments { get; set; }

        [DisplayName("Apply End Date To Treatments")]
        public bool ApplyEndDateToTreatments { get; set; }

        [DisplayName("Apply Start Date To Project")]
        public bool ApplyStartDateToProject { get; set; }

        [DisplayName("Import As Detailed Location In Addition To Treatments")]
        public bool ImportAsDetailedLocationInAdditionToTreatments { get; set; }




        /// <summary>
        /// Needed by the ModelBinder
        /// </summary>
        public EditImportBasicsViewModel()
        {
        }

        public EditImportBasicsViewModel(Models.GisUploadSourceOrganization gisUploadSourceOrganization)
        {
            GisUploadSourceOrganizationID = gisUploadSourceOrganization.GisUploadSourceOrganizationID;
            ProjectTypeDefaultName = gisUploadSourceOrganization.ProjectTypeDefaultName;
            TreatmentTypeDefaultName = gisUploadSourceOrganization.TreatmentTypeDefaultName;
            ImportIsFlattened = gisUploadSourceOrganization.ImportIsFlattened ?? false;
            AdjustProjectTypeBasedOnTreatmentTypes = gisUploadSourceOrganization.AdjustProjectTypeBasedOnTreatmentTypes;
            ProjectStageDefaultID = gisUploadSourceOrganization.ProjectStageDefaultID;
            DataDeriveProjectStage = gisUploadSourceOrganization.DataDeriveProjectStage;
            DefaultLeadImplementerOrganizationID = gisUploadSourceOrganization.DefaultLeadImplementerOrganizationID;
            RelationshipTypeForDefaultOrganizationID = gisUploadSourceOrganization.RelationshipTypeForDefaultOrganizationID;
            ImportAsDetailedLocationInsteadOfTreatments = gisUploadSourceOrganization.ImportAsDetailedLocationInsteadOfTreatments;
            ProjectDescriptionDefaultText = gisUploadSourceOrganization.ProjectDescriptionDefaultText;
            ApplyCompletedDateToProject = gisUploadSourceOrganization.ApplyCompletedDateToProject;
            ApplyStartDateToTreatments = gisUploadSourceOrganization.ApplyStartDateToTreatments;
            ApplyEndDateToTreatments = gisUploadSourceOrganization.ApplyEndDateToTreatments;
            ApplyStartDateToProject = gisUploadSourceOrganization.ApplyStartDateToProject;
            ImportAsDetailedLocationInAdditionToTreatments = gisUploadSourceOrganization.ImportAsDetailedLocationInAdditionToTreatments;
        }

        public void UpdateModel(Models.GisUploadSourceOrganization gisUploadSourceOrganization, Person currentPerson)
        {
            gisUploadSourceOrganization.GisUploadSourceOrganizationID = GisUploadSourceOrganizationID;
            gisUploadSourceOrganization.ProjectTypeDefaultName = ProjectTypeDefaultName;
            gisUploadSourceOrganization.TreatmentTypeDefaultName = TreatmentTypeDefaultName;
            gisUploadSourceOrganization.ImportIsFlattened = ImportIsFlattened;
            gisUploadSourceOrganization.AdjustProjectTypeBasedOnTreatmentTypes = AdjustProjectTypeBasedOnTreatmentTypes;
            gisUploadSourceOrganization.ProjectStageDefaultID = ProjectStageDefaultID;
            gisUploadSourceOrganization.DataDeriveProjectStage = DataDeriveProjectStage;
            gisUploadSourceOrganization.DefaultLeadImplementerOrganizationID = DefaultLeadImplementerOrganizationID;
            gisUploadSourceOrganization.RelationshipTypeForDefaultOrganizationID = RelationshipTypeForDefaultOrganizationID;
            gisUploadSourceOrganization.ImportAsDetailedLocationInsteadOfTreatments = ImportAsDetailedLocationInsteadOfTreatments;
            gisUploadSourceOrganization.ProjectDescriptionDefaultText = ProjectDescriptionDefaultText;
            gisUploadSourceOrganization.ApplyCompletedDateToProject = ApplyCompletedDateToProject;
            gisUploadSourceOrganization.ApplyStartDateToTreatments = ApplyStartDateToTreatments;
            gisUploadSourceOrganization.ApplyEndDateToTreatments = ApplyEndDateToTreatments;
            gisUploadSourceOrganization.ApplyStartDateToProject = ApplyStartDateToProject;
            gisUploadSourceOrganization.ImportAsDetailedLocationInAdditionToTreatments = ImportAsDetailedLocationInAdditionToTreatments;

        }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {

            return new List<ValidationResult>();

        }
    }
}
