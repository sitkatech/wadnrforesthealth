using System;

namespace ProjectFirma.Web.Models.DhtmlxExcelDownload
{
    public class ExcelCells
    {
        public ExcelWorksheet Worksheet { get; protected set; }

        internal ExcelCells(ExcelWorksheet wsheet)
        {
            this.Worksheet = wsheet;
        }

        public ExcelCell this[string address]
        {
            get
            {
                RowColumn rc = ExcelAddress.ToRowColumn(address);
                return this[rc.Row, rc.Column];
            }
        }

        public ExcelCell this[uint row, uint col]
        {
            get
            {
                if (row < 1 || row > ExcelConstraints.MaxRows)
                    throw new ArgumentException("Invalid row value");
                if (col < 1 || col > ExcelConstraints.MaxColumns)
                    throw new ArgumentException("Invalid column value");
                return new ExcelCell(row, col, this.Worksheet);
            }
        }
    }
}