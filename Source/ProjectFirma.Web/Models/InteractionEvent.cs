using LtInfo.Common;
using ProjectFirma.Web.Common;
using ProjectFirma.Web.Controllers;

namespace ProjectFirma.Web.Models
{
    public partial class InteractionEvent : IAuditableEntity
    {

        public string DateDisplay => InteractionEventDate.ToShortDateString();

        public string AuditDescriptionString => InteractionEventTitle;
    }
}