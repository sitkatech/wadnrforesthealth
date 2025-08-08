using LtInfo.Common;
using ProjectFirma.Web.Common;
using ProjectFirma.Web.Controllers;

namespace ProjectFirma.Web.Models
{
    public partial class FundSourceAllocationBudgetLineItem : IAuditableEntity
    {
        public string AuditDescriptionString => $" {FieldDefinition.FundSourceAllocation.GetFieldDefinitionLabel()} Budget Line Item {this.FundSourceAllocationBudgetLineItemID} for {FieldDefinition.FundSourceAllocation.GetFieldDefinitionLabel()} {(this.FundSourceAllocation != null ? this.FundSourceAllocation.FundSourceAllocationName : string.Empty)}";
    }

}