using LtInfo.Common;
using ProjectFirma.Web.Common;
using ProjectFirma.Web.Controllers;

namespace ProjectFirma.Web.Models
{
    public partial class GrantAllocationBudgetLineItem : IAuditableEntity
    {
        public string AuditDescriptionString => $"Grant Allocation Budget Line Item {this.GrantAllocationBudgetLineItemID} for Grant Allocation {(this.GrantAllocation != null ? this.GrantAllocation.GrantAllocationName : string.Empty)}";
    }
}