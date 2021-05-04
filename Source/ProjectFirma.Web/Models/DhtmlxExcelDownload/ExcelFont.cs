using DocumentFormat.OpenXml.Spreadsheet;


namespace ProjectFirma.Web.Models.DhtmlxExcelDownload
{
    public class ExcelFont
    {
        private DocumentStyles _styles;
        private IStylable _stylable;
        private uint? _fontId;
        internal Font FontObject { get; set; }

        internal ExcelFont(IStylable stylable, DocumentStyles styles, uint? fontId)
        {
            _stylable = stylable;
            _styles = styles;
            _fontId = fontId;
            if (_fontId != null)
                FontObject = (Font)_styles.GetFont(_fontId.Value).CloneNode(true);
            else
                FontObject = new Font();
        }




        public string Name
        {
            get
            {
                return FontObject.FontName.Val;
            }
            set
            {
                if (FontObject.FontName == null)
                    FontObject.FontName = new FontName();
                FontObject.FontName.Val = value;
                if (FontObject.FontScheme == null)
                    FontObject.FontScheme = new FontScheme();
                FontObject.FontScheme.Val = FontSchemeValues.None;
                if (_stylable != null)
                    _stylable.Style.Font = this;
            }
        }

        public double Size
        {
            get
            {
                return FontObject.FontSize.Val.Value;
            }
            set
            {
                if (FontObject.FontSize == null)
                    FontObject.FontSize = new FontSize();
                FontObject.FontSize.Val = value;
                if (_stylable != null)
                    _stylable.Style.Font = this;
            }
        }

        public bool Bold
        {
            get
            {
                return FontObject.Bold.Val;
            }
            set
            {
                if (FontObject.Bold == null)
                    FontObject.Bold = new Bold();
                FontObject.Bold.Val = value;
                if (_stylable != null)
                    _stylable.Style.Font = this;
            }
        }

        public bool Italic
        {
            get
            {
                return FontObject.Italic.Val;
            }
            set
            {
                if (FontObject.Italic == null)
                    FontObject.Italic = new Italic();
                FontObject.Italic.Val = value;
                if (_stylable != null)
                    _stylable.Style.Font = this;
            }
        }

        public string Color
        {
            get
            {

                return FontObject.Color.Rgb.Value;

            }
            set
            {
                FontObject.Color = new Color() { Rgb = value };
                if (_stylable != null)
                    _stylable.Style.Font = this;
            }
        }

    }
}