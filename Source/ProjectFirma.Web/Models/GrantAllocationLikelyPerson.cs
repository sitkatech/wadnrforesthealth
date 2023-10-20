namespace ProjectFirma.Web.Models
{
    public partial class GrantAllocationLikelyPerson : IAuditableEntity
    {
        public string AuditDescriptionString => $"Grant Allocation Likely Person: GrantAllocationID:{GrantAllocationID} - PersonID:{PersonID}";

    }
}