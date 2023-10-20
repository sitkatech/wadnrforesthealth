using System;

namespace ProjectFirma.Web.Models
{
    public partial class GrantAllocationPriority : IAuditableEntity
    {
        public string AuditDescriptionString => $"Grant Allocation Priority Grant Allocation Priority ID: {this.GrantAllocationPriorityID} Grant Allocation Priority Number: {this.GrantAllocationPriorityNumber}";
    }
}