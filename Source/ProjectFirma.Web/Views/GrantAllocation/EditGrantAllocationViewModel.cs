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
using System.Linq;
using System.Web;
using ProjectFirma.Web.Common;
using ProjectFirma.Web.Models;
using LtInfo.Common;
using LtInfo.Common.DesignByContract;
using LtInfo.Common.Models;
using LtInfo.Common.Mvc;
using ProjectFirma.Web.Views.ProgramIndex;
using ProjectFirma.Web.Views.ProjectCode;

namespace ProjectFirma.Web.Views.GrantAllocation
{
    public class EditGrantAllocationViewModel : FormViewModel, IValidatableObject, IEditProjectCodeWithMultiselectViewModel, IEditProgramIndexViewModel
    {
        public int GrantAllocationID { get; set; }

        [FieldDefinitionDisplay(FieldDefinitionEnum.GrantAllocationName)]
        [Required]
        public string GrantAllocationName { get; set; }

        [FieldDefinitionDisplay(FieldDefinitionEnum.Organization)]
        [Required]
        public int? OrganizationID { get; set; }

        [FieldDefinitionDisplay(FieldDefinitionEnum.GrantNumber)]
        public int GrantID { get; set; }

        [FieldDefinitionDisplay(FieldDefinitionEnum.ProgramIndex)]
        public int? ProgramIndexID { get; set; }

        public string ProgramIndexSearchCriteria { get; set; }

        [FieldDefinitionDisplay(FieldDefinitionEnum.ProjectCode)]
        public string ProjectCodesString { get; set; }

        [FieldDefinitionDisplay(FieldDefinitionEnum.FederalFundCode)]
        public int? FederalFundCodeID { get; set; }

        [FieldDefinitionDisplay(FieldDefinitionEnum.Division)]
        public int? DivisionID { get; set; }

        [FieldDefinitionDisplay(FieldDefinitionEnum.Region)]
        public int? RegionID { get; set; }

        [FieldDefinitionDisplay(FieldDefinitionEnum.AllocationAmount)]
        public Money? AllocationAmount { get; set; }

        [FieldDefinitionDisplay(FieldDefinitionEnum.GrantStartDate)]
        public DateTime? StartDate { get; set; }

        [FieldDefinitionDisplay(FieldDefinitionEnum.GrantEndDate)]
        public DateTime? EndDate { get; set; }

        [FieldDefinitionDisplay(FieldDefinitionEnum.ProgramManager)]
        public List<int> ProgramManagerPersonIDs { get; set; }

        [FieldDefinitionDisplay(FieldDefinitionEnum.GrantManager)]
        public int? GrantManagerID { get; set; }

        [DisplayName("Grant Allocation File Upload")]
        [WADNRFileExtensions(FileResourceMimeTypeEnum.PDF, FileResourceMimeTypeEnum.ExcelXLSX, FileResourceMimeTypeEnum.xExcelXLSX, FileResourceMimeTypeEnum.ExcelXLS, FileResourceMimeTypeEnum.PowerpointPPT, FileResourceMimeTypeEnum.PowerpointPPTX, FileResourceMimeTypeEnum.WordDOC, FileResourceMimeTypeEnum.WordDOCX, FileResourceMimeTypeEnum.TXT, FileResourceMimeTypeEnum.JPEG, FileResourceMimeTypeEnum.PNG)]
        public HttpPostedFileBase GrantAllocationFileResourceData { get; set; }

        /// <summary>
        /// Needed by the ModelBinder
        /// </summary>
        public EditGrantAllocationViewModel()
        {
            
        }

        public EditGrantAllocationViewModel(Models.GrantAllocation grantAllocation)
        {
            GrantAllocationID = grantAllocation.GrantAllocationID;
            GrantAllocationName = grantAllocation.GrantAllocationName;
            OrganizationID = grantAllocation.OrganizationID;
            GrantID = grantAllocation.GrantID;
            ProgramIndexID = grantAllocation.ProgramIndexID;
            ProgramIndexSearchCriteria = grantAllocation.ProgramIndexDisplay;
            ProjectCodesString = grantAllocation.ProjectCodes.Any() ? grantAllocation.ProjectCodes.Select(pc => pc.ProjectCodeAbbrev).Aggregate((x, y) => x + ", " + y) : string.Empty;
            FederalFundCodeID = grantAllocation.FederalFundCodeID;
            DivisionID = grantAllocation.DivisionID;
            RegionID = grantAllocation.RegionIDDisplay;
            AllocationAmount = grantAllocation.AllocationAmount;
            StartDate = grantAllocation.StartDate;
            EndDate = grantAllocation.EndDate;
            ProgramManagerPersonIDs = grantAllocation.ProgramManagerPersonIDs;
            GrantManagerID = grantAllocation.GrantManagerID;
        }

        public static int CountWordsSeparatedByWhitespaceOrCommaInString(string stringToCheck)
        {
            char[] delimiters = new char[] { ' ', ',', '\r', '\n' };
            int wordCount = stringToCheck.Split(delimiters, StringSplitOptions.RemoveEmptyEntries).Length;
            return wordCount;
        }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (GrantAllocationName == "")
            {
                yield return new SitkaValidationResult<EditGrantAllocationViewModel, string>(
                    FirmaValidationMessages.OrganizationNameUnique, m => m.GrantAllocationName);
            }

