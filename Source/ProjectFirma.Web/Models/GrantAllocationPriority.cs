using System;

namespace ProjectFirma.Web.Models
{
    public partial class GrantAllocationPriority : IAuditableEntity
    {
        public string AuditDescriptionString => $"Grant Allocation Priority {this.GrantAllocationPriorityID}{this.GrantAllocationPriorityNumber}";
    }
}