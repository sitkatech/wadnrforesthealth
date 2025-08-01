namespace ProjectFirma.Web.Models
{
    public partial class GrantAllocationProgramManager: IAuditableEntity
    {
        public string AuditDescriptionString => $"GrantAllocationID: {this.GrantAllocationID} - GrantAllocationProgramManagerID {this.GrantAllocationProgramManagerID} - {this.Person?.FullNameFirstLast} - {this.FundSourceAllocation?.GrantAllocationName}";
    }
}