using System;

namespace ProjectFirma.Web.Models
{
    public partial class GrantAllocationExpenditure : IAuditableEntity
    {
        public string AuditDescriptionString => $"{this.Biennium}{this.FiscalMonth}{this.CostTypeID}{this.GrantAllocationID}{this.GrantAllocationExpenditureID}{this.ExpenditureAmount}";
    }
}