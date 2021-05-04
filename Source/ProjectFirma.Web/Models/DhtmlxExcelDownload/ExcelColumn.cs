using System.Xml;
using DocumentFormat.OpenXml.Spreadsheet;

namespace ProjectFirma.Web.Models.DhtmlxExcelDownload
{
    public class ExcelColumn : IStylable
    {
        private uint _col;
        private ExcelWorksheet _wsheet;

        internal ExcelColumn(uint col, ExcelWorksheet wsheet)
        {
            _col = col;
            _wsheet = wsheet;
        }

        public uint Column
        {
            get
            {
                return _col;
            }
        }

        public double? Width
        {
            get
            {
                Column c = _wsheet.GetColumnDefinition(_col);
                if (c != null)
                    return c.Width;
                return null;
            }
            set
            {
                if (value != null)
                {
                    Column c = _wsheet.EnsureColumnDefinition(_col);
                    c.Width = value.Value;
                    c.CustomWidth = true;
                }
                else
                {
                    Column c = _wsheet.EnsureColumnDefinition(_col);
                    _wsheet.DeleteColumnDefinition(_col);
                }
                _wsheet.Modified = true;
            }
        }

        public bool Hidden
        {
            get
            {
                Column c = _wsheet.GetColumnDefinition(_col);
                if (c != null && c.Hidden.HasValue)
                    return c.Hidden.Value;
                return false;
            }
            set
            {
                Column c = _wsheet.EnsureColumnDefinition(_col);
                c.Hidden = value;
                _wsheet.Modified = true;
            }
        }

        public ExcelStyle Style
        {
            get
            {
                uint? styleIdx = null;
                Column col = _wsheet.GetColumnDefinition(_col);
                if (col != null)
                    styleIdx = col.Style;
                return new ExcelStyle(this, _wsheet.Document.Styles, styleIdx);
            }
            set
            {
                if (value != null)
                {
                    Column col = _wsheet.EnsureColumnDefinition(_col);
                    CellFormat cfNew = _wsheet.Document.Styles.GetCellFormat(value.StyleIndex ?? 0);
                    col.Style = _wsheet.Document.Styles.MergeAndRegisterCellFormat(cfNew, col.Style, false);
                    _wsheet.Modified = true;
                }
                else
                {
                    Column col = _wsheet.EnsureColumnDefinition(_col);
                    col.Style = null;
                    _wsheet.Modified = true;
                }
            }
        }
    }
}