using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web;
using LtInfo.Common;
using LtInfo.Common.Models;
using ProjectFirma.Web.Common;
using ProjectFirma.Web.Models;

namespace ProjectFirma.Web.Views.GrantAllocation
{
    public class NewGrantAllocationFileViewModel : FormViewModel, IValidatableObject
    {
        public int GrantAllocationID { get; set; }

        [DisplayName("Grant Allocation File Upload")]
        [WADNRFileExtensions(FileResourceMimeTypeEnum.PDF, FileResourceMimeTypeEnum.ExcelXLSX, FileResourceMimeTypeEnum.xExcelXLSX, FileResourceMimeTypeEnum.ExcelXLS, FileResourceMimeTypeEnum.PowerpointPPT, FileResourceMimeTypeEnum.PowerpointPPTX, FileResourceMimeTypeEnum.WordDOC, FileResourceMimeTypeEnum.WordDOCX, FileResourceMimeTypeEnum.TXT, FileResourceMimeTypeEnum.JPEG, FileResourceMimeTypeEnum.PNG)]
        public List<HttpPostedFileBase> GrantAllocationFileResourceDatas { get; set; }

        /// <summary>
        /// Needed by the ModelBinder
        /// </summary>
        public NewGrantAllocationFileViewModel()
        {
        }

        public NewGrantAllocationFileViewModel(Models.GrantAllocation grantAllocation)
        {
            
        }

        public void UpdateModel(Models.GrantAllocation grantAllocation, Person currentPerson)
        {
            if (GrantAllocationFileResourceDatas != null)
            {
                for (int key = 0; key < GrantAllocationFileResourceDatas.Count; key++)
                {
                    var fileResource = FileResource.CreateNewFromHttpPostedFile(GrantAllocationFileResourceDatas[key], currentPerson);
                    HttpRequestStorage.DatabaseEntities.FileResources.Add(fileResource);
                    var grantAllocationFileResource = new GrantAllocationFileResource(grantAllocation, fileResource);
                    grantAllocation.GrantAllocationFileResources.Add(grantAllocationFileResource);
                }
            }
        }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            // No real validation cases here yet
            yield return null;
            
        }
    }
}