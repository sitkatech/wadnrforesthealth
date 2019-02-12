namespace ProjectFirma.Web.Models
{
    public partial class AgreementGrantAllocation : IAuditableEntity
    {
        public string AuditDescriptionString
        {
            get { return this.GrantAllocation != null ? this.GrantAllocation.ProjectName : "NullGrantAllocation"; }
        }
    }
}