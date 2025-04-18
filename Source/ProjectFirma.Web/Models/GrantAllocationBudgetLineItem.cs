using LtInfo.Common;
using ProjectFirma.Web.Common;
using ProjectFirma.Web.Controllers;

namespace ProjectFirma.Web.Models
{
    public partial class GrantAllocationBudgetLineItem : IAuditableEntity
    {
        public string AuditDescriptionString => $" {FieldDefinition.GrantAllocation.GetFieldDefinitionLabel()} Budget Line Item {this.GrantAllocationBudgetLineItemID} for {FieldDefinition.GrantAllocation.GetFieldDefinitionLabel()} {(this.GrantAllocation != null ? this.GrantAllocation.GrantAllocationName : string.Empty)}";
    }

}