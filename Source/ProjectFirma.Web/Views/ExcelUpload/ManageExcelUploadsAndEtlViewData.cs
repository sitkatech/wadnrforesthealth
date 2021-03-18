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
                //bool fbmsProcessingIsNeeded = LatestImportProcessingForFbms?.LastProcessedDate == null;
                //bool pnBudgetProcessingIsNeeded = LatestImportProcessingForPnBudget?.LastProcessedDate == null;

                //bool publishingProcessingIsNeeded = fbmsProcessingIsNeeded || pnBudgetProcessingIsNeeded;
                //return publishingProcessingIsNeeded;
                return true;
            }
        }

        //public ImpProcessing LatestImportProcessingForFbms;
        //public ImpProcessing LatestImportProcessingForPnBudget;

        public ManageExcelUploadsAndEtlViewData(Person currentPerson,
                                       Models.FirmaPage firmaPage,
                                       string uploadLoaNortheastSpreadSheetUrl,
                                       string uploadLoaSoutheastSpreadSheetUrl,
                                       string doPublishingProcessingPostUrl,
                                       string uploadLoaFormID
                                       ) : base(currentPerson, firmaPage)
        {
            PageTitle = $"Upload Budget and Invoice Data";
            UploadLoaNortheastSpreadSheetUrl = uploadLoaNortheastSpreadSheetUrl;
            UploadLoaSoutheastSpreadSheetUrl = uploadLoaSoutheastSpreadSheetUrl;
            UploadLoaFormID = uploadLoaFormID;
            DoPublishingProcessingPostUrl = doPublishingProcessingPostUrl;

            //LatestImportProcessingForFbms = ImpProcessing.GetLatestImportProcessingForGivenType(HttpRequestStorage.DatabaseEntities, ImpProcessingTableType.FBMS);
            //LatestImportProcessingForPnBudget = ImpProcessing.GetLatestImportProcessingForGivenType(HttpRequestStorage.DatabaseEntities, ImpProcessingTableType.PNBudget);
        }

        public string GetLastLoaNortheastUploadedDateAndPersonString()
        {
            //var lastFbmsUploadDate = LatestImportProcessingForFbms?.UploadDate;
            //return lastFbmsUploadDate != null ? $"{lastFbmsUploadDate.ToString()} - {LatestImportProcessingForFbms.UploadPerson.GetFullNameFirstLast()}" : "Unknown";
            return string.Empty;
        }

        public string GetLastLoaSoutheastUploadedDateAndPersonString()
        {
            //var lastFbmsUploadDate = LatestImportProcessingForFbms?.UploadDate;
            //return lastFbmsUploadDate != null ? $"{lastFbmsUploadDate.ToString()} - {LatestImportProcessingForFbms.UploadPerson.GetFullNameFirstLast()}" : "Unknown";
            return string.Empty;
        }



        public string GetLastLoaNortheastLastProcessedDateAndPersonString()
        {
            //var lastFbmsProcessedDate = LatestImportProcessingForFbms?.LastProcessedDate;
            //return lastFbmsProcessedDate != null ? $"{lastFbmsProcessedDate.ToString()} - {LatestImportProcessingForFbms.LastProcessedPerson.GetFullNameFirstLast()}" : "Unknown";
            return string.Empty;
        }

        public string GetLastLoaSoutheastLastProcessedDateAndPersonString()
        {
            //var lastFbmsProcessedDate = LatestImportProcessingForFbms?.LastProcessedDate;
            //return lastFbmsProcessedDate != null ? $"{lastFbmsProcessedDate.ToString()} - {LatestImportProcessingForFbms.LastProcessedPerson.GetFullNameFirstLast()}" : "Unknown";
            return string.Empty;
        }

    }
}