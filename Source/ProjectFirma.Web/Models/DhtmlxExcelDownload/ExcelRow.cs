using System.Xml;
using DocumentFormat.OpenXml.Spreadsheet;

namespace ProjectFirma.Web.Models.DhtmlxExcelDownload
{
    public class ExcelRow : IStylable
    {
        private uint _row;
        private ExcelWorksheet _wsheet;

        internal ExcelRow(uint row, ExcelWorksheet wsheet)
        {
            _row = row;
            _wsheet = wsheet;
        }

        public uint Row
        {
            get
            {
                return _row;
            }
        }

        public double? Height
        {
            get
            {
                Row r = _wsheet.GetRow(_row);
                if (r != null)
                    return r.Height;
                return null;
            }
            set
            {
                if (value != null)
                {
                    Row r = _wsheet.EnsureRow(_row);
                    r.Height = value.Value;
                    r.CustomHeight = true;
                }
                else
                {
                    Row r = _wsheet.EnsureRow(_row);
                    r.Height = null;
                    r.CustomHeight = (bool?)null;
                }
                _wsheet.Modified = true;
            }
        }

        public bool Hidden
        {
            get
            {
                Row r = _wsheet.GetRow(_row);
                if (r != null && r.Hidden.HasValue)
                    return r.Hidden.Value;
                return false;
            }
            set
            {
                Row r = _wsheet.EnsureRow(_row);
                r.Hidden = value;
                _wsheet.Modified = true;
            }
        }

        public ExcelStyle Style
        {
            get
            {
                uint? styleIdx = null;
                Row r = _wsheet.GetRow(_row);
                if (r != null && r.StyleIndex != null)
                    styleIdx = r.StyleIndex;
                return new ExcelStyle(this, _wsheet.Document.Styles, styleIdx);
            }
            set
            {
                if (value != null)
                {
                    Row r = _wsheet.EnsureRow(_row);
                    CellFormat cfNew = _wsheet.Document.Styles.GetCellFormat(value.StyleIndex ?? 0);
                    r.StyleIndex = _wsheet.Document.Styles.MergeAndRegisterCellFormat(cfNew, r.StyleIndex, false);
                    r.CustomFormat = true;
                    _wsheet.Modified = true;
                }
                else
                {
                    Row r = _wsheet.GetRow(_row);
                    if (r != null)
                    {
                        r.StyleIndex = null;
                        r.CustomFormat = false;
                        _wsheet.Modified = true;
                    }
                }
            }
        }
    }
}