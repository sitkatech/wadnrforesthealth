using System.Collections.Generic;
using System.Linq;
using DocumentFormat.OpenXml.Spreadsheet;


namespace ProjectFirma.Web.Models.DhtmlxExcelDownload
{
    public class ExcelNumberFormat
    {
        private static Dictionary<uint, string> _builtInFormats_Global = new Dictionary<uint, string>()
        {
            {0, "General"},
            {1, "0"},
            {2, "0.00"},
            {3, "#,##0"},
            {4, "#,##0.00"},
            {9, "0%"},
            {10, "0.00%"},
            {11, "0.00E+00"},
            {12, "# ?/?"},
            {13, "# ??/??"},
            {14, "m/d/yyyy"},
            {15, "d-mmm-yy"},
            {16, "d-mmm"},
            {17, "mmm-yy"},
            {18, "h:mm AM/PM"},
            {19, "h:mm:ss AM/PM"},
            {20, "h:mm"},
            {21, "h:mm:ss"},
            {22, "m/d/yy h:mm"},
            {37, "#,##0 ;(#,##0)"},
            {38, "#,##0 ;[Red](#,##0)"},
            {39, "#,##0.00;(#,##0.00)"},
            {40, "#,##0.00;[Red](#,##0.00)"},
            {45, "mm:ss"},
            {46, "[h]:mm:ss"},
            {47, "mmss.0"},
            {48, "##0.0E+0"},
            {49, "@"}
        };

        private DocumentStyles _styles;
        private IStylable _stylable;

        internal uint NumFmtId { get; set; }

        internal ExcelNumberFormat(IStylable stylable, DocumentStyles styles, uint numFmtId)
        {
            _stylable = stylable;
            _styles = styles;
            NumFmtId = numFmtId;
        }

        public string Format
        {
            get
            {
                if (_builtInFormats_Global.ContainsKey(NumFmtId))
                    return _builtInFormats_Global[NumFmtId];
                NumberingFormat numFmt = _styles.GetNumberingFormat(NumFmtId);
                return numFmt.FormatCode;
            }
            set
            {
                uint newNumFmtId;
                KeyValuePair<uint, string> builtInFmt = (from i in _builtInFormats_Global
                                                         where i.Value == value
                                                         select i).FirstOrDefault();
                if (builtInFmt.Value == value)
                {
                    newNumFmtId = builtInFmt.Key;
                }
                else
                {
                    NumberingFormat numFmt = new NumberingFormat() { FormatCode = value };
                    newNumFmtId = _styles.EnsureCustomNumberingFormat(numFmt);
                }
                if (newNumFmtId != NumFmtId)
                {
                    NumFmtId = newNumFmtId;
                    if (_stylable != null)
                        _stylable.Style.NumberFormat = this;
                }
            }
        }
    }
}