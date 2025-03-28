using Newtonsoft.Json;

namespace ProjectFirma.Web.Views.Shared
{
    public class GoogleChartTextStyle
    {
        [JsonProperty(PropertyName = "italic")]
        public bool IsItalic { get; set; }
        [JsonProperty(PropertyName = "bold")]
        public bool IsBold { get; set; }
        [JsonProperty(PropertyName = "color", NullValueHandling = NullValueHandling.Ignore)]
        public string Color { get; set; }
        [JsonProperty(PropertyName = "fontName", NullValueHandling = NullValueHandling.Ignore)]
        public string FontName { get; set; }
        [JsonProperty(PropertyName = "fontSize")]
        public int FontSize { get; set; }
        [JsonProperty(PropertyName = "fontWidth")]
        public string FontWidth { get; set; }

        public GoogleChartTextStyle()
        {
            IsItalic = false;
            FontSize = 12;
            FontWidth = "normal";
        }

        public GoogleChartTextStyle(string color)
            : this()
        {
            Color = color;
        }
    }
}