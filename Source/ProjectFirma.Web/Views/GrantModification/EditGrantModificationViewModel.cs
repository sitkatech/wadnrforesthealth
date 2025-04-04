/*-----------------------------------------------------------------------
<copyright file="EditGrantModificationViewModel.cs" company="Tahoe Regional Planning Agency and Environmental Science Associates">
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
using System.Linq;
using System.Web;
using LtInfo.Common;
using LtInfo.Common.Models;
using ProjectFirma.Web.Common;
using ProjectFirma.Web.Models;

namespace ProjectFirma.Web.Views.GrantModification
{
    public class EditGrantModificationViewModel : FormViewModel, IValidatableObject
    {
        public int GrantModificationID { get; set; }
        public int GrantID { get; set; }

        [FieldDefinitionDisplay(FieldDefinitionEnum.GrantModificationStatus)]
        [Required]
        public int GrantModificationStatusID { get; set; }

        [FieldDefinitionDisplay(FieldDefinitionEnum.GrantModificationName)]
        [StringLength(Models.GrantModification.FieldLengths.GrantModificationName)]
        [Required]
        public string GrantModificationName { get; set; }

        [FieldDefinitionDisplay(FieldDefinitionEnum.GrantModificationAmount)]
        [Required]
        [ValidateMoneyInRangeForSqlServer]
        public Money GrantModificationAmount { get; set; }

        [FieldDefinitionDisplay(FieldDefinitionEnum.GrantModificationDescription)]
        public string GrantModificationDescription { get; set; }

        [FieldDefinitionDisplay(FieldDefinitionEnum.GrantModificationStartDate)]
        public DateTime GrantModificationStartDate { get; set; }

        [FieldDefinitionDisplay(FieldDefinitionEnum.GrantModificationEndDate)]
        public DateTime GrantModificationEndDate { get; set; }

        [DisplayName("File Upload")]
        [WADNRFileExtensions(FileResourceMimeTypeEnum.PDF, FileResourceMimeTypeEnum.ExcelXLSX, FileResourceMimeTypeEnum.xExcelXLSX, FileResourceMimeTypeEnum.ExcelXLS, FileResourceMimeTypeEnum.PowerpointPPT, FileResourceMimeTypeEnum.PowerpointPPTX, FileResourceMimeTypeEnum.WordDOC, FileResourceMimeTypeEnum.WordDOCX, FileResourceMimeTypeEnum.TXT, FileResourceMimeTypeEnum.JPEG, FileResourceMimeTypeEnum.PNG)]
        public List<HttpPostedFileBase> GrantModificationFileResourceData { get; set; }

        [Required]
        [FieldDefinitionDisplay(FieldDefinitionEnum.GrantModificationPurpose)]
        public List<int> GrantModificationPurposeIDs { get; set; }



        /// <summary>
        /// Needed by the ModelBinder
        /// </summary>
        public EditGrantModificationViewModel()
        {
        }

        public EditGrantModificationViewModel(Models.Grant grantToAssociate)
        {
            GrantID = grantToAssociate.GrantID;
            GrantModificationStartDate = grantToAssociate.StartDate ?? DateTime.Now;
            GrantModificationEndDate = grantToAssociate.EndDate ?? DateTime.Now;
        }

        public EditGrantModificationViewModel(Models.GrantModification grantModification)
        {
            GrantModificationName = grantModification.GrantModificationName;
            GrantModificationStatusID = grantModification.GrantModificationStatusID;
            GrantModificationStartDate = grantModification.GrantModificationStartDate;
            GrantModificationEndDate = grantModification.GrantModificationEndDate;
            GrantModificationDescription = grantModification.GrantModificationDescription;
            GrantModificationAmount = grantModification.GrantModificationAmount;
            GrantID = grantModification.GrantID;
            GrantModificationPurposeIDs = grantModification.GrantModificationGrantModificationPurposes.Select(x => x.GrantModificationPurposeID).ToList();
        }

        public void UpdateModel(Models.GrantModification grantModification, Person currentPerson, List<GrantModificationGrantModificationPurpose> allGrantModificationGrantModificationPurposes)
        {
            grantModification.GrantModificationName = GrantModificationName;
            grantModification.GrantModificationStatusID = GrantModificationStatusID;
            grantModification.GrantModificationStartDate = GrantModificationStartDate;
            grantModification.GrantModificationEndDate = GrantModificationEndDate;
            grantModification.GrantModificationDescription = GrantModificationDescription;
            grantModification.GrantID = GrantID;
            grantModification.GrantModificationAmount = GrantModificationAmount;

            if (GrantModificationFileResourceData?[0] != null)
            {
                var fileResources = GrantModificationFileResourceData.Select(fileData =>
                    FileResource.CreateNewFromHttpPostedFile(fileData, currentPerson));

                foreach (var fileResource in fileResources)
                {
                    HttpRequestStorage.DatabaseEntities.FileResources.Add(fileResource);
                    var grantModificationFileResource = new GrantModificationFileResource(grantModification, fileResource, fileResource.OriginalCompleteFileName);
                    grantModification.GrantModificationFileResources.Add(grantModificationFileResource);
                }
            }

            var existingRecordsToClear = HttpRequestStorage.DatabaseEntities.GrantModificationGrantModificationPurposes
                .Where(gmgmp => gmgmp.GrantModificationID == grantModification.GrantModificationID).ToList();
            HttpRequestStorage.DatabaseEntities.GrantModificationGrantModificationPurposes.RemoveRange(existingRecordsToClear);

            var grantModificationPurposesUpdated = GrantModificationPurposeIDs.Select(x => new GrantModificationGrantModificationPurpose(grantModification.GrantModificationID, x)).ToList();
            HttpRequestStorage.DatabaseEntities.GrantModificationGrantModificationPurposes.AddRange(grantModificationPurposesUpdated);
        }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            return new List<ValidationResult>();
        }
    }
}