            // If there is something entered by the user in the Program Index text field..
            if (!GeneralUtility.IsNullOrEmptyOrOnlyWhitespace(ProgramIndexSearchCriteria))
            {
                // .. Then ProgramIndex must have been looked up successfully. If this
                // failed, we don't have a valid ProgramIndex.
                if (ProgramIndexID == null)
                {
                    yield return new SitkaValidationResult<EditGrantAllocationViewModel, string>(
                        FirmaValidationMessages.ProgramIndexInvalid, m => m.ProgramIndexSearchCriteria);
                }
            }

            if (!GeneralUtility.IsNullOrEmptyOrOnlyWhitespace(ProjectCodesString))
            {
                // Count whitespace in original string. We do expect comma delimited input, but the user can type anything and they
                // may not play by the rules.
                int wordCountFromOriginalString = CountWordsSeparatedByWhitespaceOrCommaInString(ProjectCodesString);
                var parsedProjectCodes = Models.ProjectCode.GetListProjectCodesFromCommaDelimitedString(ProjectCodesString).ToList();

                bool noParsedProjectCodes = !parsedProjectCodes.Any();
                bool wordCountDoesNotMatch = wordCountFromOriginalString != parsedProjectCodes.Count;

                if (noParsedProjectCodes || wordCountDoesNotMatch)
                {
                    yield return new SitkaValidationResult<EditGrantAllocationViewModel, string>(
                        FirmaValidationMessages.ProjectCodeInvalid, m => m.ProjectCodesString);
                }
            }
        }

        public void UpdateModel(Models.GrantAllocation grantAllocation, Person currentPerson)
        {
            grantAllocation.GrantAllocationName = GrantAllocationName;
            grantAllocation.OrganizationID = OrganizationID;
            grantAllocation.GrantID = GrantID;
            grantAllocation.ProgramIndexID = ProgramIndexID;
            grantAllocation.ProjectCodes = Models.ProjectCode.GetListProjectCodesFromCommaDelimitedString(ProjectCodesString);
            grantAllocation.FederalFundCodeID = FederalFundCodeID;
            grantAllocation.DivisionID = DivisionID;
            grantAllocation.RegionID = RegionID;
            grantAllocation.AllocationAmount = AllocationAmount;
            grantAllocation.StartDate = StartDate;
            grantAllocation.EndDate = EndDate;
            grantAllocation.GrantManagerID = GrantManagerID;

            // Who is actually allowed to be a Program Manager for this Grant Allocation?
            List<Person> personsAllowedToBeProgramManager = new List<Person>();
            // Anyone who CURRENTLY has the role can keep it, even if their Person record has lost the permission in the meantime
            personsAllowedToBeProgramManager.AddRange(grantAllocation.GrantAllocationProgramManagers.Select(pm => pm.Person).ToList());
            // Also, anyone who has the right in the database is allowed
            personsAllowedToBeProgramManager.AddRange(HttpRequestStorage.DatabaseEntities.People.ToList().Where(p => p.IsProgramManager == true).ToList());
            personsAllowedToBeProgramManager = personsAllowedToBeProgramManager.Distinct().ToList();

            var personIDsAllowedToBeProgramManager = personsAllowedToBeProgramManager.Select(papm => papm.PersonID).ToList();
            var personIDsNotAllowed = new List<int>();
            if (this.ProgramManagerPersonIDs != null)
            {
                personIDsNotAllowed = this.ProgramManagerPersonIDs.Except(personIDsAllowedToBeProgramManager).ToList();
            }
            Check.Ensure(!personIDsNotAllowed.Any(), $"Found {personIDsNotAllowed.Count} PersonIDs not allowed to be Program Managers attempting to be saved: {string.Join(", ", personIDsNotAllowed)}");

            // Deleting existing records
            grantAllocation.GrantAllocationProgramManagers.ToList().ForEach(gapm => gapm.DeleteFull(HttpRequestStorage.DatabaseEntities));
            grantAllocation.GrantAllocationProgramManagers = this.ProgramManagerPersonIDs != null ? this.ProgramManagerPersonIDs.Select(p => new GrantAllocationProgramManager(grantAllocation.GrantAllocationID, p)).ToList() : new List<GrantAllocationProgramManager>();

            if (GrantAllocationFileResourceData != null)
            {
                var currentGrantAllocationFileResource = grantAllocation.GrantAllocationFileResource;
                grantAllocation.GrantAllocationFileResource = null;
                // Delete old grantAllocation file, if present
                if (currentGrantAllocationFileResource != null)
                {
                    HttpRequestStorage.DatabaseEntities.SaveChanges();
                    HttpRequestStorage.DatabaseEntities.FileResources.DeleteFileResource(currentGrantAllocationFileResource);
                }
                grantAllocation.GrantAllocationFileResource = FileResource.CreateNewFromHttpPostedFileAndSave(GrantAllocationFileResourceData, currentPerson);
            }
        }
    }
}