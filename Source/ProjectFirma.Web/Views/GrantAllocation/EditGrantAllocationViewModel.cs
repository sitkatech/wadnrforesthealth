﻿/*-----------------------------------------------------------------------
<copyright file="EditProjectViewModel.cs" company="Tahoe Regional Planning Agency and Environmental Science Associates">
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
using LtInfo.Common.DesignByContract;
using LtInfo.Common.Models;
using ProjectFirma.Web.Common;
using ProjectFirma.Web.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using DocumentFormat.OpenXml.Office2013.Word;
using Person = ProjectFirma.Web.Models.Person;

namespace ProjectFirma.Web.Views.GrantAllocation
{
    public class EditGrantAllocationViewModel : FormViewModel, IValidatableObject
    {
        public int GrantAllocationID { get; set; }

        [FieldDefinitionDisplay(FieldDefinitionEnum.GrantModification)]
        [Required]
        public int GrantModificationID { get; set; }

        [StringLength(Models.GrantAllocation.FieldLengths.GrantAllocationName)]
        [FieldDefinitionDisplay(FieldDefinitionEnum.GrantAllocationName)]
        [Required]
        public string GrantAllocationName { get; set; }

        [FieldDefinitionDisplay(FieldDefinitionEnum.Organization)]
        [Required]
        public int? OrganizationID { get; set; }

        [FieldDefinitionDisplay(FieldDefinitionEnum.ProgramIndexProjectCode)]
        public List<ProgramIndexProjectCodeJson> ProgramIndexProjectCodeJsons { get; set; }

        [FieldDefinitionDisplay(FieldDefinitionEnum.FederalFundCode)]
        public int? FederalFundCodeID { get; set; }

        [FieldDefinitionDisplay(FieldDefinitionEnum.Division)]
        public int? DivisionID { get; set; }

        [FieldDefinitionDisplay(FieldDefinitionEnum.DNRUplandRegion)]
        public int? DNRUplandRegionID { get; set; }

        [FieldDefinitionDisplay(FieldDefinitionEnum.AllocationAmount)]
        public Money? AllocationAmount { get; set; }

        [StringLength(Models.GrantAllocationChangeLog.FieldLengths.GrantAllocationAmountNote)]
        [FieldDefinitionDisplay(FieldDefinitionEnum.GrantAllocationChangeLogNote)]
        public string GrantAllocationChangeLogNote { get; set; }

        [FieldDefinitionDisplay(FieldDefinitionEnum.GrantStartDate)]
        public DateTime? StartDate { get; set; }

        [FieldDefinitionDisplay(FieldDefinitionEnum.GrantEndDate)]
        public DateTime? EndDate { get; set; }

        [FieldDefinitionDisplay(FieldDefinitionEnum.ProgramManager)]
        public List<int> ProgramManagerPersonIDs { get; set; }

        [FieldDefinitionDisplay(FieldDefinitionEnum.GrantManager)]
        public int? GrantManagerID { get; set; }

        [FieldDefinitionDisplay(FieldDefinitionEnum.GrantAllocationPriority)]
        public int? PriorityID { get; set; }

        [FieldDefinitionDisplay(FieldDefinitionEnum.GrantAllocationFundFSPs)]
        public bool? FundFSPsBool { get; set; }

        public bool? LikelyToUsePeopleBool { get; set; }

        [FieldDefinitionDisplay(FieldDefinitionEnum.GrantAllocationLikelyToUse)]
        public List<int> LikelyToUsePeopleIds { get; set; }

        [FieldDefinitionDisplay(FieldDefinitionEnum.GrantAllocationSource)]
        public int? SourceID { get; set; }

        [DisplayName("File Upload")]
        [WADNRFileExtensions(FileResourceMimeTypeEnum.PDF, FileResourceMimeTypeEnum.ExcelXLSX, FileResourceMimeTypeEnum.xExcelXLSX, FileResourceMimeTypeEnum.ExcelXLS, FileResourceMimeTypeEnum.PowerpointPPT, FileResourceMimeTypeEnum.PowerpointPPTX, FileResourceMimeTypeEnum.WordDOC, FileResourceMimeTypeEnum.WordDOCX, FileResourceMimeTypeEnum.TXT, FileResourceMimeTypeEnum.JPEG, FileResourceMimeTypeEnum.PNG)]
        public List<HttpPostedFileBase> GrantAllocationFileResourceDatas { get; set; }

        

        /// <summary>
        /// Needed by the ModelBinder
        /// </summary>
        public EditGrantAllocationViewModel()
        {
            ProgramIndexProjectCodeJsons = new List<ProgramIndexProjectCodeJson>();
        }

        public EditGrantAllocationViewModel(Models.GrantAllocation grantAllocation)
        {
            GrantAllocationID = grantAllocation.GrantAllocationID;
            GrantAllocationName = grantAllocation.GrantAllocationName;
            OrganizationID = grantAllocation.OrganizationID;
            GrantModificationID = grantAllocation.GrantModificationID;

            ProgramIndexProjectCodeJsons =
                ProgramIndexProjectCodeJson
                    .MakeProgramIndexProjectCodeJsonsFromGrantAllocationProgramIndexProjectCodes(grantAllocation.GrantAllocationProgramIndexProjectCodes.ToList());

            FederalFundCodeID = grantAllocation.FederalFundCodeID;
            DivisionID = grantAllocation.DivisionID;
            DNRUplandRegionID = grantAllocation.RegionIDDisplay;
            AllocationAmount = grantAllocation.AllocationAmount;
            StartDate = grantAllocation.StartDate;
            EndDate = grantAllocation.EndDate;
            ProgramManagerPersonIDs = grantAllocation.ProgramManagerPersonIDs;
            GrantManagerID = grantAllocation.GrantManagerID;
            PriorityID = grantAllocation.GrantAllocationPriorityID;
            FundFSPsBool = grantAllocation.HasFundFSPs;
            SourceID = grantAllocation.GrantAllocationSource?.GrantAllocationSourceID;
            LikelyToUsePeopleBool = grantAllocation.LikelyToUse;
            LikelyToUsePeopleIds = grantAllocation.GrantAllocationLikelyPeople.Select(x=>x.PersonID).ToList();

            //GrantAllocationFileResourceDatas = grantAllocation.GrantAllocationFileResources
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


            //validate programIndex and projectCodes, need to check for user entered values to confirm they are valid.
            if (ProgramIndexProjectCodeJsons != null)
            {
                foreach (var programIndexProjectCodePair in ProgramIndexProjectCodeJsons)
                {
                    if ((programIndexProjectCodePair.ProgramIndexID == null || programIndexProjectCodePair.ProgramIndexID < 1) &&
                        string.IsNullOrEmpty(programIndexProjectCodePair.ProgramIndexName))
                    {
                        yield return new SitkaValidationResult<EditGrantAllocationViewModel, List<ProgramIndexProjectCodeJson>>($"{Models.FieldDefinition.ProgramIndex.GetFieldDefinitionLabel()} cannot be blank.",
                            m => m.ProgramIndexProjectCodeJsons);
                    }else if (programIndexProjectCodePair.ProgramIndexID < 1 && !string.IsNullOrEmpty(programIndexProjectCodePair.ProgramIndexName))
                    {
                        //check user entered PI is valid and set programIndexID
                        var foundProgramIndex = HttpRequestStorage.DatabaseEntities.ProgramIndices.FirstOrDefault(pi => pi.ProgramIndexCode.ToUpper() == programIndexProjectCodePair.ProgramIndexName.ToUpper());
                        if (foundProgramIndex == null)
                        {
                            yield return new SitkaValidationResult<EditGrantAllocationViewModel, List<ProgramIndexProjectCodeJson>>($"{Models.FieldDefinition.ProgramIndex.GetFieldDefinitionLabel()}({programIndexProjectCodePair.ProgramIndexName}) is invalid.",
                                m => m.ProgramIndexProjectCodeJsons);
                        }
                        else
                        {
                            programIndexProjectCodePair.ProgramIndexID = foundProgramIndex.ProgramIndexID;
                            programIndexProjectCodePair.ProgramIndexName = foundProgramIndex.ProgramIndexCode;
                        }
                    }

                    if ((programIndexProjectCodePair.ProjectCodeID == null || programIndexProjectCodePair.ProjectCodeID < 1) && !string.IsNullOrEmpty(programIndexProjectCodePair.ProjectCodeName))
                    {
                        //check user entered PC is valid and set projectCodeID if it is
                        var foundProjectCode = HttpRequestStorage.DatabaseEntities.ProjectCodes.FirstOrDefault(pc => pc.ProjectCodeName.ToUpper() == programIndexProjectCodePair.ProjectCodeName.ToUpper());
                        if (foundProjectCode == null)
                        {
                            yield return new SitkaValidationResult<EditGrantAllocationViewModel, List<ProgramIndexProjectCodeJson>>($"{Models.FieldDefinition.ProjectCode.GetFieldDefinitionLabel()}({programIndexProjectCodePair.ProjectCodeName}) is invalid.", m => m.ProgramIndexProjectCodeJsons);
                        }
                        else
                        {
                            programIndexProjectCodePair.ProjectCodeID = foundProjectCode.ProjectCodeID;
                            programIndexProjectCodePair.ProjectCodeName = foundProjectCode.ProjectCodeName;
                        }

                    }
                }

            }

        }

        public void UpdateModel(Models.GrantAllocation grantAllocation, Person currentPerson)
        {
            grantAllocation.GrantAllocationName = GrantAllocationName;
            grantAllocation.OrganizationID = OrganizationID;
            //grantAllocation.GrantID = GrantID;
            
            grantAllocation.FederalFundCodeID = FederalFundCodeID;
            grantAllocation.DivisionID = DivisionID;
            grantAllocation.GrantAllocationPriorityID = PriorityID;
            grantAllocation.DNRUplandRegionID = DNRUplandRegionID;
            if (grantAllocation.AllocationAmount != AllocationAmount)
            {
                GrantAllocationChangeLog newChange = new GrantAllocationChangeLog(
                         grantAllocation,
                         currentPerson,
                         DateTime.Now
                    );
                newChange.GrantAllocationAmountOldValue = grantAllocation.AllocationAmount;
                newChange.GrantAllocationAmountNewValue = AllocationAmount;
                newChange.GrantAllocationAmountNote = GrantAllocationChangeLogNote;
            }
            grantAllocation.AllocationAmount = AllocationAmount;
            grantAllocation.StartDate = StartDate;
            grantAllocation.EndDate = EndDate;
            grantAllocation.GrantManagerID = GrantManagerID;
            grantAllocation.HasFundFSPs = FundFSPsBool;
            grantAllocation.GrantAllocationSourceID = SourceID;
            grantAllocation.LikelyToUse = LikelyToUsePeopleBool;

            // Deleting existing records
            grantAllocation.GrantAllocationProgramManagers.ToList().ForEach(gapm => gapm.DeleteFull(HttpRequestStorage.DatabaseEntities));
            grantAllocation.GrantAllocationProgramManagers = this.ProgramManagerPersonIDs != null ? this.ProgramManagerPersonIDs.Select(p => new GrantAllocationProgramManager(grantAllocation.GrantAllocationID, p)).ToList() : new List<GrantAllocationProgramManager>();

            if (GrantAllocationFileResourceDatas?[0] != null)
            {
                var fileResources = GrantAllocationFileResourceDatas.Select(fileData =>
                    FileResource.CreateNewFromHttpPostedFile(fileData, currentPerson));

                foreach (var fileResource in fileResources)
                {
                    HttpRequestStorage.DatabaseEntities.FileResources.Add(fileResource);
                    var grantAllocationFileResource = new GrantAllocationFileResource(grantAllocation, fileResource, fileResource.OriginalCompleteFileName);
                    grantAllocation.GrantAllocationFileResources.Add(grantAllocationFileResource);
                }
            }

            //delete existing GrantAllocationProgramIndexProjectCode records
            grantAllocation.GrantAllocationProgramIndexProjectCodes.ToList().ForEach(gapipc => gapipc.DeleteFull(HttpRequestStorage.DatabaseEntities));
            //create new rows of GrantAllocationProgramIndexProjectCode
            grantAllocation.GrantAllocationProgramIndexProjectCodes =
                ProgramIndexProjectCodeJsons.Where(gapipc => gapipc.ProgramIndexID != null).Select(gapipc =>
                    new GrantAllocationProgramIndexProjectCode(grantAllocation.GrantAllocationID, (int)gapipc.ProgramIndexID, gapipc.ProjectCodeID)).ToList();

            // Deleting existing records
            grantAllocation.GrantAllocationLikelyPeople.ToList().ForEach(galp => galp.DeleteFull(HttpRequestStorage.DatabaseEntities));
            if (LikelyToUsePeopleBool == true)
            {
                grantAllocation.GrantAllocationLikelyPeople = this.LikelyToUsePeopleIds != null
                    ? this.LikelyToUsePeopleIds
                        .Select(p => new GrantAllocationLikelyPerson(grantAllocation.GrantAllocationID, p)).ToList()
                    : new List<GrantAllocationLikelyPerson>();
            }


        }

        // Some fields can't be serialized to JSON which is needed for the Angular controller,
        // so this creates a clone without those fields
        public EditGrantAllocationViewModel SerializableClone()
        {
            var clone = new EditGrantAllocationViewModel
            {
                GrantAllocationID = GrantAllocationID,
                GrantModificationID = GrantModificationID,
                GrantAllocationName = GrantAllocationName,
                OrganizationID = OrganizationID,
                ProgramIndexProjectCodeJsons = ProgramIndexProjectCodeJsons,
                FederalFundCodeID = FederalFundCodeID,
                DivisionID = DivisionID,
                DNRUplandRegionID = DNRUplandRegionID,
                AllocationAmount = AllocationAmount,
                GrantAllocationChangeLogNote = GrantAllocationChangeLogNote,
                StartDate = StartDate,
                EndDate = EndDate,
                ProgramManagerPersonIDs = ProgramManagerPersonIDs,
                GrantManagerID = GrantManagerID,
                PriorityID = PriorityID,
                FundFSPsBool = FundFSPsBool,
                SourceID = SourceID,
                LikelyToUsePeopleIds = LikelyToUsePeopleIds,
                GrantAllocationFileResourceDatas = new List<HttpPostedFileBase>()
            };

            // HttpPostedFileBase cannot be serialize by the default serializer

            return clone;
        }
    }


    public class ProgramIndexProjectCodeJson
    {
        public int? ProgramIndexID { get; set; }
        public string ProgramIndexName { get; set; }

        public int? ProjectCodeID { get; set; }
        public string ProjectCodeName { get; set; }

        // For use by model binder
        public ProgramIndexProjectCodeJson()
        {
        }

        public ProgramIndexProjectCodeJson(Models.ProgramIndex programIndex)
        {
            this.ProgramIndexID = programIndex.ProgramIndexID;
            this.ProgramIndexName = programIndex.ProgramIndexCode;
        }

        public ProgramIndexProjectCodeJson(Models.ProgramIndex programIndex, Models.ProjectCode projectCode)
        {
            this.ProgramIndexID = programIndex.ProgramIndexID;
            this.ProgramIndexName = programIndex.ProgramIndexCode;

            this.ProjectCodeID = projectCode.ProjectCodeID;
            this.ProjectCodeName = projectCode.ProjectCodeName;

        }

        public static List<ProgramIndexProjectCodeJson> MakeProgramIndexProjectCodeJsonsFromGrantAllocationProgramIndexProjectCodes(List<Models.GrantAllocationProgramIndexProjectCode> grantAllocationProgramIndexProjectCodes)
        {
            return grantAllocationProgramIndexProjectCodes.Select(gapipc => gapipc.ProjectCode == null ? new ProgramIndexProjectCodeJson(gapipc.ProgramIndex) : new ProgramIndexProjectCodeJson(gapipc.ProgramIndex, gapipc.ProjectCode)).ToList();
        }


    }
}