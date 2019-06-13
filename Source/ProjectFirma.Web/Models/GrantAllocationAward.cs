namespace ProjectFirma.Web.Models
{
    public partial class GrantAllocationAward : IAuditableEntity
    {
        public string AuditDescriptionString => GrantAllocationAwardName;

        //todo: tom this is not right
        public decimal SpentAmount
        {
            get { return GrantAllocationAwardAmount - 100m; }
        }

        public decimal RemainingAmount => GrantAllocationAwardAmount - SpentAmount;
    }
}