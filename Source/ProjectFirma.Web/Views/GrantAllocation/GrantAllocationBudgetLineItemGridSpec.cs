using LtInfo.Common.DhtmlWrappers;
using LtInfo.Common.ModalDialog;
using LtInfo.Common.Views;
using ProjectFirma.Web.Models;
using ProjectFirma.Web.Security;

namespace ProjectFirma.Web.Views.GrantAllocation
{
    public class GrantAllocationBudgetLineItemGridSpec : GridSpec<Models.GrantAllocationBudgetLineItem>
    {
        public GrantAllocationBudgetLineItemGridSpec(Models.Person currentPerson)
        {
            ObjectNameSingular = $"{Models.FieldDefinition.GrantAllocationBudgetLineItem.GetFieldDefinitionLabel()}";
            ObjectNamePlural = $"{Models.FieldDefinition.GrantAllocationBudgetLineItem.GetFieldDefinitionLabelPluralized()}";


            var userHasEditPermissions = new GrantAllocationBudgetLineItemEditAsAdminFeature().HasPermissionByPerson(currentPerson);
            if (userHasEditPermissions)
            {
                Add(string.Empty, x => DhtmlxGridHtmlHelpers.MakeEditIconAsModalDialogLinkBootstrap(new ModalDialogForm(x.GetEditGrantAllocationBudgetLineItemUrl(), $"Edit this {Models.FieldDefinition.GrantAllocationBudgetLineItem.GetFieldDefinitionLabel()}")), 30, DhtmlxGridColumnFilterType.None);
            }

            var userHasDeletePermissions = new GrantAllocationBudgetLineItemDeleteAsAdminFeature().HasPermissionByPerson(currentPerson);
            if (userHasDeletePermissions)
            {
                Add(string.Empty, x => DhtmlxGridHtmlHelpers.MakeDeleteIconAndLinkBootstrap(x.GetDeleteGrantAllocationBudgetLineItemUrl(), true, true), 30, DhtmlxGridColumnFilterType.None);
            }

            Add("Cost Type", x => x.CostType.CostTypeDescription, 125, DhtmlxGridColumnFilterType.Text);
            Add("Grant Allocation", x => x.GrantAllocation.GrantAllocationName, 125, DhtmlxGridColumnFilterType.Text);
            Add("Amount", x => x.GrantAllocationBudgetLineItemAmount, 70, DhtmlxGridColumnFormatType.Currency);
            Add("Note", x => x.GrantAllocationBudgetLineItemNote, 250, DhtmlxGridColumnFilterType.Text);
           
        }
    }
}