using System;

namespace ProjectFirma.Web.Models
{
    public partial class GrantAllocationSource : IAuditableEntity
    {
        public string AuditDescriptionString => $"Grant Allocation Source {this.GrantAllocationSourceName}{this.GrantAllocationSourceID}";
    }
}