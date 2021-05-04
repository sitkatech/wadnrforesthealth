using System;
using DocumentFormat.OpenXml.Spreadsheet;


namespace ProjectFirma.Web.Models.DhtmlxExcelDownload
{
    public class ExcelCell : IStylable
    {
        public ExcelWorksheet Worksheet { get; protected set; }
        public uint Row { get; protected set; }
        public uint Column { get; protected set; }

        internal ExcelCell(uint row, uint col, ExcelWorksheet wsheet)
        {
            this.Row = row;
            this.Column = col;
            this.Worksheet = wsheet;
        }

        public string Address
        {
            get
            {
                return RowColumn.ToAddress(this.Row, this.Column);
            }
        }

        public object Value
        {
            get
            {
                return GetValue();
            }
            set
            {
                SetValue(value);
            }
        }

       

        public ExcelStyle Style
        {
            get
            {
                uint? styleIdx = null;
                CellProxy c = this.Worksheet.GetCell(this.Row, this.Column);
                if (c != null)
                    styleIdx = c.StyleIndex;
                return new ExcelStyle(this, this.Worksheet.Document.Styles, styleIdx);
            }
            set
            {
                if (value != null)
                {
                    CellProxy c = this.Worksheet.EnsureCell(this.Row, this.Column);
                    CellFormat cfNew = this.Worksheet.Document.Styles.GetCellFormat(value.StyleIndex ?? 0);
                    c.StyleIndex = this.Worksheet.Document.Styles.MergeAndRegisterCellFormat(cfNew, c.StyleIndex, false);
                    this.Worksheet.Modified = true;
                }
                else
                {
                    CellProxy c = this.Worksheet.GetCell(this.Row, this.Column);
                    if (c != null)
                    {
                        c.StyleIndex = null;
                        this.Worksheet.Modified = true;
                    }
                }
            }
        }



        private object GetValue()
        {
            CellProxy c = this.Worksheet.GetCell(this.Row, this.Column);
            if (c != null)
            {
                if (c.DataType != null)
                {
                    CellValues cellDataType = (CellValues)c.DataType;
                    if (cellDataType == CellValues.Number)
                    {
                        return c.Value;
                    }
                    if (cellDataType == CellValues.InlineString)
                    {
                        return c.Value;
                    }
                    else if (cellDataType == CellValues.SharedString)
                    {
                        if (c.Value != null)
                            return this.Worksheet.Document.SharedStrings.Get(Convert.ToUInt32(c.Value));
                    }
                }
                if (c.StyleIndex != null)
                {
                    CellFormat cf = this.Worksheet.Document.Styles.GetCellFormat(c.StyleIndex.Value);
                    if (this.Worksheet.Document.Styles.IsDateFormat(cf))
                    {
                        if (c.Value != null)
                            return DateTime.FromOADate((double)c.Value);
                    }
                    else
                    {
                        if (c.Value != null)
                            return c.Value;
                    }
                }
                else
                {
                    if (c.Value != null)
                        return c.Value;
                }
            }
            return null;
        }

        private void SetValue(object value)
        {
            CellProxy c = this.Worksheet.EnsureCell(this.Row, this.Column);
            bool hasDateFormat = false;
            if (c.StyleIndex != null)
            {
                CellFormat cfCurrent = this.Worksheet.Document.Styles.GetCellFormat(c.StyleIndex.Value);
                if (this.Worksheet.Document.Styles.IsDateFormat(cfCurrent))
                    hasDateFormat = true;
            }

            if (value == null)
            {
                c.DataType = null;
                c.Value = null;
            }
            else
            {
                Type valueType = value.GetType();
                if (valueType == typeof(DateTime))
                {
                    c.DataType = null;
                    if (!hasDateFormat)
                    {
                        // 14 = generic date format
                        CellFormat cfDate = new CellFormat() { ApplyNumberFormat = true, NumberFormatId = 14 };
                        uint cfIdxDate = this.Worksheet.Document.Styles.MergeAndRegisterCellFormat(cfDate, c.StyleIndex, false);
                        c.StyleIndex = cfIdxDate;
                    }
                    DateTime dtValue = (DateTime)value;
                    c.Value = dtValue.ToOADate();
                }
                else if (ValueChecker.IsNumeric(valueType))
                {
                    if (hasDateFormat)
                    {
                        CellFormat cfGeneric = new CellFormat() { NumberFormatId = 0 };
                        uint cfIfxGeneric = this.Worksheet.Document.Styles.MergeAndRegisterCellFormat(cfGeneric, c.StyleIndex, false);
                        c.StyleIndex = cfIfxGeneric;
                    }
                    c.DataType = CellValues.Number;
                    c.Value = value;
                }
                else
                {
                    if (hasDateFormat)
                    {
                        CellFormat cfGeneric = new CellFormat() { NumberFormatId = 0 };
                        uint cfIfxGeneric = this.Worksheet.Document.Styles.MergeAndRegisterCellFormat(cfGeneric, c.StyleIndex, false);
                        c.StyleIndex = cfIfxGeneric;
                    }
                    string valueStr = value.ToString();
                    int sharedStrIdx = this.Worksheet.Document.SharedStrings.Put(valueStr);
                    c.DataType = CellValues.SharedString;
                    c.Value = (uint)sharedStrIdx;
                }
            }
        }
    }
}