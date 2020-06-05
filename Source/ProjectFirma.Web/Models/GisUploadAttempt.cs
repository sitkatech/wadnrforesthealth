using LtInfo.Common;

namespace ProjectFirma.Web.Models
{
    public partial class GisUploadAttempt : IAuditableEntity
    {
        public string AuditDescriptionString => $"{GisUploadAttemptCreateDate.ToStringDateTime()}";
    }
}