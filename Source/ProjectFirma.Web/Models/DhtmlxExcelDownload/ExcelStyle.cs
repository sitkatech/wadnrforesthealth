using DocumentFormat.OpenXml.Spreadsheet;
using OpenExcel.OfficeOpenXml.Style;

namespace ProjectFirma.Web.Models.DhtmlxExcelDownload
{
    public class ExcelStyle
    {
        private DocumentStyles _styles;
        private IStylable _stylable;

        internal ExcelStyle(IStylable stylable, DocumentStyles styles, uint? baseStyleIndex)
        {
            _stylable = stylable;
            _styles = styles;
            this.StyleIndex = baseStyleIndex;
        }

        internal uint? StyleIndex { get; set; }

        public ExcelNumberFormat NumberFormat
        {
            get
            {
                if (this.StyleIndex != null)
                {
                    CellFormat cf = _styles.GetCellFormat(this.StyleIndex.Value);
                    if (cf.NumberFormatId != null)
                    {
                        return new ExcelNumberFormat(_stylable, _styles, cf.NumberFormatId);
                    }
                }
                return new ExcelNumberFormat(_stylable, _styles, 0);
            }
            set
            {
                this.StyleIndex = _styles.MergeAndRegisterCellFormat(new CellFormat() { NumberFormatId = value.NumFmtId, ApplyNumberFormat = true }, this.StyleIndex, false);
                if (_stylable != null)
                    _stylable.Style = this;
            }
        }

        public ExcelFont Font
        {
            get
            {
                CellFormat cf = _styles.GetCellFormat(this.StyleIndex ?? 0);
                uint fontId = cf.FontId ?? 0;
                return new ExcelFont(_stylable, _styles, fontId);
            }
            set
            {
                CellFormat cf = _styles.GetCellFormat(this.StyleIndex ?? 0);
                uint fontId = cf.FontId ?? 0;
                uint newFontId = _styles.MergeAndRegisterFont(value.FontObject, fontId, false);
                if (newFontId != fontId)
                {
                    this.StyleIndex = _styles.MergeAndRegisterCellFormat(new CellFormat() { FontId = newFontId, ApplyFont = true }, this.StyleIndex, false);
                    if (_stylable != null)
                        _stylable.Style = this;
                }
            }
        }
        public void ApplySettings(Font font, Fill fill, params ExcelBorder[] borders)
        {


        }
        public uint GetBorderId()
        {
            CellFormat cf = _styles.GetCellFormat(this.StyleIndex ?? 0);
            return cf.BorderId ?? 0;
        }
        public ExcelBorder Border
        {
            get
            {
                CellFormat cf = _styles.GetCellFormat(this.StyleIndex ?? 0);
                uint borderId = cf.BorderId ?? 0;
                return new ExcelBorder(_stylable, _styles, borderId);
            }
            set
            {
                CellFormat cf = _styles.GetCellFormat(this.StyleIndex ?? 0);
                uint borderId = cf.BorderId ?? 0;
                uint newBorderId = _styles.MergeAndRegisterBorder(value.BorderObject, borderId, false);
                if (newBorderId != borderId)
                {
                    this.StyleIndex = _styles.MergeAndRegisterCellFormat(new CellFormat() { BorderId = newBorderId, ApplyBorder = true }, this.StyleIndex, false);
                    if (_stylable != null)
                        _stylable.Style = this;
                }
            }
        }

        public ExcelFill Fill
        {
            get
            {
                CellFormat cf = _styles.GetCellFormat(this.StyleIndex ?? 0);
                uint fillId = cf.FillId ?? 0;
                return new ExcelFill(_stylable, _styles, fillId);
            }
            set
            {
                CellFormat cf = _styles.GetCellFormat(this.StyleIndex ?? 0);
                uint fillId = cf.FillId ?? 0;
                uint newFillId = _styles.MergeAndRegisterFill(value.FillObject, fillId, false);
                if (newFillId != fillId)
                {
                    this.StyleIndex = _styles.MergeAndRegisterCellFormat(new CellFormat() { FillId = newFillId, ApplyFill = true }, this.StyleIndex, false);
                    if (_stylable != null)
                        _stylable.Style = this;
                }
            }
        }
    }
}