using DocumentFormat.OpenXml.Spreadsheet;


namespace ProjectFirma.Web.Models.DhtmlxExcelDownload
{
    public class ExcelBorder
    {
        private DocumentStyles _styles;
        private IStylable _stylable;
        private uint? _borderId;
        internal Border BorderObject { get; set; }

        public ExcelBorder(IStylable stylable, DocumentStyles styles, uint? borderId)
        {
            _stylable = stylable;
            _styles = styles;
            _borderId = borderId;
            if (_borderId != null)
                BorderObject = (Border)_styles.GetBorder(_borderId.Value).CloneNode(true);
            else
                BorderObject = new Border();
        }

        public ExcelBorderStyleValues LeftStyle
        {
            get
            {
                return GetBorderStyle(BorderObject.LeftBorder);
            }
            set
            {
                SetBorderStyle(BorderObject.LeftBorder, value);
            }
        }

        public ExcelBorderStyleValues RightStyle
        {
            get
            {
                return GetBorderStyle(BorderObject.RightBorder);
            }
            set
            {
                SetBorderStyle(BorderObject.RightBorder, value);
            }
        }

        public ExcelBorderStyleValues TopStyle
        {
            get
            {
                return GetBorderStyle(BorderObject.TopBorder);
            }
            set
            {
                SetBorderStyle(BorderObject.TopBorder, value);
            }
        }

        public string BottomColor
        {
            get
            {
                return GetBorderColor(BorderObject.BottomBorder) != null ? GetBorderColor(BorderObject.BottomBorder).Rgb.Value : "";
            }
            set
            {
                SetBorderColor(BorderObject.BottomBorder, new Color() { Rgb = value });
            }
        }
        public string TopColor
        {
            get
            {
                return GetBorderColor(BorderObject.TopBorder) != null ? GetBorderColor(BorderObject.TopBorder).Rgb.Value : "";
            }
            set
            {
                SetBorderColor(BorderObject.TopBorder, new Color() { Rgb = value });
            }
        }
        public string LeftColor
        {
            get
            {
                return GetBorderColor(BorderObject.LeftBorder) != null ? GetBorderColor(BorderObject.LeftBorder).Rgb.Value : "";
            }
            set
            {
                SetBorderColor(BorderObject.LeftBorder, new Color() { Rgb = value });
            }
        }
        public string RightColor
        {
            get
            {
                return GetBorderColor(BorderObject.RightBorder) != null ? GetBorderColor(BorderObject.RightBorder).Rgb.Value : "";
            }
            set
            {
                SetBorderColor(BorderObject.RightBorder, new Color() { Rgb = value });
            }
        }



        public ExcelBorderStyleValues BottomStyle
        {
            get
            {
                return GetBorderStyle(BorderObject.BottomBorder);
            }
            set
            {
                SetBorderStyle(BorderObject.BottomBorder, value);
            }
        }

        private ExcelBorderStyleValues GetBorderStyle(BorderPropertiesType b)
        {
            return (ExcelBorderStyleValues)b.Style.Value;
        }

        private void SetBorderColor(BorderPropertiesType b, Color val)
        {
            b.Color = val;
            if (_stylable != null)
                _stylable.Style.Border = this;
        }
        private Color GetBorderColor(BorderPropertiesType b)
        {
            return b.Color;

        }
        private void SetBorderStyle(BorderPropertiesType b, ExcelBorderStyleValues val)
        {

            b.Style = (BorderStyleValues)val;

            if (_stylable != null)
                _stylable.Style.Border = this;
        }
    }
}