/*-----------------------------------------------------------------------
<copyright file="NewFileViewModel.cs" company="Tahoe Regional Planning Agency and Sitka Technology Group">
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

using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using LtInfo.Common.Models;
using ProjectFirma.Web.Common;
using ProjectFirma.Web.Models;

namespace ProjectFirma.Web.Views.Shared.FileResourceControls
{
    public class NewFileViewModel : FormViewModel, IValidatableObject
    {
        [DisplayName("New File Upload")]
        [WADNRFileExtensions(FileResourceMimeTypeEnum.PDF, FileResourceMimeTypeEnum.ExcelXLSX, FileResourceMimeTypeEnum.xExcelXLSX, FileResourceMimeTypeEnum.ExcelXLS, FileResourceMimeTypeEnum.PowerpointPPT, FileResourceMimeTypeEnum.PowerpointPPTX, FileResourceMimeTypeEnum.WordDOC, FileResourceMimeTypeEnum.WordDOCX, FileResourceMimeTypeEnum.TXT, FileResourceMimeTypeEnum.JPEG, FileResourceMimeTypeEnum.PNG)]
        public List<HttpPostedFileBase> FileResourcesData { get; set; }

        /// <summary>
        /// Needed by the ModelBinder
        /// </summary>
        public NewFileViewModel()
        {
            FileResourcesData = new List<HttpPostedFileBase>();
        }

        public void UpdateModel(ICanUploadNewFiles fileContainer, Person currentPerson)
        {
            if (FileResourcesData == null) return;

            var fileResources = FileResourcesData.Select(fileData =>
                FileResource.CreateNewFromHttpPostedFile(fileData, currentPerson));

            foreach (var fileResource in fileResources)
            {
                HttpRequestStorage.DatabaseEntities.FileResources.Add(fileResource);
                fileContainer.AddNewFileResource(fileResource);
            }
        }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            yield return null;
        }
    }
}
