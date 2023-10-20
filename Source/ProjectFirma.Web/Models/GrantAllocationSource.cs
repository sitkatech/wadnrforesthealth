using System;

namespace ProjectFirma.Web.Models
{
    public partial class GrantAllocationSource : IAuditableEntity
    {
        public string AuditDescriptionString => $"Grant Allocation Source Grant Allocation Source Name: {this.GrantAllocationSourceName} Grant Allocation Source ID:{this.GrantAllocationSourceID}";
    }
}