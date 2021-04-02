using System.Linq;
using System.Collections.Generic;

namespace ProjectFirma.Web.Models
{
    public partial class TabularDataImport
    {

        public static TabularDataImport GetLatestImportProcessingForGivenType(List<TabularDataImport> tabularDataImportList, TabularDataImportTableType tabularDataImportTableType)
        {
            var latest = tabularDataImportList.Where(ip => ip.TabularDataImportTableTypeID == tabularDataImportTableType.TabularDataImportTableTypeID).
                ToList().OrderBy(ip => ip.UploadDate).
                LastOrDefault();
            return latest;
        }

    }
}