namespace ProjectFirma.Web.Models
{
    public partial class FundSourceAllocationChangeLog : IAuditableEntity
    {
        public string AuditDescriptionString => $"FundSourceAllocationChangeLogID: {FundSourceAllocationChangeLogID} Old Value: {FundSourceAllocationAmountOldValue} New Value: {FundSourceAllocationAmountNewValue} Note: {FundSourceAllocationAmountNote}";
    }
}