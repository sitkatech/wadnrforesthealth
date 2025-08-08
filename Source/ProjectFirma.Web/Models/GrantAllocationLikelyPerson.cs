namespace ProjectFirma.Web.Models
{
    public partial class FundSourceAllocationLikelyPerson : IAuditableEntity
    {
        public string AuditDescriptionString => $"FundSource Allocation Likely Person: FundSourceAllocationID:{FundSourceAllocationID} - PersonID:{PersonID}";

    }
}