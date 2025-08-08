using System;

namespace ProjectFirma.Web.Models
{
    public partial class FundSourceAllocationSource : IAuditableEntity
    {
        public string AuditDescriptionString => $"FundSource Allocation Source FundSource Allocation Source Name: {this.FundSourceAllocationSourceName} FundSource Allocation Source ID:{this.FundSourceAllocationSourceID}";
    }
}