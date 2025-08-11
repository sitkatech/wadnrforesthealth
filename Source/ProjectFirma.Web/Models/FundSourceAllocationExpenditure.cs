using System;

namespace ProjectFirma.Web.Models
{
    public partial class FundSourceAllocationExpenditure : IAuditableEntity
    {
        public string AuditDescriptionString => $"{this.Biennium}{this.FiscalMonth}{this.CostTypeID}{this.FundSourceAllocationID}{this.FundSourceAllocationExpenditureID}{this.ExpenditureAmount}";
    }
}