using System;
using System.Xml;

namespace ProjectFirma.Web.Models.DhtmlxExcelDownload
{
    public struct RowColumn
    {
        public uint Row { get; set; }
        public uint Column { get; set; }

        /// <summary>
        /// Convert to Excel address format, e.g. Row=5, Col=3 --> C5
        /// </summary>
        /// <param name="row"></param>
        /// <param name="col"></param>
        /// <returns></returns>
        public static string ToAddress(uint row, uint col)
        {
            if (row < 1 || row > ExcelConstraints.MaxRows)
                throw new ArgumentException("Invalid row value");
            if (col < 1 || col > ExcelConstraints.MaxColumns)
                throw new ArgumentException("Invalid column value");
            return ExcelAddress.ColumnIndexToName(col) + (row);
        }

        /// <summary>
        /// Convert to Excel address format, e.g. Row=5, Col=3 --> C5
        /// </summary>
        /// <returns></returns>
        public string ToAddress()
        {
            if (this.Row < 1 || this.Row > ExcelConstraints.MaxRows)
                throw new ArgumentException("Invalid row value");
            if (this.Column < 1 || this.Column > ExcelConstraints.MaxColumns)
                throw new ArgumentException("Invalid column value");
            return ExcelAddress.ColumnIndexToName(this.Column) + (this.Row);
        }
    }
}