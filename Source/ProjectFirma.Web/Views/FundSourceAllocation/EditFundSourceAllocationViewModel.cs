/*-----------------------------------------------------------------------
<copyright file="EditFundSourceAllocationViewModel.cs" company="Tahoe Regional Planning Agency and Environmental Science Associates">
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

namespace ProjectFirma.Web.Views.FundSourceAllocation
{
    public class EditFundSourceAllocationViewModel : FormViewModel, IValidatableObject
    {
        public int FundSourceAllocationID { get; set; }

        [FieldDefinitionDisplay(FieldDefinitionEnum.FundSource)]
        [Required]
        public int FundSourceID { get; set; }

        [StringLength(Models.FundSourceAllocation.FieldLengths.FundSourceAllocationName)]
        [FieldDefinitionDisplay(FieldDefinitionEnum.FundSourceAllocationName)]
        [Required]
        public string FundSourceAllocationName { get; set; }

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

        [StringLength(Models.FundSourceAllocationChangeLog.FieldLengths.FundSourceAllocationAmountNote)]
        [FieldDefinitionDisplay(FieldDefinitionEnum.FundSourceAllocationChangeLogNote)]
        public string FundSourceAllocationChangeLogNote { get; set; }

        [FieldDefinitionDisplay(FieldDefinitionEnum.FundSourceStartDate)]
        public DateTime? StartDate { get; set; }

        [FieldDefinitionDisplay(FieldDefinitionEnum.FundSourceEndDate)]
        public DateTime? EndDate { get; set; }

        [FieldDefinitionDisplay(FieldDefinitionEnum.ProgramManager)]
        public List<int> ProgramManagerPersonIDs { get; set; }

        [FieldDefinitionDisplay(FieldDefinitionEnum.FundSourceManager)]
        public int? FundSourceManagerID { get; set; }

        [FieldDefinitionDisplay(FieldDefinitionEnum.FundSourceAllocationPriority)]
        public int? PriorityID { get; set; }

        [FieldDefinitionDisplay(FieldDefinitionEnum.FundSourceAllocationFundFSPs)]
        public bool? FundFSPsBool { get; set; }

        public bool? LikelyToUsePeopleBool { get; set; }

        [FieldDefinitionDisplay(FieldDefinitionEnum.FundSourceAllocationLikelyToUse)]
        public List<int> LikelyToUsePeopleIds { get; set; }

        [FieldDefinitionDisplay(FieldDefinitionEnum.FundSourceAllocationSource)]
        public int? SourceID { get; set; }

        [DisplayName("File Upload")]
        [WADNRFileExtensions(FileResourceMimeTypeEnum.PDF, FileResourceMimeTypeEnum.ExcelXLSX, FileResourceMimeTypeEnum.xExcelXLSX, FileResourceMimeTypeEnum.ExcelXLS, FileResourceMimeTypeEnum.PowerpointPPT, FileResourceMimeTypeEnum.PowerpointPPTX, FileResourceMimeTypeEnum.WordDOC, FileResourceMimeTypeEnum.WordDOCX, FileResourceMimeTypeEnum.TXT, FileResourceMimeTypeEnum.JPEG, FileResourceMimeTypeEnum.PNG)]
        public List<HttpPostedFileBase> FundSourceAllocationFileResourceDatas { get; set; }

        

        /// <summary>
        /// Needed by the ModelBinder
        /// </summary>
        public EditFundSourceAllocationViewModel()
        {
            ProgramIndexProjectCodeJsons = new List<ProgramIndexProjectCodeJson>();
        }

        public EditFundSourceAllocationViewModel(Models.FundSourceAllocation fundSourceAllocation)
        {
            FundSourceAllocationID = fundSourceAllocation.FundSourceAllocationID;
            FundSourceAllocationName = fundSourceAllocation.FundSourceAllocationName;
            OrganizationID = fundSourceAllocation.OrganizationID;
            FundSourceID = fundSourceAllocation.FundSourceID;

            ProgramIndexProjectCodeJsons =
                ProgramIndexProjectCodeJson
                    .MakeProgramIndexProjectCodeJsonsFromFundSourceAllocationProgramIndexProjectCodes(fundSourceAllocation.FundSourceAllocationProgramIndexProjectCodes.ToList());

            FederalFundCodeID = fundSourceAllocation.FederalFundCodeID;
            DivisionID = fundSourceAllocation.DivisionID;
            DNRUplandRegionID = fundSourceAllocation.RegionIDDisplay;
            AllocationAmount = fundSourceAllocation.AllocationAmount;
            StartDate = fundSourceAllocation.StartDate;
            EndDate = fundSourceAllocation.EndDate;
            ProgramManagerPersonIDs = fundSourceAllocation.ProgramManagerPersonIDs;
            FundSourceManagerID = fundSourceAllocation.FundSourceManagerID;
            PriorityID = fundSourceAllocation.FundSourceAllocationPriorityID;
            FundFSPsBool = fundSourceAllocation.HasFundFSPs;
            SourceID = fundSourceAllocation.FundSourceAllocationSource?.FundSourceAllocationSourceID;
            LikelyToUsePeopleBool = fundSourceAllocation.LikelyToUse;
            LikelyToUsePeopleIds = fundSourceAllocation.FundSourceAllocationLikelyPeople.Select(x=>x.PersonID).ToList();

            //FundSourceAllocationFileResourceDatas = fundSourceAllocation.FundSourceAllocationFileResources
        }

        public static int CountWordsSeparatedByWhitespaceOrCommaInString(string stringToCheck)
        {
            char[] delimiters = new char[] { ' ', ',', '\r', '\n' };
            int wordCount = stringToCheck.Split(delimiters, StringSplitOptions.RemoveEmptyEntries).Length;
            return wordCount;
        }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (FundSourceAllocationName == "")
            {
                yield return new SitkaValidationResult<EditFundSourceAllocationViewModel, string>(
                    FirmaValidationMessages.OrganizationNameUnique, m => m.FundSourceAllocationName);
            }


            //validate programIndex and projectCodes, need to check for user entered values to confirm they are valid.
            if (ProgramIndexProjectCodeJsons != null)
            {
                foreach (var programIndexProjectCodePair in ProgramIndexProjectCodeJsons)
                {
                    if ((programIndexProjectCodePair.ProgramIndexID == null || programIndexProjectCodePair.ProgramIndexID < 1) &&
                        string.IsNullOrEmpty(programIndexProjectCodePair.ProgramIndexName))
                    {
                        yield return new SitkaValidationResult<EditFundSourceAllocationViewModel, List<ProgramIndexProjectCodeJson>>($"{Models.FieldDefinition.ProgramIndex.GetFieldDefinitionLabel()} cannot be blank.",
                            m => m.ProgramIndexProjectCodeJsons);
                    }else if (programIndexProjectCodePair.ProgramIndexID < 1 && !string.IsNullOrEmpty(programIndexProjectCodePair.ProgramIndexName))
                    {
                        //check user entered PI is valid and set programIndexID
                        var foundProgramIndex = HttpRequestStorage.DatabaseEntities.ProgramIndices.FirstOrDefault(pi => pi.ProgramIndexCode.ToUpper() == programIndexProjectCodePair.ProgramIndexName.ToUpper());
                        if (foundProgramIndex == null)
                        {
                            yield return new SitkaValidationResult<EditFundSourceAllocationViewModel, List<ProgramIndexProjectCodeJson>>($"{Models.FieldDefinition.ProgramIndex.GetFieldDefinitionLabel()}({programIndexProjectCodePair.ProgramIndexName}) is invalid.",
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
                            yield return new SitkaValidationResult<EditFundSourceAllocationViewModel, List<ProgramIndexProjectCodeJson>>($"{Models.FieldDefinition.ProjectCode.GetFieldDefinitionLabel()}({programIndexProjectCodePair.ProjectCodeName}) is invalid.", m => m.ProgramIndexProjectCodeJsons);
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

        public void UpdateModel(Models.FundSourceAllocation fundSourceAllocation, Person currentPerson)
        {
            fundSourceAllocation.FundSourceAllocationName = FundSourceAllocationName;
            fundSourceAllocation.OrganizationID = OrganizationID;
            fundSourceAllocation.FundSourceID = FundSourceID;
            
            fundSourceAllocation.FederalFundCodeID = FederalFundCodeID;
            fundSourceAllocation.DivisionID = DivisionID;
            fundSourceAllocation.FundSourceAllocationPriorityID = PriorityID;
            fundSourceAllocation.DNRUplandRegionID = DNRUplandRegionID;
            if (fundSourceAllocation.AllocationAmount != AllocationAmount)
            {
                FundSourceAllocationChangeLog newChange = new FundSourceAllocationChangeLog(
                         fundSourceAllocation,
                         currentPerson,
                         DateTime.Now
                    );
                newChange.FundSourceAllocationAmountOldValue = fundSourceAllocation.AllocationAmount;
                newChange.FundSourceAllocationAmountNewValue = AllocationAmount;
                newChange.FundSourceAllocationAmountNote = FundSourceAllocationChangeLogNote;
            }
            fundSourceAllocation.AllocationAmount = AllocationAmount;
            fundSourceAllocation.StartDate = StartDate;
            fundSourceAllocation.EndDate = EndDate;
            fundSourceAllocation.FundSourceManagerID = FundSourceManagerID;
            fundSourceAllocation.HasFundFSPs = FundFSPsBool;
            fundSourceAllocation.FundSourceAllocationSourceID = SourceID;
            fundSourceAllocation.LikelyToUse = LikelyToUsePeopleBool;

            // Deleting existing records
            fundSourceAllocation.FundSourceAllocationProgramManagers.ToList().ForEach(gapm => gapm.DeleteFull(HttpRequestStorage.DatabaseEntities));
            fundSourceAllocation.FundSourceAllocationProgramManagers = this.ProgramManagerPersonIDs != null ? this.ProgramManagerPersonIDs.Select(p => new FundSourceAllocationProgramManager(fundSourceAllocation.FundSourceAllocationID, p)).ToList() : new List<FundSourceAllocationProgramManager>();

            if (FundSourceAllocationFileResourceDatas?[0] != null)
            {
                var fileResources = FundSourceAllocationFileResourceDatas.Select(fileData =>
                    FileResource.CreateNewFromHttpPostedFile(fileData, currentPerson));

                foreach (var fileResource in fileResources)
                {
                    HttpRequestStorage.DatabaseEntities.FileResources.Add(fileResource);
                    var fundSourceAllocationFileResource = new FundSourceAllocationFileResource(fundSourceAllocation, fileResource, fileResource.OriginalCompleteFileName);
                    fundSourceAllocation.FundSourceAllocationFileResources.Add(fundSourceAllocationFileResource);
                }
            }

            //delete existing FundSourceAllocationProgramIndexProjectCode records
            fundSourceAllocation.FundSourceAllocationProgramIndexProjectCodes.ToList().ForEach(gapipc => gapipc.DeleteFull(HttpRequestStorage.DatabaseEntities));
            //create new rows of FundSourceAllocationProgramIndexProjectCode
            fundSourceAllocation.FundSourceAllocationProgramIndexProjectCodes =
                ProgramIndexProjectCodeJsons.Where(gapipc => gapipc.ProgramIndexID != null).Select(gapipc =>
                    new FundSourceAllocationProgramIndexProjectCode(fundSourceAllocation.FundSourceAllocationID, (int)gapipc.ProgramIndexID, gapipc.ProjectCodeID)).ToList();

            // Deleting existing records
            fundSourceAllocation.FundSourceAllocationLikelyPeople.ToList().ForEach(galp => galp.DeleteFull(HttpRequestStorage.DatabaseEntities));
            if (LikelyToUsePeopleBool == true)
            {
                fundSourceAllocation.FundSourceAllocationLikelyPeople = this.LikelyToUsePeopleIds != null
                    ? this.LikelyToUsePeopleIds
                        .Select(p => new FundSourceAllocationLikelyPerson(fundSourceAllocation.FundSourceAllocationID, p)).ToList()
                    : new List<FundSourceAllocationLikelyPerson>();
            }


        }

        // Some fields can't be serialized to JSON which is needed for the Angular controller,
        // so this creates a clone without those fields
        public EditFundSourceAllocationViewModel SerializableClone()
        {
            var clone = new EditFundSourceAllocationViewModel
            {
                FundSourceAllocationID = FundSourceAllocationID,
                FundSourceID = FundSourceID,
                FundSourceAllocationName = FundSourceAllocationName,
                OrganizationID = OrganizationID,
                ProgramIndexProjectCodeJsons = ProgramIndexProjectCodeJsons,
                FederalFundCodeID = FederalFundCodeID,
                DivisionID = DivisionID,
                DNRUplandRegionID = DNRUplandRegionID,
                AllocationAmount = AllocationAmount,
                FundSourceAllocationChangeLogNote = FundSourceAllocationChangeLogNote,
                StartDate = StartDate,
                EndDate = EndDate,
                ProgramManagerPersonIDs = ProgramManagerPersonIDs,
                FundSourceManagerID = FundSourceManagerID,
                PriorityID = PriorityID,
                FundFSPsBool = FundFSPsBool,
                SourceID = SourceID,
                LikelyToUsePeopleIds = LikelyToUsePeopleIds,
                FundSourceAllocationFileResourceDatas = new List<HttpPostedFileBase>()
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

        public static List<ProgramIndexProjectCodeJson> MakeProgramIndexProjectCodeJsonsFromFundSourceAllocationProgramIndexProjectCodes(List<Models.FundSourceAllocationProgramIndexProjectCode> fundSourceAllocationProgramIndexProjectCodes)
        {
            return fundSourceAllocationProgramIndexProjectCodes.Select(gapipc => gapipc.ProjectCode == null ? new ProgramIndexProjectCodeJson(gapipc.ProgramIndex) : new ProgramIndexProjectCodeJson(gapipc.ProgramIndex, gapipc.ProjectCode)).ToList();
        }


    }
}