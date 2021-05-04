using System;

namespace ProjectFirma.Web.Models.DhtmlxExcelDownload
{
    public class ExcelRows
    {
        public ExcelWorksheet Worksheet { get; protected set; }

        internal ExcelRows(ExcelWorksheet wsheet)
        {
            this.Worksheet = wsheet;
        }

        public ExcelRow this[uint row]
        {
            get
            {
                if (row < 1 || row > ExcelConstraints.MaxRows)
                    throw new ArgumentException("Invalid row value");
                return new ExcelRow(row, this.Worksheet);
            }
        }
    }
}