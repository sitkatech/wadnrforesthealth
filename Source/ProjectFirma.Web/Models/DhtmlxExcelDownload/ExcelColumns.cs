using System;

namespace ProjectFirma.Web.Models.DhtmlxExcelDownload
{
    public class ExcelColumns
    {
        public ExcelWorksheet Worksheet { get; protected set; }

        internal ExcelColumns(ExcelWorksheet wsheet)
        {
            this.Worksheet = wsheet;
        }

        public ExcelColumn this[string colName]
        {
            get
            {
                return this[ExcelAddress.ColumnNameToIndex(colName)];
            }
        }

        public ExcelColumn this[uint col]
        {
            get
            {
                if (col < 1 || col > ExcelConstraints.MaxColumns)
                    throw new ArgumentException("Invalid column value");
                return new ExcelColumn(col, this.Worksheet);
            }
        }
    }
}