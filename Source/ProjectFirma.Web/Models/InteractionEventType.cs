using LtInfo.Common;
using ProjectFirma.Web.Common;
using ProjectFirma.Web.Controllers;

namespace ProjectFirma.Web.Models
{
    public partial class InteractionEventType : IAuditableEntity
    {
        public string AuditDescriptionString => InteractionEventTypeDisplayName;
    }
}