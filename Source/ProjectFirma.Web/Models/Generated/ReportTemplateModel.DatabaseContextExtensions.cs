//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[ReportTemplateModel]
using System.Collections.Generic;
using System.Linq;
using Z.EntityFramework.Plus;
using CodeFirstStoreFunctions;
using LtInfo.Common.DesignByContract;
using LtInfo.Common.Models;
using ProjectFirma.Web.Common;

namespace ProjectFirma.Web.Models
{
    public static partial class DatabaseContextExtensions
    {
        public static ReportTemplateModel GetReportTemplateModel(this IQueryable<ReportTemplateModel> reportTemplateModels, int reportTemplateModelID)
        {
            var reportTemplateModel = reportTemplateModels.SingleOrDefault(x => x.ReportTemplateModelID == reportTemplateModelID);
            Check.RequireNotNullThrowNotFound(reportTemplateModel, "ReportTemplateModel", reportTemplateModelID);
            return reportTemplateModel;
        }

        // Delete using an IDList (WADNR style)
        public static void DeleteReportTemplateModel(this IQueryable<ReportTemplateModel> reportTemplateModels, List<int> reportTemplateModelIDList)
        {
            if(reportTemplateModelIDList.Any())
            {
                var reportTemplateModelsInSourceCollectionToDelete = reportTemplateModels.Where(x => reportTemplateModelIDList.Contains(x.ReportTemplateModelID));
                foreach (var reportTemplateModelToDelete in reportTemplateModelsInSourceCollectionToDelete.ToList())
                {
                    reportTemplateModelToDelete.Delete(HttpRequestStorage.DatabaseEntities);
                }
            }
        }

        // Delete using an object list (WADNR style)
        public static void DeleteReportTemplateModel(this IQueryable<ReportTemplateModel> reportTemplateModels, ICollection<ReportTemplateModel> reportTemplateModelsToDelete)
        {
            if(reportTemplateModelsToDelete.Any())
            {
                var reportTemplateModelIDList = reportTemplateModelsToDelete.Select(x => x.ReportTemplateModelID).ToList();
                var reportTemplateModelsToDeleteFromSourceList = reportTemplateModels.Where(x => reportTemplateModelIDList.Contains(x.ReportTemplateModelID)).ToList();

                foreach (var reportTemplateModelToDelete in reportTemplateModelsToDeleteFromSourceList)
                {
                    reportTemplateModelToDelete.Delete(HttpRequestStorage.DatabaseEntities);
                }
            }
        }

        public static void DeleteReportTemplateModel(this IQueryable<ReportTemplateModel> reportTemplateModels, int reportTemplateModelID)
        {
            DeleteReportTemplateModel(reportTemplateModels, new List<int> { reportTemplateModelID });
        }

        public static void DeleteReportTemplateModel(this IQueryable<ReportTemplateModel> reportTemplateModels, ReportTemplateModel reportTemplateModelToDelete)
        {
            DeleteReportTemplateModel(reportTemplateModels, new List<ReportTemplateModel> { reportTemplateModelToDelete });
        }
    }
}