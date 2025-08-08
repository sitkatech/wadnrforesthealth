namespace ProjectFirma.Web.Models
{
    public partial class FundSourceAllocationLikelyPerson : IAuditableEntity
    {
        public string AuditDescriptionString => $"Fund Source Allocation Likely Person: FundSourceAllocationID:{FundSourceAllocationID} - PersonID:{PersonID}";

    }
}