namespace ProjectFirma.Web.Models
{
    public partial class Grant
    {
        public string StartDateDisplay => StartDate.HasValue ? StartDate.Value.ToShortDateString() : string.Empty;
        public string EndDateDisplay => EndDate.HasValue ? EndDate.Value.ToShortDateString() : string.Empty;
        public string GrantTypeDisplay => GrantTypeID.HasValue ? GrantType.GrantTypeName : string.Empty;
    }
}