﻿using System.Collections.Generic;
using System.IO;
using LtInfo.Common.ExcelWorkbookUtilities;
using ProjectFirma.Web.Models.ExcelUpload;

namespace ProjectFirma.Web.Views.ExcelUpload
{
    public class LoaStageImportsHelper
    {
        #region New Unexpended Balance Version
        public static LoaStageImports LoadLoaStagesFromXlsFile(Stream stream, int headerRowOffset, List<string> errorList)
        {
            var dataTable = OpenXmlSpreadSheetDocument.ExcelWorksheetToDataTable(stream, LoaStageImports.FbmsUnexpendedPayrecV3SheetName, LoaStageImports.UseExistingSheetNameIfSingleSheetFound, headerRowOffset);
            return LoaStageImports.LoadFromXlsFile(dataTable, errorList);
        }

        #endregion New Unexpended Balance Version

    }
}