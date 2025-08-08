namespace ProjectFirma.Web.Models
{
    public partial class FundSourceAllocationProgramManager: IAuditableEntity
    {
        public string AuditDescriptionString => $"FundSourceAllocationID: {this.FundSourceAllocationID} - FundSourceAllocationProgramManagerID {this.FundSourceAllocationProgramManagerID} - {this.Person?.FullNameFirstLast} - {this.FundSourceAllocation?.FundSourceAllocationName}";
    }
}