using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using LtInfo.Common;
using LtInfo.Common.DesignByContract;
using LtInfo.Common.Models;
using ProjectFirma.Web.Common;
using ProjectFirma.Web.Models;

namespace ProjectFirma.Web.Views.Grant
{
    public class NewGrantViewModel : FormViewModel, IValidatableObject
    {
        public int GrantID { get; set; }

        [FieldDefinitionDisplay(FieldDefinitionEnum.Organization)]
        [Required]
        public int OrganizationID { get; set; }

        [FieldDefinitionDisplay(FieldDefinitionEnum.GrantStatus)]
        [Required]
        public int GrantStatusID { get; set; }

        [FieldDefinitionDisplay(FieldDefinitionEnum.GrantType)]
        public int? GrantTypeID { get; set; }

        [FieldDefinitionDisplay(FieldDefinitionEnum.GrantName)]
        [StringLength(Models.Grant.FieldLengths.GrantName)]
        [Required]
        public string GrantName { get; set; }

        [FieldDefinitionDisplay(FieldDefinitionEnum.GrantShortName)]
        [StringLength(Models.Grant.FieldLengths.ShortName)]
        public string GrantShortName { get; set; }

        [FieldDefinitionDisplay(FieldDefinitionEnum.GrantNumber)]
        [StringLength(Models.Grant.FieldLengths.GrantNumber)]
        public string GrantNumber { get; set; }

        [FieldDefinitionDisplay(FieldDefinitionEnum.CFDA)]
        [StringLength(Models.Grant.FieldLengths.CFDANumber)]
        public string CFDANumber { get; set; }

        [FieldDefinitionDisplay(FieldDefinitionEnum.GrantStartDate)]
        public DateTime? GrantStartDate { get; set; }

        [FieldDefinitionDisplay(FieldDefinitionEnum.GrantEndDate)]
        public DateTime? GrantEndDate { get; set; }

        [DisplayName("Grant File Upload")]
        [WADNRFileExtensions(FileResourceMimeTypeEnum.PDF, FileResourceMimeTypeEnum.ExcelXLSX, FileResourceMimeTypeEnum.xExcelXLSX, FileResourceMimeTypeEnum.ExcelXLS, FileResourceMimeTypeEnum.PowerpointPPT, FileResourceMimeTypeEnum.PowerpointPPTX, FileResourceMimeTypeEnum.WordDOC, FileResourceMimeTypeEnum.WordDOCX, FileResourceMimeTypeEnum.TXT, FileResourceMimeTypeEnum.JPEG, FileResourceMimeTypeEnum.PNG)]
        public List<HttpPostedFileBase> GrantFileResourceDatas { get; set; }

        /// <summary>
        /// Needed by the ModelBinder
        /// </summary>
        public NewGrantViewModel()
        {
        }

        public NewGrantViewModel(Models.Grant grant)
        {
            GrantName = grant.GrantName;
            GrantShortName = grant.ShortName;
            OrganizationID = grant.OrganizationID;
            GrantStatusID = grant.GrantStatusID;
            GrantTypeID = grant.GrantTypeID;
            GrantNumber = grant.GrantNumber;
            CFDANumber = grant.CFDANumber;
            GrantStartDate = grant.StartDate;
            GrantEndDate = grant.EndDate;
        }

        public void UpdateModel(Models.Grant grant, Person currentPerson)
        {
            grant.GrantName = GrantName;
            grant.ShortName = GrantShortName;
            grant.OrganizationID = OrganizationID;
            grant.GrantStatusID = GrantStatusID;
            grant.GrantTypeID = GrantTypeID;
            grant.GrantNumber = GrantNumber;
            grant.CFDANumber = CFDANumber;
            grant.StartDate = GrantStartDate;
            grant.EndDate = GrantEndDate;

            if (GrantFileResourceDatas != null)
            {
                // We allow for empty file resources to be posted - at least until such time as they become required.
                bool anyActualFileResourceDatasSupplied = GrantFileResourceDatas.Any(frd => frd != null);
                if (anyActualFileResourceDatasSupplied)
                {
                    foreach (var currentGrantFileResourceData in GrantFileResourceDatas)
                    {
                        Check.EnsureNotNull(currentGrantFileResourceData);

                        var fileResource = FileResource.CreateNewFromHttpPostedFile(currentGrantFileResourceData, currentPerson);
                        HttpRequestStorage.DatabaseEntities.FileResources.Add(fileResource);
                        var grantFileResource = new GrantFileResource(grant, fileResource, fileResource.OriginalCompleteFileName);
                        grant.GrantFileResources.Add(grantFileResource);
                    }
                }
            }
        }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (OrganizationID == 0)
            {
                yield return new SitkaValidationResult<EditGrantViewModel, int>(
                    FirmaValidationMessages.OrganizationNameUnique, m => m.OrganizationID);
            }
        }
    }
}