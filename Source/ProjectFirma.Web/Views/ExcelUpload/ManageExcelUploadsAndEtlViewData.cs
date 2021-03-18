using System.Collections.Generic;
using ProjectFirma.Web.Common;
using ProjectFirma.Web.Models;
using ProjectFirmaModels.Models;

namespace ProjectFirma.Web.Views.ExcelUpload
{
    public class ManageExcelUploadsAndEtlViewData : FirmaViewData
    {
        public string UploadLoaNortheastSpreadSheetUrl { get; set; }
        public string UploadLoaSoutheastSpreadSheetUrl { get; set; }
        public string UploadLoaFormID { get; set; }

        public string DoPublishingProcessingPostUrl { get; set; }

        public bool PublishingProcessingIsNeeded
        {
            get
            {
                bool fbmsProcessingIsNeeded = LatestTabularDataImportForLoaNortheast?.LastProcessedDate == null;
                bool pnBudgetProcessingIsNeeded = LatestTabularDataImportForLoaSoutheast?.LastProcessedDate == null;

                bool publishingProcessingIsNeeded = fbmsProcessingIsNeeded || pnBudgetProcessingIsNeeded;
                return publishingProcessingIsNeeded;
            }
        }

        public TabularDataImport LatestTabularDataImportForLoaNortheast;
        public TabularDataImport LatestTabularDataImportForLoaSoutheast;

        public ManageExcelUploadsAndEtlViewData(Person currentPerson,
                                       Models.FirmaPage firmaPage,
                                       string uploadLoaNortheastSpreadSheetUrl,
                                       string uploadLoaSoutheastSpreadSheetUrl,
                                       string doPublishingProcessingPostUrl,
                                       string uploadLoaFormID,
                                       List<TabularDataImport> tabularDataImports
                                       ) : base(currentPerson, firmaPage)
        {
            PageTitle = $"Upload Loa Tabular Data";
            UploadLoaNortheastSpreadSheetUrl = uploadLoaNortheastSpreadSheetUrl;
            UploadLoaSoutheastSpreadSheetUrl = uploadLoaSoutheastSpreadSheetUrl;
            UploadLoaFormID = uploadLoaFormID;
            DoPublishingProcessingPostUrl = doPublishingProcessingPostUrl;

            LatestTabularDataImportForLoaNortheast = TabularDataImport.GetLatestImportProcessingForGivenType(tabularDataImports, TabularDataImportTableType.LoaNortheast);
            LatestTabularDataImportForLoaSoutheast = TabularDataImport.GetLatestImportProcessingForGivenType(tabularDataImports, TabularDataImportTableType.LoaSoutheast);
        }

        public string GetLastLoaNortheastUploadedDateAndPersonString()
        {
            var lastLoaNortheastUploadDate = LatestTabularDataImportForLoaNortheast?.UploadDate;
            return lastLoaNortheastUploadDate != null ? $"{lastLoaNortheastUploadDate} - {LatestTabularDataImportForLoaNortheast.UploadPerson.FullNameFirstLast}" : "Unknown";
        }

        public string GetLastLoaSoutheastUploadedDateAndPersonString()
        {
            var lastLoaSoutheastUploadDate = LatestTabularDataImportForLoaSoutheast?.UploadDate;
            return lastLoaSoutheastUploadDate != null ? $"{lastLoaSoutheastUploadDate} - {LatestTabularDataImportForLoaNortheast.UploadPerson.FullNameFirstLast}" : "Unknown";
        }



        public string GetLastLoaNortheastLastProcessedDateAndPersonString()
        {
            var lastNortheastProcessedDate = LatestTabularDataImportForLoaNortheast?.LastProcessedDate;
            return lastNortheastProcessedDate != null ? $"{lastNortheastProcessedDate.ToString()} - {LatestTabularDataImportForLoaNortheast.LastProcessedPerson.FullNameFirstLast}" : "Unknown";
        }

        public string GetLastLoaSoutheastLastProcessedDateAndPersonString()
        {
            var lastSoutheastProcessedDate = LatestTabularDataImportForLoaSoutheast?.LastProcessedDate;
            return lastSoutheastProcessedDate != null ? $"{lastSoutheastProcessedDate.ToString()} - {LatestTabularDataImportForLoaSoutheast.LastProcessedPerson.FullNameFirstLast}" : "Unknown";

        }

    }
}