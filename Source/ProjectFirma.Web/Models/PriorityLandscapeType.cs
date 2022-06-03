namespace ProjectFirma.Web.Models
{
    public partial class PriorityLandscapeType
    {

        public string GetPriorityLandscapeMapLayerIconImagePath()
        {
            return $"/Content/leaflet/images/washington_priority_landscape-{this.PriorityLandscapeTypeName}.png";
        }

    }
}