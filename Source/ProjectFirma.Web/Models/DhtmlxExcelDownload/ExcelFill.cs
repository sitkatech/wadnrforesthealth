using DocumentFormat.OpenXml.Spreadsheet;

namespace ProjectFirma.Web.Models.DhtmlxExcelDownload
{
    public class ExcelFill
    {
        private DocumentStyles _styles;
        private IStylable _stylable;
        private uint? _fillId;
        internal Fill FillObject { get; set; }

        internal ExcelFill(IStylable stylable, DocumentStyles styles, uint? fillId)
        {
            _stylable = stylable;
            _styles = styles;
            _fillId = fillId;
            if (_fillId != null)
                FillObject = (Fill)_styles.GetFill(_fillId.Value).CloneNode(true);
            else
                FillObject = new Fill();
        }

        public string ForegroundColor
        {
            get
            {
                if (FillObject.PatternFill != null)
                {
                    return FillObject.PatternFill.ForegroundColor.Rgb.ToString();
                }
                return null;
            }
            set
            {
                EnsurePatternFill();
                FillObject.PatternFill.PatternType = PatternValues.Solid;
                FillObject.PatternFill.ForegroundColor = new ForegroundColor()
                {
                    Rgb = new DocumentFormat.OpenXml.HexBinaryValue(value)
                };
                if (_stylable != null)
                    _stylable.Style.Fill = this;
            }
        }

        public string BackgroundColor
        {
            get
            {
                if (FillObject.PatternFill != null)
                {
                    return FillObject.PatternFill.BackgroundColor.Rgb.ToString();
                }
                return null;
            }
            set
            {
                EnsurePatternFill();
                FillObject.PatternFill.PatternType = PatternValues.Solid;
                FillObject.PatternFill.BackgroundColor = new BackgroundColor()
                {
                    Rgb = new DocumentFormat.OpenXml.HexBinaryValue(value)
                };
                if (_stylable != null)
                    _stylable.Style.Fill = this;
            }
        }

        private void EnsurePatternFill()
        {
            if (FillObject.GradientFill != null)
                FillObject.GradientFill.Remove();
            if (FillObject.PatternFill == null)
                FillObject.PatternFill = new PatternFill();
        }
    }
}