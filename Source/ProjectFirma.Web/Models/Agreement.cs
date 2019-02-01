using LtInfo.Common.Models;

namespace ProjectFirma.Web.Models
{
    public partial class Agreement : IAuditableEntity
    {
        public string StartDateDisplay => StartDate.HasValue ? StartDate.Value.ToShortDateString() : string.Empty;
        public string EndDateDisplay => EndDate.HasValue ? EndDate.Value.ToShortDateString() : string.Empty;
        public string AuditDescriptionString => AgreementTitle;
       
    }
}