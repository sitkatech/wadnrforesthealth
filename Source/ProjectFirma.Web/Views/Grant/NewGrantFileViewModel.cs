using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web;
using LtInfo.Common;
using LtInfo.Common.Models;
using ProjectFirma.Web.Common;
using ProjectFirma.Web.Models;

namespace ProjectFirma.Web.Views.Grant
{
    public class NewGrantFileViewModel : FormViewModel, IValidatableObject
    {
        public int GrantID { get; set; }

        [DisplayName("Grant File Upload")]
        [WADNRFileExtensions(FileResourceMimeTypeEnum.PDF, FileResourceMimeTypeEnum.ExcelXLSX, FileResourceMimeTypeEnum.xExcelXLSX, FileResourceMimeTypeEnum.ExcelXLS, FileResourceMimeTypeEnum.PowerpointPPT, FileResourceMimeTypeEnum.PowerpointPPTX, FileResourceMimeTypeEnum.WordDOC, FileResourceMimeTypeEnum.WordDOCX, FileResourceMimeTypeEnum.TXT, FileResourceMimeTypeEnum.JPEG, FileResourceMimeTypeEnum.PNG)]
        public List<HttpPostedFileBase> GrantFileResourceDatas { get; set; }

        /// <summary>
        /// Needed by the ModelBinder
        /// </summary>
        public NewGrantFileViewModel()
        {
        }

        public NewGrantFileViewModel(Models.Grant grant)
        {
            
        }

        public void UpdateModel(Models.Grant grant, Person currentPerson)
        {
            if (GrantFileResourceDatas != null)
            {
                for (int key = 0; key < GrantFileResourceDatas.Count; key++)
                {
                    var fileResource = FileResource.CreateNewFromHttpPostedFile(GrantFileResourceDatas[key], currentPerson);
                    HttpRequestStorage.DatabaseEntities.FileResources.Add(fileResource);
                    var grantFileResource = new GrantFileResource(grant, fileResource);
                    grant.GrantFileResources.Add(grantFileResource);
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