//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[ReportTemplateModelType]
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
        public static ReportTemplateModelType GetReportTemplateModelType(this IQueryable<ReportTemplateModelType> reportTemplateModelTypes, int reportTemplateModelTypeID)
        {
            var reportTemplateModelType = reportTemplateModelTypes.SingleOrDefault(x => x.ReportTemplateModelTypeID == reportTemplateModelTypeID);
            Check.RequireNotNullThrowNotFound(reportTemplateModelType, "ReportTemplateModelType", reportTemplateModelTypeID);
            return reportTemplateModelType;
        }

        // Delete using an IDList (WADNR style)
        public static void DeleteReportTemplateModelType(this IQueryable<ReportTemplateModelType> reportTemplateModelTypes, List<int> reportTemplateModelTypeIDList)
        {
            if(reportTemplateModelTypeIDList.Any())
            {
                var reportTemplateModelTypesInSourceCollectionToDelete = reportTemplateModelTypes.Where(x => reportTemplateModelTypeIDList.Contains(x.ReportTemplateModelTypeID));
                foreach (var reportTemplateModelTypeToDelete in reportTemplateModelTypesInSourceCollectionToDelete.ToList())
                {
                    reportTemplateModelTypeToDelete.Delete(HttpRequestStorage.DatabaseEntities);
                }
            }
        }

        // Delete using an object list (WADNR style)
        public static void DeleteReportTemplateModelType(this IQueryable<ReportTemplateModelType> reportTemplateModelTypes, ICollection<ReportTemplateModelType> reportTemplateModelTypesToDelete)
        {
            if(reportTemplateModelTypesToDelete.Any())
            {
                var reportTemplateModelTypeIDList = reportTemplateModelTypesToDelete.Select(x => x.ReportTemplateModelTypeID).ToList();
                var reportTemplateModelTypesToDeleteFromSourceList = reportTemplateModelTypes.Where(x => reportTemplateModelTypeIDList.Contains(x.ReportTemplateModelTypeID)).ToList();

                foreach (var reportTemplateModelTypeToDelete in reportTemplateModelTypesToDeleteFromSourceList)
                {
                    reportTemplateModelTypeToDelete.Delete(HttpRequestStorage.DatabaseEntities);
                }
            }
        }

        public static void DeleteReportTemplateModelType(this IQueryable<ReportTemplateModelType> reportTemplateModelTypes, int reportTemplateModelTypeID)
        {
            DeleteReportTemplateModelType(reportTemplateModelTypes, new List<int> { reportTemplateModelTypeID });
        }

        public static void DeleteReportTemplateModelType(this IQueryable<ReportTemplateModelType> reportTemplateModelTypes, ReportTemplateModelType reportTemplateModelTypeToDelete)
        {
            DeleteReportTemplateModelType(reportTemplateModelTypes, new List<ReportTemplateModelType> { reportTemplateModelTypeToDelete });
        }
    }
}