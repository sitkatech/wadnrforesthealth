namespace ProjectFirma.Web.Models
{
    public partial class PriorityLandscapeCategory
    {

        public string GetPriorityLandscapeMapLayerIconImagePath()
        {
            return $"/Content/leaflet/images/washington_priority_landscape-{this.PriorityLandscapeCategoryName}.png";
        }

    }
}