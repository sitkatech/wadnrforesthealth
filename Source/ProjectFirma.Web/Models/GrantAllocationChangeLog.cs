namespace ProjectFirma.Web.Models
{
    public partial class GrantAllocationChangeLog : IAuditableEntity
    {
        public string AuditDescriptionString => $"GrantAllocationChangeLogID: {GrantAllocationChangeLogID} Old Value: {GrantAllocationAmountOldValue} New Value: {GrantAllocationAmountNewValue} Note: {GrantAllocationAmountNote}";
    }
}