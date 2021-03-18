using System;
using LtInfo.Common;

namespace ProjectFirma.Web.Models.ExcelUpload
{
    /// <summary>
    /// An exception generated when a cell value is bad
    /// </summary>
    public class ExcelImportBadCellException : SitkaDisplayErrorException
    {
        public string ColumnName;
        public int RowIndex;
        public string CellValue;

        public ExcelImportBadCellException(string columnName, int rowIndex, string cellValue, string errorMessage, Exception innerException) : base(string.Format("{0} - Cell {1}{2} - Cell Value \"{3}\"", errorMessage, columnName, rowIndex + 1, cellValue), innerException)
        {
            ColumnName = columnName;
            CellValue = cellValue;
            RowIndex = rowIndex;
        }
    }
}