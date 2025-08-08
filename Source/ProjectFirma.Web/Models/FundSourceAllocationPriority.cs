using System;

namespace ProjectFirma.Web.Models
{
    public partial class FundSourceAllocationPriority : IAuditableEntity
    {
        public string AuditDescriptionString => $"FundSource Allocation Priority FundSource Allocation Priority ID: {this.FundSourceAllocationPriorityID} FundSource Allocation Priority Number: {this.FundSourceAllocationPriorityNumber}";
    }
}