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

namespace ProjectFirma.Web.Views.FundSource
{
    public class NewFundSourceViewModel : FormViewModel, IValidatableObject
    {
        public int FundSourceID { get; set; }

        [FieldDefinitionDisplay(FieldDefinitionEnum.Organization)]
        [Required]
        public int OrganizationID { get; set; }

        [FieldDefinitionDisplay(FieldDefinitionEnum.FundSourceStatus)]
        [Required]
        public int FundSourceStatusID { get; set; }

        [FieldDefinitionDisplay(FieldDefinitionEnum.FundSourceType)]
        public int? FundSourceTypeID { get; set; }

        [FieldDefinitionDisplay(FieldDefinitionEnum.FundSourceName)]
        [StringLength(Models.FundSource.FieldLengths.FundSourceName)]
        [Required]
        public string FundSourceName { get; set; }

        [FieldDefinitionDisplay(FieldDefinitionEnum.FundSourceShortName)]
        [StringLength(Models.FundSource.FieldLengths.ShortName)]
        public string FundSourceShortName { get; set; }

        [FieldDefinitionDisplay(FieldDefinitionEnum.FundSourceNumber)]
        [StringLength(Models.FundSource.FieldLengths.FundSourceNumber)]
        public string FundSourceNumber { get; set; }

        [FieldDefinitionDisplay(FieldDefinitionEnum.CFDA)]
        [StringLength(Models.FundSource.FieldLengths.CFDANumber)]
        public string CFDANumber { get; set; }

        [FieldDefinitionDisplay(FieldDefinitionEnum.FundSourceStartDate)]
        public DateTime? FundSourceStartDate { get; set; }

        [FieldDefinitionDisplay(FieldDefinitionEnum.FundSourceEndDate)]
        public DateTime? FundSourceEndDate { get; set; }

        [DisplayName("File Upload")]
        [WADNRFileExtensions(FileResourceMimeTypeEnum.PDF, FileResourceMimeTypeEnum.ExcelXLSX, FileResourceMimeTypeEnum.xExcelXLSX, FileResourceMimeTypeEnum.ExcelXLS, FileResourceMimeTypeEnum.PowerpointPPT, FileResourceMimeTypeEnum.PowerpointPPTX, FileResourceMimeTypeEnum.WordDOC, FileResourceMimeTypeEnum.WordDOCX, FileResourceMimeTypeEnum.TXT, FileResourceMimeTypeEnum.JPEG, FileResourceMimeTypeEnum.PNG)]
        public List<HttpPostedFileBase> FundSourceFileResourceDatas { get; set; }

        /// <summary>
        /// Needed by the ModelBinder
        /// </summary>
        public NewFundSourceViewModel()
        {
        }

        public NewFundSourceViewModel(Models.FundSource fundSource)
        {
            FundSourceName = fundSource.FundSourceName;
            FundSourceShortName = fundSource.ShortName;
            OrganizationID = fundSource.OrganizationID;
            FundSourceStatusID = fundSource.FundSourceStatusID;
            FundSourceTypeID = fundSource.FundSourceTypeID;
            FundSourceNumber = fundSource.FundSourceNumber;
            CFDANumber = fundSource.CFDANumber;
            FundSourceStartDate = fundSource.StartDate;
            FundSourceEndDate = fundSource.EndDate;
        }

        public void UpdateModel(Models.FundSource fundSource, Person currentPerson)
        {
            fundSource.FundSourceName = FundSourceName;
            fundSource.ShortName = FundSourceShortName;
            fundSource.OrganizationID = OrganizationID;
            fundSource.FundSourceStatusID = FundSourceStatusID;
            fundSource.FundSourceTypeID = FundSourceTypeID;
            fundSource.FundSourceNumber = FundSourceNumber;
            fundSource.CFDANumber = CFDANumber;
            fundSource.StartDate = FundSourceStartDate;
            fundSource.EndDate = FundSourceEndDate;

            if (FundSourceFileResourceDatas != null)
            {
                // We allow for empty file resources to be posted - at least until such time as they become required.
                bool anyActualFileResourceDatasSupplied = FundSourceFileResourceDatas.Any(frd => frd != null);
                if (anyActualFileResourceDatasSupplied)
                {
                    foreach (var currentFundSourceFileResourceData in FundSourceFileResourceDatas)
                    {
                        Check.EnsureNotNull(currentFundSourceFileResourceData);

                        var fileResource = FileResource.CreateNewFromHttpPostedFile(currentFundSourceFileResourceData, currentPerson);
                        HttpRequestStorage.DatabaseEntities.FileResources.Add(fileResource);
                        var fundSourceFileResource = new FundSourceFileResource(fundSource, fileResource, fileResource.OriginalCompleteFileName);
                        fundSource.FundSourceFileResources.Add(fundSourceFileResource);
                    }
                }
            }
        }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (OrganizationID == 0)
            {
                yield return new SitkaValidationResult<EditFundSourceViewModel, int>(
                    FirmaValidationMessages.OrganizationNameUnique, m => m.OrganizationID);
            }
        }
    }
